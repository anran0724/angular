// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpElevatorChangeLogDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp elevator change log dto.
    /// </summary>
    public class EccpElevatorChangeLogDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the elevator id.
        /// </summary>
        public Guid ElevatorId { get; set; }

        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the new value.
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        public string OldValue { get; set; }
    }
}