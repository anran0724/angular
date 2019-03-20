// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceTroubledWorkOrderDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance troubled work order dto.
    /// </summary>
    public class CreateOrEditEccpMaintenanceTroubledWorkOrderDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the is audit.
        /// </summary>
        public int? IsAudit { get; set; }

        /// <summary>
        /// Gets or sets the maintenance work order id.
        /// </summary>
        public int MaintenanceWorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the troubled desc.
        /// </summary>
        [StringLength(
            EccpMaintenanceTroubledWorkOrderConsts.MaxTroubledDescLength,
            MinimumLength = EccpMaintenanceTroubledWorkOrderConsts.MinTroubledDescLength)]
        public string TroubledDesc { get; set; }

        /// <summary>
        /// Gets or sets the work order status name.
        /// </summary>
        [StringLength(
            EccpMaintenanceTroubledWorkOrderConsts.MaxWorkOrderStatusNameLength,
            MinimumLength = EccpMaintenanceTroubledWorkOrderConsts.MinWorkOrderStatusNameLength)]
        public string WorkOrderStatusName { get; set; }
    }
}