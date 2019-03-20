using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Extensions;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Identity;
using Sinodom.ElevatorCloud.Authorization.Accounts.Dto;
using Sinodom.ElevatorCloud.Authorization.Impersonation;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.Configuration;
using Sinodom.ElevatorCloud.Debugging;
using Sinodom.ElevatorCloud.MultiTenancy;
using Sinodom.ElevatorCloud.Security.Recaptcha;
using Sinodom.ElevatorCloud.Url;

namespace Sinodom.ElevatorCloud.Authorization.Accounts
{
    using System.Linq;

    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;

    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.Editions;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions;

    public class AccountAppService : ElevatorCloudAppServiceBase, IAccountAppService
    {
        public IAppUrlService AppUrlService { get; set; }

        public IRecaptchaValidator RecaptchaValidator { get; set; }

        private readonly IUserEmailer _userEmailer;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IImpersonationManager _impersonationManager;
        private readonly IUserLinkManager _userLinkManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IWebUrlService _webUrlService;
        private readonly EditionManager _editionManager;
        private readonly IRepository<ECCPBaseMaintenanceCompany> _eccpBaseMaintenanceCompanyRepository;
        private readonly IRepository<ECCPBasePropertyCompany> _eccpBasePropertyCompanyRepository;
        private readonly IRepository<EccpMaintenanceCompanyAuditLog> _eccpMaintenanceCompanyAuditLogRepository;
        private readonly IRepository<EccpPropertyCompanyAuditLog> _eccpPropertyCompanyAuditLogRepository;

        public AccountAppService(
            IUserEmailer userEmailer,
            UserRegistrationManager userRegistrationManager,
            IImpersonationManager impersonationManager,
            IUserLinkManager userLinkManager,
            IPasswordHasher<User> passwordHasher,
            IWebUrlService webUrlService,
            IRepository<ECCPBaseMaintenanceCompany> eccpBaseMaintenanceCompanyRepository,
            IRepository<ECCPBasePropertyCompany> eccpBasePropertyCompanyRepository,
            IRepository<EccpMaintenanceCompanyAuditLog> eccpMaintenanceCompanyAuditLogRepository,
            IRepository<EccpPropertyCompanyAuditLog> eccpPropertyCompanyAuditLogRepository,
            EditionManager editionManager)
        {
            _userEmailer = userEmailer;
            _userRegistrationManager = userRegistrationManager;
            _impersonationManager = impersonationManager;
            _userLinkManager = userLinkManager;
            _passwordHasher = passwordHasher;
            _webUrlService = webUrlService;
            _eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
            _eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
            _eccpMaintenanceCompanyAuditLogRepository = eccpMaintenanceCompanyAuditLogRepository;
            _eccpPropertyCompanyAuditLogRepository = eccpPropertyCompanyAuditLogRepository;
            _editionManager = editionManager;

            AppUrlService = NullAppUrlService.Instance;
            RecaptchaValidator = NullRecaptchaValidator.Instance;
        }

        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id, _webUrlService.GetServerRootAddress(input.TenancyName));
        }

        public Task<int?> ResolveTenantId(ResolveTenantIdInput input)
        {
            if (string.IsNullOrEmpty(input.c))
            {
                return Task.FromResult(AbpSession.TenantId);
            }

            var parameters = SimpleStringCipher.Instance.Decrypt(input.c);
            var query = HttpUtility.ParseQueryString(parameters);

            if (query["tenantId"] == null)
            {
                return Task.FromResult<int?>(null);
            }

            var tenantId = Convert.ToInt32(query["tenantId"]) as int?;
            return Task.FromResult(tenantId);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            if (UseCaptchaOnRegistration())
            {
                await RecaptchaValidator.ValidateAsync(input.CaptchaResponse);
            }

            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                true,
                AppUrlService.CreateEmailActivationUrlFormat(AbpSession.TenantId)
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }

        public async Task SendPasswordResetCode(SendPasswordResetCodeInput input)
        {
            var user = await GetUserByChecking(input.EmailAddress);
            user.SetNewPasswordResetCode();
            await _userEmailer.SendPasswordResetLinkAsync(
                user,
                AppUrlService.CreatePasswordResetUrlFormat(AbpSession.TenantId)
                );
        }

        public async Task<ResetPasswordOutput> ResetPassword(ResetPasswordInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            if (user == null || user.PasswordResetCode.IsNullOrEmpty() || user.PasswordResetCode != input.ResetCode)
            {
                throw new UserFriendlyException(L("InvalidPasswordResetCode"), L("InvalidPasswordResetCode_Detail"));
            }

            await UserManager.InitializeOptionsAsync(AbpSession.TenantId);
            CheckErrors(await UserManager.ChangePasswordAsync(user, input.Password));
            user.PasswordResetCode = null;
            user.IsEmailConfirmed = true;
            user.ShouldChangePasswordOnNextLogin = false;

            await UserManager.UpdateAsync(user);

            return new ResetPasswordOutput
            {
                CanLogin = user.IsActive,
                UserName = user.UserName
            };
        }

        public async Task SendEmailActivationLink(SendEmailActivationLinkInput input)
        {
            var user = await GetUserByChecking(input.EmailAddress);
            user.SetNewEmailConfirmationCode();
            await _userEmailer.SendEmailActivationLinkAsync(
                user,
                AppUrlService.CreateEmailActivationUrlFormat(AbpSession.TenantId)
            );
        }

        public async Task ActivateEmail(ActivateEmailInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            if (user == null || user.EmailConfirmationCode.IsNullOrEmpty() || user.EmailConfirmationCode != input.ConfirmationCode)
            {
                throw new UserFriendlyException(L("InvalidEmailConfirmationCode"), L("InvalidEmailConfirmationCode_Detail"));
            }

            user.IsEmailConfirmed = true;
            user.EmailConfirmationCode = null;

            await UserManager.UpdateAsync(user);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Impersonation)]
        public virtual async Task<ImpersonateOutput> Impersonate(ImpersonateInput input)
        {
            return new ImpersonateOutput
            {
                ImpersonationToken = await _impersonationManager.GetImpersonationToken(input.UserId, input.TenantId),
                TenancyName = await GetTenancyNameOrNullAsync(input.TenantId)
            };
        }

        public virtual async Task<ImpersonateOutput> BackToImpersonator()
        {
            return new ImpersonateOutput
            {
                ImpersonationToken = await _impersonationManager.GetBackToImpersonatorToken(),
                TenancyName = await GetTenancyNameOrNullAsync(AbpSession.ImpersonatorTenantId)
            };
        }

        public virtual async Task<SwitchToLinkedAccountOutput> SwitchToLinkedAccount(SwitchToLinkedAccountInput input)
        {
            if (!await _userLinkManager.AreUsersLinked(AbpSession.ToUserIdentifier(), input.ToUserIdentifier()))
            {
                throw new Exception(L("This account is not linked to your account"));
            }

            return new SwitchToLinkedAccountOutput
            {
                SwitchAccountToken = await _userLinkManager.GetAccountSwitchToken(input.TargetUserId, input.TargetTenantId),
                TenancyName = await GetTenancyNameOrNullAsync(input.TargetTenantId)
            };
        }

        private bool UseCaptchaOnRegistration()
        {
            if (DebugHelper.IsDebug)
            {
                return false;
            }

            return SettingManager.GetSettingValue<bool>(AppSettings.UserManagement.UseCaptchaOnRegistration);
        }

        private async Task<Tenant> GetActiveTenantAsync(int tenantId)
        {
            var tenant = await TenantManager.FindByIdAsync(tenantId);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("UnknownTenantId{0}", tenantId));
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIdIsNotActive{0}", tenantId));
            }

            return tenant;
        }

        private async Task<string> GetTenancyNameOrNullAsync(int? tenantId)
        {
            return tenantId.HasValue ? (await GetActiveTenantAsync(tenantId.Value)).TenancyName : null;
        }

        private async Task<User> GetUserByChecking(string inputEmailAddress)
        {
            var user = await UserManager.FindByEmailAsync(inputEmailAddress);
            if (user == null)
            {
                throw new UserFriendlyException(L("InvalidEmailAddress"));
            }

            return user;
        }

        public async Task<QueryTenantStatusOutput> QueryTenantStatus(QueryTenantStatusInput input)
        {
            var tenant = await this.TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new QueryTenantStatusOutput
                           {
                               StatusCode = -1, StatusName = this.L("ThereIsNoTenantDefinedWithName{0}", input.TenancyName)
                           };
            }

            if (tenant.IsActive)
            {
                return new QueryTenantStatusOutput
                           {
                               StatusCode = 1,
                               StatusName = this.L("QueryTenantStatus_Success{0}", input.TenancyName)
                           };
            }

            var edition = (SubscribableEdition)await this._editionManager.FindByIdAsync(tenant.EditionId.GetValueOrDefault(0));
            if (edition == null)
            {
                throw new UserFriendlyException(this.L("QueryTenantStatus_EditionError"));
            }

            var dictEditionTypes = (await this._editionManager.GetAllEditionTypeAsync()).ToDictionary(e => e.Id);
            if (dictEditionTypes[edition.ECCPEditionsTypeId.GetValueOrDefault(0)].Name == "维保公司")
            {
                using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    var maintenanceCompany =
                        await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(
                            e => e.TenantId == tenant.Id);
                    if (maintenanceCompany == null)
                    {
                        throw new UserFriendlyException(this.L("QueryTenantStatus_CompanyError"));
                    }

                    if (maintenanceCompany.IsAudit == null)
                    {
                        return new QueryTenantStatusOutput
                                   {
                                       StatusCode = 2,
                                       StatusName = this.L("QueryTenantStatus_NotYet{0}", input.TenancyName)
                                   };
                    }

                    var maintenanceCompanyAuditLog = this._eccpMaintenanceCompanyAuditLogRepository.GetAll()
                        .OrderByDescending(e => e.Id).FirstOrDefault(e => e.MaintenanceCompanyId == maintenanceCompany.Id);

                    return new QueryTenantStatusOutput
                               {
                                   StatusCode = 0,
                                   StatusName = this.L("QueryTenantStatus_Refuse{0}", input.TenancyName),
                                   StatusRemarks = maintenanceCompanyAuditLog.Remarks
                               };
                }
            }

            if (dictEditionTypes[edition.ECCPEditionsTypeId.GetValueOrDefault(0)].Name == "物业公司")
            {
                using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    var propertyCompany =
                        await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(e => e.TenantId == tenant.Id);
                    if (propertyCompany == null)
                    {
                        throw new UserFriendlyException(this.L("QueryTenantStatus_CompanyError"));
                    }

                    if (propertyCompany.IsAudit == null)
                    {
                        return new QueryTenantStatusOutput
                                   {
                                       StatusCode = 2,
                                       StatusName = this.L("QueryTenantStatus_NotYet{0}", input.TenancyName)
                                   };
                    }

                    var propertyCompanyAuditLog = this._eccpPropertyCompanyAuditLogRepository.GetAll()
                        .OrderByDescending(e => e.Id).FirstOrDefault(e => e.PropertyCompanyId == propertyCompany.Id);

                    return new QueryTenantStatusOutput
                               {
                                   StatusCode = 0,
                                   StatusName = this.L("QueryTenantStatus_Refuse{0}", input.TenancyName),
                                   StatusRemarks = propertyCompanyAuditLog.Remarks
                               };
                }
            }

            return new QueryTenantStatusOutput
                       {
                           StatusCode = -2,
                           StatusName = "NullNullNull"
                       };
        }
    }
}
