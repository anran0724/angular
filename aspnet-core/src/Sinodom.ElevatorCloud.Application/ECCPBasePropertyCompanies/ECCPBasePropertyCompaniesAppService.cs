// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBasePropertyCompaniesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies
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
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Exporting;
    using Sinodom.ElevatorCloud.MultiTenancy;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions;
    using Sinodom.ElevatorCloud.MultiTenancy.Dto;
    using Sinodom.ElevatorCloud.MultiTenancy.Payments.Dto;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    ///     The eccp base property companies app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies)]
    public class ECCPBasePropertyCompaniesAppService : ElevatorCloudAppServiceBase, IECCPBasePropertyCompaniesAppService
    {
        /// <summary>
        ///     The _eccp base area repository.
        /// </summary>
        private readonly IRepository<ECCPBaseArea, int> _eccpBaseAreaRepository;

        /// <summary>
        ///     The _eccp base property companies excel exporter.
        /// </summary>
        private readonly IECCPBasePropertyCompaniesExcelExporter _eccpBasePropertyCompaniesExcelExporter;

        /// <summary>
        ///     The _eccp base property company repository.
        /// </summary>
        private readonly IRepository<ECCPBasePropertyCompany> _eccpBasePropertyCompanyRepository;

        /// <summary>
        ///     The _eccp property company change log repository.
        /// </summary>
        private readonly IRepository<EccpPropertyCompanyChangeLog, int> _eccpPropertyCompanyChangeLogRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private const int MaxProfilPictureBytes = 5048576; //5MB
        private readonly IRepository<EccpPropertyCompanyExtension> _eccpPropertyCompanyExtensionRepository;
        private readonly ITenantRegistrationAppService _tenantRegistrationAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBasePropertyCompaniesAppService"/> class.
        /// </summary>
        /// <param name="eccpBasePropertyCompanyRepository">
        /// The eccp base property company repository.
        /// </param>
        /// <param name="eccpBasePropertyCompaniesExcelExporter">
        /// The eccp base property companies excel exporter.
        /// </param>
        /// <param name="eccpBaseAreaRepository">
        /// The eccp base area repository.
        /// </param>
        /// <param name="eccpPropertyCompanyChangeLogRepository">
        /// The eccp property company change log repository.
        /// </param>
        public ECCPBasePropertyCompaniesAppService(
            IRepository<ECCPBasePropertyCompany> eccpBasePropertyCompanyRepository,
            IECCPBasePropertyCompaniesExcelExporter eccpBasePropertyCompaniesExcelExporter,
            IRepository<ECCPBaseArea, int> eccpBaseAreaRepository,
            IRepository<EccpPropertyCompanyChangeLog, int> eccpPropertyCompanyChangeLogRepository,
            ITempFileCacheManager tempFileCacheManager,
            IBinaryObjectManager binaryObjectManager,
            IRepository<EccpPropertyCompanyExtension> eccpPropertyCompanyExtensionRepository,
            ITenantRegistrationAppService tenantRegistrationAppService)
        {
            this._eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
            this._eccpBasePropertyCompaniesExcelExporter = eccpBasePropertyCompaniesExcelExporter;
            this._eccpBaseAreaRepository = eccpBaseAreaRepository;
            this._eccpPropertyCompanyChangeLogRepository = eccpPropertyCompanyChangeLogRepository;
            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            _eccpPropertyCompanyExtensionRepository = eccpPropertyCompanyExtensionRepository;
            _tenantRegistrationAppService = tenantRegistrationAppService;
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
        public async Task CreateOrEdit(CreateOrEditECCPBasePropertyCompanyDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpBasePropertyCompanyRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetECCPBasePropertyCompanyForView>> GetAll(
            GetAllECCPBasePropertyCompaniesInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpBasePropertyCompanies = this._eccpBasePropertyCompanyRepository.GetAll()
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.Name.Contains(input.Filter) || e.Addresse.Contains(input.Filter)
                                                           || e.Telephone.Contains(input.Filter))
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.NameFilter),
                        e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.AddresseFilter),
                        e => e.Addresse.ToLower() == input.AddresseFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.TelephoneFilter),
                        e => e.Telephone.ToLower() == input.TelephoneFilter.ToLower().Trim());

                var query = from o in filteredEccpBasePropertyCompanies
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
                                ECCPBasePropertyCompany = o,
                                ProvinceName = s1 == null ? string.Empty : s1.Name,
                                CityName = s2 == null ? string.Empty : s2.Name,
                                DistrictName = s3 == null ? string.Empty : s3.Name,
                                StreetName = s4 == null ? string.Empty : s4.Name
                            };

                var totalCount = await query.CountAsync();

                var eccpBasePropertyCompanies = new List<GetECCPBasePropertyCompanyForView>();

                query.OrderBy(input.Sorting ?? "eCCPBasePropertyCompany.id asc").PageBy(input).MapTo(eccpBasePropertyCompanies);

                return new PagedResultDto<GetECCPBasePropertyCompanyForView>(totalCount, eccpBasePropertyCompanies);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies)]
        public async Task<PagedResultDto<ECCPBaseAreaLookupTableDto>> GetAllECCPBaseAreaForLookupTable(
            GetAllForLookupTableInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
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
        }

        /// <summary>
        /// The get eccp base property companies to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetECCPBasePropertyCompaniesToExcel(
            GetAllECCPBasePropertyCompaniesForExcelInput input)
        {
            var filteredEccpBasePropertyCompanies = this._eccpBasePropertyCompanyRepository.GetAll()
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
                (from o in filteredEccpBasePropertyCompanies
                 join o1 in this._eccpBaseAreaRepository.GetAll() on o.ProvinceId equals o1.Id into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpBaseAreaRepository.GetAll() on o.CityId equals o2.Id into j2
                 from s2 in j2.DefaultIfEmpty()
                 join o3 in this._eccpBaseAreaRepository.GetAll() on o.DistrictId equals o3.Id into j3
                 from s3 in j3.DefaultIfEmpty()
                 join o4 in this._eccpBaseAreaRepository.GetAll() on o.StreetId equals o4.Id into j4
                 from s4 in j4.DefaultIfEmpty()
                 select new GetECCPBasePropertyCompanyForView
                 {
                     ECCPBasePropertyCompany = this.ObjectMapper.Map<ECCPBasePropertyCompanyDto>(o),
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

            var eccpBasePropertyCompanyListDtos = await query.ToListAsync();

            return this._eccpBasePropertyCompaniesExcelExporter.ExportToFile(eccpBasePropertyCompanyListDtos);
        }

        /// <summary>
        /// The get eccp base property company for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies_Edit)]
        public async Task<GetECCPBasePropertyCompanyForEditOutput> GetECCPBasePropertyCompanyForEdit(EntityDto input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpBasePropertyCompany =
                    await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(input.Id);

                var output = new GetECCPBasePropertyCompanyForEditOutput
                {
                    ECCPBasePropertyCompany =
                                         this.ObjectMapper.Map<EditECCPBasePropertyCompanyDto>(
                                             eccpBasePropertyCompany)
                };


                var eccpPropertyCompanyExtension = await _eccpPropertyCompanyExtensionRepository.FirstOrDefaultAsync(w => w.PropertyCompanyId == input.Id);
                if (eccpPropertyCompanyExtension != null)
                {
                    output.AptitudePhotoId = eccpPropertyCompanyExtension.AptitudePhotoId;
                    output.BusinessLicenseId = eccpPropertyCompanyExtension.BusinessLicenseId;
                }


                if (output.ECCPBasePropertyCompany.ProvinceId != null)
                {
                    var province =
                        await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                            (int)output.ECCPBasePropertyCompany.ProvinceId);
                    output.ProvinceName = province.Name;
                }

                if (output.ECCPBasePropertyCompany.CityId != null)
                {
                    var city = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                                   (int)output.ECCPBasePropertyCompany.CityId);
                    output.CityName = city.Name;
                }

                if (output.ECCPBasePropertyCompany.DistrictId != null)
                {
                    var district =
                        await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                            (int)output.ECCPBasePropertyCompany.DistrictId);
                    output.DistrictName = district.Name;
                }

                if (output.ECCPBasePropertyCompany.StreetId != null)
                {
                    var street =
                        await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                            (int)output.ECCPBasePropertyCompany.StreetId);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies_Create)]
        private async Task Create(CreateOrEditECCPBasePropertyCompanyDto input)
        {
            RegisterTenantInput tenant = new RegisterTenantInput();
            tenant.EditionId = 3;
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

            var eccpBasePropertyCompany = this._eccpBasePropertyCompanyRepository.FirstOrDefault(m => m.TenantId == result.TenantId);
            input.Id = eccpBasePropertyCompany.Id;
            this.ObjectMapper.Map(input, eccpBasePropertyCompany);

            if (input.BusinessLicenseIdFileToken != null && input.AptitudePhotoIdFileToken != null)
            {
                input.BusinessLicenseId = await this.SaveFileAndGetFileId(input.BusinessLicenseIdFileToken);
                input.AptitudePhotoId = await this.SaveFileAndGetFileId(input.AptitudePhotoIdFileToken);
            }

            var eccpPropertyCompanyExtension = await this._eccpPropertyCompanyExtensionRepository.FirstOrDefaultAsync(w => w.PropertyCompanyId == input.Id);
            eccpPropertyCompanyExtension.BusinessLicenseId = input.BusinessLicenseId;
            eccpPropertyCompanyExtension.AptitudePhotoId = input.AptitudePhotoId;
            await this._eccpPropertyCompanyExtensionRepository.UpdateAsync(eccpPropertyCompanyExtension);           
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies_Edit)]
        private async Task Update(CreateOrEditECCPBasePropertyCompanyDto input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                if (input.Id != null)
                {
                    var eccpBasePropertyCompany =
                        await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync((int)input.Id);

                    if (eccpBasePropertyCompany.Name != input.Name)
                    {
                        await this._eccpPropertyCompanyChangeLogRepository.InsertAsync(
                            new EccpPropertyCompanyChangeLog
                            {
                                FieldName = "Name",
                                OldValue = eccpBasePropertyCompany.Name,
                                NewValue = input.Name,
                                PropertyCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBasePropertyCompany.Addresse != input.Addresse)
                    {
                        await this._eccpPropertyCompanyChangeLogRepository.InsertAsync(
                            new EccpPropertyCompanyChangeLog
                            {
                                FieldName = "Addresse",
                                OldValue = eccpBasePropertyCompany.Addresse,
                                NewValue = input.Addresse,
                                PropertyCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBasePropertyCompany.Longitude != input.Longitude)
                    {
                        await this._eccpPropertyCompanyChangeLogRepository.InsertAsync(
                            new EccpPropertyCompanyChangeLog
                            {
                                FieldName = "Longitude",
                                OldValue = eccpBasePropertyCompany.Longitude,
                                NewValue = input.Longitude,
                                PropertyCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBasePropertyCompany.Latitude != input.Latitude)
                    {
                        await this._eccpPropertyCompanyChangeLogRepository.InsertAsync(
                            new EccpPropertyCompanyChangeLog
                            {
                                FieldName = "Latitude",
                                OldValue = eccpBasePropertyCompany.Latitude,
                                NewValue = input.Latitude,
                                PropertyCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBasePropertyCompany.Telephone != input.Telephone)
                    {
                        await this._eccpPropertyCompanyChangeLogRepository.InsertAsync(
                            new EccpPropertyCompanyChangeLog
                            {
                                FieldName = "Telephone",
                                OldValue = eccpBasePropertyCompany.Telephone,
                                NewValue = input.Telephone,
                                PropertyCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBasePropertyCompany.Summary != input.Summary)
                    {
                        await this._eccpPropertyCompanyChangeLogRepository.InsertAsync(
                            new EccpPropertyCompanyChangeLog
                            {
                                FieldName = "Summary",
                                OldValue = eccpBasePropertyCompany.Summary,
                                NewValue = input.Summary,
                                PropertyCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBasePropertyCompany.ProvinceId != input.ProvinceId)
                    {
                        await this._eccpPropertyCompanyChangeLogRepository.InsertAsync(
                            new EccpPropertyCompanyChangeLog
                            {
                                FieldName = "ProvinceId",
                                OldValue = eccpBasePropertyCompany.ProvinceId.ToString(),
                                NewValue = input.ProvinceId.ToString(),
                                PropertyCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBasePropertyCompany.CityId != input.CityId)
                    {
                        await this._eccpPropertyCompanyChangeLogRepository.InsertAsync(
                            new EccpPropertyCompanyChangeLog
                            {
                                FieldName = "CityId",
                                OldValue = eccpBasePropertyCompany.CityId.ToString(),
                                NewValue = input.CityId.ToString(),
                                PropertyCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBasePropertyCompany.DistrictId != input.DistrictId)
                    {
                        await this._eccpPropertyCompanyChangeLogRepository.InsertAsync(
                            new EccpPropertyCompanyChangeLog
                            {
                                FieldName = "DistrictId",
                                OldValue = eccpBasePropertyCompany.DistrictId.ToString(),
                                NewValue = input.DistrictId.ToString(),
                                PropertyCompanyId = input.Id.Value
                            });
                    }

                    if (eccpBasePropertyCompany.StreetId != input.StreetId)
                    {
                        await this._eccpPropertyCompanyChangeLogRepository.InsertAsync(
                            new EccpPropertyCompanyChangeLog
                            {
                                FieldName = "StreetId",
                                OldValue = eccpBasePropertyCompany.StreetId.ToString(),
                                NewValue = input.StreetId.ToString(),
                                PropertyCompanyId = input.Id.Value
                            });
                    }

                    var eccpPropertyCompanyExtension = await _eccpPropertyCompanyExtensionRepository.FirstOrDefaultAsync(w => w.PropertyCompanyId == input.Id);
                    if (eccpPropertyCompanyExtension == null)
                    {
                        input.BusinessLicenseId = await this.SaveFileAndGetFileId(input.BusinessLicenseIdFileToken);
                        input.AptitudePhotoId = await this.SaveFileAndGetFileId(input.AptitudePhotoIdFileToken);

                        await this._eccpPropertyCompanyExtensionRepository.InsertAsync(
                                new EccpPropertyCompanyExtension
                                {
                                    AptitudePhotoId = input.AptitudePhotoId,
                                    BusinessLicenseId = input.BusinessLicenseId,
                                    IsMember = false,
                                    LegalPerson = "暂无",
                                    Mobile = "暂无",
                                    PropertyCompanyId = Convert.ToInt32(input.Id)
                                });
                    }
                    else
                    {
                        if (eccpPropertyCompanyExtension.BusinessLicenseId == null)
                        {
                            eccpPropertyCompanyExtension.BusinessLicenseId = await this.SaveFileAndGetFileId(input.BusinessLicenseIdFileToken);

                            await this._eccpPropertyCompanyExtensionRepository.UpdateAsync(eccpPropertyCompanyExtension);
                        }
                        else if (input.BusinessLicenseIdFileToken != eccpPropertyCompanyExtension.BusinessLicenseId.ToString())
                        {
                            await this._binaryObjectManager.DeleteAsync(eccpPropertyCompanyExtension.BusinessLicenseId.Value);

                            eccpPropertyCompanyExtension.BusinessLicenseId = await this.SaveFileAndGetFileId(input.BusinessLicenseIdFileToken);

                            await this._eccpPropertyCompanyExtensionRepository.UpdateAsync(eccpPropertyCompanyExtension);
                        }


                        if (eccpPropertyCompanyExtension.AptitudePhotoId == null)
                        {
                            eccpPropertyCompanyExtension.AptitudePhotoId = await this.SaveFileAndGetFileId(input.AptitudePhotoIdFileToken);

                            await this._eccpPropertyCompanyExtensionRepository.UpdateAsync(eccpPropertyCompanyExtension);
                        }
                        else if (input.AptitudePhotoIdFileToken != eccpPropertyCompanyExtension.AptitudePhotoId.ToString())
                        {
                            await this._binaryObjectManager.DeleteAsync(eccpPropertyCompanyExtension.AptitudePhotoId.Value);

                            eccpPropertyCompanyExtension.AptitudePhotoId = await this.SaveFileAndGetFileId(input.AptitudePhotoIdFileToken);

                            await this._eccpPropertyCompanyExtensionRepository.UpdateAsync(eccpPropertyCompanyExtension);
                        }
                    }

                    this.ObjectMapper.Map(input, eccpBasePropertyCompany);
                }
            }
        }
    }
}