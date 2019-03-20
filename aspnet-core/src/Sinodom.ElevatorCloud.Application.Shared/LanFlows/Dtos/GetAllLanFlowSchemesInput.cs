using Abp.Application.Services.Dto;
using System;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    public class GetAllLanFlowSchemesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string SchemeTypeFilter { get; set; }

		public string TableNameFilter { get; set; }

		public int? MaxAuthorizeTypeFilter { get; set; }
		public int? MinAuthorizeTypeFilter { get; set; }



    }
}