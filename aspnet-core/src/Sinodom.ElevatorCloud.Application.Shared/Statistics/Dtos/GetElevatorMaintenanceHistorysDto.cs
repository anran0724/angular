using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetElevatorMaintenanceHistorysDto
    {
        public int WorkOrderId { get; set; }
        public DateTime? ComplateDate { get; set; }
        public DateTime PlanCheckDate { get; set; }
        public string MaintenanceTypeName { get; set; }
        public string MaintenanceStatusName { get; set; }
        public string MaintenanceUserName { get; set; }
        public string InstallationAddress { get; set; }
        public string Remark { get; set; }
    }
}
