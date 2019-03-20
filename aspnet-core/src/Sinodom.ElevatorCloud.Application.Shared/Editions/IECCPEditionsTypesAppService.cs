using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Editions.Dtos;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.Editions
{
    public interface IECCPEditionsTypesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetECCPEditionsTypeForView>> GetAll(GetAllECCPEditionsTypesInput input);

		Task<GetECCPEditionsTypeForEditOutput> GetECCPEditionsTypeForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditECCPEditionsTypeDto input);

		Task Delete(EntityDto input);

		
    }
}