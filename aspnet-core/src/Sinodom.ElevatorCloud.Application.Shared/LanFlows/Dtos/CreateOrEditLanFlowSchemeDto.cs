
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    public class CreateOrEditLanFlowSchemeDto : FullAuditedEntityDto<int?>
    {

		[Required]
		[StringLength(LanFlowSchemeConsts.MaxSchemeNameLength, MinimumLength = LanFlowSchemeConsts.MinSchemeNameLength)]
		public string SchemeName { get; set; }
		
		
		[StringLength(LanFlowSchemeConsts.MaxSchemeTypeLength, MinimumLength = LanFlowSchemeConsts.MinSchemeTypeLength)]
		public string SchemeType { get; set; }
		
		
		[Required]
		[StringLength(LanFlowSchemeConsts.MaxSchemeContentLength, MinimumLength = LanFlowSchemeConsts.MinSchemeContentLength)]
		public string SchemeContent { get; set; }
		
		
		[Required]
		[StringLength(LanFlowSchemeConsts.MaxTableNameLength, MinimumLength = LanFlowSchemeConsts.MinTableNameLength)]
		public string TableName { get; set; }
		
		
		public int? AuthorizeType { get; set; }
		
		
		public int SortCode { get; set; }
		
		

    }
}