// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorBrandsAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpBaseElevatorBrands
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpBaseElevatorBrands;
    using Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos;

    /// <summary>
    /// The eccp base elevator brands app service_ tests.
    /// </summary>
    public class EccpBaseElevatorBrandsAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _base elevator brands app service.
        /// </summary>
        private readonly IEccpBaseElevatorBrandsAppService _baseElevatorBrandsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorBrandsAppService_Tests"/> class.
        /// </summary>
        public EccpBaseElevatorBrandsAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._baseElevatorBrandsAppService = this.Resolve<IEccpBaseElevatorBrandsAppService>();
        }

        /// <summary>
        /// The should_ get_ base elevator brands.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_BaseElevatorBrands()
        {
            var entities =
                await this._baseElevatorBrandsAppService.GetAll(
                    new GetAllEccpBaseElevatorBrandsInput { Filter = "电梯品牌1" });

            entities.TotalCount.ShouldBe(1);
            entities.Items[0].EccpBaseElevatorBrand.Name.ShouldBe("电梯品牌1");
        }

        /// <summary>
        /// The test_ create_ base elevator brands.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_BaseElevatorBrands()
        {
            // Arrange
            var name = "电梯品牌2";

            // Act
            await this._baseElevatorBrandsAppService.CreateOrEdit(
                new CreateOrEditEccpBaseElevatorBrandDto { Name = name, ProductionCompanyId = 1 });

            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
            entity.ProductionCompanyId.ShouldBe(1);
        }

        /// <summary>
        /// The test_ delete_ base elevator brands.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_BaseElevatorBrands()
        {
            var entity = this.GetEntity("电梯品牌1");

            // Act
            await this._baseElevatorBrandsAppService.Delete(new EntityDto(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ base elevator brands.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_BaseElevatorBrands()
        {
            var entity = this.GetEntity("电梯品牌1");
            var newName = "电梯品牌2";

            // Act
            await this._baseElevatorBrandsAppService.CreateOrEdit(
                new CreateOrEditEccpBaseElevatorBrandDto { Id = entity.Id, Name = newName, ProductionCompanyId = 1 });

            // Assert
            var newEntity = this.GetEntity(entity.Id);

            newEntity.Name.ShouldBe(newName);
            newEntity.ProductionCompanyId.ShouldBe(1);
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="displayName">
        /// The diplay name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpBaseElevatorBrand"/>.
        /// </returns>
        private EccpBaseElevatorBrand GetEntity(string displayName)
        {
            var entity = this.UsingDbContext(
                context => context.EccpBaseElevatorBrands.FirstOrDefault(e => e.Name == displayName));
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
        /// The <see cref="EccpBaseElevatorBrand"/>.
        /// </returns>
        private EccpBaseElevatorBrand GetEntity(long id)
        {
            var entity = this.UsingDbContext(context => context.EccpBaseElevatorBrands.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}