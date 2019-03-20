using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.EccpBaseSaicUnits.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
        public int ParentId { get; set; }
    }
}