// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceWorkOrderEvaluationsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance work order evaluations input.
    /// </summary>
    public class GetAllEccpMaintenanceWorkOrderEvaluationsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp maintenance work order remark filter.
        /// </summary>
        public string EccpMaintenanceWorkOrderRemarkFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the max rank filter.
        /// </summary>
        public int? MaxRankFilter { get; set; }

        /// <summary>
        /// Gets or sets the min rank filter.
        /// </summary>
        public int? MinRankFilter { get; set; }
    }
}