using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos
{
    public class EditECCPBasePropertyCompanyDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the addresse.
        /// </summary>
        [Required]
        [StringLength(
            ECCPBasePropertyCompanyConsts.MaxAddresseLength,
            MinimumLength = ECCPBasePropertyCompanyConsts.MinAddresseLength)]
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
            ECCPBasePropertyCompanyConsts.MaxLatitudeLength,
            MinimumLength = ECCPBasePropertyCompanyConsts.MinLatitudeLength)]
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [Required]
        [StringLength(
            ECCPBasePropertyCompanyConsts.MaxLongitudeLength,
            MinimumLength = ECCPBasePropertyCompanyConsts.MinLongitudeLength)]
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(
            ECCPBasePropertyCompanyConsts.MaxNameLength,
            MinimumLength = ECCPBasePropertyCompanyConsts.MinNameLength)]
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
            ECCPBasePropertyCompanyConsts.MaxTelephoneLength,
            MinimumLength = ECCPBasePropertyCompanyConsts.MinTelephoneLength)]
        public string Telephone { get; set; }

    }
}
