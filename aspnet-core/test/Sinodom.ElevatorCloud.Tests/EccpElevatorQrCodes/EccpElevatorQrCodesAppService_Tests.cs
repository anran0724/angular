// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpElevatorQrCodesAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpElevatorQrCodes
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.EccpElevatorQrCodes;
    using Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos;

    /// <summary>
    /// The eccp elevator qr codes app service_ tests.
    /// </summary>
    public class EccpElevatorQrCodesAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _eccp elevator qr codes excel exporter.
        /// </summary>
        private readonly IEccpElevatorQrCodesAppService _eccpElevatorQrCodesExcelExporter;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpElevatorQrCodesAppService_Tests"/> class.
        /// </summary>
        public EccpElevatorQrCodesAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._eccpElevatorQrCodesExcelExporter = this.Resolve<IEccpElevatorQrCodesAppService>();
        }

        /// <summary>
        /// The should_ edit_ binding.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Edit_Binding()
        {
            var oldAreaName = "黑";
            var oldElevatorNum = "999999";
            var newAreaName = "辽";
            var newElevatorNum = "999999";

            var entity = this.GetEntity(oldAreaName, oldElevatorNum);
            var entities = this.GetEntity(newAreaName, newElevatorNum);

            await this._eccpElevatorQrCodesExcelExporter.Binding(
                new CreateOrEditEccpElevatorQrCodeDto
                    {
                        Id = entities.Id,
                        AreaName = oldAreaName,
                        ElevatorNum = oldElevatorNum,
                        IsInstall = false,
                        IsGrant = false,
                        ElevatorId = entity.ElevatorId
                    });

            this.GetEntity(entities.Id).ElevatorId.ShouldBe(entity.ElevatorId);
        }

        /// <summary>
        /// The should_ edit_ modify.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Edit_Modify()
        {
            var oldAreaName = "黑";
            var oldElevatorNum = "999999";
            var newAreaName = "辽";
            var newElevatorNum = "999999";

            var entity = this.GetEntity(oldAreaName, oldElevatorNum);
            var entities = this.GetEntity(newAreaName, newElevatorNum);

            await this._eccpElevatorQrCodesExcelExporter.Modify(
                new CreateOrEditEccpElevatorQrCodeDto
                    {
                        Id = entity.Id,
                        AreaName = newAreaName,
                        ElevatorNum = newElevatorNum,
                        IsInstall = false,
                        IsGrant = false,
                        ElevatorId = entities.ElevatorId
                    });

            var newEntity = this.GetEntity(entity.Id);

            newEntity.IsInstall.ShouldBe(false);
            newEntity.IsGrant.ShouldBe(false);
            newEntity.ElevatorId.ShouldBe(entities.ElevatorId);
        }

        /// <summary>
        /// The should_ edit_ modify eccp.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Edit_ModifyEccp()
        {
            var oldAreaName = "黑";
            var oldElevatorNum = "999999";
            var newAreaName = "辽";
            var newElevatorNum = "999999";

            var entity = this.GetEntity(oldAreaName, oldElevatorNum);
            var entitys = this.GetEntity(newAreaName, newElevatorNum);

            await this._eccpElevatorQrCodesExcelExporter.ModifyEccp(
                new CreateOrEditEccpElevatorQrCodeDto
                    {
                        Id = entitys.Id,
                        AreaName = oldAreaName,
                        ElevatorNum = oldElevatorNum,
                        IsInstall = false,
                        IsGrant = false,
                        ElevatorId = entity.ElevatorId
                    });

            var newEntity = this.GetEntity(entitys.Id);

            // newEntity.AreaName.ShouldBe(oldAreaName);
            // newEntity.ElevatorNum.ShouldBe(oldElevatorNum);
            newEntity.IsInstall.ShouldBe(false);
            newEntity.IsGrant.ShouldBe(false);
            newEntity.ElevatorId.ShouldBe(entity.ElevatorId);
        }

        /// <summary>
        /// The should_ edit_ modify qr code.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Edit_ModifyQRCode()
        {
            var oldAreaName = "黑";
            var oldElevatorNum = "999999";
            var newAreaName = "辽";
            var newElevatorNum = "999999";

            var entity = this.GetEntity(oldAreaName, oldElevatorNum);
            var entities = this.GetEntity(newAreaName, newElevatorNum);

            await this._eccpElevatorQrCodesExcelExporter.ModifyQRCode(
                new CreateOrEditEccpElevatorQrCodeDto
                    {
                        Id = entity.Id,
                        AreaName = newAreaName,
                        ElevatorNum = newElevatorNum,
                        IsInstall = false,
                        IsGrant = false,
                        ElevatorId = entities.ElevatorId
                    });

            var newEntity = this.GetEntity(entity.Id);

            newEntity.IsInstall.ShouldBe(false);
            newEntity.IsGrant.ShouldBe(false);
            newEntity.ElevatorId.ShouldBe(null);
        }

        /// <summary>
        /// The should_ get_place types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_placeTypes()
        {
            var output =
                await this._eccpElevatorQrCodesExcelExporter.GetAll(
                    new GetAllEccpElevatorQrCodesInput { Filter = "黑" });
            output.TotalCount.ShouldBe(1);
            output.Items[0].EccpElevatorQrCode.AreaName.ShouldBe("黑");
        }

        /// <summary>
        /// The test_ delete_maintenance types.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_maintenanceTypes()
        {
            var areaName = "黑";
            var elevatorNum = "999999";

            var entity = this.GetEntity(areaName, elevatorNum);

            // Act
            await this._eccpElevatorQrCodesExcelExporter.Delete(new EntityDto<Guid>(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ create or edit.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_CreateOrEdit()
        {
            var dt = DateTime.Now;
            var areaName = "黑";
            var elevatorNum = "999999";
            var entity = this.GetEntity(areaName, elevatorNum);
            var elevatorEntity = this.GetElevatorEntity("测试电梯5");
            await this._eccpElevatorQrCodesExcelExporter.CreateOrEdit(
                new CreateOrEditEccpElevatorQrCodeDto
                    {
                        Id = entity.Id,
                        AreaName = areaName,
                        ElevatorNum = elevatorNum,
                        IsGrant = true,
                        IsInstall = true,
                        GrantDateTime = dt,
                        InstallDateTime = dt,
                        ElevatorId = elevatorEntity.Id
                    });
            var newEntity = this.GetEntity(entity.Id);
            newEntity.IsInstall.ShouldBe(true);
            newEntity.IsGrant.ShouldBe(true);
        }

        /// <summary>
        /// The get elevator entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpBaseElevator"/>.
        /// </returns>
        private EccpBaseElevator GetElevatorEntity(string name)
        {
            var entity = this.UsingDbContext(context => context.EccpBaseElevators.FirstOrDefault(e => e.Name == name));
            entity.ShouldNotBeNull();

            return entity;
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="areaName">
        /// The area name.
        /// </param>
        /// <param name="elevatorNum">
        /// The elevator num.
        /// </param>
        /// <returns>
        /// The <see cref="EccpElevatorQrCode"/>.
        /// </returns>
        private EccpElevatorQrCode GetEntity(string areaName, string elevatorNum)
        {
            var entity = this.UsingDbContext(
                context => context.EccpElevatorQrCodes.FirstOrDefault(
                    e => e.AreaName == areaName && e.ElevatorNum == elevatorNum));
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
        /// The <see cref="EccpElevatorQrCode"/>.
        /// </returns>
        private EccpElevatorQrCode GetEntity(Guid id)
        {
            var entity = this.UsingDbContext(context => context.EccpElevatorQrCodes.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}