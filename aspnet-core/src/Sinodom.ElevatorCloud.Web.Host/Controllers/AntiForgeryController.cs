using Microsoft.AspNetCore.Antiforgery;

namespace Sinodom.ElevatorCloud.Web.Controllers
{
    public class AntiForgeryController : ElevatorCloudControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
