// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetECCPBaseProductionCompanyForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseProductionCompanies.Dtos
{
    /// <summary>
    /// The get eccp base production company for edit output.
    /// </summary>
    public class GetECCPBaseProductionCompanyForEditOutput
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
        /// Gets or sets the eccp base production company.
        /// </summary>
        public CreateOrEditECCPBaseProductionCompanyDto ECCPBaseProductionCompany { get; set; }

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