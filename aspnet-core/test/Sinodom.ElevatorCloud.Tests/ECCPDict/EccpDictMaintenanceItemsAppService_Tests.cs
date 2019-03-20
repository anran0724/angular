// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceItemsAppService_Tests.cs" company="Sinodom">
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
    /// The eccp dict maintenance items app service_ tests.
    /// </summary>
    public class EccpDictMaintenanceItemsAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _ eccp dict maintenance items app service.
        /// </summary>
        private readonly IEccpDictMaintenanceItemsAppService _eccpDictMaintenanceItemsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictMaintenanceItemsAppService_Tests"/> class.
        /// </summary>
        public EccpDictMaintenanceItemsAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._eccpDictMaintenanceItemsAppService = this.Resolve<IEccpDictMaintenanceItemsAppService>();
        }

        /// <summary>
        /// The should_ get_ eccp dict maintenance items.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_EccpDictMaintenanceItems()
        {
            var entities = await this._eccpDictMaintenanceItemsAppService.GetAll(
                               new GetAllEccpDictMaintenanceItemsInput { Filter = "消防开关" });

            entities.Items.Count.ShouldBe(1);
            entities.Items[0].EccpDictMaintenanceItem.Name.ShouldBe("消防开关");
        }

        /// <summary>
        /// The test_ create_ eccp dict maintenance items.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_EccpDictMaintenanceItems()
        {
            // Arrange
            var name = "测试维保项目";

            // Act
            await this._eccpDictMaintenanceItemsAppService.CreateOrEdit(
                new CreateOrEditEccpDictMaintenanceItemDto
                    {
                        Name = name, TermCode = "10000", DisOrder = 1, TermDesc = "测试维保项目"
                    });

            // Assert
            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
            entity.TermCode.ShouldBe("10000");
            entity.DisOrder.ShouldBe(1);
            entity.TermDesc.ShouldBe("测试维保项目");
        }

        /// <summary>
        /// The test_ delete_ eccp dict maintenance items.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_EccpDictMaintenanceItems()
        {
            // Arrange
            var entity = this.GetEntity("消防开关");

            // Act
            await this._eccpDictMaintenanceItemsAppService.Delete(new EntityDto(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ eccp dict maintenance items.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_EccpDictMaintenanceItems()
        {
            // Arrange
            var entity = this.GetEntity("消防开关");
            var newName = "测试维保项目2";

            // Act
            await this._eccpDictMaintenanceItemsAppService.CreateOrEdit(
                new CreateOrEditEccpDictMaintenanceItemDto
                    {
                        Id = entity.Id,
                        Name = newName,
                        TermCode = "100001",
                        DisOrder = 2,
                        TermDesc = "测试维保项目2"
                    });

            // Assert
            var newEntity = this.GetEntity(entity.Id);

            newEntity.Name.ShouldBe(newName);
            newEntity.TermCode.ShouldBe("100001");
            newEntity.DisOrder.ShouldBe(2);
            newEntity.TermDesc.ShouldBe("测试维保项目2");
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpDictMaintenanceItem"/>.
        /// </returns>
        private EccpDictMaintenanceItem GetEntity(string name)
        {
            var entity = this.UsingDbContext(
                context => context.EccpDictMaintenanceItems.FirstOrDefault(e => e.Name == name));
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
        /// The <see cref="EccpDictMaintenanceItem"/>.
        /// </returns>
        private EccpDictMaintenanceItem GetEntity(long id)
        {
            var entity = this.UsingDbContext(
                context => context.EccpDictMaintenanceItems.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}