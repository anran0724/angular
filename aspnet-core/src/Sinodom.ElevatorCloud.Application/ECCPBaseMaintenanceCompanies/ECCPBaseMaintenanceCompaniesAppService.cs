// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseMaintenanceCompaniesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.AutoMapper;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Linq.Extensions;
    using Abp.UI;
    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseAreas;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Exporting;
    using Sinodom.ElevatorCloud.MultiTenancy;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions;
    using Sinodom.ElevatorCloud.MultiTenancy.Dto;
    using Sinodom.ElevatorCloud.MultiTenancy.Payments.Dto;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    ///     The eccp base maintenance companies app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies)]
    public class ECCPBaseMaintenanceCompaniesAppService : ElevatorCloudAppServiceBase,
                                                          IECCPBaseMaintenanceCompaniesAppService
    {
        /// <summary>
        ///     The _eccp base area repository.
        /// </summary>
        private readonly IRepository<ECCPBaseArea, int> _eccpBaseAreaRepository;

        /// <summary>
        ///     The _eccp base maintenance companies excel exporter.
        /// </summary>
        private readonly IECCPBaseMaintenanceCompaniesExcelExporter _eccpBaseMaintenanceCompaniesExcelExporter;

        /// <summary>
        ///     The _eccp base maintenance company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseMaintenanceCompany> _eccpBaseMaintenanceCompanyRepository;

        /// <summary>
        ///     The _eccp maintenance company change log repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceCompanyChangeLog, int> _eccpMaintenanceCompanyChangeLogRepository;

        private readonly ITenantRegistrationAppService _tenantRegistrationAppService;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private const int MaxProfilPictureBytes = 5048576; //5MB
        private readonly IRepository<EccpMaintenanceCompanyExtension> _eccpMaintenanceCompanyExtensionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseMaintenanceCompaniesAppService"/> class.
        /// </summary>
        /// <param name="eccpBaseMaintenanceCompanyRepository">
        /// The eccp base maintenance company repository.
        /// </param>
        /// <param name="eccpBaseMaintenanceCompaniesExcelExporter">
        /// The eccp base maintenance companies excel exporter.
        /// </param>
        /// <param name="eccpBaseAreaRepository">
        /// The eccp base area repository.
        /// </param>
        /// <param name="eccpMaintenanceCompanyChangeLogRepository">
        /// The eccp maintenance company change log repository.
        /// </param>
        public ECCPBaseMaintenanceCompaniesAppService(
            IRepository<ECCPBaseMaintenanceCompany> eccpBaseMaintenanceCompanyRepository,
            IECCPBaseMaintenanceCompaniesExcelExporter eccpBaseMaintenanceCompaniesExcelExporter,
            IRepository<ECCPBaseArea, int> eccpBaseAreaRepository,
            IRepository<EccpMaintenanceCompanyChangeLog, int> eccpMaintenanceCompanyChangeLogRepository,
            ITenantRegistrationAppService tenantRegistrationAppService,
            ITempFileCacheManager tempFileCacheManager,
            IBinaryObjectManager binaryObjectManager,
            IRepository<EccpMaintenanceCompanyExtension> eccpMaintenanceCompanyExtensionRepository
            )
        {
            this._eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
            this._eccpBaseMaintenanceCompaniesExcelExporter = eccpBaseMaintenanceCompaniesExcelExporter;
            this._eccpBaseAreaRepository = eccpBaseAreaRepository;
            this._eccpMaintenanceCompanyChangeLogRepository = eccpMaintenanceCompanyChangeLogRepository;
            _tenantRegistrationAppService = tenantRegistrationAppService;
            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            this._eccpMaintenanceCompanyExtensionRepository = eccpMaintenanceCompanyExtensionRepository;

        }

        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task CreateOrEdit(CreateOrEditECCPBaseMaintenanceCompanyDto input)
        {
            if (input.Id == null)
            {
                await this.Create(input);
            }
            else
            {
                await this.Update(input);
            }
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies_Delete)]
        public async Task Delete(EntityDto input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                await this._eccpBaseMaintenanceCompanyRepository.DeleteAsync(input.Id);
            }
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<GetECCPBaseMaintenanceCompanyForView>> GetAll(
            GetAllECCPBaseMaintenanceCompaniesInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpBaseMaintenanceCompanies = this._eccpBaseMaintenanceCompanyRepository.GetAll()
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.Name.Contains(input.Filter) || e.Addresse.Contains(input.Filter)
                                                           || e.Longitude.Contains(input.Filter)
                                                           || e.Latitude.Contains(input.Filter)
                                                           || e.Telephone.Contains(input.Filter)
                                                           || e.Summary.Contains(input.Filter))
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.NameFilter),
                        e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.AddresseFilter),
                        e => e.Addresse.ToLower() == input.AddresseFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.LongitudeFilter),
                        e => e.Longitude.ToLower() == input.LongitudeFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.LatitudeFilter),
                        e => e.Latitude.ToLower() == input.LatitudeFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.TelephoneFilter),
                        e => e.Telephone.ToLower() == input.TelephoneFilter.ToLower().Trim());

                var query =
                    (from o in filteredEccpBaseMaintenanceCompanies
                     join o1 in this._eccpBaseAreaRepository.GetAll() on o.ProvinceId equals o1.Id into j1
                     from s1 in j1.DefaultIfEmpty()
                     join o2 in this._eccpBaseAreaRepository.GetAll() on o.CityId equals o2.Id into j2
                     from s2 in j2.DefaultIfEmpty()
                     join o3 in this._eccpBaseAreaRepository.GetAll() on o.DistrictId equals o3.Id into j3
                     from s3 in j3.DefaultIfEmpty()
                     join o4 in this._eccpBaseAreaRepository.GetAll() on o.StreetId equals o4.Id into j4
                     from s4 in j4.DefaultIfEmpty()
                     select new
                     {
                         ECCPBaseMaintenanceCompany = o,
                         ProvinceName = s1 == null ? string.Empty : s1.Name,
                         CityName = s2 == null ? string.Empty : s2.Name,
                         DistrictName = s3 == null ? string.Empty : s3.Name,
                         StreetName = s4 == null ? string.Empty : s4.Name
                     }).WhereIf(
                        !string.IsNullOrWhiteSpace(input.ProvinceNameFilter),
                        e => e.ProvinceName.ToLower() == input.ProvinceNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.CityNameFilter),
                        e => e.CityName.ToLower() == input.CityNameFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.DistrictNameFilter),
                        e => e.DistrictName.ToLower() == input.DistrictNameFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.StreetNameFilter),
                        e => e.StreetName.ToLower() == input.StreetNameFilter.ToLower().Trim());

                var totalCount = await query.CountAsync();

                var eccpBaseMaintenanceCompanies = new List<GetECCPBaseMaintenanceCompanyForView>();

                query.OrderBy(input.Sorting ?? "eCCPBaseMaintenanceCompany.id asc").PageBy(input).MapTo(eccpBaseMaintenanceCompanies);

                return new PagedResultDto<GetECCPBaseMaintenanceCompanyForView>(
                    totalCount,
                    eccpBaseMaintenanceCompanies);
            }
        }

        /// <summary>
        /// The get all eccp base area for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies)]
        public async Task<PagedResultDto<ECCPBaseAreaLookupTableDto>> GetAllECCPBaseAreaForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseAreaRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter)).Where(e => e.ParentId == input.ParentId);

            var totalCount = await query.CountAsync();

            var eccpBaseAreaList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<ECCPBaseAreaLookupTableDto>();
            foreach (var eccpBaseArea in eccpBaseAreaList)
            {
                lookupTableDtoList.Add(
                    new ECCPBaseAreaLookupTableDto { Id = eccpBaseArea.Id, DisplayName = eccpBaseArea.Name });
            }

            return new PagedResultDto<ECCPBaseAreaLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get eccp base maintenance companies to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetECCPBaseMaintenanceCompaniesToExcel(
            GetAllECCPBaseMaintenanceCompaniesForExcelInput input)
        {
            var filteredEccpBaseMaintenanceCompanies = this._eccpBaseMaintenanceCompanyRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.Contains(input.Filter) || e.Addresse.Contains(input.Filter)
                                                       || e.Longitude.Contains(input.Filter)
                                                       || e.Latitude.Contains(input.Filter)
                                                       || e.Telephone.Contains(input.Filter)
                                                       || e.Summary.Contains(input.Filter))
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.NameFilter),
                    e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.AddresseFilter),
                    e => e.Addresse.ToLower() == input.AddresseFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.LongitudeFilter),
                    e => e.Longitude.ToLower() == input.LongitudeFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.LatitudeFilter),
                    e => e.Latitude.ToLower() == input.LatitudeFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.TelephoneFilter),
                    e => e.Telephone.ToLower() == input.TelephoneFilter.ToLower().Trim());

            var query =
                (from o in filteredEccpBaseMaintenanceCompanies
                 join o1 in this._eccpBaseAreaRepository.GetAll() on o.ProvinceId equals o1.Id into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpBaseAreaRepository.GetAll() on o.CityId equals o2.Id into j2
                 from s2 in j2.DefaultIfEmpty()
                 join o3 in this._eccpBaseAreaRepository.GetAll() on o.DistrictId equals o3.Id into j3
                 from s3 in j3.DefaultIfEmpty()
                 join o4 in this._eccpBaseAreaRepository.GetAll() on o.StreetId equals o4.Id into j4
                 from s4 in j4.DefaultIfEmpty()
                 select new GetECCPBaseMaintenanceCompanyForView
                 {
                     ECCPBaseMaintenanceCompany = this.ObjectMapper.Map<ECCPBaseMaintenanceCompanyDto>(o),
                     ProvinceName = s1 == null ? string.Empty : s1.Name,
                     CityName = s2 == null ? string.Empty : s2.Name,
                     DistrictName = s3 == null ? string.Empty : s3.Name,
                     StreetName = s4 == null ? string.Empty : s4.Name
                 }).WhereIf(
                    !string.IsNullOrWhiteSpace(input.ProvinceNameFilter),
                    e => e.ProvinceName.ToLower() == input.ProvinceNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.CityNameFilter),
                    e => e.CityName.ToLower() == input.CityNameFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.DistrictNameFilter),
                    e => e.DistrictName.ToLower() == input.DistrictNameFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.StreetNameFilter),
                    e => e.StreetName.ToLower() == input.StreetNameFilter.ToLower().Trim());

            var eccpBaseMaintenanceCompanyListDtos = await query.ToListAsync();

            return this._eccpBaseMaintenanceCompaniesExcelExporter.ExportToFile(eccpBaseMaintenanceCompanyListDtos);
        }

        /// <summary>
        /// The get eccp base maintenance company for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies_Edit)]
        public async Task<GetECCPBaseMaintenanceCompanyForEditOutput> GetECCPBaseMaintenanceCompanyForEdit(
            EntityDto input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpBaseMaintenanceCompany =
                    await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(input.Id);

                var output = new GetECCPBaseMaintenanceCompanyForEditOutput
                {
                    ECCPBaseMaintenanceCompany =
                                         this.ObjectMapper.Map<EditECCPBaseMaintenanceCompanyDto>(
                                             eccpBaseMaintenanceCompany)
                };

                var eccpMaintenanceCompanyExtension = await this._eccpMaintenanceCompanyExtensionRepository.FirstOrDefaultAsync(w => w.MaintenanceCompanyId == input.Id);
                if (eccpMaintenanceCompanyExtension != null)
                {
                    output.BusinessLicenseId = eccpMaintenanceCompanyExtension.BusinessLicenseId;
                    output.AptitudePhotoId = eccpMaintenanceCompanyExtension.AptitudePhotoId;
                }
                
                if (output.ECCPBaseMaintenanceCompany.ProvinceId != null)
                {
                    var province =
                        await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                            (int)output.ECCPBaseMaintenanceCompany.ProvinceId);
                    output.ProvinceName = province.Name;
                }

                if (output.ECCPBaseMaintenanceCompany.CityId != null)
                {
                    var city = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                                   (int)output.ECCPBaseMaintenanceCompany.CityId);
                    output.CityName = city.Name;
                }

                if (output.ECCPBaseMaintenanceCompany.DistrictId != null)
                {
                    var district =
                        await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                            (int)output.ECCPBaseMaintenanceCompany.DistrictId);
                    output.DistrictName = district.Name;
                }

                if (output.ECCPBaseMaintenanceCompany.StreetId != null)
                {
                    var street =
                        await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                            (int)output.ECCPBaseMaintenanceCompany.StreetId);
                    output.StreetName = street.Name;
                }

                return output;
            }
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies_Create)]
        private async Task Create(CreateOrEditECCPBaseMaintenanceCompanyDto input)
        {
            RegisterTenantInput tenant = new RegisterTenantInput();
            tenant.EditionId = 2;
            tenant.SubscriptionStartType = SubscriptionStartType.Free;          
            tenant.TenancyName = input.TenancyName;
            tenant.Name = input.Name;
            tenant.AdminPassword = input.AdminPassword;
            tenant.AdminEmailAddress = input.AdminEmailAddress;
            tenant.LegalPerson = input.LegalPerson;
            tenant.Mobile = input.Mobile;
            tenant.IsMember = false;

            var result = await _tenantRegistrationAppService.RegisterTenant(tenant);
            CurrentUnitOfWork.SetTenantId(result.TenantId);

            var eccpBaseMaintenanceCompany = this._eccpBaseMaintenanceCompanyRepository.FirstOrDefault(m=>m.TenantId== result.TenantId);
            input.Id = eccpBaseMaintenanceCompany.Id;
            this.ObjectMapper.Map(input, eccpBaseMaintenanceCompany);

            if (input.BusinessLicenseIdFileToken != null && input.AptitudePhotoIdFileToken != null)
            {
                input.BusinessLicenseId = await this.SaveFileAndGetFileId(input.BusinessLicenseIdFileToken);
                input.AptitudePhotoId = await this.SaveFileAndGetFileId(input.AptitudePhotoIdFileToken);
            }

            var eccpMaintenanceCompanyExtension = await this._eccpMaintenanceCompanyExtensionRepository.FirstOrDefaultAsync(w => w.MaintenanceCompanyId == input.Id);
            eccpMaintenanceCompanyExtension.BusinessLicenseId = input.BusinessLicenseId;
            eccpMaintenanceCompanyExtension.AptitudePhotoId = input.AptitudePhotoId;
            await this._eccpMaintenanceCompanyExtensionRepository.UpdateAsync(eccpMaintenanceCompanyExtension);
           
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

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies_Edit)]
        private async Task Update(CreateOrEditECCPBaseMaintenanceCompanyDto input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                if (input.Id != null)
                {
                    var eccpBaseMaintenanceCompany =
                        await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync((int)input.Id);

                    if (eccpBaseMaintenanceCompany.OrgOrganizationalCode != input.OrgOrganizationalCode)
                    {
                        await this._eccpMaintenanceCompanyChangeLogRepository.InsertAsync(
                            new EccpMaintenanceCompanyChangeLog
                            {
                                FieldName = "OrgOrganizationalCode",
                                OldValue = eccpBaseMaintenanceCompany.OrgOrganizationalCode,
                                NewValue = input.OrgOrganizationalCode,
                                MaintenanceCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBaseMaintenanceCompany.Name != input.Name)
                    {
                        await this._eccpMaintenanceCompanyChangeLogRepository.InsertAsync(
                            new EccpMaintenanceCompanyChangeLog
                            {
                                FieldName = "Name",
                                OldValue = eccpBaseMaintenanceCompany.Name,
                                NewValue = input.Name,
                                MaintenanceCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBaseMaintenanceCompany.Addresse != input.Addresse)
                    {
                        await this._eccpMaintenanceCompanyChangeLogRepository.InsertAsync(
                            new EccpMaintenanceCompanyChangeLog
                            {
                                FieldName = "Addresse",
                                OldValue = eccpBaseMaintenanceCompany.Addresse,
                                NewValue = input.Addresse,
                                MaintenanceCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBaseMaintenanceCompany.Longitude != input.Longitude)
                    {
                        await this._eccpMaintenanceCompanyChangeLogRepository.InsertAsync(
                            new EccpMaintenanceCompanyChangeLog
                            {
                                FieldName = "Longitude",
                                OldValue = eccpBaseMaintenanceCompany.Longitude,
                                NewValue = input.Longitude,
                                MaintenanceCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBaseMaintenanceCompany.Latitude != input.Latitude)
                    {
                        await this._eccpMaintenanceCompanyChangeLogRepository.InsertAsync(
                            new EccpMaintenanceCompanyChangeLog
                            {
                                FieldName = "Latitude",
                                OldValue = eccpBaseMaintenanceCompany.Latitude,
                                NewValue = input.Latitude,
                                MaintenanceCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBaseMaintenanceCompany.Telephone != input.Telephone)
                    {
                        await this._eccpMaintenanceCompanyChangeLogRepository.InsertAsync(
                            new EccpMaintenanceCompanyChangeLog
                            {
                                FieldName = "Telephone",
                                OldValue = eccpBaseMaintenanceCompany.Telephone,
                                NewValue = input.Telephone,
                                MaintenanceCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBaseMaintenanceCompany.Summary != input.Summary)
                    {
                        await this._eccpMaintenanceCompanyChangeLogRepository.InsertAsync(
                            new EccpMaintenanceCompanyChangeLog
                            {
                                FieldName = "Summary",
                                OldValue = eccpBaseMaintenanceCompany.Summary,
                                NewValue = input.Summary,
                                MaintenanceCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBaseMaintenanceCompany.ProvinceId != input.ProvinceId)
                    {
                        await this._eccpMaintenanceCompanyChangeLogRepository.InsertAsync(
                            new EccpMaintenanceCompanyChangeLog
                            {
                                FieldName = "ProvinceId",
                                OldValue = eccpBaseMaintenanceCompany.ProvinceId.ToString(),
                                NewValue = input.ProvinceId.ToString(),
                                MaintenanceCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBaseMaintenanceCompany.CityId != input.CityId)
                    {
                        await this._eccpMaintenanceCompanyChangeLogRepository.InsertAsync(
                            new EccpMaintenanceCompanyChangeLog
                            {
                                FieldName = "CityId",
                                OldValue = eccpBaseMaintenanceCompany.CityId.ToString(),
                                NewValue = input.CityId.ToString(),
                                MaintenanceCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBaseMaintenanceCompany.DistrictId != input.DistrictId)
                    {
                        await this._eccpMaintenanceCompanyChangeLogRepository.InsertAsync(
                            new EccpMaintenanceCompanyChangeLog
                            {
                                FieldName = "DistrictId",
                                OldValue = eccpBaseMaintenanceCompany.DistrictId.ToString(),
                                NewValue = input.DistrictId.ToString(),
                                MaintenanceCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBaseMaintenanceCompany.StreetId != input.StreetId)
                    {
                        await this._eccpMaintenanceCompanyChangeLogRepository.InsertAsync(
                            new EccpMaintenanceCompanyChangeLog
                            {
                                FieldName = "StreetId",
                                OldValue = eccpBaseMaintenanceCompany.StreetId.ToString(),
                                NewValue = input.StreetId.ToString(),
                                MaintenanceCompanyId = input.Id.Value
                            });
                    }

                    var eccpMaintenanceCompanyExtension = await _eccpMaintenanceCompanyExtensionRepository.FirstOrDefaultAsync(w => w.MaintenanceCompanyId == input.Id);
                    if (eccpMaintenanceCompanyExtension == null)
                    {
                        input.BusinessLicenseId = await this.SaveFileAndGetFileId(input.BusinessLicenseIdFileToken);
                        input.AptitudePhotoId = await this.SaveFileAndGetFileId(input.AptitudePhotoIdFileToken);

                        await this._eccpMaintenanceCompanyExtensionRepository.InsertAsync(
                                new EccpMaintenanceCompanyExtension
                                {
                                    AptitudePhotoId = input.AptitudePhotoId,
                                    BusinessLicenseId = input.BusinessLicenseId,
                                    IsMember = false,
                                    LegalPerson = "暂无",
                                    Mobile = "暂无",
                                    MaintenanceCompanyId = Convert.ToInt32(input.Id)
                                });
                    }
                    else
                    {
                        if (eccpMaintenanceCompanyExtension.BusinessLicenseId == null)
                        {
                            eccpMaintenanceCompanyExtension.BusinessLicenseId = await this.SaveFileAndGetFileId(input.BusinessLicenseIdFileToken);

                            await this._eccpMaintenanceCompanyExtensionRepository.UpdateAsync(eccpMaintenanceCompanyExtension);
                        }
                        else if (input.BusinessLicenseIdFileToken != eccpMaintenanceCompanyExtension.BusinessLicenseId.ToString())
                        {
                            await this._binaryObjectManager.DeleteAsync(eccpMaintenanceCompanyExtension.BusinessLicenseId.Value);

                            eccpMaintenanceCompanyExtension.BusinessLicenseId = await this.SaveFileAndGetFileId(input.BusinessLicenseIdFileToken);

                            await this._eccpMaintenanceCompanyExtensionRepository.UpdateAsync(eccpMaintenanceCompanyExtension);
                        }


                        if (eccpMaintenanceCompanyExtension.AptitudePhotoId == null)
                        {
                            eccpMaintenanceCompanyExtension.AptitudePhotoId = await this.SaveFileAndGetFileId(input.AptitudePhotoIdFileToken);

                            await this._eccpMaintenanceCompanyExtensionRepository.UpdateAsync(eccpMaintenanceCompanyExtension);
                        }
                        else if (input.AptitudePhotoIdFileToken != eccpMaintenanceCompanyExtension.AptitudePhotoId.ToString())
                        {
                            await this._binaryObjectManager.DeleteAsync(eccpMaintenanceCompanyExtension.AptitudePhotoId.Value);

                            eccpMaintenanceCompanyExtension.AptitudePhotoId = await this.SaveFileAndGetFileId(input.AptitudePhotoIdFileToken);

                            await this._eccpMaintenanceCompanyExtensionRepository.UpdateAsync(eccpMaintenanceCompanyExtension);
                        }
                    }

                    this.ObjectMapper.Map(input, eccpBaseMaintenanceCompany);
                }
            }
        }
    }
}