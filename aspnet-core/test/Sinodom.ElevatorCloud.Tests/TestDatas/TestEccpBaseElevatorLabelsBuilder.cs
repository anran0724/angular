using Sinodom.ElevatorCloud.EccpBaseElevatorLabels;
using Sinodom.ElevatorCloud.EccpDict;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public class TestEccpBaseElevatorLabelsBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        private readonly int _tenantId;

        public TestEccpBaseElevatorLabelsBuilder(ElevatorCloudDbContext context, int tenantId)
        {
            this._context = context;
            this._tenantId = tenantId;
        }

        public void Create()
        {
            this.CreateElevatorLabels();
        }

        private void CreateElevatorLabels()
        {
            this._context.EccpBaseElevatorLabels.Add(
                         new EccpBaseElevatorLabel
                         {
                             TenantId = _tenantId,
                             LabelName = "制动器",
                             UniqueId = "432423432",
                             BinaryObjectsId = Guid.NewGuid(),                            
                             LabelStatus = new EccpDictLabelStatus
                             {
                                 Name = "未绑定"
                             }
                         });

            this._context.SaveChanges();
        }
    }
}
