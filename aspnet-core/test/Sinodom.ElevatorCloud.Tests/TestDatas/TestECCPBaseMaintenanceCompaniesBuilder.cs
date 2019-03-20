using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public class TestECCPBaseMaintenanceCompaniesBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        public TestECCPBaseMaintenanceCompaniesBuilder(ElevatorCloudDbContext context)
        {
            this._context = context;
        }

        public void Create()
        {
            this.CreateMaintenanceCompanies();
        }

        private void CreateMaintenanceCompanies()
        {
            this._context.ECCPBaseMaintenanceCompanies.Add(
                new ECCPBaseMaintenanceCompany
                {
                    Name = "测试维保单位1",
                    Addresse = "Addresse",
                    Longitude = "Longitude",
                    Latitude = "Latitude",
                    Telephone = "Telephone",
                    OrgOrganizationalCode = "111"
                });
            this._context.SaveChanges();
        }
    }
}
