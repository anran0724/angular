// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseProductionCompaniesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseProductionCompanies
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.AutoMapper;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Linq.Extensions;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseAreas;
    using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies.Dtos;
    using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies.Exporting;

    /// <summary>
    ///     The eccp base production companies app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies)]
    public class ECCPBaseProductionCompaniesAppService : ElevatorCloudAppServiceBase,
                                                         IECCPBaseProductionCompaniesAppService
    {
        /// <summary>
        ///     The _eccp base area repository.
        /// </summary>
        private readonly IRepository<ECCPBaseArea, int> _eccpBaseAreaRepository;

        /// <summary>
        ///     The _eccp base production companies excel exporter.
        /// </summary>
        private readonly IECCPBaseProductionCompaniesExcelExporter _eccpBaseProductionCompaniesExcelExporter;

        /// <summary>
        ///     The _eccp base production company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseProductionCompany, long> _eccpBaseProductionCompanyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseProductionCompaniesAppService"/> class.
        /// </summary>
        /// <param name="eccpBaseProductionCompanyRepository">
        /// The eccp base production company repository.
        /// </param>
        /// <param name="eccpBaseProductionCompaniesExcelExporter">
        /// The eccp base production companies excel exporter.
        /// </param>
        /// <param name="eccpBaseAreaRepository">
        /// The eccp base area repository.
        /// </param>
        public ECCPBaseProductionCompaniesAppService(
            IRepository<ECCPBaseProductionCompany, long> eccpBaseProductionCompanyRepository,
            IECCPBaseProductionCompaniesExcelExporter eccpBaseProductionCompaniesExcelExporter,
            IRepository<ECCPBaseArea, int> eccpBaseAreaRepository)
        {
            this._eccpBaseProductionCompanyRepository = eccpBaseProductionCompanyRepository;
            this._eccpBaseProductionCompaniesExcelExporter = eccpBaseProductionCompaniesExcelExporter;
            this._eccpBaseAreaRepository = eccpBaseAreaRepository;
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
        public async Task CreateOrEdit(CreateOrEditECCPBaseProductionCompanyDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                await this._eccpBaseProductionCompanyRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetECCPBaseProductionCompanyForView>> GetAll(
            GetAllECCPBaseProductionCompaniesInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpBaseProductionCompanies = this._eccpBaseProductionCompanyRepository.GetAll()
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.Name.Contains(input.Filter) || e.Addresse.Contains(input.Filter)
                                                           || e.Telephone.Contains(input.Filter)
                                                           || e.Summary.Contains(input.Filter))
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.NameFilter),
                        e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.AddresseFilter),
                        e => e.Addresse.ToLower() == input.AddresseFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.TelephoneFilter),
                        e => e.Telephone.ToLower() == input.TelephoneFilter.ToLower().Trim());

                var query =
                    (from o in filteredEccpBaseProductionCompanies
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
                                    ECCPBaseProductionCompany = o,
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

                var eccpBaseProductionCompanies = new List<GetECCPBaseProductionCompanyForView>();
                    
                query.OrderBy(input.Sorting ?? "eCCPBaseProductionCompany.id asc").PageBy(input).MapTo(eccpBaseProductionCompanies);

                return new PagedResultDto<GetECCPBaseProductionCompanyForView>(totalCount, eccpBaseProductionCompanies);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies)]
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
        /// The get eccp base production companies to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetECCPBaseProductionCompaniesToExcel(
            GetAllECCPBaseProductionCompaniesForExcelInput input)
        {
            var filteredEccpBaseProductionCompanies = this._eccpBaseProductionCompanyRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.Contains(input.Filter) || e.Addresse.Contains(input.Filter)
                                                       || e.Telephone.Contains(input.Filter)
                                                       || e.Summary.Contains(input.Filter))
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.NameFilter),
                    e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.AddresseFilter),
                    e => e.Addresse.ToLower() == input.AddresseFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.TelephoneFilter),
                    e => e.Telephone.ToLower() == input.TelephoneFilter.ToLower().Trim());

            var query =
                (from o in filteredEccpBaseProductionCompanies
                 join o1 in this._eccpBaseAreaRepository.GetAll() on o.ProvinceId equals o1.Id into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpBaseAreaRepository.GetAll() on o.CityId equals o2.Id into j2
                 from s2 in j2.DefaultIfEmpty()
                 join o3 in this._eccpBaseAreaRepository.GetAll() on o.DistrictId equals o3.Id into j3
                 from s3 in j3.DefaultIfEmpty()
                 join o4 in this._eccpBaseAreaRepository.GetAll() on o.StreetId equals o4.Id into j4
                 from s4 in j4.DefaultIfEmpty()
                 select new GetECCPBaseProductionCompanyForView
                            {
                                ECCPBaseProductionCompany = this.ObjectMapper.Map<ECCPBaseProductionCompanyDto>(o),
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

            var eccpBaseProductionCompanyListDtos = await query.ToListAsync();

            return this._eccpBaseProductionCompaniesExcelExporter.ExportToFile(eccpBaseProductionCompanyListDtos);
        }

        /// <summary>
        /// The get eccp base production company for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies_Edit)]
        public async Task<GetECCPBaseProductionCompanyForEditOutput> GetECCPBaseProductionCompanyForEdit(
            EntityDto<long> input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpBaseProductionCompany =
                    await this._eccpBaseProductionCompanyRepository.FirstOrDefaultAsync(input.Id);

                var output = new GetECCPBaseProductionCompanyForEditOutput
                                 {
                                     ECCPBaseProductionCompany =
                                         this.ObjectMapper.Map<CreateOrEditECCPBaseProductionCompanyDto>(
                                             eccpBaseProductionCompany)
                                 };

                if (output.ECCPBaseProductionCompany.ProvinceId != null)
                {
                    var province =
                        await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                            (int)output.ECCPBaseProductionCompany.ProvinceId);
                    output.ProvinceName = province.Name;
                }

                if (output.ECCPBaseProductionCompany.CityId != null)
                {
                    var city = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                                   (int)output.ECCPBaseProductionCompany.CityId);
                    output.CityName = city.Name;
                }

                if (output.ECCPBaseProductionCompany.DistrictId != null)
                {
                    var district =
                        await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                            (int)output.ECCPBaseProductionCompany.DistrictId);
                    output.DistrictName = district.Name;
                }

                if (output.ECCPBaseProductionCompany.StreetId != null)
                {
                    var street =
                        await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                            (int)output.ECCPBaseProductionCompany.StreetId);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies_Create)]
        private async Task Create(CreateOrEditECCPBaseProductionCompanyDto input)
        {
            var eccpBaseProductionCompany = this.ObjectMapper.Map<ECCPBaseProductionCompany>(input);

            await this._eccpBaseProductionCompanyRepository.InsertAsync(eccpBaseProductionCompany);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies_Edit)]
        private async Task Update(CreateOrEditECCPBaseProductionCompanyDto input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                if (input.Id != null)
                {
                    var eccpBaseProductionCompany =
                        await this._eccpBaseProductionCompanyRepository.FirstOrDefaultAsync((long)input.Id);
                    this.ObjectMapper.Map(input, eccpBaseProductionCompany);
                }
            }
        }
    }
}