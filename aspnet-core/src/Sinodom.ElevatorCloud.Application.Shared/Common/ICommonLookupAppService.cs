using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Common.Dto;
using Sinodom.ElevatorCloud.Editions.Dto;

namespace Sinodom.ElevatorCloud.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();

        string Synchronize(string jsonData);
        Task<int> CompaniesDataSynchronous(DeptDataSynchronousEntity entity);
        Task<long> UserDataSynchronous(UserDataSynchronousEntity entity);
        Task<int> ElevatorsDataSynchronous(ElevatorsDataSynchronousEntity entity);
    }
}
