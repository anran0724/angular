// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpBaseElevatorLabelDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp base elevator label dto.
    /// </summary>
    public class CreateOrEditEccpBaseElevatorLabelDto : FullAuditedEntityDto<long?>
    {
        /// <summary>
        /// Gets or sets the binary objects id.
        /// </summary>
        public Guid BinaryObjectsId { get; set; }

        /// <summary>
        /// Gets or sets the binding time.
        /// </summary>
        public DateTime? BindingTime { get; set; }

        /// <summary>
        /// Gets or sets the elevator id.
        /// </summary>
        public Guid? ElevatorId { get; set; }

        /// <summary>
        /// Gets or sets the label name.
        /// </summary>
        [Required]
        [StringLength(
            EccpBaseElevatorLabelConsts.MaxLabelNameLength,
            MinimumLength = EccpBaseElevatorLabelConsts.MinLabelNameLength)]
        public string LabelName { get; set; }

        /// <summary>
        /// Gets or sets the label status id.
        /// </summary>
        public int LabelStatusId { get; set; }

        /// <summary>
        /// Gets or sets the local information.
        /// </summary>
        [StringLength(
            EccpBaseElevatorLabelConsts.MaxLocalInformationLength,
            MinimumLength = EccpBaseElevatorLabelConsts.MinLocalInformationLength)]
        public string LocalInformation { get; set; }

        /// <summary>
        /// Gets or sets the unique id.
        /// </summary>
        [Required]
        [StringLength(
            EccpBaseElevatorLabelConsts.MaxUniqueIdLength,
            MinimumLength = EccpBaseElevatorLabelConsts.MinUniqueIdLength)]
        public string UniqueId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public long? UserId { get; set; }
    }
}