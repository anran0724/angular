// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpElevatorMaintenanceWorkOrderEvaluationForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    using System;

    /// <summary>
    /// The get eccp elevator maintenance work order evaluation for view.
    /// </summary>
    public class GetEccpElevatorMaintenanceWorkOrderEvaluationForView
    {
        /// <summary>
        /// Gets or sets the eccp dict maintenance type name.
        /// </summary>
        public string EccpDictMaintenanceTypeName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order complate date.
        /// </summary>
        public virtual DateTime? EccpMaintenanceWorkOrderComplateDate { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order evaluation.
        /// </summary>
        public EccpElevatorMaintenanceWorkOrderEvaluationDto EccpMaintenanceWorkOrderEvaluation { get; set; }
    }
}