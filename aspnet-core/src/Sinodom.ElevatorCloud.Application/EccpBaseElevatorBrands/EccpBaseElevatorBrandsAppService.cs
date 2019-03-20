// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorBrandsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorBrands
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
    using Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos;
    using Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Exporting;
    using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies;

    /// <summary>
    ///     The eccp base elevator brands app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands)]
    public class EccpBaseElevatorBrandsAppService : ElevatorCloudAppServiceBase, IEccpBaseElevatorBrandsAppService
    {
        /// <summary>
        ///     The eccp base elevator brand repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevatorBrand> _eccpBaseElevatorBrandRepository;

        /// <summary>
        ///     The eccp base elevator brands excel exporter.
        /// </summary>
        private readonly IEccpBaseElevatorBrandsExcelExporter _eccpBaseElevatorBrandsExcelExporter;

        /// <summary>
        ///     The eccp base production company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseProductionCompany, long> _eccpBaseProductionCompanyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorBrandsAppService"/> class.
        /// </summary>
        /// <param name="eccpBaseElevatorBrandRepository">
        /// The eccp base elevator brand repository.
        /// </param>
        /// <param name="eccpBaseElevatorBrandsExcelExporter">
        /// The eccp base elevator brands excel exporter.
        /// </param>
        /// <param name="eccpBaseProductionCompanyRepository">
        /// The eccp base production company repository.
        /// </param>
        public EccpBaseElevatorBrandsAppService(
            IRepository<EccpBaseElevatorBrand> eccpBaseElevatorBrandRepository,
            IEccpBaseElevatorBrandsExcelExporter eccpBaseElevatorBrandsExcelExporter,
            IRepository<ECCPBaseProductionCompany, long> eccpBaseProductionCompanyRepository)
        {
            this._eccpBaseElevatorBrandRepository = eccpBaseElevatorBrandRepository;
            this._eccpBaseElevatorBrandsExcelExporter = eccpBaseElevatorBrandsExcelExporter;
            this._eccpBaseProductionCompanyRepository = eccpBaseProductionCompanyRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpBaseElevatorBrandDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpBaseElevatorBrandRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpBaseElevatorBrandForView>> GetAll(
            GetAllEccpBaseElevatorBrandsInput input)
        {
            var filteredEccpBaseElevatorBrands = this._eccpBaseElevatorBrandRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter));

            var query = (from o in filteredEccpBaseElevatorBrands
                         join o1 in this._eccpBaseProductionCompanyRepository.GetAll() on o.ProductionCompanyId equals
                             o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         select new 
                                    {
                                        EccpBaseElevatorBrand = o,
                                        ECCPBaseProductionCompanyName = s1 == null ? string.Empty : s1.Name
                                    }).WhereIf(
                !string.IsNullOrWhiteSpace(input.ECCPBaseProductionCompanyNameFilter),
                e => e.ECCPBaseProductionCompanyName.ToLower()
                     == input.ECCPBaseProductionCompanyNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var eccpBaseElevatorBrands = new List<GetEccpBaseElevatorBrandForView>();

            query.OrderBy(input.Sorting ?? "eccpBaseElevatorBrand.id asc").PageBy(input).MapTo(eccpBaseElevatorBrands);

            return new PagedResultDto<GetEccpBaseElevatorBrandForView>(totalCount, eccpBaseElevatorBrands);
        }

        /// <summary>
        /// The get all eccp base production company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands)]
        public async Task<PagedResultDto<ECCPBaseProductionCompanyLookupTableDto>> GetAllECCPBaseProductionCompanyForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseProductionCompanyRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpBaseProductionCompanyList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<ECCPBaseProductionCompanyLookupTableDto>();
            foreach (var eccpBaseProductionCompany in eccpBaseProductionCompanyList)
            {
                lookupTableDtoList.Add(
                    new ECCPBaseProductionCompanyLookupTableDto
                        {
                            Id = eccpBaseProductionCompany.Id, DisplayName = eccpBaseProductionCompany.Name
                        });
            }

            return new PagedResultDto<ECCPBaseProductionCompanyLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get eccp base elevator brand for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands_Edit)]
        public async Task<GetEccpBaseElevatorBrandForEditOutput> GetEccpBaseElevatorBrandForEdit(EntityDto input)
        {
            var eccpBaseElevatorBrand = await this._eccpBaseElevatorBrandRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpBaseElevatorBrandForEditOutput
                             {
                                 EccpBaseElevatorBrand =
                                     this.ObjectMapper.Map<CreateOrEditEccpBaseElevatorBrandDto>(eccpBaseElevatorBrand)
                             };
            var eccpBaseProductionCompany =
                await this._eccpBaseProductionCompanyRepository.FirstOrDefaultAsync(
                    output.EccpBaseElevatorBrand.ProductionCompanyId);
            output.ECCPBaseProductionCompanyName = eccpBaseProductionCompany.Name;

            return output;
        }

        /// <summary>
        /// The get eccp base elevator brands to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetEccpBaseElevatorBrandsToExcel(GetAllEccpBaseElevatorBrandsForExcelInput input)
        {
            var filteredEccpBaseElevatorBrands = this._eccpBaseElevatorBrandRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter));

            var query = (from o in filteredEccpBaseElevatorBrands
                         join o1 in this._eccpBaseProductionCompanyRepository.GetAll() on o.ProductionCompanyId equals
                             o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         select new GetEccpBaseElevatorBrandForView
                                    {
                                        EccpBaseElevatorBrand = this.ObjectMapper.Map<EccpBaseElevatorBrandDto>(o),
                                        ECCPBaseProductionCompanyName = s1 == null ? string.Empty : s1.Name
                                    }).WhereIf(
                !string.IsNullOrWhiteSpace(input.ECCPBaseProductionCompanyNameFilter),
                e => e.ECCPBaseProductionCompanyName.ToLower()
                     == input.ECCPBaseProductionCompanyNameFilter.ToLower().Trim());

            var eccpBaseElevatorBrandListDtos = await query.ToListAsync();

            return this._eccpBaseElevatorBrandsExcelExporter.ExportToFile(eccpBaseElevatorBrandListDtos);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands_Create)]
        private async Task Create(CreateOrEditEccpBaseElevatorBrandDto input)
        {
            var eccpBaseElevatorBrand = this.ObjectMapper.Map<EccpBaseElevatorBrand>(input);

            await this._eccpBaseElevatorBrandRepository.InsertAsync(eccpBaseElevatorBrand);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands_Edit)]
        private async Task Update(CreateOrEditEccpBaseElevatorBrandDto input)
        {
            if (input.Id != null)
            {
                var eccpBaseElevatorBrand =
                    await this._eccpBaseElevatorBrandRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpBaseElevatorBrand);
            }
        }
    }
}