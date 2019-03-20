// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpElevatorChangeLogsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpElevatorChangeLogs;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp elevator change logs controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs)]
    public class EccpElevatorChangeLogsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp elevator change logs app service.
        /// </summary>
        private readonly IEccpElevatorChangeLogsAppService _eccpElevatorChangeLogsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpElevatorChangeLogsController"/> class.
        /// </summary>
        /// <param name="eccpElevatorChangeLogsAppService">
        /// The eccp elevator change logs app service.
        /// </param>
        public EccpElevatorChangeLogsController(IEccpElevatorChangeLogsAppService eccpElevatorChangeLogsAppService)
        {
            this._eccpElevatorChangeLogsAppService = eccpElevatorChangeLogsAppService;
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
            AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs_Create,
            AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpElevatorChangeLogForEditOutput getEccpElevatorChangeLogForEditOutput;

            if (id.HasValue)
            {
                getEccpElevatorChangeLogForEditOutput =
                    await this._eccpElevatorChangeLogsAppService.GetEccpElevatorChangeLogForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpElevatorChangeLogForEditOutput =
                    new GetEccpElevatorChangeLogForEditOutput
                        {
                            EccpElevatorChangeLog = new CreateOrEditEccpElevatorChangeLogDto()
                        };
            }

            var viewModel = new CreateOrEditEccpElevatorChangeLogViewModel
                                {
                                    EccpElevatorChangeLog = getEccpElevatorChangeLogForEditOutput.EccpElevatorChangeLog,
                                    EccpBaseElevatorName = getEccpElevatorChangeLogForEditOutput.EccpBaseElevatorName
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
            AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs_Create,
            AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs_Edit)]
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
            var model = new EccpElevatorChangeLogsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp elevator change log modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpElevatorChangeLogModal(GetEccpElevatorChangeLogForView data)
        {
            var model = new EccpElevatorChangeLogViewModel
                            {
                                EccpElevatorChangeLog = data.EccpElevatorChangeLog,
                                EccpBaseElevatorName = data.EccpBaseElevatorName
                            };

            return this.PartialView("_ViewEccpElevatorChangeLogModal", model);
        }
    }
}