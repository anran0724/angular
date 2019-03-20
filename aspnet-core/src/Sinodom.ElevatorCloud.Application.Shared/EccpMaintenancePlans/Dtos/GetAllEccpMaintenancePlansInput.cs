// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenancePlansInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance plans input.
    /// </summary>
    public class GetAllEccpMaintenancePlansInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the is close filter.
        /// </summary>
        public int IsCloseFilter { get; set; }

        /// <summary>
        /// Gets or sets the max polling period filter.
        /// </summary>
        public int? MaxPollingPeriodFilter { get; set; }

        /// <summary>
        /// Gets or sets the max remind hour filter.
        /// </summary>
        public int? MaxRemindHourFilter { get; set; }

        /// <summary>
        /// Gets or sets the min polling period filter.
        /// </summary>
        public int? MinPollingPeriodFilter { get; set; }

        /// <summary>
        /// Gets or sets the min remind hour filter.
        /// </summary>
        public int? MinRemindHourFilter { get; set; }

        public int? MinElevatorNumFilter { get; set; }

        public int? MaxElevatorNumFilter { get; set; }
    }
}