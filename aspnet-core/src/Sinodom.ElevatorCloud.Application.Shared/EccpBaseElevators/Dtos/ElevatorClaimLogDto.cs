// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElevatorClaimLogDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The elevator claim log dto.
    /// </summary>
    public class ElevatorClaimLogDto : EntityDto<long>
    {
        /// <summary>
        /// Gets or sets the elevator id.
        /// </summary>
        public Guid ElevatorId { get; set; }
    }
}