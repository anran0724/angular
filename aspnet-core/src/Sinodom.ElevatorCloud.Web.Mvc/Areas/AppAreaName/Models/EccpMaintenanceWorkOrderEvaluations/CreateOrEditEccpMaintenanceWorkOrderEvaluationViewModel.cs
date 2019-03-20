// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceWorkOrderEvaluationViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorkOrderEvaluations
{
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;

    /// <summary>
    /// The create or edit eccp maintenance work order evaluation view model.
    /// </summary>
    public class CreateOrEditEccpMaintenanceWorkOrderEvaluationViewModel
    {
        /// <summary>
        /// Gets or sets the eccp maintenance work order evaluation.
        /// </summary>
        public CreateOrEditEccpMaintenanceWorkOrderEvaluationDto EccpMaintenanceWorkOrderEvaluation { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order remark.
        /// </summary>
        public string EccpMaintenanceWorkOrderRemark { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpMaintenanceWorkOrderEvaluation.Id.HasValue;
    }
}