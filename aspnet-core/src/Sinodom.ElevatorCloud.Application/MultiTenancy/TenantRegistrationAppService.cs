using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using Abp.Zero.Configuration;
using Sinodom.ElevatorCloud.Configuration;
using Sinodom.ElevatorCloud.Debugging;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.Editions.Dto;
using Sinodom.ElevatorCloud.Features;
using Sinodom.ElevatorCloud.MultiTenancy.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.Payments;
using Sinodom.ElevatorCloud.MultiTenancy.Payments.Cache;
using Sinodom.ElevatorCloud.Notifications;
using Sinodom.ElevatorCloud.Security.Recaptcha;
using Sinodom.ElevatorCloud.Url;
using Abp.Extensions;
using Sinodom.ElevatorCloud.MultiTenancy.Payments.Dto;

namespace Sinodom.ElevatorCloud.MultiTenancy
{
    using System.IO;

    using Sinodom.ElevatorCloud.Storage;

    public class TenantRegistrationAppService : ElevatorCloudAppServiceBase, ITenantRegistrationAppService
    {
        public IAppUrlService AppUrlService { get; set; }

        private const int MaxProfilPictureBytes = 5048576; //5MB
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IRecaptchaValidator _recaptchaValidator;
        private readonly EditionManager _editionManager;
        private readonly IAppNotifier _appNotifier;
        private readonly ILocalizationContext _localizationContext;
        private readonly TenantManager _tenantManager;
        private readonly ISubscriptionPaymentRepository _subscriptionPaymentRepository;
        private readonly IPaymentGatewayManagerFactory _paymentGatewayManagerFactory;
        private readonly IPaymentCache _paymentCache;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        public TenantRegistrationAppService(
            IMultiTenancyConfig multiTenancyConfig,
            IRecaptchaValidator recaptchaValidator,
            EditionManager editionManager,
            IAppNotifier appNotifier,
            ILocalizationContext localizationContext,
            TenantManager tenantManager,
            ISubscriptionPaymentRepository subscriptionPaymentRepository,
            IPaymentGatewayManagerFactory paymentGatewayManagerFactory,
            IPaymentCache paymentCache,
            IBinaryObjectManager binaryObjectManager,
            ITempFileCacheManager tempFileCacheManager)
        {
            _multiTenancyConfig = multiTenancyConfig;
            _recaptchaValidator = recaptchaValidator;
            _editionManager = editionManager;
            _appNotifier = appNotifier;
            _localizationContext = localizationContext;
            _tenantManager = tenantManager;
            _subscriptionPaymentRepository = subscriptionPaymentRepository;
            _paymentGatewayManagerFactory = paymentGatewayManagerFactory;
            _paymentCache = paymentCache;
            _binaryObjectManager = binaryObjectManager;
            _tempFileCacheManager = tempFileCacheManager;

            AppUrlService = NullAppUrlService.Instance;
        }

        public async Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input)
        {
            if (input.EditionId.HasValue)
            {
                await CheckEditionSubscriptionAsync(input.EditionId.Value, input.SubscriptionStartType, input.Gateway, input.PaymentId);
            }
            else
            {
                await CheckRegistrationWithoutEdition();
            }

            using (CurrentUnitOfWork.SetTenantId(null))
            {
                CheckTenantRegistrationIsEnabled();

                if (UseCaptchaOnRegistration())
                {
                    await _recaptchaValidator.ValidateAsync(input.CaptchaResponse);
                }

                if (input.BusinessLicenseIdFileToken != null && input.AptitudePhotoIdFileToken != null)
                {
                    input.BusinessLicenseId = await this.SaveFileAndGetFileId(input.BusinessLicenseIdFileToken);
                    input.AptitudePhotoId = await this.SaveFileAndGetFileId(input.AptitudePhotoIdFileToken);
                }

                //Getting host-specific settings
                var isNewRegisteredTenantActiveByDefault = await SettingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.TenantManagement.IsNewRegisteredTenantActiveByDefault);
                var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueForApplicationAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

                DateTime? subscriptionEndDate = null;
                var isInTrialPeriod = false;

                if (input.EditionId.HasValue)
                {
                    isInTrialPeriod = input.SubscriptionStartType == SubscriptionStartType.Trial;

                    if (isInTrialPeriod)
                    {
                        var edition = (SubscribableEdition)await _editionManager.GetByIdAsync(input.EditionId.Value);
                        subscriptionEndDate = Clock.Now.AddDays(edition.TrialDayCount ?? 0);
                    }
                }

                var tenantId = await _tenantManager.CreateWithAdminUserAsync(
                                   input.TenancyName,
                                   input.Name,
                                   input.AdminPassword,
                                   input.AdminEmailAddress,
                                   null,
                                   isNewRegisteredTenantActiveByDefault,
                                   input.EditionId,
                                   false,
                                   false,
                                   subscriptionEndDate,
                                   isInTrialPeriod,
                                   AppUrlService.CreateEmailActivationUrlFormat(input.TenancyName),
                                   input.BusinessLicenseId,
                                   input.AptitudePhotoId,
                                   input.LegalPerson,
                                   input.Mobile,
                                   input.IsMember);

                Tenant tenant;

                if (input.SubscriptionStartType == SubscriptionStartType.Paid)
                {
                    if (!input.Gateway.HasValue)
                    {
                        throw new Exception("Gateway is missing!");
                    }

                    var payment = await _subscriptionPaymentRepository.GetByGatewayAndPaymentIdAsync(
                        input.Gateway.Value,
                        input.PaymentId
                    );

                    tenant = await _tenantManager.UpdateTenantAsync(
                        tenantId,
                        true,
                        false,
                        payment.PaymentPeriodType,
                        payment.EditionId,
                        EditionPaymentType.NewRegistration);

                    await _subscriptionPaymentRepository.UpdateByGatewayAndPaymentIdAsync(input.Gateway.Value,
                        input.PaymentId, tenantId, SubscriptionPaymentStatus.Completed);
                }
                else
                {
                    tenant = await TenantManager.GetByIdAsync(tenantId);
                }

                await _appNotifier.NewTenantRegisteredAsync(tenant);

                if (input.EditionId.HasValue && input.Gateway.HasValue && !input.PaymentId.IsNullOrEmpty())
                {
                    _paymentCache.RemoveCacheItem(input.Gateway.Value, input.PaymentId);
                }

                return new RegisterTenantOutput
                {
                    TenantId = tenant.Id,
                    TenancyName = input.TenancyName,
                    Name = input.Name,
                    UserName = AbpUserBase.AdminUserName,
                    EmailAddress = input.AdminEmailAddress,
                    IsActive = tenant.IsActive,
                    IsEmailConfirmationRequired = isEmailConfirmationRequiredForLogin,
                    IsTenantActive = tenant.IsActive
                };
            }
        }

        private async Task CheckRegistrationWithoutEdition()
        {
            var editions = await _editionManager.GetAllAsync();
            if (editions.Any())
            {
                throw new Exception("Tenant registration is not allowed without edition because there are editions defined !");
            }
        }

        public async Task<EditionsSelectOutput> GetEditionsForSelect()
        {
            var features = FeatureManager
                .GetAll()
                .Where(feature => (feature[FeatureMetadata.CustomFeatureKey] as FeatureMetadata)?.IsVisibleOnPricingTable ?? false);

            var flatFeatures = ObjectMapper
                .Map<List<FlatFeatureSelectDto>>(features)
                .OrderBy(f => f.DisplayName)
                .ToList();

            var editions = (await _editionManager.GetAllAsync())
                .Cast<SubscribableEdition>()
                .Where(e => e.IsRegister)
                .OrderBy(e => e.MonthlyPrice)
                .ToList();

            var featureDictionary = features.ToDictionary(feature => feature.Name, f => f);

            var editionWithFeatures = new List<EditionWithFeaturesDto>();
            foreach (var edition in editions)
            {
                editionWithFeatures.Add(await CreateEditionWithFeaturesDto(edition, featureDictionary));
            }

            int? tenantEditionId = null;
            if (AbpSession.UserId.HasValue)
            {
                tenantEditionId = (await _tenantManager.GetByIdAsync(AbpSession.GetTenantId()))
                    .EditionId;
            }

            return new EditionsSelectOutput
            {
                AllFeatures = flatFeatures,
                EditionsWithFeatures = editionWithFeatures,
                TenantEditionId = tenantEditionId
            };
        }

        public async Task<EditionSelectDto> GetEdition(int editionId)
        {
            var edition = await _editionManager.GetByIdAsync(editionId);
            var editionDto = ObjectMapper.Map<EditionSelectDto>(edition);
            foreach (var paymentGateway in Enum.GetValues(typeof(SubscriptionPaymentGatewayType)).Cast<SubscriptionPaymentGatewayType>())
            {
                using (var paymentGatewayManager = _paymentGatewayManagerFactory.Create(paymentGateway))
                {
                    var additionalData = await paymentGatewayManager.Object.GetAdditionalPaymentData(ObjectMapper.Map<SubscribableEdition>(edition));
                    editionDto.AdditionalData.Add(paymentGateway, additionalData);
                }
            }

            return editionDto;
        }

        private async Task<EditionWithFeaturesDto> CreateEditionWithFeaturesDto(SubscribableEdition edition, Dictionary<string, Feature> featureDictionary)
        {
            return new EditionWithFeaturesDto
            {
                Edition = ObjectMapper.Map<EditionSelectDto>(edition),
                FeatureValues = (await _editionManager.GetFeatureValuesAsync(edition.Id))
                    .Where(featureValue => featureDictionary.ContainsKey(featureValue.Name))
                    .Select(fv => new NameValueDto(
                        fv.Name,
                        featureDictionary[fv.Name].GetValueText(fv.Value, _localizationContext))
                    )
                    .ToList()
            };
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

        private bool IsSelfRegistrationEnabled()
        {
            return SettingManager.GetSettingValueForApplication<bool>(AppSettings.TenantManagement.AllowSelfRegistration);
        }

        private bool UseCaptchaOnRegistration()
        {
            if (DebugHelper.IsDebug)
            {
                return false;
            }

            return SettingManager.GetSettingValueForApplication<bool>(AppSettings.TenantManagement.UseCaptchaOnRegistration);
        }

        private async Task CheckEditionSubscriptionAsync(int editionId, SubscriptionStartType subscriptionStartType, SubscriptionPaymentGatewayType? gateway, string paymentId)
        {
            var edition = await _editionManager.GetByIdAsync(editionId) as SubscribableEdition;
            
            CheckSubscriptionStart(edition, subscriptionStartType);
            CheckPaymentCache(edition, subscriptionStartType, gateway, paymentId);
        }

        private void CheckPaymentCache(SubscribableEdition edition, SubscriptionStartType subscriptionStartType, SubscriptionPaymentGatewayType? gateway, string paymentId)
        {
            if (edition.IsFree || subscriptionStartType != SubscriptionStartType.Paid)
            {
                return;
            }

            if (!gateway.HasValue)
            {
                throw new Exception("Gateway cannot be empty !");
            }

            if (paymentId.IsNullOrEmpty())
            {
                throw new Exception("PaymentId cannot be empty !");
            }

            var paymentCacheItem = _paymentCache.GetCacheItemOrNull(gateway.Value, paymentId);
            if (paymentCacheItem == null)
            {
                throw new UserFriendlyException(L("PaymentMightBeExpiredWarning"));
            }
        }

        private static void CheckSubscriptionStart(SubscribableEdition edition, SubscriptionStartType subscriptionStartType)
        {
            switch (subscriptionStartType)
            {
                case SubscriptionStartType.Free:
                    if (!edition.IsFree)
                    {
                        throw new Exception("This is not a free edition !");
                    }
                    break;
                case SubscriptionStartType.Trial:
                    if (!edition.HasTrial())
                    {
                        throw new Exception("Trial is not available for this edition !");
                    }
                    break;
                case SubscriptionStartType.Paid:
                    if (edition.IsFree)
                    {
                        throw new Exception("This is a free edition and cannot be subscribed as paid !");
                    }
                    break;
            }
        }

        private async Task<Guid> SaveFileAndGetFileId(string fileToken)
        {
            Guid fileId = new Guid();

            // FileToken 4811d3f3-0bfa-4672-b875-2d47299d175d.jpg 时，为单元测试，不进行判断
            if (!string.IsNullOrWhiteSpace(fileToken) && fileToken != "4811d3f3-0bfa-4672-b875-2d47299d175d.jpg")
            {
                byte[] byteArray;
                var imageBytes = this._tempFileCacheManager.GetFile(fileToken);
                if (imageBytes == null)
                {
                    throw new UserFriendlyException(L("ThereIsNoSuchImageFileWithTheToken", fileToken));
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
                await this._binaryObjectManager.SaveAsync(storedFile);

                fileId = storedFile.Id;
            }

            // FileName的值为 4811d3f3-0bfa-4672-b875-2d47299d175d.jpg 时，为单元测试，创建一个假的Guid
            if (fileToken == "4811d3f3-0bfa-4672-b875-2d47299d175d.jpg")
            {
                fileId = Guid.NewGuid();
            }

            return fileId;
        }
    }
}
