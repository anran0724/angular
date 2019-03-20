using System;
using System.IO;
using Abp;
using Abp.AspNetZeroCore;
using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.Organizations;
using Abp.TestBase;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using Sinodom.ElevatorCloud.Authorization.Roles;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.Configuration;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using Sinodom.ElevatorCloud.MultiTenancy;
using Sinodom.ElevatorCloud.Security.Recaptcha;
using Sinodom.ElevatorCloud.Tests.Configuration;
using Sinodom.ElevatorCloud.Tests.DependencyInjection;
using Sinodom.ElevatorCloud.Tests.UiCustomization;
using Sinodom.ElevatorCloud.Tests.Url;
using Sinodom.ElevatorCloud.Tests.Web;
using Sinodom.ElevatorCloud.UiCustomization;
using Sinodom.ElevatorCloud.Url;
using NSubstitute;

namespace Sinodom.ElevatorCloud.Tests
{
    [DependsOn(
        typeof(ElevatorCloudApplicationModule),
        typeof(ElevatorCloudEntityFrameworkCoreModule),
        typeof(AbpTestBaseModule))]
    public class ElevatorCloudTestModule : AbpModule
    {
        public ElevatorCloudTestModule(ElevatorCloudEntityFrameworkCoreModule elevatorCloudEntityFrameworkCoreModule)
        {
            elevatorCloudEntityFrameworkCoreModule.SkipDbContextRegistration = true;
        }

        public override void PreInitialize()
        {
            var configuration = GetConfiguration();

            Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(30);
            Configuration.UnitOfWork.IsTransactional = false;

            //Disable static mapper usage since it breaks unit tests (see https://github.com/aspnetboilerplate/aspnetboilerplate/issues/2052)
            //Configuration.Modules.AbpAutoMapper().UseStaticMapper = false;

            //Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            RegisterFakeService<AbpZeroDbMigrator>();

            IocManager.Register<IAppUrlService, FakeAppUrlService>();
            IocManager.Register<IWebUrlService, FakeWebUrlService>();
            IocManager.Register<IRecaptchaValidator, FakeRecaptchaValidator>();

            Configuration.ReplaceService<IAppConfigurationAccessor, TestAppConfigurationAccessor>();
            Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);

            Configuration.ReplaceService<IUiThemeCustomizerFactory, NullUiThemeCustomizerFactory>();

            Configuration.Modules.AspNetZero().LicenseCode = configuration["AbpZeroLicenseCode"];

            //Uncomment below line to write change logs for the entities below:
            Configuration.EntityHistory.IsEnabled = true;
            Configuration.EntityHistory.Selectors.Add("ElevatorCloudEntities", typeof(User), typeof(Tenant));

            Configuration.Modules.AbpAutoMapper().UseStaticMapper = true;
        }

        public override void Initialize()
        {
            ServiceCollectionRegistrar.Register(IocManager);
        }

        private void RegisterFakeService<TService>()
            where TService : class
        {
            IocManager.IocContainer.Register(
                Component.For<TService>()
                    .UsingFactoryMethod(() => Substitute.For<TService>())
                    .LifestyleSingleton()
            );
        }

        private static IConfigurationRoot GetConfiguration()
        {
            return AppConfigurations.Get(Directory.GetCurrentDirectory(), addUserSecrets: true);
        }
    }
}
