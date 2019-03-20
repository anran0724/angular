// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorModelsAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpBaseElevatorModels
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpBaseElevatorModels;
    using Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos;

    /// <summary>
    /// The eccp base elevator models app service_ tests.
    /// </summary>
    public class EccpBaseElevatorModelsAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _base elevator models app service.
        /// </summary>
        private readonly IEccpBaseElevatorModelsAppService _baseElevatorModelsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorModelsAppService_Tests"/> class.
        /// </summary>
        public EccpBaseElevatorModelsAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._baseElevatorModelsAppService = this.Resolve<IEccpBaseElevatorModelsAppService>();
        }

        /// <summary>
        /// The should_ get_ base elevator models.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_BaseElevatorModels()
        {
            var entities = await this._baseElevatorModelsAppService.GetAll(
                               new GetAllEccpBaseElevatorModelsInput { Filter = "电梯型号1" });

            entities.TotalCount.ShouldBe(1);
            entities.Items[0].EccpBaseElevatorModel.Name.ShouldBe("电梯型号1");
        }

        /// <summary>
        /// The test_ create_ base elevator models.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_BaseElevatorModels()
        {
            // Arrange
            var name = "电梯型号2";

            // Act
            await this._baseElevatorModelsAppService.CreateOrEdit(
                new CreateOrEditEccpBaseElevatorModelDto { Name = name, ElevatorBrandId = 1 });

            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
            entity.ElevatorBrandId.ShouldBe(1);
        }

        /// <summary>
        /// The test_ delete_ base elevator models.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_BaseElevatorModels()
        {
            // Arrange
            var entity = this.GetEntity("电梯型号1");

            // Act
            await this._baseElevatorModelsAppService.Delete(new EntityDto(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ base elevator models.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_BaseElevatorModels()
        {
            // Arrange
            var entity = this.GetEntity("电梯型号1");
            var newName = "电梯型号3";

            // Act
            await this._baseElevatorModelsAppService.CreateOrEdit(
                new CreateOrEditEccpBaseElevatorModelDto { Id = entity.Id, Name = newName, ElevatorBrandId = 1 });

            // Assert
            var newEntity = this.GetEntity(entity.Id);

            newEntity.Name.ShouldBe(newName);
            newEntity.ElevatorBrandId.ShouldBe(1);
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="diplayName">
        /// The diplay name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpBaseElevatorModel"/>.
        /// </returns>
        private EccpBaseElevatorModel GetEntity(string diplayName)
        {
            var entity = this.UsingDbContext(
                context => context.EccpBaseElevatorModels.FirstOrDefault(e => e.Name == diplayName));
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
        /// The <see cref="EccpBaseElevatorModel"/>.
        /// </returns>
        private EccpBaseElevatorModel GetEntity(long id)
        {
            var entity = this.UsingDbContext(context => context.EccpBaseElevatorModels.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}