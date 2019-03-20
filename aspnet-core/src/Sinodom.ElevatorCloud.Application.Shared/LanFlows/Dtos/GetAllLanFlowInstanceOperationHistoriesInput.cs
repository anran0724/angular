using Abp.Application.Services.Dto;
using System;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    public class GetAllLanFlowInstanceOperationHistoriesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

        public int? MaxStatusValueFilter { get; set; }
        public int? MinStatusValueFilter { get; set; }

        public string StatusNameFilter { get; set; }

		public string ActionCodeFilter { get; set; }

        public string ActionValueFilter { get; set; }

		 
    }
}