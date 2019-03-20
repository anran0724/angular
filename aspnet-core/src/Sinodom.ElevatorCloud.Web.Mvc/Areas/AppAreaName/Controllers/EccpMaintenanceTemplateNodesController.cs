// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTemplateNodesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceTemplateNodes;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance template nodes controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes)]
    public class EccpMaintenanceTemplateNodesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp dict maintenance items app service.
        /// </summary>
        private readonly IEccpDictMaintenanceItemsAppService _eccpDictMaintenanceItemsAppService;

        /// <summary>
        /// The _eccp maintenance template nodes app service.
        /// </summary>
        private readonly IEccpMaintenanceTemplateNodesAppService _eccpMaintenanceTemplateNodesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTemplateNodesController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceTemplateNodesAppService">
        /// The eccp maintenance template nodes app service.
        /// </param>
        /// <param name="eccpDictMaintenanceItemsAppService">
        /// The eccp dict maintenance items app service.
        /// </param>
        public EccpMaintenanceTemplateNodesController(
            IEccpMaintenanceTemplateNodesAppService eccpMaintenanceTemplateNodesAppService,
            IEccpDictMaintenanceItemsAppService eccpDictMaintenanceItemsAppService)
        {
            this._eccpMaintenanceTemplateNodesAppService = eccpMaintenanceTemplateNodesAppService;
            this._eccpDictMaintenanceItemsAppService = eccpDictMaintenanceItemsAppService;
        }

        /// <summary>
        /// The create or edit modal.
        /// </summary>
        /// <param name="maintenanceTemplateId">
        /// The maintenance template id.
        /// </param>
        /// <param name="parentNodeId">
        /// The parent node id.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpMvcAuthorize(
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int maintenanceTemplateId, int parentNodeId, int? id)
        {
            GetEccpMaintenanceTemplateNodeForEditOutput getEccpMaintenanceTemplateNodeForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceTemplateNodeForEditOutput =
                    await this._eccpMaintenanceTemplateNodesAppService.GetEccpMaintenanceTemplateNodeForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpMaintenanceTemplateNodeForEditOutput = new GetEccpMaintenanceTemplateNodeForEditOutput
                {
                    EccpMaintenanceTemplateNode =
                                                                          new
                                                                              CreateOrEditEccpMaintenanceTemplateNodeDto(),
                    eccpDictMaintenanceItemDtos =
                                                                          await this._eccpDictMaintenanceItemsAppService
                                                                              .GetMaintenanceItemTemplateNodeAll()
                };
            }

            var viewModel = new CreateOrEditEccpMaintenanceTemplateNodeViewModel
            {
                EccpMaintenanceTemplateNode =
                                        getEccpMaintenanceTemplateNodeForEditOutput.EccpMaintenanceTemplateNode,
                EccpMaintenanceNextNodeName =
                                        getEccpMaintenanceTemplateNodeForEditOutput.EccpMaintenanceNextNodeName,
                EccpDictNodeTypeName =
                                        getEccpMaintenanceTemplateNodeForEditOutput.EccpDictNodeTypeName,
                EccpDictMaintenanceItemDtos = getEccpMaintenanceTemplateNodeForEditOutput
                                        .eccpDictMaintenanceItemDtos,
                EccpMaintenanceSpareNodeName = getEccpMaintenanceTemplateNodeForEditOutput.EccpMaintenanceSpareNodeName
            };
            this.ViewBag.MaintenanceTemplateId = maintenanceTemplateId;
            this.ViewBag.ParentNodeId = parentNodeId;
            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp dict node type lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Edit)]
        public PartialViewResult EccpDictNodeTypeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpDictNodeTypeLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
            };
            return this.PartialView("_EccpDictNodeTypeLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp maintenance template lookup table modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <param name="maintenanceTemplateId">
        /// The maintenance template id.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        [AbpMvcAuthorize(
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Edit)]
        public PartialViewResult EccpMaintenanceTemplateLookupTableModal(
            int? id,
            string displayName,
            int maintenanceTemplateId)
        {
            var viewModel = new EccpMaintenanceNextNodeLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                MaintenanceTemplateId = maintenanceTemplateId,
                FilterText = string.Empty
            };
            return this.PartialView("_EccpMaintenanceNextNodeLookupTableModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpMaintenanceTemplateNodesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp maintenance template node modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpMaintenanceTemplateNodeModal(GetEccpMaintenanceTemplateNodeForView data)
        {
            var model = new EccpMaintenanceTemplateNodeViewModel
            {
                EccpMaintenanceTemplateNode = data.EccpMaintenanceTemplateNode,
                EccpMaintenanceNextNodeName = data.EccpMaintenanceNextNodeName,
                EccpDictNodeTypeName = data.EccpDictNodeTypeName,
                EccpMaintenanceSpareNodeName = data.EccpMaintenanceSpareNodeName
            };
            return this.PartialView("_ViewEccpMaintenanceTemplateNodeModal", model);
        }
    }
}