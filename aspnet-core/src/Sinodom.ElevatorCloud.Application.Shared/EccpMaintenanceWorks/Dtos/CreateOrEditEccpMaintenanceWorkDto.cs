// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceWorkDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance work dto.
    /// </summary>
    public class CreateOrEditEccpMaintenanceWorkDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the maintenance work order id.
        /// </summary>
        public int MaintenanceWorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        [StringLength(
            EccpMaintenanceWorkConsts.MaxRemarkLength,
            MinimumLength = EccpMaintenanceWorkConsts.MinRemarkLength)]
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the task name.
        /// </summary>
        [Required]
        [StringLength(
            EccpMaintenanceWorkConsts.MaxTaskNameLength,
            MinimumLength = EccpMaintenanceWorkConsts.MinTaskNameLength)]
        public string TaskName { get; set; }
    }
}