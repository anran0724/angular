
using Sinodom.ElevatorCloud.EccpBaseElevators;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans
{
	[Table("EccpMaintenancePlans")]
    public class EccpMaintenancePlan : FullAuditedEntity , IMustHaveTenant
    {
		public int TenantId { get; set; }


		public virtual int PollingPeriod { get; set; }
		
		public virtual int RemindHour { get; set; }
		

		public virtual Guid ElevatorId { get; set; }
		public EccpBaseElevator Elevator { get; set; }

        public virtual bool IsClose { get; set; }

        public virtual Guid? PlanGroupGuid { get; set; }

        public virtual bool IsCancel { get; set; }
    }
}