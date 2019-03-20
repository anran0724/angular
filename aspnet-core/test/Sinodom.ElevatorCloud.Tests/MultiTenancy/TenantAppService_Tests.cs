// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TenantAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.MultiTenancy
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization.Users;
    using Abp.MultiTenancy;
    using Abp.Zero.Configuration;

    using Microsoft.EntityFrameworkCore;

    using Shouldly;

    using Sinodom.ElevatorCloud.MultiTenancy;
    using Sinodom.ElevatorCloud.MultiTenancy.Dto;
    using Sinodom.ElevatorCloud.Notifications;

    /// <summary>
    /// The tenant app service_ tests.
    /// </summary>
    public class TenantAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _tenant app service.
        /// </summary>
        private readonly ITenantAppService _tenantAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantAppService_Tests"/> class.
        /// </summary>
        public TenantAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._tenantAppService = this.Resolve<ITenantAppService>();
        }

        /// <summary>
        /// The create_ update_ and_ delete_ tenant_ test.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Create_Update_And_Delete_Tenant_Test()
        {
            // CREATE --------------------------------

            // Act
            await this._tenantAppService.CreateTenant(
                new CreateTenantInput
                    {
                        TenancyName = "testTenant",
                        Name = "Tenant for test purpose",
                        AdminEmailAddress = "admin@testtenant.com",
                        AdminPassword = "123qwe",
                        IsActive = true
                    });

            // Assert
            var tenant = await this.GetTenantOrNullAsync("testTenant");
            tenant.ShouldNotBe(null);
            tenant.Name.ShouldBe("Tenant for test purpose");
            tenant.IsActive.ShouldBe(true);

            await this.UsingDbContextAsync(
                tenant.Id,
                async context =>
                    {
                        // Check static roles
                        var staticRoleNames = this.Resolve<IRoleManagementConfig>().StaticRoles
                            .Where(r => r.Side == MultiTenancySides.Tenant).Select(role => role.RoleName).ToList();
                        foreach (var staticRoleName in staticRoleNames)
                            (await context.Roles.CountAsync(r => r.TenantId == tenant.Id && r.Name == staticRoleName))
                                .ShouldBe(1);

                        // Check default admin user
                        var adminUser = await context.Users.FirstOrDefaultAsync(
                                            u => u.TenantId == tenant.Id && u.UserName == AbpUserBase.AdminUserName);
                        adminUser.ShouldNotBeNull();

                        // Check notification registration
                        (await context.NotificationSubscriptions.FirstOrDefaultAsync(
                             ns => ns.UserId == adminUser.Id
                                   && ns.NotificationName == AppNotificationNames.NewUserRegistered)).ShouldNotBeNull();
                    });

            // GET FOR EDIT -----------------------------

            // Act
            var editDto = await this._tenantAppService.GetTenantForEdit(new EntityDto(tenant.Id));

            // Assert
            editDto.TenancyName.ShouldBe("testTenant");
            editDto.Name.ShouldBe("Tenant for test purpose");
            editDto.IsActive.ShouldBe(true);

            // UPDATE ----------------------------------
            editDto.Name = "edited tenant name";
            editDto.IsActive = false;
            await this._tenantAppService.UpdateTenant(editDto);

            // Assert
            tenant = await this.GetTenantAsync("testTenant");
            tenant.Name.ShouldBe("edited tenant name");
            tenant.IsActive.ShouldBe(false);

            // DELETE ----------------------------------

            // Act
            await this._tenantAppService.DeleteTenant(new EntityDto((await this.GetTenantAsync("testTenant")).Id));

            // Assert
            (await this.GetTenantOrNullAsync("testTenant")).IsDeleted.ShouldBe(true);
        }

        /// <summary>
        /// The get tenants_ test.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task GetTenants_Test()
        {
            // Act
            var output = await this._tenantAppService.GetTenants(new GetTenantsInput());

            // Assert
            output.TotalCount.ShouldBe(3);
            output.Items.Count.ShouldBe(3);
            output.Items[0].TenancyName.ShouldBe(AbpTenantBase.DefaultTenantName);
        }

        [MultiTenantFact]
        public async Task GetTenantNames_Test()
        {
            // Act
            var output = await this._tenantAppService.GetTenantNames("Default");

            // Assert
            output.TotalCount.ShouldBe(3);
            output.Items.Count.ShouldBe(3);
            output.Items[0].TenancyName.ShouldBe(AbpTenantBase.DefaultTenantName);
        }

    }
}