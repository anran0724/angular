// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTemplatesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplates
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
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Exporting;

    /// <summary>
    /// The eccp maintenance templates app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates)]
    public class EccpMaintenanceTemplatesAppService : ElevatorCloudAppServiceBase, IEccpMaintenanceTemplatesAppService
    {
        /// <summary>
        /// The _eccp dict elevator type repository.
        /// </summary>
        private readonly IRepository<EccpDictElevatorType, int> _eccpDictElevatorTypeRepository;

        /// <summary>
        /// The _eccp dict maintenance type repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceType, int> _eccpDictMaintenanceTypeRepository;

        /// <summary>
        /// The _eccp maintenance template repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTemplate> _eccpMaintenanceTemplateRepository;

        /// <summary>
        /// The _eccp maintenance templates excel exporter.
        /// </summary>
        private readonly IEccpMaintenanceTemplatesExcelExporter _eccpMaintenanceTemplatesExcelExporter;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTemplatesAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceTemplateRepository">
        /// The eccp maintenance template repository.
        /// </param>
        /// <param name="eccpMaintenanceTemplatesExcelExporter">
        /// The eccp maintenance templates excel exporter.
        /// </param>
        /// <param name="eccpDictMaintenanceTypeRepository">
        /// The eccp dict maintenance type repository.
        /// </param>
        /// <param name="eccpDictElevatorTypeRepository">
        /// The eccp dict elevator type repository.
        /// </param>
        public EccpMaintenanceTemplatesAppService(
            IRepository<EccpMaintenanceTemplate> eccpMaintenanceTemplateRepository,
            IEccpMaintenanceTemplatesExcelExporter eccpMaintenanceTemplatesExcelExporter,
            IRepository<EccpDictMaintenanceType, int> eccpDictMaintenanceTypeRepository,
            IRepository<EccpDictElevatorType, int> eccpDictElevatorTypeRepository)
        {
            this._eccpMaintenanceTemplateRepository = eccpMaintenanceTemplateRepository;
            this._eccpMaintenanceTemplatesExcelExporter = eccpMaintenanceTemplatesExcelExporter;
            this._eccpDictMaintenanceTypeRepository = eccpDictMaintenanceTypeRepository;
            this._eccpDictElevatorTypeRepository = eccpDictElevatorTypeRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpMaintenanceTemplateDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpMaintenanceTemplateRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpMaintenanceTemplateForView>> GetAll(
            GetAllEccpMaintenanceTemplatesInput input)
        {
            var filteredEccpMaintenanceTemplates = this._eccpMaintenanceTemplateRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.TempName.Contains(input.Filter) || e.TempDesc.Contains(input.Filter)
                         || e.TempAllow.Contains(input.Filter) || e.TempDeny.Contains(input.Filter)
                         || e.TempCondition.Contains(input.Filter))
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.TempAllowFilter),
                    e => e.TempAllow.ToLower() == input.TempAllowFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.TempDenyFilter),
                    e => e.TempDeny.ToLower() == input.TempDenyFilter.ToLower().Trim())
                .WhereIf(input.MinTempNodeCountFilter != null, e => e.TempNodeCount >= input.MinTempNodeCountFilter)
                .WhereIf(input.MaxTempNodeCountFilter != null, e => e.TempNodeCount <= input.MaxTempNodeCountFilter);

            var query =
                (from o in filteredEccpMaintenanceTemplates
                 join o1 in this._eccpDictMaintenanceTypeRepository.GetAll() on o.MaintenanceTypeId equals o1.Id into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpDictElevatorTypeRepository.GetAll() on o.ElevatorTypeId equals o2.Id into j2
                 from s2 in j2.DefaultIfEmpty()
                 select new 
                            {
                                EccpMaintenanceTemplate = o,
                                EccpDictMaintenanceTypeName = s1 == null ? string.Empty : s1.Name,
                                EccpDictElevatorTypeName = s2 == null ? string.Empty : s2.Name
                            }).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpDictMaintenanceTypeNameFilter),
                    e => e.EccpDictMaintenanceTypeName.ToLower()
                         == input.EccpDictMaintenanceTypeNameFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpDictElevatorTypeNameFilter),
                    e => e.EccpDictElevatorTypeName.ToLower() == input.EccpDictElevatorTypeNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var eccpMaintenanceTemplates = new List<GetEccpMaintenanceTemplateForView>();
                
            query.OrderBy(input.Sorting ?? "eccpMaintenanceTemplate.id asc").PageBy(input).MapTo(eccpMaintenanceTemplates);

            return new PagedResultDto<GetEccpMaintenanceTemplateForView>(totalCount, eccpMaintenanceTemplates);
        }

        /// <summary>
        /// The get all eccp dict elevator type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates)]
        public async Task<PagedResultDto<EccpDictElevatorTypeLookupTableDto>> GetAllEccpDictElevatorTypeForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpDictElevatorTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpDictElevatorTypeList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpDictElevatorTypeLookupTableDto>();
            foreach (var eccpDictElevatorType in eccpDictElevatorTypeList)
            {
                lookupTableDtoList.Add(
                    new EccpDictElevatorTypeLookupTableDto
                        {
                            Id = eccpDictElevatorType.Id, DisplayName = eccpDictElevatorType.Name
                        });
            }

            return new PagedResultDto<EccpDictElevatorTypeLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp dict maintenance type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates)]
        public async Task<PagedResultDto<EccpDictMaintenanceTypeLookupTableDto>> GetAllEccpDictMaintenanceTypeForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpDictMaintenanceTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpDictMaintenanceTypeList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpDictMaintenanceTypeLookupTableDto>();
            foreach (var eccpDictMaintenanceType in eccpDictMaintenanceTypeList)
            {
                lookupTableDtoList.Add(
                    new EccpDictMaintenanceTypeLookupTableDto
                        {
                            Id = eccpDictMaintenanceType.Id, DisplayName = eccpDictMaintenanceType.Name
                        });
            }

            return new PagedResultDto<EccpDictMaintenanceTypeLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get eccp maintenance template for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates_Edit)]
        public async Task<GetEccpMaintenanceTemplateForEditOutput> GetEccpMaintenanceTemplateForEdit(EntityDto input)
        {
            var eccpMaintenanceTemplate = await this._eccpMaintenanceTemplateRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpMaintenanceTemplateForEditOutput
                             {
                                 EccpMaintenanceTemplate =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenanceTemplateDto>(
                                         eccpMaintenanceTemplate)
                             };

            if (output.EccpMaintenanceTemplate.MaintenanceTypeId != 0)
            {
                var eccpDictMaintenanceType =
                    await this._eccpDictMaintenanceTypeRepository.FirstOrDefaultAsync(
                        output.EccpMaintenanceTemplate.MaintenanceTypeId);
                output.EccpDictMaintenanceTypeName = eccpDictMaintenanceType.Name;
            }

            if (output.EccpMaintenanceTemplate.ElevatorTypeId != 0)
            {
                var eccpDictElevatorType =
                    await this._eccpDictElevatorTypeRepository.FirstOrDefaultAsync(
                        output.EccpMaintenanceTemplate.ElevatorTypeId);
                output.EccpDictElevatorTypeName = eccpDictElevatorType.Name;
            }

            return output;
        }

        /// <summary>
        /// The get eccp maintenance templates to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetEccpMaintenanceTemplatesToExcel(GetAllEccpMaintenanceTemplatesForExcelInput input)
        {
            var filteredEccpMaintenanceTemplates = this._eccpMaintenanceTemplateRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.TempName.Contains(input.Filter) || e.TempDesc.Contains(input.Filter)
                         || e.TempAllow.Contains(input.Filter) || e.TempDeny.Contains(input.Filter)
                         || e.TempCondition.Contains(input.Filter))
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.TempAllowFilter),
                    e => e.TempAllow.ToLower() == input.TempAllowFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.TempDenyFilter),
                    e => e.TempDeny.ToLower() == input.TempDenyFilter.ToLower().Trim())
                .WhereIf(input.MinTempNodeCountFilter != null, e => e.TempNodeCount >= input.MinTempNodeCountFilter)
                .WhereIf(input.MaxTempNodeCountFilter != null, e => e.TempNodeCount <= input.MaxTempNodeCountFilter);

            var query =
                (from o in filteredEccpMaintenanceTemplates
                 join o1 in this._eccpDictMaintenanceTypeRepository.GetAll() on o.MaintenanceTypeId equals o1.Id into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpDictElevatorTypeRepository.GetAll() on o.ElevatorTypeId equals o2.Id into j2
                 from s2 in j2.DefaultIfEmpty()
                 select new GetEccpMaintenanceTemplateForView
                            {
                                EccpMaintenanceTemplate = this.ObjectMapper.Map<EccpMaintenanceTemplateDto>(o),
                                EccpDictMaintenanceTypeName = s1 == null ? string.Empty : s1.Name,
                                EccpDictElevatorTypeName = s2 == null ? string.Empty : s2.Name
                            }).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpDictMaintenanceTypeNameFilter),
                    e => e.EccpDictMaintenanceTypeName.ToLower()
                         == input.EccpDictMaintenanceTypeNameFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpDictElevatorTypeNameFilter),
                    e => e.EccpDictElevatorTypeName.ToLower() == input.EccpDictElevatorTypeNameFilter.ToLower().Trim());

            var eccpMaintenanceTemplateListDtos = await query.ToListAsync();

            return this._eccpMaintenanceTemplatesExcelExporter.ExportToFile(eccpMaintenanceTemplateListDtos);
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates_Create)]
        private async Task Create(CreateOrEditEccpMaintenanceTemplateDto input)
        {
            var eccpMaintenanceTemplate = this.ObjectMapper.Map<EccpMaintenanceTemplate>(input);

            if (this.AbpSession.TenantId != null)
            {
                eccpMaintenanceTemplate.TenantId = this.AbpSession.TenantId;
            }

            eccpMaintenanceTemplate.TempNodeCount = 0;

            await this._eccpMaintenanceTemplateRepository.InsertAsync(eccpMaintenanceTemplate);
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates_Edit)]
        private async Task Update(CreateOrEditEccpMaintenanceTemplateDto input)
        {
            if (input.Id != null)
            {
                var eccpMaintenanceTemplate =
                    await this._eccpMaintenanceTemplateRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpMaintenanceTemplate);
            }
        }
    }
}