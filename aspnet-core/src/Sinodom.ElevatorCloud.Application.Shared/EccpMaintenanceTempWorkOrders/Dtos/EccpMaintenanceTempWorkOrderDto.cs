// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTempWorkOrderDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp maintenance temp work order dto.
    /// </summary>
    public class EccpMaintenanceTempWorkOrderDto : EntityDto<Guid>
    {
        /// <summary>
        /// Gets or sets the check state.
        /// </summary>
        public int CheckState { get; set; }

        /// <summary>
        /// Gets or sets the completion time.
        /// </summary>
        public DateTime? CompletionTime { get; set; }

        /// <summary>
        /// Gets or sets the maintenance company id.
        /// </summary>
        public int MaintenanceCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public long? UserId { get; set; }
    }
}