using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
   public class GetElevatorMaintenanceInfoDto
    {
        public Guid Id { get; set; }
        public int WorkOrderId { get; set; }
        public string CertificateNum { get; set; }
        public int MaintenanceStatusId { get; set; }
        public string MaintenanceStatusName { get; set; }
        public DateTime? MaintenanceCompletionTime { get; set; }
        public DateTime? MaintenanceDueTime { get; set; }
        public string MaintenanceUserName { get; set; }
        public int MaintenanceTypeId { get; set; }
        public string MaintenanceTypeName { get; set; }
        public string InstallationAddress { get; set; }
        public string ElevatorNum { get; set; }
        public int? SyncElevatorId { get; set; }
        public bool IsCamera { get; set; }
    }
}
