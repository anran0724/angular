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
        /// ά����ͬ����
        /// </summary>
        public int MaintenanceContractCount { get; set; }
        /// <summary>
        /// ά����������
        /// </summary>
        public int ElevatorCount { get; set; }
        /// <summary>
        /// Ա������
        /// </summary>
        public int UserCount { get; set; }
        /// <summary>
        /// ��7����ɵ�ά����������
        /// </summary>
        public int BeforeMaintenanceWorkOrderCount { get; set; }
        /// <summary>
        /// δ��7���ά����������
        /// </summary>
        public int AfterMaintenanceWorkOrderCount { get; set; }
        /// <summary>
        /// ���������ά��������
        /// </summary>
        public int CompleteMaintenanceElevatorCount { get; set; }
        /// <summary>
        /// ����δ���ά��������
        /// </summary>
        public int UnfinishedMaintenanceElevatorCount { get; set; }

      /// <summary>
      /// ��ʱ�����б�
      /// </summary>
        public List<GetEccpMaintenanceTempWorkOrderForAppView> TempWorkOrderList { get; set; }
        
        /// <summary>
        /// ά���ƻ��б�
        /// </summary>
        public List<GetStatisticalEccpMaintenancePlanForView> MaintenancePlanList { get; set; }
        /// <summary>
        /// δ��7���ά������
        /// </summary>
        public List<GetStatisticalEccpBaseElevatorForView> AfterMaintenancElevatorList { get; set; }

        /// <summary>
        /// ����ά������
        /// </summary>
        public List<GetStatisticalEccpBaseElevatorForView> TodayMaintenancElevatorList { get; set; }

        /// <summary>
        /// ���ں�ͬ
        /// </summary>
        public List<GetEccpMaintenanceContractForView> MaintenanceContractList { get; set; }

        /// <summary>
        /// ���ݱ���
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
        /// ��������
        /// </summary>
          public string InstallationAddress { get; set; }
        /// <summary>
        /// ����20λ
        /// </summary>
         public string CustomNum { get; set; }
         /// <summary>
         /// ��������
         /// </summary>
        public string FaultType { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string CreateTime { get; set; }
    }
}
