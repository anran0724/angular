// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpElevatorChangeLogDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp elevator change log dto.
    /// </summary>
    public class CreateOrEditEccpElevatorChangeLogDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the elevator id.
        /// </summary>
        public Guid ElevatorId { get; set; }

        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        [Required]
        [StringLength(
            EccpElevatorChangeLogConsts.MaxFieldNameLength,
            MinimumLength = EccpElevatorChangeLogConsts.MinFieldNameLength)]
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the new value.
        /// </summary>
        [Required]
        [StringLength(
            EccpElevatorChangeLogConsts.MaxNewValueLength,
            MinimumLength = EccpElevatorChangeLogConsts.MinNewValueLength)]
        public string NewValue { get; set; }

        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        [Required]
        [StringLength(
            EccpElevatorChangeLogConsts.MaxOldValueLength,
            MinimumLength = EccpElevatorChangeLogConsts.MinOldValueLength)]
        public string OldValue { get; set; }
    }
}