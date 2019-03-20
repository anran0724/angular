
using Sinodom.ElevatorCloud.LanFlows;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.LanFlows
{
	[Table("LanFlowInstanceOperationHistories")]
    public class LanFlowInstanceOperationHistory : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual int StatusValue { get; set; }
		
		[StringLength(LanFlowInstanceOperationHistoryConsts.MaxStatusNameLength, MinimumLength = LanFlowInstanceOperationHistoryConsts.MinStatusNameLength)]
		public virtual string StatusName { get; set; }
		
		[StringLength(LanFlowInstanceOperationHistoryConsts.MaxActionDescLength, MinimumLength = LanFlowInstanceOperationHistoryConsts.MinActionDescLength)]
		public virtual string ActionDesc { get; set; }
		
		[Required]
		[StringLength(LanFlowInstanceOperationHistoryConsts.MaxActionCodeLength, MinimumLength = LanFlowInstanceOperationHistoryConsts.MinActionCodeLength)]
		public virtual string ActionCode { get; set; }
		
		[Required]
		public virtual string ActionValue { get; set; }

        public string ObjectId { get; set; }

        public virtual int FlowStatusActionId { get; set; }

        public LanFlowStatusAction FlowStatusAction { get; set; }

        public string Field { get; set; }

        public string TaskDescription { get; set; }
    }
}