// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenancePlansForExcelInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    /// <summary>
    /// The get all eccp maintenance plans for excel input.
    /// </summary>
    public class GetAllEccpMaintenancePlansForExcelInput
    {
        /// <summary>
        /// Gets or sets the eccp base elevator name filter.
        /// </summary>
        public string EccpBaseElevatorNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the max polling period filter.
        /// </summary>
        public int? MaxPollingPeriodFilter { get; set; }

        /// <summary>
        /// Gets or sets the max remind hour filter.
        /// </summary>
        public int? MaxRemindHourFilter { get; set; }

        /// <summary>
        /// Gets or sets the min polling period filter.
        /// </summary>
        public int? MinPollingPeriodFilter { get; set; }

        /// <summary>
        /// Gets or sets the min remind hour filter.
        /// </summary>
        public int? MinRemindHourFilter { get; set; }
    }
}