namespace Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos
{
    using Abp.Application.Services.Dto;

    public class GetAllEccpCompanyAuditLogInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int TenantId { get; set; }
    }
}