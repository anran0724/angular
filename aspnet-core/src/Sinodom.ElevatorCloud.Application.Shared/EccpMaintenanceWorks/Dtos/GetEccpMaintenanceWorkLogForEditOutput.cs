// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceWorkLogForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    /// <summary>
    /// The get eccp maintenance work log for edit output.
    /// </summary>
    public class GetEccpMaintenanceWorkLogForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp maintenance work log.
        /// </summary>
        public CreateOrEditEccpMaintenanceWorkLogDto EccpMaintenanceWorkLog { get; set; }
    }
}