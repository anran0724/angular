using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.LanFlows.Dtos;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.LanFlows
{
    public interface ILanFlowInstanceOperationHistoriesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLanFlowInstanceOperationHistoryForView>> GetAll(GetAllLanFlowInstanceOperationHistoriesInput input);

		Task<GetLanFlowInstanceOperationHistoryForEditOutput> GetLanFlowInstanceOperationHistoryForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditLanFlowInstanceOperationHistoryDto input);

		Task Delete(EntityDto input);
		
    }
}