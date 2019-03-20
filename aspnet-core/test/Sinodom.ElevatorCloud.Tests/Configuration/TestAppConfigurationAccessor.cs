using Abp.Dependency;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using Sinodom.ElevatorCloud.Configuration;

namespace Sinodom.ElevatorCloud.Tests.Configuration
{
    public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public TestAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(
                typeof(ElevatorCloudTestModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }
    }
}
