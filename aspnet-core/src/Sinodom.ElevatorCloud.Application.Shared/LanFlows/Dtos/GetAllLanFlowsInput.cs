// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceTroubledWorkOrdersInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance troubled work orders input.
    /// </summary>
    public class GetAllLanFlowsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp maintenance troubled desc filter.
        /// </summary>
        public string EccpMaintenanceTroubledDescFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order remark filter.
        /// </summary>
        public string EccpMaintenanceWorkOrderRemarkFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the is audit filter.
        /// </summary>
        public int? IsAuditFilter { get; set; }
    }
}