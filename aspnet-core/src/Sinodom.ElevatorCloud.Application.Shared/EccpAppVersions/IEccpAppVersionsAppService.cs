using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.EccpAppVersions.Dtos;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.EccpAppVersions
{
    public interface IEccpAppVersionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetEccpAppVersionForView>> GetAll(GetAllEccpAppVersionsInput input);

		Task<GetEccpAppVersionForEditOutput> GetEccpAppVersionForEdit(EntityDto input);

		Task CreateOrEdit(UploadCreateOrEditEccpAppVersionDto input);

		Task Delete(EntityDto input);

        Task<GetEccpAppVersionForEditOutput> GetEccpAppVersionByVersionType(string versionType);
    }
}