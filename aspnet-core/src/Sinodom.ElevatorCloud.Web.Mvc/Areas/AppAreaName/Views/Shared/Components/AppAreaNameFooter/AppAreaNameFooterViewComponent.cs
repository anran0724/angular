using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Layout;
using Sinodom.ElevatorCloud.Web.Session;
using Sinodom.ElevatorCloud.Web.Views;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Views.Shared.Components.AppAreaNameFooter
{
    public class AppAreaNameFooterViewComponent : ElevatorCloudViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppAreaNameFooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
