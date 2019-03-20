using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Layout;
using Sinodom.ElevatorCloud.Web.Session;
using Sinodom.ElevatorCloud.Web.Views;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Views.Shared.Components.AppAreaNameTheme11Brand
{
    public class AppAreaNameTheme11BrandViewComponent : ElevatorCloudViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppAreaNameTheme11BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(headerModel);
        }
    }
}
