
using Sinodom.ElevatorCloud.EccpMaintenancePlans;
using Sinodom.ElevatorCloud.EccpDict;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders
{
	[Table("EccpMaintenanceWorkOrders")]
    public class EccpMaintenanceWorkOrder : FullAuditedEntity , IMustHaveTenant
    {
		public int TenantId { get; set; }


		public virtual bool IsPassed { get; set; }
		
		[StringLength(EccpMaintenanceWorkOrderConsts.MaxRemarkLength, MinimumLength = EccpMaintenanceWorkOrderConsts.MinRemarkLength)]
		public virtual string Remark { get; set; }
		
		public virtual double? Longitude { get; set; }
		
		public virtual double? Latitude { get; set; }
		
		public virtual DateTime PlanCheckDate { get; set; }
		
        public virtual bool IsClosed { get; set; }

        public virtual bool IsComplete { get; set; }

        public virtual DateTime? ComplateDate { get; set; }

        public virtual int MaintenancePlanId { get; set; }
		public EccpMaintenancePlan MaintenancePlan { get; set; }
		
		public virtual int MaintenanceTypeId { get; set; }
		public EccpDictMaintenanceType MaintenanceType { get; set; }
		
		public virtual int MaintenanceStatusId { get; set; }
		public EccpDictMaintenanceStatus MaintenanceStatus { get; set; }
		
    }
}