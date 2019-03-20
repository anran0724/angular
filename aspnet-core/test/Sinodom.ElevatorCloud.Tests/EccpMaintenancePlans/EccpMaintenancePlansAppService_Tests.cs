// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenancePlansAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpMaintenancePlans
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization.Users;

    using Shouldly;

    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos;
    using Sinodom.ElevatorCloud.MultiTenancy;

    /// <summary>
    ///     The eccp maintenance plans app service_ tests.
    /// </summary>
    public class EccpMaintenancePlansAppService_Tests : AppTestBase
    {
        /// <summary>
        ///     The _eccp maintenance plans app service.
        /// </summary>
        private readonly IEccpMaintenancePlansAppService _eccpMaintenancePlansAppService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EccpMaintenancePlansAppService_Tests" /> class.
        /// </summary>
        public EccpMaintenancePlansAppService_Tests()
        {
            this.LoginAsTenant(Tenant.DefaultMaintenanceTenantName, AbpUserBase.AdminUserName);
            this._eccpMaintenancePlansAppService = this.Resolve<IEccpMaintenancePlansAppService>();
        }

        /// <summary>
        ///     The should_ get_ maintenance plan.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_MaintenancePlan()
        {
            var entity = this.GetEntity(1);

            var output =
                await this._eccpMaintenancePlansAppService.GetEccpMaintenancePlanForEdit(new EntityDto(entity.Id));

            output.ElevatorNames.ShouldBe("测试电梯1"); // .ShouldNotBeNull();
        }

        /// <summary>
        ///     The should_ get_ maintenance plans.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_MaintenancePlans()
        {
            // Act
            var output =
                await this._eccpMaintenancePlansAppService.GetAll(
                    new GetAllEccpMaintenancePlansInput { IsCloseFilter = 0 });

            output.TotalCount.ShouldBe(2);

            output.Items[0].EccpMaintenancePlan.IsClose.ShouldBe(Convert.ToBoolean(0));
        }

        /// <summary>
        ///     The test_ close plan_ maintenance plans.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_ClosePlan_MaintenancePlans()
        {
            var entity = this.GetEntity(1);

            //// Act
            await this._eccpMaintenancePlansAppService.ClosePlan(entity.PlanGroupGuid);

            //// Assert
            this.GetEntity(entity.Id).IsClose.ShouldBeTrue();
        }

        /// <summary>
        ///     The test_ create_ maintenance plans.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_MaintenancePlans()
        {
            // Arrange
            var elevatorEntity = this.GetElevatorEntity("a1111111111");
            var maintenanceUser = this.GetUserEntity("TestMaintenanceUser1");
            var propertyUser = this.GetUserEntity("TestPropertyUser1");

            // Act
            var input = new CreateOrEditEccpMaintenancePlanInfoDto
                            {
                                ElevatorIds = elevatorEntity.Id.ToString(),
                                MaintenanceUserIds = maintenanceUser.Id.ToString(),
                                PropertyUserIds = propertyUser.Id.ToString(),
                                PollingPeriod = 30,
                                QuarterPollingPeriod = 2,
                                HalfYearPollingPeriod = 5,
                                YearPollingPeriod = 11,
                                IsSkip = 1,
                                MaintenanceTypes = new List<GetEccpDictMaintenanceTypeForView>
                                                       {
                                                           new GetEccpDictMaintenanceTypeForView
                                                               {
                                                                   MaintenanceTemplateId = 1, TypeId = 2
                                                               }
                                                       }
                            };
            await this._eccpMaintenancePlansAppService.CreateOrEdit(input);

            // Assert
            var entity = this.GetEntity(4);
            var customRuleEntity = this.UsingDbContext(
                context => context.EccpMaintenancePlanCustomRules.FirstOrDefault(
                    e => e.PlanGroupGuid == entity.PlanGroupGuid));
            var templateLinkEntity = this.UsingDbContext(
                context => context.EccpMaintenancePlan_Template_Links.FirstOrDefault(
                    e => e.MaintenancePlanId == entity.Id));
            customRuleEntity.ShouldNotBeNull();
            templateLinkEntity.ShouldNotBeNull();

            var templateEntity = this.UsingDbContext(
                context => context.EccpMaintenanceTemplates.FirstOrDefault(
                    e => e.Id == templateLinkEntity.MaintenanceTemplateId));

            templateEntity.ShouldNotBeNull();

            customRuleEntity.QuarterPollingPeriod.ShouldBe(2);
            customRuleEntity.HalfYearPollingPeriod.ShouldBe(5);
            customRuleEntity.YearPollingPeriod.ShouldBe(11);

            templateEntity.MaintenanceTypeId.ShouldBe(2);

            entity.PollingPeriod.ShouldBe(30);
        }

        /// <summary>
        ///     The test_ delete_ maintenance plans.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_MaintenancePlans()
        {
            var entity = this.GetEntity(1);

            //// Act
            await this._eccpMaintenancePlansAppService.Delete(new EntityDto(entity.Id));

            //// Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        // [MultiTenantFact]
        // public void Test_ClosePlan()
        // {
        // EccpMaintenancePlan entity = GetEntity(1);

        // //// Act
        // _eccpMaintenancePlansManager.ClosePlan(entity.ElevatorId);

        // //// Assert
        // GetEntity(entity.Id).IsClose.ShouldBeTrue();
        // }

        /// <summary>
        ///     The test_ get all eccp base elevator for lookup table.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_GetAllEccpBaseElevatorForLookupTable()
        {
            var baseElevator = new EccpBaseElevator();

            // Arrange
            this.UsingDbContext(
                context =>
                    {
                        baseElevator = context.EccpBaseElevators.Add(
                            new EccpBaseElevator
                                {
                                    Name = "测试电梯1",
                                    CertificateNum = "30102102002009020015",
                                    Longitude = "121.589107",
                                    Latitude = "38.958835"
                                }).Entity;

                        var baseElevator1 = context.EccpBaseElevators.Add(
                            new EccpBaseElevator
                                {
                                    Name = "测试电梯2",
                                    CertificateNum = "30102102112002090012",
                                    Longitude = "124.139233",
                                    Latitude = "41.309116"
                                }).Entity;

                        var baseElevator2 = context.EccpBaseElevators.Add(
                            new EccpBaseElevator
                                {
                                    Name = "测试电梯3",
                                    CertificateNum = "30102105002006040003",
                                    Longitude = "121.595298",
                                    Latitude = "38.961681"
                                }).Entity;

                        var maintenanceContract = context.EccpMaintenanceContracts.Add(
                            new EccpMaintenanceContract
                                {
                                    TenantId = this.AbpSession.TenantId.Value,
                                    StartDate = DateTime.Now,
                                    EndDate = DateTime.Now.AddYears(2),
                                    MaintenanceCompany = new ECCPBaseMaintenanceCompany
                                                             {
                                                                 Name = "测试维保单位1",
                                                                 Addresse = "地址1",
                                                                 Longitude = string.Empty,
                                                                 Latitude = string.Empty,
                                                                 Telephone = "测试电话1",
                                                                 OrgOrganizationalCode = "333"
                                                             },
                                    PropertyCompany = new ECCPBasePropertyCompany
                                                          {
                                                              Name = "测试使用单位1",
                                                              Addresse = "地址1",
                                                              Longitude = string.Empty,
                                                              Latitude = string.Empty,
                                                              Telephone = "测试电话1",
                                                              OrgOrganizationalCode = "333"
                                                          }
                                }).Entity;

                        context.EccpMaintenanceContract_Elevator_Links.Add(
                            new EccpMaintenanceContract_Elevator_Link
                                {
                                    MaintenanceContractId = maintenanceContract.Id, ElevatorId = baseElevator.Id
                                });

                        context.EccpMaintenanceContract_Elevator_Links.Add(
                            new EccpMaintenanceContract_Elevator_Link
                                {
                                    MaintenanceContractId = maintenanceContract.Id, ElevatorId = baseElevator1.Id
                                });

                        context.EccpMaintenanceContract_Elevator_Links.Add(
                            new EccpMaintenanceContract_Elevator_Link
                                {
                                    MaintenanceContractId = maintenanceContract.Id, ElevatorId = baseElevator2.Id
                                });
                    });

            // Act
            var output =
                await this._eccpMaintenancePlansAppService.GetAllEccpBaseElevatorForLookupTable(
                    new GetAllForLookupTableInput { ElevatorId = baseElevator.Id });

            output.Items[0].DisplayName.ShouldBe("测试电梯1");
            output.Items[1].DisplayName.ShouldBe("测试电梯3");
            output.Items[2].DisplayName.ShouldBe("测试电梯2");
            output.Items[0].Distance.ShouldBeLessThan(3.ToString());
            output.Items[1].Distance.ShouldBeLessThan(3.ToString());
            output.Items[2].Distance.ShouldBeGreaterThan(3.ToString());
        }

        /// <summary>
        ///     The test_ update_ maintenance plans.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_MaintenancePlans()
        {
            // Arrange           
            var elevatorEntity = this.GetElevatorEntity("a1111111111");
            var maintenanceUser = this.GetUserEntity("TestMaintenanceUser1");
            var propertyUser = this.GetUserEntity("TestPropertyUser1");
            var entity = this.GetEntity(1);

            // Act
            await this._eccpMaintenancePlansAppService.CreateOrEdit(
                new CreateOrEditEccpMaintenancePlanInfoDto
                    {
                        Id = entity.Id,
                        PlanGroupGuid = entity.PlanGroupGuid,
                        ElevatorIds = elevatorEntity.Id.ToString(),
                        MaintenanceUserIds = maintenanceUser.Id.ToString(),
                        PropertyUserIds = propertyUser.Id.ToString(),
                        IsSkip = 1,
                        QuarterPollingPeriod = 1,
                        HalfYearPollingPeriod = 2,
                        YearPollingPeriod = 3,
                        PollingPeriod = 15
                    });

            // Assert
            entity = this.GetEntity(1);
            var customRuleEntity = this.UsingDbContext(
                context => context.EccpMaintenancePlanCustomRules.FirstOrDefault(
                    e => e.PlanGroupGuid == entity.PlanGroupGuid));

            customRuleEntity.ShouldNotBeNull();

            customRuleEntity.QuarterPollingPeriod.ShouldBe(1);
            customRuleEntity.HalfYearPollingPeriod.ShouldBe(2);
            customRuleEntity.YearPollingPeriod.ShouldBe(3);

            entity.PollingPeriod.ShouldBe(15);
        }

        /// <summary>
        /// The get elevator entity.
        /// </summary>
        /// <param name="certificateNum">
        /// The certificate num.
        /// </param>
        /// <returns>
        /// The <see cref="EccpBaseElevator"/>.
        /// </returns>
        private EccpBaseElevator GetElevatorEntity(string certificateNum)
        {
            var entity = this.UsingDbContext(
                context => context.EccpBaseElevators.FirstOrDefault(e => e.CertificateNum == certificateNum));
            entity.ShouldNotBeNull();

            return entity;
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="EccpMaintenancePlan"/>.
        /// </returns>
        private EccpMaintenancePlan GetEntity(long id)
        {
            var entity = this.UsingDbContext(context => context.EccpMaintenancePlans.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }

        /// <summary>
        /// The get user entity.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        private User GetUserEntity(string userName)
        {
            var entity = this.UsingDbContext(context => context.Users.FirstOrDefault(e => e.UserName == userName));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}