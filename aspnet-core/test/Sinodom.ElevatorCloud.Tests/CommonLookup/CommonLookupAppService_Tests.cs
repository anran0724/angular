using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Sinodom.ElevatorCloud.Common;
using Shouldly;
using Sinodom.ElevatorCloud.Authorization.Roles;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.Common.Dto;
using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.EccpDict;
using Sinodom.ElevatorCloud.EccpElevatorQrCodes;
using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
using Sinodom.ElevatorCloud.ECCPBaseAreas;
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.MultiTenancy;
using Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions;
using Sinodom.ElevatorCloud.MultiTenancy.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;

namespace Sinodom.ElevatorCloud.Tests.CommonLookup
{
    public class CommonLookupAppService_Tests : AppTestBase
    {
        private readonly ICommonLookupAppService _commonLookupAppService;
        private readonly TenantAppService _tenantAppService;

        public CommonLookupAppService_Tests()
        {
            LoginAsHostAdmin();
            _commonLookupAppService = Resolve<ICommonLookupAppService>();
            _tenantAppService = Resolve<TenantAppService>();
        }

        [MultiTenantFact]
        public async Task Should_Get_Editions()
        {
            var paidEditions = await _commonLookupAppService.GetEditionsForCombobox();
            paidEditions.Items.Count.ShouldBe(9);

            var freeEditions = await _commonLookupAppService.GetEditionsForCombobox(true);
            freeEditions.Items.Count.ShouldBe(6);
        }

        [MultiTenantFact]
        public void Synchronize()
        {
            string jsonData = "{\"SyncDeptInputs\":[{\"Name\":\"鞍钢房地产开发集团有限公司电梯部1\",\"Addresse\":\"\",\"Longitude\":\"\",\"Latitude\":\"\",\"Telephone\":\"\",\"Summary\":\"\",\"ProvinceId\":\"\",\"CityId\":\"\",\"DistrictId\":\"\",\"StreetId\":\"\",\"OrgOrganizationalCode\":\"\",\"RoleGroupId\":7}]}";

            string result = _commonLookupAppService.Synchronize(jsonData);

            result.ShouldNotBeNullOrEmpty();
        }

        [MultiTenantFact]
        public async void Test_Create_CompaniesDataSynchronous()
        {
            if (Debugging.DebugHelper.IsDebug)
            {
                // Arrange
                var editionType1 = new ECCPEditionsType { Name = "政府" };
                var editionType2 = new ECCPEditionsType { Name = "维保公司" };
                var editionType3 = new ECCPEditionsType { Name = "物业公司" };

                var edition = new SubscribableEdition { DisplayName = "Free Edition", ECCPEditionsType = editionType2 };

                //var aptitudePhotoId = Guid.NewGuid();
                //var businessLicenseId = Guid.NewGuid();

                await UsingDbContextAsync(async context =>
                    {
                        context.ECCPEditionsTypes.Add(editionType1);
                        context.ECCPEditionsTypes.Add(editionType2);
                        context.ECCPEditionsTypes.Add(editionType3);
                        context.SubscribableEditions.Add(edition);
                        await context.SaveChangesAsync();
                    });

                var dept = new DeptDataSynchronousEntity
                {
                    Name = "测试维保单位",
                    Telephone = "13898021482",
                    Addresse = "鞍山",
                    OrgOrganizationalCode = "F4891651646498",
                    Longitude = "0",
                    Latitude = "0",
                    LegalPerson = "企业法人",
                    Mobile = "13565848514",
                    RoleGroupCode = "MaintDeptRole",
                    IsMember = false,
                    BusinessLicenseBytes = new byte[0],
                    AptitudePhotoBytes = new byte[0],
                    IsActive = true,
                    EditionId = edition.Id
                };

                var result = _commonLookupAppService.CompaniesDataSynchronous(dept);

                result.Result.ShouldBeGreaterThan(0);
            }
        }

        [MultiTenantFact]
        public async void Test_Update_CompaniesDataSynchronous()
        {
            // Arrange
            var editionType1 = new ECCPEditionsType { Name = "政府" };
            var editionType2 = new ECCPEditionsType { Name = "维保公司" };
            var editionType3 = new ECCPEditionsType { Name = "物业公司" };

            var edition = new SubscribableEdition { DisplayName = "Free Edition", ECCPEditionsType = editionType2 };

            //var aptitudePhotoId = Guid.NewGuid();
            //var businessLicenseId = Guid.NewGuid();

            await UsingDbContextAsync(async context =>
            {
                context.ECCPEditionsTypes.Add(editionType1);
                context.ECCPEditionsTypes.Add(editionType2);
                context.ECCPEditionsTypes.Add(editionType3);
                context.SubscribableEditions.Add(edition);
                await context.SaveChangesAsync();
            });
            ECCPBasePropertyCompany propertyEntity = new ECCPBasePropertyCompany();
            EccpPropertyCompanyExtension propertyCompanyExtension = new EccpPropertyCompanyExtension();

            UsingDbContext(context =>
            {
                context.ECCPBasePropertyCompanies.Add(new ECCPBasePropertyCompany
                {
                    Name = "鞍山市永生电梯服务有限公司",
                    Addresse = "测试地址11",
                    Telephone = "测试电话11",
                    Longitude = "1",
                    Latitude = "1",
                    OrgOrganizationalCode = "333",

                });
                context.SaveChanges();

                propertyEntity = context.ECCPBasePropertyCompanies.FirstOrDefault(e => e.Name == "鞍山市永生电梯服务有限公司");

                context.EccpPropertyCompanyExtensions.Add(new EccpPropertyCompanyExtension
                {
                    PropertyCompanyId = propertyEntity.Id,
                    LegalPerson = "企业法人",
                    Mobile = "13695845845",
                    IsMember = false,
                    SyncCompanyId = 1
                });
                context.SaveChanges();

                propertyCompanyExtension =
                    context.EccpPropertyCompanyExtensions.FirstOrDefault(e => e.PropertyCompanyId == propertyEntity.Id);
            });

            var dept = new DeptDataSynchronousEntity
            {
                Name = "鞍山市永生电梯服务有限公司11",
                Telephone = "13898021426",
                Addresse = "鞍山",
                OrgOrganizationalCode = "F4891651646498",
                Longitude = "0",
                Latitude = "0",
                LegalPerson = "企业法人",
                Mobile = "13565848514",
                RoleGroupCode = "UseDeptRole",
                IsMember = false,
                BusinessLicenseBytes = new byte[0],
                AptitudePhotoBytes = new byte[0],
                IsActive = true,
                EditionId = edition.Id,
                SyncCompanyId = propertyCompanyExtension.SyncCompanyId
            };

            var result = _commonLookupAppService.CompaniesDataSynchronous(dept);

            result.Result.ShouldBeGreaterThan(0);
        }

        [MultiTenantFact]
        public void Test_Create_UserDataSynchronous()
        {
            var role = this.UsingDbContext(content => content.Roles.FirstOrDefault(r => r.TenantId == 2 && r.Name == StaticRoleNames.MaintenanceTenants.MainInfoManage));

            var user = new UserDataSynchronousEntity
            {
                Name = "裴克刚",
                EmailAddress = "13898152975@dianti119.cn",
                Surname = "裴",
                UserName = "13898152975",
                Companies = "默认维保公司",
                IdCard = "454",
                Mobile = "13898152975",
                RoleGroupCode = "MaintDeptRole",
                SignPictureBytes = new byte[0],
                CertificateBackPictureBytes = new byte[0],
                CertificateFrontPictureBytes = new byte[0],
                ExpirationDate = DateTime.Now.AddYears(2),
                CheckState = 0,
                RoleCode = role.Name
            };

            var result = _commonLookupAppService.UserDataSynchronous(user);

            result.Result.ShouldBeGreaterThan(0);
        }

        [MultiTenantFact]
        public void Test_Update_UserDataSynchronous()
        {
            var role = this.UsingDbContext(content => content.Roles.FirstOrDefault(r => r.TenantId == 2 && r.Name == StaticRoleNames.MaintenanceTenants.MainManage));

            var userData = new UserDataSynchronousEntity
            {
                Name = "裴克刚",
                EmailAddress = "13898152975@dianti119.cn",
                Surname = "裴",
                UserName = "TestMaintenanceUser3",
                Companies = "默认维保公司",
                IdCard = "454",
                Mobile = "13898152975",
                RoleGroupCode = "MaintDeptRole",
                SignPictureBytes = new byte[0],
                CertificateBackPictureBytes = new byte[0],
                CertificateFrontPictureBytes = new byte[0],
                ExpirationDate = DateTime.Now.AddYears(2),
                CheckState = 0,
                RoleCode = role.Name,
                SyncUserId = 1
            };

            var result = _commonLookupAppService.UserDataSynchronous(userData);

            result.Result.ShouldBeGreaterThan(0);
        }

        [MultiTenantFact]
        public void Test_Create_ElevatorsDataSynchronous()
        {
            EccpDictPlaceType placeType = new EccpDictPlaceType();
            EccpDictElevatorType elevatorType = new EccpDictElevatorType();
            ECCPDictElevatorStatus elevatorStatus = new ECCPDictElevatorStatus();
            EccpMaintenanceCompanyExtension maintenanceCompanyExtension = new EccpMaintenanceCompanyExtension();
            EccpPropertyCompanyExtension propertyCompanyExtension = new EccpPropertyCompanyExtension();

            UsingDbContext(context =>
            {
                context.EccpDictPlaceTypes.Add(new EccpDictPlaceType
                {
                    Name = "住宅"
                });
                context.SaveChanges();

                placeType = context.EccpDictPlaceTypes.FirstOrDefault(e => e.Name == "住宅");

                context.EccpDictElevatorTypes.Add(new EccpDictElevatorType
                {
                    Name = "乘客电梯"
                });
                context.SaveChanges();

                elevatorType = context.EccpDictElevatorTypes.FirstOrDefault(e => e.Name == "乘客电梯");

                context.ECCPDictElevatorStatuses.Add(new ECCPDictElevatorStatus
                {
                    Name = "启用"
                });
                context.SaveChanges();

                elevatorStatus = context.ECCPDictElevatorStatuses.FirstOrDefault(e => e.Name == "启用");

                CreateTenantInput input = new CreateTenantInput
                {
                    Name = "租户2",
                    AdminEmailAddress = "",
                    AdminPassword = "",
                    ConnectionString = "",
                    ShouldChangePasswordOnNextLogin = true,
                    SendActivationEmail = true,
                    EditionId = 1,
                    IsActive = true,
                    SubscriptionEndDateUtc = DateTime.Now,
                    IsInTrialPeriod = true
                };
                var tenant = _tenantAppService.CreateTenant(input);

                context.ECCPBaseMaintenanceCompanies.Add(new ECCPBaseMaintenanceCompany
                {
                    Name = "沈阳新力电梯有限公司",
                    Addresse = "测试地址11",
                    Telephone = "测试电话11",
                    Longitude = "1",
                    Latitude = "1",
                    OrgOrganizationalCode = "333",
                    TenantId = tenant.Id
                });
                context.SaveChanges();

                var mainEntity = context.ECCPBaseMaintenanceCompanies.FirstOrDefault(e => e.Name == "沈阳新力电梯有限公司");

                context.EccpMaintenanceCompanyExtensions.Add(new EccpMaintenanceCompanyExtension
                {
                    MaintenanceCompanyId = mainEntity.Id,
                    LegalPerson = "企业法人",
                    Mobile = "13695845845",
                    IsMember = false,
                    SyncCompanyId = 1
                });
                context.SaveChanges();

                maintenanceCompanyExtension = context.EccpMaintenanceCompanyExtensions.FirstOrDefault(e => e.MaintenanceCompanyId == mainEntity.Id);

                context.ECCPBasePropertyCompanies.Add(new ECCPBasePropertyCompany
                {
                    Name = "鞍山市永生电梯服务有限公司",
                    Addresse = "测试地址11",
                    Telephone = "测试电话11",
                    Longitude = "1",
                    Latitude = "1",
                    OrgOrganizationalCode = "333"
                });
                context.SaveChanges();

                var propertyEntity = context.ECCPBasePropertyCompanies.FirstOrDefault(e => e.Name == "鞍山市永生电梯服务有限公司");

                context.EccpPropertyCompanyExtensions.Add(new EccpPropertyCompanyExtension
                {
                    PropertyCompanyId = propertyEntity.Id,
                    LegalPerson = "企业法人",
                    Mobile = "13695845845",
                    IsMember = false,
                    SyncCompanyId = 1
                });
                context.SaveChanges();

                propertyCompanyExtension =
                    context.EccpPropertyCompanyExtensions.FirstOrDefault(e => e.PropertyCompanyId == propertyEntity.Id);

                var baseAreaProvince = context.ECCPBaseAreas.Add(
                      new ECCPBaseArea { Code = "辽宁省", Name = "辽宁省", Level = 0, ParentId = 0, Path = "1" }).Entity;

                var baseAreaCity = context.ECCPBaseAreas.Add(
                    new ECCPBaseArea { Code = "沈阳市", Name = "沈阳市", Level = 0, ParentId = baseAreaProvince.Id, Path = "1" }).Entity;

                context.ECCPBaseAreas.Add(
                    new ECCPBaseArea { Code = "和平区", Name = "和平区", Level = 0, ParentId = baseAreaCity.Id, Path = "1" });
            });

            var elevators = new ElevatorsDataSynchronousEntity
            {
                Name = "电梯名称",
                CertificateNum = "31302103002012080001",
                MachineNum = "BF2012-9001",
                InstallationAddress = "管委会院内1#",
                ProvinceName = "辽宁",
                CityName = "沈阳",
                DistrictName = "和平",
                InstallationDatetime = DateTime.Now,
                Longitude = "0",
                Latitude = "0",
                EccpDictPlaceTypeName = placeType.Name,
                EccpDictElevatorTypeName = elevatorType.Name,
                ECCPDictElevatorStatusName = elevatorStatus.Name,
                MaintenanceCompanyId = maintenanceCompanyExtension.SyncCompanyId,
                PropertyCompanyId = propertyCompanyExtension.SyncCompanyId,
                CustomNum = "",
                ManufacturingLicenseNumber = "",
                FloorNumber = 33,
                GateNumber = 1,
                RatedSpeed = 10,
                Deadweight = 1200,
                ElevatorNum = "粤000001",
                IsInstall = true,
                IsGrant = true,
                InstallDateTime = DateTime.Now,
                GrantDateTime = DateTime.Now
            };

            var result = _commonLookupAppService.ElevatorsDataSynchronous(elevators);

            result.Result.ShouldBeGreaterThan(0);
        }

        [MultiTenantFact]
        public void Test_Update_ElevatorsDataSynchronous()
        {
            EccpDictPlaceType placeType = new EccpDictPlaceType();
            EccpDictElevatorType elevatorType = new EccpDictElevatorType();
            ECCPDictElevatorStatus elevatorStatus = new ECCPDictElevatorStatus();
            EccpMaintenanceCompanyExtension maintenanceCompanyExtension = new EccpMaintenanceCompanyExtension();
            EccpPropertyCompanyExtension propertyCompanyExtension = new EccpPropertyCompanyExtension();
            EccpBaseElevator baseElevator = new EccpBaseElevator();

            UsingDbContext(context =>
            {
                context.EccpDictPlaceTypes.Add(new EccpDictPlaceType
                {
                    Name = "住宅"
                });
                context.SaveChanges();

                placeType = context.EccpDictPlaceTypes.FirstOrDefault(e => e.Name == "住宅");

                context.EccpDictElevatorTypes.Add(new EccpDictElevatorType
                {
                    Name = "乘客电梯"
                });
                context.SaveChanges();

                elevatorType = context.EccpDictElevatorTypes.FirstOrDefault(e => e.Name == "乘客电梯");

                context.ECCPDictElevatorStatuses.Add(new ECCPDictElevatorStatus
                {
                    Name = "启用"
                });
                context.SaveChanges();

                elevatorStatus = context.ECCPDictElevatorStatuses.FirstOrDefault(e => e.Name == "启用");

                CreateTenantInput input = new CreateTenantInput
                {
                    Name = "租户2",
                    AdminEmailAddress = "",
                    AdminPassword = "",
                    ConnectionString = "",
                    ShouldChangePasswordOnNextLogin = true,
                    SendActivationEmail = true,
                    EditionId = 1,
                    IsActive = true,
                    SubscriptionEndDateUtc = DateTime.Now,
                    IsInTrialPeriod = true
                };
                var tenant = _tenantAppService.CreateTenant(input);

                context.ECCPBaseMaintenanceCompanies.Add(new ECCPBaseMaintenanceCompany
                {
                    Name = "沈阳新力电梯有限公司",
                    Addresse = "测试地址11",
                    Telephone = "测试电话11",
                    Longitude = "1",
                    Latitude = "1",
                    OrgOrganizationalCode = "333",
                    TenantId = tenant.Id
                });
                context.SaveChanges();

                var mainEntity = context.ECCPBaseMaintenanceCompanies.FirstOrDefault(e => e.Name == "沈阳新力电梯有限公司");

                context.EccpMaintenanceCompanyExtensions.Add(new EccpMaintenanceCompanyExtension
                {
                    MaintenanceCompanyId = mainEntity.Id,
                    LegalPerson = "企业法人",
                    Mobile = "13695845845",
                    IsMember = false,
                    SyncCompanyId = 1
                });
                context.SaveChanges();

                maintenanceCompanyExtension = context.EccpMaintenanceCompanyExtensions.FirstOrDefault(e => e.MaintenanceCompanyId == mainEntity.Id);

                context.ECCPBasePropertyCompanies.Add(new ECCPBasePropertyCompany
                {
                    Name = "鞍山市永生电梯服务有限公司",
                    Addresse = "测试地址11",
                    Telephone = "测试电话11",
                    Longitude = "1",
                    Latitude = "1",
                    OrgOrganizationalCode = "333"
                });
                context.SaveChanges();

                var propertyEntity = context.ECCPBasePropertyCompanies.FirstOrDefault(e => e.Name == "鞍山市永生电梯服务有限公司");

                context.EccpPropertyCompanyExtensions.Add(new EccpPropertyCompanyExtension
                {
                    PropertyCompanyId = propertyEntity.Id,
                    LegalPerson = "企业法人",
                    Mobile = "13695845845",
                    IsMember = false,
                    SyncCompanyId = 1
                });
                context.SaveChanges();

                propertyCompanyExtension =
                    context.EccpPropertyCompanyExtensions.FirstOrDefault(e => e.PropertyCompanyId == propertyEntity.Id);

                var baseAreaProvince = context.ECCPBaseAreas.Add(
                    new ECCPBaseArea { Code = "辽宁省", Name = "辽宁省", Level = 0, ParentId = 0, Path = "1" }).Entity;

                var baseAreaCity = context.ECCPBaseAreas.Add(
                    new ECCPBaseArea { Code = "沈阳市", Name = "沈阳市", Level = 0, ParentId = baseAreaProvince.Id, Path = "1" }).Entity;

                var baseAreaDistrict = context.ECCPBaseAreas.Add(
                     new ECCPBaseArea { Code = "和平区", Name = "和平区", Level = 0, ParentId = baseAreaCity.Id, Path = "1" }).Entity;

                context.ECCPBaseAreas.Add(
                    new ECCPBaseArea { Code = "大东区", Name = "大东区", Level = 0, ParentId = baseAreaCity.Id, Path = "1" });

                context.EccpBaseElevators.Add(new EccpBaseElevator
                {
                    Name = "测试电梯",
                    CertificateNum = "100000001",
                    MachineNum = "123123123213",
                    InstallationAddress = "asdsadasd",
                    InstallationDatetime = new DateTime().Date,
                    Latitude = "123",
                    Longitude = "123213",
                    ProvinceId = baseAreaProvince.Id,
                    CityId = baseAreaCity.Id,
                    DistrictId = baseAreaDistrict.Id,
                    SyncElevatorId = 1
                });
                context.SaveChanges();

                baseElevator = context.EccpBaseElevators.FirstOrDefault(e => e.Name == "测试电梯");

                context.EccpBaseElevatorSubsidiaryInfos.Add(
                    new EccpBaseElevatorSubsidiaryInfo
                    {
                        CustomNum = "",
                        ManufacturingLicenseNumber = "100000001",
                        FloorNumber = 2,
                        GateNumber = 2,
                        RatedSpeed = 2,
                        Deadweight = 2,
                        ElevatorId = baseElevator.Id
                    });
                context.SaveChanges();

                var maintenanceContract = context.EccpMaintenanceContracts.Add(
                    new EccpMaintenanceContract
                    {
                        TenantId = tenant.Id,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddYears(2),
                        MaintenanceCompanyId = mainEntity.Id,
                        PropertyCompanyId = propertyEntity.Id
                    }).Entity;
                context.SaveChanges();

                context.EccpMaintenanceContract_Elevator_Links.Add(new EccpMaintenanceContract_Elevator_Link
                {
                    MaintenanceContractId = maintenanceContract.Id,
                    ElevatorId = baseElevator.Id
                });
                context.SaveChanges();

                context.EccpElevatorQrCodes.Add(new EccpElevatorQrCode
                {
                    TenantId = tenant.Id,
                    AreaName = "辽",
                    ElevatorNum = "00002",
                    IsInstall = false,
                    IsGrant = false,
                    ElevatorId = baseElevator.Id
                });
            });

            var elevators = new ElevatorsDataSynchronousEntity
            {
                Name = "电梯名称",
                CertificateNum = "31302103002012080001",
                MachineNum = "BF2012-9001",
                InstallationAddress = "管委会院内1#",
                ProvinceName = "辽宁",
                CityName = "沈阳",
                DistrictName = "大东",
                InstallationDatetime = DateTime.Now,
                Longitude = "0",
                Latitude = "0",
                EccpDictPlaceTypeName = placeType.Name,
                EccpDictElevatorTypeName = elevatorType.Name,
                ECCPDictElevatorStatusName = elevatorStatus.Name,
                MaintenanceCompanyId = maintenanceCompanyExtension.SyncCompanyId,
                PropertyCompanyId = propertyCompanyExtension.SyncCompanyId,
                CustomNum = "",
                ManufacturingLicenseNumber = "",
                FloorNumber = 33,
                GateNumber = 1,
                RatedSpeed = 10,
                Deadweight = 1200,
                ElevatorNum = "粤000001",
                IsInstall = true,
                IsGrant = true,
                InstallDateTime = DateTime.Now,
                GrantDateTime = DateTime.Now,
                SyncElevatorId = baseElevator.SyncElevatorId
            };

            var result = _commonLookupAppService.ElevatorsDataSynchronous(elevators);

            result.Result.ShouldBeGreaterThan(0);
        }
    }
}
