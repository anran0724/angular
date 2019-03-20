using System.Threading.Tasks;
using Abp.Application.Services;
using Sinodom.ElevatorCloud.Sessions.Dto;

namespace Sinodom.ElevatorCloud.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
