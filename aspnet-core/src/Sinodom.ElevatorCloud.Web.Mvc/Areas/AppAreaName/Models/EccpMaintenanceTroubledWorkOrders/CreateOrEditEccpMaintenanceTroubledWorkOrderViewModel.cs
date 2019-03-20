// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceTroubledWorkOrderViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceTroubledWorkOrders
{
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;

    /// <summary>
    /// The create or edit eccp maintenance troubled work order view model.
    /// </summary>
    public class CreateOrEditEccpMaintenanceTroubledWorkOrderViewModel
    {
        /// <summary>
        /// Gets or sets the eccp maintenance troubled work order.
        /// </summary>
        public CreateOrEditEccpMaintenanceTroubledWorkOrderDto EccpMaintenanceTroubledWorkOrder { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order remark.
        /// </summary>
        public string EccpMaintenanceWorkOrderRemark { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpMaintenanceTroubledWorkOrder.Id.HasValue;
    }
}