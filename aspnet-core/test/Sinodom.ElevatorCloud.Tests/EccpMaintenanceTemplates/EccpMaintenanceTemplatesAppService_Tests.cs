// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTemplatesAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpMaintenanceTemplates
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Dtos;

    /// <summary>
    /// The eccp maintenance templates app service_ tests.
    /// </summary>
    public class EccpMaintenanceTemplatesAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _maintenance templates app service.
        /// </summary>
        private readonly IEccpMaintenanceTemplatesAppService _maintenanceTemplatesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTemplatesAppService_Tests"/> class.
        /// </summary>
        public EccpMaintenanceTemplatesAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._maintenanceTemplatesAppService = this.Resolve<IEccpMaintenanceTemplatesAppService>();
        }

        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_AnnualInspectionUnit()
        {
            var tempName = "单元测试月度维保模板";
            var entity = this.GetEntity(tempName);

            var output =
                await this._maintenanceTemplatesAppService.GetEccpMaintenanceTemplateForEdit(new EntityDto(entity.Id));

            output.EccpMaintenanceTemplate.TempName.ShouldBe(tempName);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_AnnualInspectionUnits()
        {
            var entities = await this._maintenanceTemplatesAppService.GetAll(
                               new GetAllEccpMaintenanceTemplatesInput { Filter = "单元测试月度维保模板" });
            entities.Items.Count.ShouldBe(1);
            entities.Items[0].EccpMaintenanceTemplate.TempName.ShouldBe("单元测试月度维保模板");
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create_AnnualInspectionUnit()
        {
            // Arrange            
            var tempName = "测试模板";

            // Act
            await this._maintenanceTemplatesAppService.CreateOrEdit(
                new CreateOrEditEccpMaintenanceTemplateDto
                    {
                        TempName = tempName,
                        TempDesc = "模板描述",
                        TempAllow = "通过",
                        TempDeny = "未通过",
                        TempCondition = "saveCount",
                        ElevatorTypeId = 1,
                        MaintenanceTypeId = 1
                    });

            // Assert
            var entity = this.GetEntity(tempName);

            entity.TempName.ShouldBe(tempName);
            entity.TempDesc.ShouldBe("模板描述");
            entity.TempAllow.ShouldBe("通过");
            entity.TempDeny.ShouldBe("未通过");
            entity.TempCondition.ShouldBe("saveCount");
            entity.ElevatorTypeId.ShouldBe(1);
            entity.MaintenanceTypeId.ShouldBe(1);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_AnnualInspectionUnit()
        {
            var entity = this.GetEntity("单元测试月度维保模板");

            // Act
            await this._maintenanceTemplatesAppService.Delete(new EntityDto(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_AnnualInspectionUnit()
        {
            // Arrange          
            var entity = this.GetEntity("单元测试月度维保模板");
            var newName = "单元测试模板1";

            // Act
            await this._maintenanceTemplatesAppService.CreateOrEdit(
                new CreateOrEditEccpMaintenanceTemplateDto
                    {
                        Id = entity.Id,
                        TempName = newName,
                        TempDesc = "模板描述",
                        TempAllow = "通过",
                        TempDeny = "未通过",
                        TempCondition = "saveCount",
                        ElevatorTypeId = 1,
                        MaintenanceTypeId = 1
                    });

            // Assert
            var newEntity = this.GetEntity(entity.Id);

            newEntity.TempName.ShouldBe(newName);
            newEntity.TempDesc.ShouldBe("模板描述");
            newEntity.TempAllow.ShouldBe("通过");
            newEntity.TempDeny.ShouldBe("未通过");
            newEntity.TempCondition.ShouldBe("saveCount");
            newEntity.ElevatorTypeId.ShouldBe(1);
            newEntity.MaintenanceTypeId.ShouldBe(1);
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpMaintenanceTemplate"/>.
        /// </returns>
        private EccpMaintenanceTemplate GetEntity(string name)
        {
            var entity = this.UsingDbContext(
                context => context.EccpMaintenanceTemplates.FirstOrDefault(e => e.TempName == name));
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
        /// The <see cref="EccpMaintenanceTemplate"/>.
        /// </returns>
        private EccpMaintenanceTemplate GetEntity(long id)
        {
            var entity = this.UsingDbContext(
                context => context.EccpMaintenanceTemplates.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}