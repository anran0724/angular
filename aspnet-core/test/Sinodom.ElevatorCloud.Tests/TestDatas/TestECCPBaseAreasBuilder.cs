using Sinodom.ElevatorCloud.ECCPBaseAreas;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public class TestECCPBaseAreasBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        public TestECCPBaseAreasBuilder(ElevatorCloudDbContext context)
        {
            this._context = context;
        }

        public void Create()
        {
            this.CreateAreas("海南省", 0);
            this.CreateAreas("海口市", 1);
        }

        private void CreateAreas(string name, int level)
        {
            this._context.ECCPBaseAreas.Add(
            new ECCPBaseArea
            {
                ParentId = 0,
                Code = name,
                Name = name,
                Level = level,
                Path = "Path",
            });

            this._context.SaveChanges();
        }

    }
}
