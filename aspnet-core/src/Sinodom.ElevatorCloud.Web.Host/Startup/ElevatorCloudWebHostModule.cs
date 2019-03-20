using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Abp.AspNetZeroCore;
using Abp.AspNetZeroCore.Web.Authentication.External;
using Abp.AspNetZeroCore.Web.Authentication.External.Facebook;
using Abp.AspNetZeroCore.Web.Authentication.External.Google;
using Abp.AspNetZeroCore.Web.Authentication.External.Microsoft;
using Abp.AspNetZeroCore.Web.Authentication.External.OpenIdConnect;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Threading.BackgroundWorkers;
using Abp.Timing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Sinodom.ElevatorCloud.Configuration;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using Sinodom.ElevatorCloud.MultiTenancy;
using Newtonsoft.Json.Linq;

namespace Sinodom.ElevatorCloud.Web.Startup
{
    [DependsOn(
        typeof(ElevatorCloudWebCoreModule)
    )]
    public class ElevatorCloudWebHostModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ElevatorCloudWebHostModule(
            IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Modules.AbpWebCommon().MultiTenancy.DomainFormat = _appConfiguration["App:ServerRootAddress"] ?? "http://localhost:23742/";
            Configuration.Modules.AspNetZero().LicenseCode = _appConfiguration["AbpZeroLicenseCode"];
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ElevatorCloudWebHostModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            using (var scope = IocManager.CreateScope())
            {
                if (!scope.Resolve<DatabaseCheckHelper>().Exist(_appConfiguration["ConnectionStrings:Default"]))
                {
                    return;
                }
            }

            if (IocManager.Resolve<IMultiTenancyConfig>().IsEnabled)
            {
                var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
                workManager.Add(IocManager.Resolve<SubscriptionExpirationCheckWorker>());
                workManager.Add(IocManager.Resolve<SubscriptionExpireEmailNotifierWorker>());
            }

            ConfigureExternalAuthProviders();
        }

        private void ConfigureExternalAuthProviders()
        {
            var externalAuthConfiguration = IocManager.Resolve<ExternalAuthConfiguration>();

            if (bool.Parse(_appConfiguration["Authentication:OpenId:IsEnabled"]))
            {
                externalAuthConfiguration.Providers.Add(
                    new ExternalLoginProviderInfo(
                        OpenIdConnectAuthProviderApi.Name,
                        _appConfiguration["Authentication:OpenId:ClientId"],
                        _appConfiguration["Authentication:OpenId:ClientSecret"],
                        typeof(OpenIdConnectAuthProviderApi),
                        new Dictionary<string, string>
                        {
                            {"Authority", _appConfiguration["Authentication:OpenId:Authority"]},
                            {"LoginUrl",_appConfiguration["Authentication:OpenId:LoginUrl"]}
                        }
                    )
                );
            }

            if (bool.Parse(_appConfiguration["Authentication:Facebook:IsEnabled"]))
            {
                externalAuthConfiguration.Providers.Add(
                    new ExternalLoginProviderInfo(
                        FacebookAuthProviderApi.Name,
                        _appConfiguration["Authentication:Facebook:AppId"],
                        _appConfiguration["Authentication:Facebook:AppSecret"],
                        typeof(FacebookAuthProviderApi)
                    )
                );
            }

            if (bool.Parse(_appConfiguration["Authentication:Google:IsEnabled"]))
            {
                externalAuthConfiguration.Providers.Add(
                    new ExternalLoginProviderInfo(
                        GoogleAuthProviderApi.Name,
                        _appConfiguration["Authentication:Google:ClientId"],
                        _appConfiguration["Authentication:Google:ClientSecret"],
                        typeof(GoogleAuthProviderApi)
                    )
                );
            }

            //not implemented yet. Will be implemented with https://github.com/aspnetzero/aspnet-zero-angular/issues/5
            if (bool.Parse(_appConfiguration["Authentication:Microsoft:IsEnabled"]))
            {
                externalAuthConfiguration.Providers.Add(
                    new ExternalLoginProviderInfo(
                        MicrosoftAuthProviderApi.Name,
                        _appConfiguration["Authentication:Microsoft:ConsumerKey"],
                        _appConfiguration["Authentication:Microsoft:ConsumerSecret"],
                        typeof(MicrosoftAuthProviderApi)
                    )
                );
            }
        }
    }
}
