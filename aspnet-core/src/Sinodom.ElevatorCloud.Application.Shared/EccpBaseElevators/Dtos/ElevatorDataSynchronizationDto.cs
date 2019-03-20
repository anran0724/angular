// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElevatorDataSynchronizationDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    /// <summary>
    /// The elevator data synchronization dto.
    /// </summary>
    public class ElevatorDataSynchronizationDto
    {
        /// <summary>
        ///     市
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        ///     区
        /// </summary>
        public int? DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     省
        /// </summary>
        public int? ProvinceId { get; set; }
    }
}