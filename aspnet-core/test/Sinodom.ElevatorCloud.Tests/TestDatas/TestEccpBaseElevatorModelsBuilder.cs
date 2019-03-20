using Sinodom.ElevatorCloud.EccpBaseElevatorModels;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public class TestEccpBaseElevatorModelsBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        public TestEccpBaseElevatorModelsBuilder(ElevatorCloudDbContext context)
        {
            this._context = context;
        }

        public void Create()
        {
            this.CreateElevatorModels();
        }

        private void CreateElevatorModels()
        {
            var elevatorBrandsEntity = this._context.EccpBaseElevatorBrands.FirstOrDefault(e => e.Name == "电梯品牌1");

            this._context.EccpBaseElevatorModels.Add(
                        new EccpBaseElevatorModel
                        {
                            Name = "电梯型号1",
                            ElevatorBrand = elevatorBrandsEntity
                        });
            this._context.SaveChanges();
        }
    }
}
