// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrdersAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json.Linq;
using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits;
using Sinodom.ElevatorCloud.EccpBaseElevatorBrands;
using Sinodom.ElevatorCloud.EccpBaseElevatorLabels;
using Sinodom.ElevatorCloud.EccpBaseElevatorModels;
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders
{
    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.AutoMapper;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Linq.Extensions;
    using Microsoft.EntityFrameworkCore;
    using NPOI.XWPF.UserModel;
    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpDict.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceTransfers;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Exporting;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks;
    using Sinodom.ElevatorCloud.Editions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using GetAllForLookupTableInput = Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos.GetAllForLookupTableInput;

    /// <summary>
    ///     The eccp maintenance work orders app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders)]
    public class EccpMaintenanceWorkOrdersAppService : ElevatorCloudAppServiceBase, IEccpMaintenanceWorkOrdersAppService
    {
        /// <summary>
        ///     The _ccp base elevators repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorsRepository;

        /// <summary>
        ///     The _eccp dict maintenance item repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceItem> _eccpDictMaintenanceItemRepository;

        /// <summary>
        ///     The _eccp dict maintenance status repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceStatus, int> _eccpDictMaintenanceStatusRepository;

        /// <summary>
        ///     The _eccp dict maintenance type repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceType, int> _eccpDictMaintenanceTypeRepository;

        /// <summary>
        ///     The _eccp edition repository.
        /// </summary>
        private readonly IRepository<ECCPEdition> _eccpEditionRepository;

        /// <summary>
        ///     The _e ccp editions type repository.
        /// </summary>
        private readonly IRepository<ECCPEditionsType> _eccpEditionsTypeRepository;

        /// <summary>
        ///     The _eccp maintenance contract elevator link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract_Elevator_Link, long> _eccpMaintenanceContractElevatorLinkRepository;

        /// <summary>
        ///     The _eccp maintenance contract repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract, long> _eccpMaintenanceContractRepository;

        /// <summary>
        ///     The _eccp maintenance plan maintenance user link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> _eccpMaintenancePlanMaintenanceUserLinkRepository;

        /// <summary>
        ///     The _eccp maintenance plan property user link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan_PropertyUser_Link, long> _eccpMaintenancePlanPropertyUserLinkRepository;

        /// <summary>
        ///     The _eccp maintenance plan repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan, int> _eccpMaintenancePlanRepository;

        /// <summary>
        ///     The _eccp maintenance plan template link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan_Template_Link, long> _eccpMaintenancePlanTemplateLinkRepository;

        /// <summary>
        ///     The _eccp maintenance template node dict maintenance item link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTemplateNode_DictMaintenanceItem_Link, long> _eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository;

        /// <summary>
        ///     The _eccp maintenance template node repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTemplateNode> _eccpMaintenanceTemplateNodeRepository;

        /// <summary>
        ///     The _eccp maintenance template repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTemplate> _eccpMaintenanceTemplateRepository;

        /// <summary>
        ///     The _eccp maintenance temp work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTempWorkOrder, Guid> _eccpMaintenanceTempWorkOrderRepository;

        /// <summary>
        ///     The _eccp maintenance work flow_ item_ links.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkFlow_Item_Link, Guid> _eccpMaintenanceWorkFlowItemLinks;

        /// <summary>
        ///     The _eccp maintenance work flow repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkFlow, Guid> _eccpMaintenanceWorkFlowRepository;

        /// <summary>
        ///     The _eccp maintenance work order evaluation repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrderEvaluation> _eccpMaintenanceWorkOrderEvaluationRepository;

        /// <summary>
        ///     The _eccp maintenance work order maintenance user link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder_MaintenanceUser_Link, long> _eccpMaintenanceWorkOrderMaintenanceUserLinkRepository;

        /// <summary>
        ///     The _eccp maintenance work order manager.
        /// </summary>
        private readonly EccpMaintenanceWorkOrderManager _eccpMaintenanceWorkOrderManager;

        /// <summary>
        ///     The _eccp maintenance work order property user link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder_PropertyUser_Link, long> _eccpMaintenanceWorkOrderPropertyUserLinkRepository;

        /// <summary>
        ///     The _eccp maintenance work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder> _eccpMaintenanceWorkOrderRepository;

        /// <summary>
        ///     The _eccp maintenance work orders excel exporter.
        /// </summary>
        private readonly IEccpMaintenanceWorkOrdersExcelExporter _eccpMaintenanceWorkOrdersExcelExporter;

        /// <summary>
        /// The _eccp maintenance work orders word exporter.
        /// </summary>
        private readonly IEccpMaintenanceWorkOrdersWordExporter _eccpMaintenanceWorkOrdersWordExporter;

        /// <summary>
        ///     The _eccp maintenance work order transfer audit log repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrderTransferAuditLog, int> _eccpMaintenanceWorkOrderTransferAuditLogRepository;

        /// <summary>
        ///     The _eccp maintenance work order transfer repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrderTransfer, int> _eccpMaintenanceWorkOrderTransferRepository;
        private readonly IRepository<EccpMaintenanceWork, int> _eccpEccpMaintenanceWorkRepository;
        private readonly IRepository<EccpMaintenanceWorkFlow, Guid> _eccpEccpMaintenanceWorkFlowRepository;

        /// <summary>
        ///     The _eccp maintenance work repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWork> _eccpMaintenanceWorkRepository;

        /// <summary>
        ///     The _user manager.
        /// </summary>
        private readonly UserManager _userManager;
        /// <summary>
        ///     The eccp base maintenance company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseMaintenanceCompany, int> _eccpBaseMaintenanceCompanyRepository;

        /// <summary>
        ///     The eccp base property company repository.
        /// </summary>
        private readonly IRepository<ECCPBasePropertyCompany, int> _eccpBasePropertyCompanyRepository;
        /// <summary>
        ///     The eccp base annual inspection unit repository.
        /// </summary>
        private readonly IRepository<ECCPBaseAnnualInspectionUnit, long> _eccpBaseAnnualInspectionUnitRepository;
        /// <summary>
        ///     The eccp base elevator brand repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevatorBrand, int> _eccpBaseElevatorBrandRepository;

        /// <summary>
        ///     The eccp base elevator model repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevatorModel, int> _eccpBaseElevatorModelRepository;
        /// <summary>
        ///     The eccp dict elevator type repository.
        /// </summary>
        private readonly IRepository<EccpDictElevatorType, int> _eccpDictElevatorTypeRepository;

        /// <summary>
        ///     The eccp dict place type repository.
        /// </summary>
        private readonly IRepository<EccpDictPlaceType, int> _eccpDictPlaceTypeRepository;

        /// <summary>
        ///     The eccp base elevator label repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevatorLabel, long> _eccpBaseElevatorLabelRepository;
        private readonly IRepository<EccpDictNodeType, int> _eccpDictNodeTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkOrdersAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceWorkOrderRepository">
        /// The eccp maintenance work order repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrdersExcelExporter">
        /// The eccp maintenance work orders excel exporter.
        /// </param>
        /// <param name="eccpMaintenancePlanRepository">
        /// The eccp maintenance plan repository.
        /// </param>
        /// <param name="eccpDictMaintenanceTypeRepository">
        /// The eccp dict maintenance type repository.
        /// </param>
        /// <param name="eccpDictMaintenanceStatusRepository">
        /// The eccp dict maintenance status repository.
        /// </param>
        /// <param name="ccpBaseElevatorsRepository">
        /// The ccp base elevators repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderManager">
        /// The eccp maintenance work order manager.
        /// </param>
        /// <param name="eccpMaintenancePlanMaintenanceUserLinkRepository">
        /// The eccp maintenance plan_ maintenance user link repository.
        /// </param>
        /// <param name="eccpMaintenanceTempWorkOrderRepository">
        /// The eccp maintenance temp work order repository.
        /// </param>
        /// <param name="eccpMaintenancePlanPropertyUserLinkRepository">
        /// The eccp maintenance plan_ property user link repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderTransferRepository">
        /// The eccp maintenance work order transfer repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderTransferAuditLogRepository">
        /// The eccp maintenance work order transfer audit log repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderEvaluationRepository">
        /// The eccp maintenance work order evaluation repository.
        /// </param>
        /// <param name="eccpMaintenancePlanTemplateLinkRepository">
        /// The eccp maintenance plan_ template_ link repository.
        /// </param>
        /// <param name="eccpMaintenanceTemplateRepository">
        /// The eccp maintenance template repository.
        /// </param>
        /// <param name="eccpMaintenanceTemplateNodeRepository">
        /// The eccp maintenance template node repository.
        /// </param>
        /// <param name="eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository">
        /// The eccp maintenance template node_ dict maintenance item_ link repository.
        /// </param>
        /// <param name="eccpDictMaintenanceItemRepository">
        /// The eccp dict maintenance item repository.
        /// </param>
        /// <param name="eccpEditionRepository">
        /// The eccp edition repository.
        /// </param>
        /// <param name="eccpEditionsTypeRepository">
        /// The e ccp editions type repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderMaintenanceUserLinkRepository">
        /// The eccp maintenance work order_ maintenance user link repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderPropertyUserLinkRepository">
        /// The eccp maintenance work order_ property user link repository.
        /// </param>
        /// <param name="eccpMaintenanceContractElevatorLinkRepository">
        /// The eccp Maintenance Contract Elevator Link Repository.
        /// </param>
        /// <param name="eccpMaintenanceContractRepository">
        /// The eccp Maintenance Contract Repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkFlowItemLinks">
        /// The eccp Maintenance Work Flow Item Links.
        /// </param>
        /// <param name="eccpMaintenanceWorkFlowRepository">
        /// The eccp Maintenance Work Flow Repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkRepository">
        /// The eccp Maintenance Work Repository.
        /// </param>
        /// <param name="userManager">
        /// The user Manager.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrdersWordExporter">
        /// The eccp Maintenance Work Orders Word Exporter.
        /// </param>
        public EccpMaintenanceWorkOrdersAppService(
            IRepository<EccpMaintenanceWorkOrder> eccpMaintenanceWorkOrderRepository,
            IEccpMaintenanceWorkOrdersExcelExporter eccpMaintenanceWorkOrdersExcelExporter,
            IRepository<EccpMaintenancePlan, int> eccpMaintenancePlanRepository,
            IRepository<EccpDictMaintenanceType, int> eccpDictMaintenanceTypeRepository,
            IRepository<EccpDictMaintenanceStatus, int> eccpDictMaintenanceStatusRepository,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorsRepository,
            EccpMaintenanceWorkOrderManager eccpMaintenanceWorkOrderManager,
            IRepository<EccpMaintenancePlan_MaintenanceUser_Link, long> eccpMaintenancePlanMaintenanceUserLinkRepository,
            IRepository<EccpMaintenanceTempWorkOrder, Guid> eccpMaintenanceTempWorkOrderRepository,
            IRepository<EccpMaintenancePlan_PropertyUser_Link, long> eccpMaintenancePlanPropertyUserLinkRepository,
            IRepository<EccpMaintenanceWorkOrderTransfer, int> eccpMaintenanceWorkOrderTransferRepository,
            IRepository<EccpMaintenanceWorkOrderTransferAuditLog, int> eccpMaintenanceWorkOrderTransferAuditLogRepository,
            IRepository<EccpMaintenanceWorkOrderEvaluation> eccpMaintenanceWorkOrderEvaluationRepository,
            IRepository<EccpMaintenancePlan_Template_Link, long> eccpMaintenancePlanTemplateLinkRepository,
            IRepository<EccpMaintenanceTemplate> eccpMaintenanceTemplateRepository,
            IRepository<EccpMaintenanceTemplateNode> eccpMaintenanceTemplateNodeRepository,
            IRepository<EccpMaintenanceTemplateNode_DictMaintenanceItem_Link, long> eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository,
            IRepository<EccpDictMaintenanceItem> eccpDictMaintenanceItemRepository,
            IRepository<ECCPEdition> eccpEditionRepository,
            IRepository<ECCPEditionsType> eccpEditionsTypeRepository,
            IRepository<EccpMaintenanceWorkOrder_MaintenanceUser_Link, long> eccpMaintenanceWorkOrderMaintenanceUserLinkRepository,
            IRepository<EccpMaintenanceWorkOrder_PropertyUser_Link, long> eccpMaintenanceWorkOrderPropertyUserLinkRepository,
            IRepository<EccpMaintenanceContract_Elevator_Link, long> eccpMaintenanceContractElevatorLinkRepository,
            IRepository<EccpMaintenanceContract, long> eccpMaintenanceContractRepository,
            IRepository<EccpMaintenanceWork, int> eccpEccpMaintenanceWorkRepository,
            IRepository<EccpMaintenanceWorkFlow, Guid> eccpEccpMaintenanceWorkFlowRepository,
            IRepository<EccpMaintenanceWorkFlow_Item_Link, Guid> eccpMaintenanceWorkFlowItemLinks,
            IRepository<EccpMaintenanceWorkFlow, Guid> eccpMaintenanceWorkFlowRepository,
            IRepository<EccpMaintenanceWork> eccpMaintenanceWorkRepository,
            UserManager userManager,
            IEccpMaintenanceWorkOrdersWordExporter eccpMaintenanceWorkOrdersWordExporter,
            IRepository<ECCPBaseMaintenanceCompany, int> eccpBaseMaintenanceCompanyRepository,
            IRepository<ECCPBasePropertyCompany, int> eccpBasePropertyCompanyRepository,
            IRepository<ECCPBaseAnnualInspectionUnit, long> eccpBaseAnnualInspectionUnitRepository,
            IRepository<EccpBaseElevatorBrand, int> eccpBaseElevatorBrandRepository,
            IRepository<EccpBaseElevatorModel, int> eccpBaseElevatorModelRepository,
            IRepository<EccpDictElevatorType, int> eccpDictElevatorTypeRepository,
            IRepository<EccpDictPlaceType, int> eccpDictPlaceTypeRepository,
            IRepository<EccpBaseElevatorLabel, long> eccpBaseElevatorLabelRepository,
            IRepository<EccpDictNodeType, int> eccpDictNodeTypeRepository)
        {
            this._eccpMaintenanceTempWorkOrderRepository = eccpMaintenanceTempWorkOrderRepository;
            this._eccpMaintenancePlanMaintenanceUserLinkRepository = eccpMaintenancePlanMaintenanceUserLinkRepository;
            this._eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
            this._eccpMaintenanceWorkOrdersExcelExporter = eccpMaintenanceWorkOrdersExcelExporter;
            this._eccpMaintenancePlanRepository = eccpMaintenancePlanRepository;
            this._eccpDictMaintenanceTypeRepository = eccpDictMaintenanceTypeRepository;
            this._eccpDictMaintenanceStatusRepository = eccpDictMaintenanceStatusRepository;
            this._eccpBaseElevatorsRepository = eccpBaseElevatorsRepository;
            this._eccpMaintenanceWorkOrderManager = eccpMaintenanceWorkOrderManager;
            this._eccpMaintenancePlanPropertyUserLinkRepository = eccpMaintenancePlanPropertyUserLinkRepository;
            this._eccpMaintenanceWorkOrderTransferRepository = eccpMaintenanceWorkOrderTransferRepository;
            this._eccpMaintenanceWorkOrderTransferAuditLogRepository =
                eccpMaintenanceWorkOrderTransferAuditLogRepository;
            this._eccpMaintenanceWorkOrderEvaluationRepository = eccpMaintenanceWorkOrderEvaluationRepository;
            this._eccpMaintenancePlanTemplateLinkRepository = eccpMaintenancePlanTemplateLinkRepository;
            this._eccpMaintenanceTemplateRepository = eccpMaintenanceTemplateRepository;
            this._eccpMaintenanceTemplateNodeRepository = eccpMaintenanceTemplateNodeRepository;
            this._eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository =
                eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository;
            this._eccpDictMaintenanceItemRepository = eccpDictMaintenanceItemRepository;
            this._eccpEditionRepository = eccpEditionRepository;
            this._eccpEditionsTypeRepository = eccpEditionsTypeRepository;
            this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository =
                eccpMaintenanceWorkOrderMaintenanceUserLinkRepository;
            this._eccpMaintenanceWorkOrderPropertyUserLinkRepository =
                eccpMaintenanceWorkOrderPropertyUserLinkRepository;
            _eccpMaintenanceContractElevatorLinkRepository = eccpMaintenanceContractElevatorLinkRepository;
            _eccpMaintenanceContractRepository = eccpMaintenanceContractRepository;
            _eccpEccpMaintenanceWorkRepository = eccpEccpMaintenanceWorkRepository;
            _eccpEccpMaintenanceWorkFlowRepository = eccpEccpMaintenanceWorkFlowRepository;
            this._eccpMaintenanceContractElevatorLinkRepository = eccpMaintenanceContractElevatorLinkRepository;
            this._eccpMaintenanceContractRepository = eccpMaintenanceContractRepository;
            this._eccpMaintenanceWorkFlowItemLinks = eccpMaintenanceWorkFlowItemLinks;
            this._eccpMaintenanceWorkFlowRepository = eccpMaintenanceWorkFlowRepository;
            this._eccpMaintenanceWorkRepository = eccpMaintenanceWorkRepository;
            this._userManager = userManager;
            this._eccpMaintenanceWorkOrdersWordExporter = eccpMaintenanceWorkOrdersWordExporter;
            this._eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
            this._eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
            this._eccpBaseAnnualInspectionUnitRepository = eccpBaseAnnualInspectionUnitRepository;
            this._eccpBaseElevatorBrandRepository = eccpBaseElevatorBrandRepository;
            this._eccpBaseElevatorModelRepository = eccpBaseElevatorModelRepository;
            this._eccpDictElevatorTypeRepository = eccpDictElevatorTypeRepository;
            this._eccpDictPlaceTypeRepository = eccpDictPlaceTypeRepository;
            this._eccpBaseElevatorLabelRepository = eccpBaseElevatorLabelRepository;
            this._eccpDictNodeTypeRepository = eccpDictNodeTypeRepository;
        }

        /// <summary>
        /// The close work order.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_CloseWorkOrder)]
        public async Task CloseWorkOrder(EntityDto input)
        {
            var eccpMaintenanceWorkOrder =
                await this._eccpMaintenanceWorkOrderRepository.FirstOrDefaultAsync(e => e.Id == input.Id);
            if (eccpMaintenanceWorkOrder != null)
            {
                if (!eccpMaintenanceWorkOrder.IsClosed)
                {
                    // 关闭工单
                    eccpMaintenanceWorkOrder.IsClosed = true;
                    await this._eccpMaintenanceWorkOrderRepository.UpdateAsync(eccpMaintenanceWorkOrder);

                    // 查询是否有工单转接申请，如果有修改工单转接申请，添加工单转接审批日志
                    var eccpMaintenanceWorkOrderTransfer =
                        await this._eccpMaintenanceWorkOrderTransferRepository.FirstOrDefaultAsync(
                            e => e.MaintenanceWorkOrderId == eccpMaintenanceWorkOrder.Id);
                    if (eccpMaintenanceWorkOrderTransfer != null && eccpMaintenanceWorkOrderTransfer.IsApproved == null)
                    {
                        eccpMaintenanceWorkOrderTransfer.IsApproved = false;
                        eccpMaintenanceWorkOrderTransfer.Remark = "工单被关闭";
                        await this._eccpMaintenanceWorkOrderTransferRepository.UpdateAsync(
                            eccpMaintenanceWorkOrderTransfer);

                        // 添加工单转接审批日志
                        var workOrderTransferAuditLog = new EccpMaintenanceWorkOrderTransferAuditLog
                        {
                            MaintenanceWorkOrderTransferId =
                                                                    eccpMaintenanceWorkOrderTransfer.Id,
                            IsApproved = false,
                            Remark = "工单被关闭"
                        };
                        await this._eccpMaintenanceWorkOrderTransferAuditLogRepository.InsertAsync(
                            workOrderTransferAuditLog);
                    }
                }
            }
        }

        /// <summary>
        /// The completion work rearrange work order.
        /// 工作完成计划工单刷新
        /// </summary>
        /// <param name="eccpMaintenanceWorkOrderId">
        /// The eccp maintenance work order id.
        /// 任务id
        /// </param>
        /// <param name="completionTime">
        /// The completion time.
        /// 任务完成时间
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks)]
        public int CompletionWorkRearrangeWorkOrder(int eccpMaintenanceWorkOrderId, DateTime completionTime)
        {
            var resultCount =
                this._eccpMaintenanceWorkOrderManager.CompletionWorkRearrangeWorkOrder(
                    eccpMaintenanceWorkOrderId,
                    completionTime);
            return resultCount;
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
        public async Task CreateOrEdit(CreateOrEditEccpMaintenanceWorkOrderDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpMaintenanceWorkOrderRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpMaintenanceWorkOrderForView>> GetAll(
            GetAllEccpMaintenanceWorkOrdersInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                    .Where(w => w.TenantId == this.AbpSession.TenantId)
                    //.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Remark.Contains(input.Filter))
                    .WhereIf(input.IsPassedFilter > -1, e => Convert.ToInt32(e.IsPassed) == input.IsPassedFilter)
                    .WhereIf(input.MinLongitudeFilter != null, e => e.Longitude >= input.MinLongitudeFilter)
                    .WhereIf(input.MaxLongitudeFilter != null, e => e.Longitude <= input.MaxLongitudeFilter)
                    .WhereIf(input.MinLatitudeFilter != null, e => e.Latitude >= input.MinLatitudeFilter)
                    .WhereIf(input.MaxLatitudeFilter != null, e => e.Latitude <= input.MaxLatitudeFilter)
                    .WhereIf(input.MinPlanCheckDateFilter != null, e => e.PlanCheckDate >= input.MinPlanCheckDateFilter)
                    .WhereIf(input.MaxPlanCheckDateFilter != null, e => e.PlanCheckDate <= input.MaxPlanCheckDateFilter)
                    .WhereIf(input.IsClosedFilter > -1, e => Convert.ToInt32(e.IsClosed) == input.IsClosedFilter);
                var eccpMaintenanceWorkOrderMaintenanceUserLinks = this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository
                    .GetAll().Where(w => w.TenantId == this.AbpSession.TenantId);
                //var eccpMaintenancePlanPropertyUserLinks = this._eccpMaintenancePlanPropertyUserLinkRepository.GetAll()
                //    .Where(w => w.TenantId == this.AbpSession.TenantId);
                var users = this.UserManager.Users;

                var eccpMaintenanceWorkOrderMaintenanceUsers =
                    from eccpMaintenanceWorkOrderMaintenanceUserLink in eccpMaintenanceWorkOrderMaintenanceUserLinks
                    join user in users on eccpMaintenanceWorkOrderMaintenanceUserLink.UserId equals user.Id
                    select new { eccpMaintenanceWorkOrderMaintenanceUserLink.MaintenancePlanId, user.Name };

                //var eccpMaintenancePlanPropertyUsers =
                //    from eccpMaintenancePlanPropertyUserLink in eccpMaintenancePlanPropertyUserLinks
                //    join user in users on eccpMaintenancePlanPropertyUserLink.UserId equals user.Id
                //    select new { eccpMaintenancePlanPropertyUserLink.MaintenancePlanId, user.UserName };

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
                     join o4 in eccpMaintenanceWorkOrderMaintenanceUsers on o.Id equals o4.MaintenancePlanId
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
                        e => e.EccpElevatorName.ToLower() == input.EccpElevatorNameFilter.ToLower().Trim());

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

                var totalCount = workOrderQuery.Count();

                var eccpMaintenanceWorkOrders = new List<GetEccpMaintenanceWorkOrderForView>();

                workOrderQuery.OrderBy(input.Sorting ?? "eccpMaintenanceWorkOrder.id asc").PageBy(input)
                    .MapTo(eccpMaintenanceWorkOrders);

                return new PagedResultDto<GetEccpMaintenanceWorkOrderForView>(totalCount, eccpMaintenanceWorkOrders);
            }
        }


        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators_ViewMaintenanceWorkOrders)]
        public async Task<PagedResultDto<GetEccpMaintenanceWorkOrderForView>> GetAllByElevatorId(
          GetAllByElevatorIdEccpMaintenanceWorkOrdersInput input)
        {
            if (string.IsNullOrWhiteSpace(input.ElevatorIdFilter))
            {
                return new PagedResultDto<GetEccpMaintenanceWorkOrderForView>();
            }

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll().Where(w => w.TenantId == this.AbpSession.TenantId);

                var eccpMaintenanceWorkOrderMaintenanceUserLinks = this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository
                    .GetAll().Where(w => w.TenantId == this.AbpSession.TenantId);

                //var eccpMaintenancePlanPropertyUserLinks = this._eccpMaintenancePlanPropertyUserLinkRepository.GetAll()
                //    .Where(w => w.TenantId == this.AbpSession.TenantId);

                var users = this.UserManager.Users;

                var eccpMaintenanceWorkOrderMaintenanceUsers =
                    from eccpMaintenanceWorkOrderMaintenanceUserLink in eccpMaintenanceWorkOrderMaintenanceUserLinks
                    join user in users on eccpMaintenanceWorkOrderMaintenanceUserLink.UserId equals user.Id
                    select new { eccpMaintenanceWorkOrderMaintenanceUserLink.MaintenancePlanId, user.Name };

                //var eccpMaintenancePlanPropertyUsers =
                //    from eccpMaintenancePlanPropertyUserLink in eccpMaintenancePlanPropertyUserLinks
                //    join user in users on eccpMaintenancePlanPropertyUserLink.UserId equals user.Id
                //    select new { eccpMaintenancePlanPropertyUserLink.MaintenancePlanId, user.UserName };

                var query =
                    (from o in filteredEccpMaintenanceWorkOrders
                     join o1 in this._eccpMaintenancePlanRepository.GetAll().Where(p => p.ElevatorId.ToString() == input.ElevatorIdFilter)
                     on o.MaintenancePlanId equals o1.Id
                     join o2 in this._eccpDictMaintenanceTypeRepository.GetAll() on o.MaintenanceTypeId equals o2.Id
                         into j2
                     from s2 in j2.DefaultIfEmpty()
                     join o3 in this._eccpDictMaintenanceStatusRepository.GetAll() on o.MaintenanceStatusId equals o3.Id
                         into j3
                     from s3 in j3.DefaultIfEmpty()
                     join o4 in eccpMaintenanceWorkOrderMaintenanceUsers on o.Id equals o4.MaintenancePlanId
                         into j4
                     from s4 in j4.DefaultIfEmpty()
                         //join o5 in eccpMaintenancePlanPropertyUsers on o.MaintenancePlanId equals o5.MaintenancePlanId into
                         //    j5
                         //from s5 in j5.DefaultIfEmpty()
                     select new
                     {
                         EccpMaintenanceWorkOrder = o,
                         EccpMaintenancePlanPollingPeriod = o1.PollingPeriod.ToString(),
                         EccpDictMaintenanceTypeName = s2 == null ? string.Empty : s2.Name,
                         EccpDictMaintenanceStatusName = s3 == null ? string.Empty : s3.Name,
                         EccpElevatorName = o1.Elevator.Name,
                         EccpMaintenanceUserName = s4 == null ? string.Empty : s4.Name
                     })
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.EccpDictMaintenanceTypeName.ToLower().Contains(input.Filter.ToLower().Trim())
                        || e.EccpDictMaintenanceStatusName.ToLower().Contains(input.Filter.ToLower().Trim()));

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
                        });

                var totalCount = workOrderQuery.Count();

                var eccpMaintenanceWorkOrders = new List<GetEccpMaintenanceWorkOrderForView>();

                workOrderQuery.OrderBy(input.Sorting ?? "eccpMaintenanceWorkOrder.planCheckDate desc").PageBy(input)
                    .MapTo(eccpMaintenanceWorkOrders);

                return new PagedResultDto<GetEccpMaintenanceWorkOrderForView>(totalCount, eccpMaintenanceWorkOrders);
            }
        }

        /// <summary>
        /// The get all completed work order.
        /// 查询已完成工单
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<GetEccpMaintenanceWorkOrderForView>> GetAllCompletedWorkOrder(
            GetAllEccpMaintenanceWorkOrdersInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                    .Where(w => w.TenantId == this.AbpSession.TenantId)
                    //.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Remark.Contains(input.Filter))
                    .WhereIf(input.IsPassedFilter > -1, e => Convert.ToInt32(e.IsPassed) == input.IsPassedFilter)
                    .WhereIf(input.MinLongitudeFilter != null, e => e.Longitude >= input.MinLongitudeFilter)
                    .WhereIf(input.MaxLongitudeFilter != null, e => e.Longitude <= input.MaxLongitudeFilter)
                    .WhereIf(input.MinLatitudeFilter != null, e => e.Latitude >= input.MinLatitudeFilter)
                    .WhereIf(input.MaxLatitudeFilter != null, e => e.Latitude <= input.MaxLatitudeFilter)
                    .WhereIf(input.MinPlanCheckDateFilter != null, e => e.PlanCheckDate >= input.MinPlanCheckDateFilter)
                    .WhereIf(input.MaxPlanCheckDateFilter != null, e => e.PlanCheckDate <= input.MaxPlanCheckDateFilter)
                    .WhereIf(input.IsClosedFilter > -1, e => Convert.ToInt32(e.IsClosed) == input.IsClosedFilter);
                var eccpMaintenanceWorkOrderMaintenanceUserLinks = this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository
                    .GetAll().Where(w => w.TenantId == this.AbpSession.TenantId);
                //var eccpMaintenancePlanPropertyUserLinks = this._eccpMaintenancePlanPropertyUserLinkRepository.GetAll()
                //    .Where(w => w.TenantId == this.AbpSession.TenantId);
                var users = this.UserManager.Users;

                var eccpMaintenanceWorkOrderMaintenanceUsers =
                    from eccpMaintenanceWorkOrderMaintenanceUserLink in eccpMaintenanceWorkOrderMaintenanceUserLinks
                    join user in users on eccpMaintenanceWorkOrderMaintenanceUserLink.UserId equals user.Id
                    select new { eccpMaintenanceWorkOrderMaintenanceUserLink.MaintenancePlanId, user.Name };

                //var eccpMaintenancePlanPropertyUsers =
                //    from eccpMaintenancePlanPropertyUserLink in eccpMaintenancePlanPropertyUserLinks
                //    join user in users on eccpMaintenancePlanPropertyUserLink.UserId equals user.Id
                //    select new { eccpMaintenancePlanPropertyUserLink.MaintenancePlanId, user.Name };

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
                     join o4 in eccpMaintenanceWorkOrderMaintenanceUsers on o.Id equals o4.MaintenancePlanId
                         into j4
                     from s4 in j4.DefaultIfEmpty()
                         //join o5 in eccpMaintenancePlanPropertyUsers on o.MaintenancePlanId equals o5.MaintenancePlanId into
                         //    j5
                         //from s5 in j5.DefaultIfEmpty()
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
                             == input.EccpMaintenancePlanPollingPeriodFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpDictMaintenanceTypeNameFilter),
                        e => e.EccpDictMaintenanceTypeName.ToLower()
                             == input.EccpDictMaintenanceTypeNameFilter.ToLower().Trim())
                    .Where(e => e.EccpDictMaintenanceStatusName.ToLower() == "已完成").WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpElevatorNameFilter),
                        e => e.EccpElevatorName.ToLower() == input.EccpElevatorNameFilter.ToLower().Trim());

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

                var totalCount = workOrderQuery.Count();

                var eccpMaintenanceWorkOrders = new List<GetEccpMaintenanceWorkOrderForView>();

                workOrderQuery.OrderBy(input.Sorting ?? "eccpMaintenanceWorkOrder.id asc").PageBy(input)
                    .MapTo(eccpMaintenanceWorkOrders);

                return new PagedResultDto<GetEccpMaintenanceWorkOrderForView>(totalCount, eccpMaintenanceWorkOrders);
            }
        }

        /// <summary>
        /// The get all eccp dict maintenance status for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders)]
        public async Task<PagedResultDto<EccpDictMaintenanceStatusLookupTableDto>> GetAllEccpDictMaintenanceStatusForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpDictMaintenanceStatusRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpDictMaintenanceStatusList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpDictMaintenanceStatusLookupTableDto>();
            foreach (var eccpDictMaintenanceStatus in eccpDictMaintenanceStatusList)
            {
                lookupTableDtoList.Add(
                    new EccpDictMaintenanceStatusLookupTableDto
                    {
                        Id = eccpDictMaintenanceStatus.Id,
                        DisplayName = eccpDictMaintenanceStatus.Name
                    });
            }

            return new PagedResultDto<EccpDictMaintenanceStatusLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp dict maintenance type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders)]
        public async Task<PagedResultDto<EccpDictMaintenanceTypeLookupTableDto>> GetAllEccpDictMaintenanceTypeForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpDictMaintenanceTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpDictMaintenanceTypeList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpDictMaintenanceTypeLookupTableDto>();
            foreach (var eccpDictMaintenanceType in eccpDictMaintenanceTypeList)
            {
                lookupTableDtoList.Add(
                    new EccpDictMaintenanceTypeLookupTableDto
                    {
                        Id = eccpDictMaintenanceType.Id,
                        DisplayName = eccpDictMaintenanceType.Name
                    });
            }

            return new PagedResultDto<EccpDictMaintenanceTypeLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp maintenance plan for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders)]
        public async Task<PagedResultDto<EccpMaintenancePlanLookupTableDto>> GetAllEccpMaintenancePlanForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpMaintenancePlanRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.PollingPeriod.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpMaintenancePlanList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpMaintenancePlanLookupTableDto>();
            foreach (var eccpMaintenancePlan in eccpMaintenancePlanList)
            {
                lookupTableDtoList.Add(
                    new EccpMaintenancePlanLookupTableDto
                    {
                        Id = eccpMaintenancePlan.Id,
                        DisplayName = eccpMaintenancePlan.PollingPeriod.ToString()
                    });
            }

            return new PagedResultDto<EccpMaintenancePlanLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all pending processing.
        /// 查询待办工单（进行中和待维保）//todo 需要补充单元测试
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<GetEccpMaintenanceUsersWorkOrder>> GetAllPendingProcessing(
            GetAllEccpMaintenanceWorkOrdersInput input)
        {
            var filteredEccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                .Where(e => e.MaintenanceStatus.Name == "进行中" || e.MaintenanceStatus.Name == "未进行");
            var eccpMaintenanceWorkOrderMaintenanceUserLinks = this
                ._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository.GetAll()
                .Where(w => w.UserId == this.AbpSession.UserId);
            var ccpBaseElevators = this._eccpBaseElevatorsRepository.GetAll();

            var query = from o in filteredEccpMaintenanceWorkOrders
                        join o1 in eccpMaintenanceWorkOrderMaintenanceUserLinks on o.Id equals o1.MaintenancePlanId
                        join o2 in ccpBaseElevators on o.MaintenancePlan.ElevatorId equals o2.Id
                        select new GetEccpMaintenanceUsersWorkOrder
                        {
                            Id = o.Id,
                            EccpDictMaintenanceTypeName = o.MaintenanceType.Name,
                            EccpDictMaintenanceStatusName = o.MaintenanceStatus.Name,
                            EccpElevatorInstallationAddress = o2.InstallationAddress,
                            CertificateNum = o2.CertificateNum,
                            PlanCheckDate = o.PlanCheckDate
                        };

            var totalCount = await query.CountAsync();
            var eccpMaintenanceWorkOrders =
                await query.OrderBy(input.Sorting ?? "PlanCheckDate desc").PageBy(input).ToListAsync();
            return new PagedResultDto<GetEccpMaintenanceUsersWorkOrder>(totalCount, eccpMaintenanceWorkOrders);
        }

        /// <summary>
        /// The get all work order evaluation by work order id.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_ViewEvaluations)]
        public async Task<PagedResultDto<GetEccpMaintenanceWorkOrderEvaluationForView>> GetAllWorkOrderEvaluationByWorkOrderId(GetAllByWorkOrderIdEccpMaintenanceWorkOrderEvaluationsInput input)
        {
            var filteredEccpMaintenanceWorkOrderEvaluations = this._eccpMaintenanceWorkOrderEvaluationRepository
                .GetAll().Where(e => e.WorkOrderId == input.MaintenanceWorkOrderIdFilter);

            var query = from o in filteredEccpMaintenanceWorkOrderEvaluations
                        join o1 in this._eccpMaintenanceWorkOrderRepository.GetAll() on o.WorkOrderId equals o1.Id
                        select new
                        {
                            EccpMaintenanceWorkOrderEvaluation = o,
                            EccpDictMaintenanceTypeName =
                                           o1.MaintenanceType == null ? string.Empty : o1.MaintenanceType.Name,
                            EccpMaintenanceWorkOrderComplateDate = o1.ComplateDate
                        };

            var totalCount = await query.CountAsync();

            var eccpMaintenanceWorkOrderEvaluations = new List<GetEccpMaintenanceWorkOrderEvaluationForView>();

            query.OrderBy(input.Sorting ?? "eccpMaintenanceWorkOrderEvaluation.id asc").PageBy(input)
                .MapTo(eccpMaintenanceWorkOrderEvaluations);

            return new PagedResultDto<GetEccpMaintenanceWorkOrderEvaluationForView>(
                totalCount,
                eccpMaintenanceWorkOrderEvaluations);
        }

        /// <summary>
        /// The get app index data.
        /// 获取App首页数据接口
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<GetAppIndexDataDto> GetAppIndexData()
        {
            var appIndexDataDto = new GetAppIndexDataDto();

            if (AbpSession.TenantId == null)
            {
                return appIndexDataDto;
            }

            // 获取租户
            var tenant = await this.TenantManager.GetByIdAsync(AbpSession.TenantId.Value);

            if (tenant.EditionId == null)
            {
                return appIndexDataDto;
            }

            // 通过租户版本ID获取版本信息
            var edition = await this._eccpEditionRepository.FirstOrDefaultAsync(tenant.EditionId.Value);
            if (edition.ECCPEditionsTypeId == null)
            {
                return appIndexDataDto;
            }

            // 根据版本类型ID获取版本类型
            var eccpEditionsType = await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(edition.ECCPEditionsTypeId.Value);

            GetTempWorkOrderDto tempWorkOrderDto = null;
            GetWaitMaintenanceDto waitMaintenanceDto = null;
            GetTodayWorkOrderStatisticsDto todayWorkOrderStatisticsDto = null;
            GetThisMonthWorkOrderStatisticsDto thisMonthWorkOrderStatisticsDto = null;

            var eccpMaintenanceTempWorkOrders = this._eccpMaintenanceTempWorkOrderRepository.GetAll().Where(e => e.UserId == this.AbpSession.UserId);
            if (eccpEditionsType.Name == "维保公司")
            {
                // 获取临时工单
                tempWorkOrderDto = await eccpMaintenanceTempWorkOrders.Where(e => e.CheckState != 1)
                                       .OrderBy(e => e.Priority).ThenByDescending(e => e.CreationTime).Select(
                                           s => new GetTempWorkOrderDto
                                           {
                                               WorkOrderType = s.TempWorkOrderType.Name,
                                               Describe = s.Describe,
                                               Priority = s.Priority,
                                               CreationTime = s.CreationTime
                                           }).FirstOrDefaultAsync();

                var days7 = DateTime.Now.AddDays(-Convert.ToInt32(DateTime.Now.Date.DayOfWeek));

                // 用户的工单
                var maintenanceWorkOrderIds = this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository.GetAll()
                    .Where(e => e.UserId == this.AbpSession.UserId).Select(s => s.MaintenancePlanId);

                var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                    .Where(w => w.IsClosed == false && maintenanceWorkOrderIds.Contains(w.Id));

                List<string> maintenanceStatus = new List<string>() { "未进行", "进行中" };
                waitMaintenanceDto = new GetWaitMaintenanceDto
                {
                    ThisWeekWaitMaintenanceElevatorNum = eccpMaintenanceWorkOrders.Count(w =>
                        w.PlanCheckDate < days7 &&
                        maintenanceStatus.Contains(w.MaintenanceStatus.Name)),
                    OverdueElevatorInThreeDaysNum = eccpMaintenanceWorkOrders.Count(w =>
                        EF.Functions.DateDiffHour(DateTime.Now, w.PlanCheckDate) < w.MaintenancePlan.RemindHour &&
                        maintenanceStatus.Contains(w.MaintenanceStatus.Name)),
                    OverdueElevatorNum = eccpMaintenanceWorkOrders.Count(w => w.MaintenanceStatus.Name == "已超期"),
                    CreationTime = DateTime.Now
                };


                var maintenanceWorkIds = this._eccpEccpMaintenanceWorkFlowRepository.GetAll()
                    .Where(w => w.CreatorUserId == this.AbpSession.UserId).Select(s => s.MaintenanceWorkId).Distinct();
                var eccpEccpMaintenanceWorkOrderIds = this._eccpEccpMaintenanceWorkRepository.GetAll()
                    .Where(w => maintenanceWorkIds.Contains(w.Id))
                    .Select(s => s.MaintenanceWorkOrderId);
                var myWorks = eccpMaintenanceWorkOrders.Where(w => eccpEccpMaintenanceWorkOrderIds.Contains(w.Id));

                DateTime today = DateTime.Now.Date;
                todayWorkOrderStatisticsDto = new GetTodayWorkOrderStatisticsDto
                {
                    MaintenanceWorkOrderNum = myWorks.Count(w => w.ComplateDate != null && w.ComplateDate > today),
                    MaintenanceTempWorkOrderNum = eccpMaintenanceTempWorkOrders.Count(
                        w => w.CompletionTime != null && w.CheckState == 1 && w.CompletionTime > today),
                    CreationTime = DateTime.Now
                };
                todayWorkOrderStatisticsDto.TotalCompletionSheetNum =
                todayWorkOrderStatisticsDto.MaintenanceTempWorkOrderNum +
                todayWorkOrderStatisticsDto.MaintenanceWorkOrderNum;

                DateTime month = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
                thisMonthWorkOrderStatisticsDto = new GetThisMonthWorkOrderStatisticsDto
                {
                    MaintenanceTempWorkOrderNum = eccpMaintenanceTempWorkOrders.Count(w => w.CompletionTime != null && w.CompletionTime > month && w.CheckState == 1),
                    MaintenanceWorkOrderNum = myWorks.Count(w => w.ComplateDate != null && w.ComplateDate > month),
                    PlanCompletedWorkOrderNum = eccpMaintenanceWorkOrders.Count(w => maintenanceStatus.Contains(w.MaintenanceStatus.Name)),
                    CreationTime = DateTime.Now
                };
                thisMonthWorkOrderStatisticsDto.TotalCompletionSheetNum =
                thisMonthWorkOrderStatisticsDto.MaintenanceWorkOrderNum +
                thisMonthWorkOrderStatisticsDto.MaintenanceTempWorkOrderNum;
            }
            appIndexDataDto = new GetAppIndexDataDto
            {
                TempWorkOrder = tempWorkOrderDto,
                WaitMaintenance = waitMaintenanceDto,
                TodayWorkOrderStatistics = todayWorkOrderStatisticsDto,
                ThisMonthWorkOrderStatistics = thisMonthWorkOrderStatisticsDto
            };

            return appIndexDataDto;
        }

        /// <summary>
        /// The get eccp dict maintenance item.
        /// 维保报告生成打印
        /// </summary>
        /// <param name="workOrderId">
        /// The work order id.
        /// 工单id
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Print)]
        public async Task<List<GetEccpDictMaintenanceItemPrintForView>> GetEccpDictMaintenanceItem(int workOrderId)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                // 查询工单
                var workOrder =
                    await this._eccpMaintenanceWorkOrderRepository.GetAllIncluding(e => e.MaintenancePlan).Include(e => e.MaintenancePlan.Elevator).Include(e => e.MaintenanceType).FirstOrDefaultAsync(e => e.Id == workOrderId);

                // 根据工单查询计划模版关联
                var maintenancePlanTemplateLinks = this._eccpMaintenancePlanTemplateLinkRepository.GetAll().WhereIf(
                    workOrder != null,
                    e => e.MaintenancePlanId == workOrder.MaintenancePlanId);
                var maintenanceTemplate = (from o in maintenancePlanTemplateLinks
                                           join t in this._eccpMaintenanceTemplateRepository.GetAll() on
                                               o.MaintenanceTemplateId equals t.Id
                                           where t.MaintenanceTypeId == workOrder.MaintenanceTypeId
                                           select t).FirstOrDefault();

                // 查询模版节点
                var maintenanceTemplateNodes = this._eccpMaintenanceTemplateNodeRepository.GetAll().WhereIf(
                    maintenanceTemplate != null,
                    e => e.MaintenanceTemplateId == maintenanceTemplate.Id && e.DictNodeTypeId != 5);
                var nodeDictMaintenanceItemLinks = from o in maintenanceTemplateNodes
                                                   join t in this
                                                       ._eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository
                                                       .GetAll() on o.Id equals t.MaintenanceTemplateNodeId
                                                   select t;

                var eccpMaintenanceWork = await this._eccpMaintenanceWorkRepository.FirstOrDefaultAsync(e => e.MaintenanceWorkOrderId == workOrder.Id);
                var user = await this._userManager.FindByIdAsync(Convert.ToString(eccpMaintenanceWork.CreatorUserId));

                // 查询维保项目
                var query = from o in nodeDictMaintenanceItemLinks
                            join t in this._eccpDictMaintenanceItemRepository.GetAll() on o.DictMaintenanceItemId equals
                                t.Id
                            select new GetEccpDictMaintenanceItemPrintForView
                            {
                                MaintenanceUserName = user.Name,
                                MaintenanceComplateDate = workOrder.ComplateDate,
                                Id = t.Id,
                                DisOrder = t.DisOrder,
                                Name = t.Name,
                                TermCode = t.TermCode,
                                TermDesc = t.TermDesc,
                                CertificateNum = workOrder.MaintenancePlan.Elevator.CertificateNum,
                                InstallationAddress = workOrder.MaintenancePlan.Elevator.InstallationAddress,
                                MaintenanceTypeName = workOrder.MaintenanceType.Name
                            };
                var queryList = await query.OrderBy(e => e.DisOrder).Distinct().ToListAsync();

                // 维保工作记录
                var eccpMaintenanceWorkFlows = this._eccpMaintenanceWorkFlowRepository.GetAll()
                    .Where(e => e.MaintenanceWorkId == eccpMaintenanceWork.Id);

                // 维保工作记录涉及到的维保项目
                var worksMaintenanceTemps = from o in eccpMaintenanceWorkFlows
                                            join t in this._eccpMaintenanceWorkFlowItemLinks.GetAll() on o.Id equals
                                                t.MaintenanceWorkFlowId
                                            select new
                                            {
                                                o.MaintenanceTemplateNodeId,
                                                t.DictMaintenanceItemId,
                                                o.MaintenanceWorkId,
                                                o.MaintenanceTemplateNode.DictNodeTypeId,
                                                o.Remark
                                            };

                var worksMaintenanceTempsList = worksMaintenanceTemps.ToList();

                foreach (var m in worksMaintenanceTempsList)
                {
                    var tem = queryList.Find(w => w.Id == m.DictMaintenanceItemId);
                    if (tem == null)
                    {
                        continue;
                    }
                    if (m.DictNodeTypeId == 5)
                    {
                        tem.MaintenanceStatus = 2;
                        continue;
                    }
                    tem.MaintenanceStatus = 1;
                    tem.Remark = m.Remark;
                }

                return queryList;
            }
        }

        /// <summary>
        /// 维保报告生成导出报表
        /// </summary>
        /// <param name="workOrderId">
        /// 工单ID
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Excel)]
        public async Task<FileDto> GetEccpDictMaintenanceItemToExcel(int workOrderId)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                // 查询工单
                var workOrder =
                    await this._eccpMaintenanceWorkOrderRepository.FirstOrDefaultAsync(e => e.Id == workOrderId);

                // 根据工单查询计划模版关联
                var maintenancePlanTemplateLinks = this._eccpMaintenancePlanTemplateLinkRepository.GetAll().WhereIf(
                    workOrder != null,
                    e => e.MaintenancePlanId == workOrder.MaintenancePlanId);
                var maintenanceTemplate = (from o in maintenancePlanTemplateLinks
                                           join t in this._eccpMaintenanceTemplateRepository.GetAll() on
                                               o.MaintenanceTemplateId equals t.Id
                                           where t.MaintenanceTypeId == workOrder.MaintenanceTypeId
                                           select t).FirstOrDefault();

                // 查询模版节点
                var maintenanceTemplateNodes = this._eccpMaintenanceTemplateNodeRepository.GetAll().WhereIf(
                    maintenanceTemplate != null,
                    e => e.MaintenanceTemplateId == maintenanceTemplate.Id);
                var nodeDictMaintenanceItemLinks = from o in maintenanceTemplateNodes
                                                   join t in this
                                                       ._eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository
                                                       .GetAll() on o.Id equals t.MaintenanceTemplateNodeId
                                                   select t;

                // 查询维保项目
                var query = from o in nodeDictMaintenanceItemLinks
                            join t in this._eccpDictMaintenanceItemRepository.GetAll() on o.DictMaintenanceItemId equals
                                t.Id
                            select new GetEccpDictMaintenanceItemForView
                            {
                                EccpDictMaintenanceItem =
                                               this.ObjectMapper.Map<EccpDictMaintenanceItemDto>(t)
                            };

                var eccpDictMaintenanceItems = await query.ToListAsync();

                return this._eccpMaintenanceWorkOrdersExcelExporter.ExportToFile(eccpDictMaintenanceItems);
            }
        }

        /// <summary>
        /// The get eccp dict maintenance item to word.
        /// </summary>
        /// <param name="workOrderId">
        /// The work order id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetEccpDictMaintenanceItemToWord(int workOrderId)
        {
            var list = await this.GetEccpDictMaintenanceItem(workOrderId);

            var doc = new XWPFDocument();
            var p0 = doc.CreateParagraph();
            p0.Alignment = ParagraphAlignment.CENTER;
            var r0 = p0.CreateRun();
            r0.FontFamily = "microsoft yahei";
            r0.FontSize = 18;
            r0.IsBold = true;
            r0.SetText(list.FirstOrDefault()?.MaintenanceTypeName + "保养记录");

            var p1 = doc.CreateParagraph();
            p1.Alignment = ParagraphAlignment.LEFT;
            var r1 = p1.CreateRun();
            r1.FontFamily = "仿宋";
            r1.FontSize = 12;
            r1.IsBold = true;
            r1.SetText(
                "维保人：" + list.FirstOrDefault()?.MaintenanceUserName + "                                   维保日期："
                + list.FirstOrDefault()?.MaintenanceComplateDate?.ToString("yyyy-MM-dd"));
            r1.AddCarriageReturn();
            r1.AppendText("注册代码：" + list.FirstOrDefault()?.CertificateNum);
            r1.AddCarriageReturn();
            r1.AppendText("安装地址：" + list.FirstOrDefault()?.InstallationAddress);

            var listCount = list.Count;

            var p2 = doc.CreateTable(listCount + 2, 4);
            p2.GetRow(0).GetCell(0).SetText("序号");
            p2.GetRow(0).GetCell(1).SetText("维保项目（内容）");
            p2.GetRow(0).GetCell(2).SetText("维保基本要求");
            p2.GetRow(0).GetCell(3).SetText("结果:");
            for (var i = 0; i < listCount; i++)
            {
                p2.GetRow(i + 1).GetCell(0).SetText((i + 1).ToString());
                p2.GetRow(i + 1).GetCell(1).SetText(list[i].Name);
                p2.GetRow(i + 1).GetCell(2).SetText(list[i].TermDesc);
                p2.GetRow(i + 1).GetCell(3).SetText(
                    list[i].MaintenanceStatus == 0 ? "√" : list[i].MaintenanceStatus == 1 ? "×" : "△");
            }

            p2.GetRow(listCount + 1).MergeCells(1, 3);
            p2.GetRow(listCount + 1).GetCell(0).SetText("备注");
            p2.GetRow(listCount + 1).GetCell(1).SetText(string.Join(";", list.Where(e => !string.IsNullOrWhiteSpace(e.Remark)).Select(e => e.Remark)));

            var p3 = doc.CreateParagraph();
            p3.Alignment = ParagraphAlignment.LEFT;
            var r3 = p3.CreateRun();
            r3.FontFamily = "仿宋";
            r3.FontSize = 12;
            r3.SetText("备注说明：1.维保人员按以上项目维保，每次维保后认真填写记录。");
            r3.AddCarriageReturn();
            r3.AddTab();
            r3.AddTab();
            r3.AppendText("\n\n\n2.维保人员认可部位打√，否则打×，无此项目部位打△");
            r3.AddCarriageReturn();
            r3.AppendText("维保人员：(签字)");
            r3.AddCarriageReturn();
            r3.AppendText("使用单位安全管理员：(签字)                           日期：");

            var ms = new MemoryStream();
            doc.Write(ms);
            ms.Flush();
            var data = ms.ToArray();
            return this._eccpMaintenanceWorkOrdersWordExporter.ExportToFile(data);
        }

        /// <summary>
        /// The get eccp maintenance work order for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit)]
        public async Task<GetEccpMaintenanceWorkOrderForEditOutput> GetEccpMaintenanceWorkOrderForEdit(EntityDto input)
        {
            var eccpMaintenanceWorkOrder = await this._eccpMaintenanceWorkOrderRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpMaintenanceWorkOrderForEditOutput
            {
                EccpMaintenanceWorkOrder =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenanceWorkOrderDto>(
                                         eccpMaintenanceWorkOrder)
            };

            var eccpMaintenanceWorkOrderMaintenanceUserLinks = this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository
                .GetAll().Where(e => e.MaintenancePlanId == output.EccpMaintenanceWorkOrder.Id);
            var users = this.UserManager.Users;

            var eccpMaintenancePlanMaintenanceUsers =
                from eccpMaintenanceWorkOrderMaintenanceUserLink in eccpMaintenanceWorkOrderMaintenanceUserLinks
                join user in users on eccpMaintenanceWorkOrderMaintenanceUserLink.UserId equals user.Id
                select new GetEccpMaintenanceUserNameAndPhoneDto { Name = user.Name, PhoneNumber = user.PhoneNumber, UserId = user.Id };

            output.EccpMaintenanceUserNameAndPhones = await eccpMaintenancePlanMaintenanceUsers.ToListAsync();
            var eccpMaintenancePlan =
                await this._eccpMaintenancePlanRepository.FirstOrDefaultAsync(
                    output.EccpMaintenanceWorkOrder.MaintenancePlanId);
            output.EccpMaintenancePlanPollingPeriod = eccpMaintenancePlan.PollingPeriod.ToString();

            var eccpBaseElevator =
                await this._eccpBaseElevatorsRepository.FirstOrDefaultAsync(eccpMaintenancePlan.ElevatorId);
            output.EccpElevatorInstallationAddress = eccpBaseElevator.InstallationAddress;

            var eccpDictMaintenanceType =
                await this._eccpDictMaintenanceTypeRepository.FirstOrDefaultAsync(
                    output.EccpMaintenanceWorkOrder.MaintenanceTypeId);
            output.EccpDictMaintenanceTypeName = eccpDictMaintenanceType.Name;
            var eccpDictMaintenanceStatus =
                await this._eccpDictMaintenanceStatusRepository.FirstOrDefaultAsync(
                    output.EccpMaintenanceWorkOrder.MaintenanceStatusId);
            output.EccpDictMaintenanceStatusName = eccpDictMaintenanceStatus.Name;

            return output;
        }

        /// <summary>
        /// The get eccp maintenance work orders to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetEccpMaintenanceWorkOrdersToExcel(
            GetAllEccpMaintenanceWorkOrdersForExcelInput input)
        {
            var filteredEccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Remark.Contains(input.Filter))
                .WhereIf(input.IsPassedFilter > -1, e => Convert.ToInt32(e.IsPassed) == input.IsPassedFilter)
                .WhereIf(input.MinLongitudeFilter != null, e => e.Longitude >= input.MinLongitudeFilter)
                .WhereIf(input.MaxLongitudeFilter != null, e => e.Longitude <= input.MaxLongitudeFilter)
                .WhereIf(input.MinLatitudeFilter != null, e => e.Latitude >= input.MinLatitudeFilter)
                .WhereIf(input.MaxLatitudeFilter != null, e => e.Latitude <= input.MaxLatitudeFilter).WhereIf(
                    input.MinPlanCheckDateFilter != null,
                    e => e.PlanCheckDate >= input.MinPlanCheckDateFilter).WhereIf(
                    input.MaxPlanCheckDateFilter != null,
                    e => e.PlanCheckDate <= input.MaxPlanCheckDateFilter);

            var query =
                (from o in filteredEccpMaintenanceWorkOrders
                 join o1 in this._eccpMaintenancePlanRepository.GetAll() on o.MaintenancePlanId equals o1.Id into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpDictMaintenanceTypeRepository.GetAll() on o.MaintenanceTypeId equals o2.Id into j2
                 from s2 in j2.DefaultIfEmpty()
                 join o3 in this._eccpDictMaintenanceStatusRepository.GetAll() on o.MaintenanceStatusId equals o3.Id
                     into j3
                 from s3 in j3.DefaultIfEmpty()
                 select new GetEccpMaintenanceWorkOrderForView
                 {
                     EccpMaintenanceWorkOrder = this.ObjectMapper.Map<EccpMaintenanceWorkOrderDto>(o),
                     EccpMaintenancePlanPollingPeriod =
                                    s1 == null ? string.Empty : s1.PollingPeriod.ToString(),
                     EccpDictMaintenanceTypeName = s2 == null ? string.Empty : s2.Name,
                     EccpDictMaintenanceStatusName = s3 == null ? string.Empty : s3.Name
                 }).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpMaintenancePlanPollingPeriodFilter),
                    e => e.EccpMaintenancePlanPollingPeriod.ToLower()
                         == input.EccpMaintenancePlanPollingPeriodFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpDictMaintenanceTypeNameFilter),
                    e => e.EccpDictMaintenanceTypeName.ToLower()
                         == input.EccpDictMaintenanceTypeNameFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpDictMaintenanceStatusNameFilter),
                    e => e.EccpDictMaintenanceStatusName.ToLower()
                         == input.EccpDictMaintenanceStatusNameFilter.ToLower().Trim());

            var eccpMaintenanceWorkOrderListDtos = await query.ToListAsync();

            return this._eccpMaintenanceWorkOrdersExcelExporter.ExportToFile(eccpMaintenanceWorkOrderListDtos);
        }

        /// <summary>
        /// 临期工单
        /// </summary>
        /// <param name="input">
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<GetEccpMaintenanceWorkOrderForView>> GetPeriodAll(
            GetAllEccpMaintenanceWorkOrdersInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                    .Where(w => w.TenantId == this.AbpSession.TenantId)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Remark.Contains(input.Filter)).WhereIf(
                        input.IsPassedFilter > -1,
                        e => Convert.ToInt32(e.IsPassed) == input.IsPassedFilter);

                var eccpMaintenanceWorkOrderMaintenanceUserLinks = this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository
                    .GetAll().Where(w => w.TenantId == this.AbpSession.TenantId);
                //var eccpMaintenancePlanPropertyUserLinks = this._eccpMaintenancePlanPropertyUserLinkRepository.GetAll()
                //    .Where(w => w.TenantId == this.AbpSession.TenantId);
                var users = this.UserManager.Users;

                var eccpMaintenanceWorkOrderMaintenanceUsers =
                    from eccpMaintenanceWorkOrderMaintenanceUserLink in eccpMaintenanceWorkOrderMaintenanceUserLinks
                    join user in users on eccpMaintenanceWorkOrderMaintenanceUserLink.UserId equals user.Id
                    select new { eccpMaintenanceWorkOrderMaintenanceUserLink.MaintenancePlanId, user.Name };

                //var eccpMaintenancePlanPropertyUsers =
                //    from eccpMaintenancePlanPropertyUserLink in eccpMaintenancePlanPropertyUserLinks
                //    join user in users on eccpMaintenancePlanPropertyUserLink.UserId equals user.Id
                //    select new { eccpMaintenancePlanPropertyUserLink.MaintenancePlanId, user.Name };

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
                     join o4 in eccpMaintenanceWorkOrderMaintenanceUsers on o.Id equals o4.MaintenancePlanId
                         into j4
                     from s4 in j4.DefaultIfEmpty()
                         //join o5 in eccpMaintenancePlanPropertyUsers on o.MaintenancePlanId equals o5.MaintenancePlanId into
                         //    j5
                         //from s5 in j5.DefaultIfEmpty()
                     where o.PlanCheckDate < DateTime.Now.AddHours(s1.RemindHour)
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
                        e => e.EccpElevatorName.ToLower() == input.EccpElevatorNameFilter.ToLower().Trim());

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

                var totalCount = workOrderQuery.Count();

                var eccpMaintenanceWorkOrders = new List<GetEccpMaintenanceWorkOrderForView>();

                workOrderQuery.OrderBy(input.Sorting ?? "eccpMaintenanceWorkOrder.id asc").PageBy(input)
                    .MapTo(eccpMaintenanceWorkOrders);

                return new PagedResultDto<GetEccpMaintenanceWorkOrderForView>(totalCount, eccpMaintenanceWorkOrders);
            }
        }

        /// <summary>
        ///     维保工单超期处理
        /// </summary>
        [AbpAllowAnonymous]
        public void GetOverdueTreatmentOfMaintenanceWorkOrder()
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                .Where(e => e.PlanCheckDate < DateTime.Now);

                var eccpDictMaintenanceStatusOverdue = this._eccpDictMaintenanceStatusRepository.GetAll()
                    .FirstOrDefault(w => w.Name == "已超期");

                // 初始化工单状态
                var eccpDictMaintenanceStatus =
                    this._eccpDictMaintenanceStatusRepository.GetAll().FirstOrDefault(w => w.Name == "未进行");

                // 初始维保类型
                var eccpDictMaintenanceType = this._eccpDictMaintenanceTypeRepository.GetAll().FirstOrDefault();

                foreach (var eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders)
                {
                    var planCheckDate = eccpMaintenanceWorkOrder.PlanCheckDate;

                    eccpMaintenanceWorkOrder.MaintenanceStatusId = eccpDictMaintenanceStatusOverdue.Id;
                    this._eccpMaintenanceWorkOrderRepository.Update(eccpMaintenanceWorkOrder);

                    // 计划
                    var eccpMaintenancePlan =
                        this._eccpMaintenancePlanRepository.FirstOrDefault(
                            w => w.Id == eccpMaintenanceWorkOrder.MaintenancePlanId);

                    // 通过计划电梯与和合同的关联关系找到合同
                    var eccpMaintenanceContractElevatorLink =
                        this._eccpMaintenanceContractElevatorLinkRepository.FirstOrDefault(
                            w => w.ElevatorId == eccpMaintenancePlan.ElevatorId);

                    // 合同
                    EccpMaintenanceContract eccpMaintenanceContract;
                    using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
                    {
                        eccpMaintenanceContract = this._eccpMaintenanceContractRepository.FirstOrDefault(
                            eccpMaintenanceContractElevatorLink.MaintenanceContractId.Value);
                    }

                    if (planCheckDate < eccpMaintenanceContract.EndDate)
                    {
                        planCheckDate = planCheckDate.AddDays(eccpMaintenancePlan.PollingPeriod);
                        var order = new EccpMaintenanceWorkOrder
                        {
                            IsPassed = false,
                            MaintenancePlanId = eccpMaintenancePlan.Id,
                            PlanCheckDate = planCheckDate,
                            MaintenanceStatusId = eccpDictMaintenanceStatus.Id,
                            MaintenanceTypeId = eccpDictMaintenanceType.Id,
                            TenantId = eccpMaintenancePlan.TenantId
                        };
                        this._eccpMaintenanceWorkOrderRepository.InsertAndGetIdAsync(order);

                        // 计划维保人员
                        var eccpMaintenancePlanMaintenanceUserLinks = this
                            ._eccpMaintenancePlanMaintenanceUserLinkRepository
                            .GetAll().Where(w => w.MaintenancePlanId == order.MaintenancePlanId).ToList();

                        // 添加工单维保人员
                        eccpMaintenancePlanMaintenanceUserLinks.ForEach(
                            s => this._eccpMaintenanceWorkOrderMaintenanceUserLinkRepository.Insert(
                                new EccpMaintenanceWorkOrder_MaintenanceUser_Link
                                {
                                    TenantId = s.TenantId,
                                    UserId = s.UserId,
                                    MaintenancePlanId = order.Id
                                }));

                        // 计划使用人员
                        var eccpMaintenancePlanPropertyUserLinks = this._eccpMaintenancePlanPropertyUserLinkRepository
                            .GetAll().Where(w => w.MaintenancePlanId == order.MaintenancePlanId).ToList();

                        // 添加工单使用人员
                        eccpMaintenancePlanPropertyUserLinks.ForEach(
                            s => this._eccpMaintenanceWorkOrderPropertyUserLinkRepository.Insert(
                                new EccpMaintenanceWorkOrder_PropertyUser_Link
                                {
                                    TenantId = s.TenantId,
                                    UserId = s.UserId,
                                    MaintenancePlanId = order.Id
                                }));
                    }
                }
            }
        }

        /// <summary>
        /// 计划删除计划工单刷新
        /// </summary>
        /// <param name="maintenancePlanId">
        /// The maintenance Plan Id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks)]
        public int PlanDeletingRearrangeWorkOrder(long maintenancePlanId)
        {
            var resultCount = this._eccpMaintenanceWorkOrderManager.PlanDeletingRearrangeWorkOrder(maintenancePlanId);
            return resultCount;
        }

        /// <summary>
        /// 计划修改和添加工单刷新
        /// </summary>
        /// <param name="maintenancePlanId">
        /// The maintenance Plan Id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks)]
        public async Task<int> PlanModificationRearrangeWorkOrder(long maintenancePlanId)
        {
            var resultCount =
                await this._eccpMaintenanceWorkOrderManager.PlanModificationRearrangeWorkOrder(maintenancePlanId);
            return resultCount;
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create)]
        private async Task Create(CreateOrEditEccpMaintenanceWorkOrderDto input)
        {
            var eccpMaintenanceWorkOrder = this.ObjectMapper.Map<EccpMaintenanceWorkOrder>(input);

            if (this.AbpSession.TenantId != null)
            {
                eccpMaintenanceWorkOrder.TenantId = (int)this.AbpSession.TenantId;
            }

            await this._eccpMaintenanceWorkOrderRepository.InsertAsync(eccpMaintenanceWorkOrder);
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit)]
        private async Task Update(CreateOrEditEccpMaintenanceWorkOrderDto input)
        {
            if (input.Id != null)
            {
                var eccpMaintenanceWorkOrder =
                    await this._eccpMaintenanceWorkOrderRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpMaintenanceWorkOrder);
            }
        }

        /// <summary>
        /// 根据计划ID、工单ID查询电梯信息
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        public async Task<GetEccpBaseElevatorsInfoDto> GetEccpBaseElevatorsInfoByPlanId(int planId, int workOrderId)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpMaintenancePlan = await this._eccpMaintenancePlanRepository.GetAllIncluding(e => e.Elevator)
                    .FirstOrDefaultAsync(e => e.Id == planId);

                var output = new GetEccpBaseElevatorsInfoDto();
                if (eccpMaintenancePlan != null && eccpMaintenancePlan.Elevator != null)
                {
                    output = this.ObjectMapper.Map<GetEccpBaseElevatorsInfoDto>(eccpMaintenancePlan.Elevator);

                    var eccpMaintenanceWorkOrder = await this._eccpMaintenanceWorkOrderRepository
                        .GetAllIncluding(e => e.MaintenancePlan).FirstOrDefaultAsync(e =>
                            e.MaintenancePlanId == eccpMaintenancePlan.Id && e.Id < workOrderId);
                    if (eccpMaintenanceWorkOrder != null)
                    {
                        output.LastMaintenanceTime = eccpMaintenanceWorkOrder.ComplateDate;

                        var eccpMaintenanceWorkFlows = this._eccpEccpMaintenanceWorkFlowRepository
                            .GetAllIncluding(e => e.MaintenanceWork)
                            .Where(e => e.MaintenanceWork.MaintenanceWorkOrderId == eccpMaintenanceWorkOrder.Id);
                        var users = this.UserManager.Users;

                        var eccpMaintenanceUsers =
                            from eccpMaintenanceWorkFlow in eccpMaintenanceWorkFlows
                            join user in users on eccpMaintenanceWorkFlow.CreatorUserId equals user.Id
                            select new { user.Name };
                        output.LastMaintenanceUserNames = string.Join(",",
                            eccpMaintenanceUsers.Select(e => e.Name).Distinct().ToList());
                    }

                    if (eccpMaintenancePlan.Elevator.EccpDictPlaceTypeId != null)
                    {
                        var eccpDictPlaceType = await this._eccpDictPlaceTypeRepository.FirstOrDefaultAsync(
                            eccpMaintenancePlan.Elevator
                                .EccpDictPlaceTypeId.Value);
                        if (eccpDictPlaceType != null)
                        {
                            output.EccpDictPlaceTypeName = eccpDictPlaceType.Name;
                        }
                    }

                    if (eccpMaintenancePlan.Elevator.EccpDictElevatorTypeId != null)
                    {
                        var eccpDictElevatorType = await this._eccpDictElevatorTypeRepository.FirstOrDefaultAsync(
                            eccpMaintenancePlan.Elevator
                                .EccpDictElevatorTypeId.Value);
                        if (eccpDictElevatorType != null)
                        {
                            output.EccpDictElevatorTypeName = eccpDictElevatorType.Name;
                        }
                    }

                    if (eccpMaintenancePlan.Elevator.ECCPBasePropertyCompanyId != null)
                    {
                        var eCCPBasePropertyCompany = await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(
                            eccpMaintenancePlan.Elevator
                                .ECCPBasePropertyCompanyId.Value);
                        if (eCCPBasePropertyCompany != null)
                        {
                            output.ECCPBasePropertyCompanyName = eCCPBasePropertyCompany.Name;
                        }
                    }

                    if (eccpMaintenancePlan.Elevator.ECCPBaseMaintenanceCompanyId != null)
                    {
                        var eCCPBaseMaintenanceCompany =
                            await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(eccpMaintenancePlan
                                .Elevator
                                .ECCPBaseMaintenanceCompanyId.Value);
                        if (eCCPBaseMaintenanceCompany != null)
                        {
                            output.ECCPBaseMaintenanceCompanyName = eCCPBaseMaintenanceCompany.Name;
                        }
                    }

                    if (eccpMaintenancePlan.Elevator.ECCPBaseAnnualInspectionUnitId != null)
                    {
                        var eCCPBaseAnnualInspectionUnit =
                            await this._eccpBaseAnnualInspectionUnitRepository.FirstOrDefaultAsync(eccpMaintenancePlan
                                .Elevator
                                .ECCPBaseAnnualInspectionUnitId.Value);
                        if (eCCPBaseAnnualInspectionUnit != null)
                        {
                            output.ECCPBaseAnnualInspectionUnitName = eCCPBaseAnnualInspectionUnit.Name;
                        }
                    }

                    if (eccpMaintenancePlan.Elevator.EccpBaseElevatorBrandId != null)
                    {
                        var eccpBaseElevatorBrand = await this._eccpBaseElevatorBrandRepository.FirstOrDefaultAsync(
                            eccpMaintenancePlan.Elevator
                                .EccpBaseElevatorBrandId.Value);
                        if (eccpBaseElevatorBrand != null)
                        {
                            output.EccpBaseElevatorBrandName = eccpBaseElevatorBrand.Name;
                        }
                    }

                    if (eccpMaintenancePlan.Elevator.EccpBaseElevatorModelId != null)
                    {
                        var eccpBaseElevatorModel = await this._eccpBaseElevatorModelRepository.FirstOrDefaultAsync(
                            eccpMaintenancePlan.Elevator
                                .EccpBaseElevatorModelId.Value);
                        if (eccpBaseElevatorModel != null)
                        {
                            output.EccpBaseElevatorModelName = eccpBaseElevatorModel.Name;
                        }
                    }
                }

                return output;
            }
        }

        /// <summary>
        /// 根据工单ID查询工作时间线
        /// </summary>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        public async Task<List<GetEccpMaintenanceWorkFlowsDto>> GetAllEccpMaintenanceWorkFlowsByWorkOrderId(int workOrderId)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpMaintenanceWork = await this._eccpEccpMaintenanceWorkRepository.FirstOrDefaultAsync(e =>
                e.MaintenanceWorkOrderId == workOrderId);

                var getEccpMaintenanceWorkFlowsDtos = new List<GetEccpMaintenanceWorkFlowsDto>();

                if (eccpMaintenanceWork != null)
                {
                    var eccpDictNodeTypes = _eccpDictNodeTypeRepository.GetAll().Where(e => e.Name == "提醒节点" || e.Name == "选择节点").Select(e => e.Id);

                    var eccpMaintenanceWorkFlows = this._eccpEccpMaintenanceWorkFlowRepository.GetAllIncluding(e => e.DictMaintenanceWorkFlowStatus).Include(e => e.MaintenanceTemplateNode).Where(e => e.MaintenanceWorkId == eccpMaintenanceWork.Id && !eccpDictNodeTypes.Contains(e.MaintenanceTemplateNode.DictNodeTypeId));

                    var users = this.UserManager.Users;

                    var query = from eccpMaintenanceWorkFlow in eccpMaintenanceWorkFlows
                                join user in users on eccpMaintenanceWorkFlow.CreatorUserId equals user.Id into j2
                                from s2 in j2.DefaultIfEmpty()
                                select new GetEccpMaintenanceWorkFlowsDto
                                {
                                    MaintenanceLastModificationTime = eccpMaintenanceWorkFlow.LastModificationTime,
                                    MaintenanceUserName = s2.Name,
                                    ProfilePictureId = s2.ProfilePictureId,
                                    TemplateNodeName = eccpMaintenanceWorkFlow.MaintenanceTemplateNode.TemplateNodeName,
                                    NodeDesc = eccpMaintenanceWorkFlow.MaintenanceTemplateNode.NodeDesc,
                                    ActionCodeValue = eccpMaintenanceWorkFlow.ActionCodeValue,
                                    ActionCode = eccpMaintenanceWorkFlow.MaintenanceTemplateNode.ActionCode,
                                    DictMaintenanceWorkFlowStatusName = eccpMaintenanceWorkFlow.DictMaintenanceWorkFlowStatus.Name
                                };
                    getEccpMaintenanceWorkFlowsDtos = await query.OrderBy(e => e.MaintenanceLastModificationTime).ToListAsync();

                    foreach (var m in getEccpMaintenanceWorkFlowsDtos)
                    {
                        if (m.ActionCode == "100003")
                        {
                            var eccpBaseElevatorLabel = await this._eccpBaseElevatorLabelRepository.GetAllIncluding(e => e.Elevator).FirstOrDefaultAsync(e => e.UniqueId == m.ActionCodeValue);
                            if (eccpBaseElevatorLabel != null)
                            {
                                m.LabelName = eccpBaseElevatorLabel.LabelName;
                                m.InstallationAddress = eccpBaseElevatorLabel.Elevator.InstallationAddress;
                            }
                        }
                        if (m.ActionCode == "100001")
                        {
                            if (!string.IsNullOrWhiteSpace(m.ActionCodeValue))
                            {
                                var sArray = m.ActionCodeValue.Split(new[] { "<=>" }, StringSplitOptions.None);
                                if (sArray.Length > 1)
                                {
                                    if (sArray[0].Equals("1"))
                                    {
                                        m.JObjActionCodeValue = JObject.Parse(sArray[1]);
                                        var userId = Convert.ToInt64(m.JObjActionCodeValue["userId"].ToString());
                                        var user = await users.FirstOrDefaultAsync(e => e.Id == userId);
                                        if (user != null)
                                        {
                                            m.JObjActionCodeValue["userName"] = user.Name;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return getEccpMaintenanceWorkFlowsDtos;
            }
        }
    }
}