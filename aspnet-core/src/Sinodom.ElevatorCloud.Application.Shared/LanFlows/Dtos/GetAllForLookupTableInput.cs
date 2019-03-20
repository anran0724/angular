using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}