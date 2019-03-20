// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElevatorClaimLogsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ElevatorClaimLogs;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The elevator claim logs controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpElevator_ElevatorClaimLogs)]
    public class ElevatorClaimLogsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _elevator claim logs app service.
        /// </summary>
        private readonly IElevatorClaimLogsAppService _elevatorClaimLogsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElevatorClaimLogsController"/> class.
        /// </summary>
        /// <param name="elevatorClaimLogsAppService">
        /// The elevator claim logs app service.
        /// </param>
        public ElevatorClaimLogsController(IElevatorClaimLogsAppService elevatorClaimLogsAppService)
        {
            this._elevatorClaimLogsAppService = elevatorClaimLogsAppService;
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
            AppPermissions.Pages_EccpElevator_ElevatorClaimLogs_Create,
            AppPermissions.Pages_EccpElevator_ElevatorClaimLogs_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetElevatorClaimLogForEditOutput getElevatorClaimLogForEditOutput;

            if (id.HasValue)
            {
                getElevatorClaimLogForEditOutput =
                    await this._elevatorClaimLogsAppService.GetElevatorClaimLogForEdit(
                        new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getElevatorClaimLogForEditOutput =
                    new GetElevatorClaimLogForEditOutput { ElevatorClaimLog = new CreateOrEditElevatorClaimLogDto() };
            }

            var viewModel = new CreateOrEditElevatorClaimLogViewModel
                                {
                                    ElevatorClaimLog = getElevatorClaimLogForEditOutput.ElevatorClaimLog,
                                    EccpBaseElevatorName = getElevatorClaimLogForEditOutput.EccpBaseElevatorName
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
        [AbpMvcAuthorize(
            AppPermissions.Pages_EccpElevator_ElevatorClaimLogs_Create,
            AppPermissions.Pages_EccpElevator_ElevatorClaimLogs_Edit)]
        public PartialViewResult EccpBaseElevatorLookupTableModal(Guid? id, string displayName)
        {
            var viewModel = new EccpBaseElevatorLookupTableViewModel
                                {
                                    Id = id.ToString(), DisplayName = displayName, FilterText = string.Empty
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
            var model = new ElevatorClaimLogsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view elevator claim log modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewElevatorClaimLogModal(GetElevatorClaimLogForView data)
        {
            var model = new ElevatorClaimLogViewModel
                            {
                                ElevatorClaimLog = data.ElevatorClaimLog,
                                EccpBaseElevatorName = data.EccpBaseElevatorName
                            };

            return this.PartialView("_ViewElevatorClaimLogModal", model);
        }
    }
}