using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class AppIndexMapMaintenanceStatisticsDataDto
    {
        /// <summary>
        /// 已完成维保电梯数量
        /// </summary>
        public int NumberOfElevatorsCompletedMaintenance { get; set; }
        /// <summary>
        /// 待维保电梯数量
        /// </summary>
        public int NumberOfElevatorsToBeMaintained { get; set; }
        /// <summary>
        /// 临期电梯数量
        /// </summary>
        public int QuantityOfElevatorsInTransit { get; set; }
        /// <summary>
        /// 超期电梯数量
        /// </summary>
        public int NumberOfOverdueElevators { get; set; }
        /// <summary>
        /// 有临时工单的电梯数量
        /// </summary>
        public int NumberOfElevatorsWithTempWorkOrders { get; set; }
        /// <summary>
        /// 故障电梯数量
        /// </summary>
        public int NumberOfFailedElevators { get; set; }
        /// <summary>
        /// 投诉电梯数量
        /// </summary>
        public int NumberOfComplaintElevators { get; set; }
        /// <summary>
        /// 超期未检电梯数量
        /// </summary>
        public int OverdueElevatorQuantity { get; set; }
        /// <summary>
        /// 在线维保人员数量
        /// </summary>
        public int NumberOfOnlineMaintenancePersonnel { get; set; }
        /// <summary>
        /// 拥有电梯数量
        /// </summary>
        public int NumberOfElevatorsOwned { get; set; }
        /// <summary>
        /// 应急救援数
        /// </summary>
        public int NumberOfEmergencyRescue { get; set; }
    }
}
