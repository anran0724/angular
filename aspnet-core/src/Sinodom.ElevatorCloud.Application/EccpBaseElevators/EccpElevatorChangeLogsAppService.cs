// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpElevatorChangeLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators
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
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;

    /// <summary>
    ///     The eccp elevator change logs app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs)]
    public class EccpElevatorChangeLogsAppService : ElevatorCloudAppServiceBase, IEccpElevatorChangeLogsAppService
    {
        /// <summary>
        ///     The eccp base elevator repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;

        /// <summary>
        ///     The eccp elevator change log repository.
        /// </summary>
        private readonly IRepository<EccpElevatorChangeLog> _eccpElevatorChangeLogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpElevatorChangeLogsAppService"/> class.
        /// </summary>
        /// <param name="eccpElevatorChangeLogRepository">
        /// The eccp elevator change log repository.
        /// </param>
        /// <param name="eccpBaseElevatorRepository">
        /// The eccp base elevator repository.
        /// </param>
        public EccpElevatorChangeLogsAppService(
            IRepository<EccpElevatorChangeLog> eccpElevatorChangeLogRepository,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository)
        {
            this._eccpElevatorChangeLogRepository = eccpElevatorChangeLogRepository;
            this._eccpBaseElevatorRepository = eccpBaseElevatorRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpElevatorChangeLogDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpElevatorChangeLogRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpElevatorChangeLogForView>> GetAll(
            GetAllEccpElevatorChangeLogsInput input)
        {
            var filteredEccpElevatorChangeLogs = this._eccpElevatorChangeLogRepository.GetAll()
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

            var query = (from o in filteredEccpElevatorChangeLogs
                         join o1 in this._eccpBaseElevatorRepository.GetAll() on o.ElevatorId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         select new 
                                    {
                                        EccpElevatorChangeLog = o,
                                        EccpBaseElevatorName = s1 == null ? string.Empty : s1.Name
                                    }).WhereIf(
                !string.IsNullOrWhiteSpace(input.EccpBaseElevatorNameFilter),
                e => e.EccpBaseElevatorName.ToLower() == input.EccpBaseElevatorNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var eccpElevatorChangeLogs = new List<GetEccpElevatorChangeLogForView>();

            query.OrderBy(input.Sorting ?? "eccpElevatorChangeLog.id asc").PageBy(input).MapTo(eccpElevatorChangeLogs);

            return new PagedResultDto<GetEccpElevatorChangeLogForView>(totalCount, eccpElevatorChangeLogs);
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs)]
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
        /// The get eccp elevator change log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs_Edit)]
        public async Task<GetEccpElevatorChangeLogForEditOutput> GetEccpElevatorChangeLogForEdit(EntityDto input)
        {
            var eccpElevatorChangeLog = await this._eccpElevatorChangeLogRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpElevatorChangeLogForEditOutput
                             {
                                 EccpElevatorChangeLog =
                                     this.ObjectMapper.Map<CreateOrEditEccpElevatorChangeLogDto>(eccpElevatorChangeLog)
                             };
            var eccpBaseElevator =
                await this._eccpBaseElevatorRepository.FirstOrDefaultAsync(output.EccpElevatorChangeLog.ElevatorId);
            output.EccpBaseElevatorName = eccpBaseElevator.Name;

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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs_Create)]
        private async Task Create(CreateOrEditEccpElevatorChangeLogDto input)
        {
            var eccpElevatorChangeLog = this.ObjectMapper.Map<EccpElevatorChangeLog>(input);

            await this._eccpElevatorChangeLogRepository.InsertAsync(eccpElevatorChangeLog);
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs_Edit)]
        private async Task Update(CreateOrEditEccpElevatorChangeLogDto input)
        {
            if (input.Id != null)
            {
                var eccpElevatorChangeLog =
                    await this._eccpElevatorChangeLogRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpElevatorChangeLog);
            }
        }
    }
}