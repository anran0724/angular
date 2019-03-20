using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}