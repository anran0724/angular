// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllForLookupTableInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all for lookup table input.
    /// </summary>
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the elevator id.
        /// </summary>
        public Guid? ElevatorId { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the maintenance type id.
        /// </summary>
        public int MaintenanceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the plan group guid.
        /// </summary>
        public Guid? PlanGroupGuid { get; set; }
    }
}