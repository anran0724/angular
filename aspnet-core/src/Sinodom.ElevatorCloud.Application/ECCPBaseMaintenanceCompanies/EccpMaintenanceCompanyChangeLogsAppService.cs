// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceCompanyChangeLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies
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
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos;

    /// <summary>
    ///     The eccp maintenance company change logs app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs)]
    public class EccpMaintenanceCompanyChangeLogsAppService : ElevatorCloudAppServiceBase,
                                                              IEccpMaintenanceCompanyChangeLogsAppService
    {
        /// <summary>
        ///     The _e ccp base maintenance company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseMaintenanceCompany, int> _eccpBaseMaintenanceCompanyRepository;

        /// <summary>
        ///     The _eccp maintenance company change log repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceCompanyChangeLog> _eccpMaintenanceCompanyChangeLogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceCompanyChangeLogsAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceCompanyChangeLogRepository">
        /// The eccp maintenance company change log repository.
        /// </param>
        /// <param name="eccpBaseMaintenanceCompanyRepository">
        /// The e ccp base maintenance company repository.
        /// </param>
        public EccpMaintenanceCompanyChangeLogsAppService(
            IRepository<EccpMaintenanceCompanyChangeLog> eccpMaintenanceCompanyChangeLogRepository,
            IRepository<ECCPBaseMaintenanceCompany, int> eccpBaseMaintenanceCompanyRepository)
        {
            this._eccpMaintenanceCompanyChangeLogRepository = eccpMaintenanceCompanyChangeLogRepository;
            this._eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpMaintenanceCompanyChangeLogDto input)
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpMaintenanceCompanyChangeLogRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpMaintenanceCompanyChangeLogForView>> GetAll(
            GetAllEccpMaintenanceCompanyChangeLogsInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpMaintenanceCompanyChangeLogs = this._eccpMaintenanceCompanyChangeLogRepository.GetAll()
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.FieldName.Contains(input.Filter) || e.OldValue.Contains(input.Filter)
                                                                || e.NewValue.Contains(input.Filter))
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.FieldNameFilter),
                        e => e.FieldName.ToLower() == input.FieldNameFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.OldValueFilter),
                        e => e.OldValue.ToLower() == input.OldValueFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.NewValueFilter),
                        e => e.NewValue.ToLower() == input.NewValueFilter.ToLower().Trim());

                var query = (from o in filteredEccpMaintenanceCompanyChangeLogs
                             join o1 in this._eccpBaseMaintenanceCompanyRepository.GetAll() on o.MaintenanceCompanyId
                                 equals o1.Id into j1
                             from s1 in j1.DefaultIfEmpty()
                             select new 
                                        {
                                            EccpMaintenanceCompanyChangeLog = o,
                                            ECCPBaseMaintenanceCompanyName = s1 == null ? string.Empty : s1.Name
                                        }).WhereIf(
                    !string.IsNullOrWhiteSpace(input.ECCPBaseMaintenanceCompanyNameFilter),
                    e => e.ECCPBaseMaintenanceCompanyName.ToLower()
                         == input.ECCPBaseMaintenanceCompanyNameFilter.ToLower().Trim());

                var totalCount = await query.CountAsync();

                var eccpMaintenanceCompanyChangeLogs = new List<GetEccpMaintenanceCompanyChangeLogForView>();

                query.OrderBy(input.Sorting ?? "eccpMaintenanceCompanyChangeLog.id asc").PageBy(input).MapTo(eccpMaintenanceCompanyChangeLogs);

                return new PagedResultDto<GetEccpMaintenanceCompanyChangeLogForView>(
                    totalCount,
                    eccpMaintenanceCompanyChangeLogs);
            }
        }

        /// <summary>
        /// The get all eccp base maintenance company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs)]
        public async Task<PagedResultDto<ECCPBaseMaintenanceCompanyLookupTableDto>> GetAllECCPBaseMaintenanceCompanyForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseMaintenanceCompanyRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpBaseMaintenanceCompanyList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<ECCPBaseMaintenanceCompanyLookupTableDto>();
            foreach (var eccpBaseMaintenanceCompany in eccpBaseMaintenanceCompanyList)
            {
                lookupTableDtoList.Add(
                    new ECCPBaseMaintenanceCompanyLookupTableDto
                        {
                            Id = eccpBaseMaintenanceCompany.Id, DisplayName = eccpBaseMaintenanceCompany.Name
                        });
            }

            return new PagedResultDto<ECCPBaseMaintenanceCompanyLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get eccp maintenance company change log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs_Edit)]
        public async Task<GetEccpMaintenanceCompanyChangeLogForEditOutput> GetEccpMaintenanceCompanyChangeLogForEdit(
            EntityDto input)
        {
            var eccpMaintenanceCompanyChangeLog =
                await this._eccpMaintenanceCompanyChangeLogRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpMaintenanceCompanyChangeLogForEditOutput
                             {
                                 EccpMaintenanceCompanyChangeLog =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenanceCompanyChangeLogDto>(
                                         eccpMaintenanceCompanyChangeLog)
                             };
            var eccpBaseMaintenanceCompany =
                await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(
                    output.EccpMaintenanceCompanyChangeLog.MaintenanceCompanyId);
            output.ECCPBaseMaintenanceCompanyName = eccpBaseMaintenanceCompany.Name;

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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs_Create)]
        private async Task Create(CreateOrEditEccpMaintenanceCompanyChangeLogDto input)
        {
            var eccpMaintenanceCompanyChangeLog = this.ObjectMapper.Map<EccpMaintenanceCompanyChangeLog>(input);

            await this._eccpMaintenanceCompanyChangeLogRepository.InsertAsync(eccpMaintenanceCompanyChangeLog);
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs_Edit)]
        private async Task Update(CreateOrEditEccpMaintenanceCompanyChangeLogDto input)
        {
            if (input.Id != null)
            {
                var eccpMaintenanceCompanyChangeLog =
                    await this._eccpMaintenanceCompanyChangeLogRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpMaintenanceCompanyChangeLog);
            }
        }
    }
}