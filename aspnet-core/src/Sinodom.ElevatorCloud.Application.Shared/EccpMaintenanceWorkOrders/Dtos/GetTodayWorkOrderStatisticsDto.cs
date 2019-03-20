// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetTodayWorkOrderStatisticsDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using System;

    /// <summary>
    /// The get today work order statistics dto.
    /// </summary>
    public class GetTodayWorkOrderStatisticsDto
    {
        /// <summary>
        ///     时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        ///     临时工单数
        /// </summary>
        public int MaintenanceTempWorkOrderNum { get; set; }

        /// <summary>
        ///     维保工单数
        /// </summary>
        public int MaintenanceWorkOrderNum { get; set; }

        /// <summary>
        ///     总计完成工单数
        /// </summary>
        public int TotalCompletionSheetNum { get; set; }
    }
}