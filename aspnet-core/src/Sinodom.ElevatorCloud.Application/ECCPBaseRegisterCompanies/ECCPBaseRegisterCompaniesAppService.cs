// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseRegisterCompaniesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.AutoMapper;
    using Abp.Domain.Repositories;
    using Abp.Linq.Extensions;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseAreas;
    using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Dtos;
    using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Exporting;

    /// <summary>
    /// The eccp base register companies app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies)]
    public class ECCPBaseRegisterCompaniesAppService : ElevatorCloudAppServiceBase, IECCPBaseRegisterCompaniesAppService
    {
        /// <summary>
        /// The _eccp base area repository.
        /// </summary>
        private readonly IRepository<ECCPBaseArea, int> _eccpBaseAreaRepository;

        /// <summary>
        /// The _eccp base register companies excel exporter.
        /// </summary>
        private readonly IECCPBaseRegisterCompaniesExcelExporter _eccpBaseRegisterCompaniesExcelExporter;

        /// <summary>
        /// The _eccp base register company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseRegisterCompany, long> _eccpBaseRegisterCompanyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseRegisterCompaniesAppService"/> class.
        /// </summary>
        /// <param name="eccpBaseRegisterCompanyRepository">
        /// The eccp base register company repository.
        /// </param>
        /// <param name="eccpBaseRegisterCompaniesExcelExporter">
        /// The eccp base register companies excel exporter.
        /// </param>
        /// <param name="eccpBaseAreaRepository">
        /// The eccp base area repository.
        /// </param>
        public ECCPBaseRegisterCompaniesAppService(
            IRepository<ECCPBaseRegisterCompany, long> eccpBaseRegisterCompanyRepository,
            IECCPBaseRegisterCompaniesExcelExporter eccpBaseRegisterCompaniesExcelExporter,
            IRepository<ECCPBaseArea, int> eccpBaseAreaRepository)
        {
            this._eccpBaseRegisterCompanyRepository = eccpBaseRegisterCompanyRepository;
            this._eccpBaseRegisterCompaniesExcelExporter = eccpBaseRegisterCompaniesExcelExporter;
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
        public async Task CreateOrEdit(CreateOrEditECCPBaseRegisterCompanyDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await this._eccpBaseRegisterCompanyRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetECCPBaseRegisterCompanyForView>> GetAll(
            GetAllECCPBaseRegisterCompaniesInput input)
        {
            var filteredEccpBaseRegisterCompanies = this._eccpBaseRegisterCompanyRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.Contains(input.Filter) || e.Addresse.Contains(input.Filter)
                         || e.Telephone.Contains(input.Filter) || e.Summary.Contains(input.Filter))
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.NameFilter),
                    e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.AddresseFilter),
                    e => e.Addresse.ToLower() == input.AddresseFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.TelephoneFilter),
                    e => e.Telephone.ToLower() == input.TelephoneFilter.ToLower().Trim());

            var query =
                (from o in filteredEccpBaseRegisterCompanies
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
                                ECCPBaseRegisterCompany = o,
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

            var eccpBaseRegisterCompanies = new List<GetECCPBaseRegisterCompanyForView>();

            query.OrderBy(input.Sorting ?? "eCCPBaseRegisterCompany.id asc").PageBy(input).MapTo(eccpBaseRegisterCompanies);

            return new PagedResultDto<GetECCPBaseRegisterCompanyForView>(totalCount, eccpBaseRegisterCompanies);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies)]
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
        /// The get eccp base register companies to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetECCPBaseRegisterCompaniesToExcel(
            GetAllECCPBaseRegisterCompaniesForExcelInput input)
        {
            var filteredEccpBaseRegisterCompanies = this._eccpBaseRegisterCompanyRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.Contains(input.Filter) || e.Addresse.Contains(input.Filter)
                         || e.Telephone.Contains(input.Filter) || e.Summary.Contains(input.Filter))
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.NameFilter),
                    e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.AddresseFilter),
                    e => e.Addresse.ToLower() == input.AddresseFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.TelephoneFilter),
                    e => e.Telephone.ToLower() == input.TelephoneFilter.ToLower().Trim());

            var query =
                (from o in filteredEccpBaseRegisterCompanies
                 join o1 in this._eccpBaseAreaRepository.GetAll() on o.ProvinceId equals o1.Id into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpBaseAreaRepository.GetAll() on o.CityId equals o2.Id into j2
                 from s2 in j2.DefaultIfEmpty()
                 join o3 in this._eccpBaseAreaRepository.GetAll() on o.DistrictId equals o3.Id into j3
                 from s3 in j3.DefaultIfEmpty()
                 join o4 in this._eccpBaseAreaRepository.GetAll() on o.StreetId equals o4.Id into j4
                 from s4 in j4.DefaultIfEmpty()
                 select new GetECCPBaseRegisterCompanyForView
                            {
                                ECCPBaseRegisterCompany = this.ObjectMapper.Map<ECCPBaseRegisterCompanyDto>(o),
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

            var eccpBaseRegisterCompanyListDtos = await query.ToListAsync();

            return this._eccpBaseRegisterCompaniesExcelExporter.ExportToFile(eccpBaseRegisterCompanyListDtos);
        }

        /// <summary>
        /// The get eccp base register company for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies_Edit)]
        public async Task<GetECCPBaseRegisterCompanyForEditOutput> GetECCPBaseRegisterCompanyForEdit(
            EntityDto<long> input)
        {
            var eccpBaseRegisterCompany = await this._eccpBaseRegisterCompanyRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetECCPBaseRegisterCompanyForEditOutput
                             {
                                 ECCPBaseRegisterCompany =
                                     this.ObjectMapper.Map<CreateOrEditECCPBaseRegisterCompanyDto>(
                                         eccpBaseRegisterCompany)
                             };

            if (output.ECCPBaseRegisterCompany.ProvinceId != null)
            {
                var province =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                        (int)output.ECCPBaseRegisterCompany.ProvinceId);
                output.ProvinceName = province.Name;
            }

            if (output.ECCPBaseRegisterCompany.CityId != null)
            {
                var city =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.ECCPBaseRegisterCompany.CityId);
                output.CityName = city.Name;
            }

            if (output.ECCPBaseRegisterCompany.DistrictId != null)
            {
                var district =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                        (int)output.ECCPBaseRegisterCompany.DistrictId);
                output.DistrictName = district.Name;
            }

            if (output.ECCPBaseRegisterCompany.StreetId != null)
            {
                var street =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                        (int)output.ECCPBaseRegisterCompany.StreetId);
                output.StreetName = street.Name;
            }

            return output;
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies_Create)]
        private async Task Create(CreateOrEditECCPBaseRegisterCompanyDto input)
        {
            var eccpBaseRegisterCompany = this.ObjectMapper.Map<ECCPBaseRegisterCompany>(input);

            await this._eccpBaseRegisterCompanyRepository.InsertAsync(eccpBaseRegisterCompany);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies_Edit)]
        private async Task Update(CreateOrEditECCPBaseRegisterCompanyDto input)
        {
            if (input.Id != null)
            {
                var eccpBaseRegisterCompany =
                    await this._eccpBaseRegisterCompanyRepository.FirstOrDefaultAsync((long)input.Id);
                this.ObjectMapper.Map(input, eccpBaseRegisterCompany);
            }
        }
    }
}