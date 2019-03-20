using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Net.Mail;
using Abp.Notifications;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using Abp.Web.Models;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Sinodom.ElevatorCloud.Authentication.TwoFactor.Google;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.Authorization.Accounts;
using Sinodom.ElevatorCloud.Authorization.Accounts.Dto;
using Sinodom.ElevatorCloud.Authorization.Impersonation;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.Configuration;
using Sinodom.ElevatorCloud.Debugging;
using Sinodom.ElevatorCloud.Identity;
using Sinodom.ElevatorCloud.MultiTenancy;
using Sinodom.ElevatorCloud.Notifications;
using Sinodom.ElevatorCloud.Web.Models.Account;
using Sinodom.ElevatorCloud.Security;
using Sinodom.ElevatorCloud.Security.Recaptcha;
using Sinodom.ElevatorCloud.Sessions;
using Sinodom.ElevatorCloud.Url;
using Sinodom.ElevatorCloud.Web.IdentityServer;
using Sinodom.ElevatorCloud.Web.Security.Recaptcha;
using Sinodom.ElevatorCloud.Web.Session;
using Sinodom.ElevatorCloud.Web.Views.Shared.Components.TenantChange;
using Sinodom.ElevatorCloud.Storage;
using Abp.Domain.Repositories;
using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;
using System.IO;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Consts;
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.Extensions;
using Sinodom.ElevatorCloud.EccpBaseSaicUnits;

namespace Sinodom.ElevatorCloud.Web.Controllers
{
    public class AccountController : ProfileControllerBase
    {
        private const int MaxProfilPictureBytes = 1048576; //1MB
        private readonly UserManager _userManager;
        private readonly TenantManager _tenantManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IWebUrlService _webUrlService;
        private readonly IAppUrlService _appUrlService;
        private readonly IAppNotifier _appNotifier;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly IUserLinkManager _userLinkManager;
        private readonly LogInManager _logInManager;
        private readonly SignInManager _signInManager;
        private readonly IRecaptchaValidator _recaptchaValidator;
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly ITenantCache _tenantCache;
        private readonly IAccountAppService _accountAppService;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IImpersonationManager _impersonationManager;
        private readonly ISmsSender _smsSender;
        private readonly IEmailSender _emailSender;
        private readonly IPasswordComplexitySettingStore _passwordComplexitySettingStore;
        private readonly IdentityOptions _identityOptions;
        private readonly ISessionAppService _sessionAppService;
        private readonly IRepository<EccpCompanyUserExtension, int> _eccpCompanyUserExtension;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<ECCPEdition> _eccpEditionRepository;
        private readonly IRepository<ECCPEditionsType> _eccpEditionsTypeRepository;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IRepository<ECCPBaseMaintenanceCompany, int> _eccpBaseMaintenanceCompanyRepository;
        private readonly IRepository<ECCPBasePropertyCompany, int> _eccpBasePropertyCompanyRepository;
        private readonly IRepository<EccpBaseSaicUnit, int> _eccpBaseSaicUnitRepository;
        public AccountController(
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig,
            TenantManager tenantManager,
            IUnitOfWorkManager unitOfWorkManager,
            IAppNotifier appNotifier,
            IWebUrlService webUrlService,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            IUserLinkManager userLinkManager,
            LogInManager logInManager,
            SignInManager signInManager,
            IRecaptchaValidator recaptchaValidator,
            ITenantCache tenantCache,
            IAccountAppService accountAppService,
            UserRegistrationManager userRegistrationManager,
            IImpersonationManager impersonationManager,
            IAppUrlService appUrlService,
            IPerRequestSessionCache sessionCache,
            IEmailSender emailSender,
            ISmsSender smsSender,
            IPasswordComplexitySettingStore passwordComplexitySettingStore,
            IOptions<IdentityOptions> identityOptions,
            ISessionAppService sessionAppService, ITempFileCacheManager tempFileCacheManager,
            IRepository<EccpCompanyUserExtension, int> eccpCompanyUserExtension, IBinaryObjectManager binaryObjectManager, IRepository<ECCPEdition> eccpEditionRepository, IRepository<ECCPEditionsType> eccpEditionsTypeRepository, IRepository<ECCPBaseMaintenanceCompany, int> eccpBaseMaintenanceCompanyRepository, IRepository<ECCPBasePropertyCompany, int> eccpBasePropertyCompanyRepository, IRepository<EccpBaseSaicUnit, int> eccpBaseSaicUnitRepository) :
            base(tempFileCacheManager)
        {
            _userManager = userManager;
            _multiTenancyConfig = multiTenancyConfig;
            _tenantManager = tenantManager;
            _unitOfWorkManager = unitOfWorkManager;
            _webUrlService = webUrlService;
            _appNotifier = appNotifier;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _userLinkManager = userLinkManager;
            _logInManager = logInManager;
            _signInManager = signInManager;
            _recaptchaValidator = recaptchaValidator;
            _tenantCache = tenantCache;
            _accountAppService = accountAppService;
            _userRegistrationManager = userRegistrationManager;
            _impersonationManager = impersonationManager;
            _appUrlService = appUrlService;
            _sessionCache = sessionCache;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _passwordComplexitySettingStore = passwordComplexitySettingStore;
            _identityOptions = identityOptions.Value;
            _sessionAppService = sessionAppService;
            _eccpCompanyUserExtension = eccpCompanyUserExtension;
            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            _eccpEditionRepository = eccpEditionRepository;
            _eccpEditionsTypeRepository = eccpEditionsTypeRepository;
            _eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
            _eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
            _eccpBaseSaicUnitRepository = eccpBaseSaicUnitRepository;
        }

        #region Login / Logout

        public async Task<ActionResult> Login(string userNameOrEmailAddress = "", string returnUrl = "", string successMessage = "", string ss = "")
        {
            returnUrl = NormalizeReturnUrl(returnUrl);

            if (!string.IsNullOrEmpty(ss) && ss.Equals("true", StringComparison.OrdinalIgnoreCase) && AbpSession.UserId > 0)
            {
                var updateUserSignInTokenOutput = await _sessionAppService.UpdateUserSignInToken();
                returnUrl = AddSingleSignInParametersToReturnUrl(returnUrl, updateUserSignInTokenOutput.SignInToken, AbpSession.UserId.Value, AbpSession.TenantId);
                return Redirect(returnUrl);
            }

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;
            ViewBag.SingleSignIn = ss;

            return View(
                new LoginFormViewModel
                {
                    IsSelfRegistrationEnabled = IsSelfRegistrationEnabled(),
                    IsTenantSelfRegistrationEnabled = IsTenantSelfRegistrationEnabled(),
                    SuccessMessage = successMessage,
                    UserNameOrEmailAddress = userNameOrEmailAddress
                });
        }

        [HttpPost]
        [UnitOfWork]
        public virtual async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "", string ss = "")
        {
            returnUrl = NormalizeReturnUrl(returnUrl);
            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl = returnUrl + returnUrlHash;
            }

            var loginResult = await GetLoginResultAsync(loginModel.UsernameOrEmailAddress, loginModel.Password, GetTenancyNameOrNull());

            if (!string.IsNullOrEmpty(ss) && ss.Equals("true", StringComparison.OrdinalIgnoreCase) && loginResult.Result == AbpLoginResultType.Success)
            {
                loginResult.User.SetSignInToken();
                returnUrl = AddSingleSignInParametersToReturnUrl(returnUrl, loginResult.User.SignInToken, loginResult.User.Id, loginResult.User.TenantId);
            }

            if (loginResult.User.ShouldChangePasswordOnNextLogin)
            {
                loginResult.User.SetNewPasswordResetCode();

                return Json(new AjaxResponse
                {
                    TargetUrl = Url.Action(
                        "ResetPassword",
                        new ResetPasswordViewModel
                        {
                            TenantId = AbpSession.TenantId,
                            UserId = loginResult.User.Id,
                            ResetCode = loginResult.User.PasswordResetCode,
                            ReturnUrl = returnUrl,
                            SingleSignIn = ss
                        })
                });
            }


            var signInResult = await _signInManager.SignInOrTwoFactorAsync(loginResult, loginModel.RememberMe);
            if (signInResult.RequiresTwoFactor)
            {
                return Json(new AjaxResponse
                {
                    TargetUrl = Url.Action(
                        "SendSecurityCode",
                        new
                        {
                            returnUrl = returnUrl,
                            rememberMe = loginModel.RememberMe
                        })
                });
            }

            Debug.Assert(signInResult.Succeeded);

            await UnitOfWorkManager.Current.SaveChangesAsync();

            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }

        public async Task<ActionResult> Logout(string returnUrl = "")
        {
            await _signInManager.SignOutAsync();

            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = NormalizeReturnUrl(returnUrl);
                return Redirect(returnUrl);
            }

            return RedirectToAction("Login");
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    if (loginResult.Tenant != null)
                    {
                        await AddClaimsEccpAbpSession(loginResult.Tenant.Id, loginResult.User);
                    }
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private async Task AddClaimsEccpAbpSession(int tenantId, User user)
        {
            // 获取租户
            var tenant = await _tenantManager.GetByIdAsync(tenantId);
            // 通过租户版本ID获取版本信息
            var edition = await this._eccpEditionRepository.FirstOrDefaultAsync(tenant.EditionId.Value);
            // 根据版本类型ID获取版本类型
            var eccpEditionsType = await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(edition.ECCPEditionsTypeId.Value);

            List<Claim> list = new List<Claim>
                        {
                            new Claim(EccpAbpSessionConsts.ClaimTypes.EditionId, edition.Id.ToString()),
                            new Claim(EccpAbpSessionConsts.ClaimTypes.EditionTypeName, eccpEditionsType.Name)
                        };
            switch (eccpEditionsType.Name)
            {
                case "政府":
                    var eccpBaseSaicUnit = await this._eccpBaseSaicUnitRepository.FirstOrDefaultAsync(e =>
                        e.TenantId == tenantId);
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.CompanieName, eccpBaseSaicUnit.Name));
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.ProvinceId, eccpBaseSaicUnit.ProvinceId.ToString()));
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.CityId, eccpBaseSaicUnit.CityId.ToString()));
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.DistrictId, eccpBaseSaicUnit.DistrictId.ToString()));
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.StreetId, eccpBaseSaicUnit.StreetId.ToString()));
                    break;
                case "维保公司":
                    var eccpBaseMaintenanceCompany = await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(e =>
                        e.TenantId == tenantId);
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.CompanieName, eccpBaseMaintenanceCompany.Name));
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.ProvinceId, eccpBaseMaintenanceCompany.ProvinceId.ToString()));
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.CityId, eccpBaseMaintenanceCompany.CityId.ToString()));
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.DistrictId, eccpBaseMaintenanceCompany.DistrictId.ToString()));
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.StreetId, eccpBaseMaintenanceCompany.StreetId.ToString()));
                    break;
                case "物业公司":
                    var eccpBasePropertyCompany = await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(e =>
                        e.TenantId == tenantId);
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.CompanieName, eccpBasePropertyCompany.Name));
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.ProvinceId, eccpBasePropertyCompany.ProvinceId.ToString()));
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.CityId, eccpBasePropertyCompany.CityId.ToString()));
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.DistrictId, eccpBasePropertyCompany.DistrictId.ToString()));
                    list.Add(new Claim(EccpAbpSessionConsts.ClaimTypes.StreetId, eccpBasePropertyCompany.StreetId.ToString()));
                    break;
            }

            var claims = await _userManager.GetClaimsAsync(user);
            await _userManager.RemoveClaimsAsync(user, claims);

            await _userManager.AddClaimsAsync(user, list);
        }

        private string AddSingleSignInParametersToReturnUrl(string returnUrl, string signInToken, long userId, int? tenantId)
        {
            returnUrl += (returnUrl.Contains("?") ? "&" : "?") +
                         "accessToken=" + signInToken +
                         "&userId=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(userId.ToString()));
            if (tenantId.HasValue)
            {
                returnUrl += "&tenantId=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(tenantId.Value.ToString()));
            }

            return returnUrl;
        }

        #endregion

        #region Two Factor Auth

        public async Task<ActionResult> SendSecurityCode(string returnUrl, bool rememberMe = false)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            CheckCurrentTenant(await _signInManager.GetVerifiedTenantIdAsync());

            var userProviders = await _userManager.GetValidTwoFactorProvidersAsync(user);

            var factorOptions = userProviders.Select(
                userProvider =>
                    new SelectListItem
                    {
                        Text = userProvider,
                        Value = userProvider
                    }).ToList();

            return View(
                new SendSecurityCodeViewModel
                {
                    Providers = factorOptions,
                    ReturnUrl = returnUrl,
                    RememberMe = rememberMe
                }
            );
        }

        [HttpPost]
        public async Task<ActionResult> SendSecurityCode(SendSecurityCodeViewModel model)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            CheckCurrentTenant(await _signInManager.GetVerifiedTenantIdAsync());

            if (model.SelectedProvider != GoogleAuthenticatorProvider.Name)
            {
                var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
                var message = L("EmailSecurityCodeBody", code);

                if (model.SelectedProvider == "Email")
                {
                    await _emailSender.SendAsync(await _userManager.GetEmailAsync(user), L("EmailSecurityCodeSubject"), message);
                }
                else if (model.SelectedProvider == "Phone")
                {
                    await _smsSender.SendAsync(await _userManager.GetPhoneNumberAsync(user), message);
                }
            }

            return RedirectToAction(
                "VerifySecurityCode",
                new
                {
                    provider = model.SelectedProvider,
                    returnUrl = model.ReturnUrl,
                    rememberMe = model.RememberMe
                }
            );
        }

        public async Task<ActionResult> VerifySecurityCode(string provider, string returnUrl, bool rememberMe)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new UserFriendlyException(L("VerifySecurityCodeNotLoggedInErrorMessage"));
            }

            CheckCurrentTenant(await _signInManager.GetVerifiedTenantIdAsync());

            var isRememberBrowserEnabled = await IsRememberBrowserEnabledAsync();

            return View(
                new VerifySecurityCodeViewModel
                {
                    Provider = provider,
                    ReturnUrl = returnUrl,
                    RememberMe = rememberMe,
                    IsRememberBrowserEnabled = isRememberBrowserEnabled
                }
            );
        }

        [HttpPost]
        public async Task<JsonResult> VerifySecurityCode(VerifySecurityCodeViewModel model)
        {
            model.ReturnUrl = NormalizeReturnUrl(model.ReturnUrl);

            CheckCurrentTenant(await _signInManager.GetVerifiedTenantIdAsync());

            var result = await _signInManager.TwoFactorSignInAsync(
                model.Provider,
                model.Code,
                model.RememberMe,
                await IsRememberBrowserEnabledAsync() && model.RememberBrowser
            );

            if (result.Succeeded)
            {
                return Json(new AjaxResponse { TargetUrl = model.ReturnUrl });
            }

            if (result.IsLockedOut)
            {
                throw new UserFriendlyException(L("UserLockedOutMessage"));
            }

            throw new UserFriendlyException(L("InvalidSecurityCode"));
        }

        private Task<bool> IsRememberBrowserEnabledAsync()
        {
            return SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled);
        }

        #endregion

        #region Register

        public async Task<ActionResult> Register(string returnUrl = "", string ss = "")
        {
            return RegisterView(new RegisterViewModel
            {
                PasswordComplexitySetting = await _passwordComplexitySettingStore.GetSettingsAsync(),
                ReturnUrl = returnUrl,
                SingleSignIn = ss
            });
        }

        private ActionResult RegisterView(RegisterViewModel model)
        {
            CheckSelfRegistrationIsEnabled();

            ViewBag.UseCaptcha = !model.IsExternalLogin && UseCaptchaOnRegistration();

            return View("Register", model);
        }

        [HttpPost]
        [UnitOfWork(IsolationLevel.ReadUncommitted)]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!model.IsExternalLogin && UseCaptchaOnRegistration())
                {
                    await _recaptchaValidator.ValidateAsync(HttpContext.Request.Form[RecaptchaValidator.RecaptchaResponseKey]);
                }

                ExternalLoginInfo externalLoginInfo = null;
                if (model.IsExternalLogin)
                {
                    externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
                    if (externalLoginInfo == null)
                    {
                        throw new Exception("Can not external login!");
                    }

                    model.UserName = model.EmailAddress.ToMd5();
                    model.Password = Authorization.Users.User.CreateRandomPassword();
                }
                else
                {
                    if (model.UserName.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
                    {
                        throw new UserFriendlyException(L("FormIsNotValidMessage"));
                    }
                }
                Guid storedFileID = new Guid();
                ////-------signPicture
                if (model.SingleSignIn != null)
                {
                    byte[] byteArray;
                    var imageBytes = _tempFileCacheManager.GetFile(model.SignPictureId);
                    if (imageBytes == null)
                    {
                        throw new UserFriendlyException("There is no such image file with the token: " + model.SignPictureId);
                    }
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        byteArray = stream.ToArray();
                    }

                    if (byteArray.Length > MaxProfilPictureBytes)
                    {
                        throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit", AppConsts.ResizedMaxProfilPictureBytesUserFriendlyValue));
                    }
                    var storedFile = new BinaryObject(AbpSession.TenantId, byteArray);
                    await _binaryObjectManager.SaveAsync(storedFile);
                    storedFileID = storedFile.Id;
                }
                Guid certificateBackPictureStoredFileID = new Guid();
                ////-------certificateBackPicture
                byte[] certificateBackPictureByteArray;
                if (model.CertificateBackPictureId != null)
                {
                    var certificateBackPictureImageBytes = _tempFileCacheManager.GetFile(model.CertificateBackPictureId);
                    if (certificateBackPictureImageBytes == null)
                    {
                        throw new UserFriendlyException("There is no such image file with the token: " + model.CertificateBackPictureId);
                    }
                    using (var stream = new MemoryStream(certificateBackPictureImageBytes))
                    {
                        certificateBackPictureByteArray = stream.ToArray();
                    }

                    if (certificateBackPictureByteArray.Length > MaxProfilPictureBytes)
                    {
                        throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit", AppConsts.ResizedMaxProfilPictureBytesUserFriendlyValue));
                    }
                    var certificateBackPictureStoredFile = new BinaryObject(AbpSession.TenantId, certificateBackPictureByteArray);
                    await _binaryObjectManager.SaveAsync(certificateBackPictureStoredFile);
                    certificateBackPictureStoredFileID = certificateBackPictureStoredFile.Id;
                }

                Guid certificateFrontPictureStoredFileID = new Guid();
                ////-------certificateFrontPicture
                byte[] certificateFrontPictureByteArray;
                if (model.CertificateFrontPictureId != null)
                {
                    var certificateFrontPictureImageBytes = _tempFileCacheManager.GetFile(model.CertificateFrontPictureId);
                    if (certificateFrontPictureImageBytes == null)
                    {
                        throw new UserFriendlyException("There is no such image file with the token: " + model.CertificateFrontPictureId);
                    }
                    using (var stream = new MemoryStream(certificateFrontPictureImageBytes))
                    {
                        certificateFrontPictureByteArray = stream.ToArray();
                    }

                    if (certificateFrontPictureByteArray.Length > MaxProfilPictureBytes)
                    {
                        throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit", AppConsts.ResizedMaxProfilPictureBytesUserFriendlyValue));
                    }
                    var certificateFrontPictureStoredFile = new BinaryObject(AbpSession.TenantId, certificateFrontPictureByteArray);
                    await _binaryObjectManager.SaveAsync(certificateFrontPictureStoredFile);
                    certificateFrontPictureStoredFileID = certificateFrontPictureStoredFile.Id;
                }
                var user = await _userRegistrationManager.RegisterAsync(
                    model.Name,
                    model.Surname,
                    model.EmailAddress,
                    model.UserName,
                    model.Password,
                    true,
                    _appUrlService.CreateEmailActivationUrlFormat(AbpSession.TenantId),
                    model.Mobile
                );

                EccpCompanyUserExtension companyUserModel = new EccpCompanyUserExtension();
                companyUserModel.UserId = user.Id;
                companyUserModel.IdCard = model.IdCard;
                companyUserModel.Mobile = model.Mobile;
                companyUserModel.SignPictureId = storedFileID;
                companyUserModel.CertificateBackPictureId = certificateBackPictureStoredFileID;
                companyUserModel.CertificateFrontPictureId = certificateFrontPictureStoredFileID;
                companyUserModel.ExpirationDate = model.ExpirationDate;
                _eccpCompanyUserExtension.Insert(companyUserModel);

                //Getting tenant-specific settings
                var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

                if (model.IsExternalLogin)
                {
                    if (string.Equals(externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email), model.EmailAddress, StringComparison.OrdinalIgnoreCase))
                    {
                        user.IsEmailConfirmed = true;
                    }

                    user.Logins = new List<UserLogin>
                    {
                        new UserLogin
                        {
                            LoginProvider = externalLoginInfo.LoginProvider,
                            ProviderKey = externalLoginInfo.ProviderKey,
                            TenantId = user.TenantId
                        }
                    };
                }

                await _unitOfWorkManager.Current.SaveChangesAsync();

                var tenant = await _tenantManager.GetByIdAsync(user.TenantId.Value);

                //Directly login if possible
                if (user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin))
                {
                    AbpLoginResult<Tenant, User> loginResult;
                    if (externalLoginInfo != null)
                    {
                        loginResult = await _logInManager.LoginAsync(externalLoginInfo, tenant.TenancyName);
                    }
                    else
                    {
                        loginResult = await GetLoginResultAsync(user.UserName, model.Password, tenant.TenancyName);
                    }

                    if (loginResult.Result == AbpLoginResultType.Success)
                    {
                        await _signInManager.SignInAsync(loginResult.Identity, false);
                        if (!string.IsNullOrEmpty(model.SingleSignIn) && model.SingleSignIn.Equals("true", StringComparison.OrdinalIgnoreCase) && loginResult.Result == AbpLoginResultType.Success)
                        {
                            var returnUrl = NormalizeReturnUrl(model.ReturnUrl);
                            loginResult.User.SetSignInToken();
                            returnUrl = AddSingleSignInParametersToReturnUrl(returnUrl, loginResult.User.SignInToken, loginResult.User.Id, loginResult.User.TenantId);
                            return Redirect(returnUrl);
                        }

                        return Redirect(GetAppHomeUrl());
                    }

                    Logger.Warn("New registered user could not be login. This should not be normally. login result: " + loginResult.Result);
                }
                return Json(new { success = true, message = "" });
                //return View("RegisterResult", new RegisterResultViewModel
                //{
                //    TenancyName = tenant.TenancyName,
                //    NameAndSurname = user.Name + " " + user.Surname,
                //    UserName = user.UserName,
                //    EmailAddress = user.EmailAddress,
                //    IsActive = user.IsActive,
                //    IsEmailConfirmationRequired = isEmailConfirmationRequiredForLogin
                //});
            }
            catch (UserFriendlyException ex)
            {
                ViewBag.UseCaptcha = !model.IsExternalLogin && UseCaptchaOnRegistration();
                ViewBag.ErrorMessage = ex.Message;

                return Json(new { success = false, message = ex.Message });
            }
        }

        private bool UseCaptchaOnRegistration()
        {
            if (DebugHelper.IsDebug)
            {
                return false;
            }

            if (!AbpSession.TenantId.HasValue)
            {
                //Host users can not register
                throw new InvalidOperationException();
            }

            return SettingManager.GetSettingValue<bool>(AppSettings.UserManagement.UseCaptchaOnRegistration);
        }

        private void CheckSelfRegistrationIsEnabled()
        {
            if (!IsSelfRegistrationEnabled())
            {
                throw new UserFriendlyException(L("SelfUserRegistrationIsDisabledMessage_Detail"));
            }
        }

        private bool IsSelfRegistrationEnabled()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return false; //No registration enabled for host users!
            }

            return SettingManager.GetSettingValue<bool>(AppSettings.UserManagement.AllowSelfRegistration);
        }

        private bool IsTenantSelfRegistrationEnabled()
        {
            if (AbpSession.TenantId.HasValue)
            {
                return false;
            }

            return SettingManager.GetSettingValue<bool>(AppSettings.TenantManagement.AllowSelfRegistration);
        }

        #endregion

        #region ForgotPassword / ResetPassword

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SendPasswordResetLink(SendPasswordResetLinkViewModel model)
        {
            await _accountAppService.SendPasswordResetCode(
                new SendPasswordResetCodeInput
                {
                    EmailAddress = model.EmailAddress
                });

            return Json(new AjaxResponse());
        }

        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            await SwitchToTenantIfNeeded(model.TenantId);

            var user = await _userManager.GetUserByIdAsync(model.UserId);
            if (user == null || user.PasswordResetCode.IsNullOrEmpty() || user.PasswordResetCode != model.ResetCode)
            {
                throw new UserFriendlyException(L("InvalidPasswordResetCode"), L("InvalidPasswordResetCode_Detail"));
            }

            model.PasswordComplexitySetting = await _passwordComplexitySettingStore.GetSettingsAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordInput input)
        {
            var output = await _accountAppService.ResetPassword(input);

            if (output.CanLogin)
            {
                var user = await _userManager.GetUserByIdAsync(input.UserId);
                await _signInManager.SignInAsync(user, false);

                if (!string.IsNullOrEmpty(input.SingleSignIn) && input.SingleSignIn.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    user.SetSignInToken();
                    var returnUrl = AddSingleSignInParametersToReturnUrl(input.ReturnUrl, user.SignInToken, user.Id, user.TenantId);
                    return Redirect(returnUrl);
                }
            }

            return Redirect(NormalizeReturnUrl(input.ReturnUrl));
        }

        #endregion

        #region Email activation / confirmation

        public ActionResult EmailActivation()
        {
            return View();
        }

        [HttpPost]
        [UnitOfWork]
        public virtual async Task<JsonResult> SendEmailActivationLink(SendEmailActivationLinkInput model)
        {
            await _accountAppService.SendEmailActivationLink(model);
            return Json(new AjaxResponse());
        }

        [UnitOfWork]
        public virtual async Task<ActionResult> EmailConfirmation(EmailConfirmationViewModel input)
        {
            await SwitchToTenantIfNeeded(input.TenantId);
            await _accountAppService.ActivateEmail(input);
            return RedirectToAction(
                "Login",
                new
                {
                    successMessage = L("YourEmailIsConfirmedMessage"),
                    userNameOrEmailAddress = (await _userManager.GetUserByIdAsync(input.UserId)).UserName
                });
        }

        #endregion

        #region External Login

        [HttpPost]
        public ActionResult ExternalLogin(string provider, string returnUrl, string ss = "")
        {
            var redirectUrl = Url.Action(
                "ExternalLoginCallback",
                "Account",
                new
                {
                    ReturnUrl = returnUrl,
                    authSchema = provider,
                    ss = ss
                });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties, provider);
        }

        [UnitOfWork]
        public virtual async Task<ActionResult> ExternalLoginCallback(string returnUrl, string remoteError = null, string ss = "")
        {
            returnUrl = NormalizeReturnUrl(returnUrl);

            if (remoteError != null)
            {
                Logger.Error("Remote Error in ExternalLoginCallback: " + remoteError);
                throw new UserFriendlyException(L("CouldNotCompleteLoginOperation"));
            }

            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                Logger.Warn("Could not get information from external login.");
                return RedirectToAction(nameof(Login));
            }

            var tenancyName = GetTenancyNameOrNull();

            var loginResult = await _logInManager.LoginAsync(externalLoginInfo, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    {
                        await _signInManager.SignInAsync(loginResult.Identity, false);

                        if (!string.IsNullOrEmpty(ss) && ss.Equals("true", StringComparison.OrdinalIgnoreCase) && loginResult.Result == AbpLoginResultType.Success)
                        {
                            loginResult.User.SetSignInToken();
                            returnUrl = AddSingleSignInParametersToReturnUrl(returnUrl, loginResult.User.SignInToken, loginResult.User.Id, loginResult.User.TenantId);
                        }

                        return Redirect(returnUrl);
                    }
                case AbpLoginResultType.UnknownExternalLogin:
                    return await RegisterForExternalLogin(externalLoginInfo);
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                        loginResult.Result,
                        externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email) ?? externalLoginInfo.ProviderKey,
                        tenancyName
                    );
            }
        }

        private async Task<ActionResult> RegisterForExternalLogin(ExternalLoginInfo externalLoginInfo)
        {
            var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            var nameinfo = ExternalLoginInfoHelper.GetNameAndSurnameFromClaims(externalLoginInfo.Principal.Claims.ToList(), _identityOptions);

            var viewModel = new RegisterViewModel
            {
                EmailAddress = email,
                Name = nameinfo.name,
                Surname = nameinfo.surname,
                IsExternalLogin = true,
                ExternalLoginAuthSchema = externalLoginInfo.LoginProvider
            };

            if (nameinfo.name != null &&
                nameinfo.surname != null &&
                email != null)
            {
                return await Register(viewModel);
            }

            return RegisterView(viewModel);
        }

        #endregion

        #region Impersonation

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users_Impersonation)]
        public virtual async Task<JsonResult> Impersonate([FromBody] ImpersonateInput input)
        {
            var output = await _accountAppService.Impersonate(input);

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var user = await _userManager.FindByIdAsync(input.UserId.ToString());
                if (user != null)
                {
                    await AddClaimsEccpAbpSession(input.TenantId.Value, user);
                }
            }

            await _signInManager.SignOutAsync();

            return Json(new AjaxResponse
            {
                TargetUrl = _webUrlService.GetSiteRootAddress(output.TenancyName) + "Account/ImpersonateSignIn?tokenId=" + output.ImpersonationToken
            });
        }

        [UnitOfWork]
        public virtual async Task<ActionResult> ImpersonateSignIn(string tokenId)
        {
            var result = await _impersonationManager.GetImpersonatedUserAndIdentity(tokenId);
            await _signInManager.SignInAsync(result.Identity, false);
            return RedirectToAppHome();
        }

        public virtual JsonResult IsImpersonatedLogin()
        {
            return Json(new AjaxResponse { Result = AbpSession.ImpersonatorUserId.HasValue });
        }

        public virtual async Task<JsonResult> BackToImpersonator()
        {
            var output = await _accountAppService.BackToImpersonator();

            await _signInManager.SignOutAsync();

            return Json(new AjaxResponse
            {
                TargetUrl = _webUrlService.GetSiteRootAddress(output.TenancyName) + "Account/ImpersonateSignIn?tokenId=" + output.ImpersonationToken
            });
        }

        #endregion

        #region Linked Account

        [UnitOfWork]
        [AbpMvcAuthorize]
        public virtual async Task<JsonResult> SwitchToLinkedAccount([FromBody] SwitchToLinkedAccountInput model)
        {
            var output = await _accountAppService.SwitchToLinkedAccount(model);

            await _signInManager.SignOutAsync();

            return Json(new AjaxResponse
            {
                TargetUrl = _webUrlService.GetSiteRootAddress(output.TenancyName) + "Account/SwitchToLinkedAccountSignIn?tokenId=" + output.SwitchAccountToken
            });
        }

        [UnitOfWork]
        public virtual async Task<ActionResult> SwitchToLinkedAccountSignIn(string tokenId)
        {
            var result = await _userLinkManager.GetSwitchedUserAndIdentity(tokenId);
            result.User.LastLoginTime = Clock.Now;

            await _signInManager.SignInAsync(result.Identity, false);
            return RedirectToAppHome();
        }

        #endregion

        #region Change Tenant

        public async Task<ActionResult> TenantChangeModal()
        {
            var loginInfo = await _sessionCache.GetCurrentLoginInformationsAsync();
            return View("/Views/Shared/Components/TenantChange/_ChangeModal.cshtml", new ChangeModalViewModel
            {
                TenancyName = loginInfo.Tenant?.TenancyName
            });
        }

        #endregion

        #region Common

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private void CheckCurrentTenant(int? tenantId)
        {
            if (AbpSession.TenantId != tenantId)
            {
                throw new Exception($"Current tenant is different than given tenant. AbpSession.TenantId: {AbpSession.TenantId}, given tenantId: {tenantId}");
            }
        }

        private async Task SwitchToTenantIfNeeded(int? tenantId)
        {
            if (tenantId != AbpSession.TenantId)
            {
                if (_webUrlService.SupportsTenancyNameInUrl)
                {
                    throw new InvalidOperationException($"Given tenantid ({tenantId}) does not match to tenant's URL!");
                }

                SetTenantIdCookie(tenantId);
                CurrentUnitOfWork.SetTenantId(tenantId);
                await _signInManager.SignOutAsync();
            }
        }

        #endregion

        #region Helpers

        public ActionResult RedirectToAppHome()
        {
            return RedirectToAction("Index", "Home", new { area = "AppAreaName" });
        }

        public string GetAppHomeUrl()
        {
            return Url.Action("Index", "Home", new { area = "AppAreaName" });
        }

        private string NormalizeReturnUrl(string returnUrl, Func<string> defaultValueBuilder = null)
        {
            if (defaultValueBuilder == null)
            {
                defaultValueBuilder = GetAppHomeUrl;
            }

            if (returnUrl.IsNullOrEmpty())
            {
                return defaultValueBuilder();
            }

            if (Url.IsLocalUrl(returnUrl) || _webUrlService.GetRedirectAllowedExternalWebSites().Any(returnUrl.Contains))
            {
                return returnUrl;
            }

            return defaultValueBuilder();
        }

        #endregion

        #region Etc

        [AbpMvcAuthorize]
        public async Task<ActionResult> TestNotification(string message = "", string severity = "info")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test notification, created at " + Clock.Now;
            }

            await _appNotifier.SendMessageAsync(
                AbpSession.ToUserIdentifier(),
                message,
                severity.ToPascalCase().ToEnum<NotificationSeverity>()
                );

            return Content("Sent notification: " + message);
        }

        #endregion

        #region Query tenant status

        public ActionResult QueryTenantStatus()
        {
            this.ViewBag.DisableTenantChange = true;
            return this.View();
        }

        [HttpPost]
        [UnitOfWork]
        public virtual async Task<JsonResult> QueryTenantStatus(QueryTenantStatusInput model)
        {
            var result = await this._accountAppService.QueryTenantStatus(model);
            return this.Json(new AjaxResponse(result));
        }

        #endregion
    }
}
