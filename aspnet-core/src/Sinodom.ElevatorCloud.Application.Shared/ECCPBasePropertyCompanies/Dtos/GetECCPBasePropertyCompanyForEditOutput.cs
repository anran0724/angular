// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetECCPBasePropertyCompanyForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos
{
    /// <summary>
    /// The get eccp base property company for edit output.
    /// </summary>
    public class GetECCPBasePropertyCompanyForEditOutput
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
        /// Gets or sets the eccp base property company.
        /// </summary>
        public EditECCPBasePropertyCompanyDto ECCPBasePropertyCompany { get; set; }

        /// <summary>
        /// Gets or sets the province name.
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the street name.
        /// </summary>
        public string StreetName { get; set; }

        public Guid? BusinessLicenseId { get; set; }

        public Guid? AptitudePhotoId { get; set; }
    }
}