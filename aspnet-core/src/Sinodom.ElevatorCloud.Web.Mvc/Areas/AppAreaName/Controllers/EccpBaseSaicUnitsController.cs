using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseSaicUnits;
using Sinodom.ElevatorCloud.Web.Controllers;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.EccpBaseSaicUnits;
using Sinodom.ElevatorCloud.EccpBaseSaicUnits.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpBaseSaicUnits)]
    public class EccpBaseSaicUnitsController : ElevatorCloudControllerBase
    {
        private readonly IEccpBaseSaicUnitsAppService _eccpBaseSaicUnitsAppService;

        public EccpBaseSaicUnitsController(IEccpBaseSaicUnitsAppService eccpBaseSaicUnitsAppService)
        {
            _eccpBaseSaicUnitsAppService = eccpBaseSaicUnitsAppService;
        }

        public ActionResult Index()
        {
            var model = new EccpBaseSaicUnitsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpBaseSaicUnits_Create, AppPermissions.Pages_Administration_EccpBaseSaicUnits_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpBaseSaicUnitForEditOutput getEccpBaseSaicUnitForEditOutput;

            if (id.HasValue)
            {
                getEccpBaseSaicUnitForEditOutput = await _eccpBaseSaicUnitsAppService.GetEccpBaseSaicUnitForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpBaseSaicUnitForEditOutput = new GetEccpBaseSaicUnitForEditOutput
                {
                    EccpBaseSaicUnit = new EditEccpBaseSaicUnitDto()
                };
            }

            var viewModel = new CreateOrEditEccpBaseSaicUnitModalViewModel()
            {
                EccpBaseSaicUnit = getEccpBaseSaicUnitForEditOutput.EccpBaseSaicUnit,
                ECCPBaseAreaName = getEccpBaseSaicUnitForEditOutput.ECCPBaseAreaName,
                ECCPBaseAreaName2 = getEccpBaseSaicUnitForEditOutput.ECCPBaseAreaName2,
                ECCPBaseAreaName3 = getEccpBaseSaicUnitForEditOutput.ECCPBaseAreaName3,
                ECCPBaseAreaName4 = getEccpBaseSaicUnitForEditOutput.ECCPBaseAreaName4
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public PartialViewResult ViewEccpBaseSaicUnitModal(GetEccpBaseSaicUnitForView data)
        {
            var model = new EccpBaseSaicUnitViewModel()
            {
                EccpBaseSaicUnit = data.EccpBaseSaicUnit
,
                ECCPBaseAreaName = data.ECCPBaseAreaName
,
                ECCPBaseAreaName2 = data.ECCPBaseAreaName2
,
                ECCPBaseAreaName3 = data.ECCPBaseAreaName3
,
                ECCPBaseAreaName4 = data.ECCPBaseAreaName4

            };

            return PartialView("_ViewEccpBaseSaicUnitModal", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpBaseSaicUnits_Create, AppPermissions.Pages_Administration_EccpBaseSaicUnits_Edit)]
        public PartialViewResult ECCPBaseAreaLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ECCPBaseAreaLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_ECCPBaseAreaLookupTableModal", viewModel);
        }

    }
}