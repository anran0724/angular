// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetECCPBaseRegisterCompanyForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Dtos
{
    /// <summary>
    /// The get eccp base register company for edit output.
    /// </summary>
    public class GetECCPBaseRegisterCompanyForEditOutput
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
        /// Gets or sets the eccp base register company.
        /// </summary>
        public CreateOrEditECCPBaseRegisterCompanyDto ECCPBaseRegisterCompany { get; set; }

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