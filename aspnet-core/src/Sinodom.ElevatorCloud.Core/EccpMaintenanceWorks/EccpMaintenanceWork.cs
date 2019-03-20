
using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks
{
	[Table("EccpMaintenanceWorks")]
    public class EccpMaintenanceWork : FullAuditedEntity , IMustHaveTenant
    {
		public int TenantId { get; set; }


		[Required]
		[StringLength(EccpMaintenanceWorkConsts.MaxTaskNameLength, MinimumLength = EccpMaintenanceWorkConsts.MinTaskNameLength)]
		public virtual string TaskName { get; set; }
		
		[StringLength(EccpMaintenanceWorkConsts.MaxRemarkLength, MinimumLength = EccpMaintenanceWorkConsts.MinRemarkLength)]
		public virtual string Remark { get; set; }

        public virtual int EccpMaintenanceTemplateId { get; set; }

        public virtual int MaintenanceWorkOrderId { get; set; }
		public EccpMaintenanceWorkOrder MaintenanceWorkOrder { get; set; }

        public virtual double? Longitude { get; set; }

        public virtual double? Latitude { get; set; }

    }
}