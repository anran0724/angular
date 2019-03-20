// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceTempWorkOrderDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance temp work order dto.
    /// </summary>
    public class CreateOrEditEccpMaintenanceTempWorkOrderDto : CreationAuditedEntityDto<Guid?>
    {
        /// <summary>
        /// Gets or sets the completion time.
        /// </summary>
        public DateTime? CompletionTime { get; set; }

        /// <summary>
        /// Gets or sets the describe.
        /// </summary>
        [StringLength(
            EccpMaintenanceTempWorkOrderConsts.MaxDescribeLength,
            MinimumLength = EccpMaintenanceTempWorkOrderConsts.MinDescribeLength)]
        public string Describe { get; set; }

        /// <summary>
        /// Gets or sets the maintenance company id.
        /// </summary>
        public int MaintenanceCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the temp work order type id.
        /// </summary>
        public int TempWorkOrderTypeId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [Required]
        [StringLength(
            EccpMaintenanceTempWorkOrderConsts.MaxTitleLength,
            MinimumLength = EccpMaintenanceTempWorkOrderConsts.MinTitleLength)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public long? UserId { get; set; }
        public Guid ElevatorId { get; set; }
    }
}