namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    using System;
    using System.Linq;

    using Abp.Authorization.Users;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    using Sinodom.ElevatorCloud.Authorization.Roles;
    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.EntityFrameworkCore;
    using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;

    public class TestEccpUsersBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        private readonly int _tenantId;
        private readonly int _maintenanceTenantId;
        private readonly int _propertyTenantId;

        public TestEccpUsersBuilder(ElevatorCloudDbContext context, int tenantId, int maintenanceTenantId, int propertyTenantId)
        {
            this._context = context;
            this._tenantId = tenantId;
            this._maintenanceTenantId = maintenanceTenantId;
            this._propertyTenantId = propertyTenantId;
        }

        public void Create()
        {
            this.CreateCompanyUser("TestMaintenanceUser1", "TestMaintenanceUser1@dianti119.com", true, this._maintenanceTenantId);
            this.CreateCompanyUser("TestMaintenanceUser2", "TestMaintenanceUser2@dianti119.com", false, this._maintenanceTenantId);
            this.CreateCompanyUser("TestMaintenanceUser3", "TestMaintenanceUser3@dianti119.com", true, this._maintenanceTenantId, StaticRoleNames.MaintenanceTenants.MainManage, StaticRoleNames.MaintenanceTenants.MainManageDisplayName);

            this.CreateCompanyUser("TestPropertyUser1", "TestPropertyUser1@dianti119.com", true, this._propertyTenantId);
            this.CreateCompanyUser("TestPropertyUser2", "TestPropertyUser2@dianti119.com", false, this._propertyTenantId);
            this.CreateCompanyUser("TestPropertyUser3", "TestPropertyUser3@dianti119.com", true, this._propertyTenantId, StaticRoleNames.UseTenants.UseManage, StaticRoleNames.UseTenants.UseManageDisplayName);
        }

        private void CreateCompanyUser(string userName, string email, bool isActive, int tenantId, string roleName = null, string roleDisplayName = null)
        {
            if (roleName == null)
            {
                roleName = StaticRoleNames.Tenants.Admin;
                roleDisplayName = StaticRoleNames.Tenants.Admin;
            }

            var userRole = this._context.Roles.IgnoreQueryFilters().FirstOrDefault(
                r => r.TenantId == tenantId && r.Name == roleName);
            if (userRole == null)
            {
                this._context.Roles.Add(
                    new Role(tenantId, roleName, roleDisplayName)
                        {
                            IsStatic = true, IsDefault = true
                        });
                this._context.SaveChanges();

                userRole = this._context.Roles.IgnoreQueryFilters().FirstOrDefault(
                    r => r.TenantId == tenantId && r.Name == roleName);
            }

            var user = new User
                           {
                               TenantId = tenantId,
                               UserName = userName,
                               Name = userName,
                               Surname = userName,
                               EmailAddress = email
                           };

            user.SetNormalizedNames();

            user.Password =
                new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions()))
                    .HashPassword(user, "123qwe");
            user.IsEmailConfirmed = true;
            user.ShouldChangePasswordOnNextLogin = true;
            user.IsActive = isActive;

            this._context.Users.Add(user);
            this._context.SaveChanges();

            this._context.EccpCompanyUserExtensions.Add(
                new EccpCompanyUserExtension
                    {
                        SignPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                        CertificateFrontPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                        CertificateBackPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                        ExpirationDate = DateTime.Now,
                        CheckState = isActive ? 1 : 0,
                        IdCard = "10000",
                        Mobile = "13812312312",
                        UserId = user.Id
                    });

            // Assign User role to user
            this._context.UserRoles.Add(new UserRole(tenantId, user.Id, userRole.Id));

            this._context.SaveChanges();
        }
    }
}