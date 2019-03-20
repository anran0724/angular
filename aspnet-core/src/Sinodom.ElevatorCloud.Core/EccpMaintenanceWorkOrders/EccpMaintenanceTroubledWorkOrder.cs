
using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders
{
	[Table("EccpMaintenanceTroubledWorkOrders")]
    public class EccpMaintenanceTroubledWorkOrder : FullAuditedEntity , IMustHaveTenant
    {
		public int TenantId { get; set; }


		[StringLength(EccpMaintenanceTroubledWorkOrderConsts.MaxWorkOrderStatusNameLength, MinimumLength = EccpMaintenanceTroubledWorkOrderConsts.MinWorkOrderStatusNameLength)]
		public virtual string WorkOrderStatusName { get; set; }
		
		[StringLength(EccpMaintenanceTroubledWorkOrderConsts.MaxTroubledDescLength, MinimumLength = EccpMaintenanceTroubledWorkOrderConsts.MinTroubledDescLength)]
		public virtual string TroubledDesc { get; set; }
		
		public virtual int? IsAudit { get; set; }

        [StringLength(EccpMaintenanceTroubledWorkOrderConsts.MaxRemarksLength, MinimumLength = EccpMaintenanceTroubledWorkOrderConsts.MinRemarksLength)]
        public virtual string Remarks { get; set; }

		public virtual int MaintenanceWorkOrderId { get; set; }
		public EccpMaintenanceWorkOrder MaintenanceWorkOrder { get; set; }
		
    }
}