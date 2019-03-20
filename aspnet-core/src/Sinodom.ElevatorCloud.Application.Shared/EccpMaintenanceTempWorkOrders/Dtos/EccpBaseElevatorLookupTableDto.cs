// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorLookupTableDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos
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
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }
    }
}