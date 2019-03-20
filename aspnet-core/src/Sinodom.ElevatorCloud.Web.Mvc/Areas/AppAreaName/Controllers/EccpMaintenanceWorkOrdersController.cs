// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrdersController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance work orders controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders)]
    public class EccpMaintenanceWorkOrdersController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp maintenance work orders app service.
        /// </summary>
        private readonly IEccpMaintenanceWorkOrdersAppService _eccpMaintenanceWorkOrdersAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkOrdersController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceWorkOrdersAppService">
        /// The eccp maintenance work orders app service.
        /// </param>
        public EccpMaintenanceWorkOrdersController(
            IEccpMaintenanceWorkOrdersAppService eccpMaintenanceWorkOrdersAppService)
        {
            this._eccpMaintenanceWorkOrdersAppService = eccpMaintenanceWorkOrdersAppService;
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpMaintenanceWorkOrderForEditOutput getEccpMaintenanceWorkOrderForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceWorkOrderForEditOutput =
                    await this._eccpMaintenanceWorkOrdersAppService.GetEccpMaintenanceWorkOrderForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpMaintenanceWorkOrderForEditOutput =
                    new GetEccpMaintenanceWorkOrderForEditOutput
                    {
                        EccpMaintenanceWorkOrder = new CreateOrEditEccpMaintenanceWorkOrderDto()
                    };
            }

            var viewModel = new CreateOrEditEccpMaintenanceWorkOrderViewModel
            {
                EccpMaintenanceWorkOrder =
                                        getEccpMaintenanceWorkOrderForEditOutput.EccpMaintenanceWorkOrder,
                EccpMaintenancePlanPollingPeriod =
                                        getEccpMaintenanceWorkOrderForEditOutput.EccpMaintenancePlanPollingPeriod,
                EccpDictMaintenanceTypeName =
                                        getEccpMaintenanceWorkOrderForEditOutput.EccpDictMaintenanceTypeName,
                EccpDictMaintenanceStatusName = getEccpMaintenanceWorkOrderForEditOutput
                                        .EccpDictMaintenanceStatusName
            };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp dict maintenance status lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit)]
        public PartialViewResult EccpDictMaintenanceStatusLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpDictMaintenanceStatusLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
            };

            return this.PartialView("_EccpDictMaintenanceStatusLookupTableModal", viewModel);
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit)]
        public PartialViewResult EccpDictMaintenanceTypeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpDictMaintenanceTypeLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
            };

            return this.PartialView("_EccpDictMaintenanceTypeLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp maintenance plan lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit)]
        public PartialViewResult EccpMaintenancePlanLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpMaintenancePlanLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
            };

            return this.PartialView("_EccpMaintenancePlanLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp maintenance work order evaluations table modal.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult EccpMaintenanceWorkOrderEvaluationsTableModal()
        {
            return this.PartialView("_EccpMaintenanceWorkOrderEvaluationsTableModal");
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpMaintenanceWorkOrdersViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp maintenance work order modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public async Task<PartialViewResult> ViewEccpMaintenanceWorkOrderModal(GetEccpMaintenanceWorkOrderForView data)
        {
            var getEccpBaseElevatorsInfoDto = await this._eccpMaintenanceWorkOrdersAppService.GetEccpBaseElevatorsInfoByPlanId(data.EccpMaintenanceWorkOrder.MaintenancePlanId, data.EccpMaintenanceWorkOrder.Id);

            var getEccpMaintenanceWorkFlows = await this._eccpMaintenanceWorkOrdersAppService.GetAllEccpMaintenanceWorkFlowsByWorkOrderId(
                data.EccpMaintenanceWorkOrder.Id);

            var model = new EccpMaintenanceWorkOrderViewModel
            {
                EccpMaintenanceWorkOrder = data.EccpMaintenanceWorkOrder,
                EccpMaintenancePlanPollingPeriod = data.EccpMaintenancePlanPollingPeriod,
                EccpDictMaintenanceTypeName = data.EccpDictMaintenanceTypeName,
                EccpDictMaintenanceStatusName = data.EccpDictMaintenanceStatusName,
                EccpMaintenanceUserNameList = data.EccpMaintenanceUserNameList,
                EccpElevatorName = data.EccpElevatorName,
                EccpBaseElevatorsInfo = getEccpBaseElevatorsInfoDto,
                EccpMaintenanceWorkFlowsList = getEccpMaintenanceWorkFlows
            };

            return this.PartialView("_ViewEccpMaintenanceWorkOrderModal", model);
        }
    }
}