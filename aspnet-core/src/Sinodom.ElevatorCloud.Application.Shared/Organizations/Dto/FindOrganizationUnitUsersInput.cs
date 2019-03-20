using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.Organizations.Dto
{
    public class FindOrganizationUnitUsersInput : PagedAndFilteredInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}
