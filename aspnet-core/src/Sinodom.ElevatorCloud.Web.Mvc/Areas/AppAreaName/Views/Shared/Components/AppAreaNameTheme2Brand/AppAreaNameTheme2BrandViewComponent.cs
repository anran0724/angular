﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Layout;
using Sinodom.ElevatorCloud.Web.Session;
using Sinodom.ElevatorCloud.Web.Views;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Views.Shared.Components.AppAreaNameTheme2Brand
{
    public class AppAreaNameTheme2BrandViewComponent : ElevatorCloudViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppAreaNameTheme2BrandViewComponent(IPerRequestSessionCache sessionCache)
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
