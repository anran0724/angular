// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceTempWorkOrderActionLogForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs.Dtos
{
    /// <summary>
    /// The get eccp maintenance temp work order action log for edit output.
    /// </summary>
    public class GetEccpMaintenanceTempWorkOrderActionLogForEditOutput
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
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }
    }
}