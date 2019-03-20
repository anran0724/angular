using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Timing;
using Shouldly;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.Editions.Dtos;

namespace Sinodom.ElevatorCloud.Tests.Editions
{
    public class ECCPEditionsTypesAppService_Tests : AppTestBase
    {
        private readonly IECCPEditionsTypesAppService _editionsTypesAppService;

        public ECCPEditionsTypesAppService_Tests()
        {
            LoginAsHostAdmin();
            this._editionsTypesAppService = Resolve<IECCPEditionsTypesAppService>();
        }

        [MultiTenantFact]
        public async Task Should_Get_EditionTypes()
        {
            //Arrange
            UsingDbContext(
                context =>
                {
                    context.ECCPEditionsTypes.Add(
                        new ECCPEditionsType
                        {
                            Name = "单元测试版本类型1"
                        });

                    context.ECCPEditionsTypes.Add(
                        new ECCPEditionsType
                        {
                            Name = "单元测试版本类型2"
                        });
                });

            //Act
            var output = await _editionsTypesAppService.GetAll(new GetAllECCPEditionsTypesInput
            {
                Filter = "单元测试版本类型2"
            });

            output.TotalCount.ShouldBe(1);

            output.Items[0].ECCPEditionsType.Name.ShouldBe("单元测试版本类型2");

            output = await _editionsTypesAppService.GetAll(new GetAllECCPEditionsTypesInput
            {
                Filter = "单元测试版本"
            });

            output.TotalCount.ShouldBe(2);

            output.Items[0].ECCPEditionsType.Name.ShouldBe("单元测试版本类型1");

            output.Items[1].ECCPEditionsType.Name.ShouldBe("单元测试版本类型2");
        }

        [MultiTenantFact]
        public async Task Test_Create_EditionType()
        {
            //Arrange
            var typeName = "单元测试版本类型1";

            //Act
            await _editionsTypesAppService.CreateOrEdit(new CreateOrEditECCPEditionsTypeDto
            {
                Name = typeName
            });

            //Assert
            UsingDbContext(context =>
            {
                var entity = context.ECCPEditionsTypes.FirstOrDefault(e => e.Name == typeName);
                entity.ShouldNotBeNull();
            });
        }
        [MultiTenantFact]
        public async Task Test_Update_EditionType()
        {
            //Arrange
            var oldName = "单元测试版本类型2";
            var newName = "单元测试版本类型1";

            await _editionsTypesAppService.CreateOrEdit(new CreateOrEditECCPEditionsTypeDto
            {
                Name = oldName
            });

            var entity = GetEntity("单元测试版本类型2");

            //Act
            await _editionsTypesAppService.CreateOrEdit(new CreateOrEditECCPEditionsTypeDto
            {
                Id = entity.Id,
                Name = newName
            });

            //Assert
            GetEntity(entity.Id).Name.ShouldBe(newName);
        }

        [MultiTenantFact]
        public async Task Test_Delete_EditionType()
        {
            //Arrange
            var typeName = "单元测试版本类型1";

            await _editionsTypesAppService.CreateOrEdit(new CreateOrEditECCPEditionsTypeDto
            {
                Name = typeName
            });

            var entity = GetEntity("单元测试版本类型1");

            //Act
            await _editionsTypesAppService.Delete(new EntityDto(entity.Id));

            //Assert
            GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        private ECCPEditionsType GetEntity(string diplayName)
        {
            var entity = UsingDbContext(context => context.ECCPEditionsTypes.FirstOrDefault(e => e.Name == diplayName));
            entity.ShouldNotBeNull();

            return entity;
        }

        private ECCPEditionsType GetEntity(long id)
        {
            var entity = UsingDbContext(context => context.ECCPEditionsTypes.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}