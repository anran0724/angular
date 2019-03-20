// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceWorkFlowStatusesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictMaintenanceWorkFlowStatuses;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp dict maintenance work flow statuses controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceWorkFlowStatuses)]
    public class EccpDictMaintenanceWorkFlowStatusesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp dict maintenance work flow statuses app service.
        /// </summary>
        private readonly IEccpDictMaintenanceWorkFlowStatusesAppService _eccpDictMaintenanceWorkFlowStatusesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictMaintenanceWorkFlowStatusesController"/> class.
        /// </summary>
        /// <param name="eccpDictMaintenanceWorkFlowStatusesAppService">
        /// The eccp dict maintenance work flow statuses app service.
        /// </param>
        public EccpDictMaintenanceWorkFlowStatusesController(
            IEccpDictMaintenanceWorkFlowStatusesAppService eccpDictMaintenanceWorkFlowStatusesAppService)
        {
            this._eccpDictMaintenanceWorkFlowStatusesAppService = eccpDictMaintenanceWorkFlowStatusesAppService;
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
            AppPermissions.Pages_EccpDict_EccpDictMaintenanceWorkFlowStatuses_Create,
            AppPermissions.Pages_EccpDict_EccpDictMaintenanceWorkFlowStatuses_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpDictMaintenanceWorkFlowStatusForEditOutput getEccpDictMaintenanceWorkFlowStatusForEditOutput;

            if (id.HasValue)
            {
                getEccpDictMaintenanceWorkFlowStatusForEditOutput =
                    await this._eccpDictMaintenanceWorkFlowStatusesAppService
                        .GetEccpDictMaintenanceWorkFlowStatusForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpDictMaintenanceWorkFlowStatusForEditOutput =
                    new GetEccpDictMaintenanceWorkFlowStatusForEditOutput
                        {
                            EccpDictMaintenanceWorkFlowStatus = new CreateOrEditEccpDictMaintenanceWorkFlowStatusDto()
                        };
            }

            var viewModel = new CreateOrEditEccpDictMaintenanceWorkFlowStatusViewModel
                                {
                                    EccpDictMaintenanceWorkFlowStatus =
                                        getEccpDictMaintenanceWorkFlowStatusForEditOutput
                                            .EccpDictMaintenanceWorkFlowStatus
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
            var model = new EccpDictMaintenanceWorkFlowStatusesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp dict maintenance work flow status modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpDictMaintenanceWorkFlowStatusModal(
            GetEccpDictMaintenanceWorkFlowStatusForView data)
        {
            var model = new EccpDictMaintenanceWorkFlowStatusViewModel
                            {
                                EccpDictMaintenanceWorkFlowStatus = data.EccpDictMaintenanceWorkFlowStatus
                            };

            return this.PartialView("_ViewEccpDictMaintenanceWorkFlowStatusModal", model);
        }
    }
}