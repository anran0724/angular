// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceWorkFlowViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorkFlows
{
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;

    /// <summary>
    ///     The create or edit eccp maintenance work flow view model.
    /// </summary>
    public class CreateOrEditEccpMaintenanceWorkFlowViewModel
    {
        /// <summary>
        ///     Gets or sets the eccp dict maintenance work flow status name.
        /// </summary>
        public string EccpDictMaintenanceWorkFlowStatusName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp maintenance template node node name.
        /// </summary>
        public string EccpMaintenanceTemplateNodeNodeName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp maintenance work flow.
        /// </summary>
        public CreateOrEditEccpMaintenanceWorkFlowDto EccpMaintenanceWorkFlow { get; set; }

        /// <summary>
        ///     Gets or sets the eccp maintenance work task name.
        /// </summary>
        public string EccpMaintenanceWorkTaskName { get; set; }

        /// <summary>
        ///     The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpMaintenanceWorkFlow.Id.HasValue;
    }
}