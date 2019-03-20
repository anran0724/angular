// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceTypeLookupTableViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceTemplates
{
    /// <summary>
    /// The eccp dict maintenance type lookup table view model.
    /// </summary>
    public class EccpDictMaintenanceTypeLookupTableViewModel
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