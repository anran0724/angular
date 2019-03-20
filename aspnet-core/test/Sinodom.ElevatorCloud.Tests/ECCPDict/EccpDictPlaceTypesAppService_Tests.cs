// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictPlaceTypesAppService_Tests.cs" company="Sinodom">
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
    /// The eccp dict place types app service_ tests.
    /// </summary>
    public class EccpDictPlaceTypesAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _dict place types app service.
        /// </summary>
        private readonly IEccpDictPlaceTypesAppService _dictPlaceTypesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictPlaceTypesAppService_Tests"/> class.
        /// </summary>
        public EccpDictPlaceTypesAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._dictPlaceTypesAppService = this.Resolve<IEccpDictPlaceTypesAppService>();
        }

        /// <summary>
        /// The should_ get_place types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_placeTypes()
        {
            // Act
            var output =
                await this._dictPlaceTypesAppService.GetAll(new GetAllEccpDictPlaceTypesInput { Filter = "住宅" });

            output.TotalCount.ShouldBe(1);
            output.Items[0].EccpDictPlaceType.Name.ShouldBe("住宅");
        }

        /// <summary>
        /// The test_ create_place types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_placeTypes()
        {
            // Arrange
            var name = "测试使用场所类型1";

            // Act
            await this._dictPlaceTypesAppService.CreateOrEdit(new CreateOrEditEccpDictPlaceTypeDto { Name = name });

            // Assert
            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
        }

        /// <summary>
        /// The test_ delete_place types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_placeTypes()
        {
            // Arrange
            var entity = this.GetEntity("住宅");

            // Act
            await this._dictPlaceTypesAppService.Delete(new EntityDto(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_place types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_placeTypes()
        {
            // Arrange
            var entity = this.GetEntity("住宅");
            var newName = "测试使用场所类型1";

            // Act
            await this._dictPlaceTypesAppService.CreateOrEdit(
                new CreateOrEditEccpDictPlaceTypeDto { Id = entity.Id, Name = newName });

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
        /// The <see cref="EccpDictPlaceType"/>.
        /// </returns>
        private EccpDictPlaceType GetEntity(string diplayName)
        {
            var entity = this.UsingDbContext(
                context => context.EccpDictPlaceTypes.FirstOrDefault(e => e.Name == diplayName));
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
        /// The <see cref="EccpDictPlaceType"/>.
        /// </returns>
        private EccpDictPlaceType GetEntity(long id)
        {
            var entity = this.UsingDbContext(context => context.EccpDictPlaceTypes.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}