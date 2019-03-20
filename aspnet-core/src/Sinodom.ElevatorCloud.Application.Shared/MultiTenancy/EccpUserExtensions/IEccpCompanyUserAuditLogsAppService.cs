using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions
{
    public interface IEccpCompanyUserAuditLogsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetEccpCompanyUserAuditLogForView>> GetAll(GetAllEccpCompanyUserAuditLogsInput input);

		Task<GetEccpCompanyUserAuditLogForEditOutput> GetEccpCompanyUserAuditLogForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditEccpCompanyUserAuditLogDto input);

		Task Delete(EntityDto input);

		
		Task<PagedResultDto<UserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}