// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTempWorkOrderActionLogsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceTempWorkOrderActionLogs;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance temp work order action logs controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs)]
    public class EccpMaintenanceTempWorkOrderActionLogsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp maintenance temp work order action logs app service.
        /// </summary>
        private readonly IEccpMaintenanceTempWorkOrderActionLogsAppService
            _eccpMaintenanceTempWorkOrderActionLogsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTempWorkOrderActionLogsController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceTempWorkOrderActionLogsAppService">
        /// The eccp maintenance temp work order action logs app service.
        /// </param>
        public EccpMaintenanceTempWorkOrderActionLogsController(
            IEccpMaintenanceTempWorkOrderActionLogsAppService eccpMaintenanceTempWorkOrderActionLogsAppService)
        {
            this._eccpMaintenanceTempWorkOrderActionLogsAppService = eccpMaintenanceTempWorkOrderActionLogsAppService;
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(Guid? id)
        {
            GetEccpMaintenanceTempWorkOrderActionLogForEditOutput getEccpMaintenanceTempWorkOrderActionLogForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceTempWorkOrderActionLogForEditOutput =
                    await this._eccpMaintenanceTempWorkOrderActionLogsAppService
                        .GetEccpMaintenanceTempWorkOrderActionLogForEdit(new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getEccpMaintenanceTempWorkOrderActionLogForEditOutput =
                    new GetEccpMaintenanceTempWorkOrderActionLogForEditOutput
                        {
                            EccpMaintenanceTempWorkOrderActionLog =
                                new CreateOrEditEccpMaintenanceTempWorkOrderActionLogDto()
                        };
            }

            var viewModel = new CreateOrEditEccpMaintenanceTempWorkOrderActionLogViewModel
                                {
                                    EccpMaintenanceTempWorkOrderActionLog =
                                        getEccpMaintenanceTempWorkOrderActionLogForEditOutput
                                            .EccpMaintenanceTempWorkOrderActionLog,
                                    EccpMaintenanceTempWorkOrderTitle =
                                        getEccpMaintenanceTempWorkOrderActionLogForEditOutput
                                            .EccpMaintenanceTempWorkOrderTitle,
                                    UserName = getEccpMaintenanceTempWorkOrderActionLogForEditOutput.UserName
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp maintenance temp work order lookup table modal.
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs_Edit)]
        public PartialViewResult EccpMaintenanceTempWorkOrderLookupTableModal(Guid? id, string displayName)
        {
            var viewModel = new EccpMaintenanceTempWorkOrderLookupTableViewModel
                                {
                                    Id = id.ToString(), DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpMaintenanceTempWorkOrderLookupTableModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpMaintenanceTempWorkOrderActionLogsViewModel { FilterText = string.Empty };

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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs_Edit)]
        public PartialViewResult UserLookupTableModal(long? id, string displayName)
        {
            var viewModel = new UserLookupTableViewModel { Id = id, DisplayName = displayName, FilterText = string.Empty };

            return this.PartialView("_UserLookupTableModal", viewModel);
        }

        /// <summary>
        /// The view eccp maintenance temp work order action log modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpMaintenanceTempWorkOrderActionLogModal(
            GetEccpMaintenanceTempWorkOrderActionLogForView data)
        {
            var model = new EccpMaintenanceTempWorkOrderActionLogViewModel
                            {
                                EccpMaintenanceTempWorkOrderActionLog = data.EccpMaintenanceTempWorkOrderActionLog,
                                EccpMaintenanceTempWorkOrderTitle = data.EccpMaintenanceTempWorkOrderTitle,
                                UserName = data.UserName
                            };

            return this.PartialView("_ViewEccpMaintenanceTempWorkOrderActionLogModal", model);
        }
    }
}