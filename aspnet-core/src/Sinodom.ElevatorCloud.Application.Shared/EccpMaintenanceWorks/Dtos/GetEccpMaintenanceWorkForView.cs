// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceWorkForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    /// <summary>
    /// The get eccp maintenance work for view.
    /// </summary>
    public class GetEccpMaintenanceWorkForView
    {
        /// <summary>
        /// Gets or sets the eccp maintenance work.
        /// </summary>
        public EccpMaintenanceWorkDto EccpMaintenanceWork { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order plan check date.
        /// </summary>
        public string EccpMaintenanceWorkOrderPlanCheckDate { get; set; }
    }
}