using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Authorization.Users.Dto;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.Authorization.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input);

        Task<FileDto> GetUsersToExcel();

        Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input);

        Task<GetUserForEditOutput> GetUserForAppView(NullableIdDto<long> input);

        Task<PagedResultDto<UserListDto>> GetUsersForAppView(GetUsersInput input);

        Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(EntityDto<long> input);

        Task ResetUserSpecificPermissions(EntityDto<long> input);

        Task UpdateUserPermissions(UpdateUserPermissionsInput input);

        Task CreateOrUpdateUser(CreateOrUpdateUserInput input);

        Task DeleteUser(EntityDto<long> input);

        Task UnlockUser(EntityDto<long> input);
    }
}
