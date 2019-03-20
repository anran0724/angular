// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTroubledWorkOrdersController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceTroubledWorkOrders;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance troubled work orders controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders)]
    public class EccpMaintenanceTroubledWorkOrdersController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp maintenance troubled work orders app service.
        /// </summary>
        private readonly IEccpMaintenanceTroubledWorkOrdersAppService _eccpMaintenanceTroubledWorkOrdersAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTroubledWorkOrdersController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceTroubledWorkOrdersAppService">
        /// The eccp maintenance troubled work orders app service.
        /// </param>
        public EccpMaintenanceTroubledWorkOrdersController(
            IEccpMaintenanceTroubledWorkOrdersAppService eccpMaintenanceTroubledWorkOrdersAppService)
        {
            this._eccpMaintenanceTroubledWorkOrdersAppService = eccpMaintenanceTroubledWorkOrdersAppService;
        }

        /// <summary>
        /// The audit modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Audit)]
        public async Task<PartialViewResult> AuditModal(int? id)
        {
            GetEccpMaintenanceTroubledWorkOrderForEditOutput getEccpMaintenanceTroubledWorkOrderForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceTroubledWorkOrderForEditOutput =
                    await this._eccpMaintenanceTroubledWorkOrdersAppService.GetEccpMaintenanceTroubledWorkOrderForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpMaintenanceTroubledWorkOrderForEditOutput = new GetEccpMaintenanceTroubledWorkOrderForEditOutput
                                                                       {
                                                                           EccpMaintenanceTroubledWorkOrder =
                                                                               new
                                                                                   CreateOrEditEccpMaintenanceTroubledWorkOrderDto()
                                                                       };
            }

            var viewModel = new CreateOrEditEccpMaintenanceTroubledWorkOrderViewModel
                                {
                                    EccpMaintenanceTroubledWorkOrder =
                                        getEccpMaintenanceTroubledWorkOrderForEditOutput
                                            .EccpMaintenanceTroubledWorkOrder,
                                    EccpMaintenanceWorkOrderRemark = getEccpMaintenanceTroubledWorkOrderForEditOutput
                                        .EccpMaintenanceWorkOrderRemark
                                };

            return this.PartialView("_AuditModal", viewModel);
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpMaintenanceTroubledWorkOrderForEditOutput getEccpMaintenanceTroubledWorkOrderForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceTroubledWorkOrderForEditOutput =
                    await this._eccpMaintenanceTroubledWorkOrdersAppService.GetEccpMaintenanceTroubledWorkOrderForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpMaintenanceTroubledWorkOrderForEditOutput = new GetEccpMaintenanceTroubledWorkOrderForEditOutput
                                                                       {
                                                                           EccpMaintenanceTroubledWorkOrder =
                                                                               new
                                                                                   CreateOrEditEccpMaintenanceTroubledWorkOrderDto()
                                                                       };
            }

            var viewModel = new CreateOrEditEccpMaintenanceTroubledWorkOrderViewModel
                                {
                                    EccpMaintenanceTroubledWorkOrder =
                                        getEccpMaintenanceTroubledWorkOrderForEditOutput
                                            .EccpMaintenanceTroubledWorkOrder,
                                    EccpMaintenanceWorkOrderRemark = getEccpMaintenanceTroubledWorkOrderForEditOutput
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Edit)]
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
            var model = new EccpMaintenanceTroubledWorkOrdersViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp maintenance troubled work order modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpMaintenanceTroubledWorkOrderModal(
            GetEccpMaintenanceTroubledWorkOrderForView data)
        {
            var model = new EccpMaintenanceTroubledWorkOrderViewModel
                            {
                                EccpMaintenanceTroubledWorkOrder = data.EccpMaintenanceTroubledWorkOrder,
                                EccpMaintenanceWorkOrderRemark = data.EccpMaintenanceWorkOrderRemark
                            };

            return this.PartialView("_ViewEccpMaintenanceTroubledWorkOrderModal", model);
        }
    }
}