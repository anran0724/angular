// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllByElevatorIdEccpMaintenanceWorkOrderEvaluationsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all by elevator id eccp maintenance work order evaluations input.
    /// </summary>
    public class GetAllByElevatorIdEccpMaintenanceWorkOrderEvaluationsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the elevator id filter.
        /// </summary>
        public string ElevatorIdFilter { get; set; }
    }
}