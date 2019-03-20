using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.Editions.Dto;
using Sinodom.ElevatorCloud.Features;
using Shouldly;

namespace Sinodom.ElevatorCloud.Tests.Editions
{
    public class EditionAppService_Tests : AppTestBase
    {
        private readonly IEditionAppService _editionAppService;

        public EditionAppService_Tests()
        {
            LoginAsHostAdmin();
            _editionAppService = Resolve<IEditionAppService>();
        }

        [MultiTenantFact]
        public async Task Should_Get_Editions()
        {
            var editions = await _editionAppService.GetEditions();
            editions.Items.Count.ShouldBeGreaterThan(0);
        }

        [MultiTenantFact]
        public async Task Should_Get_EccpEditions()
        {
            var editionName = "Premium Edition";

            // 创建一个版本类型
            var typeName = "单元测试版本类型1";
            var editionTypeEntity = this.UsingDbContext(content => content.ECCPEditionsTypes.Add(new ECCPEditionsType { Name = typeName }));


            await this._editionAppService.CreateOrUpdateEdition(
                new CreateOrUpdateEditionDto
                    {
                        Edition = new EditionEditDto
                                      {
                                          DisplayName = editionName,
                                          ECCPEditionsTypeId = editionTypeEntity.Entity.Id
                                      },
                        FeatureValues = new List<NameValueDto>(),
                        GrantedPermissionNames = new List<string>()
                });

            var isHas = (await this._editionAppService.GetEditions()).Items.Any(e => e.ECCPEditionsTypeName == typeName);

            isHas.ShouldBe(true);
        }

        [MultiTenantFact]
        public async Task Should_Create_Edition()
        {
            //Getting edition for edit
            var output = await _editionAppService.GetEditionForEdit(new NullableIdDto(null));

            //Changing a sample feature value
            var chatFeature = output.FeatureValues.FirstOrDefault(f => f.Name == AppFeatures.ChatFeature);
            if (chatFeature != null)
            {
                chatFeature.Value = chatFeature.Value = "true";
            }

            await _editionAppService.CreateOrUpdateEdition(
                new CreateOrUpdateEditionDto
                {
                    Edition = new EditionEditDto
                    {
                        DisplayName = "Premium Edition"
                    },
                    FeatureValues = output.FeatureValues,
                    GrantedPermissionNames = new List<string>()
                });

            await UsingDbContextAsync(async context =>
            {
                var premiumEditon = await context.Editions.FirstOrDefaultAsync(e => e.DisplayName == "Premium Edition");
                premiumEditon.ShouldNotBeNull();

                if (chatFeature != null)
                {
                    var sampleFeatureValue = context.EditionFeatureSettings.FirstOrDefault(s => s.EditionId == premiumEditon.Id && s.Name == AppFeatures.ChatFeature);
                    sampleFeatureValue.ShouldNotBe(null);
                    sampleFeatureValue.Value.ShouldBe("true");
                }
            });
        }

        [MultiTenantFact]
        public async Task Should_Create_Subscribable_Edition()
        {
            var editionName = "Premium Edition";
            var monthlyPrice = 10;
            var annualPrice = 100;

            await _editionAppService.CreateOrUpdateEdition(
                new CreateOrUpdateEditionDto
                {
                    Edition = new EditionEditDto
                    {
                        DisplayName = editionName,
                        MonthlyPrice = monthlyPrice,
                        AnnualPrice = annualPrice,
                    },
                    FeatureValues = new List<NameValueDto>(),
                    GrantedPermissionNames = new List<string>()
                });

            var editionRecord = UsingDbContext(context => context.Editions.FirstOrDefault(e => e.DisplayName == editionName));
            var output = await _editionAppService.GetEditionForEdit(new NullableIdDto(editionRecord.Id));

            var premiumEditon = output.Edition;
            premiumEditon.ShouldNotBeNull();

            premiumEditon.MonthlyPrice.ShouldBe(monthlyPrice);
            premiumEditon.AnnualPrice.ShouldBe(annualPrice);
        }

        [MultiTenantFact]
        public async Task Should_Create_EccpEdition()
        {
            var editionName = "Premium Edition";

            //创建一个版本类型
            var typeName = "单元测试版本类型1";
            var editionTypeEntity = this.UsingDbContext(content => content.ECCPEditionsTypes.Add(new ECCPEditionsType { Name = typeName }));


            await this._editionAppService.CreateOrUpdateEdition(
                new CreateOrUpdateEditionDto
                    {
                        Edition = new EditionEditDto
                                      {
                                          DisplayName = editionName,
                                          ECCPEditionsTypeId = editionTypeEntity.Entity.Id
                        },
                        FeatureValues = new List<NameValueDto>(),
                        GrantedPermissionNames = new List<string>()
                });

            var editionRecord = UsingDbContext(context => context.Editions.FirstOrDefault(e => e.DisplayName == editionName));
            var output = await _editionAppService.GetEditionForEdit(new NullableIdDto(editionRecord.Id));

            var premiumEditon = output.Edition;
            premiumEditon.ShouldNotBeNull();

            premiumEditon.ECCPEditionsTypeId.ShouldBe(editionTypeEntity.Entity.Id);

        }

        [MultiTenantFact]
        public async Task Should_Update_EccpEdition()
        {
            var oldEditionName = "Premium Edition";
            var newEditionName = "Basic Edition";

            //创建一个版本类型
            var typeName = "单元测试版本类型1";
            var editionTypeEntity = this.UsingDbContext(content => content.ECCPEditionsTypes.Add(new ECCPEditionsType { Name = typeName }));


            await this._editionAppService.CreateOrUpdateEdition(
                new CreateOrUpdateEditionDto
                    {
                        Edition = new EditionEditDto
                                      {
                                          DisplayName = oldEditionName
                                      },
                        FeatureValues = new List<NameValueDto>(),
                        GrantedPermissionNames = new List<string>()
                });

            var editionRecord = UsingDbContext(context => context.Editions.FirstOrDefault(e => e.DisplayName == oldEditionName));

            await this._editionAppService.CreateOrUpdateEdition(
                new CreateOrUpdateEditionDto
                    {
                        Edition = new EditionEditDto
                                      {
                                          Id = editionRecord.Id,
                                          DisplayName = newEditionName,
                                          ECCPEditionsTypeId = editionTypeEntity.Entity.Id
                                      },
                        FeatureValues = new List<NameValueDto>(),
                        GrantedPermissionNames = new List<string>()
                });

            var output = await _editionAppService.GetEditionForEdit(new NullableIdDto(editionRecord.Id));

            var premiumEditon = output.Edition;
            premiumEditon.ShouldNotBeNull();

            premiumEditon.DisplayName.ShouldBe(newEditionName);
            premiumEditon.ECCPEditionsTypeId.ShouldBe(editionTypeEntity.Entity.Id);

        }

        [MultiTenantFact]
        public async Task Should_Update_Edition()
        {
            var defaultEdition = UsingDbContext(context => context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName));
            defaultEdition.ShouldNotBeNull();

            var output = await _editionAppService.GetEditionForEdit(new NullableIdDto(defaultEdition.Id));

            //Changing a sample feature value
            var chatFeature = output.FeatureValues.FirstOrDefault(f => f.Name == AppFeatures.ChatFeature);
            if (chatFeature != null)
            {
                chatFeature.Value = chatFeature.Value = "true";
            }

            await _editionAppService.CreateOrUpdateEdition(
                new CreateOrUpdateEditionDto
                {
                    Edition = new EditionEditDto
                    {
                        Id = output.Edition.Id,
                        DisplayName = "Regular Edition"
                    },
                    FeatureValues = output.FeatureValues,
                    GrantedPermissionNames = new List<string>()
                });

            UsingDbContext(context =>
            {
                defaultEdition = context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                defaultEdition.DisplayName.ShouldBe("Regular Edition");
            });
        }

        [MultiTenantFact]
        public async Task Should_Delete_Edition()
        {
            var editions = await _editionAppService.GetEditions();
            editions.Items.Count.ShouldBeGreaterThan(0);

            var defaultEdition = UsingDbContext(context => context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName));
            await _editionAppService.DeleteEdition(new EntityDto(defaultEdition.Id));

            UsingDbContext(context =>
            {
                defaultEdition = context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                defaultEdition.ShouldNotBeNull();
                defaultEdition.IsDeleted.ShouldBe(true);

                context.Tenants.Count(t => t.EditionId == defaultEdition.Id).ShouldBe(0);
            });
        }
    }
}
