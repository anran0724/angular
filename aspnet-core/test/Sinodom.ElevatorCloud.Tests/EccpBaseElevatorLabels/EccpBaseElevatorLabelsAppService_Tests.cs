// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorLabelsAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpBaseElevatorLabels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Microsoft.EntityFrameworkCore;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpBaseElevatorLabels;
    using Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos;
    using Sinodom.ElevatorCloud.EccpBaseElevators;

    /// <summary>
    /// The eccp base elevator labels app service_ tests.
    /// </summary>
    public class EccpBaseElevatorLabelsAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _eccp base elevator labels app service.
        /// </summary>
        private readonly IEccpBaseElevatorLabelsAppService _eccpBaseElevatorLabelsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorLabelsAppService_Tests"/> class.
        /// </summary>
        public EccpBaseElevatorLabelsAppService_Tests()
        {
            this.LoginAsDefaultTenantAdmin();
            this._eccpBaseElevatorLabelsAppService = this.Resolve<EccpBaseElevatorLabelsAppService>();
        }

        /// <summary>
        /// The test_ discontinue use_ base elevator labels.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_DiscontinueUse_BaseElevatorLabels()
        {
            // Arrange
            var entity = this.GetEntity("制动器");

            // Act
            await this._eccpBaseElevatorLabelsAppService.DiscontinueUse(new EntityDto<long>(entity.Id));

            // Assert
            this.GetEntity(entity.Id).LabelStatus.Name.ShouldBe("已失效");
        }

        /// <summary>
        /// The test_ elevator label bind_ base elevator labels.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_ElevatorLabelBind_BaseElevatorLabels()
        {
            var elevator = new EccpBaseElevator();

            this.UsingDbContext(
                context => { elevator = context.EccpBaseElevators.FirstOrDefault(e => e.Name == "测试电梯5"); });

            // Act
            var result = await this._eccpBaseElevatorLabelsAppService.ElevatorLabelBind(
                             new CreateOrEditEccpBaseElevatorLabelDto
                                 {
                                     LabelName = "限速器",
                                     UniqueId = "432423432",
                                     LocalInformation = "432432432432",
                                     BindingTime = DateTime.Now,
                                     BinaryObjectsId = Guid.NewGuid(),
                                     ElevatorId = elevator.Id
                                 });

            // Assert
            result.ShouldBeGreaterThan(0);
        }

        /// <summary>
        /// The test_ elevator label initialization_ base elevator labels.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_ElevatorLabelInitialization_BaseElevatorLabels()
        {
            // Act
            var result = await this._eccpBaseElevatorLabelsAppService.ElevatorLabelInitialization(
                             new CreateOrEditEccpBaseElevatorLabelDto
                                 {
                                     LabelName = "限速器1",
                                     UniqueId = "4324234321",
                                     LocalInformation = "4324324324321",
                                     BindingTime = DateTime.Now,
                                     BinaryObjectsId = Guid.NewGuid()
                                 });

            // Assert
            result.ShouldBeGreaterThan(0);
        }

        /// <summary>
        /// The test_ get all_ base elevator labels.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_GetAll_BaseElevatorLabels()
        {
            var output = await this._eccpBaseElevatorLabelsAppService.GetAll(
                             new GetAllEccpBaseElevatorLabelsInput { EccpDictLabelStatusNameFilter = "未绑定" });

            output.TotalCount.ShouldBe(1);
            output.Items[0].EccpDictLabelStatusName.ShouldBe("未绑定");
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="EccpBaseElevatorLabel"/>.
        /// </returns>
        private EccpBaseElevatorLabel GetEntity(long id)
        {
            var entity = this.UsingDbContext(
                context => context.EccpBaseElevatorLabels.Include(t => t.LabelStatus).FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpBaseElevatorLabel"/>.
        /// </returns>
        private EccpBaseElevatorLabel GetEntity(string name)
        {
            var entity = this.UsingDbContext(
                context => context.EccpBaseElevatorLabels.FirstOrDefault(e => e.LabelName == name));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}