// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorsAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpBaseElevators
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;

    /// <summary>
    /// The eccp base elevators app service_ tests.
    /// </summary>
    public class EccpBaseElevatorsAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _ eccp base elevators app service.
        /// </summary>
        private readonly IEccpBaseElevatorsAppService _eccpBaseElevatorsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorsAppService_Tests"/> class.
        /// </summary>
        public EccpBaseElevatorsAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._eccpBaseElevatorsAppService = this.Resolve<IEccpBaseElevatorsAppService>();
        }

        /// <summary>
        /// The should_ get_ eccp base elevator.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_EccpBaseElevator()
        {
            var entities = await this._eccpBaseElevatorsAppService.GetAll(
                               new GetAllEccpBaseElevatorsInput { Filter = "测试电梯4" });
            entities.Items.Count.ShouldBe(1);
            entities.Items[0].EccpBaseElevator.Name.ShouldBe("测试电梯4");
        }

        /// <summary>
        /// The test_ create_ eccp base elevator.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_EccpBaseElevator()
        {
            // Arrange
            var name = "测试电梯11";
            var elevatorSubDto = new CreateOrEditEccpBaseElevatorSubsidiaryInfoDto
                                     {
                                         CustomNum = name,
                                         ManufacturingLicenseNumber = "100000001",
                                         FloorNumber = 2,
                                         GateNumber = 2,
                                         RatedSpeed = 2,
                                         Deadweight = 2
                                     };
            var elevatorDto = new CreateOrEditEccpBaseElevatorDto
                                  {
                                      Name = name,
                                      CertificateNum = "100000001",
                                      MachineNum = "MachineNum",
                                      InstallationAddress = "InstallationAddress",
                                      InstallationDatetime = new DateTime().Date,
                                      Latitude = "123",
                                      Longitude = "123213",
                                      createOrEditEccpBaseElevatorSubsidiaryInfoDto = elevatorSubDto
                                  };

            // Act
            await this._eccpBaseElevatorsAppService.CreateOrEdit(elevatorDto);

            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
            entity.CertificateNum.ShouldBe("100000001");
            entity.MachineNum.ShouldBe("MachineNum");
            entity.InstallationAddress.ShouldBe("InstallationAddress");
            entity.Latitude.ShouldBe("123");
            entity.Longitude.ShouldBe("123213");
        }

        /// <summary>
        /// The test_ delete_ eccp base elevator.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_EccpBaseElevator()
        {
            // Arrange           
            var entity = new EccpBaseElevator();

            // Assert
            this.UsingDbContext(
                context =>
                    {
                        entity = this.GetEntity("测试电梯5");

                        context.EccpBaseElevatorSubsidiaryInfos.Add(
                            new EccpBaseElevatorSubsidiaryInfo
                                {
                                    CustomNum = "100002",
                                    ManufacturingLicenseNumber = "100000001",
                                    FloorNumber = 2,
                                    GateNumber = 2,
                                    RatedSpeed = 2,
                                    Deadweight = 2,
                                    ElevatorId = entity.Id
                                });
                    });

            // Act
            await this._eccpBaseElevatorsAppService.Delete(new EntityDto<Guid>(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ eccp base elevator.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_EccpBaseElevator()
        {
            // Arrange           
            var newName = "测试电梯8";

            var entity = this.GetEntity("测试电梯1");

            var elevatorSubDtoUp = new CreateOrEditEccpBaseElevatorSubsidiaryInfoDto
                                       {
                                           CustomNum = "100002",
                                           ManufacturingLicenseNumber = "111",
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
            
            // Assert
            this.UsingDbContext(
               context =>
               {
                   var log = context.EccpElevatorChangeLogs.FirstOrDefault(e=>e.ElevatorId==entity.Id);
                   log.FieldName.ShouldBe("Name");                  

               });
            var newEntity = this.GetEntity(entity.Id);
            newEntity.Name.ShouldBe(newName);
            newEntity.CertificateNum.ShouldBe("100000002");
            newEntity.MachineNum.ShouldBe("11111111111");
            newEntity.InstallationAddress.ShouldBe("地址");
            newEntity.Latitude.ShouldBe("124");
            newEntity.Longitude.ShouldBe("125");
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

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="EccpBaseElevator"/>.
        /// </returns>
        private EccpBaseElevator GetEntity(Guid id)
        {
            var entity = this.UsingDbContext(context => context.EccpBaseElevators.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}