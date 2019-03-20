// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetTempWorkOrderDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using System;

    /// <summary>
    /// The get temp work order dto.
    /// </summary>
    public class GetTempWorkOrderDto
    {
        /// <summary>
        ///     时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        ///     优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        ///     工单类型
        /// </summary>
        public string WorkOrderType { get; set; }
    }
}