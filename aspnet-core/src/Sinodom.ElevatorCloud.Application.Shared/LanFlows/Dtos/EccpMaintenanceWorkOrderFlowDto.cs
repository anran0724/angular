using System;
using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class EccpMaintenanceWorkOrderFlowDto
    { 
        /// <summary>
        /// 工单id
        /// </summary>    
        public int WorkOrderId { get; set; }
        /// <summary>
        /// 电梯注册代码
        /// </summary>
        public string CertificateNum { get; set; }
        /// <summary>
        /// 电梯安装地址
        /// </summary>
        public string InstallationAddress { get; set; }        
        /// <summary>
        /// 维保类型
        /// </summary>
        public string MaintenanceTypeName { get; set; }
        /// <summary>
        /// 维保完成时间
        /// </summary>
        public DateTime? MaintenanceCompletionTime { get; set; } 
        /// <summary>
        /// 维保人员
        /// </summary>
        public string MaintenanceUserName { get; set; }

        public  List<FlowStatusAction> LanFlowStates { get; set; }


    }

    public class FlowStatusAction
    {       
        public int FlowStatusActionsId { get; set; }
        public string ActionName { get; set; }
        public string ActionCode { get; set; }
        public string TableName { get; set; }
        public string SchemeType { get; set; }
    
    }

}