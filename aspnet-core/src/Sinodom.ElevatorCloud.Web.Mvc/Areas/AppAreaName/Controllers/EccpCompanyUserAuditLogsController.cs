// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpCompanyUserAuditLogsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions;
    using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpCompanyUserAuditLogs;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp company user audit logs controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs)]
    public class EccpCompanyUserAuditLogsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp company user audit logs app service.
        /// </summary>
        private readonly IEccpCompanyUserAuditLogsAppService _eccpCompanyUserAuditLogsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpCompanyUserAuditLogsController"/> class.
        /// </summary>
        /// <param name="eccpCompanyUserAuditLogsAppService">
        /// The eccp company user audit logs app service.
        /// </param>
        public EccpCompanyUserAuditLogsController(
            IEccpCompanyUserAuditLogsAppService eccpCompanyUserAuditLogsAppService)
        {
            this._eccpCompanyUserAuditLogsAppService = eccpCompanyUserAuditLogsAppService;
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
            AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs_Create,
            AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpCompanyUserAuditLogForEditOutput getEccpCompanyUserAuditLogForEditOutput;

            if (id.HasValue)
            {
                getEccpCompanyUserAuditLogForEditOutput =
                    await this._eccpCompanyUserAuditLogsAppService.GetEccpCompanyUserAuditLogForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpCompanyUserAuditLogForEditOutput =
                    new GetEccpCompanyUserAuditLogForEditOutput
                        {
                            EccpCompanyUserAuditLog = new CreateOrEditEccpCompanyUserAuditLogDto()
                        };
            }

            var viewModel = new CreateOrEditEccpCompanyUserAuditLogViewModel
                                {
                                    EccpCompanyUserAuditLog =
                                        getEccpCompanyUserAuditLogForEditOutput.EccpCompanyUserAuditLog,
                                    UserName = getEccpCompanyUserAuditLogForEditOutput.UserName
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
            var model = new EccpCompanyUserAuditLogsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The user lookup table modal.
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
            AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs_Create,
            AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs_Edit)]
        public PartialViewResult UserLookupTableModal(long? id, string displayName)
        {
            var viewModel = new UserLookupTableViewModel { Id = id, DisplayName = displayName, FilterText = string.Empty };

            return this.PartialView("_UserLookupTableModal", viewModel);
        }

        /// <summary>
        /// The view eccp company user audit log modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpCompanyUserAuditLogModal(GetEccpCompanyUserAuditLogForView data)
        {
            var model = new EccpCompanyUserAuditLogViewModel
                            {
                                EccpCompanyUserAuditLog = data.EccpCompanyUserAuditLog, UserName = data.UserName
                            };

            return this.PartialView("_ViewEccpCompanyUserAuditLogModal", model);
        }
    }
}