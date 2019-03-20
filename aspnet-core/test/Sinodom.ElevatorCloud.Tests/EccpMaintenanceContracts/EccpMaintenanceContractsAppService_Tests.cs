// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceContractsAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpMaintenanceContracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos;
    using Xunit;

    /// <summary>
    /// The eccp maintenance contracts app service_ tests.
    /// </summary>
    public class EccpMaintenanceContractsAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _eccp maintenance contract manager.
        /// </summary>
        private readonly EccpMaintenanceContractManager _eccpMaintenanceContractManager;

        /// <summary>
        /// The _eccp maintenance contracts app service.
        /// </summary>
        private readonly IEccpMaintenanceContractsAppService _eccpMaintenanceContractsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceContractsAppService_Tests"/> class.
        /// </summary>
        public EccpMaintenanceContractsAppService_Tests()
        {
            this.LoginAsDefaultTenantAdmin();
            this._eccpMaintenanceContractsAppService = this.Resolve<IEccpMaintenanceContractsAppService>();
            this._eccpMaintenanceContractManager = this.Resolve<EccpMaintenanceContractManager>();
        }

        /// <summary>
        /// The should_ get_ maintenance contracts.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_MaintenanceContracts()
        {
            var maintenanceCompanyEntity = this.GetMaintenanceCompanyEntity(2);

            // Act
            var output = await this._eccpMaintenanceContractsAppService.GetAll(
                             new GetAllEccpMaintenanceContractsInput
                                 {
                                     ECCPBaseMaintenanceCompanyNameFilter = maintenanceCompanyEntity.Name
                                 });

            output.TotalCount.ShouldBe(1);
            output.Items[0].EccpMaintenanceContract.MaintenanceCompanyId.ShouldBe(maintenanceCompanyEntity.Id);
        }

        /// <summary>
        /// The test_ create_ maintenance contracts.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Theory]
        [InlineData(null, 1, "4811d3f3-0bfa-4672-b875-2d47299d175d.jpg")]
        [InlineData(1, null, "4811d3f3-0bfa-4672-b875-2d47299d175d.jpg")]
        [InlineData(null, null, "4811d3f3-0bfa-4672-b875-2d47299d175d.jpg")]
        [InlineData(1, 1, "4811d3f3-0bfa-4672-b875-2d47299d175d.jpg")]
        public async Task Test_Create_MaintenanceContracts(int propertyCompanyId, int maintenanceCompanyId, string fileToken)
        {
            var elevatorId = this.GetElevatorId("a1111111111");
            var dt = DateTime.Now;

            // Act
            await this._eccpMaintenanceContractsAppService.CreateOrEdit(
                new UploadCreateOrEditEccpMaintenanceContractDto
                    {
                        StartDate = dt,
                        EndDate = dt,
                        PropertyCompanyId = propertyCompanyId,
                        MaintenanceCompanyId = maintenanceCompanyId,
                        ContractPictureId = Guid.NewGuid(),
                        EccpBaseElevatorsIds = elevatorId.ToString(),
                        FileToken = fileToken
                });

            // Assert
            // UsingDbContext(context =>
            // {
            // var entity = context.EccpMaintenanceContracts.FirstOrDefault(e => e.Name == name);
            // entity.ShouldNotBeNull();
            // });
        }

        /// <summary>
        /// The test_ create maintenance contract.
        /// </summary>
        [MultiTenantFact]
        public void Test_CreateMaintenanceContract()
        {
            var elevatorIdList = new List<Guid>();
            var elevatorId = this.GetElevatorId("a1111111111");

            elevatorIdList.Add(elevatorId);

            //// Act
            var result = this._eccpMaintenanceContractManager.CreateMaintenanceContract(elevatorIdList, 1, 1);

            //// Assert
            result.ShouldBeGreaterThan(0);
        }

        /// <summary>
        /// The test_ delete_ maintenance contracts.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Theory]
        [InlineData(null, 1 )]
        [InlineData(1, null)]
        [InlineData(1, 1)]
        public async Task Test_Delete_MaintenanceContracts(int propertyCompanyId, int maintenanceCompanyId)
        {
            var mainEntity = new EccpMaintenanceContract();

            // Arrange
            this.UsingDbContext(
                context =>
                    {
                        mainEntity = context.EccpMaintenanceContracts.Add(
                            new EccpMaintenanceContract
                                {
                                    StartDate = Convert.ToDateTime(DateTime.Now.ToString("2018-9-1")),
                                    EndDate = Convert.ToDateTime(DateTime.Now.ToString("2018-10-1")),
                                    PropertyCompanyId = propertyCompanyId,
                                    MaintenanceCompanyId = maintenanceCompanyId
                            }).Entity;
                    });

            var entity = this.GetEntity(mainEntity.Id);

            //// Act
            await this._eccpMaintenanceContractsAppService.Delete(new EntityDto<long>(entity.Id));

            //// Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ maintenance contracts.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Theory]
        [InlineData(null, 1)]
        [InlineData(1, null)]
        [InlineData(1, 1)]
        public async Task Test_Update_MaintenanceContracts(int propertyCompanyId, int maintenanceCompanyId)
        {
            var maintenanceEntity = new EccpMaintenanceContract();

            // Arrange
            this.UsingDbContext(
                context =>
                    {
                        maintenanceEntity = context.EccpMaintenanceContracts.Add(
                            new EccpMaintenanceContract
                                {
                                    TenantId = 1,
                                    StartDate = Convert.ToDateTime(DateTime.Now.ToString("2018-9-1")),
                                    EndDate = Convert.ToDateTime(DateTime.Now.ToString("2018-10-1")),
                                    MaintenanceCompanyId = maintenanceCompanyId,
                                    PropertyCompanyId = propertyCompanyId
                            }).Entity;
                    });

            var elevatorId = this.GetElevatorId("a1111111111");

            // Act
            await this._eccpMaintenanceContractsAppService.CreateOrEdit(
                new UploadCreateOrEditEccpMaintenanceContractDto
                    {
                        Id = maintenanceEntity.Id,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        PropertyCompanyId = 1,
                        MaintenanceCompanyId = 1,
                        ContractPictureId = Guid.NewGuid(),
                        EccpBaseElevatorsIds = elevatorId.ToString(),
                        FileToken = "4811d3f3-0bfa-4672-b875-2d47299d175d.jpg"
                    });

            // Assert
            // GetEntity(entity.Id).Name.ShouldBe(newName);
        }

        /// <summary>
        /// The get elevator id.
        /// </summary>
        /// <param name="certificateNum">
        /// The certificate num.
        /// </param>
        /// <returns>
        /// The <see cref="Guid"/>.
        /// </returns>
        private Guid GetElevatorId(string certificateNum)
        {
            return this.UsingDbContext(
                context => context.EccpBaseElevators.FirstOrDefault(e => e.CertificateNum == certificateNum)).Id;
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="EccpMaintenanceContract"/>.
        /// </returns>
        private EccpMaintenanceContract GetEntity(long id)
        {
            var entity = this.UsingDbContext(
                context => context.EccpMaintenanceContracts.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }

        /// <summary>
        /// The get maintenance company entity.
        /// </summary>
        /// <param name="tenantId">
        /// The tenant id.
        /// </param>
        /// <returns>
        /// The <see cref="ECCPBaseMaintenanceCompany"/>.
        /// </returns>
        private ECCPBaseMaintenanceCompany GetMaintenanceCompanyEntity(int tenantId)
        {
            var entity = this.UsingDbContext(
                context => context.ECCPBaseMaintenanceCompanies.FirstOrDefault(e => e.TenantId == tenantId));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}