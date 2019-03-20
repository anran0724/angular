// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkFlowsController.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.AspNetCore.Mvc.Authorization;

    using Microsoft.AspNetCore.Mvc;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorkFlows;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance work flows controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows)]
    public class EccpMaintenanceWorkFlowsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp maintenance work flows app service.
        /// </summary>
        private readonly IEccpMaintenanceWorkFlowsAppService _eccpMaintenanceWorkFlowsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkFlowsController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceWorkFlowsAppService">
        /// The eccp maintenance work flows app service.
        /// </param>
        public EccpMaintenanceWorkFlowsController(
            IEccpMaintenanceWorkFlowsAppService eccpMaintenanceWorkFlowsAppService)
        {
            this._eccpMaintenanceWorkFlowsAppService = eccpMaintenanceWorkFlowsAppService;
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(Guid? id)
        {
            GetEccpMaintenanceWorkFlowForEditOutput getEccpMaintenanceWorkFlowForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceWorkFlowForEditOutput =
                    await this._eccpMaintenanceWorkFlowsAppService.GetEccpMaintenanceWorkFlowForEdit(
                        new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getEccpMaintenanceWorkFlowForEditOutput =
                    new GetEccpMaintenanceWorkFlowForEditOutput
                        {
                            EccpMaintenanceWorkFlow = new CreateOrEditEccpMaintenanceWorkFlowDto()
                        };
            }

            var viewModel = new CreateOrEditEccpMaintenanceWorkFlowViewModel
                                {
                                    EccpMaintenanceWorkFlow =
                                        getEccpMaintenanceWorkFlowForEditOutput.EccpMaintenanceWorkFlow,
                                    EccpMaintenanceTemplateNodeNodeName =
                                        getEccpMaintenanceWorkFlowForEditOutput.EccpMaintenanceTemplateNodeNodeName,
                                    EccpMaintenanceWorkTaskName =
                                        getEccpMaintenanceWorkFlowForEditOutput.EccpMaintenanceWorkTaskName,
                                    EccpDictMaintenanceWorkFlowStatusName = getEccpMaintenanceWorkFlowForEditOutput
                                        .EccpDictMaintenanceWorkFlowStatusName
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp dict maintenance work flow status lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Edit)]
        public PartialViewResult EccpDictMaintenanceWorkFlowStatusLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpDictMaintenanceWorkFlowStatusLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpDictMaintenanceWorkFlowStatusLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp maintenance template node lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Edit)]
        public PartialViewResult EccpMaintenanceTemplateNodeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpMaintenanceTemplateNodeLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpMaintenanceTemplateNodeLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp maintenance work lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Edit)]
        public PartialViewResult EccpMaintenanceWorkLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpMaintenanceWorkLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpMaintenanceWorkLookupTableModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpMaintenanceWorkFlowsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp maintenance work flow modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpMaintenanceWorkFlowModal(GetEccpMaintenanceWorkFlowForView data)
        {
            var model = new EccpMaintenanceWorkFlowViewModel
                            {
                                EccpMaintenanceWorkFlow = data.EccpMaintenanceWorkFlow,
                                EccpMaintenanceTemplateNodeNodeName = data.EccpMaintenanceTemplateNodeNodeName,
                                EccpMaintenanceWorkTaskName = data.EccpMaintenanceWorkTaskName,
                                EccpDictMaintenanceWorkFlowStatusName = data.EccpDictMaintenanceWorkFlowStatusName
                            };

            return this.PartialView("_ViewEccpMaintenanceWorkFlowModal", model);
        }
    }
}