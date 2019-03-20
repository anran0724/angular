// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseRegisterCompaniesAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.ECCPBaseRegisterCompanies
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Dtos;

    /// <summary>
    /// The eccp base register companies app service_ tests.
    /// </summary>
    public class ECCPBaseRegisterCompaniesAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _register companies app service.
        /// </summary>
        private readonly IECCPBaseRegisterCompaniesAppService _registerCompaniesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseRegisterCompaniesAppService_Tests"/> class.
        /// </summary>
        public ECCPBaseRegisterCompaniesAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._registerCompaniesAppService = this.Resolve<ECCPBaseRegisterCompaniesAppService>();
        }

        /// <summary>
        /// The should_ get_ base register companie.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_BaseRegisterCompanie()
        {
            // Arrange
            var entity = this.GetEntity("测试注册单位");

            var output =
                await this._registerCompaniesAppService.GetECCPBaseRegisterCompanyForEdit(
                    new EntityDto<long>(entity.Id));

            output.ECCPBaseRegisterCompany.Name.ShouldBe("测试注册单位");
        }

        /// <summary>
        /// The should_ get_ base register companies.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_BaseRegisterCompanies()
        {
            // Arrange          
            var entities = await this._registerCompaniesAppService.GetAll(
                               new GetAllECCPBaseRegisterCompaniesInput { Filter = "测试注册单位" });
            entities.Items.Count.ShouldBe(1);
            entities.Items[0].ECCPBaseRegisterCompany.Name.ShouldBe("测试注册单位");
        }

        /// <summary>
        /// The test_ create_ base register companie.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_BaseRegisterCompanie()
        {
            // Arrange
            var name = "测试注册单位1";

            // Act
            await this._registerCompaniesAppService.CreateOrEdit(
                new CreateOrEditECCPBaseRegisterCompanyDto { Name = name, Addresse = "测试地址2", Telephone = "测试电话2" });

            // Assert
            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
            entity.Addresse.ShouldBe("测试地址2");
            entity.Telephone.ShouldBe("测试电话2");
        }

        /// <summary>
        /// The test_ delete_ base register companie.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_BaseRegisterCompanie()
        {
            // Arrange
            var entity = this.GetEntity("测试注册单位");

            // Act
            await this._registerCompaniesAppService.Delete(new EntityDto<long>(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ base register companie.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_BaseRegisterCompanie()
        {
            // Arrange
            var entity = this.GetEntity("测试注册单位");
            var newName = "测试单位3";

            // Act
            await this._registerCompaniesAppService.CreateOrEdit(
                new CreateOrEditECCPBaseRegisterCompanyDto
                    {
                        Id = entity.Id, Name = newName, Addresse = "测试地址3", Telephone = "测试电话3"
                    });

            // Assert
            var newEntity = this.GetEntity(entity.Id);

            newEntity.Name.ShouldBe(newName);
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
        /// The <see cref="ECCPBaseRegisterCompany"/>.
        /// </returns>
        private ECCPBaseRegisterCompany GetEntity(string name)
        {
            var entity = this.UsingDbContext(
                context => context.ECCPBaseRegisterCompanies.FirstOrDefault(e => e.Name == name));
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
        /// The <see cref="ECCPBaseRegisterCompany"/>.
        /// </returns>
        private ECCPBaseRegisterCompany GetEntity(long id)
        {
            var entity = this.UsingDbContext(
                context => context.ECCPBaseRegisterCompanies.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}