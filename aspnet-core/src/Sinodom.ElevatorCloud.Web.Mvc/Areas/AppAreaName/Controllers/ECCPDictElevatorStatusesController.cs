// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPDictElevatorStatusesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPDictElevatorStatuses;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp dict elevator statuses controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpDict_EccpDictElevatorStatuses)]
    public class ECCPDictElevatorStatusesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp dict elevator statuses app service.
        /// </summary>
        private readonly IECCPDictElevatorStatusesAppService _eccpDictElevatorStatusesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPDictElevatorStatusesController"/> class.
        /// </summary>
        /// <param name="eccpDictElevatorStatusesAppService">
        /// The eccp dict elevator statuses app service.
        /// </param>
        public ECCPDictElevatorStatusesController(
            IECCPDictElevatorStatusesAppService eccpDictElevatorStatusesAppService)
        {
            this._eccpDictElevatorStatusesAppService = eccpDictElevatorStatusesAppService;
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
            AppPermissions.Pages_EccpDict_EccpDictElevatorStatuses_Create,
            AppPermissions.Pages_EccpDict_EccpDictElevatorStatuses_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetECCPDictElevatorStatusForEditOutput getEccpDictElevatorStatusForEditOutput;

            if (id.HasValue)
            {
                getEccpDictElevatorStatusForEditOutput =
                    await this._eccpDictElevatorStatusesAppService.GetECCPDictElevatorStatusForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpDictElevatorStatusForEditOutput =
                    new GetECCPDictElevatorStatusForEditOutput
                        {
                            ECCPDictElevatorStatus = new CreateOrEditECCPDictElevatorStatusDto()
                        };
            }

            var viewModel = new CreateOrEditECCPDictElevatorStatusViewModel
                                {
                                    EccpDictElevatorStatus =
                                        getEccpDictElevatorStatusForEditOutput.ECCPDictElevatorStatus
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
            var model = new ECCPDictElevatorStatusesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp dict elevator status modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpDictElevatorStatusModal(GetECCPDictElevatorStatusForView data)
        {
            var model = new ECCPDictElevatorStatusViewModel { ECCPDictElevatorStatus = data.ECCPDictElevatorStatus };

            return this.PartialView("_ViewECCPDictElevatorStatusModal", model);
        }
    }
}