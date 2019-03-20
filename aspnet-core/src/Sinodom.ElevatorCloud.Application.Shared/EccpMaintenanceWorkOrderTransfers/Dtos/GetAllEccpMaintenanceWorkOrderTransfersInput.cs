// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceWorkOrderTransfersInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrderTransfers.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance work order transfers input.
    /// </summary>
    public class GetAllEccpMaintenanceWorkOrderTransfersInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the term code filter.
        /// </summary>
        public string TermCodeFilter { get; set; }
    }
}