// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenancePlanViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenancePlans
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos;

    /// <summary>
    /// The create or edit eccp maintenance plan view model.
    /// </summary>
    public class CreateOrEditEccpMaintenancePlanViewModel
    {
        /// <summary>
        /// Gets or sets the eccp base elevator name.
        /// </summary>
        public string EccpBaseElevatorName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance plan.
        /// </summary>
        public CreateOrEditEccpMaintenancePlanDto EccpMaintenancePlan { get; set; }

        /// <summary>
        /// Gets or sets the elevator ids.
        /// 电梯ID
        /// </summary>
        public string ElevatorIds { get; set; }

        /// <summary>
        /// Gets or sets the elevator names.
        /// 电梯名称
        /// </summary>
        public string ElevatorNames { get; set; }

        /// <summary>
        /// Gets or sets the half year polling period.
        /// </summary>
        public int HalfYearPollingPeriod { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpMaintenancePlan.Id.HasValue;

        /// <summary>
        /// Gets or sets the maintenance types.
        /// </summary>
        public List<GetEccpDictMaintenanceTypeForView> MaintenanceTypes { get; set; }

        /// <summary>
        /// Gets or sets the maintenance user ids.
        /// 维保用户ID
        /// </summary>
        public string MaintenanceUserIds { get; set; }

        /// <summary>
        /// Gets or sets the maintenance user names.
        /// 维保用户名称
        /// </summary>
        public string MaintenanceUserNames { get; set; }

        /// <summary>
        /// Gets or sets the property user ids.
        /// 使用用户ID
        /// </summary>
        public string PropertyUserIds { get; set; }

        /// <summary>
        /// Gets or sets the property user names.
        /// 使用用户姓名
        /// </summary>
        public string PropertyUserNames { get; set; }

        /// <summary>
        /// Gets or sets the quarter polling period.
        /// </summary>
        public int QuarterPollingPeriod { get; set; }

        /// <summary>
        /// Gets or sets the year polling period.
        /// </summary>
        public int YearPollingPeriod { get; set; }
    }
}