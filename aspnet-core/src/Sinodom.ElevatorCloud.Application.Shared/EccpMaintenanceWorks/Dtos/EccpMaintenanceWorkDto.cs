// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp maintenance work dto.
    /// </summary>
    public class EccpMaintenanceWorkDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the maintenance template node id.
        /// </summary>
        public int? MaintenanceTemplateNodeId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance work order id.
        /// </summary>
        public int MaintenanceWorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the task name.
        /// </summary>
        public string TaskName { get; set; }
    }
}