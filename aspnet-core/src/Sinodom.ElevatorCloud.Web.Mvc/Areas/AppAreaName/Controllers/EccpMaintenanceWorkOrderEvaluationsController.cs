// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrderEvaluationsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorkOrderEvaluations;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance work order evaluations controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations)]
    public class EccpMaintenanceWorkOrderEvaluationsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp maintenance work order evaluations app service.
        /// </summary>
        private readonly IEccpMaintenanceWorkOrderEvaluationsAppService _eccpMaintenanceWorkOrderEvaluationsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkOrderEvaluationsController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceWorkOrderEvaluationsAppService">
        /// The eccp maintenance work order evaluations app service.
        /// </param>
        public EccpMaintenanceWorkOrderEvaluationsController(
            IEccpMaintenanceWorkOrderEvaluationsAppService eccpMaintenanceWorkOrderEvaluationsAppService)
        {
            this._eccpMaintenanceWorkOrderEvaluationsAppService = eccpMaintenanceWorkOrderEvaluationsAppService;
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpMaintenanceWorkOrderEvaluationForEditOutput getEccpMaintenanceWorkOrderEvaluationForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceWorkOrderEvaluationForEditOutput =
                    await this._eccpMaintenanceWorkOrderEvaluationsAppService
                        .GetEccpMaintenanceWorkOrderEvaluationForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpMaintenanceWorkOrderEvaluationForEditOutput =
                    new GetEccpMaintenanceWorkOrderEvaluationForEditOutput
                        {
                            EccpMaintenanceWorkOrderEvaluation = new CreateOrEditEccpMaintenanceWorkOrderEvaluationDto()
                        };
            }

            var viewModel = new CreateOrEditEccpMaintenanceWorkOrderEvaluationViewModel
                                {
                                    EccpMaintenanceWorkOrderEvaluation =
                                        getEccpMaintenanceWorkOrderEvaluationForEditOutput
                                            .EccpMaintenanceWorkOrderEvaluation,
                                    EccpMaintenanceWorkOrderRemark = getEccpMaintenanceWorkOrderEvaluationForEditOutput
                                        .EccpMaintenanceWorkOrderRemark
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations_Edit)]
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
            var model = new EccpMaintenanceWorkOrderEvaluationsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp maintenance work order evaluation modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpMaintenanceWorkOrderEvaluationModal(
            GetEccpMaintenanceWorkOrderEvaluationForView data)
        {
            var model = new EccpMaintenanceWorkOrderEvaluationViewModel
                            {
                                EccpMaintenanceWorkOrderEvaluation = data.EccpMaintenanceWorkOrderEvaluation
                            };

            return this.PartialView("_ViewEccpMaintenanceWorkOrderEvaluationModal", model);
        }
    }
}