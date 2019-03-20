namespace Sinodom.ElevatorCloud.EccpMaintenanceTransfers
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;

    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;

    [Table("EccpMaintenanceTempWorkOrderTransfers")]
    public class EccpMaintenanceTempWorkOrderTransfer : FullAuditedEntity<int>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual Guid MaintenanceTempWorkOrderId { get; set; }

        public EccpMaintenanceTempWorkOrder MaintenanceTempWorkOrder { get; set; }

        public virtual bool? IsApproved { get; set; }

        public virtual string Remark { get; set; }

        public virtual long TransferUserId { get; set; }

        public User TransferUser { get; set; }
    }
}