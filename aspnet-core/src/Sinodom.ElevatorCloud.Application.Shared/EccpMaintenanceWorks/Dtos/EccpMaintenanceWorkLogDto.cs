// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkLogDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp maintenance work log dto.
    /// </summary>
    public class EccpMaintenanceWorkLogDto : EntityDto<long>
    {
        /// <summary>
        /// Gets or sets the maintenance items name.
        /// </summary>
        public string MaintenanceItemsName { get; set; }

        /// <summary>
        /// Gets or sets the maintenance work flow id.
        /// </summary>
        public Guid MaintenanceWorkFlowId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance work flow name.
        /// </summary>
        public string MaintenanceWorkFlowName { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        public string Remark { get; set; }
    }
}