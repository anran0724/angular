// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaintenanceTemplatesLookupTableDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    /// <summary>
    /// The maintenance templates lookup table dto.
    /// </summary>
    public class MaintenanceTemplatesLookupTableDto
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is default.
        /// </summary>
        public bool IsDefault { get; set; }
    }
}