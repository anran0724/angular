// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTemplatesController.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.AspNetCore.Mvc.Authorization;

    using Microsoft.AspNetCore.Mvc;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceTemplates;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance templates controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates)]
    public class EccpMaintenanceTemplatesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp maintenance template nodes app service.
        /// </summary>
        private readonly IEccpMaintenanceTemplateNodesAppService _eccpMaintenanceTemplateNodesAppService;

        /// <summary>
        /// The _eccp maintenance templates app service.
        /// </summary>
        private readonly IEccpMaintenanceTemplatesAppService _eccpMaintenanceTemplatesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTemplatesController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceTemplatesAppService">
        /// The eccp maintenance templates app service.
        /// </param>
        /// <param name="eccpMaintenanceTemplateNodesAppService">
        /// The eccp maintenance template nodes app service.
        /// </param>
        public EccpMaintenanceTemplatesController(
            IEccpMaintenanceTemplatesAppService eccpMaintenanceTemplatesAppService,
            IEccpMaintenanceTemplateNodesAppService eccpMaintenanceTemplateNodesAppService)
        {
            this._eccpMaintenanceTemplatesAppService = eccpMaintenanceTemplatesAppService;
            this._eccpMaintenanceTemplateNodesAppService = eccpMaintenanceTemplateNodesAppService;
        }

        /// <summary>
        /// The create or edit modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpMvcAuthorize(
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpMaintenanceTemplateForEditOutput getEccpMaintenanceTemplateForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceTemplateForEditOutput =
                    await this._eccpMaintenanceTemplatesAppService.GetEccpMaintenanceTemplateForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpMaintenanceTemplateForEditOutput =
                    new GetEccpMaintenanceTemplateForEditOutput
                        {
                            EccpMaintenanceTemplate = new CreateOrEditEccpMaintenanceTemplateDto()
                        };
            }

            var viewModel = new CreateOrEditEccpMaintenanceTemplateViewModel
                                {
                                    EccpMaintenanceTemplate =
                                        getEccpMaintenanceTemplateForEditOutput.EccpMaintenanceTemplate,
                                    EccpDictMaintenanceTypeName =
                                        getEccpMaintenanceTemplateForEditOutput.EccpDictMaintenanceTypeName,
                                    EccpDictElevatorTypeName =
                                        getEccpMaintenanceTemplateForEditOutput.EccpDictElevatorTypeName
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp dict elevator type lookup table modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        [AbpMvcAuthorize(
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates_Edit)]
        public PartialViewResult EccpDictElevatorTypeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpDictElevatorTypeLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpDictElevatorTypeLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp dict maintenance type lookup table modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        [AbpMvcAuthorize(
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates_Edit)]
        public PartialViewResult EccpDictMaintenanceTypeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpDictMaintenanceTypeLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpDictMaintenanceTypeLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp maintenance template nodes table modal.
        /// 弹出子类页面
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult EccpMaintenanceTemplateNodesTableModal(int? id)
        {
            this.ViewBag.MaintenanceTemplateId = id;

            return this.PartialView("_EccpMaintenanceTemplateNodesTableModal");
        }

        /// <summary>
        /// The get maintenance template nodes.
        /// 获取模板节点
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The<see cref="JsonResult"/>.
        /// </returns>
        public JsonResult GetMaintenanceTemplateNodes(int id)
        {
            return this.Json(this._eccpMaintenanceTemplateNodesAppService.GetMaintenanceTemplateNodes(id));
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpMaintenanceTemplatesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp maintenance template modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpMaintenanceTemplateModal(GetEccpMaintenanceTemplateForView data)
        {
            var model = new EccpMaintenanceTemplateViewModel
                            {
                                EccpMaintenanceTemplate = data.EccpMaintenanceTemplate,
                                EccpDictMaintenanceTypeName = data.EccpDictMaintenanceTypeName,
                                EccpDictElevatorTypeName = data.EccpDictElevatorTypeName
                            };

            return this.PartialView("_ViewEccpMaintenanceTemplateModal", model);
        }
    }
}