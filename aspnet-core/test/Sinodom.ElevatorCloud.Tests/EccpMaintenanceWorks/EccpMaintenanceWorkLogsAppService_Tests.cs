// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkLogsAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpMaintenanceWorks
{
    using System.Linq;
    using System.Threading.Tasks;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpMaintenanceWorks;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;

    /// <summary>
    /// The eccp maintenance work logs app service_ tests.
    /// </summary>
    public class EccpMaintenanceWorkLogsAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _eccp maintenance work logs app service.
        /// </summary>
        private readonly IEccpMaintenanceWorkLogsAppService _eccpMaintenanceWorkLogsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkLogsAppService_Tests"/> class.
        /// </summary>
        public EccpMaintenanceWorkLogsAppService_Tests()
        {
            this.LoginAsDefaultTenantAdmin();
            this._eccpMaintenanceWorkLogsAppService = this.Resolve<IEccpMaintenanceWorkLogsAppService>();
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
            var entities = await this._eccpMaintenanceWorkLogsAppService.GetAll(
                               new GetAllEccpMaintenanceWorkLogsInput { Filter = null });

            entities.Items.Count.ShouldBe(1);
            entities.Items[0].EccpMaintenanceWorkLog.MaintenanceItemsName.ShouldBe("测试ItemsName111");
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