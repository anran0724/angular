// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceTempWorkOrderActionLogForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs.Dtos
{
    /// <summary>
    /// The get eccp maintenance temp work order action log for view.
    /// </summary>
    public class GetEccpMaintenanceTempWorkOrderActionLogForView
    {
        /// <summary>
        /// Gets or sets the eccp maintenance temp work order action log.
        /// </summary>
        public EccpMaintenanceTempWorkOrderActionLogDto EccpMaintenanceTempWorkOrderActionLog { get; set; }

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