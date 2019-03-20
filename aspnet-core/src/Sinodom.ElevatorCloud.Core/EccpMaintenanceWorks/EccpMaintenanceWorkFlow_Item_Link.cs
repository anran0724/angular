namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;

    using Sinodom.ElevatorCloud.EccpDict;

    public class EccpMaintenanceWorkFlow_Item_Link : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [StringLength(EccpMaintenanceWorkFlow_Item_LinkConsts.MaxRemarkLength, MinimumLength = EccpMaintenanceWorkFlowConsts.MinRemarkLength)]
        public virtual string Remark { get; set; }

        public virtual Guid MaintenanceWorkFlowId { get; set; }
        public EccpMaintenanceWorkFlow MaintenanceWorkFlow { get; set; }

        public virtual int DictMaintenanceItemId { get; set; }
        public EccpDictMaintenanceItem DictMaintenanceItem { get; set; }

        public virtual bool? IsQualified { get; set; }
    }
}