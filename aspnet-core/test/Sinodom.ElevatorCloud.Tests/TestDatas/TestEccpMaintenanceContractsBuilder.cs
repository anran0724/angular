namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    using System;
    using System.Linq;

    using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
    using Sinodom.ElevatorCloud.EntityFrameworkCore;

    public class TestEccpMaintenanceContractsBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        private readonly int _maintenanceTenantId;
        private readonly int _propertyTenantId;

        public TestEccpMaintenanceContractsBuilder(ElevatorCloudDbContext context, int maintenanceTenantId, int propertyTenantId)
        {
            this._context = context;
            this._maintenanceTenantId = maintenanceTenantId;
            this._propertyTenantId = propertyTenantId;
        }

        public void Create()
        {
            this.CreateMaintenanceContract(this._maintenanceTenantId, this._propertyTenantId, "a1111111111");           
        }

        private void CreateMaintenanceContract(int maintenanceTenantId, int propertyTenantId, string certificateNum)
        {
            var maintenanceCompanyId = this._context.ECCPBaseMaintenanceCompanies.FirstOrDefault(e => e.TenantId == maintenanceTenantId).Id;
            var propertyCompanyId = this._context.ECCPBasePropertyCompanies.FirstOrDefault(e => e.TenantId == propertyTenantId).Id;
            var elevatorId = this.GetElevatorId(certificateNum);

            var eccpMaintenanceContract = new EccpMaintenanceContract()
                                              {
                                                  StartDate = DateTime.Now,
                                                  EndDate = DateTime.Now.AddDays(91),
                                                  MaintenanceCompanyId = maintenanceCompanyId,
                                                  PropertyCompanyId = propertyCompanyId,
                                                  ContractPictureId = Guid.NewGuid(),
                                                  TenantId = 1
                                              };
            this._context.EccpMaintenanceContracts.Add(eccpMaintenanceContract);

            this._context.EccpMaintenanceContract_Elevator_Links.Add(
                new EccpMaintenanceContract_Elevator_Link()
                    {
                        MaintenanceContract = eccpMaintenanceContract,
                        ElevatorId = elevatorId
                    });
            this._context.SaveChanges();
        }

        private Guid GetElevatorId(string certificateNum)
        {
            return this._context.EccpBaseElevators.FirstOrDefault(e => e.CertificateNum == certificateNum).Id;
        }
    }
}