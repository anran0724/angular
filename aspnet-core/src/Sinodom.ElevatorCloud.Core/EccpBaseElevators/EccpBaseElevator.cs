// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevator.cs" company="">
//   
// </copyright>
// <summary>
//   The eccp base elevator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;

    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits;
    using Sinodom.ElevatorCloud.ECCPBaseAreas;
    using Sinodom.ElevatorCloud.ECCPBaseCommunities;
    using Sinodom.ElevatorCloud.EccpBaseElevatorBrands;
    using Sinodom.ElevatorCloud.EccpBaseElevatorModels;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies;
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;

    /// <summary>
    /// The eccp base elevator.
    /// </summary>
    [Table("EccpBaseElevators")]
    public class EccpBaseElevator : FullAuditedEntity<Guid>, IHasArea
    {
        /// <summary>
        /// Gets or sets the certificate num.
        /// </summary>
        [Required]
        [StringLength(
            EccpBaseElevatorConsts.MaxCertificateNumLength,
            MinimumLength = EccpBaseElevatorConsts.MinCertificateNumLength)]
        public virtual string CertificateNum { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public ECCPBaseArea City { get; set; }

        /// <summary>
        /// Gets or sets the city id.
        /// </summary>
        public virtual int? CityId { get; set; }

        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        public ECCPBaseArea District { get; set; }

        /// <summary>
        /// Gets or sets the district id.
        /// </summary>
        public virtual int? DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base annual inspection unit.
        /// </summary>
        public ECCPBaseAnnualInspectionUnit ECCPBaseAnnualInspectionUnit { get; set; }

        /// <summary>
        /// Gets or sets the eccp base annual inspection unit id.
        /// </summary>
        public virtual long? ECCPBaseAnnualInspectionUnitId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base community.
        /// </summary>
        public ECCPBaseCommunity ECCPBaseCommunity { get; set; }

        /// <summary>
        /// Gets or sets the eccp base community id.
        /// </summary>
        public virtual long? ECCPBaseCommunityId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator brand.
        /// </summary>
        public EccpBaseElevatorBrand EccpBaseElevatorBrand { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator brand id.
        /// </summary>
        public virtual int? EccpBaseElevatorBrandId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator model.
        /// </summary>
        public EccpBaseElevatorModel EccpBaseElevatorModel { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator model id.
        /// </summary>
        public virtual int? EccpBaseElevatorModelId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base maintenance company.
        /// </summary>
        public ECCPBaseMaintenanceCompany ECCPBaseMaintenanceCompany { get; set; }

        /// <summary>
        /// Gets or sets the eccp base maintenance company id.
        /// </summary>
        public virtual int? ECCPBaseMaintenanceCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base property company.
        /// </summary>
        public ECCPBasePropertyCompany ECCPBasePropertyCompany { get; set; }

        /// <summary>
        /// Gets or sets the eccp base property company id.
        /// </summary>
        public virtual int? ECCPBasePropertyCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base production company.
        /// </summary>
        public ECCPBaseProductionCompany ECCPBaseProductionCompany { get; set; }

        /// <summary>
        /// Gets or sets the eccp base production company id.
        /// </summary>
        public virtual long? ECCPBaseProductionCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base register company.
        /// </summary>
        public ECCPBaseRegisterCompany ECCPBaseRegisterCompany { get; set; }

        /// <summary>
        /// Gets or sets the eccp base register company id.
        /// </summary>
        public virtual long? ECCPBaseRegisterCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict elevator status.
        /// </summary>
        public ECCPDictElevatorStatus ECCPDictElevatorStatus { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict elevator status id.
        /// </summary>
        public virtual int? ECCPDictElevatorStatusId { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict elevator type.
        /// </summary>
        public EccpDictElevatorType EccpDictElevatorType { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict elevator type id.
        /// </summary>
        public virtual int? EccpDictElevatorTypeId { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict place type.
        /// </summary>
        public EccpDictPlaceType EccpDictPlaceType { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict place type id.
        /// </summary>
        public virtual int? EccpDictPlaceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the installation address.
        /// </summary>
        [StringLength(
            EccpBaseElevatorConsts.MaxInstallationAddressLength,
            MinimumLength = EccpBaseElevatorConsts.MinInstallationAddressLength)]
        public virtual string InstallationAddress { get; set; }

        /// <summary>
        /// Gets or sets the installation datetime.
        /// </summary>
        public virtual DateTime? InstallationDatetime { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [Required]
        [StringLength(
            EccpBaseElevatorConsts.MaxLatitudeLength,
            MinimumLength = EccpBaseElevatorConsts.MinLatitudeLength)]
        public virtual string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [Required]
        [StringLength(
            EccpBaseElevatorConsts.MaxLongitudeLength,
            MinimumLength = EccpBaseElevatorConsts.MinLongitudeLength)]
        public virtual string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the machine num.
        /// </summary>
        [StringLength(
            EccpBaseElevatorConsts.MaxMachineNumLength,
            MinimumLength = EccpBaseElevatorConsts.MinMachineNumLength)]
        public virtual string MachineNum { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [StringLength(EccpBaseElevatorConsts.MaxNameLength, MinimumLength = EccpBaseElevatorConsts.MinNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        public ECCPBaseArea Province { get; set; }

        /// <summary>
        /// Gets or sets the province id.
        /// </summary>
        public virtual int? ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        public ECCPBaseArea Street { get; set; }

        /// <summary>
        /// Gets or sets the street id.
        /// </summary>
        public virtual int? StreetId { get; set; }

        /// <summary>
        /// Gets or sets the sync elevator id.
        /// </summary>
        public virtual int? SyncElevatorId { get; set; }

        /// <summary>
        /// Gets or sets the org level.
        /// </summary>
        public virtual string OrgLevel { get; set; }
    }
}