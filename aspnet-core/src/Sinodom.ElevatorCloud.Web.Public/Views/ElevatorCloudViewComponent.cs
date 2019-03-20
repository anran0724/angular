using Abp.AspNetCore.Mvc.ViewComponents;

namespace Sinodom.ElevatorCloud.Web.Public.Views
{
    public abstract class ElevatorCloudViewComponent : AbpViewComponent
    {
        protected ElevatorCloudViewComponent()
        {
            LocalizationSourceName = ElevatorCloudConsts.LocalizationSourceName;
        }
    }
}
