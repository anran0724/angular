namespace Sinodom.ElevatorCloud.EccpMaintenanceTransfers
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities.Auditing;

    [Table("EccpMaintenanceTempWorkOrderTransferAuditLogs")]
    public class EccpMaintenanceTempWorkOrderTransferAuditLog : FullAuditedEntity<int>
    {
        public virtual int MaintenanceTempWorkOrderTransferId { get; set; }

        public EccpMaintenanceTempWorkOrderTransfer MaintenanceTempWorkOrderTransfer { get; set; }

        public virtual bool IsApproved { get; set; }

        public virtual string Remark { get; set; }
    }
}