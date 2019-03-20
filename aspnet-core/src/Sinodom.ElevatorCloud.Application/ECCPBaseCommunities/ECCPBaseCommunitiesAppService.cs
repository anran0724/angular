// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseCommunitiesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseCommunities
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
    using Sinodom.ElevatorCloud.ECCPBaseCommunities.Dtos;
    using Sinodom.ElevatorCloud.ECCPBaseCommunities.Exporting;

    /// <summary>
    ///     The eccp base communities app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseCommunities)]
    public class ECCPBaseCommunitiesAppService : ElevatorCloudAppServiceBase, IECCPBaseCommunitiesAppService
    {
        /// <summary>
        ///     The eccp base area repository.
        /// </summary>
        private readonly IRepository<ECCPBaseArea, int> _eccpBaseAreaRepository;

        /// <summary>
        ///     The eccp base communities excel exporter.
        /// </summary>
        private readonly IECCPBaseCommunitiesExcelExporter _eccpBaseCommunitiesExcelExporter;

        /// <summary>
        ///     The eccp base community repository.
        /// </summary>
        private readonly IRepository<ECCPBaseCommunity, long> _eccpBaseCommunityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseCommunitiesAppService"/> class.
        /// </summary>
        /// <param name="eccpBaseCommunityRepository">
        /// The eccp base community repository.
        /// </param>
        /// <param name="eccpBaseCommunitiesExcelExporter">
        /// The eccp base communities excel exporter.
        /// </param>
        /// <param name="eccpBaseAreaRepository">
        /// The eccp base area repository.
        /// </param>
        public ECCPBaseCommunitiesAppService(
            IRepository<ECCPBaseCommunity, long> eccpBaseCommunityRepository,
            IECCPBaseCommunitiesExcelExporter eccpBaseCommunitiesExcelExporter,
            IRepository<ECCPBaseArea, int> eccpBaseAreaRepository)
        {
            this._eccpBaseCommunityRepository = eccpBaseCommunityRepository;
            this._eccpBaseCommunitiesExcelExporter = eccpBaseCommunitiesExcelExporter;
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
        public async Task CreateOrEdit(CreateOrEditECCPBaseCommunityDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseCommunities_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await this._eccpBaseCommunityRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetECCPBaseCommunityForView>> GetAll(GetAllECCPBaseCommunitiesInput input)
        {
            var filteredEccpBaseCommunities = this._eccpBaseCommunityRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.Contains(input.Filter) || e.Address.Contains(input.Filter)
                         || e.Longitude.Contains(input.Filter) || e.Latitude.Contains(input.Filter))
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.NameFilter),
                    e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.AddressFilter),
                    e => e.Address.ToLower() == input.AddressFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.LongitudeFilter),
                    e => e.Longitude.ToLower() == input.LongitudeFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.LatitudeFilter),
                    e => e.Latitude.ToLower() == input.LatitudeFilter.ToLower().Trim());

            var query =
                (from o in filteredEccpBaseCommunities
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
                                ECCPBaseCommunity = o,
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

            var eccpBaseCommunities = new List<GetECCPBaseCommunityForView>();

            query.OrderBy(input.Sorting ?? "eCCPBaseCommunity.id asc").PageBy(input).MapTo(eccpBaseCommunities);

            return new PagedResultDto<GetECCPBaseCommunityForView>(totalCount, eccpBaseCommunities);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseCommunities)]
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
        /// The get eccp base communities to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetECCPBaseCommunitiesToExcel(GetAllECCPBaseCommunitiesForExcelInput input)
        {
            var filteredEccpBaseCommunities = this._eccpBaseCommunityRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.Contains(input.Filter) || e.Address.Contains(input.Filter)
                                                       || e.Longitude.Contains(input.Filter)
                                                       || e.Latitude.Contains(input.Filter))
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.NameFilter),
                    e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.AddressFilter),
                    e => e.Address.ToLower() == input.AddressFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.LongitudeFilter),
                    e => e.Longitude.ToLower() == input.LongitudeFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.LatitudeFilter),
                    e => e.Latitude.ToLower() == input.LatitudeFilter.ToLower().Trim());

            var query =
                (from o in filteredEccpBaseCommunities
                 join o1 in this._eccpBaseAreaRepository.GetAll() on o.ProvinceId equals o1.Id into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpBaseAreaRepository.GetAll() on o.CityId equals o2.Id into j2
                 from s2 in j2.DefaultIfEmpty()
                 join o3 in this._eccpBaseAreaRepository.GetAll() on o.DistrictId equals o3.Id into j3
                 from s3 in j3.DefaultIfEmpty()
                 join o4 in this._eccpBaseAreaRepository.GetAll() on o.StreetId equals o4.Id into j4
                 from s4 in j4.DefaultIfEmpty()
                 select new GetECCPBaseCommunityForView
                            {
                                ECCPBaseCommunity = this.ObjectMapper.Map<ECCPBaseCommunityDto>(o),
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

            var eccpBaseCommunityListDtos = await query.ToListAsync();

            return this._eccpBaseCommunitiesExcelExporter.ExportToFile(eccpBaseCommunityListDtos);
        }

        /// <summary>
        /// The get eccp base community for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseCommunities_Edit)]
        public async Task<GetECCPBaseCommunityForEditOutput> GetECCPBaseCommunityForEdit(EntityDto<long> input)
        {
            var eccpBaseCommunity = await this._eccpBaseCommunityRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetECCPBaseCommunityForEditOutput
                             {
                                 ECCPBaseCommunity =
                                     this.ObjectMapper.Map<CreateOrEditECCPBaseCommunityDto>(eccpBaseCommunity)
                             };

            if (output.ECCPBaseCommunity.ProvinceId != null)
            {
                var province =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.ECCPBaseCommunity.ProvinceId);
                output.ProvinceName = province.Name;
            }

            if (output.ECCPBaseCommunity.CityId != null)
            {
                var city = await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.ECCPBaseCommunity.CityId);
                output.CityName = city.Name;
            }

            if (output.ECCPBaseCommunity.DistrictId != null)
            {
                var district =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.ECCPBaseCommunity.DistrictId);
                output.DistrictName = district.Name;
            }

            if (output.ECCPBaseCommunity.StreetId != null)
            {
                var street =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.ECCPBaseCommunity.StreetId);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseCommunities_Create)]
        private async Task Create(CreateOrEditECCPBaseCommunityDto input)
        {
            var eccpBaseCommunity = this.ObjectMapper.Map<ECCPBaseCommunity>(input);

            await this._eccpBaseCommunityRepository.InsertAsync(eccpBaseCommunity);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseCommunities_Edit)]
        private async Task Update(CreateOrEditECCPBaseCommunityDto input)
        {
            if (input.Id != null)
            {
                var eccpBaseCommunity = await this._eccpBaseCommunityRepository.FirstOrDefaultAsync((long)input.Id);
                this.ObjectMapper.Map(input, eccpBaseCommunity);
            }
        }
    }
}