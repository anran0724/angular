// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPDictElevatorStatusesAppService_Tests.cs" company="Sinodom">
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
    /// The eccp dict elevator statuses app service_ tests.
    /// </summary>
    public class ECCPDictElevatorStatusesAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _dict elevator statuses app service.
        /// </summary>
        private readonly IECCPDictElevatorStatusesAppService _dictElevatorStatusesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPDictElevatorStatusesAppService_Tests"/> class.
        /// </summary>
        public ECCPDictElevatorStatusesAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._dictElevatorStatusesAppService = this.Resolve<IECCPDictElevatorStatusesAppService>();
        }

        /// <summary>
        /// The should_ get_ dict elevator statuses.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_DictElevatorStatuses()
        {
            // Act
            var output =
                await this._dictElevatorStatusesAppService.GetAll(
                    new GetAllECCPDictElevatorStatusesInput { Filter = "屏蔽" });

            output.TotalCount.ShouldBe(1);
            output.Items[0].ECCPDictElevatorStatus.Name.ShouldBe("屏蔽");
        }

        /// <summary>
        /// The test_ create_ dict elevator statuses.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_DictElevatorStatuses()
        {
            // Arrange
            var name = "电梯状态1";

            // Act
            await this._dictElevatorStatusesAppService.CreateOrEdit(
                new CreateOrEditECCPDictElevatorStatusDto { Name = name });

            // Assert
            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
        }

        /// <summary>
        /// The test_ delete_ dict elevator statuses.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_DictElevatorStatuses()
        {
            // Arrange
            var entity = this.GetEntity("屏蔽");

            // Act
            await this._dictElevatorStatusesAppService.Delete(new EntityDto(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ dict elevator statuses.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_DictElevatorStatuses()
        {
            // Arrange
            var entity = this.GetEntity("屏蔽");
            var newName = "电梯状态1";

            // Act
            await this._dictElevatorStatusesAppService.CreateOrEdit(
                new CreateOrEditECCPDictElevatorStatusDto { Id = entity.Id, Name = newName });

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
        /// The <see cref="ECCPDictElevatorStatus"/>.
        /// </returns>
        private ECCPDictElevatorStatus GetEntity(string diplayName)
        {
            var entity = this.UsingDbContext(
                context => context.ECCPDictElevatorStatuses.FirstOrDefault(e => e.Name == diplayName));
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
        /// The <see cref="ECCPDictElevatorStatus"/>.
        /// </returns>
        private ECCPDictElevatorStatus GetEntity(long id)
        {
            var entity = this.UsingDbContext(
                context => context.ECCPDictElevatorStatuses.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}