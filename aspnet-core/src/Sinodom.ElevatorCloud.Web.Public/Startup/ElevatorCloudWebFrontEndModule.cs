using Abp.AspNetZeroCore;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Sinodom.ElevatorCloud.Configuration;
using Sinodom.ElevatorCloud.EntityFrameworkCore;

namespace Sinodom.ElevatorCloud.Web.Public.Startup
{
    [DependsOn(
        typeof(ElevatorCloudWebCoreModule)
    )]
    public class ElevatorCloudWebFrontEndModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public ElevatorCloudWebFrontEndModule(IHostingEnvironment env, ElevatorCloudEntityFrameworkCoreModule elevatorCloudEntityFrameworkCoreModule)
        {
            _appConfiguration = env.GetAppConfiguration();
            elevatorCloudEntityFrameworkCoreModule.SkipDbSeed = true;
        }

        public override void PreInitialize()
        {
            Configuration.Modules.AbpWebCommon().MultiTenancy.DomainFormat = _appConfiguration["App:WebSiteRootAddress"] ?? "http://localhost:46776/";
            Configuration.Modules.AspNetZero().LicenseCode = _appConfiguration["AbpZeroLicenseCode"];

            //Changed AntiForgery token/cookie names to not conflict to the main application while redirections.
            Configuration.Modules.AbpWebCommon().AntiForgery.TokenCookieName = "Public-XSRF-TOKEN";
            Configuration.Modules.AbpWebCommon().AntiForgery.TokenHeaderName = "Public-X-XSRF-TOKEN";

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

            Configuration.Navigation.Providers.Add<FrontEndNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ElevatorCloudWebFrontEndModule).GetAssembly());
        }
    }
}
