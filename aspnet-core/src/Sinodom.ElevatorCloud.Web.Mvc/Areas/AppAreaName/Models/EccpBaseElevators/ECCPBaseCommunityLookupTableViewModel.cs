// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseCommunityLookupTableViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseElevators
{
    /// <summary>
    /// The eccp base community lookup table view model.
    /// </summary>
    public class ECCPBaseCommunityLookupTableViewModel
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
        public long? Id { get; set; }
    }
}