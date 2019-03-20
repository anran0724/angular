// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseProductionCompaniesAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.ECCPBaseProductionCompanies
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies.Dtos;

    /// <summary>
    /// The eccp base production companies app service_ tests.
    /// </summary>
    public class ECCPBaseProductionCompaniesAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _production companies app service.
        /// </summary>
        private readonly IECCPBaseProductionCompaniesAppService _productionCompaniesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseProductionCompaniesAppService_Tests"/> class.
        /// </summary>
        public ECCPBaseProductionCompaniesAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._productionCompaniesAppService = this.Resolve<IECCPBaseProductionCompaniesAppService>();
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
            var entity = this.GetEntity("测试制造单位1");

            var output =
                await this._productionCompaniesAppService.GetECCPBaseProductionCompanyForEdit(
                    new EntityDto<long>(entity.Id));

            output.ECCPBaseProductionCompany.Name.ShouldBe("测试制造单位1");
            output.ProvinceName.ShouldBe("海南省");
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
            var entities = await this._productionCompaniesAppService.GetAll(
                               new GetAllECCPBaseProductionCompaniesInput { Filter = "测试制造单位1" });
            entities.Items.Count.ShouldBe(1);
            entities.Items[0].ProvinceName.ShouldBe("海南省");
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
            var name = "测试制造单位2";

            // Act
            await this._productionCompaniesAppService.CreateOrEdit(
                new CreateOrEditECCPBaseProductionCompanyDto { Name = name, Addresse = "测试地址2", Telephone = "测试电话2" });

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
            var entity = this.GetEntity("测试制造单位1");

            // Act
            await this._productionCompaniesAppService.Delete(new EntityDto<long>(entity.Id));

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
            var entity = this.GetEntity("测试制造单位1");
            var newName = "测试制造单位3";

            // Act
            await this._productionCompaniesAppService.CreateOrEdit(
                new CreateOrEditECCPBaseProductionCompanyDto
                    {
                        Id = entity.Id, Name = newName, Addresse = "测试地址3", Telephone = "测试电话3"
                    });

            // Assert
            var newEntity = this.GetEntity(entity.Id);

            newEntity.Name.ShouldBe("测试制造单位3");
            newEntity.Addresse.ShouldBe("测试地址3");
            newEntity.Telephone.ShouldBe("测试电话3");
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="ECCPBaseProductionCompany"/>.
        /// </returns>
        private ECCPBaseProductionCompany GetEntity(string name)
        {
            var entity = this.UsingDbContext(
                context => context.ECCPBaseProductionCompanies.FirstOrDefault(e => e.Name == name));
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
        /// The <see cref="ECCPBaseProductionCompany"/>.
        /// </returns>
        private ECCPBaseProductionCompany GetEntity(long id)
        {
            var entity = this.UsingDbContext(
                context => context.ECCPBaseProductionCompanies.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}