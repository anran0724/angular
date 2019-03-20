using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.LanFlowSchemes;
using Sinodom.ElevatorCloud.Web.Controllers;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.LanFlows;
using Sinodom.ElevatorCloud.LanFlows.Dtos;
using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_LanFlowSchemes)]
    public class LanFlowSchemesController : ElevatorCloudControllerBase
    {
        private readonly ILanFlowSchemesAppService _lanFlowSchemesAppService;

        public LanFlowSchemesController(ILanFlowSchemesAppService lanFlowSchemesAppService)
        {
            _lanFlowSchemesAppService = lanFlowSchemesAppService;
        }

        public ActionResult Index()
        {
            var model = new LanFlowSchemesViewModel
			{
				FilterText = ""
			};

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_LanFlowSchemes_Create, AppPermissions.Pages_Administration_LanFlowSchemes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
			GetLanFlowSchemeForEditOutput getLanFlowSchemeForEditOutput;

			if (id.HasValue){
				getLanFlowSchemeForEditOutput = await _lanFlowSchemesAppService.GetLanFlowSchemeForEdit(new EntityDto { Id = (int) id });
			}
			else{
				getLanFlowSchemeForEditOutput = new GetLanFlowSchemeForEditOutput{
					LanFlowScheme = new CreateOrEditLanFlowSchemeDto()
				};
			}

            var viewModel = new CreateOrEditLanFlowSchemeModalViewModel()
            {
				LanFlowScheme = getLanFlowSchemeForEditOutput.LanFlowScheme
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public PartialViewResult ViewLanFlowSchemeModal(GetLanFlowSchemeForView data)
        {
            var model = new LanFlowSchemeViewModel()
            {
				LanFlowScheme = data.LanFlowScheme

            };

            return PartialView("_ViewLanFlowSchemeModal", model);
        }


    }
}