// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpPropertyCompanyChangeLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies
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
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos;

    /// <summary>
    ///     The eccp property company change logs app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs)]
    public class EccpPropertyCompanyChangeLogsAppService : ElevatorCloudAppServiceBase,
                                                           IEccpPropertyCompanyChangeLogsAppService
    {
        /// <summary>
        ///     The _eccp base property company repository.
        /// </summary>
        private readonly IRepository<ECCPBasePropertyCompany, int> _eccpBasePropertyCompanyRepository;

        /// <summary>
        ///     The _eccp property company change log repository.
        /// </summary>
        private readonly IRepository<EccpPropertyCompanyChangeLog> _eccpPropertyCompanyChangeLogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpPropertyCompanyChangeLogsAppService"/> class.
        /// </summary>
        /// <param name="eccpPropertyCompanyChangeLogRepository">
        /// The eccp property company change log repository.
        /// </param>
        /// <param name="eccpBasePropertyCompanyRepository">
        /// The e ccp base property company repository.
        /// </param>
        public EccpPropertyCompanyChangeLogsAppService(
            IRepository<EccpPropertyCompanyChangeLog> eccpPropertyCompanyChangeLogRepository,
            IRepository<ECCPBasePropertyCompany, int> eccpBasePropertyCompanyRepository)
        {
            this._eccpPropertyCompanyChangeLogRepository = eccpPropertyCompanyChangeLogRepository;
            this._eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpPropertyCompanyChangeLogDto input)
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpPropertyCompanyChangeLogRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpPropertyCompanyChangeLogForView>> GetAll(
            GetAllEccpPropertyCompanyChangeLogsInput input)
        {
            var filteredEccpPropertyCompanyChangeLogs = this._eccpPropertyCompanyChangeLogRepository.GetAll()
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

            var query = (from o in filteredEccpPropertyCompanyChangeLogs
                         join o1 in this._eccpBasePropertyCompanyRepository.GetAll() on o.PropertyCompanyId equals o1.Id
                             into j1
                         from s1 in j1.DefaultIfEmpty()
                         select new 
                                    {
                                        EccpPropertyCompanyChangeLog = o,
                                        ECCPBasePropertyCompanyName = s1 == null ? string.Empty : s1.Name
                                    }).WhereIf(
                !string.IsNullOrWhiteSpace(input.ECCPBasePropertyCompanyNameFilter),
                e => e.ECCPBasePropertyCompanyName.ToLower()
                     == input.ECCPBasePropertyCompanyNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var eccpPropertyCompanyChangeLogs = new List<GetEccpPropertyCompanyChangeLogForView>();

            query.OrderBy(input.Sorting ?? "eccpPropertyCompanyChangeLog.id asc").PageBy(input).MapTo(eccpPropertyCompanyChangeLogs);

            return new PagedResultDto<GetEccpPropertyCompanyChangeLogForView>(
                totalCount,
                eccpPropertyCompanyChangeLogs);
        }

        /// <summary>
        /// The get all eccp base property company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs)]
        public async Task<PagedResultDto<ECCPBasePropertyCompanyLookupTableDto>> GetAllECCPBasePropertyCompanyForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpBasePropertyCompanyRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpBasePropertyCompanyList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<ECCPBasePropertyCompanyLookupTableDto>();
            foreach (var eccpBasePropertyCompany in eccpBasePropertyCompanyList)
            {
                lookupTableDtoList.Add(
                    new ECCPBasePropertyCompanyLookupTableDto
                        {
                            Id = eccpBasePropertyCompany.Id, DisplayName = eccpBasePropertyCompany.Name
                        });
            }

            return new PagedResultDto<ECCPBasePropertyCompanyLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get eccp property company change log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs_Edit)]
        public async Task<GetEccpPropertyCompanyChangeLogForEditOutput> GetEccpPropertyCompanyChangeLogForEdit(
            EntityDto input)
        {
            var eccpPropertyCompanyChangeLog =
                await this._eccpPropertyCompanyChangeLogRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpPropertyCompanyChangeLogForEditOutput
                             {
                                 EccpPropertyCompanyChangeLog =
                                     this.ObjectMapper.Map<CreateOrEditEccpPropertyCompanyChangeLogDto>(
                                         eccpPropertyCompanyChangeLog)
                             };
            var eccpBasePropertyCompany =
                await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(
                    output.EccpPropertyCompanyChangeLog.PropertyCompanyId);
            output.ECCPBasePropertyCompanyName = eccpBasePropertyCompany.Name;

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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs_Create)]
        private async Task Create(CreateOrEditEccpPropertyCompanyChangeLogDto input)
        {
            var eccpPropertyCompanyChangeLog = this.ObjectMapper.Map<EccpPropertyCompanyChangeLog>(input);

            await this._eccpPropertyCompanyChangeLogRepository.InsertAsync(eccpPropertyCompanyChangeLog);
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs_Edit)]
        private async Task Update(CreateOrEditEccpPropertyCompanyChangeLogDto input)
        {
            if (input.Id != null)
            {
                var eccpPropertyCompanyChangeLog =
                    await this._eccpPropertyCompanyChangeLogRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpPropertyCompanyChangeLog);
            }
        }
    }
}