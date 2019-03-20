// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceWorkOrderViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorkOrders
{
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;

    /// <summary>
    /// The create or edit eccp maintenance work order modal view model.
    /// </summary>
    public class CreateOrEditEccpMaintenanceWorkOrderViewModel
    {
        /// <summary>
        /// Gets or sets the eccp dict maintenance status name.
        /// </summary>
        public string EccpDictMaintenanceStatusName { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict maintenance type name.
        /// </summary>
        public string EccpDictMaintenanceTypeName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance plan polling period.
        /// </summary>
        public string EccpMaintenancePlanPollingPeriod { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order.
        /// </summary>
        public CreateOrEditEccpMaintenanceWorkOrderDto EccpMaintenanceWorkOrder { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpMaintenanceWorkOrder.Id.HasValue;
    }
}