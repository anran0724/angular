// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictWorkOrderTypesAppService_Tests.cs" company="Sinodom">
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
    /// The eccp dict work order types app service_ tests.
    /// </summary>
    public class EccpDictWorkOrderTypesAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _dict work order types app service.
        /// </summary>
        private readonly IEccpDictWorkOrderTypesAppService _dictWorkOrderTypesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictWorkOrderTypesAppService_Tests"/> class.
        /// </summary>
        public EccpDictWorkOrderTypesAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._dictWorkOrderTypesAppService = this.Resolve<IEccpDictWorkOrderTypesAppService>();
        }

        /// <summary>
        /// The should_ get_ dict work order types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_DictWorkOrderTypes()
        {
            // Act
            var output =
                await this._dictWorkOrderTypesAppService.GetAll(
                    new GetAllEccpDictWorkOrderTypesInput { Filter = "维保工单" });

            output.TotalCount.ShouldBe(1);
            output.Items[0].EccpDictWorkOrderType.Name.ShouldBe("维保工单");
        }

        /// <summary>
        /// The test_ create_ dict work order types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_DictWorkOrderTypes()
        {
            // Arrange
            var name = "维保工单1";

            // Act
            await this._dictWorkOrderTypesAppService.CreateOrEdit(
                new CreateOrEditEccpDictWorkOrderTypeDto { Name = name });

            // Assert
            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
        }

        /// <summary>
        /// The test_ delete_ dict work order types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_DictWorkOrderTypes()
        {
            // Arrange
            var entity = this.GetEntity("维保工单");

            // Act
            await this._dictWorkOrderTypesAppService.Delete(new EntityDto(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ dict work order types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_DictWorkOrderTypes()
        {
            // Arrange
            var entity = this.GetEntity("维保工单");
            var newName = "维保工单2";

            // Act
            await this._dictWorkOrderTypesAppService.CreateOrEdit(
                new CreateOrEditEccpDictWorkOrderTypeDto { Id = entity.Id, Name = newName });

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
        /// The <see cref="EccpDictWorkOrderType"/>.
        /// </returns>
        private EccpDictWorkOrderType GetEntity(string diplayName)
        {
            var entity = this.UsingDbContext(
                context => context.EccpDictWorkOrderTypes.FirstOrDefault(e => e.Name == diplayName));
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
        /// The <see cref="EccpDictWorkOrderType"/>.
        /// </returns>
        private EccpDictWorkOrderType GetEntity(long id)
        {
            var entity = this.UsingDbContext(context => context.EccpDictWorkOrderTypes.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}