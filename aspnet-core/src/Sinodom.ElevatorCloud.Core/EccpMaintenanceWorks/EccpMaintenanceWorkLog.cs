

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks
{
	[Table("EccpMaintenanceWorkLogs")]
    public class EccpMaintenanceWorkLog : FullAuditedEntity<long> , IMustHaveTenant
    {
		public int TenantId { get; set; }


		[StringLength(EccpMaintenanceWorkLogConsts.MaxMaintenanceItemsNameLength, MinimumLength = EccpMaintenanceWorkLogConsts.MinMaintenanceItemsNameLength)]
		public virtual string MaintenanceItemsName { get; set; }
		
		[StringLength(EccpMaintenanceWorkLogConsts.MaxRemarkLength, MinimumLength = EccpMaintenanceWorkLogConsts.MinRemarkLength)]
		public virtual string Remark { get; set; }
		
		public virtual Guid MaintenanceWorkFlowId { get; set; }
		
		[Required]
		[StringLength(EccpMaintenanceWorkLogConsts.MaxMaintenanceWorkFlowNameLength, MinimumLength = EccpMaintenanceWorkLogConsts.MinMaintenanceWorkFlowNameLength)]
		public virtual string MaintenanceWorkFlowName { get; set; }
		

    }
}