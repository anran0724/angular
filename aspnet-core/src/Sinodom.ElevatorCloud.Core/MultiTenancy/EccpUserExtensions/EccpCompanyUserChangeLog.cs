namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;

    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.EccpBaseElevators;

    [Table("EccpCompanyUserChangeLogs")]
    public class EccpCompanyUserChangeLog : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(EccpCompanyUserChangeLogConsts.MaxFieldNameLength, MinimumLength = EccpCompanyUserChangeLogConsts.MinFieldNameLength)]
        public virtual string FieldName { get; set; }

        [Required]
        [StringLength(EccpCompanyUserChangeLogConsts.MaxOldValueLength, MinimumLength = EccpCompanyUserChangeLogConsts.MinOldValueLength)]
        public virtual string OldValue { get; set; }

        [Required]
        [StringLength(EccpCompanyUserChangeLogConsts.MaxNewValueLength, MinimumLength = EccpCompanyUserChangeLogConsts.MinNewValueLength)]
        public virtual string NewValue { get; set; }

        public virtual long UserId { get; set; }

        public User User { get; set; }
    }
}