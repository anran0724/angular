// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkManager_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpMaintenanceWorks
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpMaintenanceWorks;

    /// <summary>
    /// The eccp maintenance work manager_ tests.
    /// </summary>
    public class EccpMaintenanceWorkManager_Tests : AppTestBase
    {
        /// <summary>
        /// The _eccp maintenance work manager.
        /// </summary>
        private readonly EccpMaintenanceWorkManager _eccpMaintenanceWorkManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkManager_Tests"/> class.
        /// </summary>
        public EccpMaintenanceWorkManager_Tests()
        {
            this.LoginAsDefaultTenantAdmin();
            this._eccpMaintenanceWorkManager = this.Resolve<EccpMaintenanceWorkManager>();
        }

        /// <summary>
        /// The should_ get_ eccp maintenance work log.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_EccpMaintenanceWorkLog()
        {
            var eccpMaintenanceWorkLogList = await this._eccpMaintenanceWorkManager.GetAllAsync();
            eccpMaintenanceWorkLogList.Count.ShouldBe(1);
            eccpMaintenanceWorkLogList[0].MaintenanceWorkFlowName.ShouldBe("测试WorkFlowName111");
        }

        /// <summary>
        /// The test_ create_ eccp maintenance work log.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_EccpMaintenanceWorkLog()
        {
            var dt = DateTime.Now;
            var flowId = Guid.NewGuid();

            var eccpMaintenanceWorkLogEntity = new EccpMaintenanceWorkLog
                                                   {
                                                       CreationTime = dt,
                                                       IsDeleted = false,
                                                       TenantId = 1,
                                                       MaintenanceItemsName = "测试ItemsName",
                                                       Remark = "测试Remark",
                                                       MaintenanceWorkFlowId = flowId,
                                                       MaintenanceWorkFlowName = "测试WorkFlowName"
                                                   };

            await this._eccpMaintenanceWorkManager.Create(eccpMaintenanceWorkLogEntity);

            var entity = this.GetEntity("测试ItemsName");

            entity.MaintenanceItemsName.ShouldBe("测试ItemsName");
            entity.CreationTime.ShouldBe(dt);
            entity.IsDeleted.ShouldBe(false);
            entity.TenantId.ShouldBe(1);
            entity.Remark.ShouldBe("测试Remark");
            entity.MaintenanceWorkFlowId.ShouldBe(flowId);
            entity.MaintenanceWorkFlowName.ShouldBe("测试WorkFlowName");
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpMaintenanceWorkLog"/>.
        /// </returns>
        private EccpMaintenanceWorkLog GetEntity(string name)
        {
            var entity = this.UsingDbContext(
                context => context.EccpMaintenanceWorkLogs.FirstOrDefault(e => e.MaintenanceItemsName == name));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}