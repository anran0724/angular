// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceWorkOrderEvaluationForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    /// <summary>
    /// The get eccp maintenance work order evaluation for edit output.
    /// </summary>
    public class GetEccpMaintenanceWorkOrderEvaluationForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp maintenance work order evaluation.
        /// </summary>
        public CreateOrEditEccpMaintenanceWorkOrderEvaluationDto EccpMaintenanceWorkOrderEvaluation { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order remark.
        /// </summary>
        public string EccpMaintenanceWorkOrderRemark { get; set; }
    }
}