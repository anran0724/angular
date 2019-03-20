// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceTempWorkOrderTransfersAuditLogInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrderTransfers.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance temp work order transfers audit log input.
    /// </summary>
    public class GetAllEccpMaintenanceTempWorkOrderTransfersAuditLogInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the term code filter.
        /// </summary>
        public string TermCodeFilter { get; set; }
    }
}