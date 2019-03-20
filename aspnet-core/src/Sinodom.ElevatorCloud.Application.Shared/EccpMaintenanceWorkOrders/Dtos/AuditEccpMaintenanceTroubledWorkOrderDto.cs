// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuditEccpMaintenanceTroubledWorkOrderDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The audit eccp maintenance troubled work order dto.
    /// </summary>
    public class AuditEccpMaintenanceTroubledWorkOrderDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the is audit.
        /// </summary>
        public int? IsAudit { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        [StringLength(
            EccpMaintenanceTroubledWorkOrderConsts.MaxTroubledDescLength,
            MinimumLength = EccpMaintenanceTroubledWorkOrderConsts.MinTroubledDescLength)]
        public string Remarks { get; set; }
    }
}