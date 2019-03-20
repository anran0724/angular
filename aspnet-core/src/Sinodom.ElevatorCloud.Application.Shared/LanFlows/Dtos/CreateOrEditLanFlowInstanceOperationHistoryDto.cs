
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    public class CreateOrEditLanFlowInstanceOperationHistoryDto : FullAuditedEntityDto<int?>
    {

        [Required]
        public string StatusValue { get; set; }


        [StringLength(LanFlowInstanceOperationHistoryConsts.MaxStatusNameLength, MinimumLength = LanFlowInstanceOperationHistoryConsts.MinStatusNameLength)]
		public string StatusName { get; set; }
		
		
		[StringLength(LanFlowInstanceOperationHistoryConsts.MaxActionDescLength, MinimumLength = LanFlowInstanceOperationHistoryConsts.MinActionDescLength)]
		public string ActionDesc { get; set; }
		
		
		[Required]
		[StringLength(LanFlowInstanceOperationHistoryConsts.MaxActionCodeLength, MinimumLength = LanFlowInstanceOperationHistoryConsts.MinActionCodeLength)]
		public string ActionCode { get; set; }
		
		
		[Required]
		public string ActionValue { get; set; }
		 
		 
    }
}