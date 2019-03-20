using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public class TestECCPBaseProductionCompaniesBuilder
    {
        private readonly ElevatorCloudDbContext _context;


        public TestECCPBaseProductionCompaniesBuilder(ElevatorCloudDbContext context)
        {
            this._context = context;
        }

        public void Create()
        {
            this.CreateProductionCompanies();
        }

        private void CreateProductionCompanies()
        {
            var areaEntity = this._context.ECCPBaseAreas.FirstOrDefault(e => e.Name == "海南省");

            this._context.ECCPBaseProductionCompanies.Add(
                        new ECCPBaseProductionCompany
                        {
                            Name = "测试制造单位1",
                            Addresse = "测试地址1",
                            Telephone = "测试电话1",
                            ProvinceId = areaEntity.Id
                        });
            this._context.SaveChanges();
        }
    }
}
