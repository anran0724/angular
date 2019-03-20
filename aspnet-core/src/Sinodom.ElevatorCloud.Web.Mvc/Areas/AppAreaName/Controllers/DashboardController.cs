using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.StatisticalElevator;
using Sinodom.ElevatorCloud.StatisticalElevator.Dto;
using Sinodom.ElevatorCloud.Web.Controllers;
using System.Threading.Tasks;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Tenant_Dashboard)]
    public class DashboardController : ElevatorCloudControllerBase
    {
        private readonly IStatisticalElevatorAppService _eccpBaseAreasAppService;

        public DashboardController(IStatisticalElevatorAppService eccpBaseAreasAppService)
        {
            _eccpBaseAreasAppService = eccpBaseAreasAppService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PropertyCompaniesIndex()
        {
            return View();
        }
        [HttpPost]
        public  async Task<JsonResult> GetArea(GetRegionalCollectionInput input)
        {           
            var data =await  _eccpBaseAreasAppService.RegionalCollection(input);

            return Json(data);
        }
        [HttpPost]
        public async Task<JsonResult> GetElevator(GetElevatorCollectionInput input)
        {
            var data = await _eccpBaseAreasAppService.ElevatorCollection(input);          

            return Json(data);
        } 
    } 
}
