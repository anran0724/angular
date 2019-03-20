// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrderEvaluationDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp maintenance work order evaluation dto.
    /// </summary>
    public class EccpMaintenanceWorkOrderEvaluationDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the work order id.
        /// </summary>
        public int WorkOrderId { get; set; }
    }
}