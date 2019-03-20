// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditECCPBaseCommunityDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseCommunities.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp base community dto.
    /// </summary>
    public class CreateOrEditECCPBaseCommunityDto : FullAuditedEntityDto<long?>
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [Required]
        [StringLength(
            ECCPBaseCommunityConsts.MaxAddressLength,
            MinimumLength = ECCPBaseCommunityConsts.MinAddressLength)]
        public string Address { get; set; }

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
            ECCPBaseCommunityConsts.MaxLatitudeLength,
            MinimumLength = ECCPBaseCommunityConsts.MinLatitudeLength)]
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [Required]
        [StringLength(
            ECCPBaseCommunityConsts.MaxLongitudeLength,
            MinimumLength = ECCPBaseCommunityConsts.MinLongitudeLength)]
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(ECCPBaseCommunityConsts.MaxNameLength, MinimumLength = ECCPBaseCommunityConsts.MinNameLength)]
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