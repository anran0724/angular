// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseAreas_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.ECCPBaseAreas
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.ECCPBaseAreas;
    using Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos;
    using Xunit;

    /// <summary>
    /// The eccp base areas_ tests.
    /// </summary>
    public class ECCPBaseAreas_Tests : AppTestBase
    {
        /// <summary>
        /// The _areas app service.
        /// </summary>
        private readonly IECCPBaseAreasAppService _areasAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseAreas_Tests"/> class.
        /// </summary>
        public ECCPBaseAreas_Tests()
        {
            this.LoginAsHostAdmin();
            this._areasAppService = this.Resolve<IECCPBaseAreasAppService>();
        }

        /// <summary>
        /// The test_ create_ areas.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Theory]
        [InlineData("测试区域1", 0, "Code1", "111", 1)]
        [InlineData(null, 0, "Code1", "111", 1)]
        [InlineData("测试区域1", null, "Code1", "111", 1)]
        [InlineData("测试区域1", 0, "Code1", "111", null)]
        [InlineData("测试区域1", 0, null, "111", 1)]
        [InlineData("测试区域1", 0, "Code1", null, 1)]
        public async Task Test_Create_Areas(string name, int parentId, string code, string path, int level)
        {
            // Arrange


            // Act
            await this._areasAppService.CreateOrEdit(
                new CreateOrEditECCPBaseAreaDto
                {
                    ParentId = parentId,
                    Code = code,
                    Name = name,
                    Level = level,
                    Path = path
                });

            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
            entity.ParentId.ShouldBe(0);
            entity.Code.ShouldBe("Code1");
            entity.Level.ShouldBe(1);
        }

        /// <summary>
        /// The test_ delete_ areas.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_Areas()
        {
            var entity = this.GetEntity("海南省");

            // Act
            await this._areasAppService.Delete(new EntityDto(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ areas.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Theory]
        [InlineData("测试区域2", 1, "Code2", "222", 1)]
        [InlineData(null, 0, "Code1", "111", 1)]
        [InlineData("测试区域1", null, "Code1", "111", 1)]
        [InlineData("测试区域1", 0, "Code1", "111", null)]
        [InlineData("测试区域1", 0, null, "111", 1)]
        [InlineData("测试区域1", 0, "Code1", null, 1)]
        public async Task Test_Update_Areas(string name, int parentId, string code, string path, int level)
        {
            var entity = this.GetEntity("海南省");

            // Act
            await this._areasAppService.CreateOrEdit(
                new CreateOrEditECCPBaseAreaDto
                {
                    Id = entity.Id,
                    ParentId = parentId,
                    Code = code,
                    Name = name,
                    Level = level,
                    Path = path
                });

            var newEntity = this.GetEntity(entity.Id);
            newEntity.Name.ShouldBe("测试区域2");
            newEntity.ParentId.ShouldBe(1);
            newEntity.Code.ShouldBe("Code2");
            newEntity.Level.ShouldBe(1);
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="ECCPBaseArea"/>.
        /// </returns>
        private ECCPBaseArea GetEntity(string name)
        {
            var entity = this.UsingDbContext(context => context.ECCPBaseAreas.FirstOrDefault(e => e.Name == name));
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
        /// The <see cref="ECCPBaseArea"/>.
        /// </returns>
        private ECCPBaseArea GetEntity(int id)
        {
            var entity = this.UsingDbContext(context => context.ECCPBaseAreas.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}