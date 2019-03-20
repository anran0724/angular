using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.EccpDict;
using Sinodom.ElevatorCloud.EccpMaintenancePlans;
using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public  class TestEccpMaintenanceBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        private readonly int _tenantId;       

        public TestEccpMaintenanceBuilder(ElevatorCloudDbContext context, int tenantId)
        {
            _context = context;
            this._tenantId = tenantId;     
        }
        public void Create()
        {
            this.CreateEccpMaintenanceTemplates();
            this.CreateEccpMaintenancePlans();            
           
        }
        /// <summary>
        /// 创建模板默认数据
        /// </summary>
        private void CreateEccpMaintenanceTemplates()
        {
            EccpDictElevatorType    elevatorType = _context.EccpDictElevatorTypes.FirstOrDefault(e => e.Name == "载货电梯");
            EccpDictMaintenanceType maintenanceType = _context.EccpDictMaintenanceTypes.FirstOrDefault(e => e.Name == "半月维保");
            _context.EccpMaintenanceTemplates.Add(
                       new EccpMaintenanceTemplate
                       {
                           TempName = "测试模板",
                           TempDesc = "模板描述",
                           TempAllow = "通过",
                           TempDeny = "未通过",
                           TempCondition = "saveCount",
                           TempNodeCount = 0,
                           ElevatorTypeId = elevatorType.Id,
                           MaintenanceTypeId = maintenanceType.Id,
                           TenantId = _tenantId
                       });
        }
        /// <summary>
        /// 创建任务计划默认数据
        /// </summary>
        private void CreateEccpMaintenancePlans()
        {
            EccpBaseElevator   elevatorEntity = _context.EccpBaseElevators.FirstOrDefault(e => e.CertificateNum== "a1111111111");
            _context.EccpMaintenancePlans.Add(
                   new EccpMaintenancePlan
                   {
                       TenantId =  _tenantId,
                       PollingPeriod = 30,
                       RemindHour = 72,
                       PlanGroupGuid = Guid.NewGuid(),
                       ElevatorId = elevatorEntity.Id
                   });
        }
        /// <summary>
        /// 创建任务计划关联模板中间表
        /// </summary>
        private void CreateEccpMaintenancePlan_Template_Links()
        {         

            EccpMaintenanceTemplate  templateEntity = _context.EccpMaintenanceTemplates.FirstOrDefault(e => e.TempName == "测试模板");
            EccpMaintenancePlan   mainenancePlanEntity = _context.EccpMaintenancePlans.FirstOrDefault(e => e.PollingPeriod == 30);
            _context.EccpMaintenancePlan_Template_Links.Add(
                  new EccpMaintenancePlan_Template_Link
                  {
                      TenantId = _tenantId,
                      MaintenancePlanId = mainenancePlanEntity.Id,
                      MaintenanceTemplateId = templateEntity.Id
                  });
        }
        

    }
}
