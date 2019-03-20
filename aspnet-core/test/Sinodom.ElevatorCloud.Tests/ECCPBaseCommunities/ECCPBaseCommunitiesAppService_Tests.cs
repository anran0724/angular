// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseCommunitiesAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.ECCPBaseCommunities
{
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.ECCPBaseCommunities;
    using Sinodom.ElevatorCloud.ECCPBaseCommunities.Dtos;

    /// <summary>
    /// The eccp base communities app service_ tests.
    /// </summary>
    public class ECCPBaseCommunitiesAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _ communities app service.
        /// </summary>
        private readonly IECCPBaseCommunitiesAppService _communitiesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseCommunitiesAppService_Tests"/> class.
        /// </summary>
        public ECCPBaseCommunitiesAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._communitiesAppService = this.Resolve<IECCPBaseCommunitiesAppService>();
        }

        /// <summary>
        /// The should_ get_ communities.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_Communities()
        {
            var entities = await this._communitiesAppService.GetAll(
                               new GetAllECCPBaseCommunitiesInput { Filter = "测试园区1" });
            entities.Items.Count.ShouldBe(1);
            entities.Items[0].ProvinceName.ShouldBe("海南省");
        }

        /// <summary>
        /// The should_ get_ community.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Should_Get_Community()
        {
            var entity = this.GetEntity("测试园区1");

            var output = await this._communitiesAppService.GetECCPBaseCommunityForEdit(new EntityDto<long>(entity.Id));

            output.ECCPBaseCommunity.Name.ShouldBe("测试园区1");
            output.ProvinceName.ShouldBe("海南省");
        }

        /// <summary>
        /// The test_ create__ communities unit.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Create__CommunitiesUnit()
        {
            // Arrange
            var name = "测试园区2";

            // Act
            await this._communitiesAppService.CreateOrEdit(
                new CreateOrEditECCPBaseCommunityDto
                    {
                        Name = name, Address = "测试地址2", Longitude = "测试经纬度3", Latitude = "测试经纬度4"
                    });

            var entity = this.GetEntity(name);
            entity.Name.ShouldBe(name);
            entity.Address.ShouldBe("测试地址2");
            entity.Longitude.ShouldBe("测试经纬度3");
            entity.Latitude.ShouldBe("测试经纬度4");
        }

        /// <summary>
        /// The test_ delete_ communities.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Delete_Communities()
        {
            var entity = this.GetEntity("测试园区1");

            // Act
            await this._communitiesAppService.Delete(new EntityDto<long>(entity.Id));

            // Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        /// <summary>
        /// The test_ update_ communities.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task Test_Update_Communities()
        {
            var entity = this.GetEntity("测试园区1");
            var newName = "测试园区3";

            await this._communitiesAppService.CreateOrEdit(
                new CreateOrEditECCPBaseCommunityDto
                    {
                        Id = entity.Id,
                        Name = newName,
                        Address = "测试地址3",
                        Longitude = "测试经纬度3",
                        Latitude = "测试经纬度4"
                    });

            var newEntity = this.GetEntity(entity.Id);
            newEntity.Name.ShouldBe(newName);
            newEntity.Address.ShouldBe("测试地址3");
            newEntity.Longitude.ShouldBe("测试经纬度3");
            newEntity.Latitude.ShouldBe("测试经纬度4");
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="ECCPBaseCommunity"/>.
        /// </returns>
        private ECCPBaseCommunity GetEntity(string name)
        {
            var entity =
                this.UsingDbContext(context => context.ECCPBaseCommunities.FirstOrDefault(e => e.Name == name));
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
        /// The <see cref="ECCPBaseCommunity"/>.
        /// </returns>
        private ECCPBaseCommunity GetEntity(long id)
        {
            var entity = this.UsingDbContext(context => context.ECCPBaseCommunities.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}