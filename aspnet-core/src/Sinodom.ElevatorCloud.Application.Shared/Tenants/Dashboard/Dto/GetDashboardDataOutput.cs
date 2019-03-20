using Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos;
using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos;
using Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos;
using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos;
using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.Tenants.Dashboard.Dto
{
    public class GetDashboardDataOutput
    {
        /// <summary>
        /// 维保合同数量
        /// </summary>
        public int MaintenanceContractCount { get; set; }
        /// <summary>
        /// 维保电梯数量
        /// </summary>
        public int ElevatorCount { get; set; }
        /// <summary>
        /// 员工总数
        /// </summary>
        public int UserCount { get; set; }
        /// <summary>
        /// 进7天完成的维保工单数量
        /// </summary>
        public int BeforeMaintenanceWorkOrderCount { get; set; }
        /// <summary>
        /// 未来7天的维保工单数量
        /// </summary>
        public int AfterMaintenanceWorkOrderCount { get; set; }
        /// <summary>
        /// 本月已完成维保电梯数
        /// </summary>
        public int CompleteMaintenanceElevatorCount { get; set; }
        /// <summary>
        /// 本月未完成维保电梯数
        /// </summary>
        public int UnfinishedMaintenanceElevatorCount { get; set; }

      /// <summary>
      /// 临时工单列表
      /// </summary>
        public List<GetEccpMaintenanceTempWorkOrderForAppView> TempWorkOrderList { get; set; }
        
        /// <summary>
        /// 维保计划列表
        /// </summary>
        public List<GetStatisticalEccpMaintenancePlanForView> MaintenancePlanList { get; set; }
        /// <summary>
        /// 未来7天待维保电梯
        /// </summary>
        public List<GetStatisticalEccpBaseElevatorForView> AfterMaintenancElevatorList { get; set; }

        /// <summary>
        /// 今日维保电梯
        /// </summary>
        public List<GetStatisticalEccpBaseElevatorForView> TodayMaintenancElevatorList { get; set; }

        /// <summary>
        /// 临期合同
        /// </summary>
        public List<GetEccpMaintenanceContractForView> MaintenanceContractList { get; set; }

        /// <summary>
        /// 电梯报警
        /// </summary>
        public List<LiftFunction> LiftFunctionList { get; set; }
    }
    public class JsonMessage
    {
        public JsonMessage() { }
        public bool Success { get; set; }
         public  string   Code { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
        public List<LiftFunction> Data { get; set; }
    }


    public class LiftFunction
    {
        /// <summary>
        /// 电梯名称
        /// </summary>
          public string InstallationAddress { get; set; }
        /// <summary>
        /// 电梯20位
        /// </summary>
         public string CustomNum { get; set; }
         /// <summary>
         /// 报警类型
         /// </summary>
        public string FaultType { get; set; }
        /// <summary>
        /// 发生时间
        /// </summary>
        public string CreateTime { get; set; }
    }
}
