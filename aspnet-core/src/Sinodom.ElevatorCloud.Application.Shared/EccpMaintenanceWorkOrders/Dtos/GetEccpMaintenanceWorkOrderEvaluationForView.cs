// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceWorkOrderEvaluationForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using System;

    /// <summary>
    /// The get eccp maintenance work order evaluation for view.
    /// </summary>
    public class GetEccpMaintenanceWorkOrderEvaluationForView
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
        public EccpMaintenanceWorkOrderEvaluationDto EccpMaintenanceWorkOrderEvaluation { get; set; }
    }
}