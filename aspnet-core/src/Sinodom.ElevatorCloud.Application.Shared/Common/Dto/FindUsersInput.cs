using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}
