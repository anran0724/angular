// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpBaseElevatorDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp base elevator dto.
    /// </summary>
    public class CreateOrEditEccpBaseElevatorDto : FullAuditedEntityDto<Guid?>
    {
        /// <summary>
        /// Gets or sets the certificate num.
        /// </summary>
        [Required]
        [StringLength(
            EccpBaseElevatorConsts.MaxCertificateNumLength,
            MinimumLength = EccpBaseElevatorConsts.MinCertificateNumLength)]
        public string CertificateNum { get; set; }

        /// <summary>
        /// Gets or sets the city id.
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// Gets or sets the create or edit eccp base elevator subsidiary info dto.
        /// </summary>
        public CreateOrEditEccpBaseElevatorSubsidiaryInfoDto createOrEditEccpBaseElevatorSubsidiaryInfoDto { get; set; }

        /// <summary>
        /// Gets or sets the district id.
        /// </summary>
        public int? DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base annual inspection unit id.
        /// </summary>
        public long? ECCPBaseAnnualInspectionUnitId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base community id.
        /// </summary>
        public long? ECCPBaseCommunityId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator brand id.
        /// </summary>
        public int? EccpBaseElevatorBrandId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator model id.
        /// </summary>
        public int? EccpBaseElevatorModelId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base maintenance company id.
        /// </summary>
        public int? ECCPBaseMaintenanceCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base production company id.
        /// </summary>
        public long? ECCPBaseProductionCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base property company id.
        /// </summary>
        public int? ECCPBasePropertyCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base register company id.
        /// </summary>
        public long? ECCPBaseRegisterCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict elevator status id.
        /// </summary>
        public int? ECCPDictElevatorStatusId { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict elevator type id.
        /// </summary>
        public int? EccpDictElevatorTypeId { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict place type id.
        /// </summary>
        public int? EccpDictPlaceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the installation address.
        /// </summary>
        [StringLength(
            EccpBaseElevatorConsts.MaxInstallationAddressLength,
            MinimumLength = EccpBaseElevatorConsts.MinInstallationAddressLength)]
        public string InstallationAddress { get; set; }

        /// <summary>
        /// Gets or sets the installation datetime.
        /// </summary>
        public DateTime? InstallationDatetime { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [Required]
        [StringLength(
            EccpBaseElevatorConsts.MaxLatitudeLength,
            MinimumLength = EccpBaseElevatorConsts.MinLatitudeLength)]
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [Required]
        [StringLength(
            EccpBaseElevatorConsts.MaxLongitudeLength,
            MinimumLength = EccpBaseElevatorConsts.MinLongitudeLength)]
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the machine num.
        /// </summary>
        [StringLength(
            EccpBaseElevatorConsts.MaxMachineNumLength,
            MinimumLength = EccpBaseElevatorConsts.MinMachineNumLength)]
        public string MachineNum { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [StringLength(EccpBaseElevatorConsts.MaxNameLength, MinimumLength = EccpBaseElevatorConsts.MinNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the province id.
        /// </summary>
        public int? ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the street id.
        /// </summary>
        public int? StreetId { get; set; }
    }
}