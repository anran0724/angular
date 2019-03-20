namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies;
    using Sinodom.ElevatorCloud.EntityFrameworkCore;

    public class TestEccpBaseCompaniesBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        private readonly int _tenantId;
        private readonly int _maintenanceTenantId;
        private readonly int _propertyTenantId;

        public TestEccpBaseCompaniesBuilder(ElevatorCloudDbContext context, int tenantId, int maintenanceTenantId, int propertyTenantId)
        {
            _context = context;
            this._tenantId = tenantId;
            this._maintenanceTenantId = maintenanceTenantId;
            this._propertyTenantId = propertyTenantId;
        }

        public void Create()
        {
            this.CreateMaintenanceCompanies();
            this.CreatePropertyCompanies();
            this.CreateECCPBaseAnnualInspectionUnits();
            this.CreateECCPBaseRegisterCompanies();
            this.CreateECCPBaseProductionCompanies();
        }

        private void CreateMaintenanceCompanies()
        {
            _context.ECCPBaseMaintenanceCompanies.Add(
                new ECCPBaseMaintenanceCompany
                {
                    Name = "宿主测试维保",
                    Addresse = "Addresse",
                    Longitude = "Longitude",
                    Latitude = "Latitude",
                    Telephone = "Telephone",
                    OrgOrganizationalCode = "111"
                });

            _context.SaveChanges();
        }

        private void CreatePropertyCompanies()
        {
            _context.ECCPBasePropertyCompanies.Add(
                new ECCPBasePropertyCompany
                {
                    Name = "宿主测试使用单位",
                    Addresse = "Addresse",
                    Longitude = "Longitude",
                    Latitude = "Latitude",
                    Telephone = "Telephone",
                    OrgOrganizationalCode = "888"
                });

            _context.SaveChanges();
        }
        //默认创建年检单位
        private void CreateECCPBaseAnnualInspectionUnits()
        {
            _context.ECCPBaseAnnualInspectionUnits.Add(
                    new ECCPBaseAnnualInspectionUnit { Name = "测试年检单位", Addresse = "Addresse", Telephone = "Telephone" });
            _context.SaveChanges();
        }


        //默认创建注册单位
        private void CreateECCPBaseRegisterCompanies()
        {
            _context.ECCPBaseRegisterCompanies.Add(
                     new ECCPBaseRegisterCompany { Name = "测试注册单位", Addresse = "Addresse", Telephone = "Telephone" });
            _context.SaveChanges();
        }

        //默认创建生产企业
        private void CreateECCPBaseProductionCompanies()
        {
            _context.ECCPBaseProductionCompanies.Add(
                 new ECCPBaseProductionCompany { Name = "测试生产企业", Addresse = "Addresse", Telephone = "Telephone" });
            _context.SaveChanges();
        }

    }
}