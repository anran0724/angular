using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Sinodom.ElevatorCloud.Authentication.TwoFactor.Google;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.Authorization.Roles;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.MultiTenancy;

namespace Sinodom.ElevatorCloud.Identity
{
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>(options =>
                {
                    options.Tokens.ProviderMap[GoogleAuthenticatorProvider.Name] = new TokenProviderDescriptor(typeof(GoogleAuthenticatorProvider));
                })
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpSignInManager<SignInManager>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}
