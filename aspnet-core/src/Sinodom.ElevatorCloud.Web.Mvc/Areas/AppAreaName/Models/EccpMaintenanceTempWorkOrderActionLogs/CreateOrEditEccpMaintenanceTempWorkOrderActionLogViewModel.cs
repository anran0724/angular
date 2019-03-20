// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceTempWorkOrderActionLogViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceTempWorkOrderActionLogs
{
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs.Dtos;

    /// <summary>
    /// The create or edit eccp maintenance temp work order action log view model.
    /// </summary>
    public class CreateOrEditEccpMaintenanceTempWorkOrderActionLogViewModel
    {
        /// <summary>
        /// Gets or sets the eccp maintenance temp work order action log.
        /// </summary>
        public CreateOrEditEccpMaintenanceTempWorkOrderActionLogDto EccpMaintenanceTempWorkOrderActionLog { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance temp work order title.
        /// </summary>
        public string EccpMaintenanceTempWorkOrderTitle { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpMaintenanceTempWorkOrderActionLog.Id.HasValue;

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }
    }
}