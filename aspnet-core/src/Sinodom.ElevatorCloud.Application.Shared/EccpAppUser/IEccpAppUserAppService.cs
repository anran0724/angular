 
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.EccpAppVersions.Dtos;
 
using Sinodom.ElevatorCloud.EccpAppUser.Dtos;

namespace Sinodom.ElevatorCloud.EccpAppUser
{
    public interface IEccpAppUserAppService : IApplicationService 
    {
        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <returns></returns>
        Task<UserInfo> GetUser();
 
    }
}