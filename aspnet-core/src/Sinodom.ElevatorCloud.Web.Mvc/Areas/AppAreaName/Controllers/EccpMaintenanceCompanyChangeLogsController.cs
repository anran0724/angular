// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceCompanyChangeLogsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceCompanyChangeLogs;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance company change logs controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs)]
    public class EccpMaintenanceCompanyChangeLogsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp maintenance company change logs app service.
        /// </summary>
        private readonly IEccpMaintenanceCompanyChangeLogsAppService _eccpMaintenanceCompanyChangeLogsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceCompanyChangeLogsController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceCompanyChangeLogsAppService">
        /// The eccp maintenance company change logs app service.
        /// </param>
        public EccpMaintenanceCompanyChangeLogsController(
            IEccpMaintenanceCompanyChangeLogsAppService eccpMaintenanceCompanyChangeLogsAppService)
        {
            this._eccpMaintenanceCompanyChangeLogsAppService = eccpMaintenanceCompanyChangeLogsAppService;
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
            AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs_Create,
            AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpMaintenanceCompanyChangeLogForEditOutput getEccpMaintenanceCompanyChangeLogForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceCompanyChangeLogForEditOutput =
                    await this._eccpMaintenanceCompanyChangeLogsAppService.GetEccpMaintenanceCompanyChangeLogForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpMaintenanceCompanyChangeLogForEditOutput = new GetEccpMaintenanceCompanyChangeLogForEditOutput
                                                                      {
                                                                          EccpMaintenanceCompanyChangeLog =
                                                                              new
                                                                                  CreateOrEditEccpMaintenanceCompanyChangeLogDto()
                                                                      };
            }

            var viewModel = new CreateOrEditEccpMaintenanceCompanyChangeLogViewModel
                                {
                                    EccpMaintenanceCompanyChangeLog =
                                        getEccpMaintenanceCompanyChangeLogForEditOutput.EccpMaintenanceCompanyChangeLog,
                                    EccpBaseMaintenanceCompanyName = getEccpMaintenanceCompanyChangeLogForEditOutput
                                        .ECCPBaseMaintenanceCompanyName
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp base maintenance company lookup table modal.
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
            AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs_Create,
            AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs_Edit)]
        public PartialViewResult EccpBaseMaintenanceCompanyLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ECCPBaseMaintenanceCompanyLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_ECCPBaseMaintenanceCompanyLookupTableModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpMaintenanceCompanyChangeLogsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp maintenance company change log modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpMaintenanceCompanyChangeLogModal(
            GetEccpMaintenanceCompanyChangeLogForView data)
        {
            var model = new EccpMaintenanceCompanyChangeLogViewModel
                            {
                                EccpMaintenanceCompanyChangeLog = data.EccpMaintenanceCompanyChangeLog,
                                ECCPBaseMaintenanceCompanyName = data.ECCPBaseMaintenanceCompanyName
                            };

            return this.PartialView("_ViewEccpMaintenanceCompanyChangeLogModal", model);
        }
    }
}