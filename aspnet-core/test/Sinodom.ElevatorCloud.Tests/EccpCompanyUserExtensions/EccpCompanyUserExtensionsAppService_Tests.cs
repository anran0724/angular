// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpCompanyUserExtensionsAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpCompanyUserExtensions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization.Users;

    using Shouldly;

    using Sinodom.ElevatorCloud.Authorization.Roles;
    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.Authorization.Users.Dto;
    using Sinodom.ElevatorCloud.MultiTenancy;
    using Sinodom.ElevatorCloud.Tests.Authorization.Users;

    /// <summary>
    /// The eccp company user extensions app service_ tests.
    /// </summary>
    public class EccpCompanyUserExtensionsAppService_Tests : UserAppServiceTestBase
    {
        /// <summary>
        /// The _association use app servicer.
        /// </summary>
        private readonly IEccpCompanyUserExtensionsAppServicer _associationUseAppServicer;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpCompanyUserExtensionsAppService_Tests"/> class.
        /// </summary>
        public EccpCompanyUserExtensionsAppService_Tests()
        {
            this.LoginAsTenant(Tenant.DefaultMaintenanceTenantName, AbpUserBase.AdminUserName);
            this._associationUseAppServicer = this.Resolve<IEccpCompanyUserExtensionsAppServicer>();
        }

        /// <summary>
        /// The should_ get_ company user extensions.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_CompanyUserExtensions()
        {
            var entities = await this._associationUseAppServicer.GetUsers(
                               new GetAssociationUsersInput { Filter = "13812312312" });
            entities.Items.Count.ShouldBe(2);
            entities.Items[0].Mobile.ShouldBe("13812312312");
        }

        /// <summary>
        /// The test_ create_ company user extension.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_CompanyUserExtension()
        {
            // Arrange
            var maintenanceTenant = await this.GetTenantAsync(Tenant.DefaultMaintenanceTenantName);
            var name = "测试";
            var expirationDate = DateTime.Now;
            this.AbpSession.TenantId = maintenanceTenant.Id;
            maintenanceTenant.ShouldNotBeNull();

            // Act
            await this.UserAppService.CreateOrUpdateUser(
                new CreateOrUpdateUserInput
                    {
                        User =
                            new UserEditDto
                                {
                                    Name = name,
                                    Surname = "测试s",
                                    UserName = "test",
                                    EmailAddress = "test@163.com",
                                    PhoneNumber = "13812312312",
                                    Password = "123123"
                                },
                        AssignedRoleNames = new[] { "User" },
                        SendActivationEmail = false,
                        SetRandomPassword = false,
                        SignPictureToken = "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423",
                        CertificateBackPictureToken = "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423",
                        CertificateFrontPictureToken = "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423",
                        CompanyUser = new EccpCompanyUserExtensionEditDto
                                          {
                                              IdCard = "10000",
                                              Mobile = "13812312312",
                                              SignPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                                              CertificateFrontPictureId =
                                                  new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                                              CertificateBackPictureId =
                                                  new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                                              ExpirationDate = expirationDate
                                          }
                    });

            // Assert
            this.UsingDbContext(
                context =>
                    {
                        var entity = context.Users.FirstOrDefault(e => e.Name == name);
                        entity.ShouldNotBeNull();

                        var userRole = context.UserRoles.FirstOrDefault(e => e.UserId == entity.Id);
                        userRole.ShouldNotBeNull();

                        var role = context.Roles.FirstOrDefault(e => e.Id == userRole.RoleId);
                        role.ShouldNotBeNull();
                        role.Name.ShouldBe("User");

                        var userExtension =
                            context.EccpCompanyUserExtensions.FirstOrDefault(e => e.UserId == entity.Id);
                        userExtension.ShouldNotBeNull();
                        userExtension.IdCard.ShouldBe("10000");
                        userExtension.Mobile.ShouldBe("13812312312");
                        userExtension.SignPictureId.ShouldBe(new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"));
                        userExtension.CertificateBackPictureId.ShouldBe(
                            new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"));
                        userExtension.ExpirationDate.ShouldBe(expirationDate);
                    });
        }

        /// <summary>
        /// The test_ update_ check state inspection unit.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_CheckStateInspectionUnit()
        {
            var entity = this.GetEntity("TestMaintenanceUser2");

            await this._associationUseAppServicer.UpdateCompanyUserCheckStateAsync(
                Convert.ToInt32(entity.Id),
                1,
                "审核通过");

            this.UsingDbContext(
                context =>
                    {
                        context.EccpCompanyUserExtensions.FirstOrDefault(m => m.UserId == entity.Id)?.CheckState
                            .ShouldBe(1);
                        context.EccpCompanyUserAuditLogs.Count(m => m.UserId == entity.Id).ShouldBe(1);
                    });
        }

        /// <summary>
        /// The test_ update_ company user extension.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_CompanyUserExtension()
        {
            // Arrange
            var userEntity = await this.GetUserByUserNameOrNullAsync(AbpUserBase.AdminUserName);
            var userOutput = await this.UserAppService.GetUserForEdit(new NullableIdDto<long>(userEntity.Id));

            userOutput.ShouldNotBeNull();
            userOutput.User.ShouldNotBeNull();
            userOutput.CompanyUser.ShouldNotBeNull();

            var newName = "测试新";
            var newIdCard = "123123";
            var newMobile = "13333333333";

            var input = new CreateOrUpdateUserInput { User = userOutput.User };
            input.User.Name = newName;
            input.AssignedRoleNames = userOutput.Roles.Select(e => e.RoleName).ToArray();
            input.CompanyUser = userOutput.CompanyUser;
            input.CompanyUser.IdCard = newIdCard;
            input.CompanyUser.Mobile = newMobile;

            // Act
            await this.UserAppService.CreateOrUpdateUser(input);

            // Assert
            var newUserOutput = await this.UserAppService.GetUserForEdit(new NullableIdDto<long>(userEntity.Id));

            newUserOutput.ShouldNotBeNull();
            newUserOutput.User.ShouldNotBeNull();

            newUserOutput.User.Name.ShouldBe(newName);

            userOutput.CompanyUser.ShouldNotBeNull();

            userOutput.CompanyUser.IdCard.ShouldBe(newIdCard);
            userOutput.CompanyUser.Mobile.ShouldBe(newMobile);
        }

        /// <summary>
        /// The create role.
        /// </summary>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <param name="tenantId">
        /// The tenant id.
        /// </param>
        /// <returns>
        /// The <see cref="Role"/>.
        /// </returns>
        protected Role CreateRole(string roleName, int tenantId)
        {
            var entity = this.UsingDbContext(
                context => context.Roles.Add(new Role(tenantId, roleName, roleName)).Entity);
            entity.ShouldNotBeNull();

            return entity;
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        private User GetEntity(string name)
        {
            var entity = this.UsingDbContext(context => context.Users.FirstOrDefault(e => e.Name == name));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}