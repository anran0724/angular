// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestEccpMaintenanceWorkOrdersBuilder.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// <summary>
//   The test eccp maintenance work orders builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Sinodom.ElevatorCloud.EccpMaintenanceTransfers;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    using System; 
    using Sinodom.ElevatorCloud.EntityFrameworkCore;

    /// <summary>
    /// The test MaintenanceWorkOrderTransfers builder.
    /// </summary>
    public class TestMaintenanceWorkOrderTransfersBuilder
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
        public TestMaintenanceWorkOrderTransfersBuilder(ElevatorCloudDbContext context, int maintenanceTenantId)
        {
            this.context = context;
            this.maintenanceTenantId = maintenanceTenantId;
        }

        /// <summary>
        /// The create.
        /// </summary>
        public void Create()
        {
            this.MaintenanceWorkOrderTransfersr(1);
        }

        /// <summary>
        /// The MaintenanceWorkOrderTransfers.
        /// </summary>
        /// <param name="maintenanceWorkOrderId">
        /// The maintenanceWorkOrderId
        /// </param>
        private void MaintenanceWorkOrderTransfersr(int maintenanceWorkOrderId)
        {
            this.context.EccpMaintenanceWorkOrderTransfers.Add(
                new EccpMaintenanceWorkOrderTransfer
                {
                        MaintenanceWorkOrderId = maintenanceWorkOrderId,
                        IsApproved = null,
                        Remark ="工单转接",
                        CreationTime = DateTime.Now,                       
                        CreatorUserId = 1,                    
                        TransferUserId = 2,
                        TenantId = this.maintenanceTenantId
                    });

            this.context.SaveChanges();
        }
    }
}