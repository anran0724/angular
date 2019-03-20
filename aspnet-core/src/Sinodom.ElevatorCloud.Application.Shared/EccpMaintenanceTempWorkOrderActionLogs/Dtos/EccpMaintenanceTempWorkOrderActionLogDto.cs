// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTempWorkOrderActionLogDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp maintenance temp work order action log dto.
    /// </summary>
    public class EccpMaintenanceTempWorkOrderActionLogDto : EntityDto<Guid>
    {
        /// <summary>
        /// Gets or sets the check state.
        /// </summary>
        public int CheckState { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the temp work order id.
        /// </summary>
        public Guid TempWorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public long UserId { get; set; }
    }
}