using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp;
using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Sinodom.ElevatorCloud.Authorization.Roles;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.MultiTenancy.Demo;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Runtime.Security;
using Microsoft.AspNetCore.Identity;
using Sinodom.ElevatorCloud.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq.Expressions;
using Abp.Authorization;
using Abp.Runtime.Session;
using Abp.Runtime.Validation;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.EccpBaseSaicUnits;
using Sinodom.ElevatorCloud.MultiTenancy.Payments;

namespace Sinodom.ElevatorCloud.MultiTenancy
{
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions;

    /// <summary>
    /// Tenant manager.
    /// </summary>
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public IAbpSession AbpSession { get; set; }

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IUserEmailer _userEmailer;
        private readonly TenantDemoDataBuilder _demoDataBuilder;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IAppNotifier _appNotifier;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<SubscriptionPayment, long> _subscriptionPaymentRepository;
        private readonly IRepository<SubscribableEdition> _subscribableEditionRepository;

        private readonly IRepository<ECCPEditionsType> _eccpEditionsTypeRepository;
        private readonly IRepository<ECCPBaseMaintenanceCompany> _eccpMaintenanceCompanyRepository;
        private readonly IRepository<ECCPBasePropertyCompany> _eccpPropertyCompanyRepository;
        private readonly IRepository<EccpMaintenanceCompanyExtension> _eccpMaintenanceCompanyExtensionRepository;
        private readonly IRepository<EccpPropertyCompanyExtension> _eccpPropertyCompanyExtensionRepository;
        private readonly IRepository<EccpBaseSaicUnit> _eccpBaseSaicUnitRepository;
        private readonly IPermissionManager _permissionManager;

        public TenantManager(
            IRepository<Tenant> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            EditionManager editionManager,
            IUnitOfWorkManager unitOfWorkManager,
            RoleManager roleManager,
            IUserEmailer userEmailer,
            TenantDemoDataBuilder demoDataBuilder,
            UserManager userManager,
            INotificationSubscriptionManager notificationSubscriptionManager,
            IAppNotifier appNotifier,
            IAbpZeroFeatureValueStore featureValueStore,
            IAbpZeroDbMigrator abpZeroDbMigrator,
            IPasswordHasher<User> passwordHasher,
            IRepository<SubscriptionPayment, long> subscriptionPaymentRepository,
            IRepository<SubscribableEdition> subscribableEditionRepository,
            IRepository<ECCPBaseMaintenanceCompany> eccpMaintenanceCompanyRepository,
            IRepository<ECCPBasePropertyCompany> eccpPropertyCompanyRepository,
            IRepository<EccpMaintenanceCompanyExtension> eccpMaintenanceCompanyExtensionRepository,
            IRepository<EccpPropertyCompanyExtension> eccpPropertyCompanyExtensionRepository,
            IRepository<ECCPEditionsType> eccpEditionsTypeRepository,
            IPermissionManager permissionManager,
            IRepository<EccpBaseSaicUnit> eccpBaseSaicUnitRepository) : base(
                tenantRepository,
                tenantFeatureRepository,
                editionManager,
                featureValueStore
            )
        {
            AbpSession = NullAbpSession.Instance;

            _unitOfWorkManager = unitOfWorkManager;
            _roleManager = roleManager;
            _userEmailer = userEmailer;
            _demoDataBuilder = demoDataBuilder;
            _userManager = userManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _appNotifier = appNotifier;
            _abpZeroDbMigrator = abpZeroDbMigrator;
            _passwordHasher = passwordHasher;
            _subscriptionPaymentRepository = subscriptionPaymentRepository;
            _subscribableEditionRepository = subscribableEditionRepository;
            _eccpEditionsTypeRepository = eccpEditionsTypeRepository;
            _eccpMaintenanceCompanyRepository = eccpMaintenanceCompanyRepository;
            _eccpPropertyCompanyRepository = eccpPropertyCompanyRepository;
            _eccpMaintenanceCompanyExtensionRepository = eccpMaintenanceCompanyExtensionRepository;
            _eccpPropertyCompanyExtensionRepository = eccpPropertyCompanyExtensionRepository;
            _permissionManager = permissionManager;
            _eccpBaseSaicUnitRepository = eccpBaseSaicUnitRepository;
        }

        public async Task<int> CreateWithAdminUserAsync(
            string tenancyName,
            string name,
            string adminPassword,
            string adminEmailAddress,
            string connectionString,
            bool isActive,
            int? editionId,
            bool shouldChangePasswordOnNextLogin,
            bool sendActivationEmail,
            DateTime? subscriptionEndDate,
            bool isInTrialPeriod,
            string emailActivationLink,
            Guid? businessLicenseId,
            Guid? aptitudePhotoId,
            string legalPerson,
            string mobile,
            bool isMember)
        {
            int newTenantId;
            long newAdminId;

            await CheckEditionAsync(editionId, isInTrialPeriod);

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                //Create tenant
                var tenant = new Tenant(tenancyName, name)
                {
                    IsActive = isActive,
                    EditionId = editionId,
                    SubscriptionEndDateUtc = subscriptionEndDate?.ToUniversalTime(),
                    IsInTrialPeriod = isInTrialPeriod,
                    ConnectionString = connectionString.IsNullOrWhiteSpace() ? null : SimpleStringCipher.Instance.Encrypt(connectionString)
                };

                if (await this.TenantRepository.FirstOrDefaultAsync(t => t.Name == tenant.Name) != null)
                {
                    throw new UserFriendlyException(string.Format(this.LocalizationManager.GetSource(ElevatorCloudConsts.LocalizationSourceName).GetString("NameIsAlreadyTaken{0}"), tenant.Name));
                }

                await CreateAsync(tenant);
                await _unitOfWorkManager.Current.SaveChangesAsync(); //To get new tenant's id.

                //Create tenant database
                _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

                //We are working entities of new tenant, so changing tenant filter
                using (_unitOfWorkManager.Current.SetTenantId(tenant.Id))
                {
                    await this.CreateOrEditCompanyExtension(null, tenant.Id, legalPerson, mobile, businessLicenseId, aptitudePhotoId, isMember);

                    //Create static roles for new tenant
                    CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));
                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get static role ids

                    //grant all permissions to admin role
                    var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                    await _roleManager.GrantAllPermissionsAsync(adminRole);

                    //User role should be default
                    var userRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.User);
                    userRole.IsDefault = true;
                    CheckErrors(await _roleManager.UpdateAsync(userRole));

                    //Create admin user for the tenant
                    var adminUser = User.CreateTenantAdminUser(tenant.Id, adminEmailAddress);
                    adminUser.ShouldChangePasswordOnNextLogin = shouldChangePasswordOnNextLogin;
                    adminUser.IsActive = true;

                    if (adminPassword.IsNullOrEmpty())
                    {
                        adminPassword = User.CreateRandomPassword();
                    }
                    else
                    {
                        await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
                        foreach (var validator in _userManager.PasswordValidators)
                        {
                            CheckErrors(await validator.ValidateAsync(_userManager, adminUser, adminPassword));
                        }

                    }

                    adminUser.Password = _passwordHasher.HashPassword(adminUser, adminPassword);

                    CheckErrors(await _userManager.CreateAsync(adminUser));
                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get admin user's id

                    //Assign admin user to admin role!
                    CheckErrors(await _userManager.AddToRoleAsync(adminUser, adminRole.Name));

                    //Notifications
                    await _appNotifier.WelcomeToTheApplicationAsync(adminUser);

                    //Send activation email
                    if (sendActivationEmail)
                    {
                        adminUser.SetNewEmailConfirmationCode();
                        await _userEmailer.SendEmailActivationLinkAsync(adminUser, emailActivationLink, adminPassword);
                    }

                    await _unitOfWorkManager.Current.SaveChangesAsync();

                    await _demoDataBuilder.BuildForAsync(tenant);

                    newTenantId = tenant.Id;
                    newAdminId = adminUser.Id;
                }

                await uow.CompleteAsync();
            }

            //Used a second UOW since UOW above sets some permissions and _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync needs these permissions to be saved.
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                {
                    await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(new UserIdentifier(newTenantId, newAdminId));
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                    await uow.CompleteAsync();
                }
            }

            return newTenantId;
        }

        public async Task CheckEditionAsync(int? editionId, bool isInTrialPeriod)
        {
            if (!editionId.HasValue || !isInTrialPeriod)
            {
                return;
            }

            var edition = await _subscribableEditionRepository.GetAsync(editionId.Value);
            if (!edition.IsFree)
            {
                return;
            }

            var error = LocalizationManager.GetSource(ElevatorCloudConsts.LocalizationSourceName).GetString("FreeEditionsCannotHaveTrialVersions");
            throw new UserFriendlyException(error);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<SubscriptionPayment> GetLastPaymentAsync(Expression<Func<SubscriptionPayment, bool>> predicate)
        {
            return await _subscriptionPaymentRepository.GetAll().LastOrDefaultAsync(predicate);
        }

        public decimal GetUpgradePrice(SubscribableEdition currentEdition, SubscribableEdition targetEdition, int remainingDaysCount)
        {
            decimal additionalPrice;

            // If remainingDaysCount is longer than annual, then calculate price with using
            // both annual and monthly prices
            if (remainingDaysCount > (int)PaymentPeriodType.Annual)
            {
                var remainingsYearsCount = remainingDaysCount / (int)PaymentPeriodType.Annual;
                remainingDaysCount = remainingDaysCount % (int)PaymentPeriodType.Annual;

                additionalPrice = GetMonthlyCalculatedPrice(currentEdition, targetEdition, remainingDaysCount);
                additionalPrice += GetAnnualCalculatedPrice(currentEdition, targetEdition, remainingsYearsCount); // add yearly price to montly calculated price
            }
            else
            {
                additionalPrice = GetMonthlyCalculatedPrice(currentEdition, targetEdition, remainingDaysCount);
            }

            return additionalPrice;
        }

        public async Task<Tenant> UpdateTenantAsync(int tenantId, bool isActive, bool isInTrialPeriod, PaymentPeriodType? paymentPeriodType, int editionId, EditionPaymentType editionPaymentType)
        {
            var tenant = await FindByIdAsync(tenantId);

            tenant.IsActive = isActive;
            tenant.IsInTrialPeriod = isInTrialPeriod;
            tenant.EditionId = editionId;

            if (paymentPeriodType.HasValue)
            {
                tenant.UpdateSubscriptionDateForPayment(paymentPeriodType.Value, editionPaymentType);
            }

            return tenant;
        }

        public async Task<EndSubscriptionResult> EndSubscriptionAsync(Tenant tenant, SubscribableEdition edition, DateTime nowUtc)
        {
            if (tenant.EditionId == null || tenant.HasUnlimitedTimeSubscription())
            {
                throw new Exception($"Can not end tenant {tenant.TenancyName} subscription for {edition.DisplayName} tenant has unlimited time subscription!");
            }

            Debug.Assert(tenant.SubscriptionEndDateUtc != null, "tenant.SubscriptionEndDateUtc != null");

            var subscriptionEndDateUtc = tenant.SubscriptionEndDateUtc.Value;
            if (!tenant.IsInTrialPeriod)
            {
                subscriptionEndDateUtc = tenant.SubscriptionEndDateUtc.Value.AddDays(edition.WaitingDayAfterExpire ?? 0);
            }

            if (subscriptionEndDateUtc >= nowUtc)
            {
                throw new Exception($"Can not end tenant {tenant.TenancyName} subscription for {edition.DisplayName} since subscription has not expired yet!");
            }

            if (!tenant.IsInTrialPeriod && edition.ExpiringEditionId.HasValue)
            {
                tenant.EditionId = edition.ExpiringEditionId.Value;
                tenant.SubscriptionEndDateUtc = null;

                await UpdateAsync(tenant);

                return EndSubscriptionResult.AssignedToAnotherEdition;
            }

            tenant.IsActive = false;
            tenant.IsInTrialPeriod = false;

            await UpdateAsync(tenant);

            return EndSubscriptionResult.TenantSetInActive;
        }

        public async Task<EccpCompanyExtensionResult> GetEccpCompanyExtension(int tenantId)
        {
            var tenant = await this.GetByIdAsync(tenantId);
            var dictEditionTypes = await this._eccpEditionsTypeRepository.GetAll().ToDictionaryAsync(e => e.Id);
            var edition = await this._subscribableEditionRepository.FirstOrDefaultAsync(tenant.EditionId.GetValueOrDefault(1));
            EccpCompanyExtensionResult eccpCompanyExtensionResult = new EccpCompanyExtensionResult();
            if (dictEditionTypes[edition.ECCPEditionsTypeId.GetValueOrDefault(1)].Name == "物业公司")
            {
                var propertyCompany = await this._eccpPropertyCompanyRepository.FirstOrDefaultAsync(e => e.TenantId == tenantId);
                var propertyCompanyExtension = await this._eccpPropertyCompanyExtensionRepository.FirstOrDefaultAsync(e => e.PropertyCompanyId == propertyCompany.Id);

                eccpCompanyExtensionResult.AptitudePhotoId = propertyCompanyExtension.AptitudePhotoId;
                eccpCompanyExtensionResult.BusinessLicenseId = propertyCompanyExtension.BusinessLicenseId;
                eccpCompanyExtensionResult.IsMember = propertyCompanyExtension.IsMember;
                eccpCompanyExtensionResult.LegalPerson = propertyCompanyExtension.LegalPerson;
                eccpCompanyExtensionResult.Mobile = propertyCompanyExtension.Mobile;
            }
            else if (dictEditionTypes[edition.ECCPEditionsTypeId.GetValueOrDefault(1)].Name == "维保公司")
            {
                var maintenanceCompany = await this._eccpMaintenanceCompanyRepository.FirstOrDefaultAsync(e => e.TenantId == tenantId);
                var maintenanceCompanyExtension = await this._eccpMaintenanceCompanyExtensionRepository.FirstOrDefaultAsync(e => e.MaintenanceCompanyId == maintenanceCompany.Id);

                eccpCompanyExtensionResult.AptitudePhotoId = maintenanceCompanyExtension.AptitudePhotoId;
                eccpCompanyExtensionResult.BusinessLicenseId = maintenanceCompanyExtension.BusinessLicenseId;
                eccpCompanyExtensionResult.IsMember = maintenanceCompanyExtension.IsMember;
                eccpCompanyExtensionResult.LegalPerson = maintenanceCompanyExtension.LegalPerson;
                eccpCompanyExtensionResult.Mobile = maintenanceCompanyExtension.Mobile;
            }

            return eccpCompanyExtensionResult;
        }

        private static decimal GetMonthlyCalculatedPrice(SubscribableEdition currentEdition, SubscribableEdition upgradeEdition, int remainingDaysCount)
        {
            decimal currentUnusedPrice = 0;
            decimal upgradeUnusedPrice = 0;

            if (currentEdition.MonthlyPrice.HasValue)
            {
                currentUnusedPrice = (currentEdition.MonthlyPrice.Value / (int)PaymentPeriodType.Monthly) * remainingDaysCount;
            }

            if (upgradeEdition.MonthlyPrice.HasValue)
            {
                upgradeUnusedPrice = (upgradeEdition.MonthlyPrice.Value / (int)PaymentPeriodType.Monthly) * remainingDaysCount;
            }

            var additionalPrice = upgradeUnusedPrice - currentUnusedPrice;
            return additionalPrice;
        }

        private static decimal GetAnnualCalculatedPrice(SubscribableEdition currentEdition, SubscribableEdition upgradeEdition, int remainingYearsCount)
        {
            var currentUnusedPrice = (currentEdition.AnnualPrice ?? 0) * remainingYearsCount;
            var upgradeUnusedPrice = (upgradeEdition.AnnualPrice ?? 0) * remainingYearsCount;

            var additionalPrice = upgradeUnusedPrice - currentUnusedPrice;
            return additionalPrice;
        }

        private async Task CreateOrEditCompanyExtension(int? extensionId, int tenantId, string legalPerson, string mobile, Guid? businessLicenseId, Guid? aptitudePhotoId, bool isMember)
        {
            if (extensionId.HasValue)
            {

            }
            else
            {
                await this.CreateCompanyExtension(tenantId, legalPerson, mobile, businessLicenseId, aptitudePhotoId, isMember);
            }
        }

        private async Task CreateCompanyExtension(int tenantId, string legalPerson, string mobile, Guid? businessLicenseId, Guid? aptitudePhotoId, bool isMember)
        {
            var tenant = await this.GetByIdAsync(tenantId);
            var dictEditionTypes = await this._eccpEditionsTypeRepository.GetAll().ToDictionaryAsync(e => e.Id);
            var edition = await this._subscribableEditionRepository.FirstOrDefaultAsync(tenant.EditionId.GetValueOrDefault(1));
            int companyId = 0;
            if (dictEditionTypes[edition.ECCPEditionsTypeId.GetValueOrDefault(1)].Name == "物业公司")
            {
                var propertyCompany = await this._eccpPropertyCompanyRepository.FirstOrDefaultAsync(e => e.TenantId == tenantId);
                if (propertyCompany == null)
                {
                    companyId = await this._eccpPropertyCompanyRepository.InsertAndGetIdAsync(
                                    new ECCPBasePropertyCompany
                                    {
                                        Name = tenant.Name,
                                        Addresse = "未填写",
                                        Longitude = "未填写",
                                        Latitude = "未填写",
                                        Telephone = "未填写",
                                        OrgOrganizationalCode = "未填写",
                                        TenantId = tenantId
                                    });
                }
                else
                {
                    companyId = propertyCompany.Id;
                }

                // Use info manage role
                await this.CreateCompanyRole(tenantId, StaticRoleNames.UseTenants.UseInfoManage, StaticRoleNames.UseTenants.UseInfoManageDisplayName);

                // Use manage role
                await this.CreateCompanyRole(tenantId, StaticRoleNames.UseTenants.UseManage, StaticRoleNames.UseTenants.UseManageDisplayName);

                // Use leader role
                await this.CreateCompanyRole(tenantId, StaticRoleNames.UseTenants.UseLeader, StaticRoleNames.UseTenants.UseLeaderDisplayName);

                // Use user role
                await this.CreateCompanyRole(tenantId, StaticRoleNames.UseTenants.UseUser, StaticRoleNames.UseTenants.UseUserDisplayName);

                await this._eccpPropertyCompanyExtensionRepository.InsertAsync(
                    new EccpPropertyCompanyExtension
                    {
                        AptitudePhotoId = aptitudePhotoId,
                        BusinessLicenseId = businessLicenseId,
                        IsMember = isMember,
                        LegalPerson = legalPerson,
                        Mobile = mobile,
                        PropertyCompanyId = companyId
                    });
            }
            else if (dictEditionTypes[edition.ECCPEditionsTypeId.GetValueOrDefault(1)].Name == "维保公司")
            {
                var maintenanceCompany = await this._eccpMaintenanceCompanyRepository.FirstOrDefaultAsync(e => e.TenantId == tenantId);
                if (maintenanceCompany == null)
                {
                    companyId = await this._eccpMaintenanceCompanyRepository.InsertAndGetIdAsync(
                        new ECCPBaseMaintenanceCompany
                        {
                            Name = tenant.Name,
                            Addresse = "未填写",
                            Longitude = "未填写",
                            Latitude = "未填写",
                            Telephone = "未填写",
                            OrgOrganizationalCode = "未填写",
                            TenantId = tenantId
                        });
                }
                else
                {
                    companyId = maintenanceCompany.Id;
                }

                // Maintenance manage role
                await this.CreateCompanyRole(tenantId, StaticRoleNames.MaintenanceTenants.MainManage, StaticRoleNames.MaintenanceTenants.MainManageDisplayName);

                // Maintenance info manage role
                await this.CreateCompanyRole(tenantId, StaticRoleNames.MaintenanceTenants.MainInfoManage, StaticRoleNames.MaintenanceTenants.MainInfoManageDisplayName);

                // Maintenance principal role
                await this.CreateCompanyRole(tenantId, StaticRoleNames.MaintenanceTenants.MaintPrincipal, StaticRoleNames.MaintenanceTenants.MaintPrincipalDisplayName);

                // Maintenance user role
                await this.CreateCompanyRole(tenantId, StaticRoleNames.MaintenanceTenants.MainUser, StaticRoleNames.MaintenanceTenants.MainUserDisplayName);

                await this._eccpMaintenanceCompanyExtensionRepository.InsertAsync(
                    new EccpMaintenanceCompanyExtension
                    {
                        AptitudePhotoId = aptitudePhotoId,
                        BusinessLicenseId = businessLicenseId,
                        IsMember = isMember,
                        LegalPerson = legalPerson,
                        Mobile = mobile,
                        MaintenanceCompanyId = companyId
                    });
            }
            else if (dictEditionTypes[edition.ECCPEditionsTypeId.GetValueOrDefault(1)].Name == "政府")
            {
                var eccpBaseSaicUnit =
                    await this._eccpBaseSaicUnitRepository.FirstOrDefaultAsync(e => e.TenantId == tenantId);
                if (eccpBaseSaicUnit == null)
                {
                    await this._eccpBaseSaicUnitRepository.InsertAsync(
                       new EccpBaseSaicUnit
                       {
                           Name = tenant.Name,
                           Address = "未填写",
                           Telephone = "未填写",
                           TenantId = tenantId
                       });
                }
            }
        }

        private async Task CreateCompanyRole(int tenantId, string romeName, string roleDisplayName)
        {
            var roleEntity = await this._roleManager.Roles.SingleOrDefaultAsync(r => r.Name == romeName);
            if (roleEntity == null)
            {
                await this._roleManager.CreateAsync(new Role(tenantId, romeName, roleDisplayName));

                // role should be static
                roleEntity = this._roleManager.Roles.Single(r => r.Name == romeName);
                roleEntity.IsStatic = true;
                this.CheckErrors(await this._roleManager.UpdateAsync(roleEntity));

                await RolePermissionAssignment(roleEntity);
            }
        }

        private async Task RolePermissionAssignment(Role role)
        {
            List<string> grantedPermissionNames = new List<string>();

            switch (role.Name)
            {
                case StaticRoleNames.UseTenants.UseInfoManage:
                    //维保报告生成
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceReportGeneration);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Print);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Excel);
                    break;
                case StaticRoleNames.UseTenants.UseManage:
                    //维保工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Delete);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_CloseWorkOrder);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_ViewEvaluations);
                    //维保计划
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_Delete);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_ClosePlan);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders);
                    //临期工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePeriodWorkOrders);
                    //临时工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Delete);
                    //维保报告生成
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceReportGeneration);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Print);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Excel);
                    break;
                case StaticRoleNames.UseTenants.UseLeader:
                    //维保合同
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_Administration_EccpMaintenanceContracts);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_Administration_EccpMaintenanceContracts_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_Administration_EccpMaintenanceContracts_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_Administration_EccpMaintenanceContracts_Delete);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_Administration_EccpMaintenanceContracts_StopContract);
                    break;
                case StaticRoleNames.UseTenants.UseUser:
                    //维保工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Delete);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_CloseWorkOrder);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_ViewEvaluations);
                    //临期工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePeriodWorkOrders);
                    //临时工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Delete);
                    break;
                case StaticRoleNames.MaintenanceTenants.MainManage:
                    //维保合同
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_Administration_EccpMaintenanceContracts);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_Administration_EccpMaintenanceContracts_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_Administration_EccpMaintenanceContracts_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_Administration_EccpMaintenanceContracts_Delete);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_Administration_EccpMaintenanceContracts_StopContract);
                    //维保工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Delete);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_CloseWorkOrder);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_ViewEvaluations);
                    //维保计划
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_Delete);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_ClosePlan);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders);
                    //临期工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePeriodWorkOrders);
                    //临时工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Delete);
                    //维保报告生成
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceReportGeneration);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Print);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Excel);
                    //维保工单转接
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers_Audit);
                    //问题工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Delete);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Audit);
                    //维保工作
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorks);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorks_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorks_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorks_Delete);
                    //维保工作流程
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkFlows);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Delete);
                    break;
                case StaticRoleNames.MaintenanceTenants.MainInfoManage:
                    //维保报告生成
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceReportGeneration);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Print);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Excel);
                    //维保工单转接
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers_Audit);
                    //问题工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Delete);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Audit);
                    break;
                case StaticRoleNames.MaintenanceTenants.MaintPrincipal:
                    //维保工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Delete);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_CloseWorkOrder);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_ViewEvaluations);
                    //维保计划
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_Delete);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_ClosePlan);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders);
                    //临期工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePeriodWorkOrders);
                    //临时工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Delete);
                    //计划模板
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplates);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplates_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplates_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplates_Delete);
                    //模板节点
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Delete);
                    break;
                case StaticRoleNames.MaintenanceTenants.MainUser:
                    //维保工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Delete);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_CloseWorkOrder);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_ViewEvaluations);
                    //临期工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenancePeriodWorkOrders);
                    //临时工单
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Delete);
                    //计划模板
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplates);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplates_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplates_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplates_Delete);
                    //模板节点
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Create);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Edit);
                    grantedPermissionNames.Add(AppPermissions
                        .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Delete);
                    break;
            }

            var grantedPermissions = GetPermissionsFromNamesByValidating(grantedPermissionNames);
            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }

        private IEnumerable<Permission> GetPermissionsFromNamesByValidating(IEnumerable<string> permissionNames)
        {
            var permissions = new List<Permission>();
            var undefinedPermissionNames = new List<string>();

            foreach (var permissionName in permissionNames)
            {
                var permission = this._permissionManager.GetPermissionOrNull(permissionName);
                if (permission == null)
                {
                    undefinedPermissionNames.Add(permissionName);
                }

                permissions.Add(permission);
            }

            if (undefinedPermissionNames.Count > 0)
            {
                throw new AbpValidationException($"There are {undefinedPermissionNames.Count} undefined permission names.")
                {
                    ValidationErrors = undefinedPermissionNames.Select(permissionName => new ValidationResult("Undefined permission: " + permissionName)).ToList()
                };
            }

            return permissions;
        }
    }
}
