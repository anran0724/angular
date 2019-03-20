// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpPropertyCompanyChangeLogsAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpPropertyCompanyChangeLogs
{
    using System.Linq;
    using System.Threading.Tasks;

    using Shouldly;

    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos;

    /// <summary>
    /// The eccp property company change logs app service_ tests.
    /// </summary>
    public class EccpPropertyCompanyChangeLogsAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _ eccp base property companies app service.
        /// </summary>
        private readonly IECCPBasePropertyCompaniesAppService _eccpBasePropertyCompaniesAppService;

        /// <summary>
        /// The _ eccp property company change logs app service.
        /// </summary>
        private readonly IEccpPropertyCompanyChangeLogsAppService _eccpPropertyCompanyChangeLogsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpPropertyCompanyChangeLogsAppService_Tests"/> class.
        /// </summary>
        public EccpPropertyCompanyChangeLogsAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._eccpBasePropertyCompaniesAppService = this.Resolve<IECCPBasePropertyCompaniesAppService>();
            this._eccpPropertyCompanyChangeLogsAppService = this.Resolve<IEccpPropertyCompanyChangeLogsAppService>();
        }

        /// <summary>
        /// The should_ get_ eccp property company change logs.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_EccpPropertyCompanyChangeLogs()
        {
            // Arrange
            var oldName = "测试物业公司1";
            var newName = "测试物业公司2";

            await this._eccpBasePropertyCompaniesAppService.CreateOrEdit(
                new CreateOrEditECCPBasePropertyCompanyDto
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
            await this._eccpBasePropertyCompaniesAppService.CreateOrEdit(
                new CreateOrEditECCPBasePropertyCompanyDto
                    {
                        Id = entity.Id,
                        Name = newName,
                        Addresse = "测试地址2",
                        Telephone = "测试电话2",
                        Latitude = "123",
                        Longitude = "123",
                        OrgOrganizationalCode = "111"
                    });

            var entities = await this._eccpPropertyCompanyChangeLogsAppService.GetAll(
                               new GetAllEccpPropertyCompanyChangeLogsInput { Filter = newName });
            entities.Items.Count.ShouldBe(1);
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
    }
}