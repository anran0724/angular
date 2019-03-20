using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.ECCPBaseAreas;
using Sinodom.ElevatorCloud.ECCPBaseCommunities;
using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
using Sinodom.ElevatorCloud.EccpDict;
using Sinodom.ElevatorCloud.EccpMaintenancePlans;
using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.MultiTenancy;
using Sinodom.ElevatorCloud.StatisticalElevator.Dto;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Identity;
using Sinodom.ElevatorCloud.Authorization.Roles;
using Abp.Authorization.Users;
using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
using Newtonsoft.Json.Linq;
using Sinodom.ElevatorCloud.EccpElevatorQrCodes;

namespace Sinodom.ElevatorCloud.StatisticalElevator
{
    [AbpAuthorize]
    public class StatisticalElevatorAppService : ElevatorCloudAppServiceBase, IStatisticalElevatorAppService
    {
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;
        private readonly IRepository<Tenant, int> _tenantRepository;
        private readonly IRepository<ECCPEdition, int> _editionRepository;
        private readonly IRepository<ECCPBaseMaintenanceCompany, int> _eccpBaseMaintenanceCompanyRepository;
        private readonly IRepository<ECCPBasePropertyCompany, int> _eccpBasePropertyCompanyRepository;
        private readonly IRepository<ECCPBaseArea, int> _eccpBaseAreaRepository;
        private readonly IRepository<EccpMaintenancePlan, int> _eccpMaintenancePlanRepository;
        private readonly IRepository<EccpMaintenanceWorkOrder> _eccpMaintenanceWorkOrderRepository;
        private readonly IRepository<EccpDictMaintenanceStatus, int> _eccpDictMaintenanceStatusRepository;
        private readonly IRepository<ECCPBaseCommunity, long> _eccpBaseCommunityRepository;
        private readonly IRepository<EccpMaintenanceWorkOrder_MaintenanceUser_Link, long> _eccpMaintenanceWorkOrderMaintenanceUserLinkRepository;
        private readonly IRepository<EccpCompanyUserExtension, int> _eccpCompanyUserExtension;
        private readonly RoleManager _roleManager;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<EccpMaintenanceContract, long> _eccpMaintenanceContractRepository;
        private readonly IRepository<EccpMaintenanceContract_Elevator_Link, long> _eccpMaintenanceContractElevatorLinkRepository;
        private readonly IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> _eccpMaintenancePlanMaintenanceUserLinkRepository;
        private readonly IRepository<EccpElevatorQrCode, Guid> _eccpElevatorQrCodeRepository;

        public StatisticalElevatorAppService(
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository,
            IRepository<Tenant, int> tenantRepository,
            IRepository<ECCPEdition, int> editionRepository,
            IRepository<ECCPBaseMaintenanceCompany, int> eccpBaseMaintenanceCompanyRepository,
            IRepository<ECCPBasePropertyCompany, int> eccpBasePropertyCompanyRepository,
            IRepository<ECCPBaseArea, int> eccpBaseAreaRepository,
            IRepository<EccpMaintenancePlan, int> eccpMaintenancePlanRepository,
            IRepository<EccpMaintenanceWorkOrder> eccpMaintenanceWorkOrderRepository,
            IRepository<EccpDictMaintenanceStatus, int> eccpDictMaintenanceStatusRepository,
            IRepository<ECCPBaseCommunity, long> eccpBaseCommunityRepository,
            IRepository<EccpMaintenanceWorkOrder_MaintenanceUser_Link, long> eccpMaintenanceWorkOrderMaintenanceUserLinkRepository,
            IRepository<EccpCompanyUserExtension, int> eccpCompanyUserExtension,
            RoleManager roleManager,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<EccpMaintenanceContract, long> eccpMaintenanceContractRepository,
            IRepository<EccpMaintenanceContract_Elevator_Link, long> eccpMaintenanceContractElevatorLinkRepository,
            IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> eccpMaintenancePlanMaintenanceUserLinkRepository,
             IRepository<EccpElevatorQrCode, Guid> eccpElevatorQrCodeRepository
            )
        {
            this._eccpBaseElevatorRepository = eccpBaseElevatorRepository;
            this._tenantRepository = tenantRepository;
            this._editionRepository = editionRepository;
            this._eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
            this._eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
            this._eccpBaseAreaRepository = eccpBaseAreaRepository;
            this._eccpMaintenancePlanRepository = eccpMaintenancePlanRepository;
            this._eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
            this._eccpDictMaintenanceStatusRepository = eccpDictMaintenanceStatusRepository;
            this._eccpBaseCommunityRepository = eccpBaseCommunityRepository;
            this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository = eccpMaintenanceWorkOrderMaintenanceUserLinkRepository;
            this._eccpCompanyUserExtension = eccpCompanyUserExtension;
            this._roleManager = roleManager;
            this._userRoleRepository = userRoleRepository;
            this._eccpMaintenanceContractRepository = eccpMaintenanceContractRepository;
            this._eccpMaintenanceContractElevatorLinkRepository = eccpMaintenanceContractElevatorLinkRepository;
            this._eccpMaintenancePlanMaintenanceUserLinkRepository = eccpMaintenancePlanMaintenanceUserLinkRepository;
            this._eccpElevatorQrCodeRepository = eccpElevatorQrCodeRepository;
        }

        /// <summary>
        /// 电梯分布区域接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>        
        [AbpAllowAnonymous]
        public async Task<GetRegionalCollectionForView> RegionalCollection(GetRegionalCollectionInput input)
        {
            if (this.AbpSession.TenantId == null)
            {
                return new GetRegionalCollectionForView();
            }

            // 获取租户
            var tenantModel = await this._tenantRepository.GetAsync(this.AbpSession.TenantId.Value);

            if (tenantModel.EditionId == null)
            {
                return new GetRegionalCollectionForView();
            }

            // 通过租户版本ID获取版本信息
            var edition = await this._editionRepository.FirstOrDefaultAsync(tenantModel.EditionId.Value);

            if (edition.ECCPEditionsTypeId == null)
            {
                return new GetRegionalCollectionForView();
            }

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpBaseElevators = this._eccpBaseElevatorRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(input.CertificateNum), e => e.CertificateNum.Contains(input.CertificateNum));

                var roles = this._roleManager.Roles;
                var userRoles = this._userRoleRepository.GetAll().Where(e => e.UserId == AbpSession.UserId);
                var uRoles = from role in roles
                             join userRole in userRoles
                                 on role.Id equals userRole.RoleId
                             select new
                             {
                                 role.Name,
                                 role.DisplayName
                             };

                var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().Where(e => !e.IsCancel && !e.IsClose && e.TenantId == AbpSession.TenantId);
                var eccpMaintenanceContracts = this._eccpMaintenanceContractRepository.GetAll().Where(e => !e.IsStop && e.EndDate > DateTime.Now);
                var eccpMaintenanceContractElevatorLinks = this._eccpMaintenanceContractElevatorLinkRepository.GetAll();

                if (edition.ECCPEditionsTypeId == 3)//物业公司
                {
                    var eccpBasePropertyCompany = this._eccpBasePropertyCompanyRepository.GetAll().FirstOrDefault(w => w.TenantId == this.AbpSession.TenantId);
                    filteredEccpBaseElevators = filteredEccpBaseElevators.Where(e => e.ECCPBasePropertyCompanyId == eccpBasePropertyCompany.Id);

                    filteredEccpBaseElevators = filteredEccpBaseElevators.WhereIf(input.CompaniesId > 0,
                    e => e.ECCPBaseMaintenanceCompanyId == input.CompaniesId);

                    eccpMaintenanceContracts = eccpMaintenanceContracts.Where(e => e.PropertyCompanyId == eccpBasePropertyCompany.Id);

                    var contractQuery = from eccpMaintenanceContract in eccpMaintenanceContracts
                                        join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                        on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink.MaintenanceContractId
                                        select new
                                        {

                                            ElevatorId = eccpMaintenanceContractElevatorLink.ElevatorId
                                        };

                    if (!contractQuery.Any())
                    {
                        return new GetRegionalCollectionForView();
                    }

                    filteredEccpBaseElevators = from contract in contractQuery
                                                join elevator in filteredEccpBaseElevators
                                                on contract.ElevatorId equals elevator.Id
                                                select new EccpBaseElevator
                                                {
                                                    Id = elevator.Id,
                                                    Longitude = elevator.Longitude,
                                                    Latitude = elevator.Latitude,
                                                    ECCPBaseCommunityId = elevator.ECCPBaseCommunityId,
                                                    ProvinceId = elevator.ProvinceId,
                                                    CityId = elevator.CityId,
                                                    DistrictId = elevator.DistrictId,
                                                };
                }
                else if (edition.ECCPEditionsTypeId == 2)//维保公司
                {
                    var eccpBaseMaintenanceCompany = this._eccpBaseMaintenanceCompanyRepository.GetAll().FirstOrDefault(w => w.TenantId == this.AbpSession.TenantId);
                    filteredEccpBaseElevators = filteredEccpBaseElevators.Where(e => e.ECCPBaseMaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);

                    filteredEccpBaseElevators = filteredEccpBaseElevators.WhereIf(input.CompaniesId > 0,
                    e => e.ECCPBasePropertyCompanyId == input.CompaniesId);

                    var roleName = uRoles.Select(e => e.Name);
                    if (roleName.Contains("Admin") || roleName.Contains("MainManage") || roleName.Contains("MainInfoManage"))
                    {
                        eccpMaintenanceContracts = eccpMaintenanceContracts.Where(e => e.MaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);

                        var contractQuery = from eccpMaintenanceContract in eccpMaintenanceContracts
                                            join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                            on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink.MaintenanceContractId
                                            select new
                                            {

                                                ElevatorId = eccpMaintenanceContractElevatorLink.ElevatorId
                                            };

                        if (!contractQuery.Any())
                        {
                            return new GetRegionalCollectionForView();
                        }

                        filteredEccpBaseElevators = from contract in contractQuery
                                                    join elevator in filteredEccpBaseElevators
                                                    on contract.ElevatorId equals elevator.Id
                                                    select new EccpBaseElevator
                                                    {
                                                        Id = elevator.Id,
                                                        Longitude = elevator.Longitude,
                                                        Latitude = elevator.Latitude,
                                                        ECCPBaseCommunityId = elevator.ECCPBaseCommunityId,
                                                        ProvinceId = elevator.ProvinceId,
                                                        CityId = elevator.CityId,
                                                        DistrictId = elevator.DistrictId,
                                                    };
                    }
                    else if (roleName.Contains("MaintPrincipal") || roleName.Contains("MainUser"))
                    {
                        var eccpMaintenancePlanMaintenanceUserLinks = this._eccpMaintenancePlanMaintenanceUserLinkRepository.GetAll().Where(e => e.UserId == AbpSession.UserId).Select(e => e.MaintenancePlanId);
                        eccpMaintenancePlans = eccpMaintenancePlans.Where(e => eccpMaintenancePlanMaintenanceUserLinks.Contains(e.Id));

                        filteredEccpBaseElevators = from o in filteredEccpBaseElevators
                                                    join o1 in eccpMaintenancePlans
                                                    on o.Id equals o1.ElevatorId
                                                    select new EccpBaseElevator
                                                    {
                                                        Id = o.Id,
                                                        Longitude = o.Longitude,
                                                        Latitude = o.Latitude,
                                                        ECCPBaseCommunityId = o.ECCPBaseCommunityId,
                                                        ProvinceId = o.ProvinceId,
                                                        CityId = o.CityId,
                                                        DistrictId = o.DistrictId,
                                                    };
                    }
                }

                if (!string.IsNullOrWhiteSpace(input.TopLeft) && !string.IsNullOrWhiteSpace(input.BottomRight))
                {
                    List<string> pointList = new List<string>();
                    pointList.Add(input.TopLeft);
                    pointList.Add(input.BottomRight);
                    //根据输入的地点坐标计算中心点
                    string[] centerPoint = GetCenterPointFromListOfCoordinates(pointList).Split(',');

                    // 计算两点位置的距离，返回两点的距离，单位 米                    
                    var distance = GetDistance(Convert.ToDouble(input.TopLeft.Split(',')[0]), Convert.ToDouble(input.TopLeft.Split(',')[1]),
                            Convert.ToDouble(input.BottomRight.Split(',')[0]), Convert.ToDouble(input.BottomRight.Split(',')[1])) / 1000;

                    filteredEccpBaseElevators = filteredEccpBaseElevators
                                                .Where(
                                                e => Math.Sqrt(
                                                (Convert.ToDouble(centerPoint[1]) - Convert.ToDouble(e.Longitude)) * Math.PI * 12656
                                                * Math.Cos(
                                                (Convert.ToDouble(centerPoint[0]) + Convert.ToDouble(e.Latitude)) / distance * Math.PI / 180)
                                                / 180 * ((Convert.ToDouble(centerPoint[1]) - Convert.ToDouble(e.Longitude)) * Math.PI * 12656
                                                * Math.Cos(
                                                (Convert.ToDouble(centerPoint[0]) + Convert.ToDouble(e.Latitude)) / distance * Math.PI
                                                / 180) / 180)
                                                + (Convert.ToDouble(centerPoint[0]) - Convert.ToDouble(e.Latitude)) * Math.PI * 12656 / 180
                                                * ((Convert.ToDouble(centerPoint[0]) - Convert.ToDouble(e.Latitude)) * Math.PI * 12656 / 180)) < distance);

                }

                if (!filteredEccpBaseElevators.Any())
                {
                    return new GetRegionalCollectionForView();
                }

                if (input.ECCPDictElevatorStatusId == 5)//临期
                {
                    var dateTime = DateTime.Now.AddDays(-31);
                    var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId && w.PlanCheckDate > dateTime);


                    filteredEccpBaseElevators = from o in filteredEccpBaseElevators
                                                join o1 in eccpMaintenancePlans
                                                on o.Id equals o1.ElevatorId
                                                join o2 in eccpMaintenanceWorkOrders
                                                on o1.Id equals o2.MaintenancePlanId
                                                where EF.Functions.DateDiffHour(DateTime.Now, o2.PlanCheckDate) < o1.RemindHour
                                                select new EccpBaseElevator
                                                {
                                                    Id = o.Id,
                                                    Longitude = o.Longitude,
                                                    Latitude = o.Latitude,
                                                    ECCPBaseCommunityId = o.ECCPBaseCommunityId,
                                                    ProvinceId = o.ProvinceId,
                                                    CityId = o.CityId,
                                                    DistrictId = o.DistrictId,
                                                };
                }
                else if (input.ECCPDictElevatorStatusId == 4)//超期
                {
                    var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId && w.MaintenanceStatusId == 4);

                    filteredEccpBaseElevators = from o in filteredEccpBaseElevators
                                                join o1 in eccpMaintenancePlans
                                                on o.Id equals o1.ElevatorId
                                                join o2 in eccpMaintenanceWorkOrders
                                                on o1.Id equals o2.MaintenancePlanId
                                                where EF.Functions.DateDiffHour(DateTime.Now, o2.PlanCheckDate) < 31
                                                select new EccpBaseElevator
                                                {
                                                    Id = o.Id,
                                                    Longitude = o.Longitude,
                                                    Latitude = o.Latitude,
                                                    ECCPBaseCommunityId = o.ECCPBaseCommunityId,
                                                    ProvinceId = o.ProvinceId,
                                                    CityId = o.CityId,
                                                    DistrictId = o.DistrictId,
                                                };

                }


                if (!filteredEccpBaseElevators.Any())
                {
                    return new GetRegionalCollectionForView();
                }

                switch (input.Type)
                {
                    //跨省                    
                    case 1:
                        {
                            var query = from o3 in filteredEccpBaseElevators
                                        join o4 in this._eccpBaseAreaRepository.GetAll().Where(t => t.Level == 0)
                                        on o3.ProvinceId equals o4.Id
                                        select new
                                        {
                                            AreaName = o4.Name,
                                            AreaId = o4.Id
                                        };

                            var gQuery = query.GroupBy(g => g.AreaId)
                                .Select(
                                    e => new RegionalDto
                                    {
                                        AreaId = e.Key,
                                        AreaName = e.FirstOrDefault().AreaName,
                                        ElevatorNumber = e.Count(),
                                        Point = BaiduMapPoint(e.FirstOrDefault().AreaName)
                                    });

                            return new GetRegionalCollectionForView()
                            {
                                Type = 1,
                                CenterPoint = BaiduMapPoint("北京市"),
                                RegionalCollection = gQuery.ToList()
                            };
                        }
                    // 跨市
                    case 2:
                        {
                            var query = from o3 in filteredEccpBaseElevators
                                        join o4 in this._eccpBaseAreaRepository.GetAll().Where(t => t.Level == 1)
                                        on o3.CityId equals o4.Id
                                        select new
                                        {
                                            AreaName = o4.Name,
                                            AreaId = o4.Id
                                        };

                            var gQuery = query.GroupBy(g => g.AreaId)
                                .Select(
                                    e => new RegionalDto
                                    {
                                        AreaId = e.Key,
                                        AreaName = e.FirstOrDefault().AreaName,
                                        ElevatorNumber = e.Count(),
                                        Point = BaiduMapPoint(e.FirstOrDefault().AreaName)
                                    });

                            var centerPoint = "";
                            var provinceId = filteredEccpBaseElevators.FirstOrDefault(e => e.CityId > 0)?.ProvinceId;

                            if (provinceId != null)
                            {
                                centerPoint = this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == provinceId) == null ? "" :
                                    BaiduMapPoint(this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == provinceId).Name);
                            }

                            return new GetRegionalCollectionForView()
                            {
                                Type = 2,
                                CenterPoint = centerPoint,
                                RegionalCollection = gQuery.ToList()
                            };
                        }
                    //跨区
                    case 3:
                        {
                            var cityId = filteredEccpBaseElevators.FirstOrDefault(e => e.CityId > 0)?.CityId;
                            var cityName = "";
                            var centerPoint = "";
                            if (cityId != null)
                            {
                                cityName = this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == cityId).Name;
                                centerPoint = BaiduMapPoint(cityName);
                            }

                            var query = from o3 in filteredEccpBaseElevators.Where(e => e.DistrictId > 0)
                                        join o4 in this._eccpBaseAreaRepository.GetAll().Where(t => t.Level == 2)
                                        on o3.DistrictId equals o4.Id
                                        select new
                                        {
                                            AreaName = o4.Name,
                                            AreaId = o4.Id
                                        };

                            var gQuery = query.GroupBy(g => g.AreaId)
                                .Select(
                                    e => new RegionalDto
                                    {
                                        AreaId = e.Key,
                                        AreaName = e.FirstOrDefault().AreaName,
                                        ElevatorNumber = e.Count(),
                                        Point = BaiduMapPoint(cityName + ',' + e.FirstOrDefault().AreaName)
                                    });

                            return new GetRegionalCollectionForView()
                            {
                                Type = 3,
                                CenterPoint = centerPoint,
                                RegionalCollection = gQuery.ToList()
                            };
                        }
                    //跨小区
                    case 4:
                        {
                            var centerPoint = "";
                            if (filteredEccpBaseElevators.Where(e => e.ProvinceId > 0).GroupBy(e => e.ProvinceId).Count() > 1)//跨省
                            {
                                centerPoint = BaiduMapPoint("北京市");
                            }
                            else if (filteredEccpBaseElevators.Where(e => e.CityId > 0).GroupBy(e => e.CityId).Count() > 1)//跨市
                            {
                                var provinceId = filteredEccpBaseElevators.FirstOrDefault(e => e.CityId > 0)?.ProvinceId;

                                if (provinceId != null)
                                {
                                    centerPoint = this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == provinceId) == null ? "" :
                                        BaiduMapPoint(this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == provinceId).Name);
                                }
                            }
                            else if (filteredEccpBaseElevators.Where(e => e.DistrictId > 0).GroupBy(e => e.DistrictId).Count() > 1)//跨区                  
                            {
                                var cityId = filteredEccpBaseElevators.FirstOrDefault(e => e.DistrictId > 0)?.CityId;

                                if (cityId != null)
                                {
                                    centerPoint = this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == cityId) == null ? "" :
                                        BaiduMapPoint(this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == cityId).Name);
                                }
                            }
                            else
                            {
                                var elevators = filteredEccpBaseElevators.FirstOrDefault(e => e.DistrictId > 0);
                                if (elevators != null)
                                {
                                    var cityName = this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == elevators.CityId).Name;
                                    var districtName = this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == elevators.DistrictId).Name;
                                    var name = cityName + ',' + districtName;
                                    centerPoint = BaiduMapPoint(name);
                                }
                            }

                            var query = from o3 in filteredEccpBaseElevators
                                        join o4 in this._eccpBaseCommunityRepository.GetAll() on o3.ECCPBaseCommunityId equals o4.Id
                                        into j4
                                        from s4 in j4.DefaultIfEmpty()
                                        select new
                                        {
                                            ECCPBaseCommunityId = o3.ECCPBaseCommunityId,
                                            Community = s4
                                        };

                            var gQuery = query.GroupBy(g => g.ECCPBaseCommunityId)
                                .Select(
                                    e => new RegionalDto
                                    {
                                        AreaId = Convert.ToInt32(e.Key),
                                        AreaName = e.FirstOrDefault().Community == null ? "未知小区" : e.FirstOrDefault().Community.Name,
                                        Point = e.FirstOrDefault().Community == null ? centerPoint
                                                : e.FirstOrDefault().Community.Latitude + "," + e.FirstOrDefault().Community.Longitude,
                                        ElevatorNumber = e.Count()
                                    });

                            return new GetRegionalCollectionForView()
                            {
                                Type = 4,
                                CenterPoint = centerPoint,
                                RegionalCollection = gQuery.ToList()
                            };
                        }
                }

                if (filteredEccpBaseElevators.Where(e => e.ProvinceId > 0).GroupBy(e => e.ProvinceId).Count() > 1)//跨省
                {
                    var query = from o3 in filteredEccpBaseElevators
                                join o4 in this._eccpBaseAreaRepository.GetAll().Where(t => t.Level == 0)
                                on o3.ProvinceId equals o4.Id
                                select new
                                {
                                    AreaName = o4.Name,
                                    AreaId = o4.Id
                                };

                    var gQuery = query.GroupBy(g => g.AreaId)
                    .Select(
                        e => new RegionalDto
                        {
                            AreaId = e.Key,
                            AreaName = e.FirstOrDefault().AreaName,
                            ElevatorNumber = e.Count(),
                            Point = BaiduMapPoint(e.FirstOrDefault().AreaName)
                        });

                    return new GetRegionalCollectionForView()
                    {
                        Type = 1,
                        CenterPoint = BaiduMapPoint("北京市"),
                        RegionalCollection = gQuery.ToList()
                    };

                }
                else if (filteredEccpBaseElevators.Where(e => e.CityId > 0).GroupBy(e => e.CityId).Count() > 1)//跨市
                {
                    var query = from o3 in filteredEccpBaseElevators
                                join o4 in this._eccpBaseAreaRepository.GetAll().Where(t => t.Level == 1)
                                on o3.CityId equals o4.Id
                                select new
                                {
                                    AreaName = o4.Name,
                                    AreaId = o4.Id
                                };

                    var gQuery = query.GroupBy(g => g.AreaId)
                    .Select(
                        e => new RegionalDto
                        {
                            AreaId = e.Key,
                            AreaName = e.FirstOrDefault().AreaName,
                            ElevatorNumber = e.Count(),
                            Point = BaiduMapPoint(e.FirstOrDefault().AreaName)
                        });

                    var centerPoint = "";
                    var provinceId = filteredEccpBaseElevators.FirstOrDefault(e => e.CityId > 0)?.ProvinceId;

                    if (provinceId != null)
                    {
                        centerPoint = this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == provinceId) == null ? "" :
                                      BaiduMapPoint(this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == provinceId).Name);
                    }

                    return new GetRegionalCollectionForView()
                    {
                        Type = 2,
                        CenterPoint = centerPoint,
                        RegionalCollection = gQuery.ToList()
                    };
                }
                else if (filteredEccpBaseElevators.Where(e => e.DistrictId > 0).GroupBy(e => e.DistrictId).Count() > 1)//跨区                  
                {
                    var cityId = filteredEccpBaseElevators.FirstOrDefault(e => e.CityId > 0).CityId;
                    var cityName = this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == cityId).Name;
                    var centerPoint = BaiduMapPoint(cityName);

                    var query = from o3 in filteredEccpBaseElevators.Where(e => e.DistrictId > 0)
                                join o4 in this._eccpBaseAreaRepository.GetAll().Where(t => t.Level == 2)
                                on o3.DistrictId equals o4.Id
                                select new
                                {
                                    AreaName = o4.Name,
                                    AreaId = o4.Id
                                };

                    var gQuery = query.GroupBy(g => g.AreaId)
                    .Select(
                        e => new RegionalDto
                        {
                            AreaId = e.Key,
                            AreaName = e.FirstOrDefault().AreaName,
                            ElevatorNumber = e.Count(),
                            Point = BaiduMapPoint(cityName + ',' + e.FirstOrDefault().AreaName)
                        });

                    return new GetRegionalCollectionForView()
                    {
                        Type = 3,
                        CenterPoint = centerPoint,
                        RegionalCollection = gQuery.ToList()
                    };
                }
                else
                {
                    var centerPoint = "";
                    var elevators = filteredEccpBaseElevators.FirstOrDefault(e => e.DistrictId > 0);
                    if (elevators != null)
                    {
                        var cityName = this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == elevators.CityId).Name;
                        var districtName = this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == elevators.DistrictId).Name;
                        var name = cityName + ',' + districtName;
                        centerPoint = BaiduMapPoint(name);
                    }

                    var query = from o3 in filteredEccpBaseElevators
                                join o4 in this._eccpBaseCommunityRepository.GetAll() on o3.ECCPBaseCommunityId equals o4.Id
                                into j4
                                from s4 in j4.DefaultIfEmpty()
                                select new
                                {
                                    ECCPBaseCommunityId = o3.ECCPBaseCommunityId,
                                    Community = s4
                                };

                    var gQuery = query.GroupBy(g => g.ECCPBaseCommunityId)
                        .Select(
                            e => new RegionalDto
                            {
                                AreaId = Convert.ToInt32(e.Key),
                                AreaName = e.FirstOrDefault().Community == null ? "未知小区" : e.FirstOrDefault().Community.Name,
                                Point = e.FirstOrDefault().Community == null ? centerPoint
                                        : e.FirstOrDefault().Community.Latitude + "," + e.FirstOrDefault().Community.Longitude,
                                ElevatorNumber = e.Count()
                            });

                    return new GetRegionalCollectionForView()
                    {
                        Type = 4,
                        CenterPoint = centerPoint,
                        RegionalCollection = gQuery.ToList()
                    };
                }
            }

        }


        /// <summary>
        /// 根据输入的地点坐标计算中心点
        /// </summary>
        /// < returns ></ returns >
        public string GetCenterPointFromListOfCoordinates(List<string> pointList)
        {
            int total = pointList.Count;
            double lat = 0, lon = 0;
            foreach (string point in pointList)
            {
                string[] sArray = point.Split(',');
                lat += Convert.ToDouble(sArray[0]) * Math.PI / 180;
                lon += Convert.ToDouble(sArray[1]) * Math.PI / 180;
            }
            lat /= total;
            lon /= total;
            return lat * 180 / Math.PI + "," + lon * 180 / Math.PI;
        }


        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        public double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            //地球半径，单位米
            double EARTH_RADIUS = 6378137;
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }

        /// <summary>
        /// 电梯分布接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<GetElevatorCollectionForView> ElevatorCollection(GetElevatorCollectionInput input)
        {
            if (string.IsNullOrWhiteSpace(input.TopLeft) || string.IsNullOrWhiteSpace(input.BottomRight))
            {
                return new GetElevatorCollectionForView();
            }

            if (this.AbpSession.TenantId == null)
            {
                return new GetElevatorCollectionForView();
            }

            var tenantModel = await this._tenantRepository.GetAsync(this.AbpSession.TenantId.Value);

            if (tenantModel.EditionId == null)
            {
                return new GetElevatorCollectionForView();
            }

            // 通过租户版本ID获取版本信息
            var edition = await this._editionRepository.FirstOrDefaultAsync(tenantModel.EditionId.Value);

            if (edition.ECCPEditionsTypeId == null)
            {
                return new GetElevatorCollectionForView();
            }

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpBaseElevators = this._eccpBaseElevatorRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(input.CertificateNum), e => e.CertificateNum.Contains(input.CertificateNum));

                List<string> pointList = new List<string>();
                pointList.Add(input.TopLeft);
                pointList.Add(input.BottomRight);
                //根据输入的地点坐标计算中心点
                string[] centerPoint = GetCenterPointFromListOfCoordinates(pointList).Split(',');

                // 计算两点位置的距离，返回两点的距离，单位 米                    
                var distance = GetDistance(Convert.ToDouble(input.TopLeft.Split(',')[0]), Convert.ToDouble(input.TopLeft.Split(',')[1]),
                                   Convert.ToDouble(input.BottomRight.Split(',')[0]), Convert.ToDouble(input.BottomRight.Split(',')[1])) / 1000;


                filteredEccpBaseElevators = filteredEccpBaseElevators
                   .Where(
                       e => Math.Sqrt(
                           (Convert.ToDouble(centerPoint[1]) - Convert.ToDouble(e.Longitude)) * Math.PI * 12656
                           * Math.Cos(
                           (Convert.ToDouble(centerPoint[0]) + Convert.ToDouble(e.Latitude)) / distance * Math.PI / 180)
                            / 180 * ((Convert.ToDouble(centerPoint[1]) - Convert.ToDouble(e.Longitude)) * Math.PI * 12656
                           * Math.Cos(
                           (Convert.ToDouble(centerPoint[0]) + Convert.ToDouble(e.Latitude)) / distance * Math.PI
                            / 180) / 180)
                           + (Convert.ToDouble(centerPoint[0]) - Convert.ToDouble(e.Latitude)) * Math.PI * 12656 / 180
                           * ((Convert.ToDouble(centerPoint[0]) - Convert.ToDouble(e.Latitude)) * Math.PI * 12656 / 180)) < distance);

                var roles = this._roleManager.Roles;
                var userRoles = this._userRoleRepository.GetAll().Where(e => e.UserId == AbpSession.UserId);
                var uRoles = from role in roles
                             join userRole in userRoles
                                 on role.Id equals userRole.RoleId
                             select new
                             {
                                 role.Name,
                                 role.DisplayName
                             };

                var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().Where(e => !e.IsCancel && !e.IsClose && e.TenantId == AbpSession.TenantId);
                var eccpMaintenanceContracts = this._eccpMaintenanceContractRepository.GetAll().Where(e => !e.IsStop && e.EndDate > DateTime.Now);
                var eccpMaintenanceContractElevatorLinks = this._eccpMaintenanceContractElevatorLinkRepository.GetAll();

                var dateTime = DateTime.Now.AddDays(-31);
                var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                    .Where(w => w.TenantId == this.AbpSession.TenantId && w.PlanCheckDate > dateTime && (w.MaintenanceStatusId == 1 || w.MaintenanceStatusId == 2));

                if (!filteredEccpBaseElevators.Any())
                {
                    return new GetElevatorCollectionForView();
                }

                if (input.ECCPDictElevatorStatusId == 5)//临期
                {
                    filteredEccpBaseElevators = from o in filteredEccpBaseElevators
                                                join o1 in eccpMaintenancePlans
                                                on o.Id equals o1.ElevatorId
                                                join o2 in eccpMaintenanceWorkOrders
                                                on o1.Id equals o2.MaintenancePlanId
                                                where EF.Functions.DateDiffHour(DateTime.Now, o2.PlanCheckDate) < o1.RemindHour
                                                select new EccpBaseElevator
                                                {
                                                    Id = o.Id,
                                                    CertificateNum = o.CertificateNum,
                                                    Longitude = o.Longitude,
                                                    Latitude = o.Latitude,
                                                    InstallationAddress = o.InstallationAddress
                                                };

                }
                else if (input.ECCPDictElevatorStatusId == 4)//超期
                {
                    var workOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                        .Where(w => w.TenantId == this.AbpSession.TenantId && w.MaintenanceStatusId == 4);

                    filteredEccpBaseElevators = from o in filteredEccpBaseElevators
                                                join o1 in eccpMaintenancePlans
                                                on o.Id equals o1.ElevatorId
                                                join o2 in workOrders
                                                on o1.Id equals o2.MaintenancePlanId
                                                where EF.Functions.DateDiffHour(DateTime.Now, o2.PlanCheckDate) < 31
                                                select new EccpBaseElevator
                                                {
                                                    Id = o.Id,
                                                    CertificateNum = o.CertificateNum,
                                                    Longitude = o.Longitude,
                                                    Latitude = o.Latitude,
                                                    InstallationAddress = o.InstallationAddress
                                                };

                }

                var elevatorQrCodes = this._eccpElevatorQrCodeRepository.GetAll();


                if (edition.ECCPEditionsTypeId == 3)//物业公司
                {
                    var eccpBasePropertyCompany = this._eccpBasePropertyCompanyRepository.GetAll().FirstOrDefault(w => w.TenantId == this.AbpSession.TenantId);
                    filteredEccpBaseElevators = filteredEccpBaseElevators.Where(e => e.ECCPBasePropertyCompanyId == eccpBasePropertyCompany.Id);

                    filteredEccpBaseElevators = filteredEccpBaseElevators.WhereIf(input.CompaniesId > 0,
                    e => e.ECCPBaseMaintenanceCompanyId == input.CompaniesId);


                    if (!filteredEccpBaseElevators.Any())
                    {
                        return new GetElevatorCollectionForView();
                    }

                    eccpMaintenanceContracts = eccpMaintenanceContracts.Where(e => e.PropertyCompanyId == eccpBasePropertyCompany.Id);

                    var contractQuery = from eccpMaintenanceContract in eccpMaintenanceContracts
                                        join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                         on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink.MaintenanceContractId
                                        select new
                                        {
                                            ElevatorId = eccpMaintenanceContractElevatorLink.ElevatorId
                                        };

                    if (!contractQuery.Any())
                    {
                        return new GetElevatorCollectionForView();
                    }

                    var planQuery = (from o1 in eccpMaintenancePlans
                                     join o2 in eccpMaintenanceWorkOrders
                                     on o1.Id equals o2.MaintenancePlanId
                                     select new
                                     {
                                         ElevatorsId = o1.ElevatorId,
                                         WorkOrderId = o2.Id
                                     }).GroupBy(e => e.ElevatorsId).Select(e => new
                                     {
                                         ElevatorsId = e.Key,
                                         e.FirstOrDefault().WorkOrderId
                                     });

                    var elevatorQuery = from contract in contractQuery
                                        join elevator in filteredEccpBaseElevators
                                        on contract.ElevatorId equals elevator.Id
                                        join plan in planQuery
                                        on elevator.Id equals plan.ElevatorsId
                                        into j1
                                        from s1 in j1.DefaultIfEmpty()
                                        join elevatorQrCode in elevatorQrCodes
                                        on elevator.Id equals elevatorQrCode.ElevatorId
                                        into j2
                                        from s2 in j2.DefaultIfEmpty()
                                        select new ElevatorCollectionDto
                                        {
                                            ElevatorId = elevator.Id,
                                            CertificateNum = elevator.CertificateNum,
                                            Latitude = elevator.Latitude,
                                            Longitude = elevator.Longitude,
                                            InstallationAddress = elevator.InstallationAddress,
                                            WorkOrderId = s1 == null ? 0 : s1.WorkOrderId,
                                            ElevatorNum = s2 == null ? string.Empty : s2.ElevatorNum
                                        };

                    return new GetElevatorCollectionForView()
                    {
                        ElevatorNumber = elevatorQuery.Count(),
                        ElevatorCollection = elevatorQuery.Take(30).ToList()
                    };

                }
                else if (edition.ECCPEditionsTypeId == 2)//维保公司
                {
                    var eccpBaseMaintenanceCompany = this._eccpBaseMaintenanceCompanyRepository.GetAll().FirstOrDefault(w => w.TenantId == this.AbpSession.TenantId);
                    filteredEccpBaseElevators = filteredEccpBaseElevators.Where(e => e.ECCPBaseMaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);

                    filteredEccpBaseElevators = filteredEccpBaseElevators.WhereIf(input.CompaniesId > 0,
                    e => e.ECCPBasePropertyCompanyId == input.CompaniesId);


                    if (!filteredEccpBaseElevators.Any())
                    {
                        return new GetElevatorCollectionForView();
                    }

                    var roleName = uRoles.Select(e => e.Name);
                    if (roleName.Contains("Admin") || roleName.Contains("MainManage") || roleName.Contains("MainInfoManage"))
                    {
                        eccpMaintenanceContracts = eccpMaintenanceContracts.Where(e => e.MaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);

                        var contractQuery = from eccpMaintenanceContract in eccpMaintenanceContracts
                                            join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                            on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink.MaintenanceContractId
                                            select new
                                            {
                                                ElevatorId = eccpMaintenanceContractElevatorLink.ElevatorId
                                            };

                        if (!contractQuery.Any())
                        {
                            return new GetElevatorCollectionForView();
                        }

                        var planQuery = (from o1 in eccpMaintenancePlans
                                         join o2 in eccpMaintenanceWorkOrders
                                         on o1.Id equals o2.MaintenancePlanId
                                         select new
                                         {
                                             ElevatorsId = o1.ElevatorId,
                                             WorkOrderId = o2.Id
                                         }).GroupBy(e => e.ElevatorsId).Select(e => new
                                         {
                                             ElevatorsId = e.Key,
                                             e.FirstOrDefault().WorkOrderId
                                         });

                        var elevatorQuery = from contract in contractQuery
                                            join elevator in filteredEccpBaseElevators
                                            on contract.ElevatorId equals elevator.Id
                                            join plan in planQuery
                                            on elevator.Id equals plan.ElevatorsId
                                            into j1
                                            from s1 in j1.DefaultIfEmpty()
                                            join elevatorQrCode in elevatorQrCodes
                                            on elevator.Id equals elevatorQrCode.ElevatorId
                                            into j2
                                            from s2 in j2.DefaultIfEmpty()
                                            select new ElevatorCollectionDto
                                            {
                                                ElevatorId = elevator.Id,
                                                CertificateNum = elevator.CertificateNum,
                                                Latitude = elevator.Latitude,
                                                Longitude = elevator.Longitude,
                                                InstallationAddress = elevator.InstallationAddress,
                                                WorkOrderId = s1 == null ? 0 : s1.WorkOrderId,
                                                ElevatorNum = s2 == null ? string.Empty : s2.ElevatorNum
                                            };

                        return new GetElevatorCollectionForView()
                        {
                            ElevatorNumber = elevatorQuery.Count(),
                            ElevatorCollection = elevatorQuery.Take(30).ToList()
                        };

                    }
                    else if (roleName.Contains("MaintPrincipal") || roleName.Contains("MainUser"))
                    {
                        var eccpMaintenancePlanMaintenanceUserLinks = this._eccpMaintenancePlanMaintenanceUserLinkRepository.GetAll().Where(e => e.UserId == AbpSession.UserId).Select(e => e.MaintenancePlanId);
                        eccpMaintenancePlans = eccpMaintenancePlans.Where(e => eccpMaintenancePlanMaintenanceUserLinks.Contains(e.Id));

                        var planQuery = from o1 in eccpMaintenancePlans
                                        join o2 in eccpMaintenanceWorkOrders
                                        on o1.Id equals o2.MaintenancePlanId
                                        select new
                                        {
                                            ElevatorId = o1.ElevatorId,
                                            WorkOrderId = o2.Id
                                        };

                        var elevatorQuery = from elevator in filteredEccpBaseElevators
                                            join plan in planQuery
                                            on elevator.Id equals plan.ElevatorId
                                            join elevatorQrCode in elevatorQrCodes
                                            on elevator.Id equals elevatorQrCode.ElevatorId
                                            into j2
                                            from s2 in j2.DefaultIfEmpty()
                                            select new ElevatorCollectionDto
                                            {
                                                ElevatorId = elevator.Id,
                                                CertificateNum = elevator.CertificateNum,
                                                Latitude = elevator.Latitude,
                                                Longitude = elevator.Longitude,
                                                InstallationAddress = elevator.InstallationAddress,
                                                WorkOrderId = plan.WorkOrderId,
                                                ElevatorNum = s2 == null ? string.Empty : s2.ElevatorNum
                                            };

                        return new GetElevatorCollectionForView()
                        {
                            ElevatorNumber = elevatorQuery.Count(),
                            ElevatorCollection = elevatorQuery.Take(30).ToList()
                        };
                    }
                }

                return new GetElevatorCollectionForView();
            }
        }


        /// <summary>
        /// 根据名字返回坐标点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string BaiduMapPoint(string name)
        {
            string url = "http://api.map.baidu.com/geocoder/v2/?address=" + name + "&output=json&ak=nSRkLovUkrPf8Nxv8ESy0L83&callback=showLocation";
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            int len = content.IndexOf('(', 1);
            var str = content.Substring(len + 1).Substring(0, content.Substring(len + 1).Length - 1);
            var point = JsonConvert.DeserializeObject<PointDto>(str);
            reader.Close();
            return point.result.location.lat + "," + point.result.location.lng;
        }


        /// <summary>
        /// 人员分布区域接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<GetRegionalPersonCollectionForView> RegionalPersonCollection(GetRegionalPersonCollectionInput input)
        {
            if (this.AbpSession.TenantId == null)
            {
                return new GetRegionalPersonCollectionForView();
            }

            // 获取租户
            var tenantModel = await this._tenantRepository.GetAsync(this.AbpSession.TenantId.Value);

            if (tenantModel.EditionId == null)
            {
                return new GetRegionalPersonCollectionForView();
            }

            // 通过租户版本ID获取版本信息
            var edition = await this._editionRepository.FirstOrDefaultAsync(tenantModel.EditionId.Value);

            if (edition.ECCPEditionsTypeId == null)
            {
                return new GetRegionalPersonCollectionForView();
            }

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var users = this.UserManager.Users;
                var filteredCompanyUserExtension = this._eccpCompanyUserExtension.GetAll();

                if (edition.ECCPEditionsTypeId == 3)//物业公司
                {
                    var eccpBasePropertyCompany = this._eccpBasePropertyCompanyRepository.FirstOrDefault(e =>
                                                  e.TenantId == AbpSession.TenantId.Value);

                    var eccpMaintenanceContracts = this._eccpMaintenanceContractRepository.GetAll().Where(e => !e.IsStop && e.EndDate > DateTime.Now
                                                  && e.PropertyCompanyId == eccpBasePropertyCompany.Id);

                    var maintenanceCompanyTenantId = eccpMaintenanceContracts.GroupBy(e => e.MaintenanceCompany.TenantId)
                        .Select(e => e.Key);

                    var userIds = users.Where(e => maintenanceCompanyTenantId.Contains(e.TenantId)).Select(w => w.Id);

                    filteredCompanyUserExtension = filteredCompanyUserExtension.Where(e => userIds.Contains(e.UserId)
                                                   && e.IsOnline && EF.Functions.DateDiffMinute(e.Heartbeat, DateTime.Now) < 20);
                }
                else if (edition.ECCPEditionsTypeId == 2)//维保公司
                {
                    var userIds = users.Where(e => e.TenantId == AbpSession.TenantId).Select(w => w.Id);
                    filteredCompanyUserExtension = filteredCompanyUserExtension.Where(e => userIds.Contains(e.UserId)
                                                   && e.IsOnline && EF.Functions.DateDiffMinute(e.Heartbeat, DateTime.Now) < 20);
                }
                else
                {
                    return new GetRegionalPersonCollectionForView();
                }

                if (!filteredCompanyUserExtension.Any())
                {
                    return new GetRegionalPersonCollectionForView();
                }

                if (!input.UserName.IsNullOrWhiteSpace())
                {
                    users = users.Where(u => u.UserName == input.UserName);

                    filteredCompanyUserExtension = from o in filteredCompanyUserExtension
                                                   join o1 in users
                                                   on o.UserId equals o1.Id
                                                   select new EccpCompanyUserExtension
                                                   {
                                                       Id = o.Id,
                                                       Longitude = o.Longitude,
                                                       Latitude = o.Latitude,
                                                       PositionProvinceId = o.PositionProvinceId,
                                                       PositionCityId = o.PositionCityId,
                                                       PositionDistrictId = o.PositionDistrictId,
                                                       PositionCommunityId = o.PositionCommunityId
                                                   };
                }

                if (!filteredCompanyUserExtension.Any())
                {
                    return new GetRegionalPersonCollectionForView();
                }

                if (input.ECCPBaseMaintenanceCompaniesId > 0)
                {
                    var tenantId = _eccpBaseMaintenanceCompanyRepository.FirstOrDefault(m => m.Id == input.ECCPBaseMaintenanceCompaniesId).TenantId;
                    if (tenantId == null)
                    {
                        return new GetRegionalPersonCollectionForView();
                    }

                    users = users.Where(u => u.TenantId == tenantId);

                    filteredCompanyUserExtension = from o in filteredCompanyUserExtension
                                                   join o1 in users
                                                   on o.UserId equals o1.Id
                                                   select new EccpCompanyUserExtension
                                                   {
                                                       Id = o.Id,
                                                       Longitude = o.Longitude,
                                                       Latitude = o.Latitude,
                                                       PositionProvinceId = o.PositionProvinceId,
                                                       PositionCityId = o.PositionCityId,
                                                       PositionDistrictId = o.PositionDistrictId,
                                                       PositionCommunityId = o.PositionCommunityId
                                                   };
                }

                if (!string.IsNullOrWhiteSpace(input.TopLeft) && !string.IsNullOrWhiteSpace(input.BottomRight))
                {
                    List<string> pointList = new List<string>();
                    pointList.Add(input.TopLeft);
                    pointList.Add(input.BottomRight);
                    //根据输入的地点坐标计算中心点
                    string[] centerPoint = GetCenterPointFromListOfCoordinates(pointList).Split(',');

                    // 计算两点位置的距离，返回两点的距离，单位 米                    
                    var distance = GetDistance(Convert.ToDouble(input.TopLeft.Split(',')[0]), Convert.ToDouble(input.TopLeft.Split(',')[1]),
                            Convert.ToDouble(input.BottomRight.Split(',')[0]), Convert.ToDouble(input.BottomRight.Split(',')[1])) / 1000;

                    filteredCompanyUserExtension = filteredCompanyUserExtension.Where(o =>
                                                      Math.Sqrt(
                                                      (Convert.ToDouble(centerPoint[1]) - Convert.ToDouble(o.Longitude)) * Math.PI * 12656
                                                      * Math.Cos(
                                                      (Convert.ToDouble(centerPoint[0]) + Convert.ToDouble(o.Latitude)) / distance * Math.PI / 180)
                                                      / 180 * ((Convert.ToDouble(centerPoint[1]) - Convert.ToDouble(o.Longitude)) * Math.PI * 12656
                                                      * Math.Cos(
                                                     (Convert.ToDouble(centerPoint[0]) + Convert.ToDouble(o.Latitude)) / distance * Math.PI
                                                      / 180) / 180)
                                                      + (Convert.ToDouble(centerPoint[0]) - Convert.ToDouble(o.Latitude)) * Math.PI * 12656 / 180
                                                      * ((Convert.ToDouble(centerPoint[0]) - Convert.ToDouble(o.Latitude)) * Math.PI * 12656 / 180)) < distance);
                }

                if (!filteredCompanyUserExtension.Any())
                {
                    return new GetRegionalPersonCollectionForView();
                }

                switch (input.Type)
                {
                    case 1:
                        {
                            var query = from o1 in filteredCompanyUserExtension
                                        join o2 in this._eccpBaseAreaRepository.GetAll().Where(t => t.Level == 0)
                                            on o1.PositionProvinceId equals o2.Id
                                        select new
                                        {
                                            AreaId = o1.PositionProvinceId,
                                            AreaName = o2.Name
                                        };

                            var gQuery = query.GroupBy(g => g.AreaId)
                                .Select(
                                    e => new RegionalPersonDto
                                    {
                                        AreaId = e.Key,
                                        AreaName = e.FirstOrDefault().AreaName,
                                        PersonNumber = e.Count(),
                                        Point = BaiduMapPoint(e.FirstOrDefault().AreaName)
                                    });

                            return new GetRegionalPersonCollectionForView()
                            {
                                Type = 1,
                                RegionalPersonCollection = gQuery.ToList()
                            };
                        }
                    case 2:
                        {
                            var query = from o1 in filteredCompanyUserExtension
                                        join o2 in this._eccpBaseAreaRepository.GetAll().Where(t => t.Level == 1)
                                            on o1.PositionCityId equals o2.Id
                                        select new
                                        {
                                            AreaId = o1.PositionCityId,
                                            AreaName = o2.Name
                                        };

                            var gQuery = query.GroupBy(g => g.AreaId)
                                .Select(
                                    e => new RegionalPersonDto
                                    {
                                        AreaId = e.Key,
                                        AreaName = e.FirstOrDefault().AreaName,
                                        PersonNumber = e.Count(),
                                        Point = BaiduMapPoint(e.FirstOrDefault().AreaName)
                                    });

                            return new GetRegionalPersonCollectionForView()
                            {
                                Type = 2,
                                RegionalPersonCollection = gQuery.ToList()
                            };
                        }
                    case 3:
                        {
                            var cityId = filteredCompanyUserExtension.FirstOrDefault(e => e.PositionCityId > 0)?.PositionCityId;
                            var cityName = "";
                            if (cityId != null)
                            {
                                cityName = this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == cityId) == null ? "" :
                                    this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == cityId).Name;
                            }

                            var query = from o1 in filteredCompanyUserExtension
                                        join o2 in this._eccpBaseAreaRepository.GetAll().Where(t => t.Level == 2)
                                            on o1.PositionDistrictId equals o2.Id
                                        select new
                                        {
                                            AreaId = o1.PositionDistrictId,
                                            AreaName = o2.Name
                                        };

                            var gQuery = query.GroupBy(g => g.AreaId)
                                .Select(
                                    e => new RegionalPersonDto
                                    {
                                        AreaId = e.Key,
                                        AreaName = e.FirstOrDefault().AreaName,
                                        PersonNumber = e.Count(),
                                        Point = BaiduMapPoint(cityName + ',' + e.FirstOrDefault().AreaName)
                                    });

                            return new GetRegionalPersonCollectionForView()
                            {
                                Type = 3,
                                RegionalPersonCollection = gQuery.ToList()
                            };
                        }
                }

                if (filteredCompanyUserExtension.Where(e => e.PositionProvinceId > 0).GroupBy(e => e.PositionProvinceId).Count() > 1)//跨省
                {
                    var query = from o1 in filteredCompanyUserExtension
                                join o2 in this._eccpBaseAreaRepository.GetAll().Where(t => t.Level == 0)
                                on o1.PositionProvinceId equals o2.Id
                                select new
                                {
                                    AreaId = o1.PositionProvinceId,
                                    AreaName = o2.Name
                                };

                    var gQuery = query.GroupBy(g => g.AreaId)
                    .Select(
                        e => new RegionalPersonDto
                        {
                            AreaId = e.Key,
                            AreaName = e.FirstOrDefault().AreaName,
                            PersonNumber = e.Count(),
                            Point = (e.FirstOrDefault().AreaName)
                        });

                    return new GetRegionalPersonCollectionForView()
                    {
                        Type = 1,
                        RegionalPersonCollection = gQuery.ToList()
                    };
                }
                else if (filteredCompanyUserExtension.Where(e => e.PositionCityId > 0).GroupBy(e => e.PositionCityId).Count() > 1)//跨市
                {
                    var query = from o1 in filteredCompanyUserExtension
                                join o2 in this._eccpBaseAreaRepository.GetAll().Where(t => t.Level == 1)
                                on o1.PositionCityId equals o2.Id
                                select new
                                {
                                    AreaId = o1.PositionCityId,
                                    AreaName = o2.Name
                                };

                    var gQuery = query.GroupBy(g => g.AreaId)
                    .Select(
                        e => new RegionalPersonDto
                        {
                            AreaId = e.Key,
                            AreaName = e.FirstOrDefault().AreaName,
                            PersonNumber = e.Count(),
                            Point = BaiduMapPoint(e.FirstOrDefault().AreaName)
                        });

                    return new GetRegionalPersonCollectionForView()
                    {
                        Type = 2,
                        RegionalPersonCollection = gQuery.ToList()
                    };

                }
                else if (filteredCompanyUserExtension.Where(e => e.PositionDistrictId > 0).GroupBy(e => e.PositionDistrictId).Count() > 1)//跨区                  
                {
                    var cityId = filteredCompanyUserExtension.FirstOrDefault(e => e.PositionCityId > 0)?.PositionCityId;
                    var cityName = this._eccpBaseAreaRepository.FirstOrDefault(a => a.Id == cityId).Name;

                    var query = from o1 in filteredCompanyUserExtension
                                join o2 in this._eccpBaseAreaRepository.GetAll().Where(t => t.Level == 2)
                                on o1.PositionDistrictId equals o2.Id
                                select new
                                {
                                    AreaId = o1.PositionDistrictId,
                                    AreaName = o2.Name
                                };

                    var gQuery = query.GroupBy(g => g.AreaId)
                    .Select(
                        e => new RegionalPersonDto
                        {
                            AreaId = e.Key,
                            AreaName = e.FirstOrDefault().AreaName,
                            PersonNumber = e.Count(),
                            Point = BaiduMapPoint(cityName + ',' + e.FirstOrDefault().AreaName)
                        });

                    return new GetRegionalPersonCollectionForView()
                    {
                        Type = 3,
                        RegionalPersonCollection = gQuery.ToList()
                    };
                }
            }

            return new GetRegionalPersonCollectionForView();
        }

        /// <summary>
        /// 人员分布接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<GetPersonnelDistributionForView> PersonnelDistribution(GetPersonnelDistributionInput input)
        {
            if (this.AbpSession.TenantId == null)
            {
                return new GetPersonnelDistributionForView();
            }

            // 获取租户
            var tenantModel = await this._tenantRepository.GetAsync(this.AbpSession.TenantId.Value);

            if (tenantModel.EditionId == null)
            {
                return new GetPersonnelDistributionForView();
            }

            // 通过租户版本ID获取版本信息
            var edition = await this._editionRepository.FirstOrDefaultAsync(tenantModel.EditionId.Value);

            if (edition.ECCPEditionsTypeId == null)
            {
                return new GetPersonnelDistributionForView();
            }

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var users = this.UserManager.Users;
                var filteredCompanyUserExtension = this._eccpCompanyUserExtension.GetAll();

                if (edition.ECCPEditionsTypeId == 3)//物业公司
                {
                    var eccpBasePropertyCompany = this._eccpBasePropertyCompanyRepository.FirstOrDefault(e =>
                                                  e.TenantId == AbpSession.TenantId.Value);

                    var eccpMaintenanceContracts = this._eccpMaintenanceContractRepository.GetAll().Where(e => !e.IsStop && e.EndDate > DateTime.Now
                                                   && e.PropertyCompanyId == eccpBasePropertyCompany.Id);

                    var maintenanceCompanyTenantId = eccpMaintenanceContracts.GroupBy(e => e.MaintenanceCompany.TenantId)
                                                     .Select(e => e.Key);

                    var userIds = users.Where(e => maintenanceCompanyTenantId.Contains(e.TenantId)).Select(w => w.Id);

                    filteredCompanyUserExtension = filteredCompanyUserExtension.Where(e => userIds.Contains(e.UserId)
                                                   && e.IsOnline && EF.Functions.DateDiffMinute(e.Heartbeat, DateTime.Now) < 20);
                }
                else if (edition.ECCPEditionsTypeId == 2)//维保公司
                {
                    var userIds = users.Where(e => e.TenantId == AbpSession.TenantId).Select(w => w.Id);
                    filteredCompanyUserExtension = filteredCompanyUserExtension.Where(e => userIds.Contains(e.UserId)
                                                   && e.IsOnline && EF.Functions.DateDiffMinute(e.Heartbeat, DateTime.Now) < 20);
                }
                else
                {
                    return new GetPersonnelDistributionForView();
                }


                if (!string.IsNullOrWhiteSpace(input.TopLeft) && !string.IsNullOrWhiteSpace(input.BottomRight))
                {
                    List<string> pointList = new List<string>();
                    pointList.Add(input.TopLeft);
                    pointList.Add(input.BottomRight);
                    //根据输入的地点坐标计算中心点
                    string[] centerPoint = GetCenterPointFromListOfCoordinates(pointList).Split(',');

                    // 计算两点位置的距离，返回两点的距离，单位 米                    
                    var distance = GetDistance(Convert.ToDouble(input.TopLeft.Split(',')[0]), Convert.ToDouble(input.TopLeft.Split(',')[1]),
                            Convert.ToDouble(input.BottomRight.Split(',')[0]), Convert.ToDouble(input.BottomRight.Split(',')[1])) / 1000;

                    filteredCompanyUserExtension = filteredCompanyUserExtension.Where(o =>
                                                       Math.Sqrt(
                                                       (Convert.ToDouble(centerPoint[1]) - Convert.ToDouble(o.Longitude)) * Math.PI * 12656
                                                       * Math.Cos(
                                                       (Convert.ToDouble(centerPoint[0]) + Convert.ToDouble(o.Latitude)) / distance * Math.PI / 180)
                                                       / 180 * ((Convert.ToDouble(centerPoint[1]) - Convert.ToDouble(o.Longitude)) * Math.PI * 12656
                                                       * Math.Cos(
                                                      (Convert.ToDouble(centerPoint[0]) + Convert.ToDouble(o.Latitude)) / distance * Math.PI
                                                       / 180) / 180)
                                                       + (Convert.ToDouble(centerPoint[0]) - Convert.ToDouble(o.Latitude)) * Math.PI * 12656 / 180
                                                       * ((Convert.ToDouble(centerPoint[0]) - Convert.ToDouble(o.Latitude)) * Math.PI * 12656 / 180)) < distance).Take(30);

                    if (!filteredCompanyUserExtension.Any())
                    {
                        return new GetPersonnelDistributionForView();
                    }

                    var query = from o1 in filteredCompanyUserExtension
                                join o2 in users
                                on o1.UserId equals o2.Id
                                join o3 in this._eccpBaseMaintenanceCompanyRepository.GetAll()
                                on o2.TenantId equals o3.TenantId
                                into j3
                                from s3 in j3.DefaultIfEmpty()
                                select new PersonnelDto
                                {
                                    UserId = o1.UserId,
                                    Latitude = o1.Latitude,
                                    Longitude = o1.Longitude,
                                    UserName = o2.UserName,
                                    PhoneNumber = o2.PhoneNumber,
                                    MaintenanceCompaniesName = s3 == null ? string.Empty : s3.Name
                                };

                    return new GetPersonnelDistributionForView
                    {
                        PersonnelNumber = query.Count(),
                        PersonnelCollection = query.ToList()
                    };
                }
            }

            return new GetPersonnelDistributionForView();
        }

    }
}

