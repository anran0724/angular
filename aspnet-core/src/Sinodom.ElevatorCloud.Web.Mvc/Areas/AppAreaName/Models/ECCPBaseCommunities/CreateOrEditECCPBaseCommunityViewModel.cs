// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditECCPBaseCommunityViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPBaseCommunities
{
    using Sinodom.ElevatorCloud.ECCPBaseCommunities.Dtos;

    /// <summary>
    /// The create or edit eccp base community modal view model.
    /// </summary>
    public class CreateOrEditECCPBaseCommunityViewModel
    {
        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the district name.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base community.
        /// </summary>
        public CreateOrEditECCPBaseCommunityDto EccpBaseCommunity { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpBaseCommunity.Id.HasValue;

        /// <summary>
        /// Gets or sets the province name.
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the street name.
        /// </summary>
        public string StreetName { get; set; }
    }
}