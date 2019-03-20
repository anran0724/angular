// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictMaintenanceStatusViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictMaintenanceStatuses
{
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The create or edit eccp dict maintenance status view model.
    /// </summary>
    public class CreateOrEditEccpDictMaintenanceStatusViewModel
    {
        /// <summary>
        /// Gets or sets the eccp dict maintenance status.
        /// </summary>
        public CreateOrEditEccpDictMaintenanceStatusDto EccpDictMaintenanceStatus { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpDictMaintenanceStatus.Id.HasValue;
    }
}