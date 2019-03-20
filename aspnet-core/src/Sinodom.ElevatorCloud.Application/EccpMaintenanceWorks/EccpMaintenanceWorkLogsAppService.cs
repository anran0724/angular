// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks
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

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.EccpElevatorQrCodes;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;

    /// <summary>
    ///     The eccp maintenance work logs app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkLogs)]
    public class EccpMaintenanceWorkLogsAppService : ElevatorCloudAppServiceBase, IEccpMaintenanceWorkLogsAppService
    {
        /// <summary>
        ///     The _eccp base elevators repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorsRepository;

        /// <summary>
        ///     The _e ccp base property company repository.
        /// </summary>
        private readonly IRepository<ECCPBasePropertyCompany> _eccpBasePropertyCompanyRepository;

        /// <summary>
        ///     The _eccp elevator qr code repository.
        /// </summary>
        private readonly IRepository<EccpElevatorQrCode, Guid> _eccpElevatorQrCodeRepository;

        /// <summary>
        ///     The _eccp maintenance plan_ maintenance user_ link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> _eccpMaintenancePlanMaintenanceUserLinkRepository;

        /// <summary>
        ///     The _eccp maintenance plan repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan, int> _eccpMaintenancePlanRepository;

        /// <summary>
        ///     The _eccp maintenance work flow repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkFlow, Guid> _eccpMaintenanceWorkFlowRepository;

        /// <summary>
        ///     The _eccp maintenance work log repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkLog, long> _eccpMaintenanceWorkLogRepository;

        /// <summary>
        ///     The _eccp maintenance work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder> _eccpMaintenanceWorkOrderRepository;

        /// <summary>
        ///     The _eccp maintenance work repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWork> _eccpMaintenanceWorkRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkLogsAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceWorkLogRepository">
        /// The eccp maintenance work log repository.
        /// </param>
        /// <param name="eccpMaintenancePlanMaintenanceUserLinkRepository">
        /// The eccp maintenance plan_ maintenance user_ link repository.
        /// </param>
        /// <param name="eccpMaintenancePlanRepository">
        /// The eccp maintenance plan repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderRepository">
        /// The eccp maintenance work order repository.
        /// </param>
        /// <param name="eccpBaseElevatorsRepository">
        /// The eccp base elevators repository.
        /// </param>
        /// <param name="eccpElevatorQrCodeRepository">
        /// The eccp elevator qr code repository.
        /// </param>
        /// <param name="eccpBasePropertyCompanyRepository">
        /// The e ccp base property company repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkFlowRepository">
        /// The eccp maintenance work flow repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkRepository">
        /// The eccp maintenance work repository.
        /// </param>
        public EccpMaintenanceWorkLogsAppService(
            IRepository<EccpMaintenanceWorkLog, long> eccpMaintenanceWorkLogRepository,
            IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> eccpMaintenancePlanMaintenanceUserLinkRepository,
            IRepository<EccpMaintenancePlan, int> eccpMaintenancePlanRepository,
            IRepository<EccpMaintenanceWorkOrder> eccpMaintenanceWorkOrderRepository,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorsRepository,
            IRepository<EccpElevatorQrCode, Guid> eccpElevatorQrCodeRepository,
            IRepository<ECCPBasePropertyCompany> eccpBasePropertyCompanyRepository,
            IRepository<EccpMaintenanceWorkFlow, Guid> eccpMaintenanceWorkFlowRepository,
            IRepository<EccpMaintenanceWork> eccpMaintenanceWorkRepository)
        {
            this._eccpMaintenanceWorkLogRepository = eccpMaintenanceWorkLogRepository;
            this._eccpMaintenancePlanMaintenanceUserLinkRepository = eccpMaintenancePlanMaintenanceUserLinkRepository;
            this._eccpMaintenancePlanRepository = eccpMaintenancePlanRepository;
            this._eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
            this._eccpBaseElevatorsRepository = eccpBaseElevatorsRepository;
            this._eccpElevatorQrCodeRepository = eccpElevatorQrCodeRepository;
            this._eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
            this._eccpMaintenanceWorkFlowRepository = eccpMaintenanceWorkFlowRepository;
            this._eccpMaintenanceWorkRepository = eccpMaintenanceWorkRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpMaintenanceWorkLogDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkLogs_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await this._eccpMaintenanceWorkLogRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpMaintenanceWorkLogForView>> GetAll(
            GetAllEccpMaintenanceWorkLogsInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpMaintenanceWorkLogs = this._eccpMaintenanceWorkLogRepository.GetAll()
                    .Where(w => w.TenantId == this.AbpSession.TenantId);

                var query = from o in filteredEccpMaintenanceWorkLogs select new { EccpMaintenanceWorkLog = o };

                List<GetEccpMaintenanceWorkLogForView> eccpMaintenanceWorkLogs;
                int totalCount;

                if (!string.IsNullOrWhiteSpace(input.EccpMaintenanceUserNameFilter)
                    || !string.IsNullOrWhiteSpace(input.EccpMaintenanceCompanyNameFilter)
                    || !string.IsNullOrWhiteSpace(input.EccpPropertyCompanyNameFilter)
                    || !string.IsNullOrWhiteSpace(input.EccpElevatorNumFilter))
                {
                    var eccpMaintenancePlanMaintenanceUserLinks = this._eccpMaintenancePlanMaintenanceUserLinkRepository
                        .GetAll().Where(w => w.TenantId == this.AbpSession.TenantId);

                    var users = this.UserManager.Users;

                    var eccpMaintenancePlanMaintenanceUsers =
                        from eccpMaintenancePlanMaintenanceUserLink in eccpMaintenancePlanMaintenanceUserLinks
                        join user in users on eccpMaintenancePlanMaintenanceUserLink.UserId equals user.Id
                        select new { eccpMaintenancePlanMaintenanceUserLink.MaintenancePlanId, user.UserName };

                    var filteredEccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId);

                    var maintenanceWorkOrderQuery = from o in filteredEccpMaintenanceWorkOrders
                                                    join o1 in this._eccpMaintenancePlanRepository.GetAll() on
                                                        o.MaintenancePlanId equals o1.Id into j1
                                                    from s1 in j1.DefaultIfEmpty()
                                                    join o2 in eccpMaintenancePlanMaintenanceUsers on
                                                        o.MaintenancePlanId equals o2.MaintenancePlanId into j2
                                                    from s2 in j2.DefaultIfEmpty()
                                                    select new
                                                               {
                                                                   EccpMaintenanceWorkOrder = o,
                                                                   EccpElevatorId =
                                                                       s1 == null ? Guid.Empty : s1.ElevatorId,
                                                                   EccpMaintenanceUserName =
                                                                       s2 == null ? string.Empty : s2.UserName
                                                               };

                    var workOrderQuery = maintenanceWorkOrderQuery.GroupBy(g => g.EccpMaintenanceWorkOrder.Id).Select(
                        m => new
                                 {
                                     EccpMaintenanceWorkOrderId = m.Key,
                                     m.FirstOrDefault().EccpElevatorId,
                                     EccpMaintenanceUserNameList =
                                         m.Select(u => u.EccpMaintenanceUserName).Distinct().ToList()
                                 });

                    var filteredEccpBaseElevators = this._eccpBaseElevatorsRepository.GetAll();
                    var filteredEccpElevatorQrCodes = this._eccpElevatorQrCodeRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId);
                    var filteredEccpBasePropertyCompanies = this._eccpBasePropertyCompanyRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId);

                    var filteredEccpMaintenanceWorkFlows = this._eccpMaintenanceWorkFlowRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId);
                    var filteredEccpMaintenanceWorks = this._eccpMaintenanceWorkRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId);

                    var workLogQuery =
                        (from o in query
                         join o1 in filteredEccpMaintenanceWorkFlows on o.EccpMaintenanceWorkLog.MaintenanceWorkFlowId
                             equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         join o2 in filteredEccpMaintenanceWorks on s1.MaintenanceWorkId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         join o3 in workOrderQuery on s2.MaintenanceWorkOrderId equals o3.EccpMaintenanceWorkOrderId
                             into j3
                         from s3 in j3.DefaultIfEmpty()
                         join o4 in filteredEccpBaseElevators on s3.EccpElevatorId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()
                         join o5 in filteredEccpElevatorQrCodes on s4.Id equals o5.ElevatorId into j5
                         from s5 in j5.DefaultIfEmpty()
                         join o6 in filteredEccpBasePropertyCompanies on s4.ECCPBasePropertyCompanyId equals o6.Id into
                             j6
                         from s6 in j6.DefaultIfEmpty()
                         select new
                                    {
                                        o.EccpMaintenanceWorkLog,
                                        EccpMaintenanceCompanyName =
                                            s4.Name == null ? string.Empty :
                                            s4.ECCPBaseMaintenanceCompany == null ? string.Empty :
                                            s4.ECCPBaseMaintenanceCompany.Name,
                                        EccpPropertyCompanyName = s6 == null ? string.Empty : s6.Name,
                                        EccpBaseElevatorNum = s5 == null ? string.Empty : s5.AreaName + s5.ElevatorNum,
                                        EccpMaintenanceUserNameList = s3 == null ? null : s3.EccpMaintenanceUserNameList
                                    }).WhereIf(
                            !string.IsNullOrWhiteSpace(input.EccpMaintenanceUserNameFilter),
                            e => e.EccpMaintenanceUserNameList.Contains(input.EccpMaintenanceUserNameFilter.Trim()))
                        .WhereIf(
                            !string.IsNullOrWhiteSpace(input.EccpMaintenanceCompanyNameFilter),
                            e => e.EccpMaintenanceCompanyName.Contains(input.EccpMaintenanceCompanyNameFilter.Trim()))
                        .WhereIf(
                            !string.IsNullOrWhiteSpace(input.EccpPropertyCompanyNameFilter),
                            e => e.EccpPropertyCompanyName.Contains(input.EccpPropertyCompanyNameFilter.Trim()))
                        .WhereIf(
                            !string.IsNullOrWhiteSpace(input.EccpElevatorNumFilter),
                            e => e.EccpBaseElevatorNum.Contains(input.EccpElevatorNumFilter.Trim()));

                    totalCount = workLogQuery.Count();

                    eccpMaintenanceWorkLogs = new List<GetEccpMaintenanceWorkLogForView>();

                    workLogQuery.OrderBy(input.Sorting ?? "eccpMaintenanceWorkLog.id asc").PageBy(input)
                        .MapTo(eccpMaintenanceWorkLogs);

                    return new PagedResultDto<GetEccpMaintenanceWorkLogForView>(totalCount, eccpMaintenanceWorkLogs);
                }
                else
                {
                    var eccpMaintenancePlanMaintenanceUserLinks = this._eccpMaintenancePlanMaintenanceUserLinkRepository
                        .GetAll().Where(w => w.TenantId == this.AbpSession.TenantId);

                    var users = this.UserManager.Users;

                    var eccpMaintenancePlanMaintenanceUsers =
                        from eccpMaintenancePlanMaintenanceUserLink in eccpMaintenancePlanMaintenanceUserLinks
                        join user in users on eccpMaintenancePlanMaintenanceUserLink.UserId equals user.Id
                        select new { eccpMaintenancePlanMaintenanceUserLink.MaintenancePlanId, user.UserName };

                    var filteredEccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId);

                    var maintenanceWorkOrderQuery = from o in filteredEccpMaintenanceWorkOrders
                                                    join o1 in this._eccpMaintenancePlanRepository.GetAll() on
                                                        o.MaintenancePlanId equals o1.Id into j1
                                                    from s1 in j1.DefaultIfEmpty()
                                                    join o2 in eccpMaintenancePlanMaintenanceUsers on
                                                        o.MaintenancePlanId equals o2.MaintenancePlanId into j2
                                                    from s2 in j2.DefaultIfEmpty()
                                                    select new
                                                               {
                                                                   EccpMaintenanceWorkOrder = o,
                                                                   EccpElevatorId =
                                                                       s1 == null ? Guid.Empty : s1.ElevatorId,
                                                                   EccpMaintenanceUserName =
                                                                       s2 == null ? string.Empty : s2.UserName
                                                               };

                    var workOrderQuery = maintenanceWorkOrderQuery.GroupBy(g => g.EccpMaintenanceWorkOrder.Id).Select(
                        m => new
                                 {
                                     EccpMaintenanceWorkOrderId = m.Key,
                                     m.FirstOrDefault().EccpElevatorId,
                                     EccpMaintenanceUserNameList =
                                         m.Select(u => u.EccpMaintenanceUserName).Distinct().ToList()
                                 });

                    var filteredEccpBaseElevators = this._eccpBaseElevatorsRepository.GetAll();
                    var filteredEccpElevatorQrCodes = this._eccpElevatorQrCodeRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId);
                    var filteredEccpBasePropertyCompanies = this._eccpBasePropertyCompanyRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId);

                    var filteredEccpMaintenanceWorkFlows = this._eccpMaintenanceWorkFlowRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId);
                    var filteredEccpMaintenanceWorks = this._eccpMaintenanceWorkRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId);

                    var workLogQuery = from o in query
                                       join o1 in filteredEccpMaintenanceWorkFlows on
                                           o.EccpMaintenanceWorkLog.MaintenanceWorkFlowId equals o1.Id into j1
                                       from s1 in j1.DefaultIfEmpty()
                                       join o2 in filteredEccpMaintenanceWorks on s1.MaintenanceWorkId equals o2.Id into
                                           j2
                                       from s2 in j2.DefaultIfEmpty()
                                       join o3 in workOrderQuery on s2.MaintenanceWorkOrderId equals
                                           o3.EccpMaintenanceWorkOrderId into j3
                                       from s3 in j3.DefaultIfEmpty()
                                       join o4 in filteredEccpBaseElevators on s3.EccpElevatorId equals o4.Id into j4
                                       from s4 in j4.DefaultIfEmpty()
                                       join o5 in filteredEccpElevatorQrCodes on s4.Id equals o5.ElevatorId into j5
                                       from s5 in j5.DefaultIfEmpty()
                                       join o6 in filteredEccpBasePropertyCompanies on s4.ECCPBasePropertyCompanyId
                                           equals o6.Id into j6
                                       from s6 in j6.DefaultIfEmpty()
                                       select new
                                                  {
                                                      o.EccpMaintenanceWorkLog,
                                                      EccpMaintenanceCompanyName =
                                                          s4.Name == null ? string.Empty :
                                                          s4.ECCPBaseMaintenanceCompany == null ? string.Empty :
                                                          s4.ECCPBaseMaintenanceCompany.Name,
                                                      EccpPropertyCompanyName = s6 == null ? string.Empty : s6.Name,
                                                      EccpBaseElevatorNum =
                                                          s5 == null ? string.Empty : s5.AreaName + s5.ElevatorNum,
                                                      EccpMaintenanceUserNameList =
                                                          s3 == null ? null : s3.EccpMaintenanceUserNameList
                                                  };

                    totalCount = workLogQuery.Count();

                    eccpMaintenanceWorkLogs = new List<GetEccpMaintenanceWorkLogForView>();

                    workLogQuery.OrderBy(input.Sorting ?? "eccpMaintenanceWorkLog.id asc").PageBy(input)
                        .MapTo(eccpMaintenanceWorkLogs);

                    return new PagedResultDto<GetEccpMaintenanceWorkLogForView>(totalCount, eccpMaintenanceWorkLogs);
                }
            }
        }

        /// <summary>
        /// The get eccp maintenance work log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkLogs_Edit)]
        public async Task<GetEccpMaintenanceWorkLogForEditOutput> GetEccpMaintenanceWorkLogForEdit(
            EntityDto<long> input)
        {
            var eccpMaintenanceWorkLog = await this._eccpMaintenanceWorkLogRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpMaintenanceWorkLogForEditOutput
                             {
                                 EccpMaintenanceWorkLog =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenanceWorkLogDto>(
                                         eccpMaintenanceWorkLog)
                             };

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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkLogs_Create)]
        private async Task Create(CreateOrEditEccpMaintenanceWorkLogDto input)
        {
            var eccpMaintenanceWorkLog = this.ObjectMapper.Map<EccpMaintenanceWorkLog>(input);

            if (this.AbpSession.TenantId != null)
            {
                eccpMaintenanceWorkLog.TenantId = (int)this.AbpSession.TenantId;
            }

            await this._eccpMaintenanceWorkLogRepository.InsertAsync(eccpMaintenanceWorkLog);
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkLogs_Edit)]
        private async Task Update(CreateOrEditEccpMaintenanceWorkLogDto input)
        {
            if (input.Id != null)
            {
                var eccpMaintenanceWorkLog =
                    await this._eccpMaintenanceWorkLogRepository.FirstOrDefaultAsync((long)input.Id);
                this.ObjectMapper.Map(input, eccpMaintenanceWorkLog);
            }
        }
    }
}