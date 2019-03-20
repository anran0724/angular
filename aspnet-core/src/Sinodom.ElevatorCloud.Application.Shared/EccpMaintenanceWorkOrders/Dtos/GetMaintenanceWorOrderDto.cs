using System;
namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
   public class GetMaintenanceWorOrderDto
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
    }
}
