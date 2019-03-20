// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceWorkForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    /// <summary>
    ///  维保工作记录明细
    /// </summary>
    public class GetEccpMaintenanceWorkFlowDetails
    {                           
        /// <summary>
        /// 工作记录id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        ///操作code
        /// </summary>
        public string ActionCode { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string TemplateNodeName { get; set; }
        /// <summary>
        /// 工作要求
        /// </summary>
        public string NodeDesc { get; set; }
        /// <summary>
        ///  节点类别
        /// </summary>
        public string NodeTypeName { get; set; }
        /// <summary>
        /// 工作值
        /// </summary>
        public string ActionCodeValue { get; set; }
        /// <summary>
        /// 节点状态
        /// </summary>
        public string WorkFlowStatusName { get; set; }
        /// <summary>
        /// 节点备注
        /// </summary>
        public string WorkFlowRemark { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 不合格
        /// </summary>
        public List<MaintenanceItem> MaintenanceItems { get; set; }

    }

    



}