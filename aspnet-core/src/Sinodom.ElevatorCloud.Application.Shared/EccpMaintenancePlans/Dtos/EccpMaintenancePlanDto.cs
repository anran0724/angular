// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenancePlanDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp maintenance plan dto.
    /// </summary>
    public class EccpMaintenancePlanDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the elevator id.
        /// </summary>
        public Guid ElevatorId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is close.
        /// </summary>
        public bool IsClose { get; set; }

        /// <summary>
        /// Gets or sets the plan group guid.
        /// </summary>
        public Guid? PlanGroupGuid { get; set; }

        /// <summary>
        /// Gets or sets the polling period.
        /// </summary>
        public int PollingPeriod { get; set; }

        /// <summary>
        /// Gets or sets the remind hour.
        /// </summary>
        public int RemindHour { get; set; }
    }
}