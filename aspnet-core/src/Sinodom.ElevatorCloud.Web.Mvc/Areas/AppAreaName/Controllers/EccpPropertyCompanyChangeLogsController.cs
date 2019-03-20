// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpPropertyCompanyChangeLogsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpPropertyCompanyChangeLogs;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp property company change logs controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs)]
    public class EccpPropertyCompanyChangeLogsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp property company change logs app service.
        /// </summary>
        private readonly IEccpPropertyCompanyChangeLogsAppService _eccpPropertyCompanyChangeLogsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpPropertyCompanyChangeLogsController"/> class.
        /// </summary>
        /// <param name="eccpPropertyCompanyChangeLogsAppService">
        /// The eccp property company change logs app service.
        /// </param>
        public EccpPropertyCompanyChangeLogsController(
            IEccpPropertyCompanyChangeLogsAppService eccpPropertyCompanyChangeLogsAppService)
        {
            this._eccpPropertyCompanyChangeLogsAppService = eccpPropertyCompanyChangeLogsAppService;
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
            AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs_Create,
            AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpPropertyCompanyChangeLogForEditOutput getEccpPropertyCompanyChangeLogForEditOutput;

            if (id.HasValue)
            {
                getEccpPropertyCompanyChangeLogForEditOutput =
                    await this._eccpPropertyCompanyChangeLogsAppService.GetEccpPropertyCompanyChangeLogForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpPropertyCompanyChangeLogForEditOutput = new GetEccpPropertyCompanyChangeLogForEditOutput
                                                                   {
                                                                       EccpPropertyCompanyChangeLog =
                                                                           new
                                                                               CreateOrEditEccpPropertyCompanyChangeLogDto()
                                                                   };
            }

            var viewModel = new CreateOrEditEccpPropertyCompanyChangeLogViewModel
                                {
                                    EccpPropertyCompanyChangeLog =
                                        getEccpPropertyCompanyChangeLogForEditOutput.EccpPropertyCompanyChangeLog,
                                    EccpBasePropertyCompanyName = getEccpPropertyCompanyChangeLogForEditOutput
                                        .ECCPBasePropertyCompanyName
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp base property company lookup table modal.
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
            AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs_Create,
            AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs_Edit)]
        public PartialViewResult EccpBasePropertyCompanyLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ECCPBasePropertyCompanyLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_ECCPBasePropertyCompanyLookupTableModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpPropertyCompanyChangeLogsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp property company change log modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpPropertyCompanyChangeLogModal(GetEccpPropertyCompanyChangeLogForView data)
        {
            var model = new EccpPropertyCompanyChangeLogViewModel
                            {
                                EccpPropertyCompanyChangeLog = data.EccpPropertyCompanyChangeLog,
                                ECCPBasePropertyCompanyName = data.ECCPBasePropertyCompanyName
                            };

            return this.PartialView("_ViewEccpPropertyCompanyChangeLogModal", model);
        }
    }
}