
namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits;
    using Sinodom.ElevatorCloud.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TestECCPBaseAnnualInspectionUnitsBuilder
    {

        private readonly ElevatorCloudDbContext _context;


        public TestECCPBaseAnnualInspectionUnitsBuilder(ElevatorCloudDbContext context)
        {
            this._context = context;
        }

        public void Create()
        {
            this.AnnualInspectionUnit("测试单位1", "测试地址1", "测试电话1");
        }


        private void AnnualInspectionUnit(string name, string addresse, string telephone)
        {
            this._context.ECCPBaseAnnualInspectionUnits.Add(
                 new ECCPBaseAnnualInspectionUnit
                 {
                     Name = name,
                     Addresse = addresse,
                     Telephone = telephone
                 });

            this._context.SaveChanges();
        }

    }
}
