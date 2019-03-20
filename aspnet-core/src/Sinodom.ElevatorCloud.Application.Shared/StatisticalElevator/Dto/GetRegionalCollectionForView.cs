using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.StatisticalElevator.Dto
{
    public class GetRegionalCollectionForView
    {
        public int Type { get; set; }

        //中心点坐标
        public string CenterPoint { get; set; }

        public List<RegionalDto> RegionalCollection { get; set; }
    }
}
