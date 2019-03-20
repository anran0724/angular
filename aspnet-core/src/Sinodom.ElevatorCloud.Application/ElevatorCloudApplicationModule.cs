using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Sinodom.ElevatorCloud.Authorization;

namespace Sinodom.ElevatorCloud
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(ElevatorCloudCoreModule)
        )]
    public class ElevatorCloudApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ElevatorCloudApplicationModule).GetAssembly());
        }
    }
}
