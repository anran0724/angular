using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.Common.Dto
{
    public class GetSyncInput
    {
        public List<SyncDeptInput> SyncDeptInputs { get; set; }

        public List<SyncAreaInput> SyncAreaInputs { get; set; }
    }
}


