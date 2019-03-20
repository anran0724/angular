

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.LanFlows
{
	[Table("LanFlowSchemes")]
    public class LanFlowScheme : FullAuditedEntity
    {
        [Required]
		[StringLength(LanFlowSchemeConsts.MaxSchemeNameLength, MinimumLength = LanFlowSchemeConsts.MinSchemeNameLength)]
		public virtual string SchemeName { get; set; }
		
		[StringLength(LanFlowSchemeConsts.MaxSchemeTypeLength, MinimumLength = LanFlowSchemeConsts.MinSchemeTypeLength)]
		public virtual string SchemeType { get; set; }
		
		[Required]
		[StringLength(LanFlowSchemeConsts.MaxSchemeContentLength, MinimumLength = LanFlowSchemeConsts.MinSchemeContentLength)]
		public virtual string SchemeContent { get; set; }
		
		[Required]
		[StringLength(LanFlowSchemeConsts.MaxTableNameLength, MinimumLength = LanFlowSchemeConsts.MinTableNameLength)]
		public virtual string TableName { get; set; }
		
		public virtual int? AuthorizeType { get; set; }
		
		public virtual int SortCode { get; set; }
		

    }
}