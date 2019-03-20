// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseMaintenanceCompanies_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.ECCPBaseMaintenanceCompanies
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos;

    /// <summary>
    /// The eccp base maintenance companies_ tests.
    /// </summary>
    public class ECCPBaseMaintenanceCompanies_Tests : AppTestBase
    {
        /// <summary>
        /// The _maintenance companies app service.
        /// </summary>
        private readonly IECCPBaseMaintenanceCompaniesAppService _maintenanceCompaniesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseMaintenanceCompanies_Tests"/> class.
        /// </summary>
        public ECCPBaseMaintenanceCompanies_Tests()
        {
            this.LoginAsHostAdmin();
            this._maintenanceCompaniesAppService = this.Resolve<IECCPBaseMaintenanceCompaniesAppService>();
        }

        /// <summary>
        /// The test_ create_ maintenance companies.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_MaintenanceCompanies()
        {
            // Arrange
            var name = "测试维保";

            // Act
            await this._maintenanceCompaniesAppService.CreateOrEdit(
                new CreateOrEditECCPBaseMaintenanceCompanyDto
                    {
                        Name = name,
                        Addresse = "Addresse",
                        Longitude = "Longitude",
                        Latitude = "Latitude",
                        Telephone = "Telephone",
                        OrgOrganizationalCode = "111"
                    });

            // Assert
            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
            entity.Addresse.ShouldBe("Addresse");
            entity.Longitude.ShouldBe("Longitude");
            entity.Latitude.ShouldBe("Latitude");
            entity.Telephone.ShouldBe("Telephone");
            entity.OrgOrganizationalCode.ShouldBe("111");
        }

        /// <summary>
        /// The test_ delete_ maintenance companies.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_MaintenanceCompanies()
        {
            // Arrange
            var entity = this.GetEntity("测试维保单位1");

            // Act
            await this._maintenanceCompaniesAppService.Delete(new EntityDto(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ maintenance companies.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_MaintenanceCompanies()
        {
            // Arrange
            var entity = this.GetEntity("测试维保单位1");
            var newName = "测试维保单位2";

            // Act
            await this._maintenanceCompaniesAppService.CreateOrEdit(
                new CreateOrEditECCPBaseMaintenanceCompanyDto
                    {
                        Id = entity.Id,
                        Name = newName,
                        Addresse = "Addresse2",
                        Longitude = "Longitude2",
                        Latitude = "Latitude2",
                        Telephone = "Telephone2",
                        OrgOrganizationalCode = "222"
                    });

            // Assert
            var newEntity = this.GetEntity(entity.Id);

            newEntity.Name.ShouldBe(newName);
            newEntity.Addresse.ShouldBe("Addresse2");
            newEntity.Longitude.ShouldBe("Longitude2");
            newEntity.Latitude.ShouldBe("Latitude2");
            newEntity.Telephone.ShouldBe("Telephone2");
            newEntity.OrgOrganizationalCode.ShouldBe("222");
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="ECCPBaseMaintenanceCompany"/>.
        /// </returns>
        private ECCPBaseMaintenanceCompany GetEntity(string name)
        {
            var entity = this.UsingDbContext(
                context => context.ECCPBaseMaintenanceCompanies.FirstOrDefault(e => e.Name == name));
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
        /// The <see cref="ECCPBaseMaintenanceCompany"/>.
        /// </returns>
        private ECCPBaseMaintenanceCompany GetEntity(int id)
        {
            var entity = this.UsingDbContext(
                context => context.ECCPBaseMaintenanceCompanies.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}