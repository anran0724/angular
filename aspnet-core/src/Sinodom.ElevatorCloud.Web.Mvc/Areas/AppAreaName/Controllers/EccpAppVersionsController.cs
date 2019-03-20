using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpAppVersions;
using Sinodom.ElevatorCloud.Web.Controllers;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.EccpAppVersions;
using Sinodom.ElevatorCloud.EccpAppVersions.Dtos;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Storage;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpAppVersions)]
    public class EccpAppVersionsController : ProfileAppVersionControllerBase
    {
        private readonly IEccpAppVersionsAppService _eccpAppVersionsAppService;

        public EccpAppVersionsController(IEccpAppVersionsAppService eccpAppVersionsAppService, ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
            _eccpAppVersionsAppService = eccpAppVersionsAppService;
        }

        public ActionResult Index()
        {
            var model = new EccpAppVersionsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpAppVersions_Create, AppPermissions.Pages_Administration_EccpAppVersions_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpAppVersionForEditOutput getEccpAppVersionForEditOutput;

            if (id.HasValue)
            {
                getEccpAppVersionForEditOutput = await _eccpAppVersionsAppService.GetEccpAppVersionForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpAppVersionForEditOutput = new GetEccpAppVersionForEditOutput
                {
                    EccpAppVersion = new CreateOrEditEccpAppVersionDto()
                };
            }

            var viewModel = new CreateOrEditEccpAppVersionModalViewModel()
            {
                EccpAppVersion = getEccpAppVersionForEditOutput.EccpAppVersion
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public PartialViewResult ViewEccpAppVersionModal(GetEccpAppVersionForView data)
        {
            var model = new EccpAppVersionViewModel()
            {
                EccpAppVersion = data.EccpAppVersion

            };

            return PartialView("_ViewEccpAppVersionModal", model);
        }


    }
}