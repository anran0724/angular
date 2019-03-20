// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrderTransferAuditLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrderTransfers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Linq.Extensions;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceTransfers;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrderTransfers.Dtos;

    /// <summary>
    /// The eccp maintenance work order transfer audit logs app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers)]
    public class EccpMaintenanceWorkOrderTransferAuditLogsAppService : ElevatorCloudAppServiceBase,
                                                                       IEccpMaintenanceWorkOrderTransferAuditLogsAppService
    {
        /// <summary>
        /// The _eccp maintenance temp work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTempWorkOrder, Guid> _eccpMaintenanceTempWorkOrderRepository;

        /// <summary>
        /// The _eccp maintenance temp work order transfer audit log repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTempWorkOrderTransferAuditLog, int> _eccpMaintenanceTempWorkOrderTransferAuditLogRepository;

        /// <summary>
        /// The _eccp maintenance temp work order transfer repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTempWorkOrderTransfer> _eccpMaintenanceTempWorkOrderTransferRepository;

        /// <summary>
        /// The _eccp maintenance work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder> _eccpMaintenanceWorkOrderRepository;

        /// <summary>
        /// The _eccp maintenance work order transfer audit log repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrderTransferAuditLog, int> _eccpMaintenanceWorkOrderTransferAuditLogRepository;

        /// <summary>
        /// The _eccp maintenance work order transfer repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrderTransfer> _eccpMaintenanceWorkOrderTransferRepository;

        /// <summary>
        /// The user repository.
        /// </summary>
        private readonly IRepository<User, long> _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkOrderTransferAuditLogsAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceTempWorkOrderTransferRepository">
        /// The eccp maintenance temp work order transfer repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderTransferRepository">
        /// The eccp maintenance work order transfer repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderRepository">
        /// The eccp maintenance work order repository.
        /// </param>
        /// <param name="eccpMaintenanceTempWorkOrderRepository">
        /// The eccp maintenance temp work order repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderTransferAuditLogRepository">
        /// The eccp maintenance work order transfer audit log repository.
        /// </param>
        /// <param name="userRepository">
        /// The user repository.
        /// </param>
        /// <param name="eccpMaintenanceTempWorkOrderTransferAuditLogRepository">
        /// The eccp maintenance temp work order transfer audit log repository.
        /// </param>
        public EccpMaintenanceWorkOrderTransferAuditLogsAppService(
            IRepository<EccpMaintenanceTempWorkOrderTransfer> eccpMaintenanceTempWorkOrderTransferRepository,
            IRepository<EccpMaintenanceWorkOrderTransfer> eccpMaintenanceWorkOrderTransferRepository,
            IRepository<EccpMaintenanceWorkOrder> eccpMaintenanceWorkOrderRepository,
            IRepository<EccpMaintenanceTempWorkOrder, Guid> eccpMaintenanceTempWorkOrderRepository,
            IRepository<EccpMaintenanceWorkOrderTransferAuditLog, int> eccpMaintenanceWorkOrderTransferAuditLogRepository,
            IRepository<User, long> userRepository,
            IRepository<EccpMaintenanceTempWorkOrderTransferAuditLog, int> eccpMaintenanceTempWorkOrderTransferAuditLogRepository)
        {
            this._eccpMaintenanceTempWorkOrderTransferRepository = eccpMaintenanceTempWorkOrderTransferRepository;
            this._eccpMaintenanceWorkOrderTransferRepository = eccpMaintenanceWorkOrderTransferRepository;
            this._eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
            this._eccpMaintenanceTempWorkOrderRepository = eccpMaintenanceTempWorkOrderRepository;
            this._eccpMaintenanceWorkOrderTransferAuditLogRepository =
                eccpMaintenanceWorkOrderTransferAuditLogRepository;
            this._userRepository = userRepository;
            this._eccpMaintenanceTempWorkOrderTransferAuditLogRepository =
                eccpMaintenanceTempWorkOrderTransferAuditLogRepository;
        }

        /// <summary>
        /// The get maintenance work order transfer audit logs.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<GetAllEccpMaintenanceWorkOrderTransferAuditLogForView>> GetMaintenanceWorkOrderTransferAuditLogs(GetAllEccpMaintenanceTempWorkOrderTransfersAuditLogInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var users = this._userRepository.GetAll();
                IQueryable<GetAllEccpMaintenanceWorkOrderTransferAuditLogForView> maintenanceTempWorkOrderTransfers;

                if (input.Category == "2")
                {
                    var eccpMaintenanceTempWorkOrderTransfers = this._eccpMaintenanceTempWorkOrderTransferRepository
                        .GetAll().Where(w => w.TenantId == this.AbpSession.TenantId && w.Id == input.Id);
                    var eccpMaintenanceTempWorkOrders = this._eccpMaintenanceTempWorkOrderRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId);
                    var eccpMaintenanceTempWorkOrderTransferAuditLogs =
                        this._eccpMaintenanceTempWorkOrderTransferAuditLogRepository.GetAll();
                    maintenanceTempWorkOrderTransfers =
                        from eccpMaintenanceTempWorkOrderTransfer in eccpMaintenanceTempWorkOrderTransfers
                        join eccpMaintenanceTempWorkOrder in eccpMaintenanceTempWorkOrders on
                            eccpMaintenanceTempWorkOrderTransfer.MaintenanceTempWorkOrderId equals
                            eccpMaintenanceTempWorkOrder.Id
                        join creatorUser in users on eccpMaintenanceTempWorkOrderTransfer.CreatorUserId equals
                            creatorUser.Id
                        join transferUser in users on eccpMaintenanceTempWorkOrderTransfer.TransferUserId equals
                            transferUser.Id
                        join eccpMaintenanceTempWorkOrderTransferAuditLog in
                            eccpMaintenanceTempWorkOrderTransferAuditLogs on eccpMaintenanceTempWorkOrderTransfer.Id
                            equals eccpMaintenanceTempWorkOrderTransferAuditLog.MaintenanceTempWorkOrderTransferId
                        join auditUser in users on eccpMaintenanceTempWorkOrderTransferAuditLog.CreatorUserId equals
                            auditUser.Id
                        select new GetAllEccpMaintenanceWorkOrderTransferAuditLogForView
                                   {
                                       Id = eccpMaintenanceTempWorkOrderTransfer.Id,
                                       Title = eccpMaintenanceTempWorkOrder.Title,
                                       OrderTypeName = eccpMaintenanceTempWorkOrder.TempWorkOrderType.Name,
                                       OrderCreationTime = eccpMaintenanceTempWorkOrder.CreationTime,
                                       ApplicationTransferName = creatorUser.Name,
                                       TransferUserName = transferUser.Name,
                                       IsApproved = eccpMaintenanceTempWorkOrderTransfer.IsApproved,
                                       ApplicationTransferCreationTime =
                                           eccpMaintenanceTempWorkOrderTransfer.CreationTime,
                                       Category = "2",
                                       AuditTime = eccpMaintenanceTempWorkOrderTransferAuditLog.CreationTime,
                                       AuditUserName = auditUser.UserName
                                   };
                }
                else
                {
                    var eccpMaintenanceWorkOrderTransfers = this._eccpMaintenanceWorkOrderTransferRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId && w.Id == input.Id);
                    var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId);
                    var eccpMaintenanceWorkOrderTransferAuditLogs =
                        this._eccpMaintenanceWorkOrderTransferAuditLogRepository.GetAll();
                    maintenanceTempWorkOrderTransfers =
                        from eccpMaintenanceWorkOrderTransfer in eccpMaintenanceWorkOrderTransfers
                        join eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders on
                            eccpMaintenanceWorkOrderTransfer.MaintenanceWorkOrderId equals eccpMaintenanceWorkOrder.Id
                        join creatorUser in users on eccpMaintenanceWorkOrderTransfer.CreatorUserId equals
                            creatorUser.Id
                        join transferUser in users on eccpMaintenanceWorkOrderTransfer.TransferUserId equals
                            transferUser.Id
                        join eccpMaintenanceWorkOrderTransferAuditLog in eccpMaintenanceWorkOrderTransferAuditLogs on
                            eccpMaintenanceWorkOrderTransfer.Id equals
                            eccpMaintenanceWorkOrderTransferAuditLog.MaintenanceWorkOrderTransferId
                        join auditUser in users on eccpMaintenanceWorkOrderTransferAuditLog.CreatorUserId equals
                            auditUser.Id
                        select new GetAllEccpMaintenanceWorkOrderTransferAuditLogForView
                                   {
                                       Id = eccpMaintenanceWorkOrderTransfer.Id,
                                       Title = eccpMaintenanceWorkOrder.MaintenanceType.Name,
                                       OrderTypeName = eccpMaintenanceWorkOrder.MaintenanceType.Name,
                                       OrderCreationTime = eccpMaintenanceWorkOrder.CreationTime,
                                       ApplicationTransferName = creatorUser.Name,
                                       TransferUserName = transferUser.Name,
                                       IsApproved = eccpMaintenanceWorkOrderTransfer.IsApproved,
                                       ApplicationTransferCreationTime = eccpMaintenanceWorkOrderTransfer.CreationTime,
                                       Category = "1",
                                       AuditTime = eccpMaintenanceWorkOrderTransferAuditLog.CreationTime,
                                       AuditUserName = auditUser.UserName
                                   };
                }

                var totalCount = await maintenanceTempWorkOrderTransfers.CountAsync();

                var eccpMaintenanceWorkOrderTransfersForViews = await maintenanceTempWorkOrderTransfers
                                                                    .OrderBy(input.Sorting ?? "Id asc").PageBy(input)
                                                                    .ToListAsync();
                foreach (var eccpMaintenanceWorkOrderTransfersForView in eccpMaintenanceWorkOrderTransfersForViews)
                {
                    if (eccpMaintenanceWorkOrderTransfersForView.IsApproved == true)
                    {
                        eccpMaintenanceWorkOrderTransfersForView.WorkOrderTransferAuditState = this.L("AuditPass");
                    }
                    else if (eccpMaintenanceWorkOrderTransfersForView.IsApproved == false)
                    {
                        eccpMaintenanceWorkOrderTransfersForView.WorkOrderTransferAuditState = this.L("AuditFailed");
                    }
                    else
                    {
                        eccpMaintenanceWorkOrderTransfersForView.WorkOrderTransferAuditState = this.L("ToBeAudited");
                    }

                    eccpMaintenanceWorkOrderTransfersForView.WorkOrderTransferType = this.L(eccpMaintenanceWorkOrderTransfersForView.Category == "1" ? "MaintenanceWorkOrder" : "MaintenanceTempWorkOrder");
                }

                return new PagedResultDto<GetAllEccpMaintenanceWorkOrderTransferAuditLogForView>(
                    totalCount,
                    eccpMaintenanceWorkOrderTransfersForViews);
            }
        }
    }
}