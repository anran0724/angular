// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonLookupAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.Authorization.Users;
    using Abp.Collections.Extensions;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.International.Converters.PinYinConverter;

    using Newtonsoft.Json;

    using NPinyin;

    using Sinodom.ElevatorCloud.Authorization.Permissions;
    using Sinodom.ElevatorCloud.Authorization.Roles;
    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.Common.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseAreas;
    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpElevatorQrCodes;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
    using Sinodom.ElevatorCloud.Editions;
    using Sinodom.ElevatorCloud.Editions.Dto;
    using Sinodom.ElevatorCloud.MultiTenancy;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions;
    using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    ///     The common lookup app service.
    /// </summary>
    [AbpAuthorize]
    public class CommonLookupAppService : ElevatorCloudAppServiceBase, ICommonLookupAppService
    {
        /// <summary>
        ///     The _binary object manager.
        /// </summary>
        private readonly IBinaryObjectManager _binaryObjectManager;

        /// <summary>
        ///     The _eccp base area repository.
        /// </summary>
        private readonly IRepository<ECCPBaseArea, int> _eccpBaseAreaRepository;

        /// <summary>
        ///     The _eccp base elevator repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;

        /// <summary>
        ///     The _eccp base elevator subsidiary info repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevatorSubsidiaryInfo, Guid> _eccpBaseElevatorSubsidiaryInfoRepository;

        /// <summary>
        ///     The _eccp base maintenance company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseMaintenanceCompany> _eccpBaseMaintenanceCompanyRepository;

        /// <summary>
        ///     The _eccp base property company repository.
        /// </summary>
        private readonly IRepository<ECCPBasePropertyCompany> _eccpBasePropertyCompanyRepository;

        /// <summary>
        ///     The _eccp company user extension repository.
        /// </summary>
        private readonly IRepository<EccpCompanyUserExtension, int> _eccpCompanyUserExtensionRepository;

        /// <summary>
        ///     The _eccp dict elevator status repository.
        /// </summary>
        private readonly IRepository<ECCPDictElevatorStatus> _eccpDictElevatorStatusRepository;

        /// <summary>
        ///     The _eccp dict elevator type repository.
        /// </summary>
        private readonly IRepository<EccpDictElevatorType> _eccpDictElevatorTypeRepository;

        /// <summary>
        ///     The _eccp dict place type repository.
        /// </summary>
        private readonly IRepository<EccpDictPlaceType> _eccpDictPlaceTypeRepository;

        /// <summary>
        ///     The _eccp edition repository.
        /// </summary>
        private readonly IRepository<ECCPEdition> _eccpEditionRepository;

        /// <summary>
        ///     The _eccp editions type repository.
        /// </summary>
        private readonly IRepository<ECCPEditionsType> _eccpEditionsTypeRepository;

        /// <summary>
        ///     The _eccp elevator qr code repository.
        /// </summary>
        private readonly IRepository<EccpElevatorQrCode, Guid> _eccpElevatorQrCodeRepository;

        /// <summary>
        ///     The _eccp maintenance company extension repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceCompanyExtension> _eccpMaintenanceCompanyExtensionRepository;

        /// <summary>
        ///     The _eccp maintenance contract manager.
        /// </summary>
        private readonly EccpMaintenanceContractManager _eccpMaintenanceContractManager;

        /// <summary>
        ///     The _eccp property company extension repository.
        /// </summary>
        private readonly IRepository<EccpPropertyCompanyExtension> _eccpPropertyCompanyExtensionRepository;

        /// <summary>
        ///     The _edition manager.
        /// </summary>
        private readonly EditionManager _editionManager;

        /// <summary>
        ///     The _password hasher.
        /// </summary>
        private readonly IPasswordHasher<User> _passwordHasher;

        /// <summary>
        ///     The _role manager.
        /// </summary>
        private readonly RoleManager _roleManager;

        /// <summary>
        ///     The _tenant repository.
        /// </summary>
        private readonly IRepository<Tenant> _tenantRepository;

        /// <summary>
        ///     The _user repository.
        /// </summary>
        private readonly IRepository<User, long> _userRepository;

        /// <summary>
        ///     The _user role repository.
        /// </summary>
        private readonly IRepository<UserRole, long> _userRoleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonLookupAppService"/> class.
        /// </summary>
        /// <param name="editionManager">
        /// The edition manager.
        /// </param>
        /// <param name="eccpBaseAreaRepository">
        /// The eccp base area repository.
        /// </param>
        /// <param name="eccpBaseMaintenanceCompanyRepository">
        /// The eccp base maintenance company repository.
        /// </param>
        /// <param name="eccpBasePropertyCompanyRepository">
        /// The eccp base property company repository.
        /// </param>
        /// <param name="eccpPropertyCompanyExtensionRepository">
        /// The eccp property company extension repository.
        /// </param>
        /// <param name="eccpMaintenanceCompanyExtensionRepository">
        /// The eccp maintenance company extension repository.
        /// </param>
        /// <param name="binaryObjectManager">
        /// The binary object manager.
        /// </param>
        /// <param name="eccpEditionsTypeRepository">
        /// The eccp editions type repository.
        /// </param>
        /// <param name="eccpEditionRepository">
        /// The eccp edition repository.
        /// </param>
        /// <param name="roleManager">
        /// The role manager.
        /// </param>
        /// <param name="passwordHasher">
        /// The password hasher.
        /// </param>
        /// <param name="eccpCompanyUserExtensionRepository">
        /// The eccp company user extension repository.
        /// </param>
        /// <param name="userRoleRepository">
        /// The user role repository.
        /// </param>
        /// <param name="eccpDictPlaceTypeRepository">
        /// The eccp dict place type repository.
        /// </param>
        /// <param name="eccpDictElevatorTypeRepository">
        /// The eccp dict elevator type repository.
        /// </param>
        /// <param name="eccpDictElevatorStatusRepository">
        /// The eccp dict elevator status repository.
        /// </param>
        /// <param name="eccpBaseElevatorRepository">
        /// The eccp base elevator repository.
        /// </param>
        /// <param name="eccpBaseElevatorSubsidiaryInfoRepository">
        /// The eccp base elevator subsidiary info repository.
        /// </param>
        /// <param name="eccpMaintenanceContractManager">
        /// The eccp maintenance contract manager.
        /// </param>
        /// <param name="userRepository">
        /// The user repository.
        /// </param>
        /// <param name="tenantRepository">
        /// The tenant repository.
        /// </param>
        /// <param name="eccpElevatorQrCodeRepository">
        /// The eccp elevator qr code repository.
        /// </param>
        public CommonLookupAppService(
            EditionManager editionManager,
            IRepository<ECCPBaseArea, int> eccpBaseAreaRepository,
            IRepository<ECCPBaseMaintenanceCompany> eccpBaseMaintenanceCompanyRepository,
            IRepository<ECCPBasePropertyCompany> eccpBasePropertyCompanyRepository,
            IRepository<EccpPropertyCompanyExtension> eccpPropertyCompanyExtensionRepository,
            IRepository<EccpMaintenanceCompanyExtension> eccpMaintenanceCompanyExtensionRepository,
            IBinaryObjectManager binaryObjectManager,
            IRepository<ECCPEditionsType> eccpEditionsTypeRepository,
            IRepository<ECCPEdition> eccpEditionRepository,
            RoleManager roleManager,
            IPasswordHasher<User> passwordHasher,
            IRepository<EccpCompanyUserExtension, int> eccpCompanyUserExtensionRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<EccpDictPlaceType> eccpDictPlaceTypeRepository,
            IRepository<EccpDictElevatorType> eccpDictElevatorTypeRepository,
            IRepository<ECCPDictElevatorStatus> eccpDictElevatorStatusRepository,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository,
            IRepository<EccpBaseElevatorSubsidiaryInfo, Guid> eccpBaseElevatorSubsidiaryInfoRepository,
            EccpMaintenanceContractManager eccpMaintenanceContractManager,
            IRepository<User, long> userRepository,
            IRepository<Tenant> tenantRepository,
            IRepository<EccpElevatorQrCode, Guid> eccpElevatorQrCodeRepository)
        {
            this._editionManager = editionManager;
            this._eccpBaseAreaRepository = eccpBaseAreaRepository;
            this._eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
            this._eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
            this._eccpPropertyCompanyExtensionRepository = eccpPropertyCompanyExtensionRepository;
            this._eccpMaintenanceCompanyExtensionRepository = eccpMaintenanceCompanyExtensionRepository;
            this._binaryObjectManager = binaryObjectManager;
            this._eccpEditionsTypeRepository = eccpEditionsTypeRepository;
            this._eccpEditionRepository = eccpEditionRepository;
            this._roleManager = roleManager;
            this._passwordHasher = passwordHasher;
            this._eccpCompanyUserExtensionRepository = eccpCompanyUserExtensionRepository;
            this._userRoleRepository = userRoleRepository;
            this._eccpDictPlaceTypeRepository = eccpDictPlaceTypeRepository;
            this._eccpDictElevatorTypeRepository = eccpDictElevatorTypeRepository;
            this._eccpDictElevatorStatusRepository = eccpDictElevatorStatusRepository;
            this._eccpBaseElevatorRepository = eccpBaseElevatorRepository;
            this._eccpBaseElevatorSubsidiaryInfoRepository = eccpBaseElevatorSubsidiaryInfoRepository;
            this._eccpMaintenanceContractManager = eccpMaintenanceContractManager;
            this._userRepository = userRepository;
            this._tenantRepository = tenantRepository;
            this._eccpElevatorQrCodeRepository = eccpElevatorQrCodeRepository;
        }

        /// <summary>
        /// The companies data synchronous.
        ///     公司数据同步
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAllowAnonymous]
        public async Task<int> CompaniesDataSynchronous(DeptDataSynchronousEntity entity)
        {
            if (entity != null)
            {
                string tenancyName;
                Tenant tenant;
                using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    tenancyName = this.GetFirstPinyin(entity.Name);
                    tenant = await this.TenantManager.Tenants.FirstOrDefaultAsync(e => e.TenancyName == tenancyName);
                }

                var rdm = new Random();
                if (entity.RoleGroupCode == "UseDeptRole")
                {
                    ECCPBasePropertyCompany propertyCompany;
                    EccpPropertyCompanyExtension propertyCompanyExtension = null;
                    using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                    {
                        if (entity.SyncCompanyId != null)
                        {
                            propertyCompanyExtension =
                                await this._eccpPropertyCompanyExtensionRepository.FirstOrDefaultAsync(
                                    e => e.SyncCompanyId == entity.SyncCompanyId);
                        }

                        if (propertyCompanyExtension != null)
                        {
                            propertyCompany = await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(
                                                  e => e.Id == propertyCompanyExtension.PropertyCompanyId);
                        }
                        else
                        {
                            propertyCompany =
                                await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(
                                    e => e.Name == entity.Name);
                        }
                    }

                    if (propertyCompany == null)
                    {
                        ECCPEdition eccpEdition;
                        using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                        {
                            var eccpEditionsType =
                                await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(e => e.Name.Contains("物业"));
                            eccpEdition =
                                await this._eccpEditionRepository.FirstOrDefaultAsync(
                                    e => e.ECCPEditionsTypeId == eccpEditionsType.Id);
                        }

                        int tenantId;
                        var tenantModel = await this._tenantRepository.FirstOrDefaultAsync(e => e.Name == entity.Name && e.EditionId == eccpEdition.Id);
                        if (tenantModel == null)
                        {
                            var rNum = rdm.Next(0, 100);
                            // 添加租户
                            tenantId = await this.TenantManager.CreateWithAdminUserAsync(
                                           tenant == null ? tenancyName : tenancyName + rNum,
                                           tenant == null ? entity.Name : entity.Name + rNum,
                                           "123qwe",
                                           entity.Telephone + "@dianti119.com",
                                           string.Empty,
                                           entity.IsActive,
                                           eccpEdition == null ? entity.EditionId : eccpEdition.Id,
                                           false,
                                           false,
                                           null,
                                           false,
                                           null,
                                           null,
                                           null,
                                           string.Empty,
                                           string.Empty,
                                           false);
                        }
                        else
                        {
                            tenantId = tenantModel.Id;
                        }

                        if (tenantId > 0)
                        {
                            int propertyCompanyId;
                            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                            {
                                var basePropertyCompany = await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(e =>
                                    e.Name == entity.Name);
                                if (basePropertyCompany == null)
                                {
                                    basePropertyCompany = this.ObjectMapper.Map<ECCPBasePropertyCompany>(entity);
                                    basePropertyCompany.TenantId = tenantId;

                                    propertyCompanyId =
                                        await this._eccpBasePropertyCompanyRepository.InsertAndGetIdAsync(
                                            basePropertyCompany);
                                    if (propertyCompanyId > 0)
                                    {
                                        var eccpPropertyCompanyExtension =
                                            this.ObjectMapper.Map<EccpPropertyCompanyExtension>(entity);

                                        if (entity.AptitudePhotoBytes.Length > 0)
                                        {
                                            var aptitudePhoto = new BinaryObject(tenantId, entity.AptitudePhotoBytes);
                                            await this._binaryObjectManager.SaveAsync(aptitudePhoto);

                                            eccpPropertyCompanyExtension.AptitudePhotoId = aptitudePhoto.Id;
                                        }

                                        if (entity.BusinessLicenseBytes.Length > 0)
                                        {
                                            var businessLicense = new BinaryObject(tenantId, entity.BusinessLicenseBytes);
                                            await this._binaryObjectManager.SaveAsync(businessLicense);

                                            eccpPropertyCompanyExtension.BusinessLicenseId = businessLicense.Id;
                                        }

                                        eccpPropertyCompanyExtension.PropertyCompanyId = propertyCompanyId;

                                        await this._eccpPropertyCompanyExtensionRepository.InsertAndGetIdAsync(
                                            eccpPropertyCompanyExtension);
                                    }
                                }
                                else
                                {
                                    this.ObjectMapper.Map(entity, basePropertyCompany);
                                    var companyExtension = await this._eccpPropertyCompanyExtensionRepository.FirstOrDefaultAsync(
                                    e => e.PropertyCompanyId == basePropertyCompany.Id);

                                    if (companyExtension != null)
                                    {
                                        if (entity.AptitudePhotoBytes.Length > 0)
                                        {
                                            if (companyExtension.AptitudePhotoId.HasValue)
                                            {
                                                await this._binaryObjectManager.DeleteAsync(companyExtension.AptitudePhotoId.Value);
                                            }

                                            var aptitudePhoto = new BinaryObject(
                                                basePropertyCompany.TenantId,
                                                entity.AptitudePhotoBytes);
                                            await this._binaryObjectManager.SaveAsync(aptitudePhoto);

                                            companyExtension.AptitudePhotoId = aptitudePhoto.Id;
                                        }

                                        if (entity.BusinessLicenseBytes.Length > 0)
                                        {
                                            if (companyExtension.BusinessLicenseId.HasValue)
                                            {
                                                await this._binaryObjectManager.DeleteAsync(
                                                    companyExtension.BusinessLicenseId.Value);
                                            }

                                            var businessLicense = new BinaryObject(
                                                basePropertyCompany.TenantId,
                                                entity.BusinessLicenseBytes);
                                            await this._binaryObjectManager.SaveAsync(businessLicense);

                                            companyExtension.BusinessLicenseId = businessLicense.Id;
                                        }

                                        companyExtension.LegalPerson = entity.LegalPerson;
                                        companyExtension.Mobile = entity.Mobile;
                                        companyExtension.IsMember = entity.IsMember;
                                        companyExtension.SyncCompanyId = entity.SyncCompanyId;

                                        await this._eccpPropertyCompanyExtensionRepository.UpdateAsync(companyExtension);

                                    }
                                    else
                                    {
                                        companyExtension =
                                           this.ObjectMapper.Map<EccpPropertyCompanyExtension>(entity);

                                        if (entity.AptitudePhotoBytes.Length > 0)
                                        {
                                            var aptitudePhoto = new BinaryObject(tenantId, entity.AptitudePhotoBytes);
                                            await this._binaryObjectManager.SaveAsync(aptitudePhoto);

                                            companyExtension.AptitudePhotoId = aptitudePhoto.Id;
                                        }

                                        if (entity.BusinessLicenseBytes.Length > 0)
                                        {
                                            var businessLicense = new BinaryObject(tenantId, entity.BusinessLicenseBytes);
                                            await this._binaryObjectManager.SaveAsync(businessLicense);

                                            companyExtension.BusinessLicenseId = businessLicense.Id;
                                        }
                                        companyExtension.PropertyCompanyId = basePropertyCompany.Id;

                                        await this._eccpPropertyCompanyExtensionRepository.InsertAndGetIdAsync(
                                            companyExtension);
                                    }

                                    propertyCompanyId = basePropertyCompany.Id;
                                }
                            }


                            // 创建角色
                            //var keyValues = new Dictionary<string, List<string>>
                            //                    {
                            //                        { "使用单位安全管理员", new List<string>() },
                            //                        { "使用单位信息管理员", new List<string>() },
                            //                        { "使用单位负责人", new List<string>() },
                            //                        { "使用单位人员", new List<string>() }
                            //                    };

                            //foreach (var s in keyValues)
                            //{
                            //    var role = new Role(tenantId, s.Key) { IsDefault = false };
                            //    await this._roleManager.CreateAsync(role);
                            //    if (s.Value.Count > 0)
                            //    {
                            //        await this.UpdateGrantedPermissionsAsync(role, s.Value);
                            //    }
                            //}

                            return propertyCompanyId;
                        }
                    }
                    else
                    {
                        using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                        {
                            this.ObjectMapper.Map(entity, propertyCompany);

                            var companyExtension =
                                await this._eccpPropertyCompanyExtensionRepository.FirstOrDefaultAsync(
                                    e => e.PropertyCompanyId == propertyCompany.Id);

                            if (entity.AptitudePhotoBytes.Length > 0)
                            {
                                if (companyExtension.AptitudePhotoId.HasValue)
                                {
                                    await this._binaryObjectManager.DeleteAsync(companyExtension.AptitudePhotoId.Value);
                                }

                                var aptitudePhoto = new BinaryObject(
                                    propertyCompany.TenantId,
                                    entity.AptitudePhotoBytes);
                                await this._binaryObjectManager.SaveAsync(aptitudePhoto);

                                companyExtension.AptitudePhotoId = aptitudePhoto.Id;
                            }

                            if (entity.BusinessLicenseBytes.Length > 0)
                            {
                                if (companyExtension.BusinessLicenseId.HasValue)
                                {
                                    await this._binaryObjectManager.DeleteAsync(
                                        companyExtension.BusinessLicenseId.Value);
                                }

                                var businessLicense = new BinaryObject(
                                    propertyCompany.TenantId,
                                    entity.BusinessLicenseBytes);
                                await this._binaryObjectManager.SaveAsync(businessLicense);

                                companyExtension.BusinessLicenseId = businessLicense.Id;
                            }

                            companyExtension.LegalPerson = entity.LegalPerson;
                            companyExtension.Mobile = entity.Mobile;
                            companyExtension.IsMember = entity.IsMember;
                            companyExtension.SyncCompanyId = companyExtension.SyncCompanyId ?? entity.SyncCompanyId;
                            await this._eccpPropertyCompanyExtensionRepository.UpdateAsync(companyExtension);

                            return propertyCompany.Id;
                        }
                    }
                }
                else if (entity.RoleGroupCode == "MaintDeptRole")
                {
                    ECCPBaseMaintenanceCompany maintenanceCompany;

                    EccpMaintenanceCompanyExtension maintenanceCompanyExtension = null;
                    using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                    {
                        if (entity.SyncCompanyId != null)
                        {
                            maintenanceCompanyExtension =
                                await this._eccpMaintenanceCompanyExtensionRepository.FirstOrDefaultAsync(
                                    e => e.SyncCompanyId == entity.SyncCompanyId);
                        }

                        if (maintenanceCompanyExtension != null)
                        {
                            maintenanceCompany = await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(
                                                     e => e.Id == maintenanceCompanyExtension.MaintenanceCompanyId);
                        }
                        else
                        {
                            maintenanceCompany =
                                await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(
                                    e => e.Name == entity.Name);
                        }
                    }

                    if (maintenanceCompany == null)
                    {
                        ECCPEdition eccpEdition;
                        using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                        {
                            var eccpEditionsType =
                                await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(e => e.Name.Contains("维保"));
                            eccpEdition =
                                await this._eccpEditionRepository.FirstOrDefaultAsync(
                                    e => e.ECCPEditionsTypeId == eccpEditionsType.Id);
                        }

                        int tenantId;
                        var tenantModel = await this._tenantRepository.FirstOrDefaultAsync(e => e.Name == entity.Name && e.EditionId == eccpEdition.Id);
                        if (tenantModel == null)
                        {
                            var rNum = rdm.Next(0, 100);
                            // 添加租户
                            tenantId = await this.TenantManager.CreateWithAdminUserAsync(
                                           tenant == null ? tenancyName : tenancyName + rNum,
                                           tenant == null ? entity.Name : entity.Name + rNum,
                                           "123qwe",
                                           entity.Telephone + "@dianti119.cn",
                                           string.Empty,
                                           entity.IsActive,
                                           eccpEdition == null ? entity.EditionId : eccpEdition.Id,
                                           false,
                                           false,
                                           null,
                                           false,
                                           null,
                                           null,
                                           null,
                                           string.Empty,
                                           string.Empty,
                                           false);
                        }
                        else
                        {
                            tenantId = tenantModel.Id;
                        }

                        if (tenantId > 0)
                        {
                            int maintenanceCompanyId;
                            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                            {
                                var baseMaintenanceCompany = await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(e =>
                                    e.Name == entity.Name);
                                if (baseMaintenanceCompany == null)
                                {
                                    baseMaintenanceCompany = this.ObjectMapper.Map<ECCPBaseMaintenanceCompany>(entity);
                                    baseMaintenanceCompany.TenantId = tenantId;

                                    maintenanceCompanyId =
                                        await this._eccpBaseMaintenanceCompanyRepository.InsertAndGetIdAsync(
                                            baseMaintenanceCompany);
                                    if (maintenanceCompanyId > 0)
                                    {
                                        var eccpMaintenanceCompanyExtension =
                                            this.ObjectMapper.Map<EccpMaintenanceCompanyExtension>(entity);

                                        if (entity.AptitudePhotoBytes.Length > 0)
                                        {
                                            var aptitudePhoto = new BinaryObject(tenantId, entity.AptitudePhotoBytes);
                                            await this._binaryObjectManager.SaveAsync(aptitudePhoto);

                                            eccpMaintenanceCompanyExtension.AptitudePhotoId = aptitudePhoto.Id;
                                        }

                                        if (entity.BusinessLicenseBytes.Length > 0)
                                        {
                                            var businessLicense = new BinaryObject(tenantId, entity.BusinessLicenseBytes);
                                            await this._binaryObjectManager.SaveAsync(businessLicense);

                                            eccpMaintenanceCompanyExtension.BusinessLicenseId = businessLicense.Id;
                                        }

                                        eccpMaintenanceCompanyExtension.MaintenanceCompanyId = maintenanceCompanyId;

                                        await this._eccpMaintenanceCompanyExtensionRepository.InsertAndGetIdAsync(
                                            eccpMaintenanceCompanyExtension);
                                    }
                                }
                                else
                                {
                                    this.ObjectMapper.Map(entity, baseMaintenanceCompany);

                                    var companyExtension =
                                        await this._eccpMaintenanceCompanyExtensionRepository.FirstOrDefaultAsync(
                                            e => e.MaintenanceCompanyId == baseMaintenanceCompany.Id);
                                    if (companyExtension != null)
                                    {
                                        if (entity.AptitudePhotoBytes.Length > 0)
                                        {
                                            if (companyExtension.AptitudePhotoId.HasValue)
                                            {
                                                await this._binaryObjectManager.DeleteAsync(companyExtension
                                                    .AptitudePhotoId.Value);
                                            }

                                            var aptitudePhoto = new BinaryObject(
                                                baseMaintenanceCompany.TenantId,
                                                entity.AptitudePhotoBytes);
                                            await this._binaryObjectManager.SaveAsync(aptitudePhoto);

                                            companyExtension.AptitudePhotoId = aptitudePhoto.Id;
                                        }

                                        if (entity.BusinessLicenseBytes.Length > 0)
                                        {
                                            if (companyExtension.BusinessLicenseId.HasValue)
                                            {
                                                await this._binaryObjectManager.DeleteAsync(
                                                    companyExtension.BusinessLicenseId.Value);
                                            }

                                            var businessLicense = new BinaryObject(
                                                baseMaintenanceCompany.TenantId,
                                                entity.BusinessLicenseBytes);
                                            await this._binaryObjectManager.SaveAsync(businessLicense);

                                            companyExtension.BusinessLicenseId = businessLicense.Id;
                                        }

                                        companyExtension.LegalPerson = entity.LegalPerson;
                                        companyExtension.Mobile = entity.Mobile;
                                        companyExtension.IsMember = entity.IsMember;
                                        companyExtension.SyncCompanyId = entity.SyncCompanyId;

                                        await this._eccpMaintenanceCompanyExtensionRepository.UpdateAsync(
                                            companyExtension);
                                    }
                                    else
                                    {
                                        companyExtension =
                                           this.ObjectMapper.Map<EccpMaintenanceCompanyExtension>(entity);

                                        if (entity.AptitudePhotoBytes.Length > 0)
                                        {
                                            var aptitudePhoto = new BinaryObject(tenantId, entity.AptitudePhotoBytes);
                                            await this._binaryObjectManager.SaveAsync(aptitudePhoto);

                                            companyExtension.AptitudePhotoId = aptitudePhoto.Id;
                                        }

                                        if (entity.BusinessLicenseBytes.Length > 0)
                                        {
                                            var businessLicense = new BinaryObject(tenantId, entity.BusinessLicenseBytes);
                                            await this._binaryObjectManager.SaveAsync(businessLicense);

                                            companyExtension.BusinessLicenseId = businessLicense.Id;
                                        }

                                        companyExtension.MaintenanceCompanyId = baseMaintenanceCompany.Id;

                                        await this._eccpMaintenanceCompanyExtensionRepository.InsertAndGetIdAsync(
                                            companyExtension);
                                    }

                                    maintenanceCompanyId = baseMaintenanceCompany.Id;
                                }
                            }


                            // 创建角色
                            //var keyValues = new Dictionary<string, List<string>>
                            //                    {
                            //                        { "维保单位信息管理员", new List<string>() },
                            //                        { "维保单位负责人", new List<string>() },
                            //                        { "维保负责人", new List<string>() },
                            //                        { "维保单位人员", new List<string>() }
                            //                    };

                            //foreach (var s in keyValues)
                            //{
                            //    var role = new Role(tenantId, s.Key) { IsDefault = false };
                            //    await this._roleManager.CreateAsync(role);
                            //    if (s.Value.Count > 0)
                            //    {
                            //        await this.UpdateGrantedPermissionsAsync(role, s.Value);
                            //    }
                            //}

                            return maintenanceCompanyId;
                        }
                    }
                    else
                    {
                        using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                        {
                            this.ObjectMapper.Map(entity, maintenanceCompany);

                            var companyExtension =
                                await this._eccpMaintenanceCompanyExtensionRepository.FirstOrDefaultAsync(
                                    e => e.MaintenanceCompanyId == maintenanceCompany.Id);

                            if (entity.AptitudePhotoBytes.Length > 0)
                            {
                                if (companyExtension.AptitudePhotoId.HasValue)
                                {
                                    await this._binaryObjectManager.DeleteAsync(companyExtension.AptitudePhotoId.Value);
                                }

                                var aptitudePhoto = new BinaryObject(
                                    maintenanceCompany.TenantId,
                                    entity.AptitudePhotoBytes);
                                await this._binaryObjectManager.SaveAsync(aptitudePhoto);

                                companyExtension.AptitudePhotoId = aptitudePhoto.Id;
                            }

                            if (entity.BusinessLicenseBytes.Length > 0)
                            {
                                if (companyExtension.BusinessLicenseId.HasValue)
                                {
                                    await this._binaryObjectManager.DeleteAsync(
                                        companyExtension.BusinessLicenseId.Value);
                                }

                                var businessLicense = new BinaryObject(
                                    maintenanceCompany.TenantId,
                                    entity.BusinessLicenseBytes);
                                await this._binaryObjectManager.SaveAsync(businessLicense);

                                companyExtension.BusinessLicenseId = businessLicense.Id;
                            }

                            companyExtension.LegalPerson = entity.LegalPerson;
                            companyExtension.Mobile = entity.Mobile;
                            companyExtension.IsMember = entity.IsMember;
                            companyExtension.SyncCompanyId = companyExtension.SyncCompanyId ?? entity.SyncCompanyId;
                            await this._eccpMaintenanceCompanyExtensionRepository.UpdateAsync(companyExtension);

                            return maintenanceCompany.Id;
                        }
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// The create or update area.
        ///     创建或编辑区域
        /// </summary>
        /// <param name="area">
        /// The area.
        /// </param>
        public void CreateOrUpdateArea(SyncAreaInput area)
        {
            if (area.Name.Length > 0)
            {
                var areaModel = this._eccpBaseAreaRepository.FirstOrDefault(m => m.Name == area.Name);
                if (areaModel != null)
                {
                    this.ObjectMapper.Map(area, areaModel);
                }
                else
                {
                    var eccpBaseArea = this.ObjectMapper.Map<ECCPBaseArea>(area);
                    this._eccpBaseAreaRepository.Insert(eccpBaseArea);
                }
            }
        }

        /// <summary>
        /// The elevators data synchronous.
        ///     电梯数据同步
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAllowAnonymous]
        public async Task<int> ElevatorsDataSynchronous(ElevatorsDataSynchronousEntity entity)
        {
            if (entity != null)
            {
                using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    // 查询维保公司
                    var eccpMaintenanceCompanyExtension =
                        await this._eccpMaintenanceCompanyExtensionRepository.FirstOrDefaultAsync(
                            e => e.SyncCompanyId == entity.MaintenanceCompanyId);

                    var eccpBaseMaintenanceCompany =
                        await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(
                            e => e.Id == eccpMaintenanceCompanyExtension.MaintenanceCompanyId);
                    entity.ECCPBaseMaintenanceCompanyId = eccpBaseMaintenanceCompany.Id;

                    // 查询使用公司
                    var eccpPropertyCompanyExtension =
                        await this._eccpPropertyCompanyExtensionRepository.FirstOrDefaultAsync(
                            e => e.SyncCompanyId == entity.PropertyCompanyId);
                    var eccpBasePropertyCompany =
                        await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(
                            e => e.Id == eccpPropertyCompanyExtension.PropertyCompanyId);
                    entity.ECCPBasePropertyCompanyId = eccpBasePropertyCompany.Id;
                    entity.TenantId = eccpBasePropertyCompany.TenantId;

                    // 查询使用场所
                    var eccpDictPlaceType =
                        await this._eccpDictPlaceTypeRepository.FirstOrDefaultAsync(
                            e => e.Name == entity.EccpDictPlaceTypeName);
                    entity.EccpDictPlaceTypeId = eccpDictPlaceType.Id;

                    // 查询电梯类型
                    var eccpDictElevatorType =
                        await this._eccpDictElevatorTypeRepository.FirstOrDefaultAsync(
                            e => e.Name == entity.EccpDictElevatorTypeName);
                    entity.EccpDictElevatorTypeId = eccpDictElevatorType.Id;

                    // 查询电梯状态
                    var eccpDictElevatorStatus =
                        await this._eccpDictElevatorStatusRepository.FirstOrDefaultAsync(
                            e => e.Name == entity.ECCPDictElevatorStatusName);
                    entity.ECCPDictElevatorStatusId = eccpDictElevatorStatus.Id;

                    // 省
                    var baseAreaProvince = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                                               e => e.Name.Contains(entity.ProvinceName));
                    entity.ProvinceId = baseAreaProvince.Id;

                    // 市
                    var baseAreaCity = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                                           e => e.ParentId == entity.ProvinceId);
                    entity.CityId = baseAreaCity.Id;

                    // 区
                    var baseAreaDistrict = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                                               e => e.ParentId == entity.CityId);
                    entity.DistrictId = baseAreaDistrict.Id;

                    // 根据电梯同步ID查询电梯，如果不存在根据注册证号查询电梯
                    EccpBaseElevator baseElevator = null;

                    if (entity.SyncElevatorId != null)
                    {
                        baseElevator =
                            await this._eccpBaseElevatorRepository.FirstOrDefaultAsync(
                                e => e.SyncElevatorId == entity.SyncElevatorId);
                    }

                    if (baseElevator == null)
                    {
                        baseElevator =
                            await this._eccpBaseElevatorRepository.FirstOrDefaultAsync(
                                e => e.CertificateNum == entity.CertificateNum);
                    }

                    // 如果电梯等于null，添加电梯信息，否则修改
                    if (baseElevator == null)
                    {
                        var elevator = this.ObjectMapper.Map<EccpBaseElevator>(entity);
                        var subsidiaryInfo = this.ObjectMapper.Map<EccpBaseElevatorSubsidiaryInfo>(entity);

                        // 添加电梯
                        var elevatorId = await this._eccpBaseElevatorRepository.InsertAndGetIdAsync(elevator);

                        // 添加电梯附属信息
                        subsidiaryInfo.ElevatorId = elevatorId;
                        await this._eccpBaseElevatorSubsidiaryInfoRepository.InsertAsync(subsidiaryInfo);

                        // 添加电梯编码信息
                        if (!string.IsNullOrWhiteSpace(entity.ElevatorNum))
                        {
                            if (int.TryParse(entity.ElevatorNum, out var elevatorNum))
                            {
                                entity.ElevatorNum = elevatorNum.ToString();
                                entity.AreaName = this.GetProvinceAbbreviationByProvince(baseAreaProvince.Name);
                            }
                            else
                            {
                                var str = entity.ElevatorNum.Substring(0, 1);
                                if (this.CheckElevatorNum(str))
                                {
                                    entity.AreaName = str;
                                    entity.ElevatorNum = entity.ElevatorNum.Substring(1);
                                }
                                else
                                {
                                    entity.AreaName = this.GetProvinceAbbreviationByProvince(baseAreaProvince.Name);
                                }
                            }

                            var elevatorQrCode = this.ObjectMapper.Map<EccpElevatorQrCode>(entity);
                            elevatorQrCode.ElevatorId = elevatorId;
                            await this._eccpElevatorQrCodeRepository.InsertAsync(elevatorQrCode);
                        }

                        // 添加合同
                        var result = this._eccpMaintenanceContractManager.CreateMaintenanceContract(
                            new List<Guid> { elevatorId },
                            eccpBaseMaintenanceCompany.Id,
                            eccpBasePropertyCompany.Id);
                        if (result > 0)
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        var eccpBaseElevatorSubsidiaryInfo =
                            await this._eccpBaseElevatorSubsidiaryInfoRepository.FirstOrDefaultAsync(
                                m => m.ElevatorId == baseElevator.Id);
                        var elevatorQrCode =
                            await this._eccpElevatorQrCodeRepository.FirstOrDefaultAsync(
                                m => m.ElevatorId == baseElevator.Id);
                        if (elevatorQrCode != null)
                        {
                            if (!string.IsNullOrWhiteSpace(entity.ElevatorNum))
                            {
                                if (int.TryParse(entity.ElevatorNum, out var elevatorNum))
                                {
                                    entity.ElevatorNum = elevatorNum.ToString();
                                    entity.AreaName = this.GetProvinceAbbreviationByProvince(baseAreaProvince.Name);
                                }
                                else
                                {
                                    var str = entity.ElevatorNum.Substring(0, 1);
                                    if (this.CheckElevatorNum(str))
                                    {
                                        entity.AreaName = str;
                                        entity.ElevatorNum = entity.ElevatorNum.Substring(1);
                                    }
                                    else
                                    {
                                        entity.AreaName = this.GetProvinceAbbreviationByProvince(baseAreaProvince.Name);
                                    }
                                }

                                this.ObjectMapper.Map(entity, elevatorQrCode);
                            }
                        }
                        else
                        {
                            // 添加电梯编码信息
                            if (!string.IsNullOrWhiteSpace(entity.ElevatorNum))
                            {
                                if (int.TryParse(entity.ElevatorNum, out var elevatorNum))
                                {
                                    entity.ElevatorNum = elevatorNum.ToString();
                                    entity.AreaName = this.GetProvinceAbbreviationByProvince(baseAreaProvince.Name);
                                }
                                else
                                {
                                    var str = entity.ElevatorNum.Substring(0, 1);
                                    if (this.CheckElevatorNum(str))
                                    {
                                        entity.AreaName = str;
                                        entity.ElevatorNum = entity.ElevatorNum.Substring(1);
                                    }
                                    else
                                    {
                                        entity.AreaName = this.GetProvinceAbbreviationByProvince(baseAreaProvince.Name);
                                    }
                                }

                                elevatorQrCode = this.ObjectMapper.Map<EccpElevatorQrCode>(entity);
                                elevatorQrCode.ElevatorId = baseElevator.Id;
                                await this._eccpElevatorQrCodeRepository.InsertAsync(elevatorQrCode);
                            }
                        }

                        this.ObjectMapper.Map(entity, baseElevator);
                        this.ObjectMapper.Map(entity, eccpBaseElevatorSubsidiaryInfo);

                        // 添加合同
                        var result = this._eccpMaintenanceContractManager.CreateMaintenanceContract(
                            new List<Guid> { baseElevator.Id },
                            eccpBaseMaintenanceCompany.Id,
                            eccpBasePropertyCompany.Id);

                        if (result > 0)
                        {
                            return 1;
                        }
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// The find users.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input)
        {
            if (this.AbpSession.TenantId != null)
            {
                // Prevent tenants to get other tenant's users.
                input.TenantId = this.AbpSession.TenantId;
            }

            using (this.CurrentUnitOfWork.SetTenantId(input.TenantId))
            {
                var query = this.UserManager.Users.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u => u.Name.Contains(input.Filter) || u.Surname.Contains(input.Filter)
                                                       || u.UserName.Contains(input.Filter)
                                                       || u.EmailAddress.Contains(input.Filter));

                var userCount = await query.CountAsync();
                var users = await query.OrderBy(u => u.Name).ThenBy(u => u.Surname).PageBy(input).ToListAsync();

                return new PagedResultDto<NameValueDto>(
                    userCount,
                    users.Select(u => new NameValueDto(u.FullName + " (" + u.EmailAddress + ")", u.Id.ToString())).ToList());
            }
        }

        /// <summary>
        ///     The get default edition name.
        /// </summary>
        /// <returns>
        ///     The <see cref="GetDefaultEditionNameOutput" />.
        /// </returns>
        public GetDefaultEditionNameOutput GetDefaultEditionName()
        {
            return new GetDefaultEditionNameOutput { Name = EditionManager.DefaultEditionName };
        }

        /// <summary>
        /// The get editions for combobox.
        /// </summary>
        /// <param name="onlyFreeItems">
        /// The only free items.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(
            bool onlyFreeItems = false)
        {
            var subscribableEditions = (await this._editionManager.Editions.Cast<SubscribableEdition>().ToListAsync())
                .WhereIf(onlyFreeItems, e => e.IsFree).OrderBy(e => e.MonthlyPrice);

            return new ListResultDto<SubscribableEditionComboboxItemDto>(
                subscribableEditions.Select(
                    e => new SubscribableEditionComboboxItemDto(e.Id.ToString(), e.DisplayName, e.IsFree)).ToList());
        }

        /// <summary>
        /// The synchronize.
        ///     同步维保单位，使用单位，行政区
        /// </summary>
        /// <param name="jsonData">
        /// The json data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [AbpAllowAnonymous]
        public string Synchronize(string jsonData)
        {
            string result;
            try
            {
                var getSyncInput = JsonConvert.DeserializeObject<GetSyncInput>(jsonData);

                // 同步公司
                foreach (var dept in getSyncInput.SyncDeptInputs)
                {
                    switch (dept.RoleGroupId)
                    {
                        // 维保公司
                        case 7:
                            this.CreateOrUpdateMaintenanceCompany(dept);
                            break;

                        // 物业公司
                        case 6:
                            this.CreateOrUpdatePropertyCompany(dept);
                            break;
                    }
                }

                result = "同步成功!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return result;
        }

        /// <summary>
        /// The user data synchronous.
        ///     用户数据同步
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAllowAnonymous]
        public async Task<long> UserDataSynchronous(UserDataSynchronousEntity entity)
        {
            if (entity != null)
            {
                using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    switch (entity.RoleGroupCode)
                    {
                        case "UseDeptRole":
                            ECCPBasePropertyCompany propertyCompany = null;
                            if (entity.CompaniesId != null)
                            {
                                var propertyCompanyExtension =
                                    await this._eccpPropertyCompanyExtensionRepository.FirstOrDefaultAsync(
                                        e => e.SyncCompanyId == entity.CompaniesId);
                                propertyCompany =
                                    await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(
                                        e => e.Id == propertyCompanyExtension.PropertyCompanyId);
                            }
                            else
                            {
                                propertyCompany =
                               await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(
                                   e => e.Name == entity.Companies);
                            }

                            entity.TenantId = propertyCompany?.TenantId;
                            break;
                        case "MaintDeptRole":
                            ECCPBaseMaintenanceCompany maintenanceCompany = null;
                            if (entity.CompaniesId != null)
                            {
                                var maintenanceCompanyExtension = await this._eccpMaintenanceCompanyExtensionRepository.FirstOrDefaultAsync(e =>
                                    e.SyncCompanyId == entity.CompaniesId);
                                maintenanceCompany = await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(
                                    e => e.Id == maintenanceCompanyExtension.MaintenanceCompanyId);
                            }
                            else
                            {
                                maintenanceCompany =
                                await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(
                                    e => e.Name == entity.Companies);
                            }

                            entity.TenantId = maintenanceCompany?.TenantId;
                            break;
                    }

                    User userModel;
                    EccpCompanyUserExtension userExtension = null;
                    if (entity.SyncUserId != null)
                    {
                        userExtension =
                            await this._eccpCompanyUserExtensionRepository.FirstOrDefaultAsync(
                                e => e.SyncUserId == entity.SyncUserId);
                    }

                    if (userExtension != null)
                    {
                        userModel = await this.UserManager.Users.FirstOrDefaultAsync(
                                        e => e.Id == userExtension.UserId && e.TenantId == entity.TenantId);
                    }
                    else
                    {
                        userModel = await this.UserManager.Users.FirstOrDefaultAsync(
                                        e => e.UserName == entity.UserName && e.TenantId == entity.TenantId);
                    }

                    if (userModel == null)
                    {
                        var user = this.ObjectMapper.Map<User>(entity);
                        user.Password = this._passwordHasher.HashPassword(user, "123qwe");
                        user.NormalizedEmailAddress = !string.IsNullOrWhiteSpace(entity.EmailAddress)
                                                          ? entity.EmailAddress.ToUpper()
                                                          : string.Empty;
                        user.NormalizedUserName = entity.UserName.ToUpper();

                        var role = await this._roleManager.Roles.FirstOrDefaultAsync(e => e.Name == entity.RoleCode && e.TenantId == entity.TenantId);

                        // await UserManager.CreateAsync(user);
                        var userId = await this._userRepository.InsertAndGetIdAsync(user);
                        var userRole = new UserRole(user.TenantId, userId, role.Id);
                        await this._userRoleRepository.InsertAsync(userRole);

                        var companyUse = this.ObjectMapper.Map<EccpCompanyUserExtension>(entity);

                        // 签名
                        if (entity.SignPictureBytes.Length > 0)
                        {
                            var signPicture = new BinaryObject(user.TenantId, entity.SignPictureBytes);
                            await this._binaryObjectManager.SaveAsync(signPicture);

                            companyUse.SignPictureId = signPicture.Id;
                        }

                        // 特种设备操作证首页照片
                        if (entity.CertificateBackPictureBytes.Length > 0)
                        {
                            var certificateBackPicture = new BinaryObject(
                                user.TenantId,
                                entity.CertificateBackPictureBytes);
                            await this._binaryObjectManager.SaveAsync(certificateBackPicture);

                            companyUse.CertificateBackPictureId = certificateBackPicture.Id;
                        }

                        // 特种设备操作证有效期照片
                        if (entity.CertificateFrontPictureBytes.Length > 0)
                        {
                            var certificateFrontPicture = new BinaryObject(
                                user.TenantId,
                                entity.CertificateFrontPictureBytes);
                            await this._binaryObjectManager.SaveAsync(certificateFrontPicture);

                            companyUse.CertificateFrontPictureId = certificateFrontPicture.Id;
                        }

                        companyUse.UserId = user.Id;

                        await this._eccpCompanyUserExtensionRepository.InsertAsync(companyUse);
                        return user.Id;
                    }
                    else
                    {
                        this.ObjectMapper.Map(entity, userModel);

                        var role = await this._roleManager.Roles.FirstOrDefaultAsync(e => e.Name == entity.RoleCode && e.TenantId == entity.TenantId);

                        var userRole =
                            await this._userRoleRepository.FirstOrDefaultAsync(e => e.UserId == userModel.Id);
                        if (userRole != null)
                        {
                            userRole.RoleId = role.Id;
                            await this._userRoleRepository.UpdateAsync(userRole);
                        }
                        else
                        {
                            userRole = new UserRole(userModel.TenantId, userModel.Id, role.Id);
                            await this._userRoleRepository.InsertAsync(userRole);
                        }

                        var eccpCompanyUserExtension =
                            await this._eccpCompanyUserExtensionRepository.FirstOrDefaultAsync(
                                e => e.UserId == userModel.Id);

                        // 签名
                        if (entity.SignPictureBytes.Length > 0)
                        {
                            if (eccpCompanyUserExtension.SignPictureId.HasValue)
                            {
                                await this._binaryObjectManager.DeleteAsync(
                                    eccpCompanyUserExtension.SignPictureId.Value);
                            }

                            var signPicture = new BinaryObject(userModel.TenantId, entity.SignPictureBytes);
                            await this._binaryObjectManager.SaveAsync(signPicture);

                            eccpCompanyUserExtension.SignPictureId = signPicture.Id;
                        }

                        // 特种设备操作证首页照片
                        if (entity.CertificateBackPictureBytes.Length > 0)
                        {
                            if (eccpCompanyUserExtension.CertificateBackPictureId.HasValue)
                            {
                                await this._binaryObjectManager.DeleteAsync(
                                    eccpCompanyUserExtension.CertificateBackPictureId.Value);
                            }

                            var certificateBackPicture = new BinaryObject(
                                userModel.TenantId,
                                entity.CertificateBackPictureBytes);
                            await this._binaryObjectManager.SaveAsync(certificateBackPicture);

                            eccpCompanyUserExtension.CertificateBackPictureId = certificateBackPicture.Id;
                        }

                        // 特种设备操作证有效期照片
                        if (entity.CertificateFrontPictureBytes.Length > 0)
                        {
                            if (eccpCompanyUserExtension.CertificateFrontPictureId.HasValue)
                            {
                                await this._binaryObjectManager.DeleteAsync(
                                    eccpCompanyUserExtension.CertificateFrontPictureId.Value);
                            }

                            var certificateFrontPicture = new BinaryObject(
                                userModel.TenantId,
                                entity.CertificateFrontPictureBytes);
                            await this._binaryObjectManager.SaveAsync(certificateFrontPicture);

                            eccpCompanyUserExtension.CertificateFrontPictureId = certificateFrontPicture.Id;
                        }

                        eccpCompanyUserExtension.IdCard = entity.IdCard;
                        eccpCompanyUserExtension.Mobile = entity.Mobile;
                        eccpCompanyUserExtension.ExpirationDate = entity.ExpirationDate;
                        eccpCompanyUserExtension.CheckState = entity.CheckState;
                        eccpCompanyUserExtension.SyncUserId = eccpCompanyUserExtension.SyncUserId ?? entity.SyncUserId;
                        await this._eccpCompanyUserExtensionRepository.UpdateAsync(eccpCompanyUserExtension);

                        return userModel.Id;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// The get spell.
        /// </summary>
        /// <param name="chr">
        /// The chr.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetSpell(char chr)
        {
            var coverchr = Pinyin.GetPinyin(chr);
            var isChineses = ChineseChar.IsValidChar(coverchr[0]);
            if (isChineses)
            {
                var chineseChar = new ChineseChar(coverchr[0]);
                foreach (var value in chineseChar.Pinyins)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value.Remove(value.Length - 1, 1);
                    }
                }
            }

            return coverchr;
        }

        /// <summary>
        /// The check elevator num.
        ///     验证电梯编号
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool CheckElevatorNum(string str)
        {
            var result = false;
            switch (str)
            {
                case "京":
                    result = true;
                    break;
                case "津":
                    result = true;
                    break;
                case "冀":
                    result = true;
                    break;
                case "晋":
                    result = true;
                    break;
                case "蒙":
                    result = true;
                    break;
                case "辽":
                    result = true;
                    break;
                case "吉":
                    result = true;
                    break;
                case "黑":
                    result = true;
                    break;
                case "沪":
                    result = true;
                    break;
                case "苏":
                    result = true;
                    break;
                case "浙":
                    result = true;
                    break;
                case "皖":
                    result = true;
                    break;
                case "闽":
                    result = true;
                    break;
                case "赣":
                    result = true;
                    break;
                case "鲁":
                    result = true;
                    break;
                case "豫":
                    result = true;
                    break;
                case "鄂":
                    result = true;
                    break;
                case "湘":
                    result = true;
                    break;
                case "粤":
                    result = true;
                    break;
                case "桂":
                    result = true;
                    break;
                case "琼":
                    result = true;
                    break;
                case "蜀":
                    result = true;
                    break;
                case "川":
                    result = true;
                    break;
                case "贵":
                    result = true;
                    break;
                case "黔":
                    result = true;
                    break;
                case "云":
                    result = true;
                    break;
                case "滇":
                    result = true;
                    break;
                case "渝":
                    result = true;
                    break;
                case "藏":
                    result = true;
                    break;
                case "陕":
                    result = true;
                    break;
                case "秦":
                    result = true;
                    break;
                case "甘":
                    result = true;
                    break;
                case "陇":
                    result = true;
                    break;
                case "青":
                    result = true;
                    break;
                case "宁":
                    result = true;
                    break;
                case "新":
                    result = true;
                    break;
                case "港":
                    result = true;
                    break;
                case "澳":
                    result = true;
                    break;
                case "台":
                    result = true;
                    break;
            }

            return result;
        }

        /// <summary>
        /// The create or update maintenance company.
        ///     创建或更新维保公司
        /// </summary>
        /// <param name="dept">
        /// The dept.
        /// </param>
        private void CreateOrUpdateMaintenanceCompany(SyncDeptInput dept)
        {
            if (dept.Name.Length > 0)
            {
                // TODO: 接口暂未提供省市区街道，暂时取本系统第一条街道作为默认值
                var path = this._eccpBaseAreaRepository.GetAllList().Where(s => s.Level == 3 && s.IsDeleted == false)
                    .OrderBy(s => s.CreationTime).FirstOrDefault()?.Path;
                var pathList = path.Split(",");

                dept.Addresse = dept.Addresse ?? "暂无";
                dept.Longitude = dept.Longitude ?? "暂无";
                dept.Latitude = dept.Latitude ?? "暂无";
                dept.Telephone = dept.Telephone ?? "暂无";
                dept.OrgOrganizationalCode = dept.OrgOrganizationalCode ?? "暂无";
                dept.ProvinceId = dept.ProvinceId ?? int.Parse(pathList[0]);
                dept.CityId = dept.CityId ?? int.Parse(pathList[1]);
                dept.DistrictId = dept.DistrictId ?? int.Parse(pathList[2]);
                dept.StreetId = dept.StreetId ?? int.Parse(pathList[3]);

                var maintenanceCompany =
                    this._eccpBaseMaintenanceCompanyRepository.FirstOrDefault(m => m.Name == dept.Name);
                if (maintenanceCompany != null)
                {
                    this.ObjectMapper.Map(dept, maintenanceCompany);
                }
                else
                {
                    var eccpBaseMaintenanceCompany = this.ObjectMapper.Map<ECCPBaseMaintenanceCompany>(dept);
                    this._eccpBaseMaintenanceCompanyRepository.Insert(eccpBaseMaintenanceCompany);
                }
            }
        }

        /// <summary>
        /// The create or update property company.
        ///     创建或更新物业公司
        /// </summary>
        /// <param name="dept">
        /// The dept.
        /// </param>
        private void CreateOrUpdatePropertyCompany(SyncDeptInput dept)
        {
            if (dept.Name.Length > 0)
            {
                // TODO: 接口暂未提供省市区街道，暂时取本系统第一条街道作为默认值
                var path = this._eccpBaseAreaRepository.GetAllList().Where(s => s.Level == 3 && s.IsDeleted == false)
                    .OrderBy(s => s.CreationTime).FirstOrDefault()?.Path;
                var pathList = path.Split(",");

                dept.Addresse = dept.Addresse ?? "暂无";
                dept.Longitude = dept.Longitude ?? "暂无";
                dept.Latitude = dept.Latitude ?? "暂无";
                dept.Telephone = dept.Telephone ?? "暂无";
                dept.OrgOrganizationalCode = dept.OrgOrganizationalCode ?? "暂无";
                dept.ProvinceId = dept.ProvinceId ?? int.Parse(pathList[0]);
                dept.CityId = dept.CityId ?? int.Parse(pathList[1]);
                dept.DistrictId = dept.DistrictId ?? int.Parse(pathList[2]);
                dept.StreetId = dept.StreetId ?? int.Parse(pathList[3]);

                var propertyCompany = this._eccpBasePropertyCompanyRepository.FirstOrDefault(m => m.Name == dept.Name);
                if (propertyCompany != null)
                {
                    this.ObjectMapper.Map(dept, propertyCompany);
                }
                else
                {
                    var eccpBasePropertyCompany = this.ObjectMapper.Map<ECCPBasePropertyCompany>(dept);
                    this._eccpBasePropertyCompanyRepository.Insert(eccpBasePropertyCompany);
                }
            }
        }

        /// <summary>
        /// The get first pinyin.
        ///     汉字转化为拼音首字母
        /// </summary>
        /// <param name="strChinese">
        /// The str Chinese.
        /// </param>
        /// <returns>
        /// 首字母
        ///     The <see cref="string"/>.
        /// </returns>
        private string GetFirstPinyin(string strChinese)
        {
            try
            {
                strChinese = Regex.Replace(strChinese, "[ \\[ \\] \\^ \\-_*×――(^)（^）$%~!@#$…&%￥—+=<>《》!！??？:：•`·、。，；,.;\"‘’“”-]", "");
                if (strChinese.Length != 0)
                {
                    var fullSpell = new StringBuilder();
                    for (var i = 0; i < strChinese.Length; i++)
                    {
                        var chr = strChinese[i];
                        fullSpell.Append(GetSpell(chr)[0]);
                    }

                    return fullSpell.ToString().ToUpper();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("首字母转化出错！" + e.Message);
            }

            return string.Empty;
        }

        /// <summary>
        /// The get province abbreviation by province.
        ///     根据省份获取省份简称
        /// </summary>
        /// <param name="province">
        /// The province.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetProvinceAbbreviationByProvince(string province)
        {
            var result = string.Empty;
            switch (province)
            {
                case "北京":
                    result = "京";
                    break;
                case "天津":
                    result = "津";
                    break;
                case "河北省":
                    result = "冀";
                    break;
                case "山西省":
                    result = "晋";
                    break;
                case "内蒙古自治区":
                    result = "蒙";
                    break;
                case "辽宁省":
                    result = "辽";
                    break;
                case "吉林省":
                    result = "吉";
                    break;
                case "黑龙江省":
                    result = "黑";
                    break;
                case "上海":
                    result = "沪";
                    break;
                case "江苏省":
                    result = "苏";
                    break;
                case "浙江省":
                    result = "浙";
                    break;
                case "安徽省":
                    result = "皖";
                    break;
                case "福建省":
                    result = "闽";
                    break;
                case "江西省":
                    result = "赣";
                    break;
                case "山东省":
                    result = "鲁";
                    break;
                case "河南省":
                    result = "豫";
                    break;
                case "湖北省":
                    result = "鄂";
                    break;
                case "湖南省":
                    result = "湘";
                    break;
                case "广东省":
                    result = "粤";
                    break;
                case "广西壮族自治区":
                    result = "桂";
                    break;
                case "海南省":
                    result = "琼";
                    break;
                case "四川省":
                    result = "川";
                    break;
                case "贵州省":
                    result = "黔";
                    break;
                case "云南省":
                    result = "云";
                    break;
                case "重庆":
                    result = "渝";
                    break;
                case "西藏自治区":
                    result = "藏";
                    break;
                case "陕西省":
                    result = "陕";
                    break;
                case "甘肃省":
                    result = "甘";
                    break;
                case "青海省":
                    result = "青";
                    break;
                case "宁夏回族自治区":
                    result = "宁";
                    break;
                case "新疆维吾尔自治区":
                    result = "新";
                    break;
                case "香港特别行政区":
                    result = "港";
                    break;
                case "澳门特别行政区":
                    result = "澳";
                    break;
                case "台湾省":
                    result = "台";
                    break;
            }

            return result;
        }

        /// <summary>
        /// The update granted permissions async.
        ///     创建角色权限
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <param name="grantedPermissionNames">
        /// The granted permission names.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task UpdateGrantedPermissionsAsync(Role role, List<string> grantedPermissionNames)
        {
            var grantedPermissions = this.PermissionManager.GetPermissionsFromNamesByValidating(grantedPermissionNames);
            await this._roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }
    }
}