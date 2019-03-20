// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseAnnualInspectionUnitsAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.ECCPBaseAnnualInspectionUnits
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits;
    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos;

    /// <summary>
    /// The eccp base annual inspection units app service_ tests.
    /// </summary>
    public class ECCPBaseAnnualInspectionUnitsAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _annual inspection units app service.
        /// </summary>
        private readonly IECCPBaseAnnualInspectionUnitsAppService _annualInspectionUnitsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseAnnualInspectionUnitsAppService_Tests"/> class.
        /// </summary>
        public ECCPBaseAnnualInspectionUnitsAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._annualInspectionUnitsAppService = this.Resolve<IECCPBaseAnnualInspectionUnitsAppService>();
        }

        /// <summary>
        /// The should_ get_ annual inspection unit.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_AnnualInspectionUnit()
        {
            var entity = this.GetEntity("测试单位1");

            var output =
                await this._annualInspectionUnitsAppService.GetECCPBaseAnnualInspectionUnitForEdit(
                    new EntityDto<long>(entity.Id));

            output.ECCPBaseAnnualInspectionUnit.Name.ShouldBe("测试单位1");
        }

        /// <summary>
        /// The should_ get_ annual inspection units.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_AnnualInspectionUnits()
        {
            var entities = await this._annualInspectionUnitsAppService.GetAll(
                               new GetAllECCPBaseAnnualInspectionUnitsInput { Filter = "测试单位1" });
            entities.Items.Count.ShouldBe(1);
            entities.Items[0].ECCPBaseAnnualInspectionUnit.Name.ShouldBe("测试单位1");
        }

        /// <summary>
        /// The test_ create_ annual inspection unit.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_AnnualInspectionUnit()
        {
            // Arrange
            var name = "测试单位123";

            // Act
            await this._annualInspectionUnitsAppService.CreateOrEdit(
                new CreateOrEditECCPBaseAnnualInspectionUnitDto
                    {
                        Name = name, Addresse = "测试地址2", Telephone = "测试电话2"
                    });

            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
            entity.Addresse.ShouldBe("测试地址2");
            entity.Telephone.ShouldBe("测试电话2");
        }

        /// <summary>
        /// The test_ delete_ annual inspection unit.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_AnnualInspectionUnit()
        {
            var entity = this.GetEntity("测试单位1");

            // Act
            await this._annualInspectionUnitsAppService.Delete(new EntityDto<long>(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ annual inspection unit.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_AnnualInspectionUnit()
        {
            // Arrange                       
            var entity = this.GetEntity("测试单位1");

            // Act
            await this._annualInspectionUnitsAppService.CreateOrEdit(
                new CreateOrEditECCPBaseAnnualInspectionUnitDto
                    {
                        Id = entity.Id, Name = "测试单位2", Addresse = "测试地址2", Telephone = "测试电话2"
                    });

            // Assert
            var newEntity = this.GetEntity(entity.Id);

            newEntity.Name.ShouldBe("测试单位2");
            newEntity.Addresse.ShouldBe("测试地址2");
            newEntity.Telephone.ShouldBe("测试电话2");
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="ECCPBaseAnnualInspectionUnit"/>.
        /// </returns>
        private ECCPBaseAnnualInspectionUnit GetEntity(string name)
        {
            var entity = this.UsingDbContext(
                context => context.ECCPBaseAnnualInspectionUnits.FirstOrDefault(e => e.Name == name));
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
        /// The <see cref="ECCPBaseAnnualInspectionUnit"/>.
        /// </returns>
        private ECCPBaseAnnualInspectionUnit GetEntity(long id)
        {
            var entity = this.UsingDbContext(
                context => context.ECCPBaseAnnualInspectionUnits.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}