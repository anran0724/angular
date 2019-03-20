// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetECCPBaseRegisterCompanyForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Dtos
{
    /// <summary>
    /// The get eccp base register company for view.
    /// </summary>
    public class GetECCPBaseRegisterCompanyForView
    {
        /// <summary>
        /// Gets or sets the city name.
        /// 市
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the district name.
        /// 区
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base register company.
        /// </summary>
        public ECCPBaseRegisterCompanyDto ECCPBaseRegisterCompany { get; set; }

        /// <summary>
        /// Gets or sets the province name.
        /// 省
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the street name.
        /// 街道
        /// </summary>
        public string StreetName { get; set; }
    }
}