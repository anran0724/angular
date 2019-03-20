// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorksController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorks;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance works controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks)]
    public class EccpMaintenanceWorksController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp maintenance works app service.
        /// </summary>
        private readonly IEccpMaintenanceWorksAppService _eccpMaintenanceWorksAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorksController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceWorksAppService">
        /// The eccp maintenance works app service.
        /// </param>
        public EccpMaintenanceWorksController(IEccpMaintenanceWorksAppService eccpMaintenanceWorksAppService)
        {
            this._eccpMaintenanceWorksAppService = eccpMaintenanceWorksAppService;
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpMaintenanceWorkForEditOutput getEccpMaintenanceWorkForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceWorkForEditOutput =
                    await this._eccpMaintenanceWorksAppService.GetEccpMaintenanceWorkForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpMaintenanceWorkForEditOutput =
                    new GetEccpMaintenanceWorkForEditOutput
                        {
                            EccpMaintenanceWork = new CreateOrEditEccpMaintenanceWorkDto()
                        };
            }

            var viewModel = new CreateOrEditEccpMaintenanceWorkViewModel
                                {
                                    EccpMaintenanceWork = getEccpMaintenanceWorkForEditOutput.EccpMaintenanceWork,
                                    EccpMaintenanceWorkOrderPlanCheckDate =
                                        getEccpMaintenanceWorkForEditOutput.EccpMaintenanceWorkOrderPlanCheckDate,
                                    EccpMaintenanceTemplateNodeNodeName = getEccpMaintenanceWorkForEditOutput
                                        .EccpMaintenanceTemplateNodeNodeName
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks_Edit)]
        public PartialViewResult EccpMaintenanceTemplateNodeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpMaintenanceTemplateNodeLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpMaintenanceTemplateNodeLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp maintenance work order lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks_Edit)]
        public PartialViewResult EccpMaintenanceWorkOrderLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpMaintenanceWorkOrderLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpMaintenanceWorkOrderLookupTableModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpMaintenanceWorksViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp maintenance work modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpMaintenanceWorkModal(GetEccpMaintenanceWorkForView data)
        {
            var model = new EccpMaintenanceWorkViewModel
                            {
                                EccpMaintenanceWork = data.EccpMaintenanceWork,
                                EccpMaintenanceWorkOrderPlanCheckDate = data.EccpMaintenanceWorkOrderPlanCheckDate
                            };

            return this.PartialView("_ViewEccpMaintenanceWorkModal", model);
        }
    }
}