using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.StatisticalElevator.Dto
{
    public class RegionalPersonDto
    {
        //区域名称
        public string AreaName { get; set; }

        //坐标点
        public string Point { get; set; }

        //区域id
        public int? AreaId { get; set; }

        //人员总数量
        public int PersonNumber { get; set; }

        public long? CommunityId { get; set; }
    }
}
