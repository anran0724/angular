// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseElevatorLookupTableDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos
{
    using System;

    /// <summary>
    /// The eccp base elevator lookup table dto.
    /// </summary>
    public class ECCPBaseElevatorLookupTableDto
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }
    }
}