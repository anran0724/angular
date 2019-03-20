// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorLookupTableDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    /// <summary>
    /// The eccp base elevator lookup table dto.
    /// </summary>
    public class EccpBaseElevatorLookupTableDto
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        public string Distance { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }
    }
}