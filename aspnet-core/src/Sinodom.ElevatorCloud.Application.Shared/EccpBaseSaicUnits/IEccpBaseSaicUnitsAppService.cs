using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.EccpBaseSaicUnits.Dtos;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.EccpBaseSaicUnits
{
    public interface IEccpBaseSaicUnitsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetEccpBaseSaicUnitForView>> GetAll(GetAllEccpBaseSaicUnitsInput input);

		Task<GetEccpBaseSaicUnitForEditOutput> GetEccpBaseSaicUnitForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditEccpBaseSaicUnitDto input);

		Task Delete(EntityDto input);

		
		Task<PagedResultDto<ECCPBaseAreaLookupTableDto>> GetAllECCPBaseAreaForLookupTable(GetAllForLookupTableInput input);
		
    }
}