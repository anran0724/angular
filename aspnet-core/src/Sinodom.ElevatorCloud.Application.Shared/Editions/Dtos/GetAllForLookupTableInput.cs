using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.Editions.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}