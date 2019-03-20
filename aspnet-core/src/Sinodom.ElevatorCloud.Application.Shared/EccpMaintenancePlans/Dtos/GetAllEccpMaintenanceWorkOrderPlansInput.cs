// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceWorkOrdersInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance work orders input.
    /// </summary>
    public class GetAllEccpMaintenanceWorkOrderPlansInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp dict maintenance status name filter.
        /// </summary>
        public string EccpDictMaintenanceStatusNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict maintenance type name filter.
        /// </summary>
        public string EccpDictMaintenanceTypeNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp elevator name filter.
        /// </summary>
        public string EccpElevatorNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance plan polling period filter.
        /// </summary>
        public string EccpMaintenancePlanPollingPeriodFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance user name filter.
        /// </summary>
        public string EccpMaintenanceUserNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp property user name filter.
        /// </summary>
        public string EccpPropertyUserNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }
        public int PlanIdFilter { get; set; }

        /// <summary>
        /// Gets or sets the is closed filter.
        /// </summary>
        public int IsClosedFilter { get; set; }

        /// <summary>
        /// Gets or sets the is passed filter.
        /// </summary>
        public int IsPassedFilter { get; set; }

        /// <summary>
        /// Gets or sets the max latitude filter.
        /// </summary>
        public double? MaxLatitudeFilter { get; set; }

        /// <summary>
        /// Gets or sets the max longitude filter.
        /// </summary>
        public double? MaxLongitudeFilter { get; set; }

        /// <summary>
        /// Gets or sets the max plan check date filter.
        /// </summary>
        public DateTime? MaxPlanCheckDateFilter { get; set; }

        /// <summary>
        /// Gets or sets the min latitude filter.
        /// </summary>
        public double? MinLatitudeFilter { get; set; }

        /// <summary>
        /// Gets or sets the min longitude filter.
        /// </summary>
        public double? MinLongitudeFilter { get; set; }

        /// <summary>
        /// Gets or sets the min plan check date filter.
        /// </summary>
        public DateTime? MinPlanCheckDateFilter { get; set; }
    }
}