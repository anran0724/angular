
using System;
using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    public class LanFlowInstanceOperationHistoryDto : EntityDto
    {
		public string StatusValue { get; set; }

		public string StatusName { get; set; }

		public string ActionCode { get; set; }

		public string ActionValue { get; set; }
        
		 
    }
}