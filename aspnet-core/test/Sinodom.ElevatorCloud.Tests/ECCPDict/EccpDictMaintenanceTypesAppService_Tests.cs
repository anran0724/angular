// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceTypesAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.ECCPDict
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The eccp dict maintenance types app service_ tests.
    /// </summary>
    public class EccpDictMaintenanceTypesAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _maintenance types app service.
        /// </summary>
        private readonly IEccpDictMaintenanceTypesAppService _maintenanceTypesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictMaintenanceTypesAppService_Tests"/> class.
        /// </summary>
        public EccpDictMaintenanceTypesAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._maintenanceTypesAppService = this.Resolve<IEccpDictMaintenanceTypesAppService>();
        }

        /// <summary>
        /// The should_ get_maintenance types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_maintenanceTypes()
        {
            // Act
            var output =
                await this._maintenanceTypesAppService.GetAll(
                    new GetAllEccpDictMaintenanceTypesInput { Filter = "半月维保" });

            output.TotalCount.ShouldBe(1);
            output.Items[0].EccpDictMaintenanceType.Name.ShouldBe("半月维保");
        }

        /// <summary>
        /// The test_ create_maintenance types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_maintenanceTypes()
        {
            // Arrange
            var name = "维保类型1";

            // Act
            await this._maintenanceTypesAppService.CreateOrEdit(
                new CreateOrEditEccpDictMaintenanceTypeDto { Name = name });

            // Assert
            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
        }

        /// <summary>
        /// The test_ delete_maintenance types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_maintenanceTypes()
        {
            // Arrange
            var entity = this.GetEntity("半月维保");

            // Act
            await this._maintenanceTypesAppService.Delete(new EntityDto(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_maintenance types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_maintenanceTypes()
        {
            // Arrange
            var entity = this.GetEntity("半月维保");
            var newName = "维保类型2";

            // Act
            await this._maintenanceTypesAppService.CreateOrEdit(
                new CreateOrEditEccpDictMaintenanceTypeDto { Id = entity.Id, Name = newName });

            // Assert
            var newEntity = this.GetEntity(entity.Id);

            newEntity.Name.ShouldBe(newName);
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="diplayName">
        /// The diplay name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpDictMaintenanceType"/>.
        /// </returns>
        private EccpDictMaintenanceType GetEntity(string diplayName)
        {
            var entity = this.UsingDbContext(
                context => context.EccpDictMaintenanceTypes.FirstOrDefault(e => e.Name == diplayName));
            entity.ShouldNotBeNull();

            return entity;
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="EccpDictMaintenanceType"/>.
        /// </returns>
        private EccpDictMaintenanceType GetEntity(long id)
        {
            var entity = this.UsingDbContext(
                context => context.EccpDictMaintenanceTypes.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}