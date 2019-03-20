// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrderEvaluationViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorkOrderEvaluations
{
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;

    /// <summary>
    /// The eccp maintenance work order evaluation view model.
    /// </summary>
    public class EccpMaintenanceWorkOrderEvaluationViewModel : GetEccpMaintenanceWorkOrderEvaluationForView
    {
        /// <summary>
        /// Gets or sets the eccp maintenance work order remark.
        /// </summary>
        public string EccpMaintenanceWorkOrderRemark { get; set; }
    }
}