// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp base elevator dto.
    /// </summary>
    public class EccpBaseElevatorDto : EntityDto<Guid>
    {
        /// <summary>
        /// Gets or sets the certificate num.
        /// </summary>
        public string CertificateNum { get; set; }

        /// <summary>
        /// Gets or sets the city id.
        /// </summary>
        public int? CityId { get; set; }

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
        public string InstallationAddress { get; set; }

        /// <summary>
        /// Gets or sets the installation datetime.
        /// </summary>
        public DateTime? InstallationDatetime { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the machine num.
        /// </summary>
        public string MachineNum { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the province id.
        /// </summary>
        public int? ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the street id.
        /// </summary>
        public int? StreetId { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}