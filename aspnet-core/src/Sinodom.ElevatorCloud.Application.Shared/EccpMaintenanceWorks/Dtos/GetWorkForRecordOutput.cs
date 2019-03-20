// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceWorkForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    /// <summary>
    /// The get eccp maintenance work for edit output.
    /// </summary>
    public class GetWorkForRecordOutput
    {
        /// <summary>
        /// iD
        /// </summary>
        public object Id { get; set; }
        /// <summary>
        ///工单类别 1 维保工单 2临时工单
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 工单类别名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 工作内容
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? CompletionTime { get; set; }

        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 工单类型
        /// </summary>
        public string TypeName { get; set; }
 
    }
}