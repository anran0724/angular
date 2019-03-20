using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.Configuration;
using Sinodom.ElevatorCloud.Debugging;
using Sinodom.ElevatorCloud.Identity;
using Sinodom.ElevatorCloud.MultiTenancy;
using Sinodom.ElevatorCloud.MultiTenancy.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.Payments;
using Sinodom.ElevatorCloud.Security;
using Sinodom.ElevatorCloud.Url;
using Sinodom.ElevatorCloud.Web.Security.Recaptcha;
using System.Threading.Tasks;
using Abp.Collections.Extensions;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.MultiTenancy.Payments.Dto;
using Sinodom.ElevatorCloud.Web.Models.TenantRegistration;

namespace Sinodom.ElevatorCloud.Web.Controllers
{
    using Abp;
    using Abp.Web.Models;

    using Microsoft.EntityFrameworkCore;

    public class TenantRegistrationController : ElevatorCloudControllerBase
    {
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly LogInManager _logInManager;
        private readonly SignInManager _signInManager;
        private readonly IWebUrlService _webUrlService;
        private readonly ITenantRegistrationAppService _tenantRegistrationAppService;
        private readonly IPasswordComplexitySettingStore _passwordComplexitySettingStore;

        public TenantRegistrationController(
            IMultiTenancyConfig multiTenancyConfig,
            TenantManager tenantManager,
            UserManager userManager,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            LogInManager logInManager,
            SignInManager signInManager,
            IWebUrlService webUrlService,
            ITenantRegistrationAppService tenantRegistrationAppService,
            IPasswordComplexitySettingStore passwordComplexitySettingStore)
        {
            _multiTenancyConfig = multiTenancyConfig;
            _tenantManager = tenantManager;
            _userManager = userManager;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _logInManager = logInManager;
            _signInManager = signInManager;
            _webUrlService = webUrlService;
            _tenantRegistrationAppService = tenantRegistrationAppService;
            _passwordComplexitySettingStore = passwordComplexitySettingStore;
        }

        public async Task<ActionResult> SelectEdition()
        {
            CheckTenantRegistrationIsEnabled();

            var output = await _tenantRegistrationAppService.GetEditionsForSelect();
            if (output.EditionsWithFeatures.IsNullOrEmpty())
            {
                return RedirectToAction("Register", "TenantRegistration");
            }

            var model = new EditionsSelectViewModel(output);

            return View(model);
        }

        public async Task<ActionResult> Register(int? editionId, SubscriptionStartType? subscriptionStartType = null, SubscriptionPaymentGatewayType? gateway = null, string paymentId = "")
        {
            CheckTenantRegistrationIsEnabled();

            var model = new TenantRegisterViewModel
            {
                PasswordComplexitySetting = await _passwordComplexitySettingStore.GetSettingsAsync(),
                SubscriptionStartType = subscriptionStartType,
                EditionPaymentType = EditionPaymentType.NewRegistration,
                Gateway = gateway,
                PaymentId = paymentId
            };

            if (editionId.HasValue)
            {
                model.EditionId = editionId.Value;
                model.Edition = await _tenantRegistrationAppService.GetEdition(editionId.Value);
            }

            ViewBag.UseCaptcha = UseCaptchaOnRegistration();

            return View(model);
        }

        [HttpPost]
        [UnitOfWork]
        public virtual async Task<ActionResult> Register(RegisterTenantInput model)
        {
            try
            {
                if (UseCaptchaOnRegistration())
                {
                    model.CaptchaResponse = HttpContext.Request.Form[RecaptchaValidator.RecaptchaResponseKey];
                }

                var result = await _tenantRegistrationAppService.RegisterTenant(model);

                CurrentUnitOfWork.SetTenantId(result.TenantId);

                var user = await _userManager.FindByNameAsync(AbpUserBase.AdminUserName);

                //Directly login if possible
                if (result.IsTenantActive && result.IsActive && !result.IsEmailConfirmationRequired &&
                    !_webUrlService.SupportsTenancyNameInUrl)
                {
                    var loginResult = await GetLoginResultAsync(user.UserName, model.AdminPassword, model.TenancyName);

                    if (loginResult.Result == AbpLoginResultType.Success)
                    {
                        await _signInManager.SignOutAsync();
                        await _signInManager.SignInAsync(loginResult.Identity, false);

                        SetTenantIdCookie(result.TenantId);

                        return Redirect(Url.Action("Index", "Home", new { area = "AppAreaName" }));
                    }

                    Logger.Warn("New registered user could not be login. This should not be normally. login result: " + loginResult.Result);
                }

                //Show result page
                var resultModel = ObjectMapper.Map<TenantRegisterResultViewModel>(result);

                resultModel.TenantLoginAddress = _webUrlService.SupportsTenancyNameInUrl
                    ? _webUrlService.GetSiteRootAddress(model.TenancyName).EnsureEndsWith('/') + "Account/Login"
                    : "";

                return View("RegisterResult", resultModel);
            }
            catch (UserFriendlyException ex)
            {
                ViewBag.UseCaptcha = UseCaptchaOnRegistration();
                ViewBag.ErrorMessage = ex.Message;

                var viewModel = new TenantRegisterViewModel
                {
                    Name = model.Name,
                    Mobile = model.Mobile,
                    AdminEmailAddress = model.AdminEmailAddress,
                    TenancyName = model.TenancyName,
                    LegalPerson = model.LegalPerson,
                    PasswordComplexitySetting = await _passwordComplexitySettingStore.GetSettingsAsync(),
                    EditionId = model.EditionId,
                    SubscriptionStartType = model.SubscriptionStartType,
                    EditionPaymentType = EditionPaymentType.NewRegistration,
                    Gateway = model.Gateway,
                    PaymentId = model.PaymentId
                };

                if (model.EditionId.HasValue)
                {
                    viewModel.Edition = await _tenantRegistrationAppService.GetEdition(model.EditionId.Value);
                    viewModel.EditionId = model.EditionId.Value;
                }

                return View("Register", viewModel);
            }
        }

        public ActionResult RegisterEdit()
        {
            return this.View();
        }

        [HttpPost]
        [UnitOfWork]
        public virtual async Task<JsonResult> CheckTenancyName(CheckTenancyNameViewModel model)
        {
            var tenant = await this._tenantManager.FindByTenancyNameAsync(model.TenancyName);
            if (tenant == null)
            {
                return this.Json(new AjaxResponse("不存在"));
            }

            if (tenant.IsActive)
            {
                return this.Json(new AjaxResponse("已审核"));
            }

            var user = await this._userManager.FindByNameOrEmailAsync(tenant.Id, AbpUserBase.AdminUserName);
            if (user == null)
            {
                return this.Json(new AjaxResponse("没有用户"));
            }

            if (await this._userManager.CheckPasswordAsync(user, model.AdminPassword))
            {



                return this.Json(new AjaxResponse("通过"));
            }
            else
            {
                return this.Json(new AjaxResponse("未通过"));
            }
        }

        private bool IsSelfRegistrationEnabled()
        {
            return SettingManager.GetSettingValueForApplication<bool>(AppSettings.TenantManagement.AllowSelfRegistration);
        }

        private void CheckTenantRegistrationIsEnabled()
        {
            if (!IsSelfRegistrationEnabled())
            {
                throw new UserFriendlyException(L("SelfTenantRegistrationIsDisabledMessage_Detail"));
            }

            if (!_multiTenancyConfig.IsEnabled)
            {
                throw new UserFriendlyException(L("MultiTenancyIsNotEnabled"));
            }
        }

        private bool UseCaptchaOnRegistration()
        {
            if (DebugHelper.IsDebug)
            {
                return false;
            }

            return SettingManager.GetSettingValueForApplication<bool>(AppSettings.TenantManagement.UseCaptchaOnRegistration);
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }
    }
}
