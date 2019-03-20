using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetAdviceListDto
    {
        public int CountNum { get; set; }
        public List<GetLiftInfoDto> LiftInfos { get; set; }
    }
}
