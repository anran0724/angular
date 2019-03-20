using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetEquipmentListDto
    {
        public int CountNum { get; set; }
        public List<GetEquipmentLiftInfoDto> EquipmentLiftInfos { get; set; }
    }
}
