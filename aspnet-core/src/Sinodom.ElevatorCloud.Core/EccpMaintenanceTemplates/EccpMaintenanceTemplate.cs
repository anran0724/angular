
using Sinodom.ElevatorCloud.EccpDict;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplates
{
    [Table("EccpMaintenanceTemplates")]
    public class EccpMaintenanceTemplate : FullAuditedEntity , IMayHaveTenant
    {
		public int? TenantId { get; set; }


		[Required]
		[StringLength(EccpMaintenanceTemplateConsts.MaxTempNameLength, MinimumLength = EccpMaintenanceTemplateConsts.MinTempNameLength)]
		public virtual string TempName { get; set; }
		
		[StringLength(EccpMaintenanceTemplateConsts.MaxTempDescLength, MinimumLength = EccpMaintenanceTemplateConsts.MinTempDescLength)]
		public virtual string TempDesc { get; set; }
		
		[StringLength(EccpMaintenanceTemplateConsts.MaxTempAllowLength, MinimumLength = EccpMaintenanceTemplateConsts.MinTempAllowLength)]
		public virtual string TempAllow { get; set; }
		
		[StringLength(EccpMaintenanceTemplateConsts.MaxTempDenyLength, MinimumLength = EccpMaintenanceTemplateConsts.MinTempDenyLength)]
		public virtual string TempDeny { get; set; }
		
		[StringLength(EccpMaintenanceTemplateConsts.MaxTempConditionLength, MinimumLength = EccpMaintenanceTemplateConsts.MinTempConditionLength)]
		public virtual string TempCondition { get; set; }
		

		public virtual int TempNodeCount { get; set; }
		

		public virtual int MaintenanceTypeId { get; set; }
		public EccpDictMaintenanceType MaintenanceType { get; set; }
		
		public virtual int ElevatorTypeId { get; set; }
		public EccpDictElevatorType ElevatorType { get; set; }
		
    }
}