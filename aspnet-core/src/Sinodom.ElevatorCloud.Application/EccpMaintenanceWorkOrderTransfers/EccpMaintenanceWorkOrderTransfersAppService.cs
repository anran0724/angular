// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrderTransfersAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrderTransfers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using Abp;
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
    using Sinodom.ElevatorCloud.Notifications;

    /// <summary>
    /// The eccp maintenance work order transfers app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers)]
    public class EccpMaintenanceWorkOrderTransfersAppService : ElevatorCloudAppServiceBase,
                                                               IEccpMaintenanceWorkOrderTransfersAppService
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
        /// The _eccp maintenance work order maintenance user link.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder_MaintenanceUser_Link, long> _eccpMaintenanceWorkOrderMaintenanceUserLink;

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
        /// The _user repository.
        /// </summary>
        private readonly IRepository<User, long> _userRepository;
        private readonly IAppNotifier _appNotifier;
        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkOrderTransfersAppService"/> class.
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
        /// <param name="eccpMaintenanceWorkOrderMaintenanceUserLink">
        /// The eccp maintenance work order maintenance user link.
        /// </param>
        /// <param name="eccpMaintenanceTempWorkOrderTransferAuditLogRepository">
        /// The eccp maintenance temp work order transfer audit log repository.
        /// </param>
        public EccpMaintenanceWorkOrderTransfersAppService(
            IRepository<EccpMaintenanceTempWorkOrderTransfer> eccpMaintenanceTempWorkOrderTransferRepository,
            IRepository<EccpMaintenanceWorkOrderTransfer> eccpMaintenanceWorkOrderTransferRepository,
            IRepository<EccpMaintenanceWorkOrder> eccpMaintenanceWorkOrderRepository,
            IRepository<EccpMaintenanceTempWorkOrder, Guid> eccpMaintenanceTempWorkOrderRepository,
            IRepository<EccpMaintenanceWorkOrderTransferAuditLog, int> eccpMaintenanceWorkOrderTransferAuditLogRepository,
            IRepository<User, long> userRepository,
            IRepository<EccpMaintenanceWorkOrder_MaintenanceUser_Link, long> eccpMaintenanceWorkOrderMaintenanceUserLink,
            IAppNotifier appNotifier,
            IRepository<EccpMaintenanceTempWorkOrderTransferAuditLog, int> eccpMaintenanceTempWorkOrderTransferAuditLogRepository)
        {
            this._eccpMaintenanceTempWorkOrderTransferRepository = eccpMaintenanceTempWorkOrderTransferRepository;
            this._eccpMaintenanceWorkOrderTransferRepository = eccpMaintenanceWorkOrderTransferRepository;
            this._eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
            this._eccpMaintenanceTempWorkOrderRepository = eccpMaintenanceTempWorkOrderRepository;
            this._eccpMaintenanceWorkOrderTransferAuditLogRepository =
                eccpMaintenanceWorkOrderTransferAuditLogRepository;
            this._userRepository = userRepository;
            this._eccpMaintenanceWorkOrderMaintenanceUserLink = eccpMaintenanceWorkOrderMaintenanceUserLink;
            this._eccpMaintenanceTempWorkOrderTransferAuditLogRepository =
                eccpMaintenanceTempWorkOrderTransferAuditLogRepository;
            _appNotifier = appNotifier;
        }

        /// <summary>
        /// 工单转接审批
        /// </summary>
        /// <param name="input">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers_Audit)]
        public int AuditMaintenanceWorkOrderTransfer(EccpMaintenanceWorkOrderTransfersForAuditOutput input)
        {
            if (input.Category == "1")
            {
                return this.MaintenanceWorkOrderTransfer(input);
            }

            if (input.Category == "2")
            {
                return this.MaintenanceTempWorkOrderTransfer(input);
            }

            return 0;
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
        public async Task<PagedResultDto<GetAllEccpMaintenanceWorkOrderTransferForView>> GetAll(
            GetAllEccpMaintenanceWorkOrderTransfersInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpMaintenanceTempWorkOrderTransfers = this._eccpMaintenanceTempWorkOrderTransferRepository
                    .GetAll().Where(w => w.TenantId == this.AbpSession.TenantId);

                var eccpMaintenanceWorkOrderTransfers = this._eccpMaintenanceWorkOrderTransferRepository.GetAll()
                    .Where(w => w.TenantId == this.AbpSession.TenantId);

                var eccpMaintenanceTempWorkOrders = this._eccpMaintenanceTempWorkOrderRepository.GetAll()
                    .Where(w => w.TenantId == this.AbpSession.TenantId);

                var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                    .Where(w => w.TenantId == this.AbpSession.TenantId);

                var users = this._userRepository.GetAll();
                var maintenanceTempWorkOrderTransfers =
                    from eccpMaintenanceTempWorkOrderTransfer in eccpMaintenanceTempWorkOrderTransfers
                    join eccpMaintenanceTempWorkOrder in eccpMaintenanceTempWorkOrders on
                        eccpMaintenanceTempWorkOrderTransfer.MaintenanceTempWorkOrderId equals
                        eccpMaintenanceTempWorkOrder.Id
                    join creatorUser in users on eccpMaintenanceTempWorkOrderTransfer.CreatorUserId equals
                        creatorUser.Id
                    join transferUser in users on eccpMaintenanceTempWorkOrderTransfer.TransferUserId equals
                        transferUser.Id
                    select new GetAllEccpMaintenanceWorkOrderTransferForView
                               {
                                   Id = eccpMaintenanceTempWorkOrderTransfer.Id,
                                   Title = eccpMaintenanceTempWorkOrder.Title,
                                   OrderTypeName = eccpMaintenanceTempWorkOrder.TempWorkOrderType.Name,
                                   OrderCreationTime = eccpMaintenanceTempWorkOrder.CreationTime,
                                   ApplicationTransferName = creatorUser.Name,
                                   TransferUserName = transferUser.Name,
                                   IsApproved = eccpMaintenanceTempWorkOrderTransfer.IsApproved,
                                   ApplicationTransferCreationTime = eccpMaintenanceTempWorkOrderTransfer.CreationTime,
                                   Category = "2"
                               };

                var maintenanceWorkOrderTransfers =
                    from eccpMaintenanceWorkOrderTransfer in eccpMaintenanceWorkOrderTransfers
                    join eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders on
                        eccpMaintenanceWorkOrderTransfer.MaintenanceWorkOrderId equals eccpMaintenanceWorkOrder.Id
                    join creatorUser in users on eccpMaintenanceWorkOrderTransfer.CreatorUserId equals creatorUser.Id
                    join transferUser in users on eccpMaintenanceWorkOrderTransfer.TransferUserId equals transferUser.Id
                    select new GetAllEccpMaintenanceWorkOrderTransferForView
                               {
                                   Id = eccpMaintenanceWorkOrderTransfer.Id,
                                   Title = eccpMaintenanceWorkOrder.MaintenanceType.Name,
                                   OrderTypeName = eccpMaintenanceWorkOrder.MaintenanceType.Name,
                                   OrderCreationTime = eccpMaintenanceWorkOrder.CreationTime,
                                   ApplicationTransferName = creatorUser.Name,
                                   TransferUserName = transferUser.Name,
                                   IsApproved = eccpMaintenanceWorkOrderTransfer.IsApproved,
                                   ApplicationTransferCreationTime = eccpMaintenanceWorkOrderTransfer.CreationTime,
                                   Category = "1"
                               };

                var query = maintenanceTempWorkOrderTransfers.Concat(maintenanceWorkOrderTransfers).WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Title.Contains(input.Filter) || e.ApplicationTransferName.Contains(input.Filter)
                         || e.TransferUserName.Contains(input.Filter));

                var totalCount = await query.CountAsync();

                var eccpMaintenanceWorkOrderTransfersForViews =
                    await query.OrderBy(input.Sorting ?? "Id asc").PageBy(input).ToListAsync();
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

                return new PagedResultDto<GetAllEccpMaintenanceWorkOrderTransferForView>(
                    totalCount,
                    eccpMaintenanceWorkOrderTransfersForViews);
            }
        }

        /// <summary>
        /// 临时工单转接处理
        /// </summary>
        /// <param name="input">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int MaintenanceTempWorkOrderTransfer(EccpMaintenanceWorkOrderTransfersForAuditOutput input)
        {
            if (input.IsApproved == null)
            {
                return 0;
            }

            var maintenanceTempWorkOrderTransfer = this._eccpMaintenanceTempWorkOrderTransferRepository.GetAll()
                .FirstOrDefault(w => w.Id == input.Id);
            if (maintenanceTempWorkOrderTransfer == null)
            {
                return 0;
            }

            if (maintenanceTempWorkOrderTransfer.IsApproved != null)
            {
                return 2;
            }

            // 添加临时工单审批日志
            var maintenanceTempWorkOrderTransferAuditLog = new EccpMaintenanceTempWorkOrderTransferAuditLog
                                                               {
                                                                   Remark = input.Remark,
                                                                   IsApproved = input.IsApproved.Value,
                                                                   MaintenanceTempWorkOrderTransferId =
                                                                       maintenanceTempWorkOrderTransfer.Id
                                                               };
            this._eccpMaintenanceTempWorkOrderTransferAuditLogRepository.Insert(
                maintenanceTempWorkOrderTransferAuditLog);

            // 更改
            maintenanceTempWorkOrderTransfer.IsApproved = input.IsApproved.Value;
            maintenanceTempWorkOrderTransfer.Remark = input.Remark;
            this._eccpMaintenanceTempWorkOrderTransferRepository.Update(maintenanceTempWorkOrderTransfer);

            if (!input.IsApproved.Value)
            {
                UserIdentifier user = new UserIdentifier(maintenanceTempWorkOrderTransfer.TenantId, maintenanceTempWorkOrderTransfer.TransferUserId);
                _appNotifier.AuditSuccess(user);
                UserIdentifier users = new UserIdentifier(maintenanceTempWorkOrderTransfer.TenantId, maintenanceTempWorkOrderTransfer.CreatorUserId.Value);
                _appNotifier.AuditSuccess(users);
                return 1;
            }

            var maintenanceTempWorkOrder = this._eccpMaintenanceTempWorkOrderRepository.FirstOrDefault(
                w => w.Id == maintenanceTempWorkOrderTransfer.MaintenanceTempWorkOrderId);
            if (maintenanceTempWorkOrder == null)
            {
                return 3;
            }

            if (maintenanceTempWorkOrder.CheckState == 1)
            {
                UserIdentifier user = new UserIdentifier(maintenanceTempWorkOrderTransfer.TenantId, maintenanceTempWorkOrderTransfer.CreatorUserId.Value);
                _appNotifier.AuditFailure(user);
                return 4;
            }

            maintenanceTempWorkOrder.UserId = maintenanceTempWorkOrderTransfer.TransferUserId;
            this._eccpMaintenanceTempWorkOrderRepository.Update(maintenanceTempWorkOrder);
            return 1;
        }

        /// <summary>
        /// </summary>
        /// <param name="input">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int MaintenanceWorkOrderTransfer(EccpMaintenanceWorkOrderTransfersForAuditOutput input)
        {
            if (input.IsApproved == null)
            {
                return 0;
            }

            var eccpMaintenanceWorkOrderTransfer = this._eccpMaintenanceWorkOrderTransferRepository.GetAll()
                .FirstOrDefault(w => w.Id == input.Id);

            if (eccpMaintenanceWorkOrderTransfer == null)
            {
                return 0;
            }

            if (eccpMaintenanceWorkOrderTransfer.IsApproved != null)
            {
                return 2;
            }

            var eccpMaintenanceWorkOrder = this._eccpMaintenanceWorkOrderRepository.GetAll()
                .FirstOrDefault(w => w.Id == eccpMaintenanceWorkOrderTransfer.MaintenanceWorkOrderId);
            if (eccpMaintenanceWorkOrder == null)
            {
                return 3;
            }

            if (eccpMaintenanceWorkOrder.IsClosed || eccpMaintenanceWorkOrder.IsComplete)
            {
                return 4;
            }

            var eccpMaintenanceWorkOrderTransferAuditLog = new EccpMaintenanceWorkOrderTransferAuditLog
                                                               {
                                                                   Remark = input.Remark,
                                                                   IsApproved = input.IsApproved.Value,
                                                                   MaintenanceWorkOrderTransferId =
                                                                       eccpMaintenanceWorkOrderTransfer.Id
                                                               };

            // 添加维保工单审批日志
            this._eccpMaintenanceWorkOrderTransferAuditLogRepository.Insert(eccpMaintenanceWorkOrderTransferAuditLog);

            // 修改维保工单申请状态
            eccpMaintenanceWorkOrderTransfer.IsApproved = input.IsApproved.Value;
            eccpMaintenanceWorkOrderTransfer.Remark = input.Remark;
            this._eccpMaintenanceWorkOrderTransferRepository.Update(eccpMaintenanceWorkOrderTransfer);

            // 变更维保工单负责人
            if (input.IsApproved.Value)
            {
                return 1;
            }

            var addUserId = eccpMaintenanceWorkOrderTransfer.TransferUserId;
            var delUserId = eccpMaintenanceWorkOrderTransfer.CreatorUserId.GetValueOrDefault(0);

            // todo  MaintenancePlanId  需要改为工单Id
            var eccpMaintenanceWorkOrderMaintenanceUserLink = this._eccpMaintenanceWorkOrderMaintenanceUserLink.GetAll()
                .FirstOrDefault(
                    w => w.UserId == delUserId
                         && w.MaintenancePlanId == eccpMaintenanceWorkOrderTransfer.MaintenanceWorkOrderId);
            if (eccpMaintenanceWorkOrderMaintenanceUserLink != null)
            {
                this._eccpMaintenanceWorkOrderMaintenanceUserLink.Delete(eccpMaintenanceWorkOrderMaintenanceUserLink);
            }

            var createMaintenanceWorkOrderMaintenanceUserLink = new EccpMaintenanceWorkOrder_MaintenanceUser_Link
                                                                    {
                                                                        TenantId =
                                                                            eccpMaintenanceWorkOrderTransfer.TenantId,
                                                                        UserId = addUserId,
                                                                        MaintenancePlanId =
                                                                            eccpMaintenanceWorkOrderTransfer
                                                                                .MaintenanceWorkOrderId
                                                                    };

            // todo  MaintenancePlanId  需要改为工单Id
            this._eccpMaintenanceWorkOrderMaintenanceUserLink.Insert(createMaintenanceWorkOrderMaintenanceUserLink);
            return 1;
        }

       

        /// <summary>
        /// 维保工单转接申请接口
        /// </summary>
        /// <param name="input">
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<int> ApplyWorkOrderTransfer(ApplyEccpMaintenanceWorkOrderTransferDto input)
        {
          
            var eccpMaintenanceWorkOrderTransfer =
                await this._eccpMaintenanceWorkOrderTransferRepository.FirstOrDefaultAsync(
                    e => e.MaintenanceWorkOrderId == input.MaintenanceWorkOrderId && e.IsApproved == null);
            if (eccpMaintenanceWorkOrderTransfer == null)
            {
                return  -1; // 此工单已申请转接，还未审批
            }

            var existence = _eccpMaintenanceWorkOrderMaintenanceUserLink.GetAll().Count(w =>
                w.MaintenancePlanId == input.MaintenanceWorkOrderId && w.UserId == input.TransferUserId);
            if (existence > 0)
            {
                return 2; // 此人也是工单负责人
            }

            EccpMaintenanceWorkOrderTransfer workOrderTransfer = new EccpMaintenanceWorkOrderTransfer
            {
                Remark = input.Remark,
                TransferUserId = input.TransferUserId,
                MaintenanceWorkOrderId = input.MaintenanceWorkOrderId,
            };
            await this._eccpMaintenanceWorkOrderTransferRepository.InsertAsync(workOrderTransfer);
            
            return 1;
        }



        /// <summary>        
        ///  维保临时工单转接申请接口
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<int> ApplyTempWorkOrderTransfer(ApplyEccpMaintenanceTempWorkOrderTransferDto input)
        {
            var eccpMaintenanceTempWorkOrderTransfer =
                await this._eccpMaintenanceTempWorkOrderTransferRepository.FirstOrDefaultAsync(
                    e => e.MaintenanceTempWorkOrderId == input.MaintenanceTempWorkOrderId && e.IsApproved == null);
            if (eccpMaintenanceTempWorkOrderTransfer != null)
            {
                return -1;
            }

            if (input.TransferUserId == AbpSession.UserId)
            {
                return -2;
            }
            EccpMaintenanceTempWorkOrderTransfer maintenanceTempWorkOrderTransfer =
                new EccpMaintenanceTempWorkOrderTransfer
                {
                    MaintenanceTempWorkOrderId = input.MaintenanceTempWorkOrderId,
                    TransferUserId = input.TransferUserId,
                    Remark = input.Remark
                };
            await this._eccpMaintenanceTempWorkOrderTransferRepository.InsertAsync(maintenanceTempWorkOrderTransfer);
            return 1;
        }


    }
}