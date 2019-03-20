// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditECCPBaseRegisterCompanyDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp base register company dto.
    /// </summary>
    public class CreateOrEditECCPBaseRegisterCompanyDto : FullAuditedEntityDto<long?>
    {
        /// <summary>
        /// Gets or sets the addresse.
        /// </summary>
        [Required]
        [StringLength(
            ECCPBaseRegisterCompanyConsts.MaxAddresseLength,
            MinimumLength = ECCPBaseRegisterCompanyConsts.MinAddresseLength)]
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
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(
            ECCPBaseRegisterCompanyConsts.MaxNameLength,
            MinimumLength = ECCPBaseRegisterCompanyConsts.MinNameLength)]
        public string Name { get; set; }

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
            ECCPBaseRegisterCompanyConsts.MaxTelephoneLength,
            MinimumLength = ECCPBaseRegisterCompanyConsts.MinTelephoneLength)]
        public string Telephone { get; set; }
    }
}