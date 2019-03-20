using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions
{
    public interface IUserPathHistoriesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetUserPathHistoryForView>> GetAll(GetAllUserPathHistoriesInput input);

		Task<GetUserPathHistoryForEditOutput> GetUserPathHistoryForEdit(EntityDto<long> input);

		Task CreateOrEdit(CreateOrEditUserPathHistoryDto input);

		Task Delete(EntityDto<long> input);

		
		Task<PagedResultDto<UserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}