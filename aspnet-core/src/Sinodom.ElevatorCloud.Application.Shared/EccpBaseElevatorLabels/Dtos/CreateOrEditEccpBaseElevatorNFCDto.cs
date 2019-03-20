using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos
{
    public class CreateOrEditEccpBaseElevatorNFCDto
    {
        public string ElevatorId { get; set; }

        public string LabelName { get; set; }

        public string UniqueId { get; set; }
    }
}
