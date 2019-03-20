
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.EccpMaintenancePlans;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans
{
	[Table("EccpMaintenancePlan_MaintenanceUser_Links")]
    public class EccpMaintenancePlan_MaintenanceUser_Link : FullAuditedEntity<long> , IMustHaveTenant
    {
		public int TenantId { get; set; }



		public virtual long UserId { get; set; }
		public User User { get; set; }
		
		public virtual int MaintenancePlanId { get; set; }
		public EccpMaintenancePlan MaintenancePlan { get; set; }
		
    }
}