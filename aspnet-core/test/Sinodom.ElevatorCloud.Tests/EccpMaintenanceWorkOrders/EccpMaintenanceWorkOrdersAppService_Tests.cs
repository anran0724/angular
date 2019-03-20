// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrdersAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Sinodom.ElevatorCloud.EccpMaintenanceWorks;
using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;

namespace Sinodom.ElevatorCloud.Tests.EccpMaintenanceWorkOrders
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization.Users;

    using Shouldly;

    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;
    using Sinodom.ElevatorCloud.MultiTenancy;

    /// <summary>
    ///     The eccp maintenance work orders app service_ tests.
    /// </summary>
    public class EccpMaintenanceWorkOrdersAppService_Tests : AppTestBase
    {
        /// <summary>
        ///     The _eccp maintenance work orders app service.
        /// </summary>
        private readonly IEccpMaintenanceWorkOrdersAppService eccpMaintenanceWorkOrdersAppService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EccpMaintenanceWorkOrdersAppService_Tests" /> class.
        /// </summary>
        public EccpMaintenanceWorkOrdersAppService_Tests()
        {
            this.LoginAsTenant(Tenant.DefaultMaintenanceTenantName, "TestMaintenanceUser1");
            this.eccpMaintenanceWorkOrdersAppService = this.Resolve<EccpMaintenanceWorkOrdersAppService>();
        }

        /// <summary>
        /// The should_ get_ maintenance pending processing work order.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_MaintenancePendingProcessingWorkOrder()
        {
            var entities = await this.eccpMaintenanceWorkOrdersAppService.GetAllPendingProcessing(
                               new GetAllEccpMaintenanceWorkOrdersInput());
            entities.Items.Count.ShouldBe(1);
            entities.Items[0].EccpDictMaintenanceTypeName.ShouldBe("月度维保");
        }

        /// <summary>
        ///     The should_ get_ maintenance period work order.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_MaintenancePeriodWorkOrder()
        {
            var entities = await this.eccpMaintenanceWorkOrdersAppService.GetPeriodAll(
                               new GetAllEccpMaintenanceWorkOrdersInput { IsPassedFilter = 0 });
            entities.Items.Count.ShouldBe(1);
            entities.Items[0].EccpDictMaintenanceTypeName.ShouldBe("月度维保");
        }

        /// <summary>
        ///     The should_ get_ maintenance work order.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_MaintenanceWorkOrder()
        {
            var entities = await this.eccpMaintenanceWorkOrdersAppService.GetAll(
                               new GetAllEccpMaintenanceWorkOrdersInput { IsPassedFilter = 0 });
            entities.Items.Count.ShouldBe(1);
            entities.Items[0].EccpDictMaintenanceTypeName.ShouldBe("月度维保");
        }

        /// <summary>
        ///     The should_ get_ maintenance work order evaluations.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_MaintenanceWorkOrderEvaluations()
        {
            var entities = await this.eccpMaintenanceWorkOrdersAppService.GetAllWorkOrderEvaluationByWorkOrderId(
                               new GetAllByWorkOrderIdEccpMaintenanceWorkOrderEvaluationsInput
                               {
                                   MaintenanceWorkOrderIdFilter = 1
                               });
            entities.Items.Count.ShouldBe(1);
        }

        /// <summary>
        ///     The test_ close work order_ maintenance work order.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_CloseWorkOrder_MaintenanceWorkOrder()
        {
            //// Act
            await this.eccpMaintenanceWorkOrdersAppService.CloseWorkOrder(new EntityDto(1));

            //// Assert
            this.GetEntity(1).IsClosed.ShouldBeTrue();
        }

        /// <summary>
        /// 测试获取App首页数据接口
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_GetAppIndexData_MaintenanceWorkOrder()
        {
            var getAppIndex = await this.eccpMaintenanceWorkOrdersAppService.GetAppIndexData();
            getAppIndex.TempWorkOrder.ShouldNotBeNull();
            getAppIndex.ThisMonthWorkOrderStatistics.ShouldNotBeNull();
            getAppIndex.TodayWorkOrderStatistics.ShouldNotBeNull();
            getAppIndex.WaitMaintenance.ShouldNotBeNull();
        }

        /// <summary>
        ///     The test ergodic maintenance plan.
        /// </summary>
        [MultiTenantFact]
        public void TestErgodicMaintenancePlan()
        {
            // Arrange
            var eccpMaintenancePlan = new EccpMaintenancePlan();
            this.UsingDbContext(
                async context =>
                    {
                        eccpMaintenancePlan = context.EccpMaintenancePlans.FirstOrDefault();



                        // 创建4个工单
                        var newOrdersCount = await this.eccpMaintenanceWorkOrdersAppService.PlanModificationRearrangeWorkOrder(eccpMaintenancePlan.Id);

                        newOrdersCount.ShouldBe(4);

                        var newOrder =
                            context.EccpMaintenanceWorkOrders.OrderByDescending(w => w.CreationTime).FirstOrDefault(w =>
                                  w.MaintenancePlanId == eccpMaintenancePlan.Id);
                        var maintenanceUserLinkCount = context.EccpMaintenanceWorkOrder_MaintenanceUser_Links.Count(w =>
                            w.MaintenancePlanId == newOrder.Id);
                        maintenanceUserLinkCount.ShouldBe(1);

                        var maintenanceWorkOrderPropertyUserLinkCount = context.EccpMaintenanceWorkOrder_PropertyUser_Links.Count(w =>
                            w.MaintenancePlanId == newOrder.Id);
                        maintenanceWorkOrderPropertyUserLinkCount.ShouldBe(1);


                    });

            var eccpMaintenanceWorkOrder = new EccpMaintenanceWorkOrder();

            this.UsingDbContext(
                context =>
                    {
                        var eccpDictMaintenanceStatuse =
                            context.EccpDictMaintenanceStatuses.FirstOrDefault(w => w.Name == "已完成");
                        eccpMaintenanceWorkOrder = context.EccpMaintenanceWorkOrders.OrderBy(o => o.PlanCheckDate)
                            .FirstOrDefault(w => w.IsDeleted == false);
                        eccpMaintenanceWorkOrder.MaintenanceStatusId = eccpDictMaintenanceStatuse.Id;
                        context.SaveChanges();
                    });

            // 完成一个工单
            var changeOrdersCount = this.eccpMaintenanceWorkOrdersAppService.CompletionWorkRearrangeWorkOrder(
                eccpMaintenanceWorkOrder.Id,
                eccpMaintenanceWorkOrder.PlanCheckDate);
            changeOrdersCount.ShouldBe(1);

            // 删除三个工单
            var deleteOrdersCount =
                this.eccpMaintenanceWorkOrdersAppService.PlanDeletingRearrangeWorkOrder(eccpMaintenancePlan.Id);
            deleteOrdersCount.ShouldBe(1);
        }

        /// <summary>
        /// 维保工单超期处理单元测试
        /// </summary>
        [MultiTenantFact]
        public void TestOverdueTreatmentOfMaintenanceWorkOrder()
        {
            // Arrange
            var eccpMaintenancePlan = new EccpMaintenancePlan();
            this.UsingDbContext(
                async context =>
                {
                    eccpMaintenancePlan = context.EccpMaintenancePlans.FirstOrDefault();

                    // 创建工单
                    var newOrdersCount = await this.eccpMaintenanceWorkOrdersAppService.PlanModificationRearrangeWorkOrder(eccpMaintenancePlan.Id);

                    newOrdersCount.ShouldBe(1);
                });

            this.eccpMaintenanceWorkOrdersAppService.GetOverdueTreatmentOfMaintenanceWorkOrder();
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="EccpMaintenanceWorkOrder"/>.
        /// </returns>
        private EccpMaintenanceWorkOrder GetEntity(long id)
        {
            var entity = this.UsingDbContext(
                context => context.EccpMaintenanceWorkOrders.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="isPassed">
        /// The is passed.
        /// </param>
        /// <returns>
        /// The <see cref="EccpMaintenanceWorkOrder"/>.
        /// </returns>
        private EccpMaintenanceWorkOrder GetEntity(bool isPassed)
        {
            var entity = this.UsingDbContext(
                context => context.EccpMaintenanceWorkOrders.FirstOrDefault(e => e.IsPassed == isPassed));
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