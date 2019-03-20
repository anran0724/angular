using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetAppMaintenanceStatisticsDto
    {
        public string EditionsType { get; set; }
        public string UserRole { get; set; }
        public List<AppMaintenanceStatisticsDataDto> AppMaintenanceStatisticsData { get; set; }
    }
}
