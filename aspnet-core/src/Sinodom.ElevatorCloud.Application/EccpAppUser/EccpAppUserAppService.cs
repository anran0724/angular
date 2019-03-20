using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.EccpAppUser.Dtos; 
using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;


namespace Sinodom.ElevatorCloud.EccpAppUser
{
    /// <summary>
    /// 用户详情
    /// </summary>
    public class EccpAppUserAppService : ElevatorCloudAppServiceBase, IEccpAppUserAppService
    {

        private readonly IRepository<EccpCompanyUserExtension, int> _eccpCompanyUserExtensionRepository;

        public EccpAppUserAppService(IRepository<EccpCompanyUserExtension, int> eccpCompanyUserExtensionRepository)
        {
            _eccpCompanyUserExtensionRepository = eccpCompanyUserExtensionRepository;
        }

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <returns></returns>
        public async Task<UserInfo> GetUser()
        {
            var userInfo = await _eccpCompanyUserExtensionRepository.GetAll().Where(w=>w.UserId == AbpSession.UserId).Select(s => new UserInfo
            {
                UserId = s.UserId,
                SyncUserId = s.SyncUserId
            }).FirstOrDefaultAsync();
            return userInfo;
        }
        

    }
}