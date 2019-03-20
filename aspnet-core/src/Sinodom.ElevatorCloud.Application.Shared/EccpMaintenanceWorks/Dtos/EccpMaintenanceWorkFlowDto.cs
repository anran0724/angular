// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkFlowDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp maintenance work flow dto.
    /// </summary>
    public class EccpMaintenanceWorkFlowDto : EntityDto<Guid>
    {
        /// <summary>
        /// Gets or sets the action code value.
        /// </summary>
        public string ActionCodeValue { get; set; }

        /// <summary>
        /// Gets or sets the dict maintenance work flow status id.
        /// </summary>
        public int DictMaintenanceWorkFlowStatusId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance template node id.
        /// </summary>
        public int MaintenanceTemplateNodeId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance work id.
        /// </summary>
        public int MaintenanceWorkId { get; set; }
    }
}