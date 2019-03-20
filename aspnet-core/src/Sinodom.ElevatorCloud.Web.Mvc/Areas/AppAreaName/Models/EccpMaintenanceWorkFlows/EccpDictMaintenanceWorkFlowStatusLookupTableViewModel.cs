// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceWorkFlowStatusLookupTableViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorkFlows
{
    /// <summary>
    /// The eccp dict maintenance work flow status lookup table view model.
    /// </summary>
    public class EccpDictMaintenanceWorkFlowStatusLookupTableViewModel
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the filter text.
        /// </summary>
        public string FilterText { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int? Id { get; set; }
    }
}