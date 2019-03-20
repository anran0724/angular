// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTroubledWorkOrdersAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpMaintenanceWorkOrders
{
    using System;
    using System.Threading.Tasks;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;

    /// <summary>
    /// The eccp maintenance troubled work orders app service_ tests.
    /// </summary>
    public class EccpMaintenanceTroubledWorkOrdersAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _eccp maintenance troubled work orders app service.
        /// </summary>
        private readonly IEccpMaintenanceTroubledWorkOrdersAppService _eccpMaintenanceTroubledWorkOrdersAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTroubledWorkOrdersAppService_Tests"/> class.
        /// </summary>
        public EccpMaintenanceTroubledWorkOrdersAppService_Tests()
        {
            this.LoginAsDefaultTenantAdmin();
            this._eccpMaintenanceTroubledWorkOrdersAppService =
                this.Resolve<EccpMaintenanceTroubledWorkOrdersAppService>();
        }

        /// <summary>
        /// The test_ apply troubled work order.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_ApplyTroubledWorkOrder()
        {
            var maintenanceWorkOrder = new EccpMaintenanceWorkOrder();

            this.UsingDbContext(
                context =>
                    {
                        maintenanceWorkOrder = context.EccpMaintenanceWorkOrders.Add(
                            new EccpMaintenanceWorkOrder
                                {
                                    TenantId = this.AbpSession.TenantId.Value,
                                    IsPassed = true,
                                    PlanCheckDate = DateTime.Now,
                                    IsClosed = false,
                                    MaintenancePlan = new EccpMaintenancePlan
                                                          {
                                                              PollingPeriod = 30,
                                                              RemindHour = 72,
                                                              PlanGroupGuid = Guid.NewGuid(),
                                                              Elevator = new EccpBaseElevator
                                                                             {
                                                                                 Name = "测试电梯1",
                                                                                 CertificateNum = "32164564641131646",
                                                                                 Longitude = "153",
                                                                                 Latitude = "345"
                                                                             }
                                                          },
                                    MaintenanceStatus = new EccpDictMaintenanceStatus { Name = "未开始" },
                                    MaintenanceType = new EccpDictMaintenanceType { Name = "季度维保" }
                                }).Entity;
                    });

            // Act
            var result = await this._eccpMaintenanceTroubledWorkOrdersAppService.ApplyTroubledWorkOrder(
                             new CreateOrEditEccpMaintenanceTroubledWorkOrderDto
                                 {
                                     TroubledDesc = "电梯有问题", MaintenanceWorkOrderId = maintenanceWorkOrder.Id
                                 });

            result.ShouldBeGreaterThan(0);
        }

        /// <summary>
        /// The test_ audit.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Audit()
        {
            var maintenanceTroubledWorkOrder = new EccpMaintenanceTroubledWorkOrder();

            this.UsingDbContext(
                context =>
                    {
                        var maintenanceWorkOrder = context.EccpMaintenanceWorkOrders.Add(
                            new EccpMaintenanceWorkOrder
                                {
                                    TenantId = this.AbpSession.TenantId.Value,
                                    IsPassed = true,
                                    PlanCheckDate = DateTime.Now,
                                    IsClosed = false,
                                    MaintenancePlan = new EccpMaintenancePlan
                                                          {
                                                              PollingPeriod = 30,
                                                              RemindHour = 72,
                                                              PlanGroupGuid = Guid.NewGuid(),
                                                              Elevator = new EccpBaseElevator
                                                                             {
                                                                                 Name = "测试电梯1",
                                                                                 CertificateNum = "32164564641131646",
                                                                                 Longitude = "153",
                                                                                 Latitude = "345"
                                                                             }
                                                          },
                                    MaintenanceStatus = new EccpDictMaintenanceStatus { Name = "未开始" },
                                    MaintenanceType = new EccpDictMaintenanceType { Name = "季度维保" }
                                }).Entity;

                        maintenanceTroubledWorkOrder = context.EccpMaintenanceTroubledWorkOrders.Add(
                            new EccpMaintenanceTroubledWorkOrder
                                {
                                    TenantId = this.AbpSession.TenantId.Value,
                                    WorkOrderStatusName = maintenanceWorkOrder.MaintenanceStatus.Name,
                                    TroubledDesc = "问题工单",
                                    IsAudit = 0,
                                    MaintenanceWorkOrderId = maintenanceWorkOrder.Id
                                }).Entity;
                    });

            // Act
            await this._eccpMaintenanceTroubledWorkOrdersAppService.Audit(
                new AuditEccpMaintenanceTroubledWorkOrderDto
                    {
                        Id = maintenanceTroubledWorkOrder.Id, Remarks = "电梯有问题"
                    });
        }

        /// <summary>
        /// The test_ get all.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_GetAll()
        {
            var maintenanceTroubledWorkOrder = new EccpMaintenanceTroubledWorkOrder();

            this.UsingDbContext(
                context =>
                    {
                        var maintenanceWorkOrder = context.EccpMaintenanceWorkOrders.Add(
                            new EccpMaintenanceWorkOrder
                                {
                                    TenantId = this.AbpSession.TenantId.Value,
                                    IsPassed = true,
                                    PlanCheckDate = DateTime.Now,
                                    IsClosed = false,
                                    MaintenancePlan = new EccpMaintenancePlan
                                                          {
                                                              PollingPeriod = 30,
                                                              RemindHour = 72,
                                                              PlanGroupGuid = Guid.NewGuid(),
                                                              Elevator = new EccpBaseElevator
                                                                             {
                                                                                 Name = "测试电梯1",
                                                                                 CertificateNum = "32164564641131646",
                                                                                 Longitude = "153",
                                                                                 Latitude = "345"
                                                                             }
                                                          },
                                    MaintenanceStatus = new EccpDictMaintenanceStatus { Name = "未开始" },
                                    MaintenanceType = new EccpDictMaintenanceType { Name = "季度维保" }
                                }).Entity;

                        maintenanceTroubledWorkOrder = context.EccpMaintenanceTroubledWorkOrders.Add(
                            new EccpMaintenanceTroubledWorkOrder
                                {
                                    TenantId = this.AbpSession.TenantId.Value,
                                    WorkOrderStatusName = maintenanceWorkOrder.MaintenanceStatus.Name,
                                    TroubledDesc = "问题工单",
                                    IsAudit = 0,
                                    MaintenanceWorkOrderId = maintenanceWorkOrder.Id
                                }).Entity;
                    });

            // Act
            var output = await this._eccpMaintenanceTroubledWorkOrdersAppService.GetAll(
                             new GetAllEccpMaintenanceTroubledWorkOrdersInput
                                 {
                                     IsAuditFilter = -1, EccpMaintenanceTroubledDescFilter = "问题工单"
                                 });

            output.TotalCount.ShouldBe(1);

            output.Items[0].EccpMaintenanceTroubledWorkOrder.TroubledDesc.ShouldBe("问题工单");
        }
    }
}