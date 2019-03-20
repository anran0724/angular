using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Editions;
using Sinodom.ElevatorCloud.Web.Controllers;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Editions)]
    public class EditionsController : ElevatorCloudControllerBase
    {
        private readonly IEditionAppService _editionAppService;

        public EditionsController(IEditionAppService editionAppService)
        {
            _editionAppService = editionAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Editions_Create, AppPermissions.Pages_Editions_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            var output = await _editionAppService.GetEditionForEdit(new NullableIdDto { Id = id });
            var editionItems = await _editionAppService.GetEditionComboboxItems();
            var freeEditionItems = await _editionAppService.GetEditionComboboxItems(output.Edition.ExpiringEditionId, false, true);

            var viewModel = new CreateOrEditEditionModalViewModel(output, editionItems, freeEditionItems);

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Editions_Create, AppPermissions.Pages_Editions_Edit)]
        public PartialViewResult ECCPEditionTypeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ECCPEditionTypeLookupTableViewModel()
                                {
                                    Id = id,
                                    DisplayName = displayName,
                                    FilterText = ""
                                };

            return PartialView("_ECCPEditionTypeLookupTableModal", viewModel);
        }
    }
}
