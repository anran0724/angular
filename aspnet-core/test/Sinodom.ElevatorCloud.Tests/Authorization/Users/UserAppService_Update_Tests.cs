using System.Linq;
using System.Threading.Tasks;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Authorization.Roles;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.Authorization.Users.Dto;
using Shouldly;
using Xunit;
using System;

namespace Sinodom.ElevatorCloud.Tests.Authorization.Users
{
    public class UserAppService_Update_Tests : UserAppServiceTestBase
    {
        [Fact]
        public async Task Update_User_Basic_Tests()
        {
            //Arrange
            var managerRole = CreateRole("Manager");
            CreateTestUsers();

            var adminUser = await GetUserByUserNameOrNullAsync("jnash");

            //Act
            await UserAppService.CreateOrUpdateUser(
                new CreateOrUpdateUserInput
                {
                    User = new UserEditDto
                    {
                        Id = adminUser.Id,
                        EmailAddress = "admin1@abp.com",
                        Name = "System1",
                        Surname = "Admin2",
                        Password = "123qwE*",
                        UserName = adminUser.UserName
                    },
                    AssignedRoleNames = new[] { "Manager" },
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

            //Assert
            await UsingDbContextAsync(async context =>
            {
                //Get created user
                var updatedAdminUser = await GetUserByUserNameOrNullAsync(adminUser.UserName, includeRoles: true);
                updatedAdminUser.ShouldNotBe(null);
                updatedAdminUser.Id.ShouldBe(adminUser.Id);

                //Check some properties
                updatedAdminUser.EmailAddress.ShouldBe("admin1@abp.com");
                updatedAdminUser.TenantId.ShouldBe(AbpSession.TenantId);

                LocalIocManager
                    .Resolve<IPasswordHasher<User>>()
                    .VerifyHashedPassword(updatedAdminUser, updatedAdminUser.Password, "123qwE*")
                    .ShouldBe(PasswordVerificationResult.Success);

                //Check roles
                updatedAdminUser.Roles.Count.ShouldBe(1);
                updatedAdminUser.Roles.Any(ur => ur.RoleId == managerRole.Id).ShouldBe(true);
            });
        }

        [Fact]
        public async Task Should_Not_Update_User_With_Duplicate_Username_Or_EmailAddress()
        {
            //Arrange

            CreateTestUsers();
            var jnashUser = await GetUserByUserNameOrNullAsync("jnash");

            //Act

            //Try to update with existing username
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(async () =>
                await UserAppService.CreateOrUpdateUser(
                    new CreateOrUpdateUserInput
                    {
                        User = new UserEditDto
                               {
                                   Id = jnashUser.Id,
                                   EmailAddress = "jnsh2000@testdomain.com",
                                   Name = "John",
                                   Surname = "Nash",
                                   UserName = "adams_d", //Changed user name to an existing user
                                   Password = "123qwE*"
                               },
                        AssignedRoleNames = new[] { StaticRoleNames.Host.Admin },
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
                            ExpirationDate = DateTime.Now,
                            UserId = jnashUser.Id
                        }
                    }));

            exception.Message.ShouldContain("adams_d");

            //Try to update with existing email address
            exception = await Assert.ThrowsAsync<UserFriendlyException>(async () =>
                await UserAppService.CreateOrUpdateUser(
                    new CreateOrUpdateUserInput
                    {
                        User = new UserEditDto
                               {
                                   Id = jnashUser.Id,
                                   EmailAddress = "adams_d@gmail.com", //Changed email to an existing user
                                   Name = "John",
                                   Surname = "Nash",
                                   UserName = "jnash",
                                   Password = "123qwE*"
                               },
                        AssignedRoleNames = new[] { StaticRoleNames.Host.Admin },
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
                            ExpirationDate = DateTime.Now,
                            UserId = jnashUser.Id,
                        }
                    }));

            exception.Message.ShouldContain("adams_d@gmail.com");
        }

        [MultiTenantFact]
        public async Task Should_Remove_From_Role()
        {
            //LoginAsHostAdmin();

            //Arrange
            //var adminUser = await GetUserByUserNameOrNullAsync(User.AdminUserName);

            CreateTestUsers();

            var adminUser = await GetUserByUserNameOrNullAsync("jnash");

            //await UsingDbContextAsync(async context =>
            //{
            //    var roleCount = await context.UserRoles.CountAsync(ur => ur.UserId == adminUser.Id);
            //    roleCount.ShouldBeGreaterThan(0); //There should be 1 role at least
            //});

            //Act
            await UserAppService.CreateOrUpdateUser(
                new CreateOrUpdateUserInput
                {
                    User = new UserEditDto //Not changing user properties
                    {
                        Id = adminUser.Id,
                        EmailAddress = adminUser.EmailAddress,
                        Name = adminUser.Name,
                        Surname = adminUser.Surname,
                        UserName = adminUser.UserName,
                        Password = null
                    },
                    AssignedRoleNames = new[]{ StaticRoleNames.Host.Admin }, //Just deleting all roles expect admin
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
                        ExpirationDate = DateTime.Now,
                        UserId = adminUser.Id,
                    }
                });

            //Assert
            await UsingDbContextAsync(async context =>
            {
                var roleCount = await context.UserRoles.CountAsync(ur => ur.UserId == adminUser.Id);
                roleCount.ShouldBe(1);
            });
        }

        protected Role CreateRole(string roleName)
        {
            return UsingDbContext(context => context.Roles.Add(new Role(AbpSession.TenantId, roleName, roleName)).Entity);
        }
    }
}
