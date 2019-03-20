using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.LanFlows.Dtos;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.LanFlows
{
    public interface ILanFlowStatusActionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLanFlowStatusActionForView>> GetAll(GetAllLanFlowStatusActionsInput input);

		Task<GetLanFlowStatusActionForEditOutput> GetLanFlowStatusActionForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditLanFlowStatusActionDto input);

		Task Delete(EntityDto input);

		
		Task<PagedResultDto<LanFlowSchemeLookupTableDto>> GetAllLanFlowSchemeForLookupTable(GetAllForLookupTableInput input);
		
    }
}