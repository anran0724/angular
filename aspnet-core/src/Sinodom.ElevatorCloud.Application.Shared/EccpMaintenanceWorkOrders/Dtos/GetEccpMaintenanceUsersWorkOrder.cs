using System;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
 
    /// <summary>
    /// The get eccp maintenance work order for view.
    /// </summary>
    public class GetEccpMaintenanceUsersWorkOrder
    {
        /// <summary>
        /// 电梯id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 工单状态
        /// </summary>
        public string EccpDictMaintenanceStatusName { get; set; }

        /// <summary>
        ///  维保类型
        /// </summary>
        public string EccpDictMaintenanceTypeName { get; set; }

        /// <summary>
        /// 电梯安装地址
        /// </summary>
        public string EccpElevatorInstallationAddress { get; set; }
      
        /// <summary>
        /// 工单创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 计划完成时间
        /// </summary>
        public DateTime PlanCheckDate { get; set; }

        /// <summary>
        /// 电梯注册代码
        /// </summary>
        public string CertificateNum { get; set; }

    }
}