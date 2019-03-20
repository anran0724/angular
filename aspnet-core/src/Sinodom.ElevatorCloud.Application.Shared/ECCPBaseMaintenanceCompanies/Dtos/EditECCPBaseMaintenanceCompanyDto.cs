// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditECCPBaseMaintenanceCompanyDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp base maintenance company dto.
    /// </summary>
    public class EditECCPBaseMaintenanceCompanyDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the addresse.
        /// </summary>
        [Required]
        [StringLength(
            ECCPBaseMaintenanceCompanyConsts.MaxAddresseLength,
            MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinAddresseLength)]
        public string Addresse { get; set; }

        /// <summary>
        /// Gets or sets the city id.
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// Gets or sets the district id.
        /// </summary>
        public int? DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [Required]
        [StringLength(
            ECCPBaseMaintenanceCompanyConsts.MaxLatitudeLength,
            MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinLatitudeLength)]
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [Required]
        [StringLength(
            ECCPBaseMaintenanceCompanyConsts.MaxLongitudeLength,
            MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinLongitudeLength)]
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(
            ECCPBaseMaintenanceCompanyConsts.MaxNameLength,
            MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the org organizational code.
        /// </summary>
        public string OrgOrganizationalCode { get; set; }

        /// <summary>
        /// Gets or sets the province id.
        /// </summary>
        public int? ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the street id.
        /// </summary>
        public int? StreetId { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the telephone.
        /// </summary>
        [Required]
        [StringLength(
            ECCPBaseMaintenanceCompanyConsts.MaxTelephoneLength,
            MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinTelephoneLength)]
        public string Telephone { get; set; }
      
    }
}