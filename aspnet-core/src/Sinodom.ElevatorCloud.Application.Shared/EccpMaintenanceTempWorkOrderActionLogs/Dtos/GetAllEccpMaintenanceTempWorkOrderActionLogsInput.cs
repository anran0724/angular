// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceTempWorkOrderActionLogsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance temp work order action logs input.
    /// </summary>
    public class GetAllEccpMaintenanceTempWorkOrderActionLogsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp maintenance temp work order title filter.
        /// </summary>
        public string EccpMaintenanceTempWorkOrderTitleFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the max check state filter.
        /// </summary>
        public int? MaxCheckStateFilter { get; set; }

        /// <summary>
        /// Gets or sets the min check state filter.
        /// </summary>
        public int? MinCheckStateFilter { get; set; }

        /// <summary>
        /// Gets or sets the user name filter.
        /// </summary>
        public string UserNameFilter { get; set; }
    }
}