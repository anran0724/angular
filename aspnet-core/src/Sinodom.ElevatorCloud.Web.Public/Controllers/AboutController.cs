using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Web.Controllers;

namespace Sinodom.ElevatorCloud.Web.Public.Controllers
{
    public class AboutController : ElevatorCloudControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
