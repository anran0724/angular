// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditECCPBaseAnnualInspectionUnitDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp base annual inspection unit dto.
    /// </summary>
    public class CreateOrEditECCPBaseAnnualInspectionUnitDto : FullAuditedEntityDto<long?>
    {
        /// <summary>
        /// Gets or sets the addresse.
        /// </summary>
        [Required]
        [StringLength(
            ECCPBaseAnnualInspectionUnitConsts.MaxAddresseLength,
            MinimumLength = ECCPBaseAnnualInspectionUnitConsts.MinAddresseLength)]
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
            ECCPBaseAnnualInspectionUnitConsts.MaxNameLength,
            MinimumLength = ECCPBaseAnnualInspectionUnitConsts.MinNameLength)]
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
            ECCPBaseAnnualInspectionUnitConsts.MaxTelephoneLength,
            MinimumLength = ECCPBaseAnnualInspectionUnitConsts.MinTelephoneLength)]
        public string Telephone { get; set; }
    }
}