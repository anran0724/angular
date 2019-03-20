// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceTempWorkOrderViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceTempWorkOrders
{
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos;

    /// <summary>
    /// The create or edit eccp maintenance temp work order view model.
    /// </summary>
    public class CreateOrEditEccpMaintenanceTempWorkOrderViewModel
    {
        /// <summary>
        /// Gets or sets the eccp dict temp work order type name.
        /// </summary>
        public string EccpDictTempWorkOrderTypeName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance temp work order.
        /// </summary>
        public CreateOrEditEccpMaintenanceTempWorkOrderDto EccpMaintenanceTempWorkOrder { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpMaintenanceTempWorkOrder.Id.HasValue;

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }
        public string EccpBaseElevatorName { get; set; }
    }
}