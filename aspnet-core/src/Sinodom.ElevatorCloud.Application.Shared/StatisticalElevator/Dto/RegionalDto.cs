using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.StatisticalElevator.Dto
{
    public class RegionalDto
    {
        //区域名称
        public string AreaName { get; set; }

        //坐标点
        public string Point { get; set; }

        //区域id
        public int AreaId { get; set; }

        public long? CommunityId { get; set; }

        public string CommunityName {get;set;}

        //电梯总数量
        public int ElevatorNumber { get; set; }

        //完成维保数量
        public int CompleteMaintenanceNumber { get; set; }

        //待维保数量
        public int WaitMaintenanceNumber { get; set; }

        //临期数量
        public int AdvanceNumber { get; set; }

        //超期数量
        public int OverdueNumber { get; set; }

        //故障数量
        public int MalfunctionNumber { get; set; }

        //投诉数量
        public int ComplaintNumber { get; set; }

    }
}
