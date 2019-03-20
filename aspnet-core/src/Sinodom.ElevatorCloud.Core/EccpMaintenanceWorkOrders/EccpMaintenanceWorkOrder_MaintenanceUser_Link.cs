
using Sinodom.ElevatorCloud.Authorization.Users;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders
{
	[Table("EccpMaintenanceWorkOrder_MaintenanceUser_Links")]
    public class EccpMaintenanceWorkOrder_MaintenanceUser_Link : FullAuditedEntity<long> , IMustHaveTenant
    {
		public int TenantId { get; set; }



		public virtual long UserId { get; set; }
		
		public virtual int MaintenancePlanId { get; set; }
		
    }
}