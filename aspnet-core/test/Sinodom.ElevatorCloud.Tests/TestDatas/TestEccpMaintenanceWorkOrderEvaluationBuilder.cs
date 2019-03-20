// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestEccpMaintenanceWorkOrderEvaluationBuilder.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.EntityFrameworkCore;

    /// <summary>
    /// The test eccp maintenance work order evaluation builder.
    /// </summary>
    public class TestEccpMaintenanceWorkOrderEvaluationBuilder
    {
        /// <summary>
        ///     The _context.
        /// </summary>
        private readonly ElevatorCloudDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestEccpMaintenanceWorkOrderEvaluationBuilder"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public TestEccpMaintenanceWorkOrderEvaluationBuilder(ElevatorCloudDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        ///     The create.
        /// </summary>
        public void Create()
        {
            this.MaintenanceWorkOrderEvaluation(1);
        }

        /// <summary>
        /// The maintenance work order evaluation.
        /// </summary>
        /// <param name="maintenanceWorkOrderId">
        /// The maintenance work order id.
        /// </param>
        private void MaintenanceWorkOrderEvaluation(int maintenanceWorkOrderId)
        {
            this.context.EccpMaintenanceWorkOrderEvaluations.Add(
                new EccpMaintenanceWorkOrderEvaluation
                    {
                        Rank = 3, WorkOrderId = maintenanceWorkOrderId, Remarks = "一般"
                    });

            this.context.SaveChanges();
        }
    }
}