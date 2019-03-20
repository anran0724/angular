using Abp.Dependency;
using Sinodom.ElevatorCloud.Configuration;
using Sinodom.ElevatorCloud.Url;
using Sinodom.ElevatorCloud.Web.Url;

namespace Sinodom.ElevatorCloud.Web.Public.Url
{
    public class WebUrlService : WebUrlServiceBase, IWebUrlService, ITransientDependency
    {
        public WebUrlService(
            IAppConfigurationAccessor appConfigurationAccessor) :
            base(appConfigurationAccessor)
        {
        }

        public override string WebSiteRootAddressFormatKey => "App:WebSiteRootAddress";

        public override string ServerRootAddressFormatKey => "App:AdminWebSiteRootAddress";
    }
}
