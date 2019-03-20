// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllECCPBaseCommunitiesForExcelInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseCommunities.Dtos
{
    /// <summary>
    /// The get all eccp base communities for excel input.
    /// </summary>
    public class GetAllECCPBaseCommunitiesForExcelInput
    {
        /// <summary>
        /// Gets or sets the address filter.
        /// </summary>
        public string AddressFilter { get; set; }

        /// <summary>
        /// Gets or sets the city name filter.
        /// </summary>
        public string CityNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the district name filter.
        /// </summary>
        public string DistrictNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the latitude filter.
        /// </summary>
        public string LatitudeFilter { get; set; }

        /// <summary>
        /// Gets or sets the longitude filter.
        /// </summary>
        public string LongitudeFilter { get; set; }

        /// <summary>
        /// Gets or sets the name filter.
        /// </summary>
        public string NameFilter { get; set; }

        /// <summary>
        /// Gets or sets the province name filter.
        /// </summary>
        public string ProvinceNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the street name filter.
        /// </summary>
        public string StreetNameFilter { get; set; }
    }
}