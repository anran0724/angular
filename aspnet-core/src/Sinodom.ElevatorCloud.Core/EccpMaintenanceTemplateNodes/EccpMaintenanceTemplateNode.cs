
using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
using Sinodom.ElevatorCloud.EccpDict;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes
{
	[Table("EccpMaintenanceTemplateNodes")]
    public class EccpMaintenanceTemplateNode : FullAuditedEntity , IMayHaveTenant
    {
		public int? TenantId { get; set; }


		public virtual int? ParentNodeId { get; set; }
		
		[Required]
		[StringLength(EccpMaintenanceTemplateNodeConsts.MaxTemplateNodeNameLength, MinimumLength = EccpMaintenanceTemplateNodeConsts.MinTemplateNodeNameLength)]
		public virtual string TemplateNodeName { get; set; }
		
		[StringLength(EccpMaintenanceTemplateNodeConsts.MaxNodeDescLength, MinimumLength = EccpMaintenanceTemplateNodeConsts.MinNodeDescLength)]
		public virtual string NodeDesc { get; set; }
		
		public virtual int NodeIndex { get; set; }
		
		[StringLength(EccpMaintenanceTemplateNodeConsts.MaxActionCodeLength, MinimumLength = EccpMaintenanceTemplateNodeConsts.MinActionCodeLength)]
		public virtual string ActionCode { get; set; }

        public virtual int? NextNodeId { get; set; }

		public virtual int MaintenanceTemplateId { get; set; }
		public EccpMaintenanceTemplate MaintenanceTemplate { get; set; }
		
		public virtual int DictNodeTypeId { get; set; }
		public EccpDictNodeType DictNodeType { get; set; }

        public virtual bool MustDo { get; set; }

        public virtual int? SpareNodeId { get; set; }
    }
}