namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;

    using Sinodom.ElevatorCloud.EccpDict;

    [Table("EccpMaintenanceTemplateNode_DictMaintenanceItem_Links")]
    public class EccpMaintenanceTemplateNode_DictMaintenanceItem_Link : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public virtual int MaintenanceTemplateNodeId { get; set; }

        public EccpMaintenanceTemplateNode MaintenanceTemplateNode { get; set; }

        public virtual int DictMaintenanceItemId { get; set; }

        public EccpDictMaintenanceItem DictMaintenanceItem { get; set; }

        public virtual int Sort { get; set; }
    }
}