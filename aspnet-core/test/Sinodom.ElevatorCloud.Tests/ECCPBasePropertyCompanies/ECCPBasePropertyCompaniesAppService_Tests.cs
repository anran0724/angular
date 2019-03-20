// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBasePropertyCompaniesAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.ECCPBasePropertyCompanies
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos;

    /// <summary>
    /// The eccp base property companies app service_ tests.
    /// </summary>
    public class ECCPBasePropertyCompaniesAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _ eccp base property companies app service.
        /// </summary>
        private readonly IECCPBasePropertyCompaniesAppService _eccpBasePropertyCompaniesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBasePropertyCompaniesAppService_Tests"/> class.
        /// </summary>
        public ECCPBasePropertyCompaniesAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._eccpBasePropertyCompaniesAppService = this.Resolve<IECCPBasePropertyCompaniesAppService>();
        }

        /// <summary>
        /// The should_ get_ property companies.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_PropertyCompanies()
        {
            var entities = await this._eccpBasePropertyCompaniesAppService.GetAll(
                               new GetAllECCPBasePropertyCompaniesInput { Filter = "宿主测试使用单位" });
            entities.Items.Count.ShouldBe(1);
            entities.Items[0].ECCPBasePropertyCompany.Name.ShouldBe("宿主测试使用单位");
        }

        /// <summary>
        /// The test_ create_ property companies.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_PropertyCompanies()
        {
            // Arrange
            var name = "测试物业公司";

            // Act
            await this._eccpBasePropertyCompaniesAppService.CreateOrEdit(
                new CreateOrEditECCPBasePropertyCompanyDto
                    {
                        Name = name,
                        Addresse = "测试地址",
                        Telephone = "测试电话",
                        Latitude = "123",
                        Longitude = "1234",
                        OrgOrganizationalCode = "111"
                    });

            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
            entity.Addresse.ShouldBe("测试地址");
            entity.Telephone.ShouldBe("测试电话");
            entity.Latitude.ShouldBe("123");
            entity.Longitude.ShouldBe("1234");
            entity.OrgOrganizationalCode.ShouldBe("111");
        }

        /// <summary>
        /// The test_ delete_ property companies.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_PropertyCompanies()
        {
            // Arrange
            var entity = this.GetEntity("宿主测试使用单位");

            // Act
            await this._eccpBasePropertyCompaniesAppService.Delete(new EntityDto(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ property companies.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_PropertyCompanies()
        {
            // Arrange
            var entity = this.GetEntity("宿主测试使用单位");
            var newName = "测试物业公司2";

            // Act
            await this._eccpBasePropertyCompaniesAppService.CreateOrEdit(
                new CreateOrEditECCPBasePropertyCompanyDto
                    {
                        Id = entity.Id,
                        Name = newName,
                        Addresse = "测试地址2",
                        Telephone = "测试电话2",
                        Latitude = "Latitude",
                        Longitude = "Longitude",
                        OrgOrganizationalCode = "OrgOrganizationalCode"
                    });

            // Assert
            var newEntity = this.GetEntity(entity.Id);
            newEntity.Name.ShouldBe(newName);
            newEntity.Addresse.ShouldBe("测试地址2");
            newEntity.Telephone.ShouldBe("测试电话2");
            newEntity.Latitude.ShouldBe("Latitude");
            newEntity.Longitude.ShouldBe("Longitude");
            newEntity.OrgOrganizationalCode.ShouldBe("OrgOrganizationalCode");
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="ECCPBasePropertyCompany"/>.
        /// </returns>
        private ECCPBasePropertyCompany GetEntity(string name)
        {
            var entity = this.UsingDbContext(
                context => context.ECCPBasePropertyCompanies.FirstOrDefault(e => e.Name == name));
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
        /// The <see cref="ECCPBasePropertyCompany"/>.
        /// </returns>
        private ECCPBasePropertyCompany GetEntity(long id)
        {
            var entity = this.UsingDbContext(
                context => context.ECCPBasePropertyCompanies.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}