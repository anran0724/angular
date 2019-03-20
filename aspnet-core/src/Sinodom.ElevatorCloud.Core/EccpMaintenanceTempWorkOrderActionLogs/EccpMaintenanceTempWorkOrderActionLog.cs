
using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;
using Sinodom.ElevatorCloud.Authorization.Users;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs
{
	[Table("EccpMaintenanceTempWorkOrderActionLogs")]
    public class EccpMaintenanceTempWorkOrderActionLog : FullAuditedEntity<Guid> , IMustHaveTenant
    {
		public int TenantId { get; set; }


		[StringLength(EccpMaintenanceTempWorkOrderActionLogConsts.MaxRemarksLength, MinimumLength = EccpMaintenanceTempWorkOrderActionLogConsts.MinRemarksLength)]
		public virtual string Remarks { get; set; }
		
		public virtual int CheckState { get; set; }
		

		public virtual Guid TempWorkOrderId { get; set; }
		public EccpMaintenanceTempWorkOrder TempWorkOrder { get; set; }
		
		public virtual long UserId { get; set; }
		public User User { get; set; }
		
    }
}