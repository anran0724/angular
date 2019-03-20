// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceStatusesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpDict.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictMaintenanceStatuses;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp dict maintenance statuses controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceStatuses)]
    public class EccpDictMaintenanceStatusesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp dict maintenance statuses app service.
        /// </summary>
        private readonly IEccpDictMaintenanceStatusesAppService _eccpDictMaintenanceStatusesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictMaintenanceStatusesController"/> class.
        /// </summary>
        /// <param name="eccpDictMaintenanceStatusesAppService">
        /// The eccp dict maintenance statuses app service.
        /// </param>
        public EccpDictMaintenanceStatusesController(
            IEccpDictMaintenanceStatusesAppService eccpDictMaintenanceStatusesAppService)
        {
            this._eccpDictMaintenanceStatusesAppService = eccpDictMaintenanceStatusesAppService;
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
            AppPermissions.Pages_EccpDict_EccpDictMaintenanceStatuses_Create,
            AppPermissions.Pages_EccpDict_EccpDictMaintenanceStatuses_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpDictMaintenanceStatusForEditOutput getEccpDictMaintenanceStatusForEditOutput;

            if (id.HasValue)
            {
                getEccpDictMaintenanceStatusForEditOutput =
                    await this._eccpDictMaintenanceStatusesAppService.GetEccpDictMaintenanceStatusForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpDictMaintenanceStatusForEditOutput =
                    new GetEccpDictMaintenanceStatusForEditOutput
                        {
                            EccpDictMaintenanceStatus = new CreateOrEditEccpDictMaintenanceStatusDto()
                        };
            }

            var viewModel = new CreateOrEditEccpDictMaintenanceStatusViewModel
                                {
                                    EccpDictMaintenanceStatus = getEccpDictMaintenanceStatusForEditOutput
                                        .EccpDictMaintenanceStatus
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
            var model = new EccpDictMaintenanceStatusesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp dict maintenance status modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpDictMaintenanceStatusModal(GetEccpDictMaintenanceStatusForView data)
        {
            var model = new EccpDictMaintenanceStatusViewModel
                            {
                                EccpDictMaintenanceStatus = data.EccpDictMaintenanceStatus
                            };

            return this.PartialView("_ViewEccpDictMaintenanceStatusModal", model);
        }
    }
}