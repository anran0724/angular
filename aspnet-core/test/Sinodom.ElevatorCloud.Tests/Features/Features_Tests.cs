using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.Authorization.Users.Dto;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.Editions.Dto;
using Sinodom.ElevatorCloud.Features;
using Shouldly;
using Xunit;

namespace Sinodom.ElevatorCloud.Tests.Features
{
    using Sinodom.ElevatorCloud.Authorization.Roles;
    using System;
    using System.Collections.Generic;

    public class Features_Tests : AppTestBase
    {
        private readonly IEditionAppService _editionAppService;
        private readonly IUserAppService _userAppService;
        private readonly ILocalizationManager _localizationManager;

        public Features_Tests()
        {
            LoginAsHostAdmin();
            _editionAppService = Resolve<IEditionAppService>();
            _userAppService = Resolve<IUserAppService>();
            _localizationManager = Resolve<ILocalizationManager>();
        }

        [MultiTenantFact]
        public async Task Should_Not_Create_User_More_Than_Allowed_Count()
        {
            //Getting edition for edit
            var output = await _editionAppService.GetEditionForEdit(new NullableIdDto(null));

            //Changing a sample feature value
            var maxUserCountFeature = output.FeatureValues.FirstOrDefault(f => f.Name == AppFeatures.MaxUserCount);
            if (maxUserCountFeature != null)
            {
                maxUserCountFeature.Value = "2";
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


            var premiumEditon = (await _editionAppService.GetEditions()).Items.FirstOrDefault(e => e.DisplayName == "Premium Edition");
            premiumEditon.ShouldNotBeNull();

            await UsingDbContextAsync(async context =>
            {
                var tenant = await context.Tenants.SingleAsync(t => t.TenancyName == AbpTenantBase.DefaultTenantName);
                tenant.EditionId = premiumEditon.Id;

                context.SaveChanges();
            });

            LoginAsDefaultTenantAdmin();

            Role role = new Role();

            UsingDbContext(
           context =>
           {
               context.Roles.Add(new Role
               {
                   DisplayName = "User",
                   Name = "User",
                   NormalizedName = "USER"
               });
               role = context.Roles.FirstOrDefault(e => e.DisplayName == "User");
           });
            // Arrange
            string name = "≤‚ ‘";

            // This is second user (first is tenant admin)
            await _userAppService.CreateOrUpdateUser(
                new CreateOrUpdateUserInput
                {
                    User = new UserEditDto
                    {
                        Name = name,
                        Surname = "≤‚ ‘s",
                        UserName = "test",
                        EmailAddress = "test@163.com",
                        PhoneNumber = "13812312312",
                        Password = "123123"
                    },
                    AssignedRoleNames = new string[] { role.Name },
                    SendActivationEmail = false,
                    SetRandomPassword = false,
                    SignPictureToken = "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423",
                    CertificateBackPictureToken = "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423",
                    CertificateFrontPictureToken = "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423",
                    CompanyUser = new EccpCompanyUserExtensionEditDto
                    {
                        IdCard = "10000",
                        Mobile = "13812312312",
                        SignPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                        CertificateFrontPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                        CertificateBackPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                        ExpirationDate = DateTime.Now
                    }
                });

            //Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(
                async () =>
                    await _userAppService.CreateOrUpdateUser(
                        new CreateOrUpdateUserInput
                        {
                            User = new UserEditDto
                            {
                                Name = name,
                                Surname = "≤‚ ‘s",
                                UserName = "test1",
                                EmailAddress = "test1@163.com",
                                PhoneNumber = "13812312312",
                                Password = "123123"
                            },
                            AssignedRoleNames = new string[] { role.Name },
                            SendActivationEmail = false,
                            SetRandomPassword = false,
                            SignPictureToken = "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423",
                            CertificateBackPictureToken = "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423",
                            CertificateFrontPictureToken = "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423",
                            CompanyUser = new EccpCompanyUserExtensionEditDto
                            {
                                IdCard = "10000",
                                Mobile = "13812312312",
                                SignPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                                CertificateFrontPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                                CertificateBackPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                                ExpirationDate = DateTime.Now
                            }
                        })
            );

            exception.Message.ShouldContain(_localizationManager.GetString(ElevatorCloudConsts.LocalizationSourceName, "MaximumUserCount_Error_Message"));
        }
    }
}
