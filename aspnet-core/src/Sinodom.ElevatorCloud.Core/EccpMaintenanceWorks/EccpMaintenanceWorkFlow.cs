
using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes;
using Sinodom.ElevatorCloud.EccpMaintenanceWorks;
using Sinodom.ElevatorCloud.EccpDict;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks
{
	[Table("EccpMaintenanceWorkFlows")]
    public class EccpMaintenanceWorkFlow : FullAuditedEntity<Guid> , IMustHaveTenant
    {
		public int TenantId { get; set; }


		[StringLength(EccpMaintenanceWorkFlowConsts.MaxActionCodeValueLength, MinimumLength = EccpMaintenanceWorkFlowConsts.MinActionCodeValueLength)]
		public virtual string ActionCodeValue { get; set; }
		
		[StringLength(EccpMaintenanceWorkFlowConsts.MaxRemarkLength, MinimumLength = EccpMaintenanceWorkFlowConsts.MinRemarkLength)]
		public virtual string Remark { get; set; }
		

		public virtual int MaintenanceTemplateNodeId { get; set; }
		public EccpMaintenanceTemplateNode MaintenanceTemplateNode { get; set; }
		
		public virtual int MaintenanceWorkId { get; set; }
		public EccpMaintenanceWork MaintenanceWork { get; set; }
		
		public virtual int DictMaintenanceWorkFlowStatusId { get; set; }
		public EccpDictMaintenanceWorkFlowStatus DictMaintenanceWorkFlowStatus { get; set; }

        public virtual double? Longitude { get; set; }

        public virtual double? Latitude { get; set; }

       
    }
}