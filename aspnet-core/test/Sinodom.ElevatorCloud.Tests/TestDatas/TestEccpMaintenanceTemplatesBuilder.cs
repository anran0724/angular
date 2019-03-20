namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
    using Sinodom.ElevatorCloud.EntityFrameworkCore;

    public class TestEccpMaintenanceTemplatesBuilder
    {
        private readonly ElevatorCloudDbContext _context;
        private readonly int _maintenanceTenantId;

        public TestEccpMaintenanceTemplatesBuilder(ElevatorCloudDbContext context, int maintenanceTenantId)
        {
            this._context = context;
            this._maintenanceTenantId = maintenanceTenantId;
        }

        public void Create()
        {
            this._context.EccpMaintenanceTemplates.Add(
                new EccpMaintenanceTemplate
                    {
                        TempName = "单元测试月度维保模板",
                        TempDesc = "单元测试月度维保模板简介",
                        TempAllow = "合格",
                        TempDeny = "不合格",
                        TempCondition = "公式",
                        TempNodeCount = 0,
                        ElevatorTypeId = 12,
                        MaintenanceTypeId = 2
                    });          

            this._context.SaveChanges();
        }
    }
}