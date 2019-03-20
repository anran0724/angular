namespace Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos;

    public interface IEccpCompanyAuditsAppService : IApplicationService
    {
        Task<PagedResultDto<GetEccpCompanyAuditForView>> GetAll(GetAllEccpCompanyAuditInput input);

        Task<GetEccpCompanyAuditForView> GetCompanyAuditForEdit(EntityDto input);

        Task EditCompanyAudit(EditCompanyAuditDto input);

        Task<PagedResultDto<GetEccpCompanyAuditLogForView>> GetCompanyAuditLogs(GetAllEccpCompanyAuditLogInput input);
    }
}