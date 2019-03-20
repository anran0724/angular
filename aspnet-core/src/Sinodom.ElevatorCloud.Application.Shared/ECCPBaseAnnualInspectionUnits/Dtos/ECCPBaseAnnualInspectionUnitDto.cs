// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseAnnualInspectionUnitDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp base annual inspection unit dto.
    /// </summary>
    public class ECCPBaseAnnualInspectionUnitDto : EntityDto<long>
    {
        /// <summary>
        /// Gets or sets the addresse.
        /// </summary>
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
        /// Gets or sets the telephone.
        /// </summary>
        public string Telephone { get; set; }
    }
}