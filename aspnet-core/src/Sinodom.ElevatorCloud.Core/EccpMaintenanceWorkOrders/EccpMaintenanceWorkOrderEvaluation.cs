
using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders
{
	[Table("EccpMaintenanceWorkOrderEvaluations")]
    public class EccpMaintenanceWorkOrderEvaluation : FullAuditedEntity 
    {



		public virtual int Rank { get; set; }
		
		[StringLength(EccpMaintenanceWorkOrderEvaluationConsts.MaxRemarksLength, MinimumLength = EccpMaintenanceWorkOrderEvaluationConsts.MinRemarksLength)]
		public virtual string Remarks { get; set; }
		

		public virtual int WorkOrderId { get; set; }
		public EccpMaintenanceWorkOrder WorkOrder { get; set; }
		
    }
}