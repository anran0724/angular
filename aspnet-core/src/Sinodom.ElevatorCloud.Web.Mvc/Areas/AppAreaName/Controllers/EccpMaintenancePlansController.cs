// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenancePlansController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenancePlans;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance plans controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans)]
    public class EccpMaintenancePlansController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp maintenance plans app service.
        /// </summary>
        private readonly IEccpMaintenancePlansAppService _eccpMaintenancePlansAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenancePlansController"/> class.
        /// </summary>
        /// <param name="eccpMaintenancePlansAppService">
        /// The eccp maintenance plans app service.
        /// </param>
        public EccpMaintenancePlansController(IEccpMaintenancePlansAppService eccpMaintenancePlansAppService)
        {
            this._eccpMaintenancePlansAppService = eccpMaintenancePlansAppService;
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpMaintenancePlanForEditOutput getEccpMaintenancePlanForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenancePlanForEditOutput =
                    await this._eccpMaintenancePlansAppService.GetEccpMaintenancePlanForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpMaintenancePlanForEditOutput = await this._eccpMaintenancePlansAppService.GetEccpMaintenancePlanForCreate();
            }

            var viewModel = new CreateOrEditEccpMaintenancePlanViewModel
            {
                EccpMaintenancePlan = getEccpMaintenancePlanForEditOutput.EccpMaintenancePlan,
                MaintenanceTypes = getEccpMaintenancePlanForEditOutput.MaintenanceTypes,
                MaintenanceUserIds = getEccpMaintenancePlanForEditOutput.MaintenanceUserIds,
                MaintenanceUserNames = getEccpMaintenancePlanForEditOutput.MaintenanceUserNames,
                PropertyUserIds = getEccpMaintenancePlanForEditOutput.PropertyUserIds,
                PropertyUserNames = getEccpMaintenancePlanForEditOutput.PropertyUserNames,
                ElevatorIds = getEccpMaintenancePlanForEditOutput.ElevatorIds,
                ElevatorNames = getEccpMaintenancePlanForEditOutput.ElevatorNames,
                QuarterPollingPeriod = getEccpMaintenancePlanForEditOutput.QuarterPollingPeriod,
                HalfYearPollingPeriod = getEccpMaintenancePlanForEditOutput.HalfYearPollingPeriod,
                YearPollingPeriod = getEccpMaintenancePlanForEditOutput.YearPollingPeriod
            };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp base elevator lookup table modal.
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
        [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Create)]
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
        /// The eccp base elevators lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Edit)]
        public PartialViewResult EccpBaseElevatorsLookupTableModal(string id, string displayName)
        {
            var viewModel = new EccpBaseElevatorLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
            };

            return this.PartialView("_EccpBaseElevatorsLookupTableModal", viewModel);
        }

        /// <summary>
        /// The guide create modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Create)]
        public async Task<PartialViewResult> GuideCreateModal(int? id)
        {
            var getEccpMaintenancePlanForEditOutput = await this._eccpMaintenancePlansAppService.GetEccpMaintenancePlanForCreate();

            var viewModel = new CreateOrEditEccpMaintenancePlanViewModel
            {
                EccpMaintenancePlan = getEccpMaintenancePlanForEditOutput.EccpMaintenancePlan,
                MaintenanceTypes = getEccpMaintenancePlanForEditOutput.MaintenanceTypes,
                MaintenanceUserIds = getEccpMaintenancePlanForEditOutput.MaintenanceUserIds,
                MaintenanceUserNames = getEccpMaintenancePlanForEditOutput.MaintenanceUserNames,
                PropertyUserIds = getEccpMaintenancePlanForEditOutput.PropertyUserIds,
                PropertyUserNames = getEccpMaintenancePlanForEditOutput.PropertyUserNames,
                ElevatorIds = getEccpMaintenancePlanForEditOutput.ElevatorIds,
                ElevatorNames = getEccpMaintenancePlanForEditOutput.ElevatorNames,
                QuarterPollingPeriod = getEccpMaintenancePlanForEditOutput.QuarterPollingPeriod,
                HalfYearPollingPeriod = getEccpMaintenancePlanForEditOutput.HalfYearPollingPeriod,
                YearPollingPeriod = getEccpMaintenancePlanForEditOutput.YearPollingPeriod
            };

            return this.PartialView("_GuideCreateModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpMaintenancePlansViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The maintenance templates lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Edit)]
        public PartialViewResult MaintenanceTemplatesLookupTableModal(string id, string displayName)
        {
            var viewModel = new MaintenanceTemplatesLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
            };

            return this.PartialView("_MaintenanceTemplatesLookupTableModal", viewModel);
        }

        /// <summary>
        /// The maintenance user lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Edit)]
        public PartialViewResult MaintenanceUserLookupTableModal(string id, string displayName)
        {
            var viewModel = new MaintenanceUserLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
            };

            return this.PartialView("_MaintenanceUserLookupTableModal", viewModel);
        }

        /// <summary>
        /// The property user lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Edit)]
        public PartialViewResult PropertyUserLookupTableModal(string id, string displayName)
        {
            var viewModel = new PropertyUserLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
            };

            return this.PartialView("_PropertyUserLookupTableModal", viewModel);
        }

        /// <summary>
        /// The view eccp maintenance plan modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public async Task<PartialViewResult> ViewEccpMaintenancePlanModal(GetEccpMaintenancePlanForView data)
        {
            var getEccpMaintenancePlanForEditOutput = await this._eccpMaintenancePlansAppService.GetEccpMaintenancePlanForView(
                new EntityDto { Id = (int)data.EccpMaintenancePlan.Id });

            var model = new EccpMaintenancePlanViewModel
            {
                EccpMaintenancePlan = getEccpMaintenancePlanForEditOutput.EccpMaintenancePlan,
                ElevatorNames = getEccpMaintenancePlanForEditOutput.ElevatorNames,
                MaintenanceUserNames = getEccpMaintenancePlanForEditOutput.MaintenanceUserNames,
                EccpBaseElevatorNum = data.EccpBaseElevatorNum,
                IsClose = data.EccpMaintenancePlan.IsClose
            };

            return this.PartialView("_ViewEccpMaintenancePlanModal", model);
        }

        public PartialViewResult EccpMaintenanceWorkOrderPlansTableModal()
        {
            return this.PartialView("_EccpMaintenanceWorkOrderPlansTableModal");
        }
    }
}