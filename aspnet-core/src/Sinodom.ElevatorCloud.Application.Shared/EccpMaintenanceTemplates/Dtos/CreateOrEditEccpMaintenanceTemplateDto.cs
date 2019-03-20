// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceTemplateDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance template dto.
    /// </summary>
    public class CreateOrEditEccpMaintenanceTemplateDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the elevator type id.
        /// </summary>
        public int ElevatorTypeId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance type id.
        /// </summary>
        public int MaintenanceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the temp allow.
        /// </summary>
        [StringLength(
            EccpMaintenanceTemplateConsts.MaxTempAllowLength,
            MinimumLength = EccpMaintenanceTemplateConsts.MinTempAllowLength)]
        public string TempAllow { get; set; }

        /// <summary>
        /// Gets or sets the temp condition.
        /// </summary>
        [StringLength(
            EccpMaintenanceTemplateConsts.MaxTempConditionLength,
            MinimumLength = EccpMaintenanceTemplateConsts.MinTempConditionLength)]
        public string TempCondition { get; set; }

        /// <summary>
        /// Gets or sets the temp deny.
        /// </summary>
        [StringLength(
            EccpMaintenanceTemplateConsts.MaxTempDenyLength,
            MinimumLength = EccpMaintenanceTemplateConsts.MinTempDenyLength)]
        public string TempDeny { get; set; }

        /// <summary>
        /// Gets or sets the temp desc.
        /// </summary>
        [StringLength(
            EccpMaintenanceTemplateConsts.MaxTempDescLength,
            MinimumLength = EccpMaintenanceTemplateConsts.MinTempDescLength)]
        public string TempDesc { get; set; }

        /// <summary>
        /// Gets or sets the temp name.
        /// </summary>
        [Required]
        [StringLength(
            EccpMaintenanceTemplateConsts.MaxTempNameLength,
            MinimumLength = EccpMaintenanceTemplateConsts.MinTempNameLength)]
        public string TempName { get; set; }
    }
}