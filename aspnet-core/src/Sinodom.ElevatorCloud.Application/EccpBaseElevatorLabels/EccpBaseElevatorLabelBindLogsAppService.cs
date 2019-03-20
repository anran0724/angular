// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorLabelBindLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorLabels
{
    using System;
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
    using Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos;
    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.EccpDict;

    /// <summary>
    ///     The eccp base elevator label bind logs app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs)]
    public class EccpBaseElevatorLabelBindLogsAppService : ElevatorCloudAppServiceBase,
                                                           IEccpBaseElevatorLabelBindLogsAppService
    {
        /// <summary>
        ///     The eccp base elevator label bind log repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevatorLabelBindLog, long> _eccpBaseElevatorLabelBindLogRepository;

        /// <summary>
        ///     The eccp base elevator repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;

        /// <summary>
        ///     The eccp dict label status repository.
        /// </summary>
        private readonly IRepository<EccpDictLabelStatus, int> _eccpDictLabelStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorLabelBindLogsAppService"/> class.
        /// </summary>
        /// <param name="eccpBaseElevatorLabelBindLogRepository">
        /// The eccp base elevator label bind log repository.
        /// </param>
        /// <param name="eccpBaseElevatorRepository">
        /// The eccp base elevator repository.
        /// </param>
        /// <param name="eccpDictLabelStatusRepository">
        /// The eccp dict label status repository.
        /// </param>
        public EccpBaseElevatorLabelBindLogsAppService(
            IRepository<EccpBaseElevatorLabelBindLog, long> eccpBaseElevatorLabelBindLogRepository,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository,
            IRepository<EccpDictLabelStatus, int> eccpDictLabelStatusRepository)
        {
            this._eccpBaseElevatorLabelBindLogRepository = eccpBaseElevatorLabelBindLogRepository;
            this._eccpBaseElevatorRepository = eccpBaseElevatorRepository;
            this._eccpDictLabelStatusRepository = eccpDictLabelStatusRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpBaseElevatorLabelBindLogDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await this._eccpBaseElevatorLabelBindLogRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpBaseElevatorLabelBindLogForView>> GetAll(
            GetAllEccpBaseElevatorLabelBindLogsInput input)
        {
            var filteredEccpBaseElevatorLabelBindLogs = this._eccpBaseElevatorLabelBindLogRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.LabelName.Contains(input.Filter) || e.LocalInformation.Contains(input.Filter)
                                                            || e.Remark.Contains(input.Filter))
                .WhereIf(input.MinBindingTimeFilter != null, e => e.BindingTime >= input.MinBindingTimeFilter).WhereIf(
                    input.MaxBindingTimeFilter != null,
                    e => e.BindingTime <= input.MaxBindingTimeFilter);

            var query = (from o in filteredEccpBaseElevatorLabelBindLogs
                         join o1 in this._eccpBaseElevatorRepository.GetAll() on o.ElevatorId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         join o2 in this._eccpDictLabelStatusRepository.GetAll() on o.LabelStatusId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         select new 
                                    {
                                        EccpBaseElevatorLabelBindLog = o,
                                        EccpBaseElevatorName = s1 == null ? string.Empty : s1.Name,
                                        EccpDictLabelStatusName = s2 == null ? string.Empty : s2.Name
                                    }).WhereIf(
                !string.IsNullOrWhiteSpace(input.EccpBaseElevatorNameFilter),
                e => e.EccpBaseElevatorName.ToLower() == input.EccpBaseElevatorNameFilter.ToLower().Trim()).WhereIf(
                !string.IsNullOrWhiteSpace(input.EccpDictLabelStatusNameFilter),
                e => e.EccpDictLabelStatusName.ToLower() == input.EccpDictLabelStatusNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();



            var eccpBaseElevatorLabelBindLogs = new List<GetEccpBaseElevatorLabelBindLogForView>();
            query.OrderBy(input.Sorting ?? "eccpBaseElevatorLabelBindLog.id asc").PageBy(input).MapTo(eccpBaseElevatorLabelBindLogs);

            return new PagedResultDto<GetEccpBaseElevatorLabelBindLogForView>(
                totalCount,
                eccpBaseElevatorLabelBindLogs);
        }

        /// <summary>
        /// The get all eccp base elevator for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs)]
        public async Task<PagedResultDto<EccpBaseElevatorLookupTableDto>> GetAllEccpBaseElevatorForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseElevatorRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpBaseElevatorList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpBaseElevatorLookupTableDto>();
            foreach (var eccpBaseElevator in eccpBaseElevatorList)
            {
                lookupTableDtoList.Add(
                    new EccpBaseElevatorLookupTableDto
                        {
                            Id = eccpBaseElevator.Id.ToString(), DisplayName = eccpBaseElevator.Name
                        });
            }

            return new PagedResultDto<EccpBaseElevatorLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp dict label status for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs)]
        public async Task<PagedResultDto<EccpDictLabelStatusLookupTableDto>> GetAllEccpDictLabelStatusForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpDictLabelStatusRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpDictLabelStatusList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpDictLabelStatusLookupTableDto>();
            foreach (var eccpDictLabelStatus in eccpDictLabelStatusList)
            {
                lookupTableDtoList.Add(
                    new EccpDictLabelStatusLookupTableDto
                        {
                            Id = eccpDictLabelStatus.Id, DisplayName = eccpDictLabelStatus.Name
                        });
            }

            return new PagedResultDto<EccpDictLabelStatusLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get eccp base elevator label bind log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs_Edit)]
        public async Task<GetEccpBaseElevatorLabelBindLogForEditOutput> GetEccpBaseElevatorLabelBindLogForEdit(
            EntityDto<long> input)
        {
            var eccpBaseElevatorLabelBindLog =
                await this._eccpBaseElevatorLabelBindLogRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpBaseElevatorLabelBindLogForEditOutput
                             {
                                 EccpBaseElevatorLabelBindLog =
                                     this.ObjectMapper.Map<CreateOrEditEccpBaseElevatorLabelBindLogDto>(
                                         eccpBaseElevatorLabelBindLog)
                             };

            if (output.EccpBaseElevatorLabelBindLog.ElevatorId != null)
            {
                var eccpBaseElevator =
                    await this._eccpBaseElevatorRepository.FirstOrDefaultAsync(
                        (Guid)output.EccpBaseElevatorLabelBindLog.ElevatorId);
                output.EccpBaseElevatorName = eccpBaseElevator.Name;
            }

            var eccpDictLabelStatus =
                await this._eccpDictLabelStatusRepository.FirstOrDefaultAsync(
                    output.EccpBaseElevatorLabelBindLog.LabelStatusId);
            output.EccpDictLabelStatusName = eccpDictLabelStatus.Name;

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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs_Create)]
        private async Task Create(CreateOrEditEccpBaseElevatorLabelBindLogDto input)
        {
            var eccpBaseElevatorLabelBindLog = this.ObjectMapper.Map<EccpBaseElevatorLabelBindLog>(input);

            await this._eccpBaseElevatorLabelBindLogRepository.InsertAsync(eccpBaseElevatorLabelBindLog);
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs_Edit)]
        private async Task Update(CreateOrEditEccpBaseElevatorLabelBindLogDto input)
        {
            if (input.Id != null)
            {
                var eccpBaseElevatorLabelBindLog =
                    await this._eccpBaseElevatorLabelBindLogRepository.FirstOrDefaultAsync((long)input.Id);
                this.ObjectMapper.Map(input, eccpBaseElevatorLabelBindLog);
            }
        }
    }
}