namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;

    [Table("EccpMaintenanceWorkFlow_Refuse_Links")]
    public class EccpMaintenanceWorkFlow_Refuse_Link : FullAuditedEntity<int>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual Guid MaintenanceWorkFlowId { get; set; }
        public EccpMaintenanceWorkFlow MaintenanceWorkFlow { get; set; }

        public virtual Guid? RefusePictureId { get; set; }
    }
}