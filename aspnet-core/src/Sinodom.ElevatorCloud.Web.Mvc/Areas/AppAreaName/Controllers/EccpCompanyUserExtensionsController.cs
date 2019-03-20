// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpCompanyUserExtensionsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.Security;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Users;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp company user extensions controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpCompanyUserExtensions)]
    public class EccpCompanyUserExtensionsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _password complexity setting store.
        /// </summary>
        private readonly IPasswordComplexitySettingStore _passwordComplexitySettingStore;

        /// <summary>
        /// The _user app service.
        /// </summary>
        private readonly IUserAppService _userAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpCompanyUserExtensionsController"/> class.
        /// </summary>
        /// <param name="userAppService">
        /// The user app service.
        /// </param>
        /// <param name="passwordComplexitySettingStore">
        /// The password complexity setting store.
        /// </param>
        public EccpCompanyUserExtensionsController(
            IUserAppService userAppService,
            IPasswordComplexitySettingStore passwordComplexitySettingStore)
        {
            this._userAppService = userAppService;
            this._passwordComplexitySettingStore = passwordComplexitySettingStore;
        }

        /// <summary>
        /// The audit modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpCompanyUserExtensions_EditState)]
        public async Task<PartialViewResult> AuditModal(long? id)
        {
            var output = await this._userAppService.GetUserForEdit(new NullableIdDto<long> { Id = id });
            var viewModel = new CreateOrEditUserModalViewModel(output)
                                {
                                    PasswordComplexitySetting =
                                        await this._passwordComplexitySettingStore.GetSettingsAsync()
                                };

            return this.PartialView("_AuditModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            return this.View();
        }
    }
}