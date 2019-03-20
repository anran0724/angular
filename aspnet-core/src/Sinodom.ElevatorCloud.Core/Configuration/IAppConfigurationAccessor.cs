using Microsoft.Extensions.Configuration;

namespace Sinodom.ElevatorCloud.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
