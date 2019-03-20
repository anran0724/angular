// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpElevatorQrCodeBindLogInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get eccp elevator qr code bind log input.
    /// </summary>
    public class GetEccpElevatorQrCodeBindLogInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the new id.
        /// </summary>
        public Guid? newId { get; set; }
    }
}