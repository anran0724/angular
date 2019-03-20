
using Sinodom.ElevatorCloud.LanFlows;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.LanFlows
{
	[Table("LanFlowStatusActions")]
    public class LanFlowStatusAction : FullAuditedEntity
    {
        public virtual int StatusValue { get; set; }
		
		[Required]
		[StringLength(LanFlowStatusActionConsts.MaxStatusNameLength, MinimumLength = LanFlowStatusActionConsts.MinStatusNameLength)]
		public virtual string StatusName { get; set; }
		
		[Required]
		[StringLength(LanFlowStatusActionConsts.MaxActionNameLength, MinimumLength = LanFlowStatusActionConsts.MinActionNameLength)]
		public virtual string ActionName { get; set; }
		
		[StringLength(LanFlowStatusActionConsts.MaxActionDescLength, MinimumLength = LanFlowStatusActionConsts.MinActionDescLength)]
		public virtual string ActionDesc { get; set; }
		
		[Required]
		[StringLength(LanFlowStatusActionConsts.MaxActionCodeLength, MinimumLength = LanFlowStatusActionConsts.MinActionCodeLength)]
		public virtual string ActionCode { get; set; }
		
		[StringLength(LanFlowStatusActionConsts.MaxUserRoleCodeLength, MinimumLength = LanFlowStatusActionConsts.MinUserRoleCodeLength)]
		public virtual string UserRoleCode { get; set; }
		
		[Required]
		public virtual int ArgumentValue { get; set; }
		
		public virtual bool IsStartProcess { get; set; }
		
		public virtual bool IsEndProcess { get; set; }
		
		public virtual bool IsAdopt { get; set; }
		
		[StringLength(LanFlowStatusActionConsts.MaxApiActionLength, MinimumLength = LanFlowStatusActionConsts.MinApiActionLength)]
		public virtual string ApiAction { get; set; }
		
		public virtual int SortCode { get; set; }
		

		public virtual int SchemeId { get; set; }
		public LanFlowScheme Scheme { get; set; }
		
    }
}