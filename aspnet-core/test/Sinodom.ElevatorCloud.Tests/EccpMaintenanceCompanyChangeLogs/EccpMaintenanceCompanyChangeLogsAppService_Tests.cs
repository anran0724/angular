// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceCompanyChangeLogsAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpMaintenanceCompanyChangeLogs
{
    using System.Linq;
    using System.Threading.Tasks;

    using Shouldly;

    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos;

    /// <summary>
    /// The eccp maintenance company change logs app service_ tests.
    /// </summary>
    public class EccpMaintenanceCompanyChangeLogsAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _maintenance companies app service.
        /// </summary>
        private readonly IECCPBaseMaintenanceCompaniesAppService _maintenanceCompaniesAppService;

        /// <summary>
        /// The _maintenance company change logs app service.
        /// </summary>
        private readonly IEccpMaintenanceCompanyChangeLogsAppService _maintenanceCompanyChangeLogsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceCompanyChangeLogsAppService_Tests"/> class.
        /// </summary>
        public EccpMaintenanceCompanyChangeLogsAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._maintenanceCompaniesAppService = this.Resolve<IECCPBaseMaintenanceCompaniesAppService>();
            this._maintenanceCompanyChangeLogsAppService = this.Resolve<IEccpMaintenanceCompanyChangeLogsAppService>();
        }

        /// <summary>
        /// The should_ get_ eccp maintenance company change logs.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_EccpMaintenanceCompanyChangeLogs()
        {
            // Arrange
            var oldName = "测试维保公司1";
            var newName = "测试维保公司2";

            await this._maintenanceCompaniesAppService.CreateOrEdit(
                new CreateOrEditECCPBaseMaintenanceCompanyDto
                    {
                        Name = oldName,
                        Addresse = "测试地址1",
                        Telephone = "测试电话1",
                        Latitude = "123",
                        Longitude = "123",
                        OrgOrganizationalCode = "111"
                    });

            var entity = this.GetEntity(oldName);

            // Act
            await this._maintenanceCompaniesAppService.CreateOrEdit(
                new CreateOrEditECCPBaseMaintenanceCompanyDto
                    {
                        Id = entity.Id,
                        Name = newName,
                        Addresse = "测试地址2",
                        Telephone = "测试电话2",
                        Latitude = "123",
                        Longitude = "123",
                        OrgOrganizationalCode = "111"
                    });

            var entities = await this._maintenanceCompanyChangeLogsAppService.GetAll(
                               new GetAllEccpMaintenanceCompanyChangeLogsInput { Filter = newName });

            entities.Items.Count.ShouldBe(1);
            entities.Items[0].ECCPBaseMaintenanceCompanyName.ShouldBe(newName);
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
    }
}