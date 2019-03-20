// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceWorkFlowForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    /// <summary>
    /// The get eccp maintenance work flow for edit output.
    /// </summary>
    public class GetEccpMaintenanceWorkFlowForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp dict maintenance work flow status name.
        /// </summary>
        public string EccpDictMaintenanceWorkFlowStatusName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance template node node name.
        /// </summary>
        public string EccpMaintenanceTemplateNodeNodeName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work flow.
        /// </summary>
        public CreateOrEditEccpMaintenanceWorkFlowDto EccpMaintenanceWorkFlow { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work task name.
        /// </summary>
        public string EccpMaintenanceWorkTaskName { get; set; }
    }
}