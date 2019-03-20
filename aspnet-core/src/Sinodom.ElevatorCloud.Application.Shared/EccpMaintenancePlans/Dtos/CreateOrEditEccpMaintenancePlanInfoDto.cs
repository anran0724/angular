// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenancePlanInfoDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    using System;
    using System.Collections.Generic;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance plan info dto.
    /// </summary>
    public class CreateOrEditEccpMaintenancePlanInfoDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the elevator id.
        /// </summary>
        public Guid ElevatorId { get; set; }

        /// <summary>
        ///     电梯ID
        /// </summary>
        public string ElevatorIds { get; set; }

        /// <summary>
        ///     电梯名称
        /// </summary>
        public string ElevatorNames { get; set; }

        /// <summary>
        /// Gets or sets the half year polling period.
        /// </summary>
        public int? HalfYearPollingPeriod { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is cancel.
        /// </summary>
        public bool IsCancel { get; set; }

        /// <summary>
        /// Gets or sets the is skip.
        /// </summary>
        public int IsSkip { get; set; }

        /// <summary>
        /// Gets or sets the maintenance types.
        /// </summary>
        public List<GetEccpDictMaintenanceTypeForView> MaintenanceTypes { get; set; }

        /// <summary>
        ///     维保用户ID
        /// </summary>
        public string MaintenanceUserIds { get; set; }

        /// <summary>
        ///     维保用户名称
        /// </summary>
        public string MaintenanceUserNames { get; set; }

        /// <summary>
        /// Gets or sets the plan group guid.
        /// </summary>
        public Guid? PlanGroupGuid { get; set; }

        /// <summary>
        /// Gets or sets the polling period.
        /// </summary>
        public int PollingPeriod { get; set; }

        /// <summary>
        ///     使用用户ID
        /// </summary>
        public string PropertyUserIds { get; set; }

        /// <summary>
        ///     使用用户姓名
        /// </summary>
        public string PropertyUserNames { get; set; }

        /// <summary>
        /// Gets or sets the quarter polling period.
        /// </summary>
        public int? QuarterPollingPeriod { get; set; }

        /// <summary>
        /// Gets or sets the remind hour.
        /// </summary>
        public int RemindHour { get; set; }

        /// <summary>
        /// Gets or sets the year polling period.
        /// </summary>
        public int? YearPollingPeriod { get; set; }
    }
}