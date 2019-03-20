// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditElevatorClaimLogDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit elevator claim log dto.
    /// </summary>
    public class CreateOrEditElevatorClaimLogDto : FullAuditedEntityDto<long?>
    {
        /// <summary>
        /// Gets or sets the elevator id.
        /// </summary>
        public Guid ElevatorId { get; set; }
    }
}