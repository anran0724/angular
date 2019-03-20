// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetWaitMaintenanceDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using System;

    /// <summary>
    /// The get wait maintenance dto.
    /// </summary>
    public class GetWaitMaintenanceDto
    {
        /// <summary>
        ///     时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        ///     三天内超期电梯数
        /// </summary>
        public int OverdueElevatorInThreeDaysNum { get; set; }

        /// <summary>
        ///     已超期电梯数
        /// </summary>
        public int OverdueElevatorNum { get; set; }

        /// <summary>
        ///     本周待维保电梯数
        /// </summary>
        public int ThisWeekWaitMaintenanceElevatorNum { get; set; }
    }
}