// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllECCPBaseRegisterCompaniesForExcelInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Dtos
{
    /// <summary>
    /// The get all eccp base register companies for excel input.
    /// </summary>
    public class GetAllECCPBaseRegisterCompaniesForExcelInput
    {
        /// <summary>
        /// Gets or sets the addresse filter.
        /// </summary>
        public string AddresseFilter { get; set; }

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

        /// <summary>
        /// Gets or sets the telephone filter.
        /// </summary>
        public string TelephoneFilter { get; set; }
    }
}