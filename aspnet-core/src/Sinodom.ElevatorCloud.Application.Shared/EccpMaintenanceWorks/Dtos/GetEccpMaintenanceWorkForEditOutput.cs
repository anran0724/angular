// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceWorkForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    /// <summary>
    /// The get eccp maintenance work for edit output.
    /// </summary>
    public class GetEccpMaintenanceWorkForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp maintenance template node node name.
        /// </summary>
        public string EccpMaintenanceTemplateNodeNodeName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work.
        /// </summary>
        public CreateOrEditEccpMaintenanceWorkDto EccpMaintenanceWork { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order plan check date.
        /// </summary>
        public string EccpMaintenanceWorkOrderPlanCheckDate { get; set; }
    }
}