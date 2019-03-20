namespace Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos
{
    using Abp.Application.Services.Dto;

    public class GetAllEccpCompanyAuditInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int CheckStateFilter { get; set; }

        public string CompanyNameFilter { get; set; }
    }
}