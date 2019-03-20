using Abp.AspNetZeroCore;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using Sinodom.ElevatorCloud.Configuration;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Migrator.DependencyInjection;

namespace Sinodom.ElevatorCloud.Migrator
{
    [DependsOn(typeof(ElevatorCloudEntityFrameworkCoreModule))]
    public class ElevatorCloudMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public ElevatorCloudMigratorModule(ElevatorCloudEntityFrameworkCoreModule elevatorCloudEntityFrameworkCoreModule)
        {
            elevatorCloudEntityFrameworkCoreModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(ElevatorCloudMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                ElevatorCloudConsts.ConnectionStringName
                );
            Configuration.Modules.AspNetZero().LicenseCode = _appConfiguration["AbpZeroLicenseCode"];

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(typeof(IEventBus), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ElevatorCloudMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
