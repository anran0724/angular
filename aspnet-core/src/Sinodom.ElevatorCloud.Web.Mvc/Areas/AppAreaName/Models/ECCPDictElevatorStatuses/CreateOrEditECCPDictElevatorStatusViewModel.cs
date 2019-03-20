// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditECCPDictElevatorStatusViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPDictElevatorStatuses
{
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The create or edit eccp dict elevator status view model.
    /// </summary>
    public class CreateOrEditECCPDictElevatorStatusViewModel
    {
        /// <summary>
        /// Gets or sets the eccp dict elevator status.
        /// </summary>
        public CreateOrEditECCPDictElevatorStatusDto EccpDictElevatorStatus { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpDictElevatorStatus.Id.HasValue;
    }
}