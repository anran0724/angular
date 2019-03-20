// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrderManager.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Domain.Repositories;
    using Abp.Domain.Services;
    using Abp.Domain.Uow;
    using Abp.Runtime.Session;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;

    /// <summary>
    /// The eccp maintenance work order manager.
    /// </summary>
    public class EccpMaintenanceWorkOrderManager : IDomainService
    {
        /// <summary>
        /// The _eccp dict maintenance status repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceStatus, int> _eccpDictMaintenanceStatusRepository;

        /// <summary>
        /// The _eccp dict maintenance type repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceType, int> _eccpDictMaintenanceTypeRepository;

        /// <summary>
        /// The _eccp maintenance contract elevator link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract_Elevator_Link, long> _eccpMaintenanceContractElevatorLinkRepository;

        /// <summary>
        /// The _eccp maintenance contract repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract, long> _eccpMaintenanceContractRepository;

        /// <summary>
        /// The _eccp maintenance plan maintenance user link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> _eccpMaintenancePlanMaintenanceUserLinkRepository;

        /// <summary>
        /// The _eccp maintenance plan property user link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan_PropertyUser_Link, long> _eccpMaintenancePlanPropertyUserLinkRepository;

        /// <summary>
        /// The _eccp maintenance plan repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan, int> _eccpMaintenancePlanRepository;

        /// <summary>
        /// The _eccp maintenance work order maintenance user link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder_MaintenanceUser_Link, long> _eccpMaintenanceWorkOrderMaintenanceUserLinkRepository;

        /// <summary>
        /// The _eccp maintenance work order property user link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder_PropertyUser_Link, long> _eccpMaintenanceWorkOrderPropertyUserLinkRepository;

        /// <summary>
        /// The _eccp maintenance work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder> _eccpMaintenanceWorkOrderRepository;

        /// <summary>
        /// The _unit of work manager.
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkOrderManager"/> class.
        /// </summary>
        /// <param name="unitOfWorkManager">
        /// The unit of work manager.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderRepository">
        /// The eccp maintenance work order repository.
        /// </param>
        /// <param name="eccpMaintenancePlanRepository">
        /// The eccp maintenance plan repository.
        /// </param>
        /// <param name="eccpDictMaintenanceTypeRepository">
        /// The eccp dict maintenance type repository.
        /// </param>
        /// <param name="eccpDictMaintenanceStatusRepository">
        /// The eccp dict maintenance status repository.
        /// </param>
        /// <param name="eccpMaintenanceContractRepository">
        /// The eccp maintenance contract repository.
        /// </param>
        /// <param name="eccpMaintenanceContractElevatorLinkRepository">
        /// The eccp maintenance contract elevator link repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderMaintenanceUserLinkRepository">
        /// The eccp maintenance work order maintenance user link repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderPropertyUserLinkRepository">
        /// The eccp maintenance work order property user link repository.
        /// </param>
        /// <param name="eccpMaintenancePlanMaintenanceUserLinkRepository">
        /// The eccp maintenance plan maintenance user link repository.
        /// </param>
        /// <param name="eccpMaintenancePlanPropertyUserLinkRepository">
        /// The eccp maintenance plan property user link repository.
        /// </param>
        public EccpMaintenanceWorkOrderManager(
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<EccpMaintenanceWorkOrder> eccpMaintenanceWorkOrderRepository,
            IRepository<EccpMaintenancePlan, int> eccpMaintenancePlanRepository,
            IRepository<EccpDictMaintenanceType, int> eccpDictMaintenanceTypeRepository,
            IRepository<EccpDictMaintenanceStatus, int> eccpDictMaintenanceStatusRepository,
            IRepository<EccpMaintenanceContract, long> eccpMaintenanceContractRepository,
            IRepository<EccpMaintenanceContract_Elevator_Link, long> eccpMaintenanceContractElevatorLinkRepository,
            IRepository<EccpMaintenanceWorkOrder_MaintenanceUser_Link, long> eccpMaintenanceWorkOrderMaintenanceUserLinkRepository,
            IRepository<EccpMaintenanceWorkOrder_PropertyUser_Link, long> eccpMaintenanceWorkOrderPropertyUserLinkRepository,
            IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> eccpMaintenancePlanMaintenanceUserLinkRepository,
            IRepository<EccpMaintenancePlan_PropertyUser_Link, long> eccpMaintenancePlanPropertyUserLinkRepository)
        {
            this.AbpSession = NullAbpSession.Instance;
            this._unitOfWorkManager = unitOfWorkManager;
            this._eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
            this._eccpMaintenancePlanRepository = eccpMaintenancePlanRepository;
            this._eccpDictMaintenanceTypeRepository = eccpDictMaintenanceTypeRepository;
            this._eccpDictMaintenanceStatusRepository = eccpDictMaintenanceStatusRepository;
            this._eccpMaintenanceContractRepository = eccpMaintenanceContractRepository;
            this._eccpMaintenanceContractElevatorLinkRepository = eccpMaintenanceContractElevatorLinkRepository;
            this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository =
                eccpMaintenanceWorkOrderMaintenanceUserLinkRepository;
            this._eccpMaintenanceWorkOrderPropertyUserLinkRepository =
                eccpMaintenanceWorkOrderPropertyUserLinkRepository;
            this._eccpMaintenancePlanMaintenanceUserLinkRepository = eccpMaintenancePlanMaintenanceUserLinkRepository;
            this._eccpMaintenancePlanPropertyUserLinkRepository = eccpMaintenancePlanPropertyUserLinkRepository;
        }

        /// <summary>
        /// Gets or sets the abp session.
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// The completion work rearrange work order.
        /// 工作完成计划工单刷新
        /// </summary>
        /// <param name="eccpMaintenanceWorkOrderId">
        /// The eccp maintenance work order id.
        /// 任务id
        /// </param>
        /// <param name="completionTime">
        /// The completion time.
        /// 任务完成时间
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CompletionWorkRearrangeWorkOrder(int eccpMaintenanceWorkOrderId, DateTime completionTime)
        {
            using (this._unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var resultCount = 0;

                // 工单
                var eccpMaintenanceWorkOrder =
                    this._eccpMaintenanceWorkOrderRepository.FirstOrDefault(w => w.Id == eccpMaintenanceWorkOrderId);
                if (eccpMaintenanceWorkOrder == null)
                {
                    return -1;
                }

                // 计划
                var eccpMaintenancePlan =
                    this._eccpMaintenancePlanRepository.GetAll().FirstOrDefault(
                        w => w.Id == eccpMaintenanceWorkOrder.MaintenancePlanId);

                if (eccpMaintenancePlan == null)
                {
                    return -2;
                }
                if (eccpMaintenancePlan.IsClose || eccpMaintenancePlan.IsCancel)
                {
                    return 1;
                }

                if (eccpMaintenancePlan.PollingPeriod < 15)
                {
                    return -8;
                }

                // 通过计划电梯与和合同的关联关系找到合同
                var eccpMaintenanceContractElevatorLink =
                    this._eccpMaintenanceContractElevatorLinkRepository.GetAll().FirstOrDefault(
                        w => w.ElevatorId == eccpMaintenancePlan.ElevatorId);
                if (eccpMaintenanceContractElevatorLink == null)
                {
                    return -3;
                }

                if (eccpMaintenanceContractElevatorLink.MaintenanceContractId == null)
                {
                    return -4;
                }

                // 合同
                EccpMaintenanceContract eccpMaintenanceContract = this._eccpMaintenanceContractRepository.GetAll().FirstOrDefault(e => e.Id ==
                              eccpMaintenanceContractElevatorLink.MaintenanceContractId.Value);
                if (eccpMaintenanceContract == null)
                {
                    return -5;
                }


                // 初始化工单状态
                var eccpDictMaintenanceStatus =
                    this._eccpDictMaintenanceStatusRepository.GetAll().FirstOrDefault(w => w.Name == "未进行");
                if (eccpDictMaintenanceStatus == null)
                {
                    return -6;
                }

                // 初始维保类型
                var eccpDictMaintenanceType = this._eccpDictMaintenanceTypeRepository.GetAll().FirstOrDefault();
                if (eccpDictMaintenanceType == null)
                {
                    return -7;
                }

                var planCheckDate = completionTime;

                //是否存在未开始工单
                var notYetBegunCount =
                    this._eccpMaintenanceWorkOrderRepository.Count(w => 
                        w.MaintenancePlanId == eccpMaintenancePlan.Id && w.MaintenanceStatusId == eccpDictMaintenanceStatus.Id);
                if (notYetBegunCount > 0)
                {
                    return 1;
                }
                
                if (planCheckDate < eccpMaintenanceContract.EndDate)
                {
                    planCheckDate = planCheckDate.AddDays(eccpMaintenancePlan.PollingPeriod);
                    var order = new EccpMaintenanceWorkOrder
                    {
                        IsPassed = false,
                        MaintenancePlanId = eccpMaintenancePlan.Id,
                        PlanCheckDate = planCheckDate,
                        MaintenanceStatusId = eccpDictMaintenanceStatus.Id,
                        MaintenanceTypeId = eccpDictMaintenanceType.Id,
                        TenantId = eccpMaintenancePlan.TenantId
                    };
                    this._eccpMaintenanceWorkOrderRepository.InsertAndGetId(order);

                    // 计划维保人员
                    var eccpMaintenancePlanMaintenanceUserLinks = this._eccpMaintenancePlanMaintenanceUserLinkRepository
                        .GetAll().Where(w => w.MaintenancePlanId == order.MaintenancePlanId).ToList();

                    // 添加工单维保人员
                    eccpMaintenancePlanMaintenanceUserLinks.ForEach(
                        s => this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository.Insert(
                            new EccpMaintenanceWorkOrder_MaintenanceUser_Link
                            {
                                TenantId = s.TenantId,
                                UserId = s.UserId,
                                MaintenancePlanId = order.Id
                            }));

                    ////计划使用人员
                    var eccpMaintenancePlanPropertyUserLinks = this._eccpMaintenancePlanPropertyUserLinkRepository.GetAll()
                        .Where(w => w.MaintenancePlanId == order.MaintenancePlanId).ToList();

                    // 添加工单使用人员
                    eccpMaintenancePlanPropertyUserLinks.ForEach(
                        s => this._eccpMaintenanceWorkOrderPropertyUserLinkRepository.Insert(
                            new EccpMaintenanceWorkOrder_PropertyUser_Link
                            {
                                TenantId = s.TenantId,
                                UserId = s.UserId,
                                MaintenancePlanId = order.Id
                            }));
                    resultCount++;
                }

                return resultCount;
            }
        }

        /// <summary>
        /// The plan deleting rearrange work order.
        /// 计划删除计划工单刷新
        /// </summary>
        /// <param name="maintenancePlanId">
        /// The maintenance plan id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int PlanDeletingRearrangeWorkOrder(long maintenancePlanId)
        {
            var resultCount = 0;

            // 未进行
            var eccpDictMaintenanceStatus =
                this._eccpDictMaintenanceStatusRepository.GetAll().FirstOrDefault(w => w.Name == "未进行");
            // 进行中
            var eccpDictMaintenanceStatus1 =
                this._eccpDictMaintenanceStatusRepository.GetAll().FirstOrDefault(w => w.Name == "进行中");
            if (eccpDictMaintenanceStatus == null)
            {
                return -1;
            }

            // 计划
            var eccpMaintenancePlan =
                this._eccpMaintenancePlanRepository.FirstOrDefault(w => w.Id == maintenancePlanId);
            if (eccpMaintenancePlan == null)
            {
                return -2;
            }

            // 工单
            var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll().Where(
                    w => w.MaintenancePlanId == maintenancePlanId
                         && (eccpDictMaintenanceStatus.Id == w.MaintenanceStatusId || eccpDictMaintenanceStatus1.Id == w.MaintenanceStatusId))
                .ToList();

            foreach (var eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders)
            {
                this._eccpMaintenanceWorkOrderRepository.Delete(eccpMaintenanceWorkOrder);
                resultCount++;
            }

            return resultCount;
        }

        /// <summary>
        /// The plan modification rearrange work order.
        /// 计划修改和添加工单刷新
        /// </summary>
        /// <param name="maintenancePlanId">
        /// The maintenance plan id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public async Task<int> PlanModificationRearrangeWorkOrder(long maintenancePlanId)
        {
            var resultCount = 0;

            // 初始化工单状态
            var eccpDictMaintenanceStatus =
                await this._eccpDictMaintenanceStatusRepository.FirstOrDefaultAsync(w => w.Name == "未进行");
            if (eccpDictMaintenanceStatus == null)
            {
                return -1;
            }

            // 初始维保类型
            var eccpDictMaintenanceType = await this._eccpDictMaintenanceTypeRepository.GetAll().FirstOrDefaultAsync();
            if (eccpDictMaintenanceType == null)
            {
                return -2;
            }

            // 计划
            var eccpMaintenancePlan = await 
                this._eccpMaintenancePlanRepository.FirstOrDefaultAsync(w => w.Id == maintenancePlanId);
            if (eccpMaintenancePlan == null)
            {
                return -3;
            }

            if (eccpMaintenancePlan.PollingPeriod < 15)
            {
                return -8;
            }

            // 未进行工单删除
            var eccpMaintenanceWorkOrders = await this._eccpMaintenanceWorkOrderRepository.GetAll().Where(
                    w => w.MaintenancePlanId == maintenancePlanId
                         && eccpDictMaintenanceStatus.Id == w.MaintenanceStatusId)
                .ToListAsync();

            foreach (var eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders)
            {
                await this._eccpMaintenanceWorkOrderRepository.DeleteAsync(eccpMaintenanceWorkOrder);
            }

            // 最后一个发生变化的工单
            var lastWorkOrder = await this._eccpMaintenanceWorkOrderRepository.GetAll().OrderBy(o => o.PlanCheckDate)
                .FirstOrDefaultAsync(
                    w => w.MaintenancePlanId == maintenancePlanId
                         && eccpDictMaintenanceStatus.Id != w.MaintenanceStatusId);

            var planCheckDate = DateTime.Now;

            if (lastWorkOrder?.ComplateDate != null && planCheckDate < lastWorkOrder.ComplateDate.Value.AddDays(eccpMaintenancePlan.PollingPeriod))
            {
                planCheckDate = lastWorkOrder.ComplateDate.Value;
            }
            // 通过计划电梯与和合同的关联关系找到合同
            var eccpMaintenanceContractElevatorLink = await 
                this._eccpMaintenanceContractElevatorLinkRepository.FirstOrDefaultAsync(
                    w => w.ElevatorId == eccpMaintenancePlan.ElevatorId);
            if (eccpMaintenanceContractElevatorLink == null)
            {
                return -4;
            }

            if (eccpMaintenanceContractElevatorLink.MaintenanceContractId == null)
            {
                return -5;
            }

            // 合同
            EccpMaintenanceContract eccpMaintenanceContract;
            using (this._unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                eccpMaintenanceContract = await 
                    this._eccpMaintenanceContractRepository.FirstOrDefaultAsync(
                        eccpMaintenanceContractElevatorLink.MaintenanceContractId.Value);
                if (eccpMaintenanceContract == null)
                {
                    return -6;
                }
            }

            if (planCheckDate < eccpMaintenanceContract.EndDate)
            {
                planCheckDate = planCheckDate.AddDays(eccpMaintenancePlan.PollingPeriod);
                var order = new EccpMaintenanceWorkOrder
                                {
                                    IsPassed = false,
                                    MaintenancePlanId = eccpMaintenancePlan.Id,
                                    PlanCheckDate = planCheckDate,
                                    MaintenanceStatusId = eccpDictMaintenanceStatus.Id,
                                    MaintenanceTypeId = eccpDictMaintenanceType.Id,
                                    TenantId = eccpMaintenancePlan.TenantId
                                };
                await this._eccpMaintenanceWorkOrderRepository.InsertAndGetIdAsync(order);

                // 计划维保人员
                var eccpMaintenancePlanMaintenanceUserLinks = await this._eccpMaintenancePlanMaintenanceUserLinkRepository
                    .GetAll().Where(w => w.MaintenancePlanId == order.MaintenancePlanId).ToListAsync();

                // 添加工单维保人员
                eccpMaintenancePlanMaintenanceUserLinks.ForEach(
                    s => this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository.InsertAsync(
                        new EccpMaintenanceWorkOrder_MaintenanceUser_Link
                        {
                            TenantId = s.TenantId,
                            UserId = s.UserId,
                            MaintenancePlanId = order.Id
                        }));

                ////计划使用人员
                var eccpMaintenancePlanPropertyUserLinks = await this._eccpMaintenancePlanPropertyUserLinkRepository.GetAll()
                    .Where(w => w.MaintenancePlanId == order.MaintenancePlanId).ToListAsync();

                // 添加工单使用人员
                eccpMaintenancePlanPropertyUserLinks.ForEach(
                    s => this._eccpMaintenanceWorkOrderPropertyUserLinkRepository.InsertAsync(
                        new EccpMaintenanceWorkOrder_PropertyUser_Link
                        {
                            TenantId = s.TenantId,
                            UserId = s.UserId,
                            MaintenancePlanId = order.Id
                        }));
                resultCount++;
            }

            return resultCount;
        }
    }
}