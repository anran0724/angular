// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceTempWorkOrdersInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance temp work orders input.
    /// </summary>
    public class GetAllEccpMaintenanceTempWorkOrdersInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp base maintenance company name filter.
        /// </summary>
        public string ECCPBaseMaintenanceCompanyNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the max check state filter.
        /// </summary>
        public int? MaxCheckStateFilter { get; set; }

        /// <summary>
        /// Gets or sets the max completion time filter.
        /// </summary>
        public DateTime? MaxCompletionTimeFilter { get; set; }

        /// <summary>
        /// Gets or sets the min check state filter.
        /// </summary>
        public int? MinCheckStateFilter { get; set; }

        /// <summary>
        /// Gets or sets the min completion time filter.
        /// </summary>
        public DateTime? MinCompletionTimeFilter { get; set; }

        /// <summary>
        /// Gets or sets the user name filter.
        /// </summary>
        public string UserNameFilter { get; set; }
        public long? UserId { get; set; }
    }
}