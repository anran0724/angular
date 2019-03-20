using Abp.Application.Services.Dto;
using System;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    public class GetAllLanFlowStatusActionsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

        public int? MaxStatusValueFilter { get; set; }
        public int? MinStatusValueFilter { get; set; }

        public string StatusNameFilter { get; set; }

		public string ActionNameFilter { get; set; }

		public string ActionCodeFilter { get; set; }

		public string UserRoleCodeFilter { get; set; }

		public string ArgumentValueFilter { get; set; }

		public int IsStartProcessFilter { get; set; }

		public int IsEndProcessFilter { get; set; }

		public int IsAdoptFilter { get; set; }

        public int? MaxArgumentValueFilter { get; set; }
        public int? MinArgumentValueFilter { get; set; }


        public string LanFlowSchemeSchemeNameFilter { get; set; }

		 
    }
}