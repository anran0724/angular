// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenancePlanDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance plan dto.
    /// </summary>
    public class CreateOrEditEccpMaintenancePlanDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the elevator id.
        /// </summary>
        public Guid ElevatorId { get; set; }

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