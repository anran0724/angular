using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abp.Configuration.Startup;
using Abp.MultiTenancy;
using Abp.Runtime;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Identity;
using Sinodom.ElevatorCloud.Consts;

namespace Sinodom.ElevatorCloud.Extensions
{
    public class EccpAbpSessionExtensions : ClaimsAbpSession, IEccpAbpSessionExtensions
    {
        public EccpAbpSessionExtensions(IPrincipalAccessor principalAccessor, IMultiTenancyConfig multiTenancy, ITenantResolver tenantResolver, IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider) : base(principalAccessor, multiTenancy, tenantResolver, sessionOverrideScopeProvider)
        {
        }

        public int? ProvinceId => GetClaimValueInt(EccpAbpSessionConsts.ClaimTypes.ProvinceId);
        public int? CityId => GetClaimValueInt(EccpAbpSessionConsts.ClaimTypes.CityId);
        public int? DistrictId => GetClaimValueInt(EccpAbpSessionConsts.ClaimTypes.DistrictId);
        public int? StreetId => GetClaimValueInt(EccpAbpSessionConsts.ClaimTypes.StreetId);
        public int? EditionId => GetClaimValueInt(EccpAbpSessionConsts.ClaimTypes.EditionId);
        public string EditionTypeName => GetClaimValue(EccpAbpSessionConsts.ClaimTypes.EditionTypeName);
        public string CompanieName => GetClaimValue(EccpAbpSessionConsts.ClaimTypes.CompanieName);


        private string GetClaimValue(string claimType)
        {
            var claimsPrincipal = PrincipalAccessor.Principal;

            var claim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == claimType);
            if (string.IsNullOrEmpty(claim?.Value))
                return null;

            return claim.Value;
        }

        private int? GetClaimValueInt(string claimType)
        {
            var claimsPrincipal = PrincipalAccessor.Principal;

            var claim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == claimType);
            if (!int.TryParse(claim?.Value, out var iValue))
                return null;

            return iValue;
        }
    }
}
