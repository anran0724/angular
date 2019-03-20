using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace Sinodom.ElevatorCloud.Web.Controllers
{
    public class HomeController : ElevatorCloudControllerBase
    {
        [DisableAuditing]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
