// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestEccpMaintenanceTempWorkOrdersBuilder.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    using System.Linq;

    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;
    using Sinodom.ElevatorCloud.EntityFrameworkCore;

    /// <summary>
    /// The test eccp maintenance temp work orders builder.
    /// </summary>
    public class TestEccpMaintenanceTempWorkOrdersBuilder
    {
        /// <summary>
        ///     The _context.
        /// </summary>
        private readonly ElevatorCloudDbContext context;

        /// <summary>
        ///     The _maintenance tenant id.
        /// </summary>
        private readonly int maintenanceTenantId;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestEccpMaintenanceTempWorkOrdersBuilder"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="maintenanceTenantId">
        /// The maintenance tenant id.
        /// </param>
        public TestEccpMaintenanceTempWorkOrdersBuilder(ElevatorCloudDbContext context, int maintenanceTenantId)
        {
            this.context = context;
            this.maintenanceTenantId = maintenanceTenantId;
        }

        /// <summary>
        ///     The create.
        /// </summary>
        public void Create()
        {
            this.CreateMaintenanceTempWorkOrder();
        }

        /// <summary>
        /// The create maintenance temp work order.
        /// </summary>
        private void CreateMaintenanceTempWorkOrder()
        {
            var userId = this.context.Users.FirstOrDefault(e => e.UserName == "TestMaintenanceUser1").Id;

            var maintenanceCompanyId = this.context.ECCPBaseMaintenanceCompanies.FirstOrDefault(e => e.TenantId == this.maintenanceTenantId).Id;

            this.context.EccpMaintenanceTempWorkOrders.Add(
                new EccpMaintenanceTempWorkOrder
                    {
                        TenantId = this.maintenanceTenantId,
                        Title = "测试临时工单",
                        Describe = "1",
                        CheckState = 0,
                        MaintenanceCompanyId = maintenanceCompanyId,
                        Priority = 3,
                        TempWorkOrderTypeId = 2,
                        UserId = userId
                    });

            this.context.SaveChanges();

            //未开始临时工单
            var dictTempWorkOrderTypeEntity = this.context.EccpDictTempWorkOrderTypes.FirstOrDefault(e => e.Name == "维修工单");

            this.context.EccpMaintenanceTempWorkOrders.Add(
                new EccpMaintenanceTempWorkOrder
                {
                    TenantId = this.maintenanceTenantId,
                    Title = "未开始临时工单",
                    Describe = "1",
                    CheckState = 0,
                    MaintenanceCompanyId = maintenanceCompanyId,
                    Priority = 1,
                    TempWorkOrderTypeId = dictTempWorkOrderTypeEntity.Id,
                    UserId = 3
                });

            this.context.SaveChanges();

            //已完成临时工单
            this.context.EccpMaintenanceTempWorkOrders.Add(new EccpMaintenanceTempWorkOrder
            {
                TenantId = this.maintenanceTenantId,
                Title = "已完成临时工单",
                Describe = "长岛壹号电梯困人",
                CheckState = 2,
                MaintenanceCompanyId = maintenanceCompanyId,
                UserId = 3,
                Priority = 1,
                TempWorkOrderTypeId = dictTempWorkOrderTypeEntity.Id
            });

            this.context.SaveChanges();


        }
    }
}