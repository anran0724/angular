
using Sinodom.ElevatorCloud.EccpMaintenancePlans;
using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
using Sinodom.ElevatorCloud.EccpDict;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans
{
	[Table("EccpMaintenancePlan_Template_Links")]
    public class EccpMaintenancePlan_Template_Link : FullAuditedEntity<long> , IMustHaveTenant
    {
		public int TenantId { get; set; }



		public virtual int MaintenancePlanId { get; set; }
		public EccpMaintenancePlan MaintenancePlan { get; set; }
		
		public virtual int MaintenanceTemplateId { get; set; }
		public EccpMaintenanceTemplate MaintenanceTemplate { get; set; }
		
    }
}