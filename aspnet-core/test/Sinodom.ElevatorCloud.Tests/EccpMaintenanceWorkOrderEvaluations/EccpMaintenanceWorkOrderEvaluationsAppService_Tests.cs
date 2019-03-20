// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrderEvaluationsAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpMaintenanceWorkOrderEvaluations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Authorization.Users;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;
    using Sinodom.ElevatorCloud.MultiTenancy;

    /// <summary>
    /// The eccp maintenance work order evaluations app service_ tests.
    /// </summary>
    public class EccpMaintenanceWorkOrderEvaluationsAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _eccp base elevators app service.
        /// </summary>
        private readonly IEccpBaseElevatorsAppService _eccpBaseElevatorsAppService;

        /// <summary>
        /// The _eccp maintenance work order evaluations app service.
        /// </summary>
        private readonly IEccpMaintenanceWorkOrderEvaluationsAppService _eccpMaintenanceWorkOrderEvaluationsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkOrderEvaluationsAppService_Tests"/> class.
        /// </summary>
        public EccpMaintenanceWorkOrderEvaluationsAppService_Tests()
        {
            this.LoginAsTenant(Tenant.DefaultMaintenanceTenantName, AbpUserBase.AdminUserName);
            this.Resolve<EccpMaintenanceWorkOrdersAppService>();
            this._eccpMaintenanceWorkOrderEvaluationsAppService =
                this.Resolve<EccpMaintenanceWorkOrderEvaluationsAppService>();
            this.Resolve<TenantAppService>();
            this._eccpBaseElevatorsAppService = this.Resolve<IEccpBaseElevatorsAppService>();
        }

        /// <summary>
        /// The should_ get_ maintenance work order evaluations.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_MaintenanceWorkOrderEvaluations()
        {
            var elevatorEntity = this.GetElevatorEntity("a1111111111");
            elevatorEntity.ShouldNotBeNull();

            var entities = await this._eccpBaseElevatorsAppService.GetAllWorkOrderEvaluationByElevatorId(
                               new GetAllByElevatorIdEccpMaintenanceWorkOrderEvaluationsInput
                                   {
                                       ElevatorIdFilter = Convert.ToString(elevatorEntity.Id)
                                   });
            entities.Items.Count.ShouldBe(1);
        }

        /// <summary>
        /// The test_ create_ eccp maintenance work order evaluations.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_EccpMaintenanceWorkOrderEvaluations()
        {
            // Act
            await this._eccpMaintenanceWorkOrderEvaluationsAppService.Commit(
                new CreateOrEditEccpMaintenanceWorkOrderEvaluationDto
                    {
                        WorkOrderId = 1, Rank = 4, Remarks = "测试维保评价"
                    });

            // Assert
            this.UsingDbContext(
                context =>
                    {
                        var evaluationEntity =
                            context.EccpMaintenanceWorkOrderEvaluations.FirstOrDefault(
                                e => e.Rank == 4 && e.WorkOrderId == 1);
                        evaluationEntity.ShouldNotBeNull();
                        evaluationEntity.Remarks.ShouldBe("测试维保评价");
                    });
        }

        /// <summary>
        /// The get elevator entity.
        /// </summary>
        /// <param name="certificateNum">
        /// The certificate num.
        /// </param>
        /// <returns>
        /// The <see cref="EccpBaseElevator"/>.
        /// </returns>
        private EccpBaseElevator GetElevatorEntity(string certificateNum)
        {
            var entity = this.UsingDbContext(
                context => context.EccpBaseElevators.FirstOrDefault(e => e.CertificateNum == certificateNum));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}