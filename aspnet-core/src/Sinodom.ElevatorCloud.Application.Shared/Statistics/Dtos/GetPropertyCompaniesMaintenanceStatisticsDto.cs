using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetPropertyCompaniesMaintenanceStatisticsDto
    {
        public int ElevatorNum { get; set; }
        public int MaintenanceNum { get; set; }
        public int OverdueMaintenanceNum { get; set; }
        public int ElevatorFaultNum { get; set; }
        public int NumberOfTemporaryMaintenance { get; set; }
    }
}
