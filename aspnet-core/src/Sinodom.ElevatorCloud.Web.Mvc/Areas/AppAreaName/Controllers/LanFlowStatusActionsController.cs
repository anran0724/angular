using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.LanFlowStatusActions;
using Sinodom.ElevatorCloud.Web.Controllers;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.LanFlows;
using Sinodom.ElevatorCloud.LanFlows.Dtos;
using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_LanFlowStatusActions)]
    public class LanFlowStatusActionsController : ElevatorCloudControllerBase
    {
        private readonly ILanFlowStatusActionsAppService _lanFlowStatusActionsAppService;

        public LanFlowStatusActionsController(ILanFlowStatusActionsAppService lanFlowStatusActionsAppService)
        {
            _lanFlowStatusActionsAppService = lanFlowStatusActionsAppService;
        }

        public ActionResult Index()
        {
            var model = new LanFlowStatusActionsViewModel
			{
				FilterText = ""
			};

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_LanFlowStatusActions_Create, AppPermissions.Pages_Administration_LanFlowStatusActions_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
			GetLanFlowStatusActionForEditOutput getLanFlowStatusActionForEditOutput;

			if (id.HasValue){
				getLanFlowStatusActionForEditOutput = await _lanFlowStatusActionsAppService.GetLanFlowStatusActionForEdit(new EntityDto { Id = (int) id });
			}
			else{
				getLanFlowStatusActionForEditOutput = new GetLanFlowStatusActionForEditOutput{
					LanFlowStatusAction = new CreateOrEditLanFlowStatusActionDto()
				};
			}

            var viewModel = new CreateOrEditLanFlowStatusActionModalViewModel()
            {
				LanFlowStatusAction = getLanFlowStatusActionForEditOutput.LanFlowStatusAction,
					LanFlowSchemeSchemeName = getLanFlowStatusActionForEditOutput.LanFlowSchemeSchemeName
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public PartialViewResult ViewLanFlowStatusActionModal(GetLanFlowStatusActionForView data)
        {
            var model = new LanFlowStatusActionViewModel()
            {
				LanFlowStatusAction = data.LanFlowStatusAction
, LanFlowSchemeSchemeName = data.LanFlowSchemeSchemeName 

            };

            return PartialView("_ViewLanFlowStatusActionModal", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_LanFlowStatusActions_Create, AppPermissions.Pages_Administration_LanFlowStatusActions_Edit)]
        public PartialViewResult LanFlowSchemeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new LanFlowSchemeLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_LanFlowSchemeLookupTableModal", viewModel);
        }

    }
}