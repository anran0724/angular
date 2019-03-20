// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElevatorClaimLogsAppService.cs" company="Sinodom">
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
    ///     The elevator claim logs app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpElevator_ElevatorClaimLogs)]
    public class ElevatorClaimLogsAppService : ElevatorCloudAppServiceBase, IElevatorClaimLogsAppService
    {
        /// <summary>
        ///     The eccp base elevator repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;

        /// <summary>
        ///     The elevator claim log repository.
        /// </summary>
        private readonly IRepository<ElevatorClaimLog, long> _elevatorClaimLogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElevatorClaimLogsAppService"/> class.
        /// </summary>
        /// <param name="elevatorClaimLogRepository">
        /// The elevator claim log repository.
        /// </param>
        /// <param name="eccpBaseElevatorRepository">
        /// The eccp base elevator repository.
        /// </param>
        public ElevatorClaimLogsAppService(
            IRepository<ElevatorClaimLog, long> elevatorClaimLogRepository,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository)
        {
            this._elevatorClaimLogRepository = elevatorClaimLogRepository;
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
        public async Task CreateOrEdit(CreateOrEditElevatorClaimLogDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_ElevatorClaimLogs_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await this._elevatorClaimLogRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetElevatorClaimLogForView>> GetAll(GetAllElevatorClaimLogsInput input)
        {
            var filteredElevatorClaimLogs = this._elevatorClaimLogRepository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false);

            var query = (from o in filteredElevatorClaimLogs
                         join o1 in this._eccpBaseElevatorRepository.GetAll() on o.ElevatorId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         select new 
                                    {
                                        ElevatorClaimLog = o,
                                        EccpBaseElevatorName = s1 == null ? string.Empty : s1.Name
                                    }).WhereIf(
                !string.IsNullOrWhiteSpace(input.EccpBaseElevatorNameFilter),
                e => e.EccpBaseElevatorName.ToLower() == input.EccpBaseElevatorNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var elevatorClaimLogs = new List<GetElevatorClaimLogForView>();

            query.OrderBy(input.Sorting ?? "elevatorClaimLog.id asc").PageBy(input).MapTo(elevatorClaimLogs);

            return new PagedResultDto<GetElevatorClaimLogForView>(totalCount, elevatorClaimLogs);
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_ElevatorClaimLogs)]
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
        /// The get elevator claim log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_ElevatorClaimLogs_Edit)]
        public async Task<GetElevatorClaimLogForEditOutput> GetElevatorClaimLogForEdit(EntityDto<long> input)
        {
            var elevatorClaimLog = await this._elevatorClaimLogRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetElevatorClaimLogForEditOutput
                             {
                                 ElevatorClaimLog =
                                     this.ObjectMapper.Map<CreateOrEditElevatorClaimLogDto>(elevatorClaimLog)
                             };

            var eccpBaseElevator =
                await this._eccpBaseElevatorRepository.FirstOrDefaultAsync(output.ElevatorClaimLog.ElevatorId);
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_ElevatorClaimLogs_Create)]
        private async Task Create(CreateOrEditElevatorClaimLogDto input)
        {
            var elevatorClaimLog = this.ObjectMapper.Map<ElevatorClaimLog>(input);

            if (this.AbpSession.TenantId != null)
            {
                elevatorClaimLog.TenantId = (int)this.AbpSession.TenantId;
            }

            await this._elevatorClaimLogRepository.InsertAsync(elevatorClaimLog);
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_ElevatorClaimLogs_Edit)]
        private async Task Update(CreateOrEditElevatorClaimLogDto input)
        {
            if (input.Id != null)
            {
                var elevatorClaimLog = await this._elevatorClaimLogRepository.FirstOrDefaultAsync((long)input.Id);
                this.ObjectMapper.Map(input, elevatorClaimLog);
            }
        }
    }
}