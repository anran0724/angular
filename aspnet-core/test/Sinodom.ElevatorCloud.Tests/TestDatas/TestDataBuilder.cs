using Sinodom.ElevatorCloud.EntityFrameworkCore;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly ElevatorCloudDbContext _context;
        private readonly int _tenantId;
        private readonly int _maintenanceTenantId;
        private readonly int _propertyTenantId;

        public TestDataBuilder(ElevatorCloudDbContext context, int tenantId, int maintenanceTenantId, int propertyTenantId)
        {
            _context = context;
            _tenantId = tenantId;
            this._maintenanceTenantId = maintenanceTenantId;
            this._propertyTenantId = propertyTenantId;
        }

        public void Create()
        {
            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();
            new TestSubscriptionPaymentBuilder(_context, _tenantId).Create();
            new TestEditionsBuilder(_context).Create();

            new TestEccpBaseCompaniesBuilder(this._context, this._tenantId, this._maintenanceTenantId, this._propertyTenantId).Create();
            new TestEccpUsersBuilder(this._context, this._tenantId, this._maintenanceTenantId, this._propertyTenantId).Create();
            new TestEccpElevatorsBuilder(this._context, this._maintenanceTenantId, this._propertyTenantId).Create();
            new TestEccpMaintenanceContractsBuilder(this._context, this._maintenanceTenantId, this._propertyTenantId).Create();
            new TestEccpMaintenanceTemplatesBuilder(this._context, this._maintenanceTenantId).Create();
            new TestEccpMaintenancePlansBuilder(this._context, this._maintenanceTenantId).Create();
            new TestEccpMaintenanceWorkOrdersBuilder(this._context, this._maintenanceTenantId).Create();
            new TestEccpMaintenanceTempWorkOrdersBuilder(this._context, this._maintenanceTenantId).Create();
            new TestEccpMaintenanceWorkOrderEvaluationBuilder(this._context).Create();

            new TestECCPBaseAnnualInspectionUnitsBuilder(this._context).Create();
            new TestECCPBaseAreasBuilder(this._context).Create();
            new TestECCPBaseCommunitiesBuilder(this._context).Create();
            new TestECCPBaseProductionCompaniesBuilder(this._context).Create();
            new TestEccpBaseElevatorBrandsBuilder(this._context).Create();
            new TestEccpBaseElevatorModelsBuilder(this._context).Create();
            new TestECCPBaseMaintenanceCompaniesBuilder(this._context).Create();
            new TestEccpCompanyUserExtensionsBuilder(this._context).Create();
            new TestEccpElevatorQrCodesBuilder(this._context).Create();
            new TestEccpMaintenanceTemplateNodesBuilder(this._context).Create();
            new TestEccpMaintenanceWorkLogsBuilder(this._context).Create();

            new TestEccpBaseElevatorsBuilde(this._context, this._tenantId).Create();
            new TestEccpMaintenanceBuilder(this._context, this._tenantId).Create();
            new TestMaintenanceWorkOrderTransfersBuilder(this._context, this._maintenanceTenantId).Create();
            new TestMaintenanceTempWorkOrderTransfersBuilder(this._context, this._maintenanceTenantId).Create();
            

            new TestEccpBaseElevatorLabelsBuilder(this._context, this._tenantId).Create();

            _context.SaveChanges();
        }
    }
}
