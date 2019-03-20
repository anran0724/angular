// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTempWorkOrderActionLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs
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
    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;

    /// <summary>
    /// The eccp maintenance temp work order action logs app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs)]
    public class EccpMaintenanceTempWorkOrderActionLogsAppService : ElevatorCloudAppServiceBase,
                                                                    IEccpMaintenanceTempWorkOrderActionLogsAppService
    {
        /// <summary>
        /// The _eccp maintenance temp work order action log repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTempWorkOrderActionLog, Guid> _eccpMaintenanceTempWorkOrderActionLogRepository;

        /// <summary>
        /// The _eccp maintenance temp work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTempWorkOrder, Guid> _eccpMaintenanceTempWorkOrderRepository;

        /// <summary>
        /// The _user repository.
        /// </summary>
        private readonly IRepository<User, long> _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTempWorkOrderActionLogsAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceTempWorkOrderActionLogRepository">
        /// The eccp maintenance temp work order action log repository.
        /// </param>
        /// <param name="eccpMaintenanceTempWorkOrderRepository">
        /// The eccp maintenance temp work order repository.
        /// </param>
        /// <param name="userRepository">
        /// The user repository.
        /// </param>
        public EccpMaintenanceTempWorkOrderActionLogsAppService(
            IRepository<EccpMaintenanceTempWorkOrderActionLog, Guid> eccpMaintenanceTempWorkOrderActionLogRepository,
            IRepository<EccpMaintenanceTempWorkOrder, Guid> eccpMaintenanceTempWorkOrderRepository,
            IRepository<User, long> userRepository)
        {
            this._eccpMaintenanceTempWorkOrderActionLogRepository = eccpMaintenanceTempWorkOrderActionLogRepository;
            this._eccpMaintenanceTempWorkOrderRepository = eccpMaintenanceTempWorkOrderRepository;
            this._userRepository = userRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpMaintenanceTempWorkOrderActionLogDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await this._eccpMaintenanceTempWorkOrderActionLogRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpMaintenanceTempWorkOrderActionLogForView>> GetAll(
            GetAllEccpMaintenanceTempWorkOrderActionLogsInput input)
        {
            var filteredEccpMaintenanceTempWorkOrderActionLogs = this._eccpMaintenanceTempWorkOrderActionLogRepository
                .GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Remarks.Contains(input.Filter))
                .WhereIf(input.MinCheckStateFilter != null, e => e.CheckState >= input.MinCheckStateFilter).WhereIf(
                    input.MaxCheckStateFilter != null,
                    e => e.CheckState <= input.MaxCheckStateFilter);

            var query = (from o in filteredEccpMaintenanceTempWorkOrderActionLogs
                         join o1 in this._eccpMaintenanceTempWorkOrderRepository.GetAll() on o.TempWorkOrderId equals
                             o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         join o2 in this._userRepository.GetAll() on o.UserId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         select new 
                                    {
                                        EccpMaintenanceTempWorkOrderActionLog = o,
                                        EccpMaintenanceTempWorkOrderTitle = s1 == null ? string.Empty : s1.Title,
                                        UserName = s2 == null ? string.Empty : s2.Name
                                    }).WhereIf(
                !string.IsNullOrWhiteSpace(input.EccpMaintenanceTempWorkOrderTitleFilter),
                e => e.EccpMaintenanceTempWorkOrderTitle.ToLower()
                     == input.EccpMaintenanceTempWorkOrderTitleFilter.ToLower().Trim()).WhereIf(
                !string.IsNullOrWhiteSpace(input.UserNameFilter),
                e => e.UserName.ToLower() == input.UserNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var eccpMaintenanceTempWorkOrderActionLogs = new List<GetEccpMaintenanceTempWorkOrderActionLogForView>();

            query.OrderBy(input.Sorting ?? "eccpMaintenanceTempWorkOrderActionLog.id asc").PageBy(input).MapTo(eccpMaintenanceTempWorkOrderActionLogs);

            return new PagedResultDto<GetEccpMaintenanceTempWorkOrderActionLogForView>(
                totalCount,
                eccpMaintenanceTempWorkOrderActionLogs);
        }

        /// <summary>
        /// The get all eccp maintenance temp work order for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs)]
        public async Task<PagedResultDto<EccpMaintenanceTempWorkOrderLookupTableDto>> GetAllEccpMaintenanceTempWorkOrderForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpMaintenanceTempWorkOrderRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Title.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpMaintenanceTempWorkOrderList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpMaintenanceTempWorkOrderLookupTableDto>();
            foreach (var eccpMaintenanceTempWorkOrder in eccpMaintenanceTempWorkOrderList)
            {
                lookupTableDtoList.Add(
                    new EccpMaintenanceTempWorkOrderLookupTableDto
                        {
                            Id = eccpMaintenanceTempWorkOrder.Id.ToString(),
                            DisplayName = eccpMaintenanceTempWorkOrder.Title
                        });
            }

            return new PagedResultDto<EccpMaintenanceTempWorkOrderLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all user for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs)]
        public async Task<PagedResultDto<UserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._userRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var userList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<UserLookupTableDto>();
            foreach (var user in userList)
            {
                lookupTableDtoList.Add(new UserLookupTableDto { Id = user.Id, DisplayName = user.Name });
            }

            return new PagedResultDto<UserLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get eccp maintenance temp work order action log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs_Edit)]
        public async Task<GetEccpMaintenanceTempWorkOrderActionLogForEditOutput> GetEccpMaintenanceTempWorkOrderActionLogForEdit(EntityDto<Guid> input)
        {
            var eccpMaintenanceTempWorkOrderActionLog =
                await this._eccpMaintenanceTempWorkOrderActionLogRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpMaintenanceTempWorkOrderActionLogForEditOutput
                             {
                                 EccpMaintenanceTempWorkOrderActionLog =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenanceTempWorkOrderActionLogDto>(
                                         eccpMaintenanceTempWorkOrderActionLog)
                             };

            var eccpMaintenanceTempWorkOrder =
                await this._eccpMaintenanceTempWorkOrderRepository.FirstOrDefaultAsync(
                    output.EccpMaintenanceTempWorkOrderActionLog.TempWorkOrderId);

            output.EccpMaintenanceTempWorkOrderTitle = eccpMaintenanceTempWorkOrder.Title;

            var user = await this._userRepository.FirstOrDefaultAsync(
                           output.EccpMaintenanceTempWorkOrderActionLog.UserId);
            output.UserName = user.Name;

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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs_Create)]
        private async Task Create(CreateOrEditEccpMaintenanceTempWorkOrderActionLogDto input)
        {
            var eccpMaintenanceTempWorkOrderActionLog =
                this.ObjectMapper.Map<EccpMaintenanceTempWorkOrderActionLog>(input);

            if (this.AbpSession.TenantId != null)
            {
                eccpMaintenanceTempWorkOrderActionLog.TenantId = (int)this.AbpSession.TenantId;
            }

            await this._eccpMaintenanceTempWorkOrderActionLogRepository.InsertAsync(
                eccpMaintenanceTempWorkOrderActionLog);
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs_Edit)]
        private async Task Update(CreateOrEditEccpMaintenanceTempWorkOrderActionLogDto input)
        {
            if (input.Id != null)
            {
                var eccpMaintenanceTempWorkOrderActionLog =
                    await this._eccpMaintenanceTempWorkOrderActionLogRepository.FirstOrDefaultAsync((Guid)input.Id);
                this.ObjectMapper.Map(input, eccpMaintenanceTempWorkOrderActionLog);
            }
        }
    }
}