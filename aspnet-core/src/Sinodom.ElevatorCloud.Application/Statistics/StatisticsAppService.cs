using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.Authorization.Roles;
using Sinodom.ElevatorCloud.EccpBaseElevatorBrands;
using Sinodom.ElevatorCloud.EccpBaseElevatorModels;
using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.EccpDict;
using Sinodom.ElevatorCloud.EccpMaintenancePlans;
using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;
using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
using Sinodom.ElevatorCloud.EccpMaintenanceWorks;
using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits;
using Sinodom.ElevatorCloud.ECCPBaseAreas;
using Sinodom.ElevatorCloud.ECCPBaseCommunities;
using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies;
using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions;
using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;
using Sinodom.ElevatorCloud.Statistics.Dtos;
using Newtonsoft.Json;
using Sinodom.ElevatorCloud.EccpElevatorQrCodes;
using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes;
using Sinodom.ElevatorCloud.StatisticalElevator.Dto;

namespace Sinodom.ElevatorCloud.Statistics
{
    using Microsoft.Extensions.Configuration;
    using Sinodom.ElevatorCloud.Configuration;

    //TODO:登录权限暂时使用工单的
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders)]
    public class StatisticsAppService : ElevatorCloudAppServiceBase, IStatisticsAppService
    {
        private readonly IRepository<ECCPEdition> _eccpEditionRepository;
        private readonly IRepository<ECCPEditionsType> _eccpEditionsTypeRepository;
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorsRepository;
        private readonly IRepository<EccpMaintenancePlan, int> _eccpMaintenancePlanRepository;
        private readonly IRepository<EccpMaintenanceWorkOrder> _eccpMaintenanceWorkOrderRepository;
        private readonly IRepository<ECCPBaseMaintenanceCompany, int> _eccpBaseMaintenanceCompanyRepository;
        private readonly IRepository<ECCPBasePropertyCompany, int> _eccpBasePropertyCompanyRepository;
        private readonly IRepository<EccpMaintenanceContract, long> _eccpMaintenanceContractRepository;
        private readonly IRepository<EccpDictMaintenanceStatus, int> _eccpDictMaintenanceStatusRepository;
        private readonly IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> _eccpMaintenancePlanMaintenanceUserLinkRepository;
        private readonly RoleManager _roleManager;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<EccpMaintenanceWorkOrder_MaintenanceUser_Link, long> _eccpMaintenanceWorkOrderMaintenanceUserLinkRepository;
        private readonly IRepository<EccpMaintenanceTempWorkOrder, Guid> _eccpMaintenanceTempWorkOrderRepository;
        private readonly IRepository<EccpCompanyUserExtension, int> _eccpCompanyUserExtensionRepository;
        private readonly IRepository<UserPathHistory, long> _userPathHistoryRepository;
        private readonly IRepository<EccpMaintenanceWork, int> _eccpEccpMaintenanceWorkRepository;
        private readonly IRepository<EccpMaintenanceWorkFlow, Guid> _eccpMaintenanceWorkFlowRepository;
        private readonly IRepository<ECCPBaseArea, int> _eccpBaseAreaRepository;
        private readonly IRepository<EccpBaseElevatorSubsidiaryInfo, Guid> _eccpBaseElevatorSubsidiaryInfoRepository;
        private readonly IRepository<EccpDictPlaceType, int> _eccpDictPlaceTypeRepository;
        private readonly IRepository<ECCPDictElevatorStatus, int> _eccpDictElevatorStatusRepository;
        private readonly IRepository<EccpDictElevatorType, int> _eccpDictElevatorTypeRepository;
        private readonly IRepository<ECCPBaseAnnualInspectionUnit, long> _eccpBaseAnnualInspectionUnitRepository;
        private readonly IRepository<ECCPBaseCommunity, long> _eccpBaseCommunityRepository;
        private readonly IRepository<EccpBaseElevatorBrand, int> _eccpBaseElevatorBrandRepository;
        private readonly IRepository<EccpBaseElevatorModel, int> _eccpBaseElevatorModelRepository;
        private readonly IRepository<ECCPBaseRegisterCompany, long> _eccpBaseRegisterCompanyRepository;
        private readonly IRepository<ECCPBaseProductionCompany, long> _eccpBaseProductionCompanyRepository;
        private readonly IRepository<EccpMaintenanceContract_Elevator_Link, long> _eccpMaintenanceContractElevatorLinkRepository;
        private readonly IRepository<EccpDictNodeType, int> _eccpDictNodeTypeRepository;
        private readonly IRepository<EccpMaintenanceTemplateNode, int> _eccpMaintenanceTemplateNodeRepository;
        private readonly IRepository<EccpElevatorQrCode, Guid> _eccpElevatorQrCodeRepository;
        private readonly IConfigurationRoot _appConfiguration;

        public StatisticsAppService(IRepository<ECCPEdition> eccpEditionRepository, IRepository<ECCPEditionsType> eccpEditionsTypeRepository, IRepository<EccpBaseElevator, Guid> eccpBaseElevatorsRepository, IRepository<EccpMaintenancePlan, int> eccpMaintenancePlanRepository, IRepository<EccpMaintenanceWorkOrder> eccpMaintenanceWorkOrderRepository, IRepository<ECCPBaseMaintenanceCompany, int> eccpBaseMaintenanceCompanyRepository, IRepository<ECCPBasePropertyCompany, int> eccpBasePropertyCompanyRepository, IRepository<EccpMaintenanceContract, long> eccpMaintenanceContractRepository, IRepository<EccpDictMaintenanceStatus, int> eccpDictMaintenanceStatusRepository, IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> eccpMaintenancePlanMaintenanceUserLinkRepository, RoleManager roleManager, IRepository<UserRole, long> userRoleRepository, IRepository<EccpMaintenanceWorkOrder_MaintenanceUser_Link, long> eccpMaintenanceWorkOrderMaintenanceUserLinkRepository, IRepository<EccpMaintenanceTempWorkOrder, Guid> eccpMaintenanceTempWorkOrderRepository, IRepository<EccpCompanyUserExtension, int> eccpCompanyUserExtensionRepository, IRepository<UserPathHistory, long> userPathHistoryRepository, IRepository<EccpMaintenanceWork, int> eccpEccpMaintenanceWorkRepository, IRepository<EccpMaintenanceWorkFlow, Guid> eccpMaintenanceWorkFlowRepository, IRepository<ECCPBaseArea, int> eccpBaseAreaRepository, IRepository<EccpBaseElevatorSubsidiaryInfo, Guid> eccpBaseElevatorSubsidiaryInfoRepository, IRepository<EccpDictPlaceType, int> eccpDictPlaceTypeRepository, IRepository<ECCPDictElevatorStatus, int> eccpDictElevatorStatusRepository, IRepository<EccpDictElevatorType, int> eccpDictElevatorTypeRepository, IRepository<ECCPBaseAnnualInspectionUnit, long> eccpBaseAnnualInspectionUnitRepository, IRepository<ECCPBaseCommunity, long> eccpBaseCommunityRepository, IRepository<EccpBaseElevatorBrand, int> eccpBaseElevatorBrandRepository, IRepository<EccpBaseElevatorModel, int> eccpBaseElevatorModelRepository, IRepository<ECCPBaseRegisterCompany, long> eccpBaseRegisterCompanyRepository, IRepository<ECCPBaseProductionCompany, long> eccpBaseProductionCompanyRepository, IRepository<EccpMaintenanceContract_Elevator_Link, long> eccpMaintenanceContractElevatorLinkRepository,
            IRepository<EccpDictNodeType, int> eccpDictNodeTypeRepository, IRepository<EccpMaintenanceTemplateNode, int> eccpMaintenanceTemplateNodeRepository,
            IRepository<EccpElevatorQrCode, Guid> eccpElevatorQrCodeRepository,
            IAppConfigurationAccessor configurationAccessor)
        {
            this._eccpEditionRepository = eccpEditionRepository;
            this._eccpEditionsTypeRepository = eccpEditionsTypeRepository;
            this._eccpBaseElevatorsRepository = eccpBaseElevatorsRepository;
            this._eccpMaintenancePlanRepository = eccpMaintenancePlanRepository;
            this._eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
            this._eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
            this._eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
            this._eccpMaintenanceContractRepository = eccpMaintenanceContractRepository;
            this._eccpDictMaintenanceStatusRepository = eccpDictMaintenanceStatusRepository;
            this._eccpMaintenancePlanMaintenanceUserLinkRepository = eccpMaintenancePlanMaintenanceUserLinkRepository;
            this._roleManager = roleManager;
            this._userRoleRepository = userRoleRepository;
            this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository =
                eccpMaintenanceWorkOrderMaintenanceUserLinkRepository;
            this._eccpMaintenanceTempWorkOrderRepository = eccpMaintenanceTempWorkOrderRepository;
            this._eccpCompanyUserExtensionRepository = eccpCompanyUserExtensionRepository;
            this._userPathHistoryRepository = userPathHistoryRepository;
            this._eccpEccpMaintenanceWorkRepository = eccpEccpMaintenanceWorkRepository;
            this._eccpMaintenanceWorkFlowRepository = eccpMaintenanceWorkFlowRepository;
            this._eccpBaseAreaRepository = eccpBaseAreaRepository;
            this._eccpBaseElevatorSubsidiaryInfoRepository = eccpBaseElevatorSubsidiaryInfoRepository;
            this._eccpDictPlaceTypeRepository = eccpDictPlaceTypeRepository;
            this._eccpDictElevatorStatusRepository = eccpDictElevatorStatusRepository;
            this._eccpDictElevatorTypeRepository = eccpDictElevatorTypeRepository;
            this._eccpBaseAnnualInspectionUnitRepository = eccpBaseAnnualInspectionUnitRepository;
            this._eccpBaseCommunityRepository = eccpBaseCommunityRepository;
            this._eccpBaseElevatorBrandRepository = eccpBaseElevatorBrandRepository;
            this._eccpBaseElevatorModelRepository = eccpBaseElevatorModelRepository;
            this._eccpBaseRegisterCompanyRepository = eccpBaseRegisterCompanyRepository;
            this._eccpBaseProductionCompanyRepository = eccpBaseProductionCompanyRepository;
            this._eccpMaintenanceContractElevatorLinkRepository = eccpMaintenanceContractElevatorLinkRepository;
            this._eccpDictNodeTypeRepository = eccpDictNodeTypeRepository;
            this._eccpMaintenanceTemplateNodeRepository = eccpMaintenanceTemplateNodeRepository;
            this._eccpElevatorQrCodeRepository = eccpElevatorQrCodeRepository;

            this._appConfiguration = configurationAccessor.Configuration;
        }

        /// <summary>
        /// 手机端维保统计的接口
        /// </summary>
        /// <returns></returns>
        public async Task<GetAppMaintenanceStatisticsDto> GetAppMaintenanceStatistics()
        {
            var getAppMaintenanceStatistics = new GetAppMaintenanceStatisticsDto();
            if (AbpSession.TenantId == null)
            {
                return getAppMaintenanceStatistics;
            }

            // 获取租户
            var tenant = await this.TenantManager.GetByIdAsync(AbpSession.TenantId.Value);

            if (tenant.EditionId == null)
            {
                return getAppMaintenanceStatistics;
            }

            // 通过租户版本ID获取版本信息
            var edition = await this._eccpEditionRepository.FirstOrDefaultAsync(tenant.EditionId.Value);
            if (edition.ECCPEditionsTypeId == null)
            {
                return getAppMaintenanceStatistics;
            }
            // 根据版本类型ID获取版本类型
            var eccpEditionsType = await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(edition.ECCPEditionsTypeId.Value);

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll();
                var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().Where(e => !e.IsCancel && !e.IsClose);

                var eccpBaseElevators = _eccpBaseElevatorsRepository.GetAll();
                var eccpMaintenanceContracts = _eccpMaintenanceContractRepository.GetAll().Where(e => !e.IsStop && e.EndDate > DateTime.Now);
                var users = this.UserManager.Users;

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

                var eccpMaintenanceTempWorkOrders = this._eccpMaintenanceTempWorkOrderRepository.GetAll().Where(e => e.CheckState == 1 || e.CheckState == 2);

                var eccpCompanyUserExtensions = this._eccpCompanyUserExtensionRepository.GetAll();
                var eccpMaintenanceContractElevatorLinks = this._eccpMaintenanceContractElevatorLinkRepository.GetAll();

                //临时工单数
                int quantityOfTemporaryWorkOrders = 0;
                //维保人员数量
                int numberOfMaintenancePersonnel = 0;
                //在线维保人员数量
                int numberOfOnlineMaintenancePersonnel = 0;
                //拥有电梯数
                int numberofElevatorsOwned = 0;

                if (eccpEditionsType.Name == "维保公司")
                {
                    var eccpBaseMaintenanceCompany = await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(e =>
                          e.TenantId == AbpSession.TenantId.Value);

                    eccpBaseElevators = eccpBaseElevators.Where(w => w.ECCPBaseMaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);

                    eccpMaintenanceContracts =
                        eccpMaintenanceContracts.Where(e => e.MaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);

                    users = users.Where(e => e.TenantId == AbpSession.TenantId);
                    numberOfMaintenancePersonnel = users.Count();
                    var userIds = users.Select(w => w.Id);
                    numberOfOnlineMaintenancePersonnel = eccpCompanyUserExtensions.Count(e => userIds.Contains(e.UserId) && e.IsOnline && EF.Functions.DateDiffMinute(e.Heartbeat, DateTime.Now) < 20);

                    var roleName = uRoles.Select(e => e.Name);
                    if (roleName.Contains("Admin") || roleName.Contains("MainManage") || roleName.Contains("MainInfoManage"))
                    {
                        eccpMaintenancePlans = eccpMaintenancePlans.Where(e => e.TenantId == AbpSession.TenantId);
                        numberofElevatorsOwned = eccpMaintenancePlans.Count();

                        var maintenanceContractElevatorIds = from eccpMaintenanceContract in eccpMaintenanceContracts
                                                             join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                                                 on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink
                                                                     .MaintenanceContractId
                                                             select eccpMaintenanceContractElevatorLink.ElevatorId;

                        quantityOfTemporaryWorkOrders = (from maintenanceContractElevatorId in maintenanceContractElevatorIds
                                                         join eccpMaintenanceTempWorkOrder in eccpMaintenanceTempWorkOrders
                                                             on maintenanceContractElevatorId equals eccpMaintenanceTempWorkOrder.ElevatorId
                                                         select eccpMaintenanceTempWorkOrder).Count();
                    }
                    else if (roleName.Contains("MaintPrincipal") || roleName.Contains("MainUser"))
                    {
                        var eccpMaintenancePlanMaintenanceUserLinks = this._eccpMaintenancePlanMaintenanceUserLinkRepository.GetAll().Where(e => e.UserId == AbpSession.UserId).Select(e => e.MaintenancePlanId);
                        eccpMaintenancePlans = eccpMaintenancePlans.Where(e =>
                            eccpMaintenancePlanMaintenanceUserLinks.Contains(e.Id));
                        numberofElevatorsOwned = eccpMaintenancePlans.Count();

                        quantityOfTemporaryWorkOrders =
                            eccpMaintenanceTempWorkOrders.Count(e => e.UserId == AbpSession.UserId);
                    }
                }
                else if (eccpEditionsType.Name == "物业公司")
                {
                    var eccpBasePropertyCompany = await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(e =>
                        e.TenantId == AbpSession.TenantId.Value);

                    eccpBaseElevators = eccpBaseElevators.Where(w => w.ECCPBasePropertyCompanyId == eccpBasePropertyCompany.Id);

                    eccpMaintenanceContracts =
                        eccpMaintenanceContracts.Where(e => e.PropertyCompanyId == eccpBasePropertyCompany.Id);


                    var maintenanceContractElevatorIds = from eccpMaintenanceContract in eccpMaintenanceContracts
                                                         join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                                             on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink
                                                                 .MaintenanceContractId
                                                         select eccpMaintenanceContractElevatorLink.ElevatorId;

                    numberofElevatorsOwned = maintenanceContractElevatorIds.Count();

                    using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                    {
                        var maintenanceCompanyTenantId = eccpMaintenanceContracts.GroupBy(e => e.MaintenanceCompany.TenantId)
                            .Select(e => e.Key);
                        users = users.Where(e => maintenanceCompanyTenantId.Contains(e.TenantId));
                        numberOfMaintenancePersonnel = users.Count();

                        quantityOfTemporaryWorkOrders = (from maintenanceContractElevatorId in maintenanceContractElevatorIds
                                                         join eccpMaintenanceTempWorkOrder in eccpMaintenanceTempWorkOrders
                                                             on maintenanceContractElevatorId equals eccpMaintenanceTempWorkOrder.ElevatorId
                                                         select eccpMaintenanceTempWorkOrder).Count();

                        var userIds = users.Select(w => w.Id);
                        numberOfOnlineMaintenancePersonnel = eccpCompanyUserExtensions.Count(e => userIds.Contains(e.UserId) && e.IsOnline && EF.Functions.DateDiffMinute(e.Heartbeat, DateTime.Now) < 20);
                    }
                }

                var ps = from eccpMaintenancePlan in eccpMaintenancePlans
                         join eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders
                             on eccpMaintenancePlan.Id equals eccpMaintenanceWorkOrder.MaintenancePlanId
                         select new
                         {
                             eccpMaintenancePlan.ElevatorId,
                             eccpMaintenancePlan.PollingPeriod,
                             eccpMaintenancePlan.RemindHour,
                             eccpMaintenanceWorkOrder.ComplateDate,
                             eccpMaintenanceWorkOrder.PlanCheckDate,
                             eccpMaintenanceWorkOrder.MaintenanceStatusId,
                             eccpMaintenanceWorkOrder.IsComplete
                         };

                var query = from eccpBaseElevator in eccpBaseElevators
                            join p in ps
                            on eccpBaseElevator.Id equals p.ElevatorId
                            select p;

                string[] typeNames = { "NumberOfCompletedMaintenance", "QuantityToBeMaintenance", "NumberOfDates", "OverdueQuantity", "QuantityOfTemporaryWorkOrders", "NumberOfFault", "NumberOfComplaints", "OverdueUncheckedQuantity", "NumberOfContracts", "NumberOfMaintenancePersonnel", "NumberOfOnlineMaintenancePersonnel", "NumberofElevatorsOwned", "CompleteMaintenanceElevatorQuantity", "NumberOfEmergencyRescue", "WorkOrderToBeConfirmed" };

                getAppMaintenanceStatistics = new GetAppMaintenanceStatisticsDto
                {
                    EditionsType = eccpEditionsType.Name,
                    UserRole = string.Join(",", uRoles.Select(e => e.DisplayName)),
                    AppMaintenanceStatisticsData = new List<AppMaintenanceStatisticsDataDto>()
                };

                var eccpDictMaintenanceStatus = _eccpDictMaintenanceStatusRepository.FirstOrDefault(e => e.Name == "未进行");
                var eccpDictMaintenanceStatusOverdue = _eccpDictMaintenanceStatusRepository.FirstOrDefault(e => e.Name == "已超期");
                var userId = this._eccpCompanyUserExtensionRepository.GetAll().Where(e => e.UserId == AbpSession.UserId)
                    .Select(e => e.SyncUserId).FirstOrDefault();
                foreach (var typeName in typeNames)
                {
                    AppMaintenanceStatisticsDataDto appMaintenanceStatisticsDataDto =
                        new AppMaintenanceStatisticsDataDto
                        {
                            TypeName = typeName
                        };
                    switch (typeName)
                    {
                        case "NumberOfCompletedMaintenance":
                            appMaintenanceStatisticsDataDto.StatisticsNum = query.Count(w => w.IsComplete && EF.Functions.DateDiffDay(DateTime.Now, w.ComplateDate) < 31);//已完成维保数量
                            break;
                        case "WorkOrderToBeConfirmed":
                            appMaintenanceStatisticsDataDto.StatisticsNum = query.Count(w => w.IsComplete);//待确认工单
                            break;
                        case "QuantityToBeMaintenance":
                            appMaintenanceStatisticsDataDto.StatisticsNum = query.Count(w => w.MaintenanceStatusId == eccpDictMaintenanceStatus.Id && EF.Functions.DateDiffDay(DateTime.Now, w.PlanCheckDate) < 31);//待维保数量
                            break;
                        case "NumberOfDates":
                            appMaintenanceStatisticsDataDto.StatisticsNum = query.Count(w => EF.Functions.DateDiffHour(DateTime.Now, w.PlanCheckDate) < w.RemindHour);//临期数量
                            break;
                        case "OverdueQuantity":
                            appMaintenanceStatisticsDataDto.StatisticsNum = query.Count(w => w.MaintenanceStatusId == eccpDictMaintenanceStatusOverdue.Id && EF.Functions.DateDiffDay(DateTime.Now, w.PlanCheckDate) < 31);//超期数量
                            break;
                        case "QuantityOfTemporaryWorkOrders":
                            appMaintenanceStatisticsDataDto.StatisticsNum = quantityOfTemporaryWorkOrders;//临时工单数量
                            break;
                        case "NumberOfFault":
                            if (userId != null)
                            {
                                //TODO: 接口地址暂时写死
                                string result = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetFaultyElevatorCount?userId=" + userId);
                                if (int.TryParse(result, out var numberOfFault))
                                {
                                    appMaintenanceStatisticsDataDto.StatisticsNum = numberOfFault;//故障数量

                                    if (appMaintenanceStatisticsDataDto.StatisticsNum > 0)
                                    {
                                        //TODO: 发现有故障电梯，推送消息，暂时这么处理
                                        var accessId = Convert.ToInt32(_appConfiguration["Xinge:AccessId"]);
                                        var secretKey = _appConfiguration["Xinge:SecretKey"];
                                        var packageName = _appConfiguration["Xinge:PackageName"];

                                        XingeApp.XingeApp xingeApp = new XingeApp.XingeApp(accessId, secretKey);

                                        XingeApp.Message message_Android = new XingeApp.Message();
                                        message_Android.setExpireTime(84600);
                                        message_Android.setTitle("发现" + appMaintenanceStatisticsDataDto.StatisticsNum + "台故障电梯");
                                        message_Android.setType(1);
                                        message_Android.setContent("点击查看故障电梯分布");

                                        var customParams = new Dictionary<string, object>();
                                        customParams.Add("page", 0);
                                        customParams.Add("action", 6);
                                        message_Android.setCustom(customParams);

                                        var clickAction = new XingeApp.ClickAction();
                                        clickAction.setActionType(1);
                                        clickAction.setActivity(packageName + ".activity.MainActivity");
                                        message_Android.setAction(clickAction);

                                        var tmpResult = xingeApp.PushSingleAccount(this.AbpSession.UserId.ToString(), message_Android);
                                    }
                                }
                            }
                            break;
                        case "NumberOfComplaints":
                            if (userId != null)
                            {
                                //TODO: 接口地址暂时写死
                                string result = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetComplaintCount?userId=" + userId);
                                if (int.TryParse(result, out var numberOfComplaints))
                                {
                                    appMaintenanceStatisticsDataDto.StatisticsNum = numberOfComplaints;//投诉数量
                                }
                            }
                            break;
                        case "OverdueUncheckedQuantity"://超期未检数量
                            break;
                        case "NumberOfContracts":
                            appMaintenanceStatisticsDataDto.StatisticsNum = eccpMaintenanceContracts.Count();//合同数量
                            break;
                        case "NumberOfMaintenancePersonnel":
                            appMaintenanceStatisticsDataDto.StatisticsNum = numberOfMaintenancePersonnel;//维保人员数量
                            break;
                        case "NumberOfOnlineMaintenancePersonnel":
                            appMaintenanceStatisticsDataDto.StatisticsNum = numberOfOnlineMaintenancePersonnel;//在线维保人员数量
                            break;
                        case "NumberofElevatorsOwned":
                            appMaintenanceStatisticsDataDto.StatisticsNum = numberofElevatorsOwned;//拥有电梯数量
                            break;
                        case "CompleteMaintenanceElevatorQuantity":
                            appMaintenanceStatisticsDataDto.StatisticsNum = query.Where(w => w.IsComplete).GroupBy(e => e.ElevatorId).Count(); //完成维保电梯数量
                            break;
                        case "NumberOfEmergencyRescue":
                            if (userId != null)
                            {
                                //TODO: 接口地址暂时写死
                                string result = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetEmergencyRescueElevatorCount?userId=" + userId);
                                if (int.TryParse(result, out var numberOfEmergencyRescue))
                                {
                                    appMaintenanceStatisticsDataDto.StatisticsNum = numberOfEmergencyRescue;//应急救援数
                                }
                            }
                            break;
                    }
                    getAppMaintenanceStatistics.AppMaintenanceStatisticsData.Add(appMaintenanceStatisticsDataDto);
                }

                return getAppMaintenanceStatistics;
            }
        }

        /// <summary>
        /// 手机端首页地图用维保统计的接口
        /// </summary>
        /// <returns></returns>
        public async Task<GetAppIndexMapMaintenanceStatisticsDto> GetAppIndexMapMaintenanceStatistics()
        {
            var getAppIndexMapMaintenanceStatistics = new GetAppIndexMapMaintenanceStatisticsDto();
            if (AbpSession.TenantId == null)
            {
                return getAppIndexMapMaintenanceStatistics;
            }

            // 获取租户
            var tenant = await this.TenantManager.GetByIdAsync(AbpSession.TenantId.Value);

            if (tenant.EditionId == null)
            {
                return getAppIndexMapMaintenanceStatistics;
            }

            // 通过租户版本ID获取版本信息
            var edition = await this._eccpEditionRepository.FirstOrDefaultAsync(tenant.EditionId.Value);
            if (edition.ECCPEditionsTypeId == null)
            {
                return getAppIndexMapMaintenanceStatistics;
            }
            // 根据版本类型ID获取版本类型
            var eccpEditionsType = await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(edition.ECCPEditionsTypeId.Value);

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var dateTime = DateTime.Now.AddDays(-30);
                var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll().Where(w => w.PlanCheckDate > dateTime);
                var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().Where(e => !e.IsCancel && !e.IsClose);

                var eccpBaseElevators = _eccpBaseElevatorsRepository.GetAll();
                var users = this.UserManager.Users;

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

                var eccpMaintenanceTempWorkOrders = this._eccpMaintenanceTempWorkOrderRepository.GetAll().Where(e => e.CheckState == 1 || e.CheckState == 2);

                var eccpCompanyUserExtensions = this._eccpCompanyUserExtensionRepository.GetAll();
                var eccpMaintenanceContracts = this._eccpMaintenanceContractRepository.GetAll().Where(e => !e.IsStop && e.EndDate > DateTime.Now);
                var eccpMaintenanceContractElevatorLinks = this._eccpMaintenanceContractElevatorLinkRepository.GetAll();

                //临时工单数
                int quantityOfTemporaryWorkOrders = 0;
                //在线维保人员数量
                int numberOfOnlineMaintenancePersonnel = 0;
                //拥有电梯数
                int numberofElevatorsOwned = 0;

                if (eccpEditionsType.Name == "维保公司")
                {
                    var eccpBaseMaintenanceCompany = await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(e =>
                          e.TenantId == AbpSession.TenantId.Value);

                    eccpBaseElevators = eccpBaseElevators.Where(w => w.ECCPBaseMaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);

                    var userIds = users.Where(e => e.TenantId == AbpSession.TenantId).Select(w => w.Id);
                    numberOfOnlineMaintenancePersonnel = eccpCompanyUserExtensions.Count(e => userIds.Contains(e.UserId) && e.IsOnline && EF.Functions.DateDiffMinute(e.Heartbeat, DateTime.Now) < 20);

                    var roleName = uRoles.Select(e => e.Name);
                    if (roleName.Contains("Admin") || roleName.Contains("MainManage") || roleName.Contains("MainInfoManage"))
                    {
                        eccpMaintenancePlans = eccpMaintenancePlans.Where(e => e.TenantId == AbpSession.TenantId);

                        eccpMaintenanceContracts = eccpMaintenanceContracts.Where(e => e.MaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);

                        var maintenanceContractElevatorIds = from eccpMaintenanceContract in eccpMaintenanceContracts
                                                             join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                                                 on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink
                                                                     .MaintenanceContractId
                                                             select eccpMaintenanceContractElevatorLink.ElevatorId;
                        numberofElevatorsOwned = maintenanceContractElevatorIds.Count();

                        quantityOfTemporaryWorkOrders = (from maintenanceContractElevatorId in maintenanceContractElevatorIds
                                                         join eccpMaintenanceTempWorkOrder in eccpMaintenanceTempWorkOrders
                                                             on maintenanceContractElevatorId equals eccpMaintenanceTempWorkOrder.ElevatorId
                                                         select eccpMaintenanceTempWorkOrder).GroupBy(e => e.ElevatorId).Count();
                    }
                    else if (roleName.Contains("MaintPrincipal") || roleName.Contains("MainUser"))
                    {
                        var eccpMaintenancePlanMaintenanceUserLinks = this._eccpMaintenancePlanMaintenanceUserLinkRepository.GetAll().Where(e => e.UserId == AbpSession.UserId).Select(e => e.MaintenancePlanId);
                        eccpMaintenancePlans = eccpMaintenancePlans.Where(e =>
                            eccpMaintenancePlanMaintenanceUserLinks.Contains(e.Id));
                        numberofElevatorsOwned = eccpMaintenancePlans.Count();

                        quantityOfTemporaryWorkOrders =
                            eccpMaintenanceTempWorkOrders.GroupBy(e => e.ElevatorId).Count(e => e.FirstOrDefault().UserId == AbpSession.UserId);
                    }
                }
                else if (eccpEditionsType.Name == "物业公司")
                {
                    var eccpBasePropertyCompany = await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(e =>
                        e.TenantId == AbpSession.TenantId.Value);

                    eccpBaseElevators = eccpBaseElevators.Where(w => w.ECCPBasePropertyCompanyId == eccpBasePropertyCompany.Id);

                    eccpMaintenanceContracts =
                       eccpMaintenanceContracts.Where(e => e.PropertyCompanyId == eccpBasePropertyCompany.Id);

                    var maintenanceContractElevatorIds = from eccpMaintenanceContract in eccpMaintenanceContracts
                                                         join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                                             on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink
                                                                 .MaintenanceContractId
                                                         select eccpMaintenanceContractElevatorLink.ElevatorId;
                    numberofElevatorsOwned = maintenanceContractElevatorIds.Count();

                    using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                    {
                        var maintenanceCompanyTenantId = eccpMaintenanceContracts.GroupBy(e => e.MaintenanceCompany.TenantId)
                            .Select(e => e.Key);
                        var userIds = users.Where(e => maintenanceCompanyTenantId.Contains(e.TenantId)).Select(w => w.Id);

                        quantityOfTemporaryWorkOrders = (from maintenanceContractElevatorId in maintenanceContractElevatorIds
                                                         join eccpMaintenanceTempWorkOrder in eccpMaintenanceTempWorkOrders
                                                             on maintenanceContractElevatorId equals eccpMaintenanceTempWorkOrder.ElevatorId
                                                         select eccpMaintenanceTempWorkOrder).GroupBy(e => e.ElevatorId).Count();
                        numberOfOnlineMaintenancePersonnel = eccpCompanyUserExtensions.Count(e => userIds.Contains(e.UserId) && e.IsOnline && EF.Functions.DateDiffMinute(e.Heartbeat, DateTime.Now) < 20);
                    }
                }

                var ps = from eccpMaintenancePlan in eccpMaintenancePlans
                         join eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders
                             on eccpMaintenancePlan.Id equals eccpMaintenanceWorkOrder.MaintenancePlanId
                         select new
                         {
                             eccpMaintenancePlan.ElevatorId,
                             eccpMaintenancePlan.PollingPeriod,
                             eccpMaintenancePlan.RemindHour,
                             eccpMaintenanceWorkOrder.ComplateDate,
                             eccpMaintenanceWorkOrder.PlanCheckDate,
                             eccpMaintenanceWorkOrder.MaintenanceStatusId,
                             eccpMaintenanceWorkOrder.IsComplete
                         };

                var query = from eccpBaseElevator in eccpBaseElevators
                            join p in ps
                            on eccpBaseElevator.Id equals p.ElevatorId
                            select p;

                var eccpDictMaintenanceStatusWaiting = _eccpDictMaintenanceStatusRepository.FirstOrDefault(e => e.Name == "未进行");
                var eccpDictMaintenanceStatusCompleted = _eccpDictMaintenanceStatusRepository.FirstOrDefault(e => e.Name == "已完成");
                var eccpDictMaintenanceStatusOverdue = _eccpDictMaintenanceStatusRepository.FirstOrDefault(e => e.Name == "已超期");
                var sg = query.GroupBy(e => e.ElevatorId).Select(f => new
                {
                    CycleElevatorNum = f.Count(w => EF.Functions.DateDiffDay(DateTime.Now, w.ComplateDate) < 31),
                    WaitingMaintenance = f.Count(w => w.MaintenanceStatusId == eccpDictMaintenanceStatusWaiting.Id && EF.Functions.DateDiffDay(DateTime.Now, w.PlanCheckDate) < 31),
                    OverdueMaintenance = f.Count(w => w.MaintenanceStatusId == eccpDictMaintenanceStatusOverdue.Id && EF.Functions.DateDiffDay(DateTime.Now, w.PlanCheckDate) < 31),
                    TemporaryMaintenanceElevatorNum = f.Count(w => w.MaintenanceStatusId != eccpDictMaintenanceStatusCompleted.Id && EF.Functions.DateDiffHour(DateTime.Now, w.PlanCheckDate) < w.RemindHour)
                });

                int numberOfFailedElevators = 0;
                int numberOfComplaintElevators = 0;
                int numberOfEmergencyRescueElevators = 0;
                var userId = this._eccpCompanyUserExtensionRepository.GetAll().Where(e => e.UserId == AbpSession.UserId)
                    .Select(e => e.SyncUserId).FirstOrDefault();
                if (userId != null)
                {
                    //TODO: 接口地址暂时写死
                    string resultNumberOfFailedElevators = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetFaultyElevatorCount?userId=" + userId);
                    if (int.TryParse(resultNumberOfFailedElevators, out var numberOfFault))
                    {
                        numberOfFailedElevators = numberOfFault;//故障数量
                    }

                    //TODO: 接口地址暂时写死
                    string resultNumberOfComplaintElevators = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetComplaintElevatorCount?userId=" + userId);
                    if (int.TryParse(resultNumberOfComplaintElevators, out var numberOfComplaints))
                    {
                        numberOfComplaintElevators = numberOfComplaints;//投诉数量
                    }

                    //TODO: 接口地址暂时写死
                    string result = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetEmergencyRescueElevatorCount?userId=" + userId);
                    if (int.TryParse(result, out var numberOfEmergencyRescue))
                    {
                        numberOfEmergencyRescueElevators = numberOfEmergencyRescue;//应急救援数
                    }
                }
                getAppIndexMapMaintenanceStatistics = new GetAppIndexMapMaintenanceStatisticsDto
                {
                    EditionsType = eccpEditionsType.Name,
                    UserRole = string.Join(",", uRoles.Select(e => e.DisplayName)),
                    AppIndexMapMaintenanceStatisticsData = new AppIndexMapMaintenanceStatisticsDataDto
                    {
                        NumberOfElevatorsCompletedMaintenance = sg.Count(w => w.CycleElevatorNum > 0),
                        NumberOfElevatorsToBeMaintained = sg.Count(w => w.WaitingMaintenance > 0),
                        QuantityOfElevatorsInTransit = sg.Count(w => w.TemporaryMaintenanceElevatorNum > 0),
                        NumberOfOverdueElevators = sg.Count(w => w.OverdueMaintenance > 0),
                        NumberOfElevatorsWithTempWorkOrders = quantityOfTemporaryWorkOrders,
                        NumberOfFailedElevators = numberOfFailedElevators,
                        NumberOfComplaintElevators = numberOfComplaintElevators,
                        OverdueElevatorQuantity = 0,
                        NumberOfOnlineMaintenancePersonnel = numberOfOnlineMaintenancePersonnel,
                        NumberOfElevatorsOwned = numberofElevatorsOwned,
                        NumberOfEmergencyRescue = numberOfEmergencyRescueElevators
                    }
                };
                return getAppIndexMapMaintenanceStatistics;
            }
        }

        /// <summary>
        /// App首页区域筛选接口
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public async Task<List<GetAreasDto>> GetAreas(int areaId)
        {
            var areas = new List<GetAreasDto>();
            if (AbpSession.TenantId == null)
            {
                return areas;
            }

            // 获取租户
            var tenant = await this.TenantManager.GetByIdAsync(AbpSession.TenantId.Value);

            if (tenant.EditionId == null)
            {
                return areas;
            }

            // 通过租户版本ID获取版本信息
            var edition = await this._eccpEditionRepository.FirstOrDefaultAsync(tenant.EditionId.Value);
            if (edition.ECCPEditionsTypeId == null)
            {
                return areas;
            }
            // 根据版本类型ID获取版本类型
            var eccpEditionsType = await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(edition.ECCPEditionsTypeId.Value);
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().Where(e => !e.IsCancel && !e.IsClose);
                var eccpBaseElevators = _eccpBaseElevatorsRepository.GetAll();
                var baseArea = this._eccpBaseAreaRepository.GetAll().FirstOrDefault(w => w.Id == areaId);

                if (areaId > 0)
                {
                    if (baseArea != null && baseArea.Level == 2)
                    {
                        eccpBaseElevators = eccpBaseElevators.Where(e => e.DistrictId == baseArea.Id);
                    }
                    else if (baseArea != null && baseArea.Level == 1)
                    {
                        eccpBaseElevators = eccpBaseElevators.Where(e => e.CityId == baseArea.Id);
                    }
                }

                var eccpMaintenanceContracts = this._eccpMaintenanceContractRepository.GetAll().Where(e => !e.IsStop && e.EndDate > DateTime.Now);
                var eccpMaintenanceContractElevatorLinks = this._eccpMaintenanceContractElevatorLinkRepository.GetAll();
                var maintenanceContractElevators = from eccpMaintenanceContract in eccpMaintenanceContracts
                                                   join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                                       on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink
                                                           .MaintenanceContractId
                                                   select eccpMaintenanceContractElevatorLink.ElevatorId;



                if (eccpEditionsType.Name == "维保公司")
                {
                    var eccpBaseMaintenanceCompany = await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(e =>
                          e.TenantId == AbpSession.TenantId.Value);

                    eccpBaseElevators = eccpBaseElevators.Where(w => w.ECCPBaseMaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);

                    var roles = this._roleManager.Roles;
                    var userRoles = this._userRoleRepository.GetAll().Where(e => e.UserId == AbpSession.UserId);
                    var roleName = from role in roles
                                   join userRole in userRoles
                                       on role.Id equals userRole.RoleId
                                   select role.Name;

                    if (roleName.Contains("MaintPrincipal") || roleName.Contains("MainUser"))
                    {
                        var eccpMaintenancePlanMaintenanceUserLinks = this._eccpMaintenancePlanMaintenanceUserLinkRepository.GetAll().Where(e => e.UserId == AbpSession.UserId);

                        var eccpMaintenancePlanElevatorIds = from eccpMaintenancePlanMaintenanceUserLink in eccpMaintenancePlanMaintenanceUserLinks
                                                             join eccpMaintenancePlan in eccpMaintenancePlans
                                                                 on eccpMaintenancePlanMaintenanceUserLink.MaintenancePlanId equals eccpMaintenancePlan
                                                                     .Id
                                                             select eccpMaintenancePlan.ElevatorId;

                        eccpBaseElevators = from eccpBaseElevator in eccpBaseElevators
                                            join eccpMaintenancePlanElevatorId in eccpMaintenancePlanElevatorIds
                                                on eccpBaseElevator.Id equals eccpMaintenancePlanElevatorId
                                            select eccpBaseElevator;
                    }
                }
                else if (eccpEditionsType.Name == "物业公司")
                {
                    var eccpBasePropertyCompany = await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(e =>
                        e.TenantId == AbpSession.TenantId.Value);

                    eccpBaseElevators = eccpBaseElevators.Where(w => w.ECCPBasePropertyCompanyId == eccpBasePropertyCompany.Id);
                }

                var baseElevators = from eccpBaseElevator in eccpBaseElevators
                                    join maintenanceContractElevator in maintenanceContractElevators
                                        on eccpBaseElevator.Id equals maintenanceContractElevator.Value
                                    select eccpBaseElevator;

                if (areaId == 0)
                {
                    areas = baseElevators.GroupBy(g => g.City.Name).Select(e => new GetAreasDto
                    {
                        CityId = e.FirstOrDefault().CityId,
                        Name = e.Key,
                        ElevatorNum = e.Count(),
                        LongitudeAndLatitude = BaiduMapPoint(e.Key)
                    }).ToList();
                }
                else
                {
                    if (baseArea != null && baseArea.Level == 2)
                    {
                        var baseAreaCity = this._eccpBaseAreaRepository.GetAll().FirstOrDefault(w => w.Id == baseArea.ParentId);
                        areas = baseElevators.GroupBy(g => g.ECCPBaseCommunity.Name).Select(e => new GetAreasDto
                        {
                            CommunityId = e.FirstOrDefault().ECCPBaseCommunityId,
                            Name = e.Key != null && e.Key != "" ? e.Key : "未知园区",
                            ElevatorNum = e.Count(),
                            LongitudeAndLatitude = e.FirstOrDefault().ECCPBaseCommunity == null ? BaiduMapPoint(baseAreaCity.Name + "," + baseArea.Name) : e.FirstOrDefault().ECCPBaseCommunity.Latitude + "," + e.FirstOrDefault().ECCPBaseCommunity.Longitude
                        }).ToList();
                    }
                    else if (baseArea != null && baseArea.Level == 1)
                    {
                        areas = baseElevators.GroupBy(g => g.District.Name).Select(e => new GetAreasDto
                        {
                            DistrictId = e.FirstOrDefault().DistrictId,
                            Name = e.Key,
                            ElevatorNum = e.Count(),
                            LongitudeAndLatitude = BaiduMapPoint(baseArea.Name + "," + e.Key)
                        }).ToList();
                    }
                }
                return areas;
            }
        }

        /// <summary>
        /// 根据名字返回坐标点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string BaiduMapPoint(string name)
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
        /// App首页临时工单（维修工单）接口
        /// </summary>
        /// <returns></returns>
        public async Task<GetMaintenanceTempWorkOrdersElevatorListDto> GetMaintenanceTempWorkOrdersElevatorList()
        {
            var getMaintenanceTempWorkOrdersElevatorList = new GetMaintenanceTempWorkOrdersElevatorListDto();
            if (AbpSession.TenantId == null)
            {
                return getMaintenanceTempWorkOrdersElevatorList;
            }

            // 获取租户
            var tenant = await this.TenantManager.GetByIdAsync(AbpSession.TenantId.Value);

            if (tenant.EditionId == null)
            {
                return getMaintenanceTempWorkOrdersElevatorList;
            }

            // 通过租户版本ID获取版本信息
            var edition = await this._eccpEditionRepository.FirstOrDefaultAsync(tenant.EditionId.Value);
            if (edition.ECCPEditionsTypeId == null)
            {
                return getMaintenanceTempWorkOrdersElevatorList;
            }
            // 根据版本类型ID获取版本类型
            var eccpEditionsType = await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(edition.ECCPEditionsTypeId.Value);

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var eccpBaseElevators = _eccpBaseElevatorsRepository.GetAll();

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

                var eccpMaintenanceTempWorkOrders = this._eccpMaintenanceTempWorkOrderRepository.GetAll().Where(e => e.CheckState == 1 || e.CheckState == 2);

                var eccpCompanyUserExtensions = this._eccpCompanyUserExtensionRepository.GetAll();
                var eccpMaintenanceContracts = this._eccpMaintenanceContractRepository.GetAll().Where(e => !e.IsStop && e.EndDate > DateTime.Now);
                var eccpMaintenanceContractElevatorLinks = this._eccpMaintenanceContractElevatorLinkRepository.GetAll();

                if (eccpEditionsType.Name == "维保公司")
                {
                    var eccpBaseMaintenanceCompany = await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(e =>
                          e.TenantId == AbpSession.TenantId.Value);

                    eccpBaseElevators = eccpBaseElevators.Where(w => w.ECCPBaseMaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);

                    var roleName = uRoles.Select(e => e.Name);
                    if (roleName.Contains("Admin") || roleName.Contains("MainManage") || roleName.Contains("MainInfoManage"))
                    {
                        eccpMaintenanceContracts = eccpMaintenanceContracts.Where(e => e.MaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);

                        var maintenanceContractElevatorIds = from eccpMaintenanceContract in eccpMaintenanceContracts
                                                             join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                                                 on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink
                                                                     .MaintenanceContractId
                                                             select eccpMaintenanceContractElevatorLink.ElevatorId;


                        eccpMaintenanceTempWorkOrders = from maintenanceContractElevatorId in maintenanceContractElevatorIds
                                                        join eccpMaintenanceTempWorkOrder in eccpMaintenanceTempWorkOrders
                                                            on maintenanceContractElevatorId equals eccpMaintenanceTempWorkOrder.ElevatorId
                                                        select eccpMaintenanceTempWorkOrder;
                    }
                    else if (roleName.Contains("MaintPrincipal") || roleName.Contains("MainUser"))
                    {
                        eccpMaintenanceTempWorkOrders =
                            eccpMaintenanceTempWorkOrders.Where(e => e.UserId == AbpSession.UserId);
                    }
                }
                else if (eccpEditionsType.Name == "物业公司")
                {
                    var eccpBasePropertyCompany = await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(e =>
                        e.TenantId == AbpSession.TenantId.Value);

                    eccpBaseElevators = eccpBaseElevators.Where(w => w.ECCPBasePropertyCompanyId == eccpBasePropertyCompany.Id);

                    eccpMaintenanceContracts =
                       eccpMaintenanceContracts.Where(e => e.PropertyCompanyId == eccpBasePropertyCompany.Id);

                    var maintenanceContractElevatorIds = from eccpMaintenanceContract in eccpMaintenanceContracts
                                                         join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                                             on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink
                                                                 .MaintenanceContractId
                                                         select eccpMaintenanceContractElevatorLink.ElevatorId;

                    using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                    {

                        eccpMaintenanceTempWorkOrders = from maintenanceContractElevatorId in maintenanceContractElevatorIds
                                                        join eccpMaintenanceTempWorkOrder in eccpMaintenanceTempWorkOrders
                                                            on maintenanceContractElevatorId equals eccpMaintenanceTempWorkOrder.ElevatorId
                                                        select eccpMaintenanceTempWorkOrder;
                    }
                }

                var query = from eccpBaseElevator in eccpBaseElevators
                            join eccpMaintenanceTempWorkOrder in eccpMaintenanceTempWorkOrders
                            on eccpBaseElevator.Id equals eccpMaintenanceTempWorkOrder.ElevatorId
                            join eccpCompanyUserExtension in eccpCompanyUserExtensions
                                on eccpMaintenanceTempWorkOrder.UserId equals eccpCompanyUserExtension.UserId into g
                            from f in g.DefaultIfEmpty()
                            select new
                            {
                                eccpBaseElevator.Id,
                                eccpBaseElevator.CertificateNum,
                                eccpBaseElevator.Longitude,
                                eccpBaseElevator.Latitude,
                                TempWorkOrderId = eccpMaintenanceTempWorkOrder.Id,
                                UserName = eccpMaintenanceTempWorkOrder.User.Name,
                                f.Mobile,
                                eccpMaintenanceTempWorkOrder.Title,
                                eccpMaintenanceTempWorkOrder.Describe,
                                TempWorkOrderTypeName = eccpMaintenanceTempWorkOrder.TempWorkOrderType.Name
                            };

                var maintenanceTempWorkOrdersElevators = query.GroupBy(e => e.Id)
                    .Select(e => new GetMaintenanceTempWorkOrdersElevatorsDto
                    {
                        ElevatorId = e.Key,
                        CertificateNum = e.FirstOrDefault().CertificateNum,
                        Longitude = e.FirstOrDefault().Longitude,
                        Latitude = e.FirstOrDefault().Latitude,
                        MaintenanceTempWorkOrders = e.Select(w => new GetMaintenanceTempWorkOrdersDto
                        {
                            TempWorkOrderId = w.TempWorkOrderId,
                            UserName = w.UserName,
                            UserMobile = w.Mobile,
                            Title = w.Title,
                            Describe = w.Describe,
                            TempWorkOrderTypeName = w.TempWorkOrderTypeName
                        }).ToList()
                    });

                getMaintenanceTempWorkOrdersElevatorList = new GetMaintenanceTempWorkOrdersElevatorListDto
                {
                    CountNum = maintenanceTempWorkOrdersElevators.Count(),
                    MaintenanceTempWorkOrdersElevators = maintenanceTempWorkOrdersElevators.ToList()
                };
                return getMaintenanceTempWorkOrdersElevatorList;
            }
        }

        /// <summary>
        /// 获取救援信息
        /// </summary>
        /// <returns></returns>
        public GetTaskListDto GetTaskList()
        {
            var taskList = new GetTaskListDto();
            if (AbpSession.UserId == null)
            {
                return taskList;
            }
            var userId = this._eccpCompanyUserExtensionRepository.GetAll().Where(e => e.UserId == AbpSession.UserId)
                .Select(e => e.SyncUserId).FirstOrDefault();
            if (userId != null)
            {
                string json = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetFaultyElevatorList?userId=" + userId);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    taskList = Newtonsoft.Json.JsonConvert.DeserializeObject<GetTaskListDto>(json);
                }
            }
            return taskList;
        }

        /// <summary>
        /// 获取投诉信息
        /// </summary>
        /// <returns></returns>
        public GetAdviceListDto GetAdviceList()
        {
            var adviceList = new GetAdviceListDto();
            if (AbpSession.UserId == null)
            {
                return adviceList;
            }
            var userId = this._eccpCompanyUserExtensionRepository.GetAll().Where(e => e.UserId == AbpSession.UserId)
                .Select(e => e.SyncUserId).FirstOrDefault();
            if (userId != null)
            {
                string json = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetAdviceElevatorList?userId=" + userId);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    adviceList = JsonConvert.DeserializeObject<GetAdviceListDto>(json);
                    if (adviceList != null && adviceList.LiftInfos.Count > 0)
                    {
                        foreach (var liftInfo in adviceList.LiftInfos)
                        {
                            var eccpBaseElevator = this._eccpBaseElevatorsRepository
                                .GetAll().FirstOrDefault(e => e.SyncElevatorId == liftInfo.LiftId);
                            if (eccpBaseElevator != null)
                            {
                                liftInfo.ElevatorId = eccpBaseElevator.Id;
                            }
                        }
                    }
                }
            }
            return adviceList;
        }

        /// <summary>
        /// 解除四维监控故障
        /// </summary>
        /// <returns></returns>
        public GetJsonMessageDto Troubleshooting(GetTroubleshootingInput input)
        {
            var jsonMessageDto = new GetJsonMessageDto
            {
                Success = false,
                Message = "操作失败"
            };

            if (AbpSession.UserId == null)
            {
                jsonMessageDto.Message = "用户ID错误";
                return jsonMessageDto;
            }

            if (input.ElevatorId <= 0)
            {
                jsonMessageDto.Message = "电梯ID错误";
                return jsonMessageDto;
            }

            var userId = this._eccpCompanyUserExtensionRepository.GetAll().Where(e => e.UserId == AbpSession.UserId)
                .Select(e => e.SyncUserId).FirstOrDefault() ?? 0;
            string result = HttpGet("http://www.dianti119.com/API/ElevatorCloud/Troubleshooting?liftId=" + input.ElevatorId + "&userId=" + userId);
            if (int.TryParse(result, out var resultNum))
            {
                if (resultNum == 1)
                {
                    jsonMessageDto.Success = true;
                    jsonMessageDto.Message = "操作成功";
                }
            }
            return jsonMessageDto;
        }

        /// <summary>
        /// 处理投诉
        /// </summary>
        /// <returns></returns>
        public GetJsonMessageDto HandlingComplaints(GetHandlingComplaintsInput input)
        {
            var jsonMessageDto = new GetJsonMessageDto
            {
                Success = false,
                Message = "操作失败"
            };

            if (AbpSession.UserId == null)
            {
                jsonMessageDto.Message = "用户ID错误";
                return jsonMessageDto;
            }

            if (input.Id <= 0)
            {
                jsonMessageDto.Message = "ID错误";
                return jsonMessageDto;
            }

            var userId = this._eccpCompanyUserExtensionRepository.GetAll().Where(e => e.UserId == AbpSession.UserId)
                             .Select(e => e.SyncUserId).FirstOrDefault() ?? 0;
            string result = HttpGet("http://www.dianti119.com/API/ElevatorCloud/HandlingComplaints?id=" + input.Id + "&userId=" + userId + "&remark=" + input.Remark);
            if (int.TryParse(result, out var resultNum))
            {
                if (resultNum == 1)
                {
                    jsonMessageDto.Success = true;
                    jsonMessageDto.Message = "操作成功";
                }
            }
            return jsonMessageDto;
        }

        /// <summary>
        /// 获取故障信息
        /// </summary>
        /// <returns></returns>
        public GetEquipmentListDto GetEquipmentList()
        {
            var equipmentList = new GetEquipmentListDto();
            if (AbpSession.UserId == null)
            {
                return equipmentList;
            }
            var userId = this._eccpCompanyUserExtensionRepository.GetAll().Where(e => e.UserId == AbpSession.UserId)
                .Select(e => e.SyncUserId).FirstOrDefault();
            if (userId != null)
            {
                string json = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetEquipmentElevatorList?userId=" + userId);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    equipmentList = JsonConvert.DeserializeObject<GetEquipmentListDto>(json);
                    if (equipmentList != null && equipmentList.EquipmentLiftInfos.Count > 0)
                    {
                        foreach (var liftInfo in equipmentList.EquipmentLiftInfos)
                        {
                            var eccpBaseElevator = this._eccpBaseElevatorsRepository
                                .GetAll().FirstOrDefault(e => e.SyncElevatorId == liftInfo.LiftId);
                            if (eccpBaseElevator != null)
                            {
                                liftInfo.ElevatorId = eccpBaseElevator.Id;
                            }
                        }
                    }
                }
            }
            return equipmentList;
        }

        /// <summary>
        /// App首页用维保公司/使用单位列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetCompanyDataDto>> GetCompanyDataList(GetCompanyDataInput input)
        {
            var companyDataList = new List<GetCompanyDataDto>();
            if (AbpSession.TenantId == null)
            {
                return companyDataList;
            }

            // 获取租户
            var tenant = await this.TenantManager.GetByIdAsync(AbpSession.TenantId.Value);

            if (tenant.EditionId == null)
            {
                return companyDataList;
            }

            // 通过租户版本ID获取版本信息
            var edition = await this._eccpEditionRepository.FirstOrDefaultAsync(tenant.EditionId.Value);
            if (edition.ECCPEditionsTypeId == null)
            {
                return companyDataList;
            }
            // 根据版本类型ID获取版本类型
            var eccpEditionsType = await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(edition.ECCPEditionsTypeId.Value);
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var eccpBaseElevators = this._eccpBaseElevatorsRepository.GetAll();
                var dateTime = DateTime.Now.AddDays(-30);
                var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll().Where(w => w.PlanCheckDate > dateTime);
                var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().Where(e => !e.IsCancel && !e.IsClose);
                var eccpMaintenanceTempWorkOrders = this._eccpMaintenanceTempWorkOrderRepository.GetAll().Where(e => e.CheckState == 1 || e.CheckState == 2);

                if (eccpEditionsType.Name == "维保公司")
                {
                    var eccpBaseMaintenanceCompany =
                        await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(e =>
                            e.TenantId == AbpSession.TenantId.Value);
                    eccpBaseElevators =
                        eccpBaseElevators.Where(e => e.ECCPBaseMaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);
                    var roles = this._roleManager.Roles;
                    var userRoles = this._userRoleRepository.GetAll().Where(e => e.UserId == AbpSession.UserId);
                    var roleName = from role in roles
                                   join userRole in userRoles
                                       on role.Id equals userRole.RoleId
                                   select role.Name;

                    if (roleName.Contains("Admin") || roleName.Contains("MainManage") || roleName.Contains("MainInfoManage"))
                    {
                        eccpMaintenancePlans = eccpMaintenancePlans.Where(e => e.TenantId == AbpSession.TenantId);
                    }
                    else if (roleName.Contains("MaintPrincipal") || roleName.Contains("MainUser"))
                    {
                        var eccpMaintenancePlanMaintenanceUserLinks = this._eccpMaintenancePlanMaintenanceUserLinkRepository.GetAll().Where(e => e.UserId == AbpSession.UserId).Select(e => e.MaintenancePlanId);
                        eccpMaintenancePlans = eccpMaintenancePlans.Where(e =>
                            eccpMaintenancePlanMaintenanceUserLinks.Contains(e.Id));
                    }
                }
                else if (eccpEditionsType.Name == "物业公司")
                {
                    var eccpBasePropertyCompany = await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(e =>
                        e.TenantId == AbpSession.TenantId.Value);
                    eccpBaseElevators =
                        eccpBaseElevators.Where(e => e.ECCPBasePropertyCompanyId == eccpBasePropertyCompany.Id);
                }

                var ps = from eccpMaintenancePlan in eccpMaintenancePlans
                         join eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders
                             on eccpMaintenancePlan.Id equals eccpMaintenanceWorkOrder.MaintenancePlanId
                         select new
                         {
                             eccpMaintenancePlan.ElevatorId,
                             eccpMaintenancePlan.PollingPeriod,
                             eccpMaintenancePlan.RemindHour,
                             eccpMaintenanceWorkOrder.ComplateDate,
                             eccpMaintenanceWorkOrder.PlanCheckDate,
                             eccpMaintenanceWorkOrder.MaintenanceStatusId,
                             eccpMaintenanceWorkOrder.IsComplete
                         };

                var query = from eccpBaseElevator in eccpBaseElevators
                            join p in ps
                                on eccpBaseElevator.Id equals p.ElevatorId
                            select p;

                var eccpDictMaintenanceStatusCompleted = _eccpDictMaintenanceStatusRepository.FirstOrDefault(e => e.Name == "已完成");
                var eccpDictMaintenanceStatusOverdue = _eccpDictMaintenanceStatusRepository.FirstOrDefault(e => e.Name == "已超期");

                List<Guid> guiIds = new List<Guid>();
                switch (input.StateId)
                {
                    case 1:
                        guiIds = eccpBaseElevators.Where(e => e.CertificateNum.Contains(input.CertificateNum))
                            .Select(e => e.Id).ToList();
                        break;
                    case 5:
                    case 6:
                    case 7:
                        if (!string.IsNullOrWhiteSpace(input.LiftIds))
                        {
                            List<int> liftIds = input.LiftIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                            guiIds = eccpBaseElevators.WhereIf(input.LiftIds != null, e => e.SyncElevatorId != null && liftIds.Contains((int)e.SyncElevatorId)).Select(e => e.Id).ToList();
                        }
                        break;
                    case 9:
                        guiIds = query.Where(e =>
                            e.MaintenanceStatusId != eccpDictMaintenanceStatusCompleted.Id &&
                            EF.Functions.DateDiffHour(DateTime.Now, e.PlanCheckDate) < e.RemindHour).Select(e => e.ElevatorId).ToList();
                        break;
                    case 10:
                        guiIds = query.Where(e =>
                            e.MaintenanceStatusId == eccpDictMaintenanceStatusOverdue.Id).Select(e => e.ElevatorId).ToList();
                        break;
                    case 11:
                        guiIds = eccpMaintenanceTempWorkOrders
                            .Where(e => eccpBaseElevators.Select(w => w.Id).Contains(e.ElevatorId))
                            .Select(e => e.ElevatorId).ToList();
                        break;
                    default:
                        guiIds = eccpBaseElevators.Select(e => e.Id).ToList();
                        break;
                }

                List<GetCompanyDataDto> list = await GetCompanyDataList(guiIds);
                return list;
            }
        }

        private async Task<List<GetCompanyDataDto>> GetCompanyDataList(List<Guid> elevatorIds)
        {
            var companyDataList = new List<GetCompanyDataDto>();
            if (AbpSession.TenantId == null)
            {
                return companyDataList;
            }

            // 获取租户
            var tenant = await this.TenantManager.GetByIdAsync(AbpSession.TenantId.Value);

            if (tenant.EditionId == null)
            {
                return companyDataList;
            }

            // 通过租户版本ID获取版本信息
            var edition = await this._eccpEditionRepository.FirstOrDefaultAsync(tenant.EditionId.Value);
            if (edition.ECCPEditionsTypeId == null)
            {
                return companyDataList;
            }
            // 根据版本类型ID获取版本类型
            var eccpEditionsType = await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(edition.ECCPEditionsTypeId.Value);

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var eccpMaintenanceContracts = this._eccpMaintenanceContractRepository.GetAll().Where(e => !e.IsStop && e.EndDate > DateTime.Now);
                var eccpMaintenanceContractElevatorLinks = this._eccpMaintenanceContractElevatorLinkRepository.GetAll().Where(e => elevatorIds.Contains(e.ElevatorId.Value));

                var roles = this._roleManager.Roles;
                var userRoles = this._userRoleRepository.GetAll().Where(e => e.UserId == AbpSession.UserId);
                var roleName = from role in roles
                               join userRole in userRoles
                                   on role.Id equals userRole.RoleId
                               select role.Name;
                if (eccpEditionsType.Name == "维保公司")
                {
                    var eccpBaseMaintenanceCompany = await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(e =>
                        e.TenantId == AbpSession.TenantId.Value);
                    eccpMaintenanceContracts =
                        eccpMaintenanceContracts.Where(e => e.MaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);

                    if (roleName.Contains("Admin") || roleName.Contains("MainManage") || roleName.Contains("MainInfoManage"))
                    {
                        using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                        {
                            var maintenanceContractElevator = from eccpMaintenanceContract in eccpMaintenanceContracts
                                                              join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                                                  on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink
                                                                      .MaintenanceContractId
                                                              select new
                                                              {
                                                                  eccpMaintenanceContract.PropertyCompanyId,
                                                                  eccpMaintenanceContract.PropertyCompany.Name
                                                              };

                            var query = maintenanceContractElevator.GroupBy(e => e.PropertyCompanyId).Select(m => new GetCompanyDataDto
                            {
                                Id = m.Key,
                                Name = m.FirstOrDefault().Name,
                                ElevatorNum = m.Count()
                            });
                            companyDataList = query.ToList();
                        }
                    }
                    else if (roleName.Contains("MaintPrincipal") || roleName.Contains("MainUser"))
                    {
                        using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                        {
                            var eccpBaseElevators = this._eccpBaseElevatorsRepository.GetAll().Where(e =>
                                    e.ECCPBaseMaintenanceCompanyId == eccpBaseMaintenanceCompany.Id).Where(e => elevatorIds.Contains(e.Id));
                            var eccpMaintenancePlanMaintenanceUserLinks = this
                                ._eccpMaintenancePlanMaintenanceUserLinkRepository.GetAll()
                                .Where(e => e.UserId == AbpSession.UserId).Select(e => e.MaintenancePlanId);
                            var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().Where(e =>
                                !e.IsCancel && !e.IsClose &&
                                eccpMaintenancePlanMaintenanceUserLinks.Contains(e.Id));

                            var maintenancePlanElevator = from eccpBaseElevator in eccpBaseElevators
                                                          join eccpMaintenancePlan in eccpMaintenancePlans
                                                              on eccpBaseElevator.Id equals eccpMaintenancePlan.ElevatorId
                                                          select new
                                                          {
                                                              eccpBaseElevator.Id,
                                                              eccpBaseElevator.ECCPBasePropertyCompanyId,
                                                              eccpBaseElevator.ECCPBasePropertyCompany.Name
                                                          };

                            var query = maintenancePlanElevator.GroupBy(e => e.ECCPBasePropertyCompanyId).Select(m =>
                                new GetCompanyDataDto
                                {
                                    Id = m.Key.Value,
                                    Name = m.FirstOrDefault().Name,
                                    ElevatorNum = m.Count()
                                });
                            companyDataList = query.ToList();
                        }
                    }
                }
                else if (eccpEditionsType.Name == "物业公司")
                {
                    var eccpBasePropertyCompany = await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(e =>
                        e.TenantId == AbpSession.TenantId.Value);
                    eccpMaintenanceContracts =
                        eccpMaintenanceContracts.Where(e => e.PropertyCompanyId == eccpBasePropertyCompany.Id);

                    using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                    {
                        var maintenanceContractElevator = from eccpMaintenanceContract in eccpMaintenanceContracts
                                                          join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                                              on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink
                                                                  .MaintenanceContractId
                                                          select new
                                                          {
                                                              eccpMaintenanceContract.MaintenanceCompanyId,
                                                              eccpMaintenanceContract.MaintenanceCompany.Name
                                                          };
                        var query = maintenanceContractElevator.GroupBy(e => e.MaintenanceCompanyId).Select(m =>
                            new GetCompanyDataDto
                            {
                                Id = m.Key,
                                Name = m.FirstOrDefault().Name,
                                ElevatorNum = m.Count()
                            });
                        companyDataList = query.ToList();
                    }
                }
                return companyDataList;
            }
        }

        /// <summary>
        /// 使用单位维保统计
        /// </summary>
        /// <returns></returns>
        public async Task<GetPropertyCompaniesMaintenanceStatisticsDto> GetPropertyCompaniesMaintenanceStatistics()
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var eccpBasePropertyCompany = await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(e =>
                    e.TenantId == AbpSession.TenantId.Value);

                var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll();
                var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().Where(e => !e.IsCancel && !e.IsClose);

                var eccpBaseElevators = _eccpBaseElevatorsRepository.GetAll().Where(w => w.ECCPBasePropertyCompanyId == eccpBasePropertyCompany.Id);
                var eccpMaintenanceContracts = _eccpMaintenanceContractRepository.GetAll().Where(e => e.PropertyCompanyId == eccpBasePropertyCompany.Id && !e.IsStop && e.EndDate > DateTime.Now);

                var eccpMaintenanceContractElevatorLinks = this._eccpMaintenanceContractElevatorLinkRepository.GetAll();

                var maintenanceContractElevator = from eccpMaintenanceContract in eccpMaintenanceContracts
                                                  join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                                      on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink
                                                          .MaintenanceContractId
                                                  select eccpMaintenanceContractElevatorLink.ElevatorId;

                var ps = from eccpMaintenancePlan in eccpMaintenancePlans
                         join eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders
                             on eccpMaintenancePlan.Id equals eccpMaintenanceWorkOrder.MaintenancePlanId
                         select new
                         {
                             eccpMaintenancePlan.ElevatorId,
                             eccpMaintenancePlan.RemindHour,
                             eccpMaintenanceWorkOrder.PlanCheckDate,
                             eccpMaintenanceWorkOrder.MaintenanceStatusId
                         };

                var query = from eccpBaseElevator in eccpBaseElevators
                            join p in ps
                            on eccpBaseElevator.Id equals p.ElevatorId
                            select p;

                var eccpDictMaintenanceStatusOverdue = _eccpDictMaintenanceStatusRepository.FirstOrDefault(e => e.Name == "已超期");
                var userId = this._eccpCompanyUserExtensionRepository.GetAll().Where(e => e.UserId == AbpSession.UserId)
                    .Select(e => e.SyncUserId).FirstOrDefault();

                int elevatorFaultNum = 0;
                if (userId != null)
                {
                    //TODO: 接口地址暂时写死
                    string result = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetFaultyElevatorCount?userId=" + userId);
                    if (int.TryParse(result, out var numberOfFault))
                    {
                        elevatorFaultNum = numberOfFault;//故障数量
                    }
                }

                var getPropertyCompaniesMaintenanceStatistics = new GetPropertyCompaniesMaintenanceStatisticsDto
                {
                    ElevatorNum = maintenanceContractElevator.Count(),
                    MaintenanceNum = query.Count(),
                    OverdueMaintenanceNum = query.Count(w => w.MaintenanceStatusId == eccpDictMaintenanceStatusOverdue.Id),
                    ElevatorFaultNum = elevatorFaultNum,
                    NumberOfTemporaryMaintenance = query.Count(w => EF.Functions.DateDiffHour(DateTime.Now, w.PlanCheckDate) < w.RemindHour)
                };

                return getPropertyCompaniesMaintenanceStatistics;
            }
        }

        /// <summary>
        /// 维保公司列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetMaintenanceCompaniesDto>> GetMaintenanceCompaniesList()
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var eccpBasePropertyCompany = await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(e =>
                    e.TenantId == AbpSession.TenantId.Value);
                var eccpMaintenanceContracts =
                     this._eccpMaintenanceContractRepository.GetAll().Where(e => !e.IsStop && e.PropertyCompanyId == eccpBasePropertyCompany.Id);
                var eccpMaintenanceContractElevatorLinks = this._eccpMaintenanceContractElevatorLinkRepository.GetAll();

                using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    var maintenanceContractElevator = from eccpMaintenanceContract in eccpMaintenanceContracts
                                                      join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks
                                                          on eccpMaintenanceContract.Id equals eccpMaintenanceContractElevatorLink
                                                              .MaintenanceContractId
                                                      select new
                                                      {
                                                          eccpMaintenanceContract.MaintenanceCompanyId,
                                                          eccpMaintenanceContract.MaintenanceCompany.Name
                                                      };
                    var maintenanceCompaniesQuery = maintenanceContractElevator.GroupBy(e => e.MaintenanceCompanyId).Select(m =>
                        new GetMaintenanceCompaniesDto
                        {
                            Id = m.Key,
                            Name = m.FirstOrDefault().Name,
                            ElevatorNum = m.Count()
                        });

                    var getMaintenanceCompanies = maintenanceCompaniesQuery.ToList();

                    var eccpDictMaintenanceStatusOverdue = _eccpDictMaintenanceStatusRepository.FirstOrDefault(e => e.Name == "已超期");
                    var eccpDictMaintenanceStatusCompleted = _eccpDictMaintenanceStatusRepository.FirstOrDefault(e => e.Name == "已完成");
                    foreach (var getMaintenance in getMaintenanceCompanies)
                    {
                        var eccpBaseElevators = _eccpBaseElevatorsRepository.GetAll().Where(w => w.ECCPBasePropertyCompanyId == eccpBasePropertyCompany.Id && w.ECCPBaseMaintenanceCompanyId == getMaintenance.Id);

                        var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().Where(e => !e.IsCancel && !e.IsClose);
                        var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll();

                        var ps = from eccpMaintenancePlan in eccpMaintenancePlans
                                 join eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders
                                     on eccpMaintenancePlan.Id equals eccpMaintenanceWorkOrder.MaintenancePlanId
                                 select new
                                 {
                                     eccpMaintenancePlan.ElevatorId,
                                     eccpMaintenancePlan.PollingPeriod,
                                     eccpMaintenancePlan.RemindHour,
                                     eccpMaintenanceWorkOrder.ComplateDate,
                                     eccpMaintenanceWorkOrder.PlanCheckDate,
                                     eccpMaintenanceWorkOrder.MaintenanceStatusId,
                                     eccpMaintenanceWorkOrder.IsComplete
                                 };

                        var query = from eccpBaseElevator in eccpBaseElevators
                                    join p in ps
                                        on eccpBaseElevator.Id equals p.ElevatorId
                                    select p;

                        getMaintenance.NumberOfTemporaryMaintenance = query.Count(w =>
                            EF.Functions.DateDiffHour(DateTime.Now, w.PlanCheckDate) < w.RemindHour);
                        getMaintenance.OverdueNum = query.Count(w =>
                            w.MaintenanceStatusId == eccpDictMaintenanceStatusOverdue.Id);
                        getMaintenance.NumberOfMaintenanceCompleted = query.Count(w =>
                             w.MaintenanceStatusId == eccpDictMaintenanceStatusCompleted.Id);
                    }

                    return getMaintenanceCompanies;
                }
            }
        }

        /// <summary>
        /// 电梯维保信息列表接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetElevatorMaintenanceInfoDto>> GetElevatorMaintenanceInfoList(GetElevatorMaintenanceInfoInput input)
        {
            var getElevatorMaintenanceInfo = new List<GetElevatorMaintenanceInfoDto>();
            if (AbpSession.TenantId == null)
            {
                return new PagedResultDto<GetElevatorMaintenanceInfoDto>(0, getElevatorMaintenanceInfo);
            }

            // 获取租户
            var tenant = await this.TenantManager.GetByIdAsync(AbpSession.TenantId.Value);

            if (tenant.EditionId == null)
            {
                return new PagedResultDto<GetElevatorMaintenanceInfoDto>(0, getElevatorMaintenanceInfo);
            }

            // 通过租户版本ID获取版本信息
            var edition = await this._eccpEditionRepository.FirstOrDefaultAsync(tenant.EditionId.Value);
            if (edition.ECCPEditionsTypeId == null)
            {
                return new PagedResultDto<GetElevatorMaintenanceInfoDto>(0, getElevatorMaintenanceInfo);
            }
            // 根据版本类型ID获取版本类型
            var eccpEditionsType = await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(edition.ECCPEditionsTypeId.Value);
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var eccpMaintenancePlans =
                    this._eccpMaintenancePlanRepository.GetAll().Where(e => !e.IsCancel && !e.IsClose);
                var eccpBaseElevators = _eccpBaseElevatorsRepository.GetAll();
                var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll();

                if (eccpEditionsType.Name == "维保公司")
                {
                    var eccpBaseMaintenanceCompany =
                        await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(e =>
                            e.TenantId == AbpSession.TenantId.Value);

                    eccpBaseElevators = eccpBaseElevators.Where(w =>
                        w.ECCPBaseMaintenanceCompanyId == eccpBaseMaintenanceCompany.Id);

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

                    var roleName = uRoles.Select(e => e.Name);
                    if (roleName.Contains("Admin") || roleName.Contains("MainManage") ||
                        roleName.Contains("MainInfoManage"))
                    {
                        eccpMaintenancePlans = eccpMaintenancePlans.Where(e => e.TenantId == AbpSession.TenantId);
                    }
                    else if (roleName.Contains("MaintPrincipal") || roleName.Contains("MainUser"))
                    {
                        var eccpMaintenancePlanMaintenanceUserLinks = this
                            ._eccpMaintenancePlanMaintenanceUserLinkRepository.GetAll()
                            .Where(e => e.UserId == AbpSession.UserId).Select(e => e.MaintenancePlanId);
                        eccpMaintenancePlans = eccpMaintenancePlans.Where(e =>
                            eccpMaintenancePlanMaintenanceUserLinks.Contains(e.Id));
                    }
                }
                else if (eccpEditionsType.Name == "物业公司")
                {
                    var eccpBasePropertyCompany = await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(e =>
                        e.TenantId == AbpSession.TenantId.Value);

                    eccpBaseElevators =
                        eccpBaseElevators.Where(w => w.ECCPBasePropertyCompanyId == eccpBasePropertyCompany.Id);
                }

                using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    var eccpMaintenanceWorkOrderMaintenanceUserLinks = this
                        ._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository
                        .GetAll();
                    var eccpElevatorQrCodes = this._eccpElevatorQrCodeRepository.GetAll();
                    var users = this.UserManager.Users;

                    var eccpMaintenanceWorkOrderMaintenanceUsers =
                        from eccpMaintenanceWorkOrderMaintenanceUserLink in eccpMaintenanceWorkOrderMaintenanceUserLinks
                        join user in users on eccpMaintenanceWorkOrderMaintenanceUserLink.UserId equals user.Id
                        select new { eccpMaintenanceWorkOrderMaintenanceUserLink.MaintenancePlanId, user.Name };

                    var ps = from eccpMaintenancePlan in eccpMaintenancePlans
                             join eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders
                                 on eccpMaintenancePlan.Id equals eccpMaintenanceWorkOrder.MaintenancePlanId
                             join eccpMaintenanceWorkOrderMaintenanceUser in eccpMaintenanceWorkOrderMaintenanceUsers
                                 on eccpMaintenanceWorkOrder.Id equals eccpMaintenanceWorkOrderMaintenanceUser
                                     .MaintenancePlanId
                             select new
                             {
                                 eccpMaintenancePlan.ElevatorId,
                                 EccpMaintenanceWorkOrder = eccpMaintenanceWorkOrder,
                                 MaintenanceTypeName = eccpMaintenanceWorkOrder.MaintenanceType.Name,
                                 MaintenanceStatusName = eccpMaintenanceWorkOrder.MaintenanceStatus.Name,
                                 EccpMaintenanceUserName = eccpMaintenanceWorkOrderMaintenanceUser.Name,
                                 eccpMaintenancePlan.RemindHour
                             };

                    var workOrderQuery = ps.GroupBy(e => e.EccpMaintenanceWorkOrder.Id).Select(m => new
                    {
                        m.Key,
                        m.FirstOrDefault().ElevatorId,
                        m.FirstOrDefault().EccpMaintenanceWorkOrder.MaintenanceStatusId,
                        m.FirstOrDefault().MaintenanceStatusName,
                        m.FirstOrDefault().EccpMaintenanceWorkOrder.ComplateDate,
                        m.FirstOrDefault().EccpMaintenanceWorkOrder.PlanCheckDate,
                        m.FirstOrDefault().RemindHour,
                        EccpMaintenanceUserName = m.Select(u => u.EccpMaintenanceUserName).Distinct().ToList(),
                        m.FirstOrDefault().EccpMaintenanceWorkOrder.MaintenanceTypeId,
                        m.FirstOrDefault().MaintenanceTypeName
                    });

                    var eccpDictMaintenanceStatuses = _eccpDictMaintenanceStatusRepository.GetAll().Where(e => e.Name == "未进行" || e.Name == "进行中").Select(e => e.Id);

                    var query = (from eccpBaseElevator in eccpBaseElevators
                                 join workOrder in workOrderQuery
                                     on eccpBaseElevator.Id equals workOrder.ElevatorId
                                 join eccpElevatorQrCode in eccpElevatorQrCodes
                                     on eccpBaseElevator.Id equals eccpElevatorQrCode.ElevatorId
                                    into j
                                 from s in j.DefaultIfEmpty()
                                 select new
                                 {
                                     eccpBaseElevator.Id,
                                     WorkOrderId = workOrder.Key,
                                     eccpBaseElevator.CertificateNum,
                                     MaintenanceStatusId =
                                         EF.Functions.DateDiffHour(DateTime.Now, workOrder.PlanCheckDate) < workOrder.RemindHour && eccpDictMaintenanceStatuses.Contains(workOrder.MaintenanceStatusId)
                                             ? 5
                                             : workOrder.MaintenanceStatusId,
                                     MaintenanceStatusName =
                                         EF.Functions.DateDiffHour(DateTime.Now, workOrder.PlanCheckDate) < workOrder.RemindHour && eccpDictMaintenanceStatuses.Contains(workOrder.MaintenanceStatusId)
                                             ? "还有" + EF.Functions.DateDiffHour(DateTime.Now, workOrder.PlanCheckDate) + "小时超期"
                                             : workOrder.MaintenanceStatusName,
                                     MaintenanceCompletionTime = workOrder.ComplateDate,
                                     MaintenanceDueTime = workOrder.PlanCheckDate,
                                     workOrder.MaintenanceTypeId,
                                     workOrder.MaintenanceTypeName,
                                     eccpBaseElevator.InstallationAddress,
                                     eccpBaseElevator.SyncElevatorId,
                                     MaintenanceUserName = string.Join(",", workOrder.EccpMaintenanceUserName),
                                     ElevatorNum = s == null ? string.Empty : s.ElevatorNum
                                 }).WhereIf(input.MaintenanceTypeId != null, e => e.MaintenanceTypeId == input.MaintenanceTypeId).WhereIf(!string.IsNullOrWhiteSpace(input.CertificateNum), e => e.CertificateNum.Contains(input.CertificateNum)).WhereIf(!string.IsNullOrWhiteSpace(input.ElevatorNum), e => e.ElevatorNum == input.ElevatorNum);

                    if (input.MaintenanceStatusId == 1)
                        query = query.Where(e => eccpDictMaintenanceStatuses.Contains(e.MaintenanceStatusId) || e.MaintenanceStatusId == 5);
                    else
                        query = query.WhereIf(input.MaintenanceStatusId != null,
                            e => e.MaintenanceStatusId == input.MaintenanceStatusId);

                    var totalCount = query.Count();

                    query.OrderBy(input.Sorting ?? "MaintenanceDueTime asc").PageBy(input).MapTo(getElevatorMaintenanceInfo);
                    foreach (var elevatorMaintenanceInfo in getElevatorMaintenanceInfo)
                    {
                        if (elevatorMaintenanceInfo.SyncElevatorId != null)
                        {
                            string equipmentInfoJson = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetEquipmentInfo?liftId=" + elevatorMaintenanceInfo.SyncElevatorId);
                            if (!string.IsNullOrWhiteSpace(equipmentInfoJson))
                            {
                                var cameraInformation = JsonConvert.DeserializeObject<GetCameraInformationDto>(equipmentInfoJson);
                                if (cameraInformation != null && !string.IsNullOrWhiteSpace(cameraInformation.EquipmentSerialNumber))
                                {
                                    elevatorMaintenanceInfo.IsCamera = true;
                                }
                            }
                        }
                    }
                    return new PagedResultDto<GetElevatorMaintenanceInfoDto>(totalCount, getElevatorMaintenanceInfo);
                }
            }
        }

        /// <summary>
        /// 接受手机端在线心跳接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateAppOnlineHeartbeat(UpdateAppOnlineHeartbeatDto input)
        {
            if (AbpSession.UserId != null)
            {
                var eccpCompanyUserExtension = await this._eccpCompanyUserExtensionRepository.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
                if (eccpCompanyUserExtension != null)
                {
                    if (input.IsOnline)
                    {
                        var coordinateTransformationJson = HttpGet(
                            "https://restapi.amap.com/v3/assistant/coordinate/convert?locations=" + input.Longitude + "," + input.Latitude + "&coordsys=baidu&key=478d1e02d4559c1b8deeb26d92c71b28");
                        JObject coordinateTransformationJobject = (JObject)JsonConvert.DeserializeObject(coordinateTransformationJson);
                        var coordinateTransformationStatus = coordinateTransformationJobject["status"].ToString();
                        if (coordinateTransformationStatus == "1")
                        {
                            var coordinateTransformationLocations = coordinateTransformationJobject["locations"].ToString();
                            if (!string.IsNullOrWhiteSpace(coordinateTransformationLocations))
                            {
                                var json = HttpGet("https://restapi.amap.com/v3/geocode/regeo?location=" + coordinateTransformationLocations + "&key=478d1e02d4559c1b8deeb26d92c71b28");

                                JObject jobject = (JObject)JsonConvert.DeserializeObject(json);
                                var status = jobject["status"].ToString();
                                if (status == "1")
                                {
                                    var resultJson = jobject["regeocode"]["addressComponent"];
                                    if (resultJson != null)
                                    {
                                        var province = resultJson["province"].ToString();
                                        if (!string.IsNullOrWhiteSpace(province))
                                        {
                                            var areaProvince = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(e => e.Name == province);
                                            input.PositionProvinceId = areaProvince?.Id;
                                        }

                                        var city = resultJson["city"].ToString();
                                        if (!string.IsNullOrWhiteSpace(city))
                                        {
                                            var areaCity = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(e => e.Name == city);
                                            input.PositionCityId = areaCity?.Id;
                                        }

                                        var district = resultJson["district"].ToString();
                                        if (!string.IsNullOrWhiteSpace(district))
                                        {
                                            var areaDistrict = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(e => e.Name == district);
                                            input.PositionDistrictId = areaDistrict?.Id;
                                        }

                                        var street = resultJson["township"].ToString();
                                        if (!string.IsNullOrWhiteSpace(street))
                                        {
                                            var areaStreet = await this._eccpBaseAreaRepository.FirstOrDefaultAsync(e => e.Name == street);
                                            input.PositionStreetId = areaStreet?.Id;
                                        }
                                    }
                                }
                            }
                        }

                        input.Heartbeat = DateTime.Now;
                        var userPathHistory = this.ObjectMapper.Map<UserPathHistory>(input);
                        userPathHistory.UserId = AbpSession.UserId.Value;
                        await this._userPathHistoryRepository.InsertAndGetIdAsync(userPathHistory);
                    }
                    this.ObjectMapper.Map(input, eccpCompanyUserExtension);
                }
            }
        }

        /// <summary>
        /// 电梯详细信息页面
        /// </summary>
        /// <param name="elevatorId"></param>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        public async Task<GetElevatorDetailedInfoDto> GetElevatorDetailedInfoById(Guid elevatorId, int? workOrderId)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var eccpBaseElevators = this._eccpBaseElevatorsRepository.GetAll();
                var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll();
                var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll().OrderByDescending(e => e.Id);
                var eccpDictMaintenanceStatuses = _eccpDictMaintenanceStatusRepository.GetAll().Where(e => e.Name == "未进行" || e.Name == "进行中").Select(e => e.Id);

                var query = await (from eccpBaseElevator in eccpBaseElevators
                                   join eccpMaintenancePlan in eccpMaintenancePlans
                                       on eccpBaseElevator.Id equals eccpMaintenancePlan.ElevatorId
                                   join eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders
                                       on eccpMaintenancePlan.Id equals eccpMaintenanceWorkOrder.MaintenancePlanId
                                   select new GetElevatorDetailedInfoDto
                                   {
                                       ElevatorId = eccpBaseElevator.Id,
                                       SyncElevatorId = eccpBaseElevator.SyncElevatorId,
                                       WorkOrderId = eccpMaintenanceWorkOrder.Id,
                                       PlanId = eccpMaintenanceWorkOrder.MaintenancePlanId,
                                       MaintenanceStatusId = EF.Functions.DateDiffHour(DateTime.Now, eccpMaintenanceWorkOrder.PlanCheckDate) < eccpMaintenancePlan.RemindHour && eccpDictMaintenanceStatuses.Contains(eccpMaintenanceWorkOrder.MaintenanceStatusId) ? 5 : eccpMaintenanceWorkOrder.MaintenanceStatusId,
                                       MaintenanceStatusName = EF.Functions.DateDiffHour(DateTime.Now, eccpMaintenanceWorkOrder.PlanCheckDate) < eccpMaintenancePlan.RemindHour && eccpDictMaintenanceStatuses.Contains(eccpMaintenanceWorkOrder.MaintenanceStatusId) ? EF.Functions.DateDiffHour(DateTime.Now, eccpMaintenanceWorkOrder.PlanCheckDate) + "小时超期" : eccpMaintenanceWorkOrder.MaintenanceStatus.Name,
                                       PlanCheckDate = eccpMaintenanceWorkOrder.PlanCheckDate,
                                       ComplateDate = eccpMaintenanceWorkOrder.ComplateDate,
                                       MaintenanceTypeName = eccpMaintenanceWorkOrder.MaintenanceType.Name,
                                       InstallationAddress = eccpBaseElevator.InstallationAddress
                                   }).Where(e => e.ElevatorId == elevatorId).WhereIf(workOrderId != null, e => e.WorkOrderId == workOrderId).FirstOrDefaultAsync();

                string equipmentUrl = "本电梯没有安装四维监控设备，请联系使用单位进行安装。";
                if (query != null)
                {
                    if (query.SyncElevatorId != null)
                    {
                        equipmentUrl = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetEquipmentByLiftId?liftId=" + query.SyncElevatorId);
                        if (string.IsNullOrWhiteSpace(equipmentUrl))
                        {
                            equipmentUrl = "本电梯没有安装四维监控设备，请联系使用单位进行安装。";
                        }

                        string json = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetEquipmentFaultListByLiftId?liftId=" + query.SyncElevatorId);
                        if (!string.IsNullOrWhiteSpace(json))
                        {
                            query.EquipmentFaultInfos = JsonConvert.DeserializeObject<List<GetEquipmentFaultInfosDto>>(json);
                        }

                        string adviceJson = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetAdviceElevatorByLiftId?liftId=" + query.SyncElevatorId);
                        if (!string.IsNullOrWhiteSpace(adviceJson))
                        {
                            query.AdviceInfos = JsonConvert.DeserializeObject<List<GetAdviceInfoDto>>(adviceJson);
                        }

                        string equipmentInfoJson = HttpGet("http://www.dianti119.com/API/ElevatorCloud/GetEquipmentInfo?liftId=" + query.SyncElevatorId);
                        if (!string.IsNullOrWhiteSpace(equipmentInfoJson))
                        {
                            query.CameraInformation = JsonConvert.DeserializeObject<GetCameraInformationDto>(equipmentInfoJson);
                        }
                    }
                    var users = this.UserManager.Users;
                    var eccpDictNodeType = await this._eccpDictNodeTypeRepository.FirstOrDefaultAsync(e => e.Name == "提醒节点");

                    if (query.MaintenanceStatusName == "已完成")
                    {
                        var eccpMaintenanceWorkOrder = await this._eccpMaintenanceWorkOrderRepository.GetAll().OrderByDescending(e => e.Id).FirstOrDefaultAsync(e => e.MaintenancePlanId == query.PlanId && e.IsComplete && e.ComplateDate < query.ComplateDate);
                        var eccpMaintenanceWork = await this._eccpEccpMaintenanceWorkRepository.FirstOrDefaultAsync(e =>
                            e.MaintenanceWorkOrderId == query.WorkOrderId);
                        var eccpMaintenanceWorkFlows = this._eccpMaintenanceWorkFlowRepository.GetAll()
                            .Where(e => e.MaintenanceWorkId == eccpMaintenanceWork.Id);

                        var eccpCompanyUserExtensions = this._eccpCompanyUserExtensionRepository.GetAll();
                        var ps = from user in users
                                 join eccpCompanyUserExtension in eccpCompanyUserExtensions
                                     on user.Id equals eccpCompanyUserExtension.UserId into g
                                 from f in g.DefaultIfEmpty()
                                 select new
                                 {
                                     user.Id,
                                     user.Name,
                                     f.Mobile
                                 };
                        var eccpMaintenanceWorkFlowMaintenanceUsers =
                            (from eccpMaintenanceWorkFlow in eccpMaintenanceWorkFlows
                             join p in ps on eccpMaintenanceWorkFlow.CreatorUserId equals p.Id
                             select new
                             {
                                 p.Id,
                                 p.Name,
                                 p.Mobile
                             }).GroupBy(e => e.Mobile).Select(
                                w => new GetMaintenanceUserInfosDto
                                {
                                    UserId = w.FirstOrDefault().Id,
                                    Mobile = w.Key,
                                    UserName = w.FirstOrDefault().Name
                                });

                        if (eccpMaintenanceWorkOrder != null)
                        {
                            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                            {
                                //上次工作
                                var eccpMaintenanceWorkLast =
                                    await this._eccpEccpMaintenanceWorkRepository.FirstOrDefaultAsync(e =>
                                        e.MaintenanceWorkOrderId == eccpMaintenanceWorkOrder.Id);
                                var eccpMaintenanceWorkFlowsLast = this._eccpMaintenanceWorkFlowRepository.GetAll()
                                    .Where(e => e.MaintenanceWorkId == eccpMaintenanceWorkLast.Id &&
                                                e.ActionCodeValue != null && e.ActionCodeValue != "");
                                var eccpMaintenanceTemplateNodes = this._eccpMaintenanceTemplateNodeRepository.GetAll()
                                    .Where(e => e.DictNodeTypeId == eccpDictNodeType.Id);
                                var lastRemarks = from eccpMaintenanceTemplateNode in eccpMaintenanceTemplateNodes
                                                  join eccpMaintenanceWorkFlowLast in eccpMaintenanceWorkFlowsLast
                                                      on eccpMaintenanceTemplateNode.Id equals eccpMaintenanceWorkFlowLast
                                                          .MaintenanceTemplateNodeId
                                                  select eccpMaintenanceWorkFlowLast.ActionCodeValue;

                                query.Remarks = string.Join(";", lastRemarks);
                            }

                            query.LastMaintenanceTime = eccpMaintenanceWorkOrder.ComplateDate;
                        }
                        query.MaintenanceUserInfos = eccpMaintenanceWorkFlowMaintenanceUsers.ToList();
                    }
                    else
                    {
                        var eccpMaintenanceWorkOrder = await this._eccpMaintenanceWorkOrderRepository.GetAll().OrderByDescending(e => e.Id).FirstOrDefaultAsync(e => e.MaintenancePlanId == query.PlanId && e.IsComplete);
                        var eccpMaintenanceWorkOrderMaintenanceUserLinks =
                            this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository.GetAll().Where(e => e.MaintenancePlanId == query.WorkOrderId);

                        var eccpCompanyUserExtensions = this._eccpCompanyUserExtensionRepository.GetAll();
                        var ps = from user in users
                                 join eccpCompanyUserExtension in eccpCompanyUserExtensions
                                     on user.Id equals eccpCompanyUserExtension.UserId into g
                                 from f in g.DefaultIfEmpty()
                                 select new
                                 {
                                     user.Id,
                                     user.Name,
                                     f.Mobile
                                 };
                        var eccpMaintenanceWorkOrderMaintenanceUsers =
                            from eccpMaintenanceWorkOrderMaintenanceUserLink in eccpMaintenanceWorkOrderMaintenanceUserLinks
                            join p in ps on eccpMaintenanceWorkOrderMaintenanceUserLink.UserId equals p.Id
                            select new GetMaintenanceUserInfosDto
                            {
                                UserId = p.Id,
                                UserName = p.Name,
                                Mobile = p.Mobile
                            };

                        if (eccpMaintenanceWorkOrder != null)
                        {
                            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                            {
                                //上次工作
                                var eccpMaintenanceWorkLast =
                                    await this._eccpEccpMaintenanceWorkRepository.FirstOrDefaultAsync(e =>
                                        e.MaintenanceWorkOrderId == eccpMaintenanceWorkOrder.Id);
                                var eccpMaintenanceWorkFlowsLast = this._eccpMaintenanceWorkFlowRepository.GetAll()
                                    .Where(e => e.MaintenanceWorkId == eccpMaintenanceWorkLast.Id &&
                                                e.ActionCodeValue != null && e.ActionCodeValue != "");
                                var eccpMaintenanceTemplateNodes = this._eccpMaintenanceTemplateNodeRepository.GetAll()
                                    .Where(e => e.DictNodeTypeId == eccpDictNodeType.Id);
                                var lastRemarks = from eccpMaintenanceTemplateNode in eccpMaintenanceTemplateNodes
                                                  join eccpMaintenanceWorkFlowLast in eccpMaintenanceWorkFlowsLast
                                                      on eccpMaintenanceTemplateNode.Id equals eccpMaintenanceWorkFlowLast
                                                          .MaintenanceTemplateNodeId
                                                  select eccpMaintenanceWorkFlowLast.ActionCodeValue;

                                query.Remarks = string.Join(";", lastRemarks);
                            }

                            query.LastMaintenanceTime = eccpMaintenanceWorkOrder.ComplateDate;
                        }
                        query.MaintenanceUserInfos = eccpMaintenanceWorkOrderMaintenanceUsers.ToList();
                    }
                    query.EquipmentUrl = equipmentUrl;
                }
                return query;
            }

        }

        /// <summary>
        /// 维保历史
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetElevatorMaintenanceHistorysDto>> GetElevatorMaintenanceHistoryList(GetElevatorMaintenanceHistorysInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var eccpBaseElevators = this._eccpBaseElevatorsRepository.GetAll().Where(e => e.Id == input.ElevatorId);
                var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll();
                var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll();

                var eccpMaintenanceWorkOrderMaintenanceUserLinks = this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository
                    .GetAll();
                var users = this.UserManager.Users;

                var eccpMaintenanceWorkOrderMaintenanceUsers =
                    from eccpMaintenanceWorkOrderMaintenanceUserLink in eccpMaintenanceWorkOrderMaintenanceUserLinks
                    join user in users on eccpMaintenanceWorkOrderMaintenanceUserLink.UserId equals user.Id
                    select new { eccpMaintenanceWorkOrderMaintenanceUserLink.MaintenancePlanId, user.Name };

                var query = from eccpBaseElevator in eccpBaseElevators
                            join eccpMaintenancePlan in eccpMaintenancePlans
                                on eccpBaseElevator.Id equals eccpMaintenancePlan.ElevatorId
                            join eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders
                                on eccpMaintenancePlan.Id equals eccpMaintenanceWorkOrder.MaintenancePlanId
                            join eccpMaintenanceWorkOrderMaintenanceUser in eccpMaintenanceWorkOrderMaintenanceUsers
                                on eccpMaintenanceWorkOrder.Id equals eccpMaintenanceWorkOrderMaintenanceUser.MaintenancePlanId
                            select new
                            {
                                WorkOrderId = eccpMaintenanceWorkOrder.Id,
                                eccpMaintenanceWorkOrder.ComplateDate,
                                eccpMaintenanceWorkOrder.PlanCheckDate,
                                MaintenanceTypeName = eccpMaintenanceWorkOrder.MaintenanceType.Name,
                                MaintenanceStatusName = eccpMaintenanceWorkOrder.MaintenanceStatus.Name,
                                eccpBaseElevator.InstallationAddress,
                                eccpMaintenanceWorkOrder.Remark,
                                EccpMaintenanceUserName = eccpMaintenanceWorkOrderMaintenanceUser.Name
                            };

                var workOrderQuery = query.GroupBy(e => e.WorkOrderId).Select(m => new
                {
                    WorkOrderId = m.Key,
                    m.FirstOrDefault().ComplateDate,
                    m.FirstOrDefault().PlanCheckDate,
                    m.FirstOrDefault().MaintenanceTypeName,
                    m.FirstOrDefault().MaintenanceStatusName,
                    m.FirstOrDefault().InstallationAddress,
                    m.FirstOrDefault().Remark,
                    MaintenanceUserName = string.Join(",", m.Select(u => u.EccpMaintenanceUserName).Distinct().ToList())
                });
                var totalCount = workOrderQuery.Count();

                var elevatorMaintenanceHistorys = new List<GetElevatorMaintenanceHistorysDto>();
                workOrderQuery.OrderBy(input.Sorting ?? "workOrderId asc").PageBy(input).MapTo(elevatorMaintenanceHistorys);
                foreach (var elevatorMaintenanceHistory in elevatorMaintenanceHistorys)
                {
                    if (elevatorMaintenanceHistory.MaintenanceStatusName == "已完成")
                    {
                        var eccpMaintenanceWork = await this._eccpEccpMaintenanceWorkRepository.FirstOrDefaultAsync(e =>
                            e.MaintenanceWorkOrderId == elevatorMaintenanceHistory.WorkOrderId);

                        var eccpMaintenanceWorkFlows = this._eccpMaintenanceWorkFlowRepository.GetAll()
                            .Where(e => e.MaintenanceWorkId == eccpMaintenanceWork.Id);

                        var eccpMaintenanceWorkFlowMaintenanceUsers =
                            (from eccpMaintenanceWorkFlow in eccpMaintenanceWorkFlows
                             join user in users on eccpMaintenanceWorkFlow.CreatorUserId equals user.Id
                             select new
                             {
                                 user.Name,
                                 user.Id
                             }).GroupBy(e => e.Id).Select(w => w.FirstOrDefault().Name);
                        elevatorMaintenanceHistory.MaintenanceUserName =
                            string.Join(",", eccpMaintenanceWorkFlowMaintenanceUsers);
                    }
                }
                return new PagedResultDto<GetElevatorMaintenanceHistorysDto>(totalCount, elevatorMaintenanceHistorys);
            }
        }

        /// <summary>
        /// 电梯档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetElevatorArchivesInfoDto> GetEccpBaseElevatorInfo(EntityDto<Guid> input)
        {
            var eccpBaseElevator = await this._eccpBaseElevatorsRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetElevatorArchivesInfoDto
            {
                EccpBaseElevator = this.ObjectMapper.Map<GetEccpBaseElevatorDto>(eccpBaseElevator)
            };

            if (output.EccpBaseElevator.EccpDictPlaceTypeId != null)
            {
                var eccpDictPlaceType =
                    await this._eccpDictPlaceTypeRepository.FirstOrDefaultAsync(
                        (int)output.EccpBaseElevator.EccpDictPlaceTypeId);
                output.EccpDictPlaceTypeName = eccpDictPlaceType.Name;
            }

            if (output.EccpBaseElevator.EccpDictElevatorTypeId != null)
            {
                var eccpDictElevatorType =
                    await this._eccpDictElevatorTypeRepository.FirstOrDefaultAsync(
                        (int)output.EccpBaseElevator.EccpDictElevatorTypeId);
                output.EccpDictElevatorTypeName = eccpDictElevatorType.Name;
            }

            if (output.EccpBaseElevator.ECCPDictElevatorStatusId != null)
            {
                var eccpDictElevatorStatus =
                    await this._eccpDictElevatorStatusRepository.FirstOrDefaultAsync(
                        (int)output.EccpBaseElevator.ECCPDictElevatorStatusId);
                output.ECCPDictElevatorStatusName = eccpDictElevatorStatus.Name;
            }

            if (output.EccpBaseElevator.ECCPBaseCommunityId != null)
            {
                var eccpBaseCommunity =
                    await this._eccpBaseCommunityRepository.FirstOrDefaultAsync(
                        (long)output.EccpBaseElevator.ECCPBaseCommunityId);
                output.ECCPBaseCommunityName = eccpBaseCommunity.Name;
            }

            if (output.EccpBaseElevator.ECCPBaseMaintenanceCompanyId != null)
            {
                using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    var eccpBaseMaintenanceCompany =
                        await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(
                            (int)output.EccpBaseElevator.ECCPBaseMaintenanceCompanyId);
                    output.ECCPBaseMaintenanceCompanyName = eccpBaseMaintenanceCompany.Name;
                }
            }

            if (output.EccpBaseElevator.ECCPBasePropertyCompanyId != null)
            {
                using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    var eccpBasePropertyCompany =
                        await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(
                            (int)output.EccpBaseElevator.ECCPBasePropertyCompanyId);
                    output.ECCPBasePropertyCompanyName = eccpBasePropertyCompany.Name;
                }
            }

            if (output.EccpBaseElevator.ECCPBaseAnnualInspectionUnitId != null)
            {
                var eccpBaseAnnualInspectionUnit =
                    await this._eccpBaseAnnualInspectionUnitRepository.FirstOrDefaultAsync(
                        (long)output.EccpBaseElevator.ECCPBaseAnnualInspectionUnitId);
                output.ECCPBaseAnnualInspectionUnitName = eccpBaseAnnualInspectionUnit.Name;
            }

            if (output.EccpBaseElevator.ECCPBaseRegisterCompanyId != null)
            {
                var eccpBaseRegisterCompany =
                    await this._eccpBaseRegisterCompanyRepository.FirstOrDefaultAsync(
                        (long)output.EccpBaseElevator.ECCPBaseRegisterCompanyId);
                output.ECCPBaseRegisterCompanyName = eccpBaseRegisterCompany.Name;
            }

            if (output.EccpBaseElevator.ECCPBaseProductionCompanyId != null)
            {
                var eccpBaseProductionCompany =
                    await this._eccpBaseProductionCompanyRepository.FirstOrDefaultAsync(
                        (long)output.EccpBaseElevator.ECCPBaseProductionCompanyId);
                output.ECCPBaseProductionCompanyName = eccpBaseProductionCompany.Name;
            }

            if (output.EccpBaseElevator.EccpBaseElevatorBrandId != null)
            {
                var eccpBaseElevatorBrand =
                    await this._eccpBaseElevatorBrandRepository.FirstOrDefaultAsync(
                        (int)output.EccpBaseElevator.EccpBaseElevatorBrandId);
                output.EccpBaseElevatorBrandName = eccpBaseElevatorBrand.Name;
            }

            if (output.EccpBaseElevator.EccpBaseElevatorModelId != null)
            {
                var eccpBaseElevatorModel =
                    await this._eccpBaseElevatorModelRepository.FirstOrDefaultAsync(
                        (int)output.EccpBaseElevator.EccpBaseElevatorModelId);
                output.EccpBaseElevatorModelName = eccpBaseElevatorModel.Name;
            }

            if (output.EccpBaseElevator.ProvinceId != null)
            {
                var province =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.EccpBaseElevator.ProvinceId);
                output.ProvinceName = province.Name;
            }

            if (output.EccpBaseElevator.CityId != null)
            {
                var city = await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.EccpBaseElevator.CityId);
                output.CityName = city.Name;
            }

            if (output.EccpBaseElevator.DistrictId != null)
            {
                var district =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.EccpBaseElevator.DistrictId);
                output.DistrictName = district.Name;
            }

            if (output.EccpBaseElevator.StreetId != null)
            {
                var street =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.EccpBaseElevator.StreetId);
                output.StreetName = street.Name;
            }

            var eccpBaseElevatorSubsidiaryInfo =
                await this._eccpBaseElevatorSubsidiaryInfoRepository.FirstOrDefaultAsync(
                    m => m.ElevatorId == (Guid)output.EccpBaseElevator.Id);
            if (eccpBaseElevatorSubsidiaryInfo != null)
            {
                output.EccpBaseElevatorSubsidiaryInfo =
                    this.ObjectMapper.Map<GetEccpBaseElevatorSubsidiaryInfoDto>(
                        eccpBaseElevatorSubsidiaryInfo);
            }
            else
            {
                output.EccpBaseElevatorSubsidiaryInfo = new GetEccpBaseElevatorSubsidiaryInfoDto();
            }

            return output;
        }
        private string HttpGet(string url)
        {
            try
            {
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/json";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }
    }
}
