// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkLogsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorkLogs;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance work logs controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkLogs)]
    public class EccpMaintenanceWorkLogsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp maintenance work logs app service.
        /// </summary>
        private readonly IEccpMaintenanceWorkLogsAppService _eccpMaintenanceWorkLogsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkLogsController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceWorkLogsAppService">
        /// The eccp maintenance work logs app service.
        /// </param>
        public EccpMaintenanceWorkLogsController(IEccpMaintenanceWorkLogsAppService eccpMaintenanceWorkLogsAppService)
        {
            this._eccpMaintenanceWorkLogsAppService = eccpMaintenanceWorkLogsAppService;
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
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkLogs_Create,
            AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkLogs_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetEccpMaintenanceWorkLogForEditOutput getEccpMaintenanceWorkLogForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceWorkLogForEditOutput =
                    await this._eccpMaintenanceWorkLogsAppService.GetEccpMaintenanceWorkLogForEdit(
                        new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getEccpMaintenanceWorkLogForEditOutput =
                    new GetEccpMaintenanceWorkLogForEditOutput
                        {
                            EccpMaintenanceWorkLog = new CreateOrEditEccpMaintenanceWorkLogDto()
                        };
            }

            var viewModel = new CreateOrEditEccpMaintenanceWorkLogViewModel
                                {
                                    EccpMaintenanceWorkLog =
                                        getEccpMaintenanceWorkLogForEditOutput.EccpMaintenanceWorkLog
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpMaintenanceWorkLogsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp maintenance work log modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpMaintenanceWorkLogModal(GetEccpMaintenanceWorkLogForView data)
        {
            var model = new EccpMaintenanceWorkLogViewModel { EccpMaintenanceWorkLog = data.EccpMaintenanceWorkLog };

            return this.PartialView("_ViewEccpMaintenanceWorkLogModal", model);
        }
    }
}