using Sinodom.ElevatorCloud.EccpMaintenanceWorks;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public class TestEccpMaintenanceWorkLogsBuilder
    {
        private readonly ElevatorCloudDbContext _context;


        public TestEccpMaintenanceWorkLogsBuilder(ElevatorCloudDbContext context)
        {
            this._context = context;
        }

        public void Create()
        {
            this.CreateMaintenanceWorkLogs();
        }

        private void CreateMaintenanceWorkLogs()
        {
            this._context.EccpMaintenanceWorkLogs.Add(
                new EccpMaintenanceWorkLog
                    {
                        CreationTime = DateTime.Now,
                        IsDeleted = false,
                        MaintenanceItemsName = "测试ItemsName111",
                        Remark = "测试Remark111",
                        MaintenanceWorkFlowId = Guid.NewGuid(),
                        MaintenanceWorkFlowName = "测试WorkFlowName111",
                        TenantId = 1
                    });

            this._context.SaveChanges();
        }
    }
}
