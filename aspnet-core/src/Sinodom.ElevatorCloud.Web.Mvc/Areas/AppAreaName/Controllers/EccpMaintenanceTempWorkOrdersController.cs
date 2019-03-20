// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTempWorkOrdersController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceTempWorkOrders;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance temp work orders controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders)]
    public class EccpMaintenanceTempWorkOrdersController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp maintenance temp work orders app service.
        /// </summary>
        private readonly IEccpMaintenanceTempWorkOrdersAppService _eccpMaintenanceTempWorkOrdersAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTempWorkOrdersController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceTempWorkOrdersAppService">
        /// The eccp maintenance temp work orders app service.
        /// </param>
        public EccpMaintenanceTempWorkOrdersController(
            IEccpMaintenanceTempWorkOrdersAppService eccpMaintenanceTempWorkOrdersAppService)
        {
            this._eccpMaintenanceTempWorkOrdersAppService = eccpMaintenanceTempWorkOrdersAppService;
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(Guid? id)
        {
            GetEccpMaintenanceTempWorkOrderForEditOutput getEccpMaintenanceTempWorkOrderForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceTempWorkOrderForEditOutput =
                    await this._eccpMaintenanceTempWorkOrdersAppService.GetEccpMaintenanceTempWorkOrderForEdit(
                        new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getEccpMaintenanceTempWorkOrderForEditOutput = new GetEccpMaintenanceTempWorkOrderForEditOutput
                {
                    EccpMaintenanceTempWorkOrder =
                                                                           new
                                                                               CreateOrEditEccpMaintenanceTempWorkOrderDto()
                };
            }

            var viewModel = new CreateOrEditEccpMaintenanceTempWorkOrderViewModel
            {
                EccpMaintenanceTempWorkOrder =
                                        getEccpMaintenanceTempWorkOrderForEditOutput.EccpMaintenanceTempWorkOrder,
                EccpDictTempWorkOrderTypeName =
                                        getEccpMaintenanceTempWorkOrderForEditOutput.EccpDictTempWorkOrderTypeName,
                UserName = getEccpMaintenanceTempWorkOrderForEditOutput.UserName,
                EccpBaseElevatorName = getEccpMaintenanceTempWorkOrderForEditOutput.EccpBaseElevatorName
            };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp dict temp work order type lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit)]
        public PartialViewResult EccpDictTempWorkOrderTypeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ECCPDictTempWorkOrderTypeLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
            };

            return this.PartialView("_ECCPDictTempWorkOrderTypeLookupTableModel", viewModel);
        }

        [AbpMvcAuthorize(
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit)]
        public PartialViewResult EccpBaseElevatorLookupTableModal(string id, string displayName)
        {
            var viewModel = new EccpBaseElevatorLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
            };

            return this.PartialView("_EccpBaseElevatorLookupTableModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpMaintenanceTempWorkOrdersViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The user lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit)]
        public PartialViewResult UserLookupTableModal(long? id, string displayName)
        {
            var viewModel = new UserLookupTableViewModel { Id = id, DisplayName = displayName, FilterText = string.Empty };

            return this.PartialView("_UserLookupTableModal", viewModel);
        }

        /// <summary>
        /// The view eccp maintenance temp work order modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpMaintenanceTempWorkOrderModal(GetEccpMaintenanceTempWorkOrderForView data)
        {
            var model = new EccpMaintenanceTempWorkOrderViewModel
            {
                EccpMaintenanceTempWorkOrder = data.EccpMaintenanceTempWorkOrder,
                ECCPBaseMaintenanceCompanyName = data.ECCPBaseMaintenanceCompanyName,
                UserName = data.UserName,
                EccpBaseElevatorName = data.EccpBaseElevatorName
            };

            return this.PartialView("_ViewEccpMaintenanceTempWorkOrderModal", model);
        }
    }
}