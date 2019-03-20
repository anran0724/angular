namespace Sinodom.ElevatorCloud.EccpMaintenanceTransfers
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;

    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;

    [Table("EccpMaintenanceWorkOrderTransfers")]
    public class EccpMaintenanceWorkOrderTransfer : FullAuditedEntity<int>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual int MaintenanceWorkOrderId { get; set; }

        public EccpMaintenanceWorkOrder MaintenanceWorkOrder { get; set; }

        public virtual bool? IsApproved { get; set; }

        public virtual string Remark { get; set; }

        public virtual long TransferUserId { get; set; }
        public User TransferUser { get; set; }
    }
}