
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    public class CreateOrEditLanFlowStatusActionDto : FullAuditedEntityDto<int?>
    {

        [Required]
        public int StatusValue { get; set; }


        [Required]
		[StringLength(LanFlowStatusActionConsts.MaxStatusNameLength, MinimumLength = LanFlowStatusActionConsts.MinStatusNameLength)]
		public string StatusName { get; set; }
		
		
		[Required]
		[StringLength(LanFlowStatusActionConsts.MaxActionNameLength, MinimumLength = LanFlowStatusActionConsts.MinActionNameLength)]
		public string ActionName { get; set; }
		
		
		[StringLength(LanFlowStatusActionConsts.MaxActionDescLength, MinimumLength = LanFlowStatusActionConsts.MinActionDescLength)]
		public string ActionDesc { get; set; }
		
		
		[Required]
		[StringLength(LanFlowStatusActionConsts.MaxActionCodeLength, MinimumLength = LanFlowStatusActionConsts.MinActionCodeLength)]
		public string ActionCode { get; set; }
		
		
		[StringLength(LanFlowStatusActionConsts.MaxUserRoleCodeLength, MinimumLength = LanFlowStatusActionConsts.MinUserRoleCodeLength)]
		public string UserRoleCode { get; set; }
		
		
		[Required]
		public int ArgumentValue { get; set; }
		
		
		public bool IsStartProcess { get; set; }
		
		
		public bool IsEndProcess { get; set; }
		
		
		public bool IsAdopt { get; set; }
		
		
		[StringLength(LanFlowStatusActionConsts.MaxApiActionLength, MinimumLength = LanFlowStatusActionConsts.MinApiActionLength)]
		public string ApiAction { get; set; }
		
		
		public int SortCode { get; set; }
		
		
		 public int SchemeId { get; set; }
		 
		 
    }
}