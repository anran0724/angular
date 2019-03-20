using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetAppIndexMapMaintenanceStatisticsDto
    {
        public string EditionsType { get; set; }
        public string UserRole { get; set; }
        public AppIndexMapMaintenanceStatisticsDataDto AppIndexMapMaintenanceStatisticsData { get; set; }
    }
}
