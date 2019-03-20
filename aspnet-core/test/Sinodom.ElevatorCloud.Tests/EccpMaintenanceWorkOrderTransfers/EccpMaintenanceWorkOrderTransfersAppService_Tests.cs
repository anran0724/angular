// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrderTransfersAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpMaintenanceWorkOrderTransfers
{
    using System.Linq;

    using Abp.Authorization.Users;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrderTransfers;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrderTransfers.Dtos;
    using Sinodom.ElevatorCloud.MultiTenancy;

    /// <summary>
    /// The eccp maintenance work order transfers app service_ tests.
    /// </summary>
    public class EccpMaintenanceWorkOrderTransfersAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _eccp maintenance work order transfer audit logs app service.
        /// </summary>
        private readonly IEccpMaintenanceWorkOrderTransferAuditLogsAppService
            _eccpMaintenanceWorkOrderTransferAuditLogsAppService;

        /// <summary>
        /// The _eccp maintenance work order transfers app service.
        /// </summary>
        private readonly IEccpMaintenanceWorkOrderTransfersAppService _eccpMaintenanceWorkOrderTransfersAppService;

        

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkOrderTransfersAppService_Tests"/> class.
        /// </summary>
        public EccpMaintenanceWorkOrderTransfersAppService_Tests()
        {
            this.LoginAsTenant(Tenant.DefaultMaintenanceTenantName, AbpUserBase.AdminUserName);
            this._eccpMaintenanceWorkOrderTransfersAppService =
                this.Resolve<EccpMaintenanceWorkOrderTransfersAppService>();
            this._eccpMaintenanceWorkOrderTransferAuditLogsAppService =
                this.Resolve<EccpMaintenanceWorkOrderTransferAuditLogsAppService>();
        }

        /// <summary>
        ///     维保工单审批拒绝
        /// </summary>
        [MultiTenantFact]
        public async void TestEccpMaintenanceWorkOrderTransfersAuditFailed()
        {
            var auditInput = new EccpMaintenanceWorkOrderTransfersForAuditOutput { Category = "1", IsApproved = false };
            this.UsingDbContext(
                context => { auditInput.Id = context.EccpMaintenanceWorkOrderTransfers.FirstOrDefault().Id; });
            var r = this._eccpMaintenanceWorkOrderTransfersAppService.AuditMaintenanceWorkOrderTransfer(auditInput);
            r.ShouldBe(1);
            var input = new GetAllEccpMaintenanceWorkOrderTransfersInput();
            var eccpMaintenanceWorkOrderTransfers =
                await this._eccpMaintenanceWorkOrderTransfersAppService.GetAll(input);
            eccpMaintenanceWorkOrderTransfers.TotalCount.ShouldBe(2);
            var eccpMaintenanceWorkOrderTransfer =
                eccpMaintenanceWorkOrderTransfers.Items.FirstOrDefault(w => w.Category == "1");
            true.ShouldBe(eccpMaintenanceWorkOrderTransfer != null);
            var eccpMaintenanceTempWorkOrderTransfer =
                eccpMaintenanceWorkOrderTransfers.Items.FirstOrDefault(w => w.Category == "2");
            true.ShouldBe(eccpMaintenanceTempWorkOrderTransfer != null);
            eccpMaintenanceWorkOrderTransfer.IsApproved.ShouldBe(false);
            var auditLogInput =
                new GetAllEccpMaintenanceTempWorkOrderTransfersAuditLogInput { Id = auditInput.Id, Category = "1" };
            var eccpMaintenanceWorkOrderTransferAuditLogs =
                await this._eccpMaintenanceWorkOrderTransferAuditLogsAppService
                    .GetMaintenanceWorkOrderTransferAuditLogs(auditLogInput);
            eccpMaintenanceWorkOrderTransferAuditLogs.TotalCount.ShouldBe(1);
            eccpMaintenanceWorkOrderTransferAuditLogs.Items[0].Category.ShouldBe("1");
        }

        /// <summary>
        ///     维保工单审批通过
        /// </summary>
        [MultiTenantFact]
        public async void TestEccpMaintenanceWorkOrderTransfersAuditPass()
        {
            var auditInput = new EccpMaintenanceWorkOrderTransfersForAuditOutput { Category = "1", IsApproved = true };
            this.UsingDbContext(
                context => { auditInput.Id = context.EccpMaintenanceWorkOrderTransfers.FirstOrDefault().Id; });
            var r = this._eccpMaintenanceWorkOrderTransfersAppService.AuditMaintenanceWorkOrderTransfer(auditInput);
            r.ShouldBe(1);

            var input = new GetAllEccpMaintenanceWorkOrderTransfersInput();
            var eccpMaintenanceWorkOrderTransfers =
                await this._eccpMaintenanceWorkOrderTransfersAppService.GetAll(input);
            eccpMaintenanceWorkOrderTransfers.TotalCount.ShouldBe(2);
            var eccpMaintenanceWorkOrderTransfer =
                eccpMaintenanceWorkOrderTransfers.Items.FirstOrDefault(w => w.Category == "1");
            true.ShouldBe(eccpMaintenanceWorkOrderTransfer != null);

            var eccpMaintenanceTempWorkOrderTransfer =
                eccpMaintenanceWorkOrderTransfers.Items.FirstOrDefault(w => w.Category == "2");
            true.ShouldBe(eccpMaintenanceTempWorkOrderTransfer != null);
            eccpMaintenanceWorkOrderTransfer.IsApproved.ShouldBe(true);

            var auditLogInput =
                new GetAllEccpMaintenanceTempWorkOrderTransfersAuditLogInput { Id = auditInput.Id, Category = "1" };
            var eccpMaintenanceWorkOrderTransferAuditLogs =
                await this._eccpMaintenanceWorkOrderTransferAuditLogsAppService
                    .GetMaintenanceWorkOrderTransferAuditLogs(auditLogInput);
            eccpMaintenanceWorkOrderTransferAuditLogs.TotalCount.ShouldBe(1);
            eccpMaintenanceWorkOrderTransferAuditLogs.Items[0].Category.ShouldBe("1");
        }

        /// <summary>
        ///     临时工单审批拒绝
        /// </summary>
        [MultiTenantFact]
        public async void TestEccpTempMaintenanceWorkOrderTransfersAuditFailed()
        {
            var auditInput = new EccpMaintenanceWorkOrderTransfersForAuditOutput { Category = "2", IsApproved = false };
            this.UsingDbContext(
                context => { auditInput.Id = context.EccpMaintenanceWorkOrderTransfers.FirstOrDefault().Id; });
            var r = this._eccpMaintenanceWorkOrderTransfersAppService.AuditMaintenanceWorkOrderTransfer(auditInput);
            r.ShouldBe(1);
            var input = new GetAllEccpMaintenanceWorkOrderTransfersInput();
            var eccpMaintenanceWorkOrderTransfers =
                await this._eccpMaintenanceWorkOrderTransfersAppService.GetAll(input);
            eccpMaintenanceWorkOrderTransfers.TotalCount.ShouldBe(2);
            var eccpMaintenanceWorkOrderTransfer =
                eccpMaintenanceWorkOrderTransfers.Items.FirstOrDefault(w => w.Category == "1");
            true.ShouldBe(eccpMaintenanceWorkOrderTransfer != null);
            var eccpMaintenanceTempWorkOrderTransfer =
                eccpMaintenanceWorkOrderTransfers.Items.FirstOrDefault(w => w.Category == "2");
            true.ShouldBe(eccpMaintenanceTempWorkOrderTransfer != null);
            var auditLogInput =
                new GetAllEccpMaintenanceTempWorkOrderTransfersAuditLogInput { Id = auditInput.Id, Category = "2" };
            var eccpMaintenanceWorkOrderTransferAuditLogs =
                await this._eccpMaintenanceWorkOrderTransferAuditLogsAppService
                    .GetMaintenanceWorkOrderTransferAuditLogs(auditLogInput);
            eccpMaintenanceWorkOrderTransferAuditLogs.TotalCount.ShouldBe(1);
            eccpMaintenanceWorkOrderTransferAuditLogs.Items[0].Category.ShouldBe("2");
        }

        /// <summary>
        ///     临时工单审批通过
        /// </summary>
        [MultiTenantFact]
        public async void TestEccpTempMaintenanceWorkOrderTransfersAuditPass()
        {
            var auditInput = new EccpMaintenanceWorkOrderTransfersForAuditOutput { Category = "2", IsApproved = true };
            this.UsingDbContext(
                context => { auditInput.Id = context.EccpMaintenanceWorkOrderTransfers.FirstOrDefault().Id; });
            var r = this._eccpMaintenanceWorkOrderTransfersAppService.AuditMaintenanceWorkOrderTransfer(auditInput);
            r.ShouldBe(1);
            var input = new GetAllEccpMaintenanceWorkOrderTransfersInput();
            var eccpMaintenanceWorkOrderTransfers =
                await this._eccpMaintenanceWorkOrderTransfersAppService.GetAll(input);
            eccpMaintenanceWorkOrderTransfers.TotalCount.ShouldBe(2);
            var eccpMaintenanceWorkOrderTransfer =
                eccpMaintenanceWorkOrderTransfers.Items.FirstOrDefault(w => w.Category == "1");
            true.ShouldBe(eccpMaintenanceWorkOrderTransfer != null);
            var eccpMaintenanceTempWorkOrderTransfer =
                eccpMaintenanceWorkOrderTransfers.Items.FirstOrDefault(w => w.Category == "2");
            true.ShouldBe(eccpMaintenanceTempWorkOrderTransfer != null);
            eccpMaintenanceTempWorkOrderTransfer.IsApproved.ShouldBe(true);
            var auditLogInput =
                new GetAllEccpMaintenanceTempWorkOrderTransfersAuditLogInput { Id = auditInput.Id, Category = "2" };
            var eccpMaintenanceWorkOrderTransferAuditLogs =
                await this._eccpMaintenanceWorkOrderTransferAuditLogsAppService
                    .GetMaintenanceWorkOrderTransferAuditLogs(auditLogInput);
            eccpMaintenanceWorkOrderTransferAuditLogs.TotalCount.ShouldBe(1);
            eccpMaintenanceWorkOrderTransferAuditLogs.Items[0].Category.ShouldBe("2");
        }

        /// <summary>
        ///     临时工单和维保工单转接申请信息查询
        /// </summary>
        [MultiTenantFact]
        public async void TestQueryEccpMaintenanceWorkOrderTransfers()
        {
            var input = new GetAllEccpMaintenanceWorkOrderTransfersInput();
            var eccpMaintenanceWorkOrderTransfers =
                await this._eccpMaintenanceWorkOrderTransfersAppService.GetAll(input);
            eccpMaintenanceWorkOrderTransfers.TotalCount.ShouldBe(2);
            var eccpMaintenanceWorkOrderTransfer =
                eccpMaintenanceWorkOrderTransfers.Items.FirstOrDefault(w => w.Category == "1");
            true.ShouldBe(eccpMaintenanceWorkOrderTransfer != null);
            var eccpMaintenanceTempWorkOrderTransfer =
                eccpMaintenanceWorkOrderTransfers.Items.FirstOrDefault(w => w.Category == "2");
            true.ShouldBe(eccpMaintenanceTempWorkOrderTransfer != null);
        }

        /// <summary>
        /// The test_ apply work order transfer_ maintenance work order.
        /// </summary>
        /// <returns>
         /// </returns>
        [MultiTenantFact]
        public async void Test_ApplyWorkOrderTransfer_MaintenanceWorkOrder()
        {

            var entity = this.UsingDbContext( context => context.EccpMaintenanceWorkOrders.FirstOrDefault(e => e.Id == 1));
            var applyEccpMaintenanceWorkOrderTransfer = new ApplyEccpMaintenanceWorkOrderTransferDto
            {
                MaintenanceWorkOrderId = entity.Id,
                TransferUserId = 2,
                Remark = "临时有事，工单转接"
            };
            //// Act
            int result = await this._eccpMaintenanceWorkOrderTransfersAppService.ApplyWorkOrderTransfer(applyEccpMaintenanceWorkOrderTransfer);

            //// Assert
            result.ShouldBeGreaterThan(0);
        }

        /// <summary>
        /// The test_ apply temp work order transfer_ maintenance temp work order.
        /// </summary>
        /// <returns>      
        /// </returns>
        [MultiTenantFact]
        public async void Test_ApplyTempWorkOrderTransfer_MaintenanceTempWorkOrder()
        {
            var entity = this.UsingDbContext(context => context.EccpMaintenanceTempWorkOrders.FirstOrDefault(e => e.UserId == AbpSession.UserId));
            //// Act
            var result = await this._eccpMaintenanceWorkOrderTransfersAppService.ApplyTempWorkOrderTransfer(
                new ApplyEccpMaintenanceTempWorkOrderTransferDto
                {
                    MaintenanceTempWorkOrderId = entity.Id,
                    TransferUserId = 1,
                    Remark = "临时有事，工单转接"
                });
            //// Assert
            result.ShouldBeGreaterThan(0);
        }


    }
}