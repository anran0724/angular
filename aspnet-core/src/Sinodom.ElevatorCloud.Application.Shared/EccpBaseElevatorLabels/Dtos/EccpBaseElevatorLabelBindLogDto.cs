// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorLabelBindLogDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp base elevator label bind log dto.
    /// </summary>
    public class EccpBaseElevatorLabelBindLogDto : EntityDto<long>
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
        /// Gets or sets the elevator label id.
        /// </summary>
        public long ElevatorLabelId { get; set; }

        /// <summary>
        /// Gets or sets the label name.
        /// </summary>
        public string LabelName { get; set; }

        /// <summary>
        /// Gets or sets the label status id.
        /// </summary>
        public int LabelStatusId { get; set; }

        /// <summary>
        /// Gets or sets the local information.
        /// </summary>
        public string LocalInformation { get; set; }
    }
}