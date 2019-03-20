// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseAnnualInspectionUnitsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits
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
    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos;
    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Exporting;
    using Sinodom.ElevatorCloud.ECCPBaseAreas;

    /// <summary>
    /// The eccp base annual inspection units app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits)]
    public class ECCPBaseAnnualInspectionUnitsAppService : ElevatorCloudAppServiceBase,
                                                           IECCPBaseAnnualInspectionUnitsAppService
    {
        /// <summary>
        /// The eccp base annual inspection unit repository.
        /// </summary>
        private readonly IRepository<ECCPBaseAnnualInspectionUnit, long> _eccpBaseAnnualInspectionUnitRepository;

        /// <summary>
        /// The eccp base annual inspection units excel exporter.
        /// </summary>
        private readonly IECCPBaseAnnualInspectionUnitsExcelExporter _eccpBaseAnnualInspectionUnitsExcelExporter;

        /// <summary>
        /// The eccp base area repository.
        /// </summary>
        private readonly IRepository<ECCPBaseArea, int> _eccpBaseAreaRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseAnnualInspectionUnitsAppService"/> class.
        /// </summary>
        /// <param name="eccpBaseAnnualInspectionUnitRepository">
        /// The e ccp base annual inspection unit repository.
        /// </param>
        /// <param name="eccpBaseAnnualInspectionUnitsExcelExporter">
        /// The e ccp base annual inspection units excel exporter.
        /// </param>
        /// <param name="eccpBaseAreaRepository">
        /// The e ccp base area repository.
        /// </param>
        public ECCPBaseAnnualInspectionUnitsAppService(
            IRepository<ECCPBaseAnnualInspectionUnit, long> eccpBaseAnnualInspectionUnitRepository,
            IECCPBaseAnnualInspectionUnitsExcelExporter eccpBaseAnnualInspectionUnitsExcelExporter,
            IRepository<ECCPBaseArea, int> eccpBaseAreaRepository)
        {
            this._eccpBaseAnnualInspectionUnitRepository = eccpBaseAnnualInspectionUnitRepository;
            this._eccpBaseAnnualInspectionUnitsExcelExporter = eccpBaseAnnualInspectionUnitsExcelExporter;
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
        public async Task CreateOrEdit(CreateOrEditECCPBaseAnnualInspectionUnitDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await this._eccpBaseAnnualInspectionUnitRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetECCPBaseAnnualInspectionUnitForView>> GetAll(
            GetAllECCPBaseAnnualInspectionUnitsInput input)
        {
            var filteredEccpBaseAnnualInspectionUnits = this._eccpBaseAnnualInspectionUnitRepository.GetAll()
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
                (from o in filteredEccpBaseAnnualInspectionUnits
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
                                EccpBaseAnnualInspectionUnit = o,
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

            List<GetECCPBaseAnnualInspectionUnitForView> eccpBaseAnnualInspectionUnits = new List<GetECCPBaseAnnualInspectionUnitForView>();

            query.OrderBy(input.Sorting ?? "ECCPBaseAnnualInspectionUnit.Id asc").PageBy(input).MapTo(eccpBaseAnnualInspectionUnits);

            return new PagedResultDto<GetECCPBaseAnnualInspectionUnitForView>(
                totalCount,
                eccpBaseAnnualInspectionUnits);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits)]
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
        /// The get eccp base annual inspection unit for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits_Edit)]
        public async Task<GetECCPBaseAnnualInspectionUnitForEditOutput> GetECCPBaseAnnualInspectionUnitForEdit(
            EntityDto<long> input)
        {
            var eccpBaseAnnualInspectionUnit =
                await this._eccpBaseAnnualInspectionUnitRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetECCPBaseAnnualInspectionUnitForEditOutput
                             {
                                 ECCPBaseAnnualInspectionUnit =
                                     this.ObjectMapper.Map<CreateOrEditECCPBaseAnnualInspectionUnitDto>(
                                         eccpBaseAnnualInspectionUnit)
                             };

            if (output.ECCPBaseAnnualInspectionUnit.ProvinceId != null)
            {
                var province =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                        (int)output.ECCPBaseAnnualInspectionUnit.ProvinceId);
                output.ProvinceName = province.Name;
            }

            if (output.ECCPBaseAnnualInspectionUnit.CityId != null)
            {
                var city =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                        (int)output.ECCPBaseAnnualInspectionUnit.CityId);
                output.CityName = city.Name;
            }

            if (output.ECCPBaseAnnualInspectionUnit.DistrictId != null)
            {
                var district =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                        (int)output.ECCPBaseAnnualInspectionUnit.DistrictId);
                output.DistrictName = district.Name;
            }

            if (output.ECCPBaseAnnualInspectionUnit.StreetId != null)
            {
                var street =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync(
                        (int)output.ECCPBaseAnnualInspectionUnit.StreetId);
                output.StreetName = street.Name;
            }

            return output;
        }

        /// <summary>
        /// The get eccp base annual inspection units to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetECCPBaseAnnualInspectionUnitsToExcel(
            GetAllECCPBaseAnnualInspectionUnitsForExcelInput input)
        {
            var filteredEccpBaseAnnualInspectionUnits = this._eccpBaseAnnualInspectionUnitRepository.GetAll()
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
                (from o in filteredEccpBaseAnnualInspectionUnits
                 join o1 in this._eccpBaseAreaRepository.GetAll() on o.ProvinceId equals o1.Id into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpBaseAreaRepository.GetAll() on o.CityId equals o2.Id into j2
                 from s2 in j2.DefaultIfEmpty()
                 join o3 in this._eccpBaseAreaRepository.GetAll() on o.DistrictId equals o3.Id into j3
                 from s3 in j3.DefaultIfEmpty()
                 join o4 in this._eccpBaseAreaRepository.GetAll() on o.StreetId equals o4.Id into j4
                 from s4 in j4.DefaultIfEmpty()
                 select new GetECCPBaseAnnualInspectionUnitForView
                            {
                                ECCPBaseAnnualInspectionUnit =
                                    this.ObjectMapper.Map<ECCPBaseAnnualInspectionUnitDto>(o),
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

            var eccpBaseAnnualInspectionUnitListDtos = await query.ToListAsync();

            return this._eccpBaseAnnualInspectionUnitsExcelExporter.ExportToFile(eccpBaseAnnualInspectionUnitListDtos);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits_Create)]
        private async Task Create(CreateOrEditECCPBaseAnnualInspectionUnitDto input)
        {
            var eccpBaseAnnualInspectionUnit = this.ObjectMapper.Map<ECCPBaseAnnualInspectionUnit>(input);

            await this._eccpBaseAnnualInspectionUnitRepository.InsertAsync(eccpBaseAnnualInspectionUnit);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits_Edit)]
        private async Task Update(CreateOrEditECCPBaseAnnualInspectionUnitDto input)
        {
            if (input.Id != null)
            {
                var eccpBaseAnnualInspectionUnit =
                    await this._eccpBaseAnnualInspectionUnitRepository.FirstOrDefaultAsync((long)input.Id);
                this.ObjectMapper.Map(input, eccpBaseAnnualInspectionUnit);
            }
        }
    }
}