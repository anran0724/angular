// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpCompanyUserAuditLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions
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
    using Abp.Domain.Uow;
    using Abp.Linq.Extensions;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos;

    /// <summary>
    /// The eccp company user audit logs app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs)]
    public class EccpCompanyUserAuditLogsAppService : ElevatorCloudAppServiceBase, IEccpCompanyUserAuditLogsAppService
    {
        /// <summary>
        /// The _eccp company user audit log repository.
        /// </summary>
        private readonly IRepository<EccpCompanyUserAuditLog> _eccpCompanyUserAuditLogRepository;

        /// <summary>
        /// The _user repository.
        /// </summary>
        private readonly IRepository<User, long> _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpCompanyUserAuditLogsAppService"/> class.
        /// </summary>
        /// <param name="eccpCompanyUserAuditLogRepository">
        /// The eccp company user audit log repository.
        /// </param>
        /// <param name="userRepository">
        /// The user repository.
        /// </param>
        public EccpCompanyUserAuditLogsAppService(
            IRepository<EccpCompanyUserAuditLog> eccpCompanyUserAuditLogRepository,
            IRepository<User, long> userRepository)
        {
            this._eccpCompanyUserAuditLogRepository = eccpCompanyUserAuditLogRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpCompanyUserAuditLogDto input)
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpCompanyUserAuditLogRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpCompanyUserAuditLogForView>> GetAll(
            GetAllEccpCompanyUserAuditLogsInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpCompanyUserAuditLogs = this._eccpCompanyUserAuditLogRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Remarks.Contains(input.Filter))
                    .WhereIf(input.CheckStateFilter > -1, e => Convert.ToInt32(e.CheckState) == input.CheckStateFilter);

                var query = (from o in filteredEccpCompanyUserAuditLogs
                             join o1 in this._userRepository.GetAll() on o.UserId equals o1.Id into j1
                             from s1 in j1.DefaultIfEmpty()
                             select new 
                                        {
                                            EccpCompanyUserAuditLog = o,
                                            UserName = s1 == null ? string.Empty : s1.Name
                                        }).WhereIf(
                    !string.IsNullOrWhiteSpace(input.UserNameFilter),
                    e => e.UserName.ToLower() == input.UserNameFilter.ToLower().Trim());

                var totalCount = await query.CountAsync();

                var eccpCompanyUserAuditLogs = new List<GetEccpCompanyUserAuditLogForView>();

                query.OrderBy(input.Sorting ?? "eccpCompanyUserAuditLog.id asc").PageBy(input).MapTo(eccpCompanyUserAuditLogs);

                return new PagedResultDto<GetEccpCompanyUserAuditLogForView>(totalCount, eccpCompanyUserAuditLogs);
            }
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs)]
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
        /// The get eccp company user audit log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs_Edit)]
        public async Task<GetEccpCompanyUserAuditLogForEditOutput> GetEccpCompanyUserAuditLogForEdit(EntityDto input)
        {
            var eccpCompanyUserAuditLog = await this._eccpCompanyUserAuditLogRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpCompanyUserAuditLogForEditOutput
                             {
                                 EccpCompanyUserAuditLog =
                                     this.ObjectMapper.Map<CreateOrEditEccpCompanyUserAuditLogDto>(
                                         eccpCompanyUserAuditLog)
                             };
            var user = await this._userRepository.FirstOrDefaultAsync(output.EccpCompanyUserAuditLog.UserId);
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs_Create)]
        private async Task Create(CreateOrEditEccpCompanyUserAuditLogDto input)
        {
            var eccpCompanyUserAuditLog = this.ObjectMapper.Map<EccpCompanyUserAuditLog>(input);

            await this._eccpCompanyUserAuditLogRepository.InsertAsync(eccpCompanyUserAuditLog);
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs_Edit)]
        private async Task Update(CreateOrEditEccpCompanyUserAuditLogDto input)
        {
            var eccpCompanyUserAuditLog =
                await this._eccpCompanyUserAuditLogRepository.FirstOrDefaultAsync((int)input.Id);
            this.ObjectMapper.Map(input, eccpCompanyUserAuditLog);
        }
    }
}