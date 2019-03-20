// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorLabelBindLogsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.EccpBaseElevatorLabels;
    using Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseElevatorLabelBindLogs;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp base elevator label bind logs controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs)]
    public class EccpBaseElevatorLabelBindLogsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp base elevator label bind logs app service.
        /// </summary>
        private readonly IEccpBaseElevatorLabelBindLogsAppService _eccpBaseElevatorLabelBindLogsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorLabelBindLogsController"/> class.
        /// </summary>
        /// <param name="eccpBaseElevatorLabelBindLogsAppService">
        /// The eccp base elevator label bind logs app service.
        /// </param>
        public EccpBaseElevatorLabelBindLogsController(
            IEccpBaseElevatorLabelBindLogsAppService eccpBaseElevatorLabelBindLogsAppService)
        {
            this._eccpBaseElevatorLabelBindLogsAppService = eccpBaseElevatorLabelBindLogsAppService;
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetEccpBaseElevatorLabelBindLogForEditOutput getEccpBaseElevatorLabelBindLogForEditOutput;

            if (id.HasValue)
            {
                getEccpBaseElevatorLabelBindLogForEditOutput =
                    await this._eccpBaseElevatorLabelBindLogsAppService.GetEccpBaseElevatorLabelBindLogForEdit(
                        new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getEccpBaseElevatorLabelBindLogForEditOutput = new GetEccpBaseElevatorLabelBindLogForEditOutput
                                                                   {
                                                                       EccpBaseElevatorLabelBindLog =
                                                                           new
                                                                               CreateOrEditEccpBaseElevatorLabelBindLogDto()
                                                                   };
            }

            var viewModel = new CreateOrEditEccpBaseElevatorLabelBindLogViewModel
                                {
                                    EccpBaseElevatorLabelBindLog =
                                        getEccpBaseElevatorLabelBindLogForEditOutput.EccpBaseElevatorLabelBindLog,
                                    EccpBaseElevatorName =
                                        getEccpBaseElevatorLabelBindLogForEditOutput.EccpBaseElevatorName,
                                    EccpDictLabelStatusName = getEccpBaseElevatorLabelBindLogForEditOutput
                                        .EccpDictLabelStatusName
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs_Edit)]
        public PartialViewResult EccpBaseElevatorLookupTableModal(Guid? id, string displayName)
        {
            var viewModel = new EccpBaseElevatorLookupTableViewModel
                                {
                                    Id = id.ToString(), DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpBaseElevatorLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp dict label status lookup table modal.
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs_Edit)]
        public PartialViewResult EccpDictLabelStatusLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpDictLabelStatusLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpDictLabelStatusLookupTableModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpBaseElevatorLabelBindLogsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp base elevator label bind log modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpBaseElevatorLabelBindLogModal(GetEccpBaseElevatorLabelBindLogForView data)
        {
            var model = new EccpBaseElevatorLabelBindLogViewModel
                            {
                                EccpBaseElevatorLabelBindLog = data.EccpBaseElevatorLabelBindLog,
                                EccpBaseElevatorName = data.EccpBaseElevatorName,
                                EccpDictLabelStatusName = data.EccpDictLabelStatusName
                            };

            return this.PartialView("_ViewEccpBaseElevatorLabelBindLogModal", model);
        }
    }
}