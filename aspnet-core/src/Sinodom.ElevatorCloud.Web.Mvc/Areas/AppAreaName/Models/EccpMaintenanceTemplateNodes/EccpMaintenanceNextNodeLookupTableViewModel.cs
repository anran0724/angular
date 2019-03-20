// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceNextNodeLookupTableViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceTemplateNodes
{
    /// <summary>
    /// The eccp maintenance next node lookup table view model.
    /// </summary>
    public class EccpMaintenanceNextNodeLookupTableViewModel
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

        /// <summary>
        /// Gets or sets the maintenance template id.
        /// </summary>
        public int MaintenanceTemplateId { get; set; }
    }
}