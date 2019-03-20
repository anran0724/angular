// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictElevatorTypesAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpDict
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The eccp dict elevator types app service_ tests.
    /// </summary>
    public class EccpDictElevatorTypesAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _ eccp dict elevator types app service.
        /// </summary>
        private readonly IEccpDictElevatorTypesAppService _eccpDictElevatorTypesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictElevatorTypesAppService_Tests"/> class.
        /// </summary>
        public EccpDictElevatorTypesAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._eccpDictElevatorTypesAppService = this.Resolve<IEccpDictElevatorTypesAppService>();
        }

        /// <summary>
        /// The should_ create_ dict elevator types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Create_DictElevatorTypes()
        {
            var name = "载货电梯1";

            await this._eccpDictElevatorTypesAppService.CreateOrEdit(
                new CreateOrEditEccpDictElevatorTypeDto { Name = name });

            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
        }

        /// <summary>
        /// The should_ edit_ dict elevator types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Edit_DictElevatorTypes()
        {
            var entity = this.GetEntity("载货电梯");
            var newName = "测试类型2";

            await this._eccpDictElevatorTypesAppService.CreateOrEdit(
                new CreateOrEditEccpDictElevatorTypeDto { Id = entity.Id, Name = newName });

            var newEntity = this.GetEntity(entity.Id);

            newEntity.Name.ShouldBe(newName);
        }

        /// <summary>
        /// The should_ get_ dict elevator types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_DictElevatorTypes()
        {
            var entities = await this._eccpDictElevatorTypesAppService.GetAll(
                               new GetAllEccpDictElevatorTypesInput { Filter = "载货电梯" });
            entities.Items.Count.ShouldBe(1);
            entities.Items[0].EccpDictElevatorType.Name.ShouldBe("载货电梯");
        }

        /// <summary>
        /// The test_ delete_ dict elevator types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_DictElevatorTypes()
        {
            // Arrange
            var entity = this.GetEntity("载货电梯");

            // Act
            await this._eccpDictElevatorTypesAppService.Delete(new EntityDto(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpDictElevatorType"/>.
        /// </returns>
        private EccpDictElevatorType GetEntity(string name)
        {
            var entity = this.UsingDbContext(
                context => context.EccpDictElevatorTypes.FirstOrDefault(e => e.Name == name));
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
        /// The <see cref="EccpDictElevatorType"/>.
        /// </returns>
        private EccpDictElevatorType GetEntity(int id)
        {
            var entity = this.UsingDbContext(context => context.EccpDictElevatorTypes.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}