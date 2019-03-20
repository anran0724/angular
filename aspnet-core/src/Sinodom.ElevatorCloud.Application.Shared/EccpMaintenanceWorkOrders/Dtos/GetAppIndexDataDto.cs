// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAppIndexDataDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    /// <summary>
    /// The get app index data dto.
    /// </summary>
    public class GetAppIndexDataDto
    {
        /// <summary>
        ///     获取临时工单
        /// </summary>
        public GetTempWorkOrderDto TempWorkOrder { get; set; }

        /// <summary>
        ///     本月工单统计
        /// </summary>
        public GetThisMonthWorkOrderStatisticsDto ThisMonthWorkOrderStatistics { get; set; }

        /// <summary>
        ///     今日工单统计
        /// </summary>
        public GetTodayWorkOrderStatisticsDto TodayWorkOrderStatistics { get; set; }

        /// <summary>
        ///     获取待维保工单
        /// </summary>
        public GetWaitMaintenanceDto WaitMaintenance { get; set; }
    }
}