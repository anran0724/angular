// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenancePlansAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.AutoMapper;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Linq.Extensions;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans.Exporting;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;

    /// <summary>
    /// The eccp maintenance plans app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans)]
    public class EccpMaintenancePlansAppService : ElevatorCloudAppServiceBase, IEccpMaintenancePlansAppService
    {
        /// <summary>
        /// The _eccp base elevator repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;

        /// <summary>
        /// The _e ccp base property company repository.
        /// </summary>
        private readonly IRepository<ECCPBasePropertyCompany> _eccpBasePropertyCompanyRepository;

        /// <summary>
        /// The _eccp dict maintenance type repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceType> _eccpDictMaintenanceTypeRepository;
        private readonly IRepository<EccpDictMaintenanceStatus, int> _eccpDictMaintenanceStatusRepository;

        /// <summary>
        /// The _eccp maintenance contract_ elevator_ link.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract_Elevator_Link, long> _eccpMaintenanceContractElevatorLink;

        /// <summary>
        /// The _eccp maintenance contract repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract, long> _eccpMaintenanceContractRepository;

        /// <summary>
        /// The _eccp maintenance plan_ maintenance user_ link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> _eccpMaintenancePlanMaintenanceUserLinkRepository;

        /// <summary>
        /// The _eccp maintenance plan_ property user_ link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan_PropertyUser_Link, long> _eccpMaintenancePlanPropertyUserLinkRepository;

        /// <summary>
        /// The _eccp maintenance plan_ template_ link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan_Template_Link, long> _eccpMaintenancePlanTemplateLinkRepository;

        /// <summary>
        /// The _eccp maintenance plan custom rule.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlanCustomRule, int> _eccpMaintenancePlanCustomRule;

        /// <summary>
        /// The _eccp maintenance plan repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan> _eccpMaintenancePlanRepository;

        /// <summary>
        /// The _eccp maintenance plans excel exporter.
        /// </summary>
        private readonly IEccpMaintenancePlansExcelExporter _eccpMaintenancePlansExcelExporter;

        /// <summary>
        /// The _eccp maintenance template repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTemplate, int> _eccpMaintenanceTemplateRepository;

        /// <summary>
        /// The _eccp maintenance work order manager.
        /// </summary>
        private readonly EccpMaintenanceWorkOrderManager _eccpMaintenanceWorkOrderManager;

        /// <summary>
        /// The _user manager.
        /// </summary>
        private readonly UserManager _userManager;
        private readonly IRepository<EccpMaintenanceWorkOrder> _eccpMaintenanceWorkOrderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenancePlansAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenancePlanRepository">
        /// The eccp maintenance plan repository.
        /// </param>
        /// <param name="eccpMaintenancePlansExcelExporter">
        /// The eccp maintenance plans excel exporter.
        /// </param>
        /// <param name="eccpBaseElevatorRepository">
        /// The eccp base elevator repository.
        /// </param>
        /// <param name="eccpMaintenancePlanMaintenanceUserLinkRepository">
        /// The eccp maintenance plan_ maintenance user_ link repository.
        /// </param>
        /// <param name="eccpMaintenancePlanPropertyUserLinkRepository">
        /// The eccp maintenance plan_ property user_ link repository.
        /// </param>
        /// <param name="eccpMaintenancePlanTemplateLinkRepository">
        /// The eccp maintenance plan_ template_ link repository.
        /// </param>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="eccpDictMaintenanceTypeRepository">
        /// The eccp dict maintenance type repository.
        /// </param>
        /// <param name="eccpMaintenanceTemplateRepository">
        /// The eccp maintenance template repository.
        /// </param>
        /// <param name="eccpMaintenanceContractRepository">
        /// The eccp maintenance contract repository.
        /// </param>
        /// <param name="eccpBasePropertyCompanyRepository">
        /// The e ccp base property company repository.
        /// </param>
        /// <param name="eccpMaintenancePlanCustomRule">
        /// The eccp maintenance plan custom rule.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderManager">
        /// The eccp maintenance work order manager.
        /// </param>
        /// <param name="eccpMaintenanceContractElevatorLink">
        /// The eccp maintenance contract_ elevator_ link.
        /// </param>
        public EccpMaintenancePlansAppService(
            IRepository<EccpMaintenancePlan> eccpMaintenancePlanRepository,
            IEccpMaintenancePlansExcelExporter eccpMaintenancePlansExcelExporter,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository,
            IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> eccpMaintenancePlanMaintenanceUserLinkRepository,
            IRepository<EccpMaintenancePlan_PropertyUser_Link, long> eccpMaintenancePlanPropertyUserLinkRepository,
            IRepository<EccpMaintenancePlan_Template_Link, long> eccpMaintenancePlanTemplateLinkRepository,
            UserManager userManager,
            IRepository<EccpDictMaintenanceType> eccpDictMaintenanceTypeRepository,
            IRepository<EccpMaintenanceTemplate, int> eccpMaintenanceTemplateRepository,
            IRepository<EccpMaintenanceContract, long> eccpMaintenanceContractRepository,
            IRepository<ECCPBasePropertyCompany> eccpBasePropertyCompanyRepository,
            IRepository<EccpMaintenancePlanCustomRule, int> eccpMaintenancePlanCustomRule,
            EccpMaintenanceWorkOrderManager eccpMaintenanceWorkOrderManager,
            IRepository<EccpMaintenanceContract_Elevator_Link, long> eccpMaintenanceContractElevatorLink, IRepository<EccpMaintenanceWorkOrder> eccpMaintenanceWorkOrderRepository, IRepository<EccpDictMaintenanceStatus, int> eccpDictMaintenanceStatusRepository)
        {
            this._eccpMaintenancePlanRepository = eccpMaintenancePlanRepository;
            this._eccpMaintenancePlansExcelExporter = eccpMaintenancePlansExcelExporter;
            this._eccpBaseElevatorRepository = eccpBaseElevatorRepository;
            this._eccpMaintenancePlanMaintenanceUserLinkRepository =
                eccpMaintenancePlanMaintenanceUserLinkRepository;
            this._eccpMaintenancePlanPropertyUserLinkRepository = eccpMaintenancePlanPropertyUserLinkRepository;
            this._eccpMaintenancePlanTemplateLinkRepository = eccpMaintenancePlanTemplateLinkRepository;
            this._userManager = userManager;
            this._eccpDictMaintenanceTypeRepository = eccpDictMaintenanceTypeRepository;
            this._eccpMaintenanceTemplateRepository = eccpMaintenanceTemplateRepository;
            this._eccpMaintenanceContractRepository = eccpMaintenanceContractRepository;
            this._eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
            this._eccpMaintenancePlanCustomRule = eccpMaintenancePlanCustomRule;
            this._eccpMaintenanceWorkOrderManager = eccpMaintenanceWorkOrderManager;
            this._eccpMaintenanceContractElevatorLink = eccpMaintenanceContractElevatorLink;
            this._eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
            this._eccpDictMaintenanceStatusRepository = eccpDictMaintenanceStatusRepository;
        }

        /// <summary>
        /// The close plan.
        /// </summary>
        /// <param name="planGroupGuid">
        /// The plan group id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// The exception.
        /// </exception>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_ClosePlan)]
        public async Task ClosePlan(Guid? planGroupGuid)
        {
            var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().WhereIf(
                planGroupGuid != null,
                e => e.PlanGroupGuid == planGroupGuid && !e.IsCancel);
            foreach (var eccpMaintenancePlan in eccpMaintenancePlans)
            {
                if (!eccpMaintenancePlan.IsClose)
                {
                    var resultCount =
                        this._eccpMaintenanceWorkOrderManager.PlanDeletingRearrangeWorkOrder(eccpMaintenancePlan.Id);
                    if (resultCount >= 0)
                    {
                        eccpMaintenancePlan.IsClose = true;
                        await this._eccpMaintenancePlanRepository.UpdateAsync(eccpMaintenancePlan);
                    }
                    else
                    {
                        throw new Exception("计划删除计划工单刷新异常，错误代码：" + resultCount);
                    }
                }
            }
        }

        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task CreateOrEdit(CreateOrEditEccpMaintenancePlanInfoDto input)
        {
            if (input.Id == null)
            {
                await this.Create(input);
            }
            else
            {
                await this.Update(input);
            }
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpMaintenancePlanRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<GetEccpMaintenancePlanForView>> GetAll(GetAllEccpMaintenancePlansInput input)
        {
            var filteredEccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().Include(e => e.Elevator).Where(e => !e.IsCancel)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                .WhereIf(input.IsCloseFilter > -1, e => Convert.ToInt32(e.IsClose) == input.IsCloseFilter)
                .WhereIf(input.MinPollingPeriodFilter != null, e => e.PollingPeriod >= input.MinPollingPeriodFilter)
                .WhereIf(input.MaxPollingPeriodFilter != null, e => e.PollingPeriod <= input.MaxPollingPeriodFilter)
                .WhereIf(input.MinRemindHourFilter != null, e => e.RemindHour >= input.MinRemindHourFilter).WhereIf(
                    input.MaxRemindHourFilter != null,
                    e => e.RemindHour <= input.MaxRemindHourFilter);

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var query = from o in filteredEccpMaintenancePlans
                            group o by o.PlanGroupGuid
                             into t
                            select new
                            {
                                EccpMaintenancePlan = t.FirstOrDefault(),
                                EccpBaseElevatorNum = t.Count()
                            };

                var totalCount = await query.CountAsync();

                var eccpMaintenancePlans = new List<GetEccpMaintenancePlanForView>();

                query.WhereIf(input.MinElevatorNumFilter != null, e => e.EccpBaseElevatorNum >= input.MinElevatorNumFilter).WhereIf(
                    input.MaxElevatorNumFilter != null,
                    e => e.EccpBaseElevatorNum <= input.MaxElevatorNumFilter).OrderBy(input.Sorting ?? "eccpMaintenancePlan.id asc").PageBy(input).MapTo(eccpMaintenancePlans);

                return new PagedResultDto<GetEccpMaintenancePlanForView>(totalCount, eccpMaintenancePlans);
            }
        }

        /// <summary>
        /// The get all eccp base elevator for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans)]
        public async Task<PagedResultDto<EccpBaseElevatorLookupTableDto>> GetAllEccpBaseElevatorForLookupTable(
            GetAllForLookupTableInput input)
        {
            var maintenanceContracts =
                this._eccpMaintenanceContractRepository.GetAll().Where(e => e.EndDate > DateTime.Now && !e.IsStop);
            var maintenanceContractList = from o in maintenanceContracts
                                          join t in this._eccpMaintenanceContractElevatorLink.GetAll() on o.Id equals
                                              t.MaintenanceContractId
                                          select t.ElevatorId;
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var maintenancePlans = this._eccpMaintenancePlanRepository.GetAll()
                    .Where(e => !e.IsClose && !e.IsCancel).WhereIf(
                        input.PlanGroupGuid != null,
                        e => e.PlanGroupGuid != input.PlanGroupGuid);

                var filteredElevators = this._eccpBaseElevatorRepository.GetAll().WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.Contains(input.Filter));

                var elevators = from o in maintenanceContractList
                                join baseElevator in filteredElevators on o equals
                                    baseElevator.Id
                                select new
                                {
                                    baseElevator.Id,
                                    baseElevator.Name,
                                    Longitude = Convert.ToDouble(baseElevator.Longitude),
                                    Latitude = Convert.ToDouble(baseElevator.Latitude)
                                };
                var query = from o in elevators
                            join t in maintenancePlans on o.Id equals t.ElevatorId into g
                            from tt in g.DefaultIfEmpty()
                            where tt == null
                            select new { o.Id, o.Name, Distance = double.NaN };
                if (input.ElevatorId != null)
                {
                    var baseElevator =
                        await this._eccpBaseElevatorRepository.FirstOrDefaultAsync(e => e.Id == input.ElevatorId);
                    if (baseElevator != null)
                    {
                        query = from o in elevators
                                join t in maintenancePlans on o.Id equals t.ElevatorId into g
                                from tt in g.DefaultIfEmpty()
                                where tt == null
                                orderby Math.Sqrt(
                                    (Convert.ToDouble(baseElevator.Longitude) - o.Longitude) * Math.PI * 12656
                                    * Math.Cos(
                                        (Convert.ToDouble(baseElevator.Latitude) + o.Latitude) / 1500 * Math.PI / 180)
                                    / 180 * ((Convert.ToDouble(baseElevator.Longitude) - o.Longitude) * Math.PI * 12656
                                             * Math.Cos(
                                                 (Convert.ToDouble(baseElevator.Latitude) + o.Latitude) / 3 * Math.PI
                                                 / 180) / 180)
                                    + (Convert.ToDouble(baseElevator.Latitude) - o.Latitude) * Math.PI * 12656 / 180
                                    * ((Convert.ToDouble(baseElevator.Latitude) - o.Latitude) * Math.PI * 12656 / 180))
                                select new
                                {
                                    o.Id,
                                    o.Name,
                                    Distance = Math.Sqrt(
                                                              (Convert.ToDouble(baseElevator.Longitude) - o.Longitude)
                                                              * (Convert.ToDouble(baseElevator.Longitude) - o.Longitude)
                                                              + (Convert.ToDouble(baseElevator.Latitude) - o.Latitude)
                                                              * (Convert.ToDouble(baseElevator.Latitude) - o.Latitude))
                                                          * 100
                                };
                    }
                }

                // var query = elevators.WhereIf(
                // !string.IsNullOrWhiteSpace(input.Filter),
                // e => e.Name.ToString().Contains(input.Filter)).WhereIf(maintenancePlans.Any(), e => !maintenancePlans.Contains(e.Id));
                var totalCount = await query.CountAsync();

                var eccpBaseElevatorList = await query.PageBy(input).ToListAsync();

                var lookupTableDtoList = new List<EccpBaseElevatorLookupTableDto>();
                foreach (var eccpBaseElevator in eccpBaseElevatorList)
                {
                    lookupTableDtoList.Add(
                        new EccpBaseElevatorLookupTableDto
                        {
                            Id = eccpBaseElevator.Id.ToString(),
                            DisplayName = eccpBaseElevator.Name,
                            Distance = $"{eccpBaseElevator.Distance:F}"
                        });
                }

                return new PagedResultDto<EccpBaseElevatorLookupTableDto>(totalCount, lookupTableDtoList);
            }
        }

        /// <summary>
        /// The get all maintenance templates for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans)]
        public async Task<PagedResultDto<MaintenanceTemplatesLookupTableDto>> GetAllMaintenanceTemplatesForLookupTable(
            GetAllForLookupTableInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var query = this._eccpMaintenanceTemplateRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.TempName.ToString().Contains(input.Filter)).WhereIf(
                    input.MaintenanceTypeId > 0,
                    e => e.MaintenanceTypeId == input.MaintenanceTypeId);

                var totalCount = await query.CountAsync();

                var eccpMaintenanceTemplates = await query.PageBy(input).ToListAsync();

                var lookupTableDtoList = new List<MaintenanceTemplatesLookupTableDto>();
                foreach (var eccpMaintenanceTemplate in eccpMaintenanceTemplates)
                {
                    lookupTableDtoList.Add(
                        new MaintenanceTemplatesLookupTableDto
                        {
                            Id = eccpMaintenanceTemplate.Id.ToString(),
                            DisplayName = eccpMaintenanceTemplate.TempName,
                            IsDefault = eccpMaintenanceTemplate.TenantId == null
                        });
                }

                return new PagedResultDto<MaintenanceTemplatesLookupTableDto>(totalCount, lookupTableDtoList);
            }
        }

        /// <summary>
        /// The get all maintenance type.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Create)]
        public async Task<GetEccpMaintenancePlanForEditOutput> GetEccpMaintenancePlanForCreate()
        {
            var output = new GetEccpMaintenancePlanForEditOutput
            {
                EccpMaintenancePlan = new CreateOrEditEccpMaintenancePlanDto { PollingPeriod = 30, RemindHour = 72 }
            };

            var filteredEccpDictMaintenanceTypes = this._eccpDictMaintenanceTypeRepository.GetAll();
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpMaintenanceTemplates = this._eccpMaintenanceTemplateRepository.GetAll()
                    .Where(e => e.TenantId == null).GroupBy(t => t.MaintenanceTypeId).Select(
                        t => new { t.FirstOrDefault().Id, MaintenanceTypeId = t.Key, t.FirstOrDefault().TempName });

                var query = from o in filteredEccpDictMaintenanceTypes
                            join t in eccpMaintenanceTemplates on o.Id equals t.MaintenanceTypeId into s
                            from j in s.DefaultIfEmpty()
                            select new GetEccpDictMaintenanceTypeForView
                            {
                                EccpDictMaintenanceType =
                                               this.ObjectMapper.Map<GetAllEccpDictMaintenanceTypeDto>(o),
                                TypeId = o.Id,
                                MaintenanceTemplateId = j == null ? 0 : j.Id,
                                MaintenanceTemplateName = j == null ? string.Empty : j.TempName
                            };
                output.MaintenanceTypes = await query.ToListAsync();
            }

            var queryMaintenanceUsers = this._userManager.Users.Where(e => e.IsActive && e.IsLockoutEnabled);
            output.MaintenanceUserIds = string.Join(",", queryMaintenanceUsers.Select(e => e.Id));
            output.MaintenanceUserNames = string.Join(",", queryMaintenanceUsers.Select(e => e.Name));
            return output;
        }

        /// <summary>
        /// The get all maintenance user for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans)]
        public async Task<PagedResultDto<MaintenanceUserLookupTableDto>> GetAllMaintenanceUserForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._userManager.Users.Where(e => e.IsActive && e.IsLockoutEnabled).WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var userList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<MaintenanceUserLookupTableDto>();
            foreach (var user in userList)
            {
                lookupTableDtoList.Add(
                    new MaintenanceUserLookupTableDto { Id = user.Id.ToString(), DisplayName = user.Name });
            }

            return new PagedResultDto<MaintenanceUserLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all property user for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans)]
        public async Task<PagedResultDto<PropertyUserLookupTableDto>> GetAllPropertyUserForLookupTable(
            GetAllForLookupTableInput input)
        {
            var eccpMaintenanceContracts = this._eccpMaintenanceContractRepository.GetAll();
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var queryTenantIds = from m in eccpMaintenanceContracts
                                     join p in this._eccpBasePropertyCompanyRepository.GetAll() on m.PropertyCompanyId
                                         equals p.Id
                                     group p by p.TenantId
                                     into g
                                     select g.Key;

                var query = this._userManager.Users.WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.ToString().Contains(input.Filter)).WhereIf(
                    queryTenantIds.Any(),
                    e => queryTenantIds.Contains(e.TenantId.Value));

                var totalCount = await query.CountAsync();

                var eccpUserList = await query.PageBy(input).ToListAsync();

                var lookupTableDtoList = new List<PropertyUserLookupTableDto>();
                foreach (var user in eccpUserList)
                {
                    lookupTableDtoList.Add(
                        new PropertyUserLookupTableDto { Id = user.Id.ToString(), DisplayName = user.Name });
                }

                return new PagedResultDto<PropertyUserLookupTableDto>(totalCount, lookupTableDtoList);
            }
        }

        /// <summary>
        /// The get eccp maintenance plan for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Edit)]
        public async Task<GetEccpMaintenancePlanForEditOutput> GetEccpMaintenancePlanForEdit(EntityDto input)
        {
            var eccpMaintenancePlan = await this._eccpMaintenancePlanRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpMaintenancePlanForEditOutput
            {
                EccpMaintenancePlan =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenancePlanDto>(eccpMaintenancePlan)
            };

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                if (output.EccpMaintenancePlan.PlanGroupGuid != null)
                {
                    var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().Where(
                        e => e.PlanGroupGuid == output.EccpMaintenancePlan.PlanGroupGuid && !e.IsCancel && !e.IsClose);
                    var elevators = from o in eccpMaintenancePlans
                                    join t in this._eccpBaseElevatorRepository.GetAll() on o.ElevatorId equals t.Id
                                    select new { t.Id, t.Name };

                    output.ElevatorIds = string.Join(",", elevators.Select(e => e.Id));
                    output.ElevatorNames = string.Join(",", elevators.Select(e => e.Name));

                    var eccpMaintenancePlanCustomRule =
                        await this._eccpMaintenancePlanCustomRule.FirstOrDefaultAsync(
                            t => t.PlanGroupGuid == output.EccpMaintenancePlan.PlanGroupGuid);
                    if (eccpMaintenancePlanCustomRule != null)
                    {
                        output.QuarterPollingPeriod = eccpMaintenancePlanCustomRule.QuarterPollingPeriod;
                        output.HalfYearPollingPeriod = eccpMaintenancePlanCustomRule.HalfYearPollingPeriod;
                        output.YearPollingPeriod = eccpMaintenancePlanCustomRule.YearPollingPeriod;
                    }
                }

                var eccpMaintenancePlanMaintenanceUserLinks = this._eccpMaintenancePlanMaintenanceUserLinkRepository
                    .GetAll().Where(a => a.MaintenancePlanId == output.EccpMaintenancePlan.Id);

                var queryMaintenanceUsers = from o in eccpMaintenancePlanMaintenanceUserLinks
                                            join u in this._userManager.Users on o.UserId equals u.Id
                                            select new { MaintenanceUserId = u.Id, MaintenanceUserName = u.Name };
                output.MaintenanceUserIds = string.Join(",", queryMaintenanceUsers.Select(e => e.MaintenanceUserId));
                output.MaintenanceUserNames = string.Join(
                    ",",
                    queryMaintenanceUsers.Select(e => e.MaintenanceUserName));

                //var eccpMaintenancePlanPropertyUserLinks = this._eccpMaintenancePlanPropertyUserLinkRepository
                //    .GetAll().Where(a => a.MaintenancePlanId == output.EccpMaintenancePlan.Id);

                //var queryPropertyUsers = from o in eccpMaintenancePlanPropertyUserLinks
                //                         join u in this._userManager.Users on o.UserId equals u.Id
                //                         select new { PropertyUserId = u.Id, PropertyUserName = u.Name };

                //output.PropertyUserNames = string.Join(",", queryPropertyUsers.Select(e => e.PropertyUserName));
                //output.PropertyUserIds = string.Join(",", queryPropertyUsers.Select(e => e.PropertyUserId));

                var maintenanceTypes = this._eccpDictMaintenanceTypeRepository.GetAll();
                var eccpMaintenancePlanTemplateLink = this._eccpMaintenancePlanTemplateLinkRepository.GetAll()
                    .WhereIf(
                        output.EccpMaintenancePlan.Id > 0,
                        a => a.MaintenancePlanId == output.EccpMaintenancePlan.Id);

                var eccpMaintenanceTemplates = this._eccpMaintenanceTemplateRepository.GetAll();

                var query = from o in eccpMaintenanceTemplates
                            join a in eccpMaintenancePlanTemplateLink on o.Id equals a.MaintenanceTemplateId
                            select new
                            {
                                TypeId = o.MaintenanceTypeId,
                                a.MaintenanceTemplateId,
                                MaintenanceTemplateName = o.TempName
                            };

                output.MaintenanceTypes = new List<GetEccpDictMaintenanceTypeForView>();
                foreach (var mainType in maintenanceTypes)
                {
                    var eccpMaintenanceTemplate = await query.WhereIf(mainType.Id > 0, m => m.TypeId == mainType.Id)
                                                      .FirstOrDefaultAsync();
                    var entity = new GetEccpDictMaintenanceTypeForView
                    {
                        TypeId = mainType.Id,
                        EccpDictMaintenanceType =
                                             new GetAllEccpDictMaintenanceTypeDto
                                             {
                                                 Id = mainType.Id,
                                                 Name = mainType.Name
                                             },
                        MaintenanceTemplateId =
                                             eccpMaintenanceTemplate?.MaintenanceTemplateId ?? 0,
                        MaintenanceTemplateName = eccpMaintenanceTemplate != null
                                                                       ? eccpMaintenanceTemplate.MaintenanceTemplateName
                                                                       : string.Empty
                    };
                    output.MaintenanceTypes.Add(entity);
                }

                // output.MaintenanceTypes = list;
                return output;
            }
        }


        public async Task<GetEccpMaintenancePlanForEditOutput> GetEccpMaintenancePlanForView(EntityDto input)
        {
            var eccpMaintenancePlan = await this._eccpMaintenancePlanRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpMaintenancePlanForEditOutput
            {
                EccpMaintenancePlan =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenancePlanDto>(eccpMaintenancePlan)
            };

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                if (output.EccpMaintenancePlan.PlanGroupGuid != null)
                {
                    var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll().Where(
                        e => e.PlanGroupGuid == output.EccpMaintenancePlan.PlanGroupGuid);
                    var elevators = from o in eccpMaintenancePlans
                                    join t in this._eccpBaseElevatorRepository.GetAll() on o.ElevatorId equals t.Id
                                    select new { t.Id, t.Name };

                    output.ElevatorIds = string.Join(",", elevators.Select(e => e.Id));
                    output.ElevatorNames = string.Join(",", elevators.Select(e => e.Name));

                    var eccpMaintenancePlanCustomRule =
                        await this._eccpMaintenancePlanCustomRule.FirstOrDefaultAsync(
                            t => t.PlanGroupGuid == output.EccpMaintenancePlan.PlanGroupGuid);
                    if (eccpMaintenancePlanCustomRule != null)
                    {
                        output.QuarterPollingPeriod = eccpMaintenancePlanCustomRule.QuarterPollingPeriod;
                        output.HalfYearPollingPeriod = eccpMaintenancePlanCustomRule.HalfYearPollingPeriod;
                        output.YearPollingPeriod = eccpMaintenancePlanCustomRule.YearPollingPeriod;
                    }
                }

                var eccpMaintenancePlanMaintenanceUserLinks = this._eccpMaintenancePlanMaintenanceUserLinkRepository
                    .GetAll().Where(a => a.MaintenancePlanId == output.EccpMaintenancePlan.Id);

                var queryMaintenanceUsers = from o in eccpMaintenancePlanMaintenanceUserLinks
                                            join u in this._userManager.Users on o.UserId equals u.Id
                                            select new { MaintenanceUserId = u.Id, MaintenanceUserName = u.Name };
                output.MaintenanceUserIds = string.Join(",", queryMaintenanceUsers.Select(e => e.MaintenanceUserId));
                output.MaintenanceUserNames = string.Join(
                    ",",
                    queryMaintenanceUsers.Select(e => e.MaintenanceUserName));

                //var eccpMaintenancePlanPropertyUserLinks = this._eccpMaintenancePlanPropertyUserLinkRepository
                //    .GetAll().Where(a => a.MaintenancePlanId == output.EccpMaintenancePlan.Id);

                //var queryPropertyUsers = from o in eccpMaintenancePlanPropertyUserLinks
                //                         join u in this._userManager.Users on o.UserId equals u.Id
                //                         select new { PropertyUserId = u.Id, PropertyUserName = u.Name };

                //output.PropertyUserNames = string.Join(",", queryPropertyUsers.Select(e => e.PropertyUserName));
                //output.PropertyUserIds = string.Join(",", queryPropertyUsers.Select(e => e.PropertyUserId));

                var maintenanceTypes = this._eccpDictMaintenanceTypeRepository.GetAll();
                var eccpMaintenancePlanTemplateLink = this._eccpMaintenancePlanTemplateLinkRepository.GetAll()
                    .WhereIf(
                        output.EccpMaintenancePlan.Id > 0,
                        a => a.MaintenancePlanId == output.EccpMaintenancePlan.Id);

                var eccpMaintenanceTemplates = this._eccpMaintenanceTemplateRepository.GetAll();

                var query = from o in eccpMaintenanceTemplates
                            join a in eccpMaintenancePlanTemplateLink on o.Id equals a.MaintenanceTemplateId
                            select new
                            {
                                TypeId = o.MaintenanceTypeId,
                                a.MaintenanceTemplateId,
                                MaintenanceTemplateName = o.TempName
                            };

                output.MaintenanceTypes = new List<GetEccpDictMaintenanceTypeForView>();
                foreach (var mainType in maintenanceTypes)
                {
                    var eccpMaintenanceTemplate = await query.WhereIf(mainType.Id > 0, m => m.TypeId == mainType.Id)
                                                      .FirstOrDefaultAsync();
                    var entity = new GetEccpDictMaintenanceTypeForView
                    {
                        TypeId = mainType.Id,
                        EccpDictMaintenanceType =
                                             new GetAllEccpDictMaintenanceTypeDto
                                             {
                                                 Id = mainType.Id,
                                                 Name = mainType.Name
                                             },
                        MaintenanceTemplateId =
                                             eccpMaintenanceTemplate?.MaintenanceTemplateId ?? 0,
                        MaintenanceTemplateName = eccpMaintenanceTemplate != null
                                                                       ? eccpMaintenanceTemplate.MaintenanceTemplateName
                                                                       : string.Empty
                    };
                    output.MaintenanceTypes.Add(entity);
                }

                // output.MaintenanceTypes = list;
                return output;
            }
        }

        /// <summary>
        /// The get eccp maintenance plans to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetEccpMaintenancePlansToExcel(GetAllEccpMaintenancePlansForExcelInput input)
        {
            var filteredEccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                .WhereIf(input.MinPollingPeriodFilter != null, e => e.PollingPeriod >= input.MinPollingPeriodFilter)
                .WhereIf(input.MaxPollingPeriodFilter != null, e => e.PollingPeriod <= input.MaxPollingPeriodFilter)
                .WhereIf(input.MinRemindHourFilter != null, e => e.RemindHour >= input.MinRemindHourFilter).WhereIf(
                    input.MaxRemindHourFilter != null,
                    e => e.RemindHour <= input.MaxRemindHourFilter);

            var query = from o in filteredEccpMaintenancePlans
                        group o by o.PlanGroupGuid
                        into t
                        select new GetEccpMaintenancePlanForView
                        {
                            EccpMaintenancePlan =
                                           this.ObjectMapper.Map<EccpMaintenancePlanDto>(t.FirstOrDefault()),
                            EccpBaseElevatorNum = t.Count()
                        };

            var eccpMaintenancePlanListDtos = await query.ToListAsync();

            return this._eccpMaintenancePlansExcelExporter.ExportToFile(eccpMaintenancePlanListDtos);
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// The exception.
        /// Exception
        /// </exception>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Create)]
        private async Task Create(CreateOrEditEccpMaintenancePlanInfoDto input)
        {
            if (!string.IsNullOrWhiteSpace(input.ElevatorIds))
            {
                var elevatorId = input.ElevatorIds.Split(',');
                var planGroupGuid = Guid.NewGuid();

                for (var i = 0; i < elevatorId.Length; i++)
                {
                    var eccpMaintenancePlan = await this._eccpMaintenancePlanRepository.FirstOrDefaultAsync(
                                                  e => e.ElevatorId == new Guid(elevatorId[i]) && e.IsCancel
                                                                                               && !e.IsClose);
                    if (eccpMaintenancePlan != null)
                    {
                        input.IsCancel = false;
                        input.PlanGroupGuid = planGroupGuid;
                        input.ElevatorId = new Guid(elevatorId[i]);
                        input.Id = eccpMaintenancePlan.Id;
                        this.ObjectMapper.Map(input, eccpMaintenancePlan);

                        // await _eccpMaintenancePlanRepository.UpdateAsync(eccpMaintenancePlan);
                    }
                    else
                    {
                        eccpMaintenancePlan = this.ObjectMapper.Map<EccpMaintenancePlan>(input);

                        if (this.AbpSession.TenantId != null)
                        {
                            eccpMaintenancePlan.TenantId = (int)this.AbpSession.TenantId;
                        }

                        eccpMaintenancePlan.ElevatorId = new Guid(elevatorId[i]);
                        eccpMaintenancePlan.PlanGroupGuid = planGroupGuid;

                        // eccpMaintenancePlan.Id = 0;
                        await this._eccpMaintenancePlanRepository.InsertAndGetIdAsync(eccpMaintenancePlan);
                    }

                    if (!string.IsNullOrWhiteSpace(input.MaintenanceUserIds))
                    {
                        var list = new List<string>(
                            input.MaintenanceUserIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));

                        var eccpMaintenancePlanMaintenanceUserLinkList =
                            await this._eccpMaintenancePlanMaintenanceUserLinkRepository.GetAllListAsync(
                                a => a.MaintenancePlanId == eccpMaintenancePlan.Id
                                     && !list.Contains(a.UserId.ToString()));

                        foreach (var eccpMaintenancePlanMaintenanceUserLink in
                            eccpMaintenancePlanMaintenanceUserLinkList)
                        {
                            await this._eccpMaintenancePlanMaintenanceUserLinkRepository.DeleteAsync(
                                eccpMaintenancePlanMaintenanceUserLink.Id);
                        }

                        foreach (var userId in list)
                        {
                            var eccpMaintenancePlanMaintenanceUserLink =
                                await this._eccpMaintenancePlanMaintenanceUserLinkRepository.FirstOrDefaultAsync(
                                    a => a.UserId == Convert.ToInt32(userId)
                                         && a.MaintenancePlanId == eccpMaintenancePlan.Id);
                            if (eccpMaintenancePlanMaintenanceUserLink == null)
                            {
                                var linkEntity = new EccpMaintenancePlan_MaintenanceUser_Link
                                {
                                    UserId = Convert.ToInt32(userId),
                                    MaintenancePlanId = eccpMaintenancePlan.Id
                                };
                                await this._eccpMaintenancePlanMaintenanceUserLinkRepository.InsertAsync(linkEntity);
                            }
                        }
                    }

                    //if (!string.IsNullOrWhiteSpace(input.PropertyUserIds))
                    //{
                    //    var list = new List<string>(
                    //        input.PropertyUserIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));

                    //    var eccpMaintenancePlanPropertyUserLinkList =
                    //        await this._eccpMaintenancePlanPropertyUserLinkRepository.GetAllListAsync(
                    //            a => a.MaintenancePlanId == eccpMaintenancePlan.Id
                    //                 && !list.Contains(a.UserId.ToString()));

                    //    foreach (var item in eccpMaintenancePlanPropertyUserLinkList)
                    //    {
                    //        await this._eccpMaintenancePlanPropertyUserLinkRepository.DeleteAsync(item.Id);
                    //    }

                    //    foreach (var item in list)
                    //    {
                    //        var eccpMaintenancePlanPropertyUserLink =
                    //            await this._eccpMaintenancePlanPropertyUserLinkRepository.FirstOrDefaultAsync(
                    //                a => a.UserId == Convert.ToInt32(item)
                    //                     && a.MaintenancePlanId == eccpMaintenancePlan.Id);
                    //        if (eccpMaintenancePlanPropertyUserLink == null)
                    //        {
                    //            var linkEntity = new EccpMaintenancePlan_PropertyUser_Link
                    //            {
                    //                UserId = Convert.ToInt32(item),
                    //                MaintenancePlanId = eccpMaintenancePlan.Id
                    //            };
                    //            await this._eccpMaintenancePlanPropertyUserLinkRepository.InsertAsync(linkEntity);
                    //        }
                    //    }
                    //}

                    if (input.MaintenanceTypes.Count > 0)
                    {
                        foreach (var maintenanceType in input.MaintenanceTypes)
                        {
                            var eccpMaintenancePlanTemplateLink = this._eccpMaintenancePlanTemplateLinkRepository
                                .GetAll().WhereIf(
                                    eccpMaintenancePlan.Id > 0,
                                    a => a.MaintenancePlanId == eccpMaintenancePlan.Id);
                            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                            {
                                var eccpMaintenanceTemplates = this._eccpMaintenanceTemplateRepository.GetAll().WhereIf(
                                    maintenanceType.TypeId > 0,
                                    a => a.MaintenanceTypeId == maintenanceType.TypeId);

                                var query = from o in eccpMaintenancePlanTemplateLink
                                            join a in eccpMaintenanceTemplates on o.MaintenanceTemplateId equals a.Id
                                            select new GetEccpDictMaintenanceTypeForView
                                            {
                                                TypeId = a.MaintenanceTypeId,
                                                MaintenanceTemplateId = o.MaintenanceTemplateId,
                                                MaintenanceTemplateName = a.TempName
                                            };
                                var eccpMaintenanceTemplate = await query.FirstOrDefaultAsync();

                                if (eccpMaintenanceTemplate != null)
                                {
                                    var linkEntity =
                                        await this._eccpMaintenancePlanTemplateLinkRepository.FirstOrDefaultAsync(
                                            a => a.MaintenancePlanId == eccpMaintenancePlan.Id
                                                 && a.MaintenanceTemplateId
                                                 == eccpMaintenanceTemplate.MaintenanceTemplateId);
                                    linkEntity.MaintenanceTemplateId = maintenanceType.MaintenanceTemplateId;
                                    await this._eccpMaintenancePlanTemplateLinkRepository.UpdateAsync(linkEntity);
                                }
                                else
                                {
                                    var linkEntity = new EccpMaintenancePlan_Template_Link
                                    {
                                        MaintenanceTemplateId =
                                                                 maintenanceType.MaintenanceTemplateId,
                                        MaintenancePlanId = eccpMaintenancePlan.Id
                                    };
                                    await this._eccpMaintenancePlanTemplateLinkRepository.InsertAsync(linkEntity);
                                }
                            }
                        }
                    }

                    var resultCount = await this._eccpMaintenanceWorkOrderManager.PlanModificationRearrangeWorkOrder(eccpMaintenancePlan.Id);
                    if (resultCount < 0)
                    {
                        throw new Exception("计划修改和添加工单刷新，错误代码：" + resultCount);
                    }
                }

                if (input.IsSkip > 0)
                {
                    var eccpMaintenancePlanCustomRule = this.ObjectMapper.Map<EccpMaintenancePlanCustomRule>(input);
                    eccpMaintenancePlanCustomRule.PlanGroupGuid = planGroupGuid;
                    await this._eccpMaintenancePlanCustomRule.InsertAsync(eccpMaintenancePlanCustomRule);
                }
            }
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// The exception.
        /// </exception>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Edit)]
        private async Task Update(CreateOrEditEccpMaintenancePlanInfoDto input)
        {
            if (!string.IsNullOrWhiteSpace(input.ElevatorIds))
            {
                // var maintenancePlan = await _eccpMaintenancePlanRepository.FirstOrDefaultAsync((int)input.Id);
                var elevatorIdList = new List<string>(
                    input.ElevatorIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                var maintenancePlans = this._eccpMaintenancePlanRepository.GetAll()
                    .WhereIf(input.PlanGroupGuid != null, e => e.PlanGroupGuid == input.PlanGroupGuid).WhereIf(
                        elevatorIdList.Count > 0,
                        e => !elevatorIdList.Contains(e.ElevatorId.ToString()));

                foreach (var elevatorId in elevatorIdList)
                {
                    input.ElevatorId = new Guid(elevatorId);
                    var eccpMaintenancePlan =
                        await this._eccpMaintenancePlanRepository.FirstOrDefaultAsync(
                            t => t.ElevatorId == input.ElevatorId && !t.IsClose);
                    if (eccpMaintenancePlan != null)
                    {
                        input.IsCancel = false;
                        input.Id = eccpMaintenancePlan.Id;
                        this.ObjectMapper.Map(input, eccpMaintenancePlan);
                    }
                    else
                    {
                        eccpMaintenancePlan = this.ObjectMapper.Map<EccpMaintenancePlan>(input);
                        eccpMaintenancePlan.Id = 0;
                        if (this.AbpSession.TenantId != null)
                        {
                            eccpMaintenancePlan.TenantId = (int)this.AbpSession.TenantId;
                        }

                        eccpMaintenancePlan.ElevatorId = input.ElevatorId;
                        eccpMaintenancePlan.PlanGroupGuid = input.PlanGroupGuid;
                        await this._eccpMaintenancePlanRepository.InsertAndGetIdAsync(eccpMaintenancePlan);
                    }

                    if (!string.IsNullOrWhiteSpace(input.MaintenanceUserIds))
                    {
                        var list = new List<string>(
                            input.MaintenanceUserIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));

                        var eccpMaintenancePlanMaintenanceUserLinkList =
                            await this._eccpMaintenancePlanMaintenanceUserLinkRepository.GetAllListAsync(
                                a => a.MaintenancePlanId == eccpMaintenancePlan.Id
                                     && !list.Contains(a.UserId.ToString()));

                        foreach (var eccpMaintenancePlanMaintenanceUserLink in
                            eccpMaintenancePlanMaintenanceUserLinkList)
                        {
                            await this._eccpMaintenancePlanMaintenanceUserLinkRepository.DeleteAsync(
                                eccpMaintenancePlanMaintenanceUserLink.Id);
                        }

                        foreach (var userId in list)
                        {
                            var eccpMaintenancePlanMaintenanceUserLink =
                                await this._eccpMaintenancePlanMaintenanceUserLinkRepository.FirstOrDefaultAsync(
                                    a => a.UserId == Convert.ToInt32(userId)
                                         && a.MaintenancePlanId == eccpMaintenancePlan.Id);
                            if (eccpMaintenancePlanMaintenanceUserLink == null)
                            {
                                var linkEntity = new EccpMaintenancePlan_MaintenanceUser_Link
                                {
                                    UserId = Convert.ToInt32(userId),
                                    MaintenancePlanId = eccpMaintenancePlan.Id
                                };
                                await this._eccpMaintenancePlanMaintenanceUserLinkRepository.InsertAsync(linkEntity);
                            }
                        }
                    }

                    //if (!string.IsNullOrWhiteSpace(input.PropertyUserIds))
                    //{
                    //    var list = new List<string>(
                    //        input.PropertyUserIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));

                    //    var eccpMaintenancePlanPropertyUserLinkList =
                    //        await this._eccpMaintenancePlanPropertyUserLinkRepository.GetAllListAsync(
                    //            a => a.MaintenancePlanId == eccpMaintenancePlan.Id
                    //                 && !list.Contains(a.UserId.ToString()));

                    //    foreach (var item in eccpMaintenancePlanPropertyUserLinkList)
                    //    {
                    //        await this._eccpMaintenancePlanPropertyUserLinkRepository.DeleteAsync(item.Id);
                    //    }

                    //    foreach (var item in list)
                    //    {
                    //        var eccpMaintenancePlanPropertyUserLink =
                    //            await this._eccpMaintenancePlanPropertyUserLinkRepository.FirstOrDefaultAsync(
                    //                a => a.UserId == Convert.ToInt32(item)
                    //                     && a.MaintenancePlanId == eccpMaintenancePlan.Id);
                    //        if (eccpMaintenancePlanPropertyUserLink == null)
                    //        {
                    //            var linkEntity = new EccpMaintenancePlan_PropertyUser_Link
                    //            {
                    //                UserId = Convert.ToInt32(item),
                    //                MaintenancePlanId = eccpMaintenancePlan.Id
                    //            };
                    //            await this._eccpMaintenancePlanPropertyUserLinkRepository.InsertAsync(linkEntity);
                    //        }
                    //    }
                    //}

                    var resultCount = await this._eccpMaintenanceWorkOrderManager.PlanModificationRearrangeWorkOrder(eccpMaintenancePlan.Id);
                    if (resultCount < 0)
                    {
                        throw new Exception("计划修改和添加工单刷新，错误代码：" + resultCount);
                    }

                    if (input.MaintenanceTypes != null && input.MaintenanceTypes.Count > 0)
                    {
                        foreach (var maintenanceType in input.MaintenanceTypes)
                        {
                            var eccpMaintenancePlanTemplateLink = this._eccpMaintenancePlanTemplateLinkRepository
                                .GetAll().WhereIf(
                                    eccpMaintenancePlan.Id > 0,
                                    a => a.MaintenancePlanId == eccpMaintenancePlan.Id);
                            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                            {
                                var eccpMaintenanceTemplates = this._eccpMaintenanceTemplateRepository.GetAll().WhereIf(
                                    maintenanceType.TypeId > 0,
                                    a => a.MaintenanceTypeId == maintenanceType.TypeId);

                                var query = from o in eccpMaintenancePlanTemplateLink
                                            join a in eccpMaintenanceTemplates on o.MaintenanceTemplateId equals a.Id
                                            select new GetEccpDictMaintenanceTypeForView
                                            {
                                                TypeId = a.MaintenanceTypeId,
                                                MaintenanceTemplateId = o.MaintenanceTemplateId,
                                                MaintenanceTemplateName = a.TempName
                                            };
                                var eccpMaintenanceTemplate = await query.FirstOrDefaultAsync();

                                if (eccpMaintenanceTemplate != null)
                                {
                                    var linkEntity =
                                        await this._eccpMaintenancePlanTemplateLinkRepository.FirstOrDefaultAsync(
                                            a => a.MaintenancePlanId == eccpMaintenancePlan.Id
                                                 && a.MaintenanceTemplateId
                                                 == eccpMaintenanceTemplate.MaintenanceTemplateId);
                                    linkEntity.MaintenanceTemplateId = maintenanceType.MaintenanceTemplateId;
                                    await this._eccpMaintenancePlanTemplateLinkRepository.UpdateAsync(linkEntity);
                                }
                                else
                                {
                                    var linkEntity = new EccpMaintenancePlan_Template_Link
                                    {
                                        MaintenanceTemplateId =
                                                                 maintenanceType.MaintenanceTemplateId,
                                        MaintenancePlanId = eccpMaintenancePlan.Id
                                    };
                                    await this._eccpMaintenancePlanTemplateLinkRepository.InsertAsync(linkEntity);
                                }
                            }
                        }
                    }
                }

                foreach (var plan in maintenancePlans)
                {
                    var resultCount =
                        this._eccpMaintenanceWorkOrderManager.PlanDeletingRearrangeWorkOrder(plan.Id);
                    if (resultCount >= 0)
                    {
                        plan.IsCancel = true;
                        await this._eccpMaintenancePlanRepository.UpdateAsync(plan);
                    }
                    else
                    {
                        throw new Exception("计划删除计划工单刷新异常，错误代码：" + resultCount);
                    }
                    //var resultCount = await this._eccpMaintenanceWorkOrderManager.PlanModificationRearrangeWorkOrder(plan.Id);
                    //if (resultCount >= 0)
                    //{
                    //    plan.IsCancel = true;
                    //    await this._eccpMaintenancePlanRepository.UpdateAsync(plan);
                    //}
                    //else
                    //{
                    //    throw new Exception("计划修改和添加工单刷新，错误代码：" + resultCount);
                    //}
                }

                if (input.IsSkip > 0)
                {
                    var eccpMaintenancePlanCustomRule =
                        await this._eccpMaintenancePlanCustomRule.FirstOrDefaultAsync(
                            t => t.PlanGroupGuid == input.PlanGroupGuid);
                    if (eccpMaintenancePlanCustomRule != null)
                    {
                        input.Id = eccpMaintenancePlanCustomRule.Id;
                        this.ObjectMapper.Map(input, eccpMaintenancePlanCustomRule);
                    }
                    else
                    {
                        eccpMaintenancePlanCustomRule = this.ObjectMapper.Map<EccpMaintenancePlanCustomRule>(input);
                        eccpMaintenancePlanCustomRule.Id = 0;
                        await this._eccpMaintenancePlanCustomRule.InsertAsync(eccpMaintenancePlanCustomRule);
                    }
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders)]
        public async Task<PagedResultDto<GetEccpMaintenanceWorkOrderPlanForView>> GetAllWorkOrdersByPlanId(
            GetAllEccpMaintenanceWorkOrderPlansInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                    .Where(w => w.TenantId == this.AbpSession.TenantId)
                    .Where(w => w.MaintenancePlanId == input.PlanIdFilter)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Remark.Contains(input.Filter))
                    .WhereIf(input.IsPassedFilter > -1, e => Convert.ToInt32(e.IsPassed) == input.IsPassedFilter)
                    .WhereIf(input.MinLongitudeFilter != null, e => e.Longitude >= input.MinLongitudeFilter)
                    .WhereIf(input.MaxLongitudeFilter != null, e => e.Longitude <= input.MaxLongitudeFilter)
                    .WhereIf(input.MinLatitudeFilter != null, e => e.Latitude >= input.MinLatitudeFilter)
                    .WhereIf(input.MaxLatitudeFilter != null, e => e.Latitude <= input.MaxLatitudeFilter)
                    .WhereIf(input.MinPlanCheckDateFilter != null, e => e.PlanCheckDate >= input.MinPlanCheckDateFilter)
                    .WhereIf(input.MaxPlanCheckDateFilter != null, e => e.PlanCheckDate <= input.MaxPlanCheckDateFilter)
                    .WhereIf(input.IsClosedFilter > -1, e => Convert.ToInt32(e.IsClosed) == input.IsClosedFilter);
                var eccpMaintenancePlanMaintenanceUserLinks = this._eccpMaintenancePlanMaintenanceUserLinkRepository
                    .GetAll().Where(w => w.TenantId == this.AbpSession.TenantId);
                var users = this.UserManager.Users;

                var eccpMaintenancePlanMaintenanceUsers =
                    from eccpMaintenancePlanMaintenanceUserLink in eccpMaintenancePlanMaintenanceUserLinks
                    join user in users on eccpMaintenancePlanMaintenanceUserLink.UserId equals user.Id
                    select new { eccpMaintenancePlanMaintenanceUserLink.MaintenancePlanId, user.Name };

                var query =
                    (from o in filteredEccpMaintenanceWorkOrders
                     join o1 in this._eccpMaintenancePlanRepository.GetAll() on o.MaintenancePlanId equals o1.Id into j1
                     from s1 in j1.DefaultIfEmpty()
                     join o2 in this._eccpDictMaintenanceTypeRepository.GetAll() on o.MaintenanceTypeId equals o2.Id
                         into j2
                     from s2 in j2.DefaultIfEmpty()
                     join o3 in this._eccpDictMaintenanceStatusRepository.GetAll() on o.MaintenanceStatusId equals o3.Id
                         into j3
                     from s3 in j3.DefaultIfEmpty()
                     join o4 in eccpMaintenancePlanMaintenanceUsers on o.MaintenancePlanId equals o4.MaintenancePlanId
                         into j4
                     from s4 in j4.DefaultIfEmpty()
                     select new
                     {
                         EccpMaintenanceWorkOrder = o,
                         EccpMaintenancePlanPollingPeriod =
                                        s1 == null ? string.Empty : s1.PollingPeriod.ToString(),
                         EccpDictMaintenanceTypeName = s2 == null ? string.Empty : s2.Name,
                         EccpDictMaintenanceStatusName = s3 == null ? string.Empty : s3.Name,
                         EccpElevatorName = s1 == null ? string.Empty : s1.Elevator.Name,
                         EccpMaintenanceUserName = s4 == null ? string.Empty : s4.Name
                     }).WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpMaintenancePlanPollingPeriodFilter),
                        e => e.EccpMaintenancePlanPollingPeriod.ToLower()
                             == input.EccpMaintenancePlanPollingPeriodFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpDictMaintenanceTypeNameFilter),
                        e => e.EccpDictMaintenanceTypeName.ToLower()
                             == input.EccpDictMaintenanceTypeNameFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpDictMaintenanceStatusNameFilter),
                        e => e.EccpDictMaintenanceStatusName.ToLower()
                             == input.EccpDictMaintenanceStatusNameFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpElevatorNameFilter),
                        e => e.EccpElevatorName.ToLower().Contains(input.EccpElevatorNameFilter.ToLower().Trim()));

                var workOrderQuery = query.GroupBy(g => g.EccpMaintenanceWorkOrder.Id)
                    .Select(
                        m => new
                        {
                            m.FirstOrDefault().EccpMaintenanceWorkOrder,
                            m.FirstOrDefault().EccpMaintenancePlanPollingPeriod,
                            m.FirstOrDefault().EccpDictMaintenanceTypeName,
                            m.FirstOrDefault().EccpDictMaintenanceStatusName,
                            m.FirstOrDefault().EccpElevatorName,
                            EccpMaintenanceUserNameList =
                                         m.Select(u => u.EccpMaintenanceUserName).Distinct().ToList()
                        }).WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpMaintenanceUserNameFilter),
                        e => e.EccpMaintenanceUserNameList.Contains(input.EccpMaintenanceUserNameFilter.Trim()));

                var totalCount = await workOrderQuery.CountAsync();

                var eccpMaintenanceWorkOrders = new List<GetEccpMaintenanceWorkOrderPlanForView>();

                workOrderQuery.OrderBy(input.Sorting ?? "eccpMaintenanceWorkOrder.id asc").PageBy(input)
                    .MapTo(eccpMaintenanceWorkOrders);

                return new PagedResultDto<GetEccpMaintenanceWorkOrderPlanForView>(totalCount, eccpMaintenanceWorkOrders);
            }
        }
    }
}