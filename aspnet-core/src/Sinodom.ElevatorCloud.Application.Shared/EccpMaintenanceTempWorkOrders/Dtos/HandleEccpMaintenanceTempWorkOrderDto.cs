// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandleEccpMaintenanceTempWorkOrderDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos
{
    using System;

    /// <summary>
    /// The handle eccp maintenance temp work order dto.
    /// </summary>
    public class HandleEccpMaintenanceTempWorkOrderDto
    {
        /// <summary>
        ///     工单状态 0 待处理 1 已完成 2 跟进中 3已拒绝
        /// </summary>
        public int CheckState { get; set; }

        /// <summary>
        ///     临时工单ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     临时工单处置备注
        /// </summary>
        public string Remarks { get; set; }
    }
}