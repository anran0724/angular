// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpElevatorChangeLogsAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpElevatorChangeLogs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;

    /// <summary>
    /// The eccp elevator change logs app service_ tests.
    /// </summary>
    public class EccpElevatorChangeLogsAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _ eccp base elevators app service.
        /// </summary>
        private readonly IEccpBaseElevatorsAppService _eccpBaseElevatorsAppService;

        /// <summary>
        /// The _ eccp elevator change logs app service.
        /// </summary>
        private readonly IEccpElevatorChangeLogsAppService _eccpElevatorChangeLogsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpElevatorChangeLogsAppService_Tests"/> class.
        /// </summary>
        public EccpElevatorChangeLogsAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._eccpBaseElevatorsAppService = this.Resolve<IEccpBaseElevatorsAppService>();
            this._eccpElevatorChangeLogsAppService = this.Resolve<IEccpElevatorChangeLogsAppService>();
        }

        /// <summary>
        /// The should_ get_ eccp elevator change logs.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_EccpElevatorChangeLogs()
        {
            // Arrange
            var name = "测试电梯1";
            var newName = "测试电梯2";
            var manufacturingLicenseNumber = "100000001";
            var newmManufacturingLicenseNumber = "100000002";
            var elevatorSubDto = new CreateOrEditEccpBaseElevatorSubsidiaryInfoDto
                                     {
                                         CustomNum = "100001",
                                         ManufacturingLicenseNumber = manufacturingLicenseNumber,
                                         FloorNumber = 2,
                                         GateNumber = 2,
                                         RatedSpeed = 2,
                                         Deadweight = 2
                                     };
            var elevatorDto = new CreateOrEditEccpBaseElevatorDto
                                  {
                                      Name = name,
                                      CertificateNum = "100000001",
                                      MachineNum = "123123123213",
                                      InstallationAddress = "asdsadasd",
                                      InstallationDatetime = new DateTime().Date,
                                      Latitude = "123",
                                      Longitude = "123213",
                                      createOrEditEccpBaseElevatorSubsidiaryInfoDto = elevatorSubDto
                                  };

            await this._eccpBaseElevatorsAppService.CreateOrEdit(elevatorDto);

            var entity = this.GetEntity(name);

            var elevatorSubDtoUp = new CreateOrEditEccpBaseElevatorSubsidiaryInfoDto
                                       {
                                           CustomNum = "100002",
                                           ManufacturingLicenseNumber = newmManufacturingLicenseNumber,
                                           FloorNumber = 3,
                                           GateNumber = 3,
                                           RatedSpeed = 3,
                                           Deadweight = 3
                                       };
            var elevatorDtoUp = new CreateOrEditEccpBaseElevatorDto
                                    {
                                        Id = entity.Id,
                                        Name = newName,
                                        CertificateNum = "100000002",
                                        MachineNum = "11111111111",
                                        InstallationAddress = "地址",
                                        InstallationDatetime = new DateTime().Date,
                                        Latitude = "124",
                                        Longitude = "125",
                                        createOrEditEccpBaseElevatorSubsidiaryInfoDto = elevatorSubDtoUp
                                    };

            // Act
            await this._eccpBaseElevatorsAppService.CreateOrEdit(elevatorDtoUp);

            var entities =
                await this._eccpElevatorChangeLogsAppService.GetAll(
                    new GetAllEccpElevatorChangeLogsInput { Filter = newName });
            entities.Items.Count.ShouldBe(1);
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpBaseElevator"/>.
        /// </returns>
        private EccpBaseElevator GetEntity(string name)
        {
            var entity = this.UsingDbContext(context => context.EccpBaseElevators.FirstOrDefault(e => e.Name == name));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}