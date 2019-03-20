// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceTempWorkOrderForDetails.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos
{
    using System;

    /// <summary>
    /// The get eccp maintenance temp work order for details.
    /// </summary>
    public class GetEccpMaintenanceTempWorkOrderDetails
    {
        /// <summary>
        ///     Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     工单状态
        /// </summary>
        public int CheckState { get; set; }

        /// <summary>
        ///     工单状态名
        /// </summary>
        public string CheckStateName { get; set; }

        /// <summary>
        ///     完成时间
        /// </summary>
        public DateTime? CompletionTime { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        ///     发布人
        /// </summary>
        public long? CreatorUserId { get; set; }

        /// <summary>
        ///     发布人 手机
        /// </summary>
        public string CreatorUserMobile { get; set; }

        /// <summary>
        ///     发布人
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        ///     工单描述
        /// </summary>
        public string Describe { get; set; }
       
        /// <summary>
        ///     维保单位名称
        /// </summary>
        public string MaintenanceCompanyNmae { get; set; }

        /// <summary>
        ///     优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        ///     工单处理备注
        /// </summary>
        public string Remarks { get; set; }
 
        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     工单负责人
        /// </summary>
        public string UsreName { get; set; }
 
        /// <summary>
        /// 操作日志
        /// </summary>
        public List<MaintenanceTempWorkOrderActionLog> MaintenanceTempWorkOrderActionLogs { get; set; }

    }
}