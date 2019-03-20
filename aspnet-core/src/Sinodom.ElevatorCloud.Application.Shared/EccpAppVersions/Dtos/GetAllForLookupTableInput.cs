using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.EccpAppVersions.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}