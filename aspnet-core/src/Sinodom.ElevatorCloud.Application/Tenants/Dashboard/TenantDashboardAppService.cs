using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
using Sinodom.ElevatorCloud.EccpMaintenancePlans;
using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
using Sinodom.ElevatorCloud.Tenants.Dashboard.Dto;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos;
using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;
using Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos;

using Abp.AutoMapper;
using Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos;
using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos;
using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
using System.Net.Http;
using System.Net.Http.Headers;
using Sinodom.ElevatorCloud.StatisticalElevator;
using Sinodom.ElevatorCloud.Statistics;
using Sinodom.ElevatorCloud.Statistics.Dtos;

namespace Sinodom.ElevatorCloud.Tenants.Dashboard
{
    [DisableAuditing]
    [AbpAuthorize(AppPermissions.Pages_Tenant_Dashboard)]
    public class TenantDashboardAppService : ElevatorCloudAppServiceBase, ITenantDashboardAppService
    {

        private readonly IRepository<EccpMaintenanceContract, long> _eccpMaintenanceContractRepository;

        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;

        private readonly IRepository<User, long> _userRepository;

        private readonly IRepository<EccpMaintenanceWorkOrder, int> _eccpMaintenanceWorkOrderRepository;

        private readonly IRepository<EccpMaintenancePlan, int> _eccpMaintenancePlanRepository;

        private readonly IRepository<ECCPBaseMaintenanceCompany, int> _eCCPBaseMaintenanceCompanyRepository;

        private readonly IRepository<EccpMaintenanceTempWorkOrder, Guid> _eccpMaintenanceTempWorkOrderRepository;

        private readonly IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> _eccpMaintenancePlan_MaintenanceUser_LinkRepository;


        private readonly IRepository<ECCPBasePropertyCompany, int> _eccpBasePropertyCompanyRepository;

        private readonly IStatisticsAppService _statisticsAppService;
        public TenantDashboardAppService(IRepository<EccpMaintenanceContract, long> eccpMaintenanceContractRepository, IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository, IRepository<User, long> userRepository, IRepository<EccpMaintenanceWorkOrder, int> eccpMaintenanceWorkOrderRepository, IRepository<EccpMaintenancePlan, int> eccpMaintenancePlanRepository, IRepository<ECCPBaseMaintenanceCompany, int> eCCPBaseMaintenanceCompanyRepository, IRepository<EccpMaintenanceTempWorkOrder, Guid> eccpMaintenanceTempWorkOrderRepository, IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> eccpMaintenancePlan_MaintenanceUser_LinkRepository, IRepository<ECCPBasePropertyCompany, int> eccpBasePropertyCompanyRepository, IStatisticsAppService statisticsAppService)
        {
            _eccpMaintenanceContractRepository = eccpMaintenanceContractRepository;
            _eccpBaseElevatorRepository = eccpBaseElevatorRepository;
            _userRepository = userRepository;
            _eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
            _eccpMaintenancePlanRepository = eccpMaintenancePlanRepository;
            _eCCPBaseMaintenanceCompanyRepository = eCCPBaseMaintenanceCompanyRepository;
            _eccpMaintenanceTempWorkOrderRepository = eccpMaintenanceTempWorkOrderRepository;
            _eccpMaintenancePlan_MaintenanceUser_LinkRepository = eccpMaintenancePlan_MaintenanceUser_LinkRepository;
            _eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
            _statisticsAppService = statisticsAppService;
        }

        public GetMemberActivityOutput GetMemberActivity()
        {
            return new GetMemberActivityOutput
            (
                DashboardRandomDataGenerator.GenerateMemberActivities()
            );
        }

        public async Task<GetDashboardDataOutput> GetDashboardData()
        {

            var maintenanceCompany = _eCCPBaseMaintenanceCompanyRepository.FirstOrDefault(e => e.TenantId == AbpSession.TenantId);

            var MaintenanceContractCount = await _eccpMaintenanceContractRepository.CountAsync();
            var ElevatorCount = await _eccpBaseElevatorRepository.CountAsync(e => e.ECCPBaseMaintenanceCompanyId == maintenanceCompany.Id);
            var UserCount = await _userRepository.CountAsync();
            var AfterMaintenanceWorkOrderCount = await _eccpMaintenanceWorkOrderRepository.CountAsync(e => e.PlanCheckDate < DateTime.Now.Date.AddDays(7) && e.PlanCheckDate > DateTime.Now.Date && e.IsComplete == false);
            var BeforeMaintenanceWorkOrderCount = await _eccpMaintenanceWorkOrderRepository.CountAsync(e => e.ComplateDate > DateTime.Now.Date.AddDays(-7) && e.ComplateDate < DateTime.Now.Date && e.IsComplete == true);
            var CompleteMaintenanceElevatorCount = GetCompleteMaintenanceElevatorCount();
            var UnfinishedMaintenanceElevatorCount = GetUnfinishedMaintenanceElevatorCount();
            var TempWorkOrderList = GetEccpMaintenanceTempWorkOrder(maintenanceCompany.Id);
            var MaintenancePlanList = GetMaintenancePlanList();
            var AfterMaintenancElevatorList = GetAfterMaintenancElevatorList();
            var TodayMaintenancElevatorList = GetTodayMaintenancElevatorList();
            var MaintenanceContractList = GetMaintenanceContractList();

            List <LiftFunction>  liftList = await PostRequest("https://www.dianti119.com/API/Equipments/GetNewAbpList?userID=" + this.AbpSession.UserId);

            var output = new GetDashboardDataOutput
            {
                MaintenanceContractCount = MaintenanceContractCount,
                ElevatorCount = ElevatorCount,
                UserCount = UserCount,
                AfterMaintenanceWorkOrderCount = AfterMaintenanceWorkOrderCount,
                BeforeMaintenanceWorkOrderCount = BeforeMaintenanceWorkOrderCount,
                CompleteMaintenanceElevatorCount = CompleteMaintenanceElevatorCount,
                UnfinishedMaintenanceElevatorCount = UnfinishedMaintenanceElevatorCount,
                TempWorkOrderList = TempWorkOrderList,
                MaintenancePlanList = MaintenancePlanList,
                AfterMaintenancElevatorList = AfterMaintenancElevatorList,
                TodayMaintenancElevatorList = TodayMaintenancElevatorList,
                MaintenanceContractList = MaintenanceContractList,
                LiftFunctionList  = liftList
            };
            

            return output;
        }

        public async Task<GetPropertyCompaniesDashboardDataOutput> GetPropertyCompaniesDashboardData()
        {
            GetPropertyCompaniesMaintenanceStatisticsDto maintenanceStatistics = await _statisticsAppService.GetPropertyCompaniesMaintenanceStatistics();
            List<GetMaintenanceCompaniesDto> maintenanceCompanies = await _statisticsAppService.GetMaintenanceCompaniesList();

            var output = new GetPropertyCompaniesDashboardDataOutput
            {
                 PropertyCompaniesMaintenanceStatistics = maintenanceStatistics,
                 MaintenanceCompaniesList = maintenanceCompanies
            };
            return output;
         }
            /// <summary>
            /// 本月已完成维保电梯数量
            /// </summary>
            /// <returns></returns>
            public int GetCompleteMaintenanceElevatorCount()
        {
            var completeMaintenanceElevator = _eccpMaintenanceWorkOrderRepository.GetAll().Count(e => e.ComplateDate.Value.Year == DateTime.Now.Year && e.ComplateDate.Value.Month == DateTime.Now.Month && e.IsComplete == true);

            //var query = from mel in completeMaintenanceElevator
            //            join plan in _eccpMaintenancePlanRepository.GetAll() on mel.MaintenancePlanId equals plan.Id
            //            join elevator in _eccpBaseElevatorRepository.GetAll() on plan.ElevatorId equals elevator.Id
            //            select new
            //            {
            //                elevatorId = elevator.Id
            //            };

            return completeMaintenanceElevator;
        }
        /// <summary>
        /// 本月未完成维保电梯数量
        /// </summary>
        /// <returns></returns>
        public int GetUnfinishedMaintenanceElevatorCount()
        {
            var completeMaintenanceElevator = _eccpMaintenanceWorkOrderRepository.GetAll().Where(e => e.PlanCheckDate.Year == DateTime.Now.Year && e.PlanCheckDate.Month == DateTime.Now.Month && e.IsComplete == false);

            var query = from mel in completeMaintenanceElevator
                        join plan in _eccpMaintenancePlanRepository.GetAll() on mel.MaintenancePlanId equals plan.Id
                        join elevator in _eccpBaseElevatorRepository.GetAll() on plan.ElevatorId equals elevator.Id
                        select new
                        {
                            elevatorId = elevator.Id
                        };

            return query.Distinct().Count();
        }
        /// <summary>
        /// 获取临时工单列表
        /// </summary>
        /// <returns></returns>
        public List<GetEccpMaintenanceTempWorkOrderForAppView> GetEccpMaintenanceTempWorkOrder(int maintenanceCompanyId)
        {
            var filteredEccpMaintenanceTempWorkOrders =
                    this._eccpMaintenanceTempWorkOrderRepository.GetAll().OrderByDescending(e => e.CreationTime).Take(5);
            var query = from o in filteredEccpMaintenanceTempWorkOrders
                        join o1 in this._eCCPBaseMaintenanceCompanyRepository.GetAll() on o.MaintenanceCompanyId
                            equals o1.Id into j1
                        from s1 in j1.DefaultIfEmpty()
                        join o2 in this._userRepository.GetAll() on o.UserId equals o2.Id into j2
                        from s2 in j2.DefaultIfEmpty()
                        where o.MaintenanceCompanyId == maintenanceCompanyId && o.CheckState == 0
                        select new GetEccpMaintenanceTempWorkOrderForAppView
                        {
                            Id = o.Id.ToString(),
                            Title = o.Title,
                            EccpDictTempWorkOrderTypesName = s1.Name,
                            CreationTime = o.CreationTime,
                            MaintenanceUserName = s2.Name
                        };
            return query.ToList();
        }
        /// <summary>
        /// 获取维保计划
        /// </summary>
        /// <returns></returns>
        public List<GetStatisticalEccpMaintenancePlanForView> GetMaintenancePlanList()
        {
            var filteredEccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().OrderByDescending(e => e.CreationTime).Take(5);

            var query = from o in filteredEccpMaintenancePlans
                        group o by o.PlanGroupGuid
                             into t
                        select new GetStatisticalEccpMaintenancePlanForView
                        {
                            EccpMaintenancePlan = this.ObjectMapper.Map<EccpMaintenancePlanDto>(t.FirstOrDefault()),
                            EccpBaseElevatorNum = t.Count(),
                            MaintenanceUserNameList = from m in _eccpMaintenancePlan_MaintenanceUser_LinkRepository.GetAll()
                                                      join u in this._userRepository.GetAll() on m.UserId equals u.Id
                                                      where m.MaintenancePlanId == t.FirstOrDefault().Id
                                                      select new MaintenanceUser
                                                      {
                                                          MaintenanceUserName = u.Name
                                                      }
                        };
            //var eccpMaintenancePlansList = new List<GetEccpMaintenancePlanForView>();

            //query.MapTo(eccpMaintenancePlansList);

            return query.ToList();
        }
        /// <summary>
        /// 未来7天待维保电梯
        /// </summary>
        /// <returns></returns>
        public List<GetStatisticalEccpBaseElevatorForView> GetAfterMaintenancElevatorList()
        {
            var completeMaintenanceElevator = _eccpMaintenanceWorkOrderRepository.GetAll().Where(e => e.PlanCheckDate < DateTime.Now.Date.AddDays(7) && e.PlanCheckDate > DateTime.Now.Date && e.IsComplete == false);

            var query = from mel in completeMaintenanceElevator
                        join plan in _eccpMaintenancePlanRepository.GetAll() on mel.MaintenancePlanId equals plan.Id
                        join elevator in _eccpBaseElevatorRepository.GetAll() on plan.ElevatorId equals elevator.Id
                        select new GetStatisticalEccpBaseElevatorForView
                        {
                            Name = elevator.Name,
                            CertificateNum = elevator.CertificateNum,
                            PlanCheckDate = mel.PlanCheckDate.ToString("yyyy-MM-dd"),
                            MaintenanceUserNameList = from m in _eccpMaintenancePlan_MaintenanceUser_LinkRepository.GetAll()
                                                      join u in this._userRepository.GetAll() on m.UserId equals u.Id
                                                      where m.MaintenancePlanId == mel.MaintenancePlanId
                                                      select new MaintenanceUser
                                                      {
                                                          MaintenanceUserName = u.Name
                                                      }
                        };
            return query.ToList();
        }

        /// <summary>
        /// 今日维保电梯
        /// </summary>
        /// <returns></returns>
        public List<GetStatisticalEccpBaseElevatorForView> GetTodayMaintenancElevatorList()
        {
            var completeMaintenanceElevator = _eccpMaintenanceWorkOrderRepository.GetAll().Where(e => e.ComplateDate.Value.Date == DateTime.Now.Date && e.IsComplete == true);

            var query = from mel in completeMaintenanceElevator
                        join plan in _eccpMaintenancePlanRepository.GetAll() on mel.MaintenancePlanId equals plan.Id
                        join elevator in _eccpBaseElevatorRepository.GetAll() on plan.ElevatorId equals elevator.Id
                        select new GetStatisticalEccpBaseElevatorForView
                        {
                            Name = elevator.Name,
                            CertificateNum = elevator.CertificateNum,
                            ComplateDate = mel.ComplateDate.ToString(),
                            MaintenanceUserNameList = from m in _eccpMaintenancePlan_MaintenanceUser_LinkRepository.GetAll()
                                                      join u in this._userRepository.GetAll() on m.UserId equals u.Id
                                                      where m.MaintenancePlanId == mel.MaintenancePlanId
                                                      select new MaintenanceUser
                                                      {
                                                          MaintenanceUserName = u.Name
                                                      }
                        };
            return query.ToList();
        }

        /// <summary>
        /// 临期合同（2个月内）
        /// </summary>
        /// <returns></returns>
        public List<GetEccpMaintenanceContractForView> GetMaintenanceContractList()
        {
            var maintenanceContractList = _eccpMaintenanceContractRepository.GetAll().Where(e => e.EndDate.Date > DateTime.Now.Date &&  e.EndDate.Date< DateTime.Now.Date.AddMonths(2) && e.IsStop == false).OrderByDescending(e => e.EndDate).Take(5);
            var query =
                   (from o in maintenanceContractList
                    join o1 in this._eCCPBaseMaintenanceCompanyRepository.GetAll() on o.MaintenanceCompanyId equals
                        o1.Id into j1
                    from s1 in j1.DefaultIfEmpty()
                    join o2 in this._eccpBasePropertyCompanyRepository.GetAll() on o.PropertyCompanyId equals o2.Id
                        into j2
                    from s2 in j2.DefaultIfEmpty()
                    select new 
                    {
                        EccpMaintenanceContract = o,
                        ECCPBaseMaintenanceCompanyName = s1 == null ? string.Empty : s1.Name,
                        ECCPBasePropertyCompanyName = s2 == null ? string.Empty : s2.Name
                    });

            var eccpMaintenanceContracts = new List<GetEccpMaintenanceContractForView>();

            return query.MapTo(eccpMaintenanceContracts);
        }


        public static async Task<List<LiftFunction>> PostRequest(string url)
        {
            HttpClient _httpClient = new HttpClient();
             var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string   s   = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            List<LiftFunction> list = new List<LiftFunction>();
            if (s != "[]")
            {
                list = JsonConvert.DeserializeObject<List<LiftFunction>>(s);
            }           
            //JsonMessage jm = JsonConvert.DeserializeObject<JsonMessage>(s);
            
            return list;
        }


        public GetSalesSummaryOutput GetSalesSummary(GetSalesSummaryInput input)
        {
            return new GetSalesSummaryOutput(DashboardRandomDataGenerator.GenerateSalesSummaryData(input.SalesSummaryDatePeriod));
        }

        public GetRegionalStatsOutput GetRegionalStats(GetRegionalStatsInput input)
        {
            return new GetRegionalStatsOutput(DashboardRandomDataGenerator.GenerateRegionalStat());
        }

        public GetGeneralStatsOutput GetGeneralStats(GetGeneralStatsInput input)
        {
            return new GetGeneralStatsOutput
            {
                TransactionPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                NewVisitPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                BouncePercent = DashboardRandomDataGenerator.GetRandomInt(10, 100)
            };
        }
    }
}
