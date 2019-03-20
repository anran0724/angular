using Sinodom.ElevatorCloud.EccpBaseElevatorBrands;
using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public class TestEccpBaseElevatorBrandsBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        public TestEccpBaseElevatorBrandsBuilder(ElevatorCloudDbContext context)
        {
            this._context = context;
        }

        public void Create()
        {
            this.CreateElevatorBrands();
        }

        private void CreateElevatorBrands()
        {
            var productionCompaniesEntity = this._context.ECCPBaseProductionCompanies.FirstOrDefault(e => e.Name == "测试制造单位1");

            this._context.EccpBaseElevatorBrands.Add(
                        new EccpBaseElevatorBrand
                        {
                            Name = "电梯品牌1",
                            ProductionCompany = productionCompaniesEntity
                        });
            this._context.SaveChanges();
        }
    }
}
