// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictMaintenanceWorkFlowStatusViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictMaintenanceWorkFlowStatuses
{
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The create or edit eccp dict maintenance work flow status view model.
    /// </summary>
    public class CreateOrEditEccpDictMaintenanceWorkFlowStatusViewModel
    {
        /// <summary>
        /// Gets or sets the eccp dict maintenance work flow status.
        /// </summary>
        public CreateOrEditEccpDictMaintenanceWorkFlowStatusDto EccpDictMaintenanceWorkFlowStatus { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpDictMaintenanceWorkFlowStatus.Id.HasValue;
    }
}