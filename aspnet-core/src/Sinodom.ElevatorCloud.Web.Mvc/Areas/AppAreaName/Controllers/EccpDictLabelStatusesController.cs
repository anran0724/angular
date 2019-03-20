// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictLabelStatusesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictLabelStatuses;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp dict label statuses controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpDict_EccpDictLabelStatuses)]
    public class EccpDictLabelStatusesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp dict label statuses app service.
        /// </summary>
        private readonly IEccpDictLabelStatusesAppService _eccpDictLabelStatusesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictLabelStatusesController"/> class.
        /// </summary>
        /// <param name="eccpDictLabelStatusesAppService">
        /// The eccp dict label statuses app service.
        /// </param>
        public EccpDictLabelStatusesController(IEccpDictLabelStatusesAppService eccpDictLabelStatusesAppService)
        {
            this._eccpDictLabelStatusesAppService = eccpDictLabelStatusesAppService;
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
            AppPermissions.Pages_EccpDict_EccpDictLabelStatuses_Create,
            AppPermissions.Pages_EccpDict_EccpDictLabelStatuses_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpDictLabelStatusForEditOutput getEccpDictLabelStatusForEditOutput;

            if (id.HasValue)
            {
                getEccpDictLabelStatusForEditOutput =
                    await this._eccpDictLabelStatusesAppService.GetEccpDictLabelStatusForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpDictLabelStatusForEditOutput =
                    new GetEccpDictLabelStatusForEditOutput
                        {
                            EccpDictLabelStatus = new CreateOrEditEccpDictLabelStatusDto()
                        };
            }

            var viewModel = new CreateOrEditEccpDictLabelStatusViewModel
                                {
                                    EccpDictLabelStatus = getEccpDictLabelStatusForEditOutput.EccpDictLabelStatus
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
            var model = new EccpDictLabelStatusesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp dict label status modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpDictLabelStatusModal(GetEccpDictLabelStatusForView data)
        {
            var model = new EccpDictLabelStatusViewModel { EccpDictLabelStatus = data.EccpDictLabelStatus };

            return this.PartialView("_ViewEccpDictLabelStatusModal", model);
        }
    }
}