namespace Sinodom.ElevatorCloud.EccpMaintenancePlans
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;

    [Table("EccpMaintenancePlanCustomRules")]
    public class EccpMaintenancePlanCustomRule : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }


        public virtual int QuarterPollingPeriod { get; set; }
        public virtual int HalfYearPollingPeriod { get; set; }
        public virtual int YearPollingPeriod { get; set; }
        public virtual Guid PlanGroupGuid { get; set; }
    }
}