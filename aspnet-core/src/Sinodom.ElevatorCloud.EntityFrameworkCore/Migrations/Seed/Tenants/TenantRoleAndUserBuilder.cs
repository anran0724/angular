using System.Linq;
using Abp;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.Authorization.Roles;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Notifications;

namespace Sinodom.ElevatorCloud.Migrations.Seed.Tenants
{
    using System;

    using Sinodom.ElevatorCloud.MultiTenancy;
    using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;

    public class TenantRoleAndUserBuilder
    {
        private readonly ElevatorCloudDbContext _context;
        private readonly int _tenantId;
        private readonly bool _isCompanyUser;

        public TenantRoleAndUserBuilder(ElevatorCloudDbContext context, int tenantId, bool isCompanyUser = false)
        {
            _context = context;
            _tenantId = tenantId;
            _isCompanyUser = isCompanyUser;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            // Admin role
            var adminRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true }).Entity;
                _context.SaveChanges();
            }

            // User role
            var userRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.User);
            if (userRole == null)
            {
                _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.User, StaticRoleNames.Tenants.User) { IsStatic = true, IsDefault = true });
                _context.SaveChanges();
            }

            var tenant = this._context.Tenants.IgnoreQueryFilters().FirstOrDefault(r => r.Id == this._tenantId);
            if (tenant != null)
            {
                if (tenant.TenancyName == Tenant.DefaultMaintenanceTenantName)
                {
                    // Maintenance manage role
                    this.CreateCompanyRole(StaticRoleNames.MaintenanceTenants.MainManage, StaticRoleNames.MaintenanceTenants.MainManageDisplayName);

                    // Maintenance info manage role
                    this.CreateCompanyRole(StaticRoleNames.MaintenanceTenants.MainInfoManage, StaticRoleNames.MaintenanceTenants.MainInfoManageDisplayName);

                    // Maintenance principal role
                    this.CreateCompanyRole(StaticRoleNames.MaintenanceTenants.MaintPrincipal, StaticRoleNames.MaintenanceTenants.MaintPrincipalDisplayName);

                    // Maintenance user role
                    this.CreateCompanyRole(StaticRoleNames.MaintenanceTenants.MainUser, StaticRoleNames.MaintenanceTenants.MainUserDisplayName);
                }

                if (tenant.TenancyName == Tenant.DefaultPropertyTenantName)
                {
                    // Use info manage role
                    this.CreateCompanyRole(StaticRoleNames.UseTenants.UseInfoManage, StaticRoleNames.UseTenants.UseInfoManageDisplayName);

                    // Use manage role
                    this.CreateCompanyRole(StaticRoleNames.UseTenants.UseManage, StaticRoleNames.UseTenants.UseManageDisplayName);

                    // Use leader role
                    this.CreateCompanyRole(StaticRoleNames.UseTenants.UseLeader, StaticRoleNames.UseTenants.UseLeaderDisplayName);

                    // Use user role
                    this.CreateCompanyRole(StaticRoleNames.UseTenants.UseUser, StaticRoleNames.UseTenants.UseUserDisplayName);
                }
            }


            // admin user
            var adminUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com");
                adminUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(adminUser, "123qwe");
                adminUser.IsEmailConfirmed = true;
                adminUser.ShouldChangePasswordOnNextLogin = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                //Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();

                //User account of admin user
                //if (_tenantId == 1)
                //{
                    _context.UserAccounts.Add(new UserAccount
                    {
                        TenantId = _tenantId,
                        UserId = adminUser.Id,
                        UserName = AbpUserBase.AdminUserName,
                        EmailAddress = adminUser.EmailAddress
                    });
                    _context.SaveChanges();
                //}

                //Notification subscription
                _context.NotificationSubscriptions.Add(new NotificationSubscriptionInfo(SequentialGuidGenerator.Instance.Create(), _tenantId, adminUser.Id, AppNotificationNames.NewUserRegistered));
                _context.SaveChanges();
            }

            if (this._isCompanyUser)
            {
                var companyUser = this._context.EccpCompanyUserExtensions.IgnoreQueryFilters().FirstOrDefault(u => u.UserId == adminUser.Id);

                if (companyUser == null)
                {
                    this._context.EccpCompanyUserExtensions.Add(
                        new EccpCompanyUserExtension
                            {
                                UserId = adminUser.Id,
                                IdCard = RandomHelper.GetRandom(100000, 999999).ToString(),
                                Mobile = RandomHelper.GetRandom(13333333, 99999999).ToString() + RandomHelper.GetRandom(10, 99),
                                SignPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                                CertificateFrontPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                                CertificateBackPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                                ExpirationDate = new DateTime(2025, 01, 01)
                            });
                }
            }
        }

        private void CreateCompanyRole(string roleName, string roleDisplayName)
        {
            var roleEntity = this._context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == this._tenantId && r.Name == roleName);
            if (roleEntity == null)
            {
                this._context.Roles.Add(new Role(this._tenantId, roleName, roleDisplayName) { IsStatic = true, IsDefault = false });
                this._context.SaveChanges();
            }
        }
    }
}
