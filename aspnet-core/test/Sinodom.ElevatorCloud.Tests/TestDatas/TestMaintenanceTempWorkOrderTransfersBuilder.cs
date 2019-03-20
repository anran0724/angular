// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestEccpMaintenanceWorkOrdersBuilder.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// <summary>
//   The test eccp maintenance work orders builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using Sinodom.ElevatorCloud.EccpMaintenanceTransfers;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    using System;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;
    using Sinodom.ElevatorCloud.EntityFrameworkCore;

    /// <summary>
    /// The test MaintenanceTempWorkOrderTransfers builder.
    /// </summary>
    public class TestMaintenanceTempWorkOrderTransfersBuilder
    {
        /// <summary>
        /// The _context.
        /// </summary>
        private readonly ElevatorCloudDbContext context;

        /// <summary>
        /// The _maintenance tenant id.
        /// </summary>
        private readonly int maintenanceTenantId;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestEccpMaintenanceWorkOrdersBuilder"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="maintenanceTenantId">
        /// The maintenance tenant id.
        /// </param>
        public TestMaintenanceTempWorkOrderTransfersBuilder(ElevatorCloudDbContext context, int maintenanceTenantId)
        {
            this.context = context;
            this.maintenanceTenantId = maintenanceTenantId;
        }

        /// <summary>
        /// The create.
        /// </summary>
        public void Create()
        {
            Guid tempId = this.context.EccpMaintenanceTempWorkOrders.FirstOrDefault(w => w.Title == "测试临时工单").Id;
            this.MaintenanceTempWorkOrderTransfers(tempId);
        }

        /// <summary>
        /// The MaintenanceTempWorkOrderTransfers.
        /// </summary>
        /// <param name="maintenanceTempWorkOrderId">
        /// The maintenanceWorkOrderId
        /// </param>
        private void MaintenanceTempWorkOrderTransfers(Guid maintenanceTempWorkOrderId)
        {
            this.context.EccpMaintenanceTempWorkOrderTransfers.Add(
                new EccpMaintenanceTempWorkOrderTransfer
                {
                        MaintenanceTempWorkOrderId = maintenanceTempWorkOrderId,
                        IsApproved = null,
                        Remark ="临时工单工单转接",
                        CreationTime = DateTime.Now,                       
                        CreatorUserId = 1,                    
                        TransferUserId = 2,
                        TenantId = this.maintenanceTenantId
                    });

            this.context.SaveChanges();
        }
    }
}