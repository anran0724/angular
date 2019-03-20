// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorModelsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorModels
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
    using Sinodom.ElevatorCloud.EccpBaseElevatorBrands;
    using Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos;
    using Sinodom.ElevatorCloud.EccpBaseElevatorModels.Exporting;

    /// <summary>
    ///     The eccp base elevator models app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorModels)]
    public class EccpBaseElevatorModelsAppService : ElevatorCloudAppServiceBase, IEccpBaseElevatorModelsAppService
    {
        /// <summary>
        ///     The eccp base elevator brand repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevatorBrand, int> _eccpBaseElevatorBrandRepository;

        /// <summary>
        ///     The eccp base elevator model repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevatorModel> _eccpBaseElevatorModelRepository;

        /// <summary>
        ///     The eccp base elevator models excel exporter.
        /// </summary>
        private readonly IEccpBaseElevatorModelsExcelExporter _eccpBaseElevatorModelsExcelExporter;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorModelsAppService"/> class.
        /// </summary>
        /// <param name="eccpBaseElevatorModelRepository">
        /// The eccp base elevator model repository.
        /// </param>
        /// <param name="eccpBaseElevatorModelsExcelExporter">
        /// The eccp base elevator models excel exporter.
        /// </param>
        /// <param name="eccpBaseElevatorBrandRepository">
        /// The eccp base elevator brand repository.
        /// </param>
        public EccpBaseElevatorModelsAppService(
            IRepository<EccpBaseElevatorModel> eccpBaseElevatorModelRepository,
            IEccpBaseElevatorModelsExcelExporter eccpBaseElevatorModelsExcelExporter,
            IRepository<EccpBaseElevatorBrand, int> eccpBaseElevatorBrandRepository)
        {
            this._eccpBaseElevatorModelRepository = eccpBaseElevatorModelRepository;
            this._eccpBaseElevatorModelsExcelExporter = eccpBaseElevatorModelsExcelExporter;
            this._eccpBaseElevatorBrandRepository = eccpBaseElevatorBrandRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpBaseElevatorModelDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorModels_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpBaseElevatorModelRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpBaseElevatorModelForView>> GetAll(
            GetAllEccpBaseElevatorModelsInput input)
        {
            var filteredEccpBaseElevatorModels = this._eccpBaseElevatorModelRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter));

            var query = (from o in filteredEccpBaseElevatorModels
                         join o1 in this._eccpBaseElevatorBrandRepository.GetAll() on o.ElevatorBrandId equals o1.Id into
                             j1
                         from s1 in j1.DefaultIfEmpty()
                         select new 
                                    {
                                        EccpBaseElevatorModel = o,
                                        EccpBaseElevatorBrandName = s1 == null ? string.Empty : s1.Name
                                    }).WhereIf(
                !string.IsNullOrWhiteSpace(input.EccpBaseElevatorBrandNameFilter),
                e => e.EccpBaseElevatorBrandName.ToLower() == input.EccpBaseElevatorBrandNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var eccpBaseElevatorModels = new List<GetEccpBaseElevatorModelForView>();

            query.OrderBy(input.Sorting ?? "eccpBaseElevatorModel.id asc").PageBy(input).MapTo(eccpBaseElevatorModels);

            return new PagedResultDto<GetEccpBaseElevatorModelForView>(totalCount, eccpBaseElevatorModels);
        }

        /// <summary>
        /// The get all eccp base elevator brand for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorModels)]
        public async Task<PagedResultDto<EccpBaseElevatorBrandLookupTableDto>> GetAllEccpBaseElevatorBrandForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseElevatorBrandRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpBaseElevatorBrandList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpBaseElevatorBrandLookupTableDto>();
            foreach (var eccpBaseElevatorBrand in eccpBaseElevatorBrandList)
            {
                lookupTableDtoList.Add(
                    new EccpBaseElevatorBrandLookupTableDto
                        {
                            Id = eccpBaseElevatorBrand.Id, DisplayName = eccpBaseElevatorBrand.Name
                        });
            }

            return new PagedResultDto<EccpBaseElevatorBrandLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get eccp base elevator model for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorModels_Edit)]
        public async Task<GetEccpBaseElevatorModelForEditOutput> GetEccpBaseElevatorModelForEdit(EntityDto input)
        {
            var eccpBaseElevatorModel = await this._eccpBaseElevatorModelRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpBaseElevatorModelForEditOutput
                             {
                                 EccpBaseElevatorModel =
                                     this.ObjectMapper.Map<CreateOrEditEccpBaseElevatorModelDto>(eccpBaseElevatorModel)
                             };
            var eccpBaseElevatorBrand =
                await this._eccpBaseElevatorBrandRepository.FirstOrDefaultAsync(
                    output.EccpBaseElevatorModel.ElevatorBrandId);
            output.EccpBaseElevatorBrandName = eccpBaseElevatorBrand.Name;

            return output;
        }

        /// <summary>
        /// The get eccp base elevator models to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetEccpBaseElevatorModelsToExcel(GetAllEccpBaseElevatorModelsForExcelInput input)
        {
            var filteredEccpBaseElevatorModels = this._eccpBaseElevatorModelRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter));

            var query = (from o in filteredEccpBaseElevatorModels
                         join o1 in this._eccpBaseElevatorBrandRepository.GetAll() on o.ElevatorBrandId equals o1.Id into
                             j1
                         from s1 in j1.DefaultIfEmpty()
                         select new GetEccpBaseElevatorModelForView
                                    {
                                        EccpBaseElevatorModel = this.ObjectMapper.Map<EccpBaseElevatorModelDto>(o),
                                        EccpBaseElevatorBrandName = s1 == null ? string.Empty : s1.Name
                                    }).WhereIf(
                !string.IsNullOrWhiteSpace(input.EccpBaseElevatorBrandNameFilter),
                e => e.EccpBaseElevatorBrandName.ToLower() == input.EccpBaseElevatorBrandNameFilter.ToLower().Trim());

            var eccpBaseElevatorModelListDtos = await query.ToListAsync();

            return this._eccpBaseElevatorModelsExcelExporter.ExportToFile(eccpBaseElevatorModelListDtos);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorModels_Create)]
        private async Task Create(CreateOrEditEccpBaseElevatorModelDto input)
        {
            var eccpBaseElevatorModel = this.ObjectMapper.Map<EccpBaseElevatorModel>(input);

            await this._eccpBaseElevatorModelRepository.InsertAsync(eccpBaseElevatorModel);
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
        [AbpAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorModels_Edit)]
        private async Task Update(CreateOrEditEccpBaseElevatorModelDto input)
        {
            if (input.Id != null)
            {
                var eccpBaseElevatorModel =
                    await this._eccpBaseElevatorModelRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpBaseElevatorModel);
            }
        }
    }
}