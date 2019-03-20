using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.Authorization.Users.Dto;

namespace Sinodom.ElevatorCloud.Tests.Authorization.Users
{
    public abstract class UserAppServiceTestBase : AppTestBase
    {
        protected readonly IUserAppService UserAppService;

        protected UserAppServiceTestBase()
        {
            UserAppService = Resolve<IUserAppService>();
        }

        protected void CreateTestUsers()
        {
            UserAppService.CreateOrUpdateUser(
                        new CreateOrUpdateUserInput
                        {
                            User = new UserEditDto
                            {
                                EmailAddress = "ArthurDent@yahoo.com",
                                Name = "Arthur",
                                Surname = "Dent",
                                UserName = "artdent", //Same username is added before (in CreateTestUsers)
                                Password = "123qwe"
                            },
                            AssignedRoleNames = new string[0],
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
            UserAppService.CreateOrUpdateUser(
                       new CreateOrUpdateUserInput
                       {
                           User = new UserEditDto
                           {
                               EmailAddress = "adams_d@gmail.com",
                               Name = "Douglas",
                               Surname = "Adams",
                               UserName = "adams_d", //Same username is added before (in CreateTestUsers)
                               Password = "123qwe",
                               IsActive = true
                           },
                           AssignedRoleNames = new string[0],
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
            UserAppService.CreateOrUpdateUser(
                       new CreateOrUpdateUserInput
                       {
                           User = new UserEditDto
                           {
                               EmailAddress = "john@nash.com",
                               Name = "John",
                               Surname = "Nash",
                               UserName = "jnash", //Same username is added before (in CreateTestUsers)
                               Password = "123qwe",
                               IsLockoutEnabled = true,
                               IsActive = true
                           },
                           AssignedRoleNames = new string[0],
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
            //Note: There is a default "admin" user also
            //UsingDbContext(
            //    context =>
            //    {
            //        context.Users.Add(CreateUserEntity("jnash", "John", "Nash", "jnsh2000@testdomain.com"));
            //        context.Users.Add(CreateUserEntity("adams_d", "Douglas", "Adams", "adams_d@gmail.com"));
            //        context.Users.Add(CreateUserEntity("artdent", "Arthur", "Dent", "ArthurDent@yahoo.com"));
            //    });
        }

        protected User CreateUserEntity(string userName, string name, string surname, string emailAddress)
        {
            var user = new User
            {
                EmailAddress = emailAddress,
                IsEmailConfirmed = true,
                Name = name,
                Surname = surname,
                UserName = userName,
                Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==", //123qwe
                TenantId = AbpSession.TenantId,
                Permissions = new List<UserPermissionSetting>
                {
                    new UserPermissionSetting {Name = "test.permission1", IsGranted = true, TenantId = AbpSession.TenantId},
                    new UserPermissionSetting {Name = "test.permission2", IsGranted = true, TenantId = AbpSession.TenantId},
                    new UserPermissionSetting {Name = "test.permission3", IsGranted = false, TenantId = AbpSession.TenantId},
                    new UserPermissionSetting {Name = "test.permission4", IsGranted = false, TenantId = AbpSession.TenantId}
                }
            };
            user.SetNormalizedNames();

            return user;
        }
    }
}
