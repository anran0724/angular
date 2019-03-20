using Abp.AspNetCore.Mvc.ViewComponents;

namespace Sinodom.ElevatorCloud.Web.Views
{
    public abstract class ElevatorCloudViewComponent : AbpViewComponent
    {
        protected ElevatorCloudViewComponent()
        {
            LocalizationSourceName = ElevatorCloudConsts.LocalizationSourceName;
        }
    }
}
