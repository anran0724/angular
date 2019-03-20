// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditECCPBaseMaintenanceCompanyViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPBaseMaintenanceCompanies
{
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos;
    using System;

    /// <summary>
    /// The create or edit eccp base maintenance company modal view model.
    /// </summary>
    public class CreateOrEditECCPBaseMaintenanceCompanyViewModel
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
        /// Gets or sets the eccp base maintenance company.
        /// </summary>
        public EditECCPBaseMaintenanceCompanyDto EccpBaseMaintenanceCompany { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpBaseMaintenanceCompany.Id.HasValue;

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