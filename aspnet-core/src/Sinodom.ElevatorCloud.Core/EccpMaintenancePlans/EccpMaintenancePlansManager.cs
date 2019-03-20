// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenancePlansManager.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans
{
    using System;

    using Abp.Domain.Repositories;
    using Abp.Domain.Services;

    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;

    /// <summary>
    /// The eccp maintenance plans manager.
    /// </summary>
    public class EccpMaintenancePlansManager : IDomainService
    {
        /// <summary>
        /// The _eccp maintenance plan repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan> _eccpMaintenancePlanRepository;

        /// <summary>
        /// The _eccp maintenance work order manager.
        /// </summary>
        private readonly EccpMaintenanceWorkOrderManager _eccpMaintenanceWorkOrderManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenancePlansManager"/> class.
        /// </summary>
        /// <param name="eccpMaintenancePlanRepository">
        /// The eccp maintenance plan repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderManager">
        /// The eccp maintenance work order manager.
        /// </param>
        public EccpMaintenancePlansManager(
            IRepository<EccpMaintenancePlan> eccpMaintenancePlanRepository,
            EccpMaintenanceWorkOrderManager eccpMaintenanceWorkOrderManager)
        {
            this._eccpMaintenancePlanRepository = eccpMaintenancePlanRepository;
            this._eccpMaintenanceWorkOrderManager = eccpMaintenanceWorkOrderManager;
        }

        /// <summary>
        /// The close plan.
        /// 根据电梯ID关闭维保计划
        /// </summary>
        /// <param name="elevatorId">
        /// The elevator id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// The exception.
        /// </exception>
        public int ClosePlan(Guid? elevatorId)
        {
            var eccpMaintenancePlan =
                this._eccpMaintenancePlanRepository.FirstOrDefault(e => e.ElevatorId == elevatorId && !e.IsCancel);

            if (eccpMaintenancePlan != null && !eccpMaintenancePlan.IsClose)
            {
                var resultCount =
                    this._eccpMaintenanceWorkOrderManager.PlanDeletingRearrangeWorkOrder(eccpMaintenancePlan.Id);
                if (resultCount >= 0)
                {
                    eccpMaintenancePlan.IsClose = true;
                    this._eccpMaintenancePlanRepository.Update(eccpMaintenancePlan);
                }
                else
                {
                    throw new Exception("计划删除计划工单刷新异常，错误代码：" + resultCount);
                }
            }

            return 1;
        }
    }
}