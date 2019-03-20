// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditCompanyAuditViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpCompanyAudits
{
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos;

    /// <summary>
    /// The edit company audit view model.
    /// </summary>
    public class EditCompanyAuditViewModel
    {
        /// <summary>
        /// Gets or sets the check state name.
        /// </summary>
        public string CheckStateName { get; set; }

        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the district name.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the eccp company info.
        /// </summary>
        public EccpCompanyInfoDto EccpCompanyInfo { get; set; }

        /// <summary>
        /// Gets or sets the eccp company info extension.
        /// </summary>
        public EccpCompanyInfoExtensionDto EccpCompanyInfoExtension { get; set; }

        /// <summary>
        /// Gets or sets the edition type name.
        /// </summary>
        public string EditionTypeName { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

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