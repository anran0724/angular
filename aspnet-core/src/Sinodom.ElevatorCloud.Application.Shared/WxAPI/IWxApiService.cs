using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.WxAPI
{
     public interface IWxApiService : IApplicationService
    {
        /// <summary>
        ///获取用户2维码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <param name="workId"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        string GetWorkQrCode(string key, int userId, int workId, string longitude, string latitude);

        /// <summary>
        /// 根据用户手机号获取公司
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        string GetTenantsByMobile(string mobile);
    }
}
