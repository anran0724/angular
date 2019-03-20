// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceWorkOrderEvaluationDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance work order evaluation dto.
    /// </summary>
    public class CreateOrEditEccpMaintenanceWorkOrderEvaluationDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        [StringLength(
            EccpMaintenanceWorkOrderEvaluationConsts.MaxRemarksLength,
            MinimumLength = EccpMaintenanceWorkOrderEvaluationConsts.MinRemarksLength)]
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the work order id.
        /// </summary>
        public int WorkOrderId { get; set; }
    }
}