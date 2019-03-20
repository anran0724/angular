using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.StatisticalElevator.Dto
{
    public class GetRegionalPersonCollectionForView
    {
        public int Type { get; set; }

        public List<RegionalPersonDto> RegionalPersonCollection { get; set; }
    }
}
