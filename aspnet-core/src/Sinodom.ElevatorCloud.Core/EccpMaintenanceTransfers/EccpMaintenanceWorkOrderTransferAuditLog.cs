namespace Sinodom.ElevatorCloud.EccpMaintenanceTransfers
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities.Auditing;

    [Table("EccpMaintenanceWorkOrderTransferAuditLogs")]
    public class EccpMaintenanceWorkOrderTransferAuditLog : FullAuditedEntity<int>
    {
        public virtual int MaintenanceWorkOrderTransferId { get; set; }

        public EccpMaintenanceWorkOrderTransfer MaintenanceWorkOrderTransfer { get; set; }

        public virtual bool IsApproved { get; set; }

        public virtual string Remark { get; set; }
    }
}