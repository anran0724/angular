// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceTempWorkOrderForDetails.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos
{
    using System;

    /// <summary>
    /// The get eccp maintenance temp work order for ActionLog.
    /// </summary>
    public class MaintenanceTempWorkOrderActionLog
    {
        /// <summary>
        ///     Id
        /// </summary>
        public Guid Id { get; set; }
       
        /// <summary>
        ///     工单处理备注
        /// </summary>
        public string Remarks { get; set; }
                  
        /// <summary>
        ///   处理人
        /// </summary>
        public string UsreName { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

    }
}