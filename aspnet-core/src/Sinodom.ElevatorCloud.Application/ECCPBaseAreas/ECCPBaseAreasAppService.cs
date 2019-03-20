// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseAreasAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAreas
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
    using Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos;
    using Sinodom.ElevatorCloud.ECCPBaseAreas.Exporting;

    using GetAllForLookupTableInput =
        Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos.GetAllForLookupTableInput;

    /// <summary>
    ///     The eccp base areas app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAreas)]
    public class ECCPBaseAreasAppService : ElevatorCloudAppServiceBase, IECCPBaseAreasAppService
    {
        /// <summary>
        ///     The eccp base area repository.
        /// </summary>
        private readonly IRepository<ECCPBaseArea> _eccpBaseAreaRepository;

        /// <summary>
        ///     The eccp base areas excel exporter.
        /// </summary>
        private readonly IECCPBaseAreasExcelExporter _eccpBaseAreasExcelExporter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseAreasAppService"/> class.
        /// </summary>
        /// <param name="eccpBaseAreaRepository">
        /// The eccp base area repository.
        /// </param>
        /// <param name="eccpBaseAreasExcelExporter">
        /// The eccp base areas excel exporter.
        /// </param>
        public ECCPBaseAreasAppService(
            IRepository<ECCPBaseArea> eccpBaseAreaRepository,
            IECCPBaseAreasExcelExporter eccpBaseAreasExcelExporter)
        {
            this._eccpBaseAreaRepository = eccpBaseAreaRepository;
            this._eccpBaseAreasExcelExporter = eccpBaseAreasExcelExporter;
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
        public async Task CreateOrEdit(CreateOrEditECCPBaseAreaDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAreas_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpBaseAreaRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetECCPBaseAreaForView>> GetAll(GetAllECCPBaseAreasInput input)
        {
            var filteredEccpBaseAreas = this._eccpBaseAreaRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter)
                                                       || e.Path.Contains(input.Filter))
                .WhereIf(input.MinParentIdFilter != null, e => e.ParentId >= input.MinParentIdFilter)
                .WhereIf(input.MaxParentIdFilter != null, e => e.ParentId <= input.MaxParentIdFilter)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.CodeFilter),
                    e => e.Code.ToLower() == input.CodeFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.NameFilter),
                    e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                .WhereIf(input.MinLevelFilter != null, e => e.Level >= input.MinLevelFilter).WhereIf(
                    input.MaxLevelFilter != null,
                    e => e.Level <= input.MaxLevelFilter);

            var query = from o in filteredEccpBaseAreas select new { ECCPBaseArea = o };

            var totalCount = await query.CountAsync();

            List<GetECCPBaseAreaForView> eccpBaseAreas = new List<GetECCPBaseAreaForView>();
            query.OrderBy(input.Sorting ?? "eCCPBaseArea.id asc").PageBy(input).MapTo(eccpBaseAreas);

            return new PagedResultDto<GetECCPBaseAreaForView>(totalCount, eccpBaseAreas);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAreas)]
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
        /// The get eccp base area for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAreas_Edit)]
        public async Task<GetECCPBaseAreaForEditOutput> GetECCPBaseAreaForEdit(EntityDto input)
        {
            var eccpBaseArea = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetECCPBaseAreaForEditOutput
                             {
                                 ECCPBaseArea = this.ObjectMapper.Map<CreateOrEditECCPBaseAreaDto>(eccpBaseArea)
                             };

            if (eccpBaseArea.Level == 1)
            {
                // 市
                var province = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(eccpBaseArea.ParentId);
                output.ProvinceName = province.Name;
                output.ProvinceId = province.Id;
            }
            else if (eccpBaseArea.Level == 2)
            {
                // 区
                var city = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(eccpBaseArea.ParentId);
                output.CityName = city.Name;
                output.CityId = city.Id;

                var province = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(city.ParentId);
                output.ProvinceName = province.Name;
                output.ProvinceId = province.Id;
            }
            else if (eccpBaseArea.Level == 3)
            {
                // 街道
                var district = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(eccpBaseArea.ParentId);
                output.DistrictName = district.Name;
                output.DistrictId = district.Id;

                var city = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(district.ParentId);
                output.CityName = city.Name;
                output.CityId = city.Id;

                var province = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(city.ParentId);
                output.ProvinceName = province.Name;
                output.ProvinceId = province.Id;
            }

            return output;
        }

        /// <summary>
        /// The get eccp base areas to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetECCPBaseAreasToExcel(GetAllECCPBaseAreasForExcelInput input)
        {
            var filteredEccpBaseAreas = this._eccpBaseAreaRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter)
                                                       || e.Path.Contains(input.Filter))
                .WhereIf(input.MinParentIdFilter != null, e => e.ParentId >= input.MinParentIdFilter)
                .WhereIf(input.MaxParentIdFilter != null, e => e.ParentId <= input.MaxParentIdFilter)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.CodeFilter),
                    e => e.Code.ToLower() == input.CodeFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.NameFilter),
                    e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                .WhereIf(input.MinLevelFilter != null, e => e.Level >= input.MinLevelFilter).WhereIf(
                    input.MaxLevelFilter != null,
                    e => e.Level <= input.MaxLevelFilter);

            var query = from o in filteredEccpBaseAreas
                        select new GetECCPBaseAreaForView { ECCPBaseArea = this.ObjectMapper.Map<ECCPBaseAreaDto>(o) };

            var eccpBaseAreaListDtos = await query.ToListAsync();

            return this._eccpBaseAreasExcelExporter.ExportToFile(eccpBaseAreaListDtos);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAreas_Create)]
        private async Task Create(CreateOrEditECCPBaseAreaDto input)
        {
            var eccpBaseArea = this.ObjectMapper.Map<ECCPBaseArea>(input);

            eccpBaseArea.Path = string.Empty;

            eccpBaseArea.Id = await this._eccpBaseAreaRepository.InsertAndGetIdAsync(eccpBaseArea);

            if (eccpBaseArea.Level == 0)
            {
                // 省1,
                eccpBaseArea.Path = eccpBaseArea.Id + ",";
            }
            else if (eccpBaseArea.Level == 1)
            {
                // 市1,2,
                eccpBaseArea.Path = eccpBaseArea.ParentId + "," + eccpBaseArea.Id + ",";
            }
            else if (eccpBaseArea.Level == 2)
            {
                // 区1,2,3,
                var city = await this._eccpBaseAreaRepository.GetAsync(eccpBaseArea.ParentId);
                eccpBaseArea.Path = city.ParentId + "," + eccpBaseArea.ParentId + "," + eccpBaseArea.Id + ",";
            }
            else if (eccpBaseArea.Level == 3)
            {
                // 街道1,2,3,4,
                var district = await this._eccpBaseAreaRepository.GetAsync(eccpBaseArea.ParentId);
                var city = await this._eccpBaseAreaRepository.GetAsync(district.ParentId);
                eccpBaseArea.Path = city.ParentId + "," + district.ParentId + "," + eccpBaseArea.ParentId + ","
                                    + eccpBaseArea.Id + ",";
            }

            await this._eccpBaseAreaRepository.UpdateAsync(eccpBaseArea);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAreas_Edit)]
        private async Task Update(CreateOrEditECCPBaseAreaDto input)
        {
            if (input.Id != null)
            {
                var eccpBaseArea = await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)input.Id);
                if (input.Level == 0)
                {
                    // 省1,
                    input.Path = input.Id + ",";
                }
                else if (input.Level == 1)
                {
                    // 市1,2,
                    input.Path = input.ParentId + "," + input.Id + ",";
                }
                else if (input.Level == 2)
                {
                    // 区1,2,3,
                    var city = await this._eccpBaseAreaRepository.GetAsync(input.ParentId);
                    input.Path = city.ParentId + "," + input.ParentId + "," + input.Id + ",";
                }
                else if (input.Level == 3)
                {
                    // 街道1,2,3,4,
                    var district = await this._eccpBaseAreaRepository.GetAsync(input.ParentId);
                    var city = await this._eccpBaseAreaRepository.GetAsync(district.ParentId);
                    input.Path = city.ParentId + "," + district.ParentId + "," + input.ParentId + "," + input.Id + ",";
                }

                this.ObjectMapper.Map(input, eccpBaseArea);
            }
        }
    }
}