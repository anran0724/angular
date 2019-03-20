// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllByWorkOrderIdEccpMaintenanceWorkOrderEvaluationsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all by work order id eccp maintenance work order evaluations input.
    /// </summary>
    public class GetAllByWorkOrderIdEccpMaintenanceWorkOrderEvaluationsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the maintenance work order id filter.
        /// </summary>
        public int MaintenanceWorkOrderIdFilter { get; set; }
    }
}