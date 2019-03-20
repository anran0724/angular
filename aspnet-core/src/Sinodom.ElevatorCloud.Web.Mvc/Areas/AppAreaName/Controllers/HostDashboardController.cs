using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.HostDashboard;
using Sinodom.ElevatorCloud.Web.Controllers;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Host_Dashboard)]
    public class HostDashboardController : ElevatorCloudControllerBase
    {
        private const int DashboardOnLoadReportDayCount = 7;

        public ActionResult Index()
        {
            return View(new HostDashboardViewModel(DashboardOnLoadReportDayCount));
        }
    }
}
