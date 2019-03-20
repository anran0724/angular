// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceWorkLogDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance work log dto.
    /// </summary>
    public class CreateOrEditEccpMaintenanceWorkLogDto : FullAuditedEntityDto<long?>
    {
        /// <summary>
        /// Gets or sets the maintenance items name.
        /// </summary>
        [StringLength(
            EccpMaintenanceWorkLogConsts.MaxMaintenanceItemsNameLength,
            MinimumLength = EccpMaintenanceWorkLogConsts.MinMaintenanceItemsNameLength)]
        public string MaintenanceItemsName { get; set; }

        /// <summary>
        /// Gets or sets the maintenance work flow id.
        /// </summary>
        public int MaintenanceWorkFlowId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance work flow name.
        /// </summary>
        [Required]
        [StringLength(
            EccpMaintenanceWorkLogConsts.MaxMaintenanceWorkFlowNameLength,
            MinimumLength = EccpMaintenanceWorkLogConsts.MinMaintenanceWorkFlowNameLength)]
        public string MaintenanceWorkFlowName { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        [StringLength(
            EccpMaintenanceWorkLogConsts.MaxRemarkLength,
            MinimumLength = EccpMaintenanceWorkLogConsts.MinRemarkLength)]
        public string Remark { get; set; }
    }
}