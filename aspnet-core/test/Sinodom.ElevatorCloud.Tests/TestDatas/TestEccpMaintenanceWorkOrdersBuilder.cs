// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestEccpMaintenanceWorkOrdersBuilder.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// <summary>
//   The test eccp maintenance work orders builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    using System;

    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.EntityFrameworkCore;

    /// <summary>
    /// The test eccp maintenance work orders builder.
    /// </summary>
    public class TestEccpMaintenanceWorkOrdersBuilder
    {
        /// <summary>
        /// The _context.
        /// </summary>
        private readonly ElevatorCloudDbContext context;

        /// <summary>
        /// The _maintenance tenant id.
        /// </summary>
        private readonly int maintenanceTenantId;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestEccpMaintenanceWorkOrdersBuilder"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="maintenanceTenantId">
        /// The maintenance tenant id.
        /// </param>
        public TestEccpMaintenanceWorkOrdersBuilder(ElevatorCloudDbContext context, int maintenanceTenantId)
        {
            this.context = context;
            this.maintenanceTenantId = maintenanceTenantId;
        }

        /// <summary>
        /// The create.
        /// </summary>
        public void Create()
        {
            this.MaintenanceWorkOrder(1);
        }

        /// <summary>
        /// The maintenance work order.
        /// </summary>
        /// <param name="maintenancePlanId">
        /// The maintenance plan id.
        /// </param>
        private void MaintenanceWorkOrder(int maintenancePlanId)
        {
            var eccpMaintenanceWorkOrder = new EccpMaintenanceWorkOrder
            {
                IsPassed = false,
                MaintenancePlanId = maintenancePlanId,
                PlanCheckDate = DateTime.Today.AddDays(2),
                MaintenanceStatusId = 1,
                MaintenanceTypeId = 2,
                TenantId = this.maintenanceTenantId
            };
            this.context.EccpMaintenanceWorkOrders.Add(eccpMaintenanceWorkOrder);

            this.context.SaveChanges();

            var eccpMaintenancePlanMaintenanceUserLinks = this.context.EccpMaintenancePlan_MaintenanceUser_Links.Where(w => w.MaintenancePlanId == eccpMaintenanceWorkOrder.MaintenancePlanId);
            var listMaintenanceUsers = eccpMaintenancePlanMaintenanceUserLinks.Select(s =>
                new EccpMaintenanceWorkOrder_MaintenanceUser_Link
                {
                    TenantId = s.TenantId,
                    UserId = s.UserId,
                    MaintenancePlanId = eccpMaintenanceWorkOrder.Id
                }
            );
            context.AddRange(listMaintenanceUsers);

            var eccpMaintenancePlanPropertyUserLinks = this.context.EccpMaintenancePlan_PropertyUser_Links.Where(w => w.MaintenancePlanId == eccpMaintenanceWorkOrder.MaintenancePlanId);
            var listMaintenanceUserPropertyUsers = eccpMaintenancePlanPropertyUserLinks.Select(s =>
                new EccpMaintenanceWorkOrder_MaintenanceUser_Link
                {
                    TenantId = s.TenantId,
                    UserId = s.UserId,
                    MaintenancePlanId = eccpMaintenanceWorkOrder.Id
                }
            );
            context.AddRange(listMaintenanceUserPropertyUsers);

            this.context.SaveChanges();

            //未开始工单     
            var dictMaintenanceStatusEntityNotStarted = this.context.EccpDictMaintenanceStatuses.FirstOrDefault(e => e.Name == "未进行");
            var eccpMaintenanceWorkOrderNotStarted = new EccpMaintenanceWorkOrder
            {
                IsPassed = true,
                MaintenancePlanId = maintenancePlanId,
                PlanCheckDate = DateTime.Now,
                MaintenanceStatusId = dictMaintenanceStatusEntityNotStarted.Id,
                MaintenanceTypeId = 2,
                TenantId = this.maintenanceTenantId,
                IsClosed = false
            };
            this.context.EccpMaintenanceWorkOrders.Add(eccpMaintenanceWorkOrderNotStarted);


            var eccpMaintenancePlanMaintenanceUserLinksNotStarted = this.context.EccpMaintenancePlan_MaintenanceUser_Links.Where(w => w.MaintenancePlanId == eccpMaintenanceWorkOrderNotStarted.MaintenancePlanId);
            var listMaintenanceUsersNotStarted = eccpMaintenancePlanMaintenanceUserLinksNotStarted.Select(s =>
                new EccpMaintenanceWorkOrder_MaintenanceUser_Link
                {
                    TenantId = s.TenantId,
                    UserId = s.UserId,
                    MaintenancePlanId = eccpMaintenanceWorkOrderNotStarted.Id
                }
            );
            context.AddRange(listMaintenanceUsersNotStarted);

            //已完成工单
            var dictMaintenanceStatusEntityDone = this.context.EccpDictMaintenanceStatuses.FirstOrDefault(e => e.Name == "已完成");

            var eccpMaintenanceWorkOrderDone = new EccpMaintenanceWorkOrder
            {
                IsPassed = true,
                MaintenancePlanId = maintenancePlanId,
                PlanCheckDate = DateTime.Now,
                MaintenanceStatusId = dictMaintenanceStatusEntityDone.Id,
                MaintenanceTypeId = 2,
                TenantId = this.maintenanceTenantId,
                IsClosed = false
            };
            this.context.EccpMaintenanceWorkOrders.Add(eccpMaintenanceWorkOrderDone);

            var eccpMaintenancePlanMaintenanceUserLinksDone = this.context.EccpMaintenancePlan_MaintenanceUser_Links.Where(w => w.MaintenancePlanId == eccpMaintenanceWorkOrderDone.MaintenancePlanId);
            var listMaintenanceUsersDone = eccpMaintenancePlanMaintenanceUserLinksDone.Select(s =>
                new EccpMaintenanceWorkOrder_MaintenanceUser_Link
                {
                    TenantId = s.TenantId,
                    UserId = s.UserId,
                    MaintenancePlanId = eccpMaintenanceWorkOrderDone.Id
                }
            );
            context.AddRange(listMaintenanceUsersDone);


            //已超期工单
            var dictMaintenanceStatusEntityOverdue = this.context.EccpDictMaintenanceStatuses.FirstOrDefault(e => e.Name == "已超期");

            var eccpMaintenanceWorkOrderOverdue = new EccpMaintenanceWorkOrder
            {
                IsPassed = true,
                MaintenancePlanId = maintenancePlanId,
                PlanCheckDate = DateTime.Now,
                MaintenanceStatusId = dictMaintenanceStatusEntityOverdue.Id,
                MaintenanceTypeId = 2,
                TenantId = this.maintenanceTenantId,
                IsClosed = false
            };
            this.context.EccpMaintenanceWorkOrders.Add(eccpMaintenanceWorkOrderOverdue);


            var eccpMaintenancePlanMaintenanceUserLinksOverdue = this.context.EccpMaintenancePlan_MaintenanceUser_Links.Where(w => w.MaintenancePlanId == eccpMaintenanceWorkOrderOverdue.MaintenancePlanId);
            var listMaintenanceUsersOverdue = eccpMaintenancePlanMaintenanceUserLinksOverdue.Select(s =>
                new EccpMaintenanceWorkOrder_MaintenanceUser_Link
                {
                    TenantId = s.TenantId,
                    UserId = s.UserId,
                    MaintenancePlanId = eccpMaintenanceWorkOrderOverdue.Id
                }
            );
            context.AddRange(listMaintenanceUsersOverdue);

        }
    }
}