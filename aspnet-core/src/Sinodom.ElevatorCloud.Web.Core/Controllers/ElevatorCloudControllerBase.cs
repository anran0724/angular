using System;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Sinodom.ElevatorCloud.Extensions;

namespace Sinodom.ElevatorCloud.Web.Controllers
{
    public abstract class ElevatorCloudControllerBase : AbpController
    {
        //Òþ²Ø¸¸ÀàµÄAbpSession
        public new IEccpAbpSessionExtensions AbpSession { get; set; }
        protected ElevatorCloudControllerBase()
        {
            LocalizationSourceName = ElevatorCloudConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        protected void SetTenantIdCookie(int? tenantId)
        {
            Response.Cookies.Append(
                "Abp.TenantId",
                tenantId?.ToString(),
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddYears(5),
                    Path = "/"
                }
            );
        }
    }
}
