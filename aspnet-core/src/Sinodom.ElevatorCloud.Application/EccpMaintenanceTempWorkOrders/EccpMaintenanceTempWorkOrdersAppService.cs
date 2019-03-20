// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTempWorkOrdersAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.EccpMaintenanceContracts;

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders
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
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Exporting;

    /// <summary>
    ///     The eccp maintenance temp work orders app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders)]
    public class EccpMaintenanceTempWorkOrdersAppService : ElevatorCloudAppServiceBase,
                                                           IEccpMaintenanceTempWorkOrdersAppService
    {
        /// <summary>
        ///     The _eccp base maintenance company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseMaintenanceCompany, int> _eccpBaseMaintenanceCompanyRepository;

        /// <summary>
        ///     The _eccp dict temp work order type repository.
        /// </summary>
        private readonly IRepository<EccpDictTempWorkOrderType, int> _eccpDictTempWorkOrderTypeRepository;

        /// <summary>
        ///     The _eccp maintenance temp work order action log repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTempWorkOrderActionLog, Guid> _eccpMaintenanceTempWorkOrderActionLogRepository;

        /// <summary>
        ///     The _eccp maintenance temp work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTempWorkOrder, Guid> _eccpMaintenanceTempWorkOrderRepository;

        /// <summary>
        ///     The _eccp maintenance temp work orders excel exporter.
        /// </summary>
        private readonly IEccpMaintenanceTempWorkOrdersExcelExporter _eccpMaintenanceTempWorkOrdersExcelExporter;

        /// <summary>
        ///     The _user repository.
        /// </summary>
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<EccpMaintenanceContract, long> _eccpMaintenanceContractRepository;
        private readonly IRepository<EccpMaintenanceContract_Elevator_Link, long> _eccpMaintenanceContractElevatorLink;
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTempWorkOrdersAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceTempWorkOrderRepository">
        /// The eccp maintenance temp work order repository.
        /// </param>
        /// <param name="eccpMaintenanceTempWorkOrdersExcelExporter">
        /// The eccp maintenance temp work orders excel exporter.
        /// </param>
        /// <param name="eccpBaseMaintenanceCompanyRepository">
        /// The eccp base maintenance company repository.
        /// </param>
        /// <param name="userRepository">
        /// The user repository.
        /// </param>
        /// <param name="eccpDictTempWorkOrderTypeRepository">
        /// The eccp dict temp work order type repository.
        /// </param>
        /// <param name="eccpMaintenanceTempWorkOrderActionLogRepository">
        /// The eccp maintenance temp work order action log repository.
        /// </param>
        public EccpMaintenanceTempWorkOrdersAppService(
            IRepository<EccpMaintenanceTempWorkOrder, Guid> eccpMaintenanceTempWorkOrderRepository,
            IEccpMaintenanceTempWorkOrdersExcelExporter eccpMaintenanceTempWorkOrdersExcelExporter,
            IRepository<ECCPBaseMaintenanceCompany, int> eccpBaseMaintenanceCompanyRepository,
            IRepository<User, long> userRepository,
            IRepository<EccpDictTempWorkOrderType, int> eccpDictTempWorkOrderTypeRepository,
            IRepository<EccpMaintenanceTempWorkOrderActionLog, Guid> eccpMaintenanceTempWorkOrderActionLogRepository,
            IRepository<EccpMaintenanceContract, long> eccpMaintenanceContractRepository,
            IRepository<EccpMaintenanceContract_Elevator_Link, long> eccpMaintenanceContractElevatorLink,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository)
        {
            this._eccpMaintenanceTempWorkOrderRepository = eccpMaintenanceTempWorkOrderRepository;
            this._eccpMaintenanceTempWorkOrdersExcelExporter = eccpMaintenanceTempWorkOrdersExcelExporter;
            this._eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
            this._userRepository = userRepository;
            this._eccpDictTempWorkOrderTypeRepository = eccpDictTempWorkOrderTypeRepository;
            this._eccpMaintenanceTempWorkOrderActionLogRepository = eccpMaintenanceTempWorkOrderActionLogRepository;
            this._eccpMaintenanceContractRepository = eccpMaintenanceContractRepository;
            this._eccpMaintenanceContractElevatorLink = eccpMaintenanceContractElevatorLink;
            this._eccpBaseElevatorRepository = eccpBaseElevatorRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpMaintenanceTempWorkOrderDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await this._eccpMaintenanceTempWorkOrderRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpMaintenanceTempWorkOrderForView>> GetAll(
            GetAllEccpMaintenanceTempWorkOrdersInput input)
        {
            var filteredEccpMaintenanceTempWorkOrders = this._eccpMaintenanceTempWorkOrderRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Title.Contains(input.Filter) || e.Describe.Contains(input.Filter))
                .WhereIf(input.MinCheckStateFilter != null, e => e.CheckState >= input.MinCheckStateFilter)
                .WhereIf(input.MaxCheckStateFilter != null, e => e.CheckState <= input.MaxCheckStateFilter).WhereIf(
                    input.MinCompletionTimeFilter != null,
                    e => e.CompletionTime >= input.MinCompletionTimeFilter).WhereIf(
                    input.MaxCompletionTimeFilter != null,
                    e => e.CompletionTime <= input.MaxCompletionTimeFilter);

            var query = (from o in filteredEccpMaintenanceTempWorkOrders
                         join o1 in this._eccpBaseMaintenanceCompanyRepository.GetAll() on o.MaintenanceCompanyId equals
                             o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         join o4 in this._eccpBaseElevatorRepository.GetAll() on o.ElevatorId equals o4.Id
                         into j4
                         from s4 in j4.DefaultIfEmpty()
                         join o2 in this._userRepository.GetAll() on o.UserId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         join o3 in this._eccpDictTempWorkOrderTypeRepository.GetAll() on o.TempWorkOrderTypeId equals
                             o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         select new
                         {
                             EccpMaintenanceTempWorkOrder = o,
                             ECCPBaseMaintenanceCompanyName = s1 == null ? string.Empty : s1.Name,
                             TempOrderTypeName = s3 == null ? string.Empty : s3.Name,
                             UserName = s2 == null ? string.Empty : s2.Name,
                             EccpBaseElevatorName = s4 == null ? string.Empty : s4.Name
                         }).WhereIf(
                !string.IsNullOrWhiteSpace(input.ECCPBaseMaintenanceCompanyNameFilter),
                e => e.ECCPBaseMaintenanceCompanyName.ToLower()
                     == input.ECCPBaseMaintenanceCompanyNameFilter.ToLower().Trim()).WhereIf(
                !string.IsNullOrWhiteSpace(input.UserNameFilter),
                e => e.UserName.ToLower() == input.UserNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var eccpMaintenanceTempWorkOrders = new List<GetEccpMaintenanceTempWorkOrderForView>();

            query.OrderBy(input.Sorting ?? "eccpMaintenanceTempWorkOrder.id asc").PageBy(input)
                .MapTo(eccpMaintenanceTempWorkOrders);

            return new PagedResultDto<GetEccpMaintenanceTempWorkOrderForView>(
                totalCount,
                eccpMaintenanceTempWorkOrders);
        }

        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders)]
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
                var filteredElevators = this._eccpBaseElevatorRepository.GetAll().WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.Contains(input.Filter));

                var query = from o in maintenanceContractList
                            join baseElevator in filteredElevators on o equals
                                baseElevator.Id
                            select new
                            {
                                baseElevator.Id,
                                baseElevator.Name
                            };

                var totalCount = await query.CountAsync();

                var lookupTableDtoList = new List<EccpBaseElevatorLookupTableDto>();
                var eccpBaseElevatorList = await query.PageBy(input).ToListAsync();

                foreach (var eccpBaseElevator in eccpBaseElevatorList)
                {
                    lookupTableDtoList.Add(
                        new EccpBaseElevatorLookupTableDto
                        {
                            Id = eccpBaseElevator.Id,
                            DisplayName = eccpBaseElevator.Name
                        });
                }
                return new PagedResultDto<EccpBaseElevatorLookupTableDto>(totalCount, lookupTableDtoList);
            }
        }


        /// <summary>
        /// The get all eccp dict temp work order type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders)]
        public async Task<PagedResultDto<ECCPDictTempWorkOrderTypeLookupTableDto>> GetAllECCPDictTempWorkOrderTypeForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpDictTempWorkOrderTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpDictTempWorkOrderTypeList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<ECCPDictTempWorkOrderTypeLookupTableDto>();
            foreach (var eccpDictTempWorkOrderType in eccpDictTempWorkOrderTypeList)
            {
                lookupTableDtoList.Add(
                    new ECCPDictTempWorkOrderTypeLookupTableDto
                    {
                        Id = eccpDictTempWorkOrderType.Id,
                        DisplayName = eccpDictTempWorkOrderType.Name
                    });
            }

            return new PagedResultDto<ECCPDictTempWorkOrderTypeLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all user for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders)]
        public async Task<PagedResultDto<UserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._userRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var userList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<UserLookupTableDto>();
            foreach (var user in userList)
            {
                lookupTableDtoList.Add(new UserLookupTableDto { Id = user.Id, DisplayName = user.Name });
            }

            return new PagedResultDto<UserLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get eccp maintenance temp work order.
        ///     app查询用户临时工单接口
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<GetEccpMaintenanceTempWorkOrderForAppView>> GetEccpMaintenanceTempWorkOrder(
            GetAllEccpMaintenanceTempWorkOrdersInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpMaintenanceTempWorkOrders =
                    this._eccpMaintenanceTempWorkOrderRepository.GetAll().OrderBy(e => e.Priority).ThenByDescending(e => e.CreationTime).Where(e => e.CheckState == 1 || e.CheckState == 2);
                var query = from o in filteredEccpMaintenanceTempWorkOrders
                            join o1 in this._eccpBaseMaintenanceCompanyRepository.GetAll() on o.MaintenanceCompanyId
                                equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()
                            where o.UserId == input.UserId && o.CompletionTime == null
                            select new GetEccpMaintenanceTempWorkOrderForAppView
                            {
                                Id = o.Id.ToString(),
                                Title = o.Title,
                                EccpDictTempWorkOrderTypesName = s1.Name,
                                CreationTime = o.CreationTime
                            };

                var totalCount = await query.CountAsync();

                var eccpMaintenanceWorkOrders = await query.PageBy(input).ToListAsync();
                return new PagedResultDto<GetEccpMaintenanceTempWorkOrderForAppView>(
                    totalCount,
                    eccpMaintenanceWorkOrders);
            }
        }

        /// <summary>
        /// The get eccp maintenance temp work order details.
        /// 临时工单详情
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<GetEccpMaintenanceTempWorkOrderDetails> GetEccpMaintenanceTempWorkOrderDetails(Guid id)
        {
            var eccpMaintenanceTempWorkOrder = await this._eccpMaintenanceTempWorkOrderRepository.GetAll()
                                                   .Include(e => e.MaintenanceCompany).Include(e => e.TempWorkOrderType)
                                                   .Select(
                                                       s => new GetEccpMaintenanceTempWorkOrderDetails
                                                       {
                                                           Id = s.Id,
                                                           CheckState = s.CheckState,
                                                           Title = s.Title,
                                                           Describe = s.Describe,
                                                           Priority = s.Priority,
                                                           CompletionTime = s.CompletionTime,
                                                           CreationTime = s.CreationTime,
                                                           MaintenanceCompanyNmae = s.MaintenanceCompany.Name
                                                       }).FirstOrDefaultAsync(w => w.Id == id);

            if (eccpMaintenanceTempWorkOrder == null)
            {
                return null;
            }

            var eccpMaintenanceTempWorkOrderActionLogs = await this._eccpMaintenanceTempWorkOrderActionLogRepository
                                                             .GetAll()
                                                             .Where(
                                                                 w => w.TempWorkOrderId
                                                                      == eccpMaintenanceTempWorkOrder.Id).Select(
                                                                 s => new MaintenanceTempWorkOrderActionLog
                                                                 {
                                                                     Id = s.Id,
                                                                     Remarks = s.Remarks,
                                                                     UsreName = s.User.Name,
                                                                     CreationTime = s.CreationTime
                                                                 }).ToListAsync();
            eccpMaintenanceTempWorkOrder.MaintenanceTempWorkOrderActionLogs = eccpMaintenanceTempWorkOrderActionLogs;

            var user = new User();
            if (eccpMaintenanceTempWorkOrder.CreatorUserId != null)
            {
                user = await this._userRepository.FirstOrDefaultAsync(eccpMaintenanceTempWorkOrder.CreatorUserId.Value);
            }

            if (user != null)
            {
                eccpMaintenanceTempWorkOrder.CreatorUserId = user.Id;
                eccpMaintenanceTempWorkOrder.CreatorUserName = user.Name;
            }

            return eccpMaintenanceTempWorkOrder;

            // todo 重写单元测
        }

        /// <summary>
        /// The get eccp maintenance temp work order for details.
        ///     维保工单详情
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit)]
        public async Task<GetEccpMaintenanceTempWorkOrderForDetails> GetEccpMaintenanceTempWorkOrderForDetails(
            EntityDto<Guid> input)
        {
            var eccpMaintenanceTempWorkOrder = await this._eccpMaintenanceTempWorkOrderRepository.GetAll()
                                                   .Include("User").Include("MaintenanceCompany")
                                                   .Include("TempWorkOrderType").Where(w => w.Id == input.Id).Select(
                                                       s => new GetEccpMaintenanceTempWorkOrderForDetails
                                                       {
                                                           Id = s.Id,
                                                           Title = s.Title,
                                                           Describe = s.Describe,
                                                           CheckState = s.CheckState,
                                                           Priority = s.Priority,
                                                           UsreName = s.User.Name,
                                                           CreationTime = s.CreationTime,
                                                           MaintenanceCompanyNmae = s.MaintenanceCompany.Name,
                                                           CompletionTime = s.CompletionTime,
                                                           TempWorkOrderTypeName = s.TempWorkOrderType.Name,
                                                           CreatorUserId = s.CreatorUserId
                                                       }).FirstOrDefaultAsync();
            if (eccpMaintenanceTempWorkOrder == null)
            {
                return null;
            }

            var eccpMaintenanceTempWorkOrderActionLog = await this._eccpMaintenanceTempWorkOrderActionLogRepository
                                                            .GetAll().OrderByDescending(o => o.CreationTime)
                                                            .FirstOrDefaultAsync(
                                                                w => w.TempWorkOrderId
                                                                     == eccpMaintenanceTempWorkOrder.Id);
            if (eccpMaintenanceTempWorkOrderActionLog != null)
            {
                eccpMaintenanceTempWorkOrder.Remarks = eccpMaintenanceTempWorkOrderActionLog.Remarks;
            }

            var user = await this._userRepository.FirstOrDefaultAsync(
                           w => w.Id == eccpMaintenanceTempWorkOrder.CreatorUserId);
            if (user != null)
            {
                eccpMaintenanceTempWorkOrder.CreatorUserName = user.UserName;
                eccpMaintenanceTempWorkOrder.CreatorUserName = user.PhoneNumber;
            }

            if (eccpMaintenanceTempWorkOrder.CheckState == 1)
            {
                eccpMaintenanceTempWorkOrder.CheckStateName = "待处理";
            }
            else if (eccpMaintenanceTempWorkOrder.CheckState == 2)
            {
                eccpMaintenanceTempWorkOrder.CheckStateName = "跟进中";
            }
            else if (eccpMaintenanceTempWorkOrder.CheckState == 3)
            {
                eccpMaintenanceTempWorkOrder.CheckStateName = "已完成";
            }

            return eccpMaintenanceTempWorkOrder;
        }

        /// <summary>
        /// The get eccp maintenance temp work order for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit)]
        public async Task<GetEccpMaintenanceTempWorkOrderForEditOutput> GetEccpMaintenanceTempWorkOrderForEdit(
            EntityDto<Guid> input)
        {
            var eccpMaintenanceTempWorkOrder =
                await this._eccpMaintenanceTempWorkOrderRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpMaintenanceTempWorkOrderForEditOutput
            {
                EccpMaintenanceTempWorkOrder =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenanceTempWorkOrderDto>(
                                         eccpMaintenanceTempWorkOrder)
            };
            var eccpDictTempWorkOrderType =
                await this._eccpDictTempWorkOrderTypeRepository.FirstOrDefaultAsync(
                    output.EccpMaintenanceTempWorkOrder.TempWorkOrderTypeId);
            output.EccpDictTempWorkOrderTypeName = eccpDictTempWorkOrderType.Name;

            var eccpBaseElevator =
                await this._eccpBaseElevatorRepository.FirstOrDefaultAsync(
                    output.EccpMaintenanceTempWorkOrder.ElevatorId);
            output.EccpBaseElevatorName = eccpBaseElevator != null ? eccpBaseElevator.Name : string.Empty;

            if (output.EccpMaintenanceTempWorkOrder.UserId != null)
            {
                var user = await this._userRepository.FirstOrDefaultAsync(
                               (long)output.EccpMaintenanceTempWorkOrder.UserId);
                output.UserName = user.Name;
            }

            return output;
        }

        /// <summary>
        /// The get eccp maintenance temp work orders to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetEccpMaintenanceTempWorkOrdersToExcel(
            GetAllEccpMaintenanceTempWorkOrdersForExcelInput input)
        {
            var filteredEccpMaintenanceTempWorkOrders = this._eccpMaintenanceTempWorkOrderRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Title.Contains(input.Filter) || e.Describe.Contains(input.Filter))
                .WhereIf(input.MinCheckStateFilter != null, e => e.CheckState >= input.MinCheckStateFilter)
                .WhereIf(input.MaxCheckStateFilter != null, e => e.CheckState <= input.MaxCheckStateFilter).WhereIf(
                    input.MinCompletionTimeFilter != null,
                    e => e.CompletionTime >= input.MinCompletionTimeFilter).WhereIf(
                    input.MaxCompletionTimeFilter != null,
                    e => e.CompletionTime <= input.MaxCompletionTimeFilter);

            var query = (from o in filteredEccpMaintenanceTempWorkOrders
                         join o1 in this._eccpBaseMaintenanceCompanyRepository.GetAll() on o.MaintenanceCompanyId equals
                             o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         join o2 in this._userRepository.GetAll() on o.UserId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         select new GetEccpMaintenanceTempWorkOrderForView
                         {
                             EccpMaintenanceTempWorkOrder =
                                            this.ObjectMapper.Map<EccpMaintenanceTempWorkOrderDto>(o),
                             ECCPBaseMaintenanceCompanyName = s1 == null ? string.Empty : s1.Name,
                             UserName = s2 == null ? string.Empty : s2.Name
                         }).WhereIf(
                !string.IsNullOrWhiteSpace(input.ECCPBaseMaintenanceCompanyNameFilter),
                e => e.ECCPBaseMaintenanceCompanyName.ToLower()
                     == input.ECCPBaseMaintenanceCompanyNameFilter.ToLower().Trim()).WhereIf(
                !string.IsNullOrWhiteSpace(input.UserNameFilter),
                e => e.UserName.ToLower() == input.UserNameFilter.ToLower().Trim());

            var eccpMaintenanceTempWorkOrderListDtos = await query.ToListAsync();

            return this._eccpMaintenanceTempWorkOrdersExcelExporter.ExportToFile(eccpMaintenanceTempWorkOrderListDtos);
        }

        /// <summary>
        /// The management eccp maintenance temp work order.
        ///     临时工单处理
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit)]
        public async Task ManagementEccpMaintenanceTempWorkOrder(HandleEccpMaintenanceTempWorkOrderDto input)
        {
            if (this.AbpSession.UserId == null)
            {
                throw new Exception(this.L("UserIdCanNotBeEmpty"));
            }

            var eccpMaintenanceTempWorkOrder =
                this._eccpMaintenanceTempWorkOrderRepository.FirstOrDefault(w => w.Id == input.Id);
            if (eccpMaintenanceTempWorkOrder == null)
            {
                throw new Exception(this.L("eccpMaintenanceTempWorkOrderNotBeNULL"));
            }

            if (input.CheckState == 0)
            {
                throw new Exception(this.L("CheckStateNotBe0"));
            }

            var eccpMaintenanceTempWorkOrderActionLog = new EccpMaintenanceTempWorkOrderActionLog
            {
                CheckState = input.CheckState,
                Remarks = input.Remarks,
                UserId = this.AbpSession.UserId.Value,
                TempWorkOrderId = eccpMaintenanceTempWorkOrder.Id
            };
            await this._eccpMaintenanceTempWorkOrderActionLogRepository.InsertAsync(
                eccpMaintenanceTempWorkOrderActionLog);
            eccpMaintenanceTempWorkOrder.CheckState = input.CheckState;
            if (input.CheckState == 3)
            {
                eccpMaintenanceTempWorkOrder.CompletionTime = DateTime.Now;
            }

            await this._eccpMaintenanceTempWorkOrderRepository.UpdateAsync(eccpMaintenanceTempWorkOrder);
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
        /// </exception>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create)]
        private async Task Create(CreateOrEditEccpMaintenanceTempWorkOrderDto input)
        {
            var eccpMaintenanceTempWorkOrder = this.ObjectMapper.Map<EccpMaintenanceTempWorkOrder>(input);

            if (this.AbpSession.TenantId == null)
            {
                throw new Exception(this.L("TenantIdCanNotBeEmpty"));
            }

            eccpMaintenanceTempWorkOrder.TenantId = (int)this.AbpSession.TenantId;

            var eccpBaseMaintenanceCompany =
                this._eccpBaseMaintenanceCompanyRepository.FirstOrDefault(
                    w => w.TenantId == eccpMaintenanceTempWorkOrder.TenantId);
            if (eccpBaseMaintenanceCompany == null)
            {
                throw new Exception(this.L("TenantIdError"));
            }

            eccpMaintenanceTempWorkOrder.MaintenanceCompanyId = eccpBaseMaintenanceCompany.Id;
            eccpMaintenanceTempWorkOrder.CheckState = 1;
            await this._eccpMaintenanceTempWorkOrderRepository.InsertAsync(eccpMaintenanceTempWorkOrder);
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit)]
        private async Task Update(CreateOrEditEccpMaintenanceTempWorkOrderDto input)
        {
            if (input.Id != null)
            {
                var eccpMaintenanceTempWorkOrder =
                    await this._eccpMaintenanceTempWorkOrderRepository.FirstOrDefaultAsync((Guid)input.Id);

                eccpMaintenanceTempWorkOrder.TempWorkOrderTypeId = input.TempWorkOrderTypeId;
                eccpMaintenanceTempWorkOrder.UserId = input.UserId;
                eccpMaintenanceTempWorkOrder.Title = input.Title;
                eccpMaintenanceTempWorkOrder.Describe = input.Describe;
                eccpMaintenanceTempWorkOrder.Priority = input.Priority;
                eccpMaintenanceTempWorkOrder.ElevatorId = input.ElevatorId;
            }
        }
    }
}