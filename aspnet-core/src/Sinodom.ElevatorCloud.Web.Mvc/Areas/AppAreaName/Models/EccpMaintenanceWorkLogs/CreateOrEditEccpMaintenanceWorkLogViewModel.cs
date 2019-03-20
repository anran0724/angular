// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceWorkLogViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorkLogs
{
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;

    /// <summary>
    ///     The create or edit eccp maintenance work log view model.
    /// </summary>
    public class CreateOrEditEccpMaintenanceWorkLogViewModel
    {
        /// <summary>
        ///     Gets or sets the eccp maintenance work log.
        /// </summary>
        public CreateOrEditEccpMaintenanceWorkLogDto EccpMaintenanceWorkLog { get; set; }

        /// <summary>
        ///     The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpMaintenanceWorkLog.Id.HasValue;
    }
}