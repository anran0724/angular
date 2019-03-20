using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.LanFlows.Dtos;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.LanFlows
{
    public interface ILanFlowSchemesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLanFlowSchemeForView>> GetAll(GetAllLanFlowSchemesInput input);

		Task<GetLanFlowSchemeForEditOutput> GetLanFlowSchemeForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditLanFlowSchemeDto input);

		Task Delete(EntityDto input);

		
    }
}