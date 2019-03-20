// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceWorkOrdersForExcelInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using System;

    /// <summary>
    /// The get all eccp maintenance work orders for excel input.
    /// </summary>
    public class GetAllEccpMaintenanceWorkOrdersForExcelInput
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
        /// Gets or sets the eccp maintenance plan polling period filter.
        /// </summary>
        public string EccpMaintenancePlanPollingPeriodFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

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