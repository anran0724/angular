using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Shouldly;
using Sinodom.ElevatorCloud.EccpAppVersions;
using Sinodom.ElevatorCloud.EccpAppVersions.Dtos;

namespace Sinodom.ElevatorCloud.Tests.EccpAppVersions
{
    public class EccpAppVersionsAppService_Tests : AppTestBase
    {
        private readonly IEccpAppVersionsAppService _eccpAppVersionsAppService;
        public EccpAppVersionsAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._eccpAppVersionsAppService = this.Resolve<IEccpAppVersionsAppService>();
        }

        [MultiTenantFact]
        public async Task Should_Get_AppVersions()
        {
            var eccpAppVersion = new EccpAppVersion();
            this.UsingDbContext(
                context =>
                {
                    eccpAppVersion = context.EccpAppVersions.Add(new EccpAppVersion
                    {
                        VersionName = "v1.0",
                        VersionCode = "1",
                        UpdateLog = "更新BUG",
                        DownloadUrl = Guid.NewGuid().ToString(),
                        VersionType = "android"
                    }).Entity;
                });
            // Act
            var output = await this._eccpAppVersionsAppService.GetAll(
                        new GetAllEccpAppVersionsInput
                        {
                            Filter = eccpAppVersion.VersionName
                        });

            output.TotalCount.ShouldBe(1);
            output.Items[0].EccpAppVersion.VersionName.ShouldBe(eccpAppVersion.VersionName);
        }

        [MultiTenantFact]
        public async Task Should_Get_AppVersionByVersionType()
        {
            var eccpAppVersion = new EccpAppVersion();
            this.UsingDbContext(
                context =>
                {
                    eccpAppVersion = context.EccpAppVersions.Add(new EccpAppVersion
                    {
                        VersionName = "v1.0",
                        VersionCode = "1",
                        UpdateLog = "更新BUG",
                        DownloadUrl = Guid.NewGuid().ToString(),
                        VersionType = "android"
                    }).Entity;
                });
            // Act
            var output = await this._eccpAppVersionsAppService.GetEccpAppVersionByVersionType(eccpAppVersion.VersionType);

            output.EccpAppVersion.VersionName.ShouldBe(eccpAppVersion.VersionName);
        }

        [MultiTenantFact]
        public async Task Test_Create_MaintenanceContracts()
        {
            // Act
            await this._eccpAppVersionsAppService.CreateOrEdit(
                new UploadCreateOrEditEccpAppVersionDto
                {
                    VersionName = "v1.0",
                    VersionCode = "1",
                    UpdateLog = "更新BUG",
                    VersionType = "android",
                    DownloadUrl = "",
                    FileToken = "4811d3f3-0bfa-4672-b875-2d47299d175d.apk"
                });
        }

        [MultiTenantFact]
        public async Task Test_Update_MaintenanceContracts()
        {
            var eccpAppVersion = new EccpAppVersion();
            this.UsingDbContext(
                context =>
                {
                    eccpAppVersion = context.EccpAppVersions.Add(new EccpAppVersion
                    {
                        VersionName = "v1.0",
                        VersionCode = "1",
                        UpdateLog = "更新BUG",
                        DownloadUrl = Guid.NewGuid().ToString(),
                        VersionType = "android"
                    }).Entity;
                });

            var entity = this.GetEntity(eccpAppVersion.Id);

            // Act
            await this._eccpAppVersionsAppService.CreateOrEdit(
                new UploadCreateOrEditEccpAppVersionDto
                {
                    Id = entity.Id,
                    VersionName = "v1.1",
                    VersionCode = "1",
                    UpdateLog = "更新BUG，新增扫码功能",
                    VersionType = "android",
                    FileToken = "4811d3f3-0bfa-4672-b875-2d47299d175d.apk"
                });

        }

        [MultiTenantFact]
        public async Task Test_Delete_MaintenanceContracts()
        {
            var eccpAppVersion = new EccpAppVersion();
            this.UsingDbContext(
                context =>
                {
                    eccpAppVersion = context.EccpAppVersions.Add(new EccpAppVersion
                    {
                        VersionName = "v1.0",
                        VersionCode = "1",
                        UpdateLog = "更新BUG",
                        DownloadUrl = Guid.NewGuid().ToString(),
                        VersionType = "android"
                    }).Entity;
                });

            var entity = this.GetEntity(eccpAppVersion.Id);

            //// Act
            await this._eccpAppVersionsAppService.Delete(new EntityDto(entity.Id));

            //// Assert
            this.GetEntity(entity.Id).IsDeleted.ShouldBeTrue();
        }

        private EccpAppVersion GetEntity(long id)
        {
            var entity = this.UsingDbContext(
                context => context.EccpAppVersions.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}
