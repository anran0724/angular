// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkFlowsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.EccpMaintenancePlans;
using Sinodom.ElevatorCloud.Web;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks
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
    using Abp.UI;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.AppReturnModel;
    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    /// The eccp maintenance work flows app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows)]
    public class EccpMaintenanceWorkFlowsAppService : ElevatorCloudAppServiceBase, IEccpMaintenanceWorkFlowsAppService
    {
        /// <summary>
        /// The _eccp dict maintenance work flow status repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceWorkFlowStatus, int> _eccpDictMaintenanceWorkFlowStatusRepository;

        /// <summary>
        /// The _eccp maintenance template node repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTemplateNode, int> _eccpMaintenanceTemplateNodeRepository;

        /// <summary>
        /// The _eccp maintenance work.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWork> _eccpMaintenanceWork;

        /// <summary>
        /// The _eccp maintenance work flow_ item_ links.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkFlow_Item_Link, Guid> _eccpMaintenanceWorkFlowItemLinks;

        /// <summary>
        /// The _eccp maintenance work flow repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkFlow, Guid> _eccpMaintenanceWorkFlowRepository;

        /// <summary>
        /// The _eccp maintenance work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder, int> _eccpMaintenanceWorkOrderRepository;

        /// <summary>
        /// The _eccp maintenance work repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWork, int> _eccpMaintenanceWorkRepository;

        /// <summary>
        /// The _temp file cache manager.
        /// </summary>
        private readonly ITempFileCacheManager _tempFileCacheManager;

        /// <summary>
        /// The _binary object manager.
        /// </summary>
        private readonly IBinaryObjectManager _binaryObjectManager;

        /// <summary>
        /// The _eccp maintenance work order manager.
        /// </summary>
        private readonly EccpMaintenanceWorkOrderManager _eccpMaintenanceWorkOrderManager;


        private readonly IRepository<EccpMaintenanceWorkFlow_Refuse_Link, int> _eccpMaintenanceWorkFlow_Refuse_LinksRepository;

        private readonly IRepository<EccpMaintenancePlan, int> _eccpMaintenancePlanRepository;

        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorsRepository;
        private readonly IRepository<EccpDictMaintenanceType, int> _eccpDictMaintenanceTypeRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkFlowsAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceWorkFlowRepository">
        /// The eccp maintenance work flow repository.
        /// </param>
        /// <param name="eccpMaintenanceTemplateNodeRepository">
        /// The eccp maintenance template node repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkRepository">
        /// The eccp maintenance work repository.
        /// </param>
        /// <param name="eccpDictMaintenanceWorkFlowStatusRepository">
        /// The eccp dict maintenance work flow status repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderRepository">
        /// The eccp maintenance work order repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkFlowItemLinks">
        /// The eccp maintenance work flow_ item_ links.
        /// </param>
        /// <param name="tempFileCacheManager">
        /// The temp File Cache Manager.
        /// </param>
        /// <param name="binaryObjectManager">
        /// The binary Object Manager.
        /// </param>
        /// <param name="eccpMaintenanceWork">
        /// The eccp maintenance work.
        /// </param>
        public EccpMaintenanceWorkFlowsAppService(
            IRepository<EccpMaintenanceWorkFlow, Guid> eccpMaintenanceWorkFlowRepository,
            IRepository<EccpMaintenanceTemplateNode, int> eccpMaintenanceTemplateNodeRepository,
            IRepository<EccpMaintenanceWork, int> eccpMaintenanceWorkRepository,
            IRepository<EccpDictMaintenanceWorkFlowStatus, int> eccpDictMaintenanceWorkFlowStatusRepository,
            IRepository<EccpMaintenanceWorkOrder, int> eccpMaintenanceWorkOrderRepository,
            IRepository<EccpMaintenanceWorkFlow_Item_Link, Guid> eccpMaintenanceWorkFlowItemLinks,
            ITempFileCacheManager tempFileCacheManager,
            IBinaryObjectManager binaryObjectManager,
            IRepository<EccpMaintenanceWork> eccpMaintenanceWork, EccpMaintenanceWorkOrderManager eccpMaintenanceWorkOrderManager, IRepository<EccpMaintenanceWorkFlow_Refuse_Link, int> eccpMaintenanceWorkFlow_Refuse_LinksRepository,
            IRepository<EccpMaintenancePlan, int> eccpMaintenancePlanRepository,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorsRepository,
            IRepository<EccpDictMaintenanceType, int> eccpDictMaintenanceTypeRepository)
        {
            this._eccpMaintenanceWorkFlowRepository = eccpMaintenanceWorkFlowRepository;
            this._eccpMaintenanceTemplateNodeRepository = eccpMaintenanceTemplateNodeRepository;
            this._eccpMaintenanceWorkRepository = eccpMaintenanceWorkRepository;
            this._eccpDictMaintenanceWorkFlowStatusRepository = eccpDictMaintenanceWorkFlowStatusRepository;
            this._eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
            this._eccpMaintenanceWorkFlowItemLinks = eccpMaintenanceWorkFlowItemLinks;
            this._tempFileCacheManager = tempFileCacheManager;
            this._binaryObjectManager = binaryObjectManager;
            this._eccpMaintenanceWork = eccpMaintenanceWork;
            this._eccpMaintenanceWorkOrderManager = eccpMaintenanceWorkOrderManager;
            this._eccpMaintenanceWorkFlow_Refuse_LinksRepository = eccpMaintenanceWorkFlow_Refuse_LinksRepository;
            this._eccpMaintenancePlanRepository = eccpMaintenancePlanRepository;
            this._eccpBaseElevatorsRepository = eccpBaseElevatorsRepository;
            this._eccpDictMaintenanceTypeRepository = eccpDictMaintenanceTypeRepository;
        }

        /// <summary>
        /// The app create.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>
        /// </returns>
        public async Task<ReturnModel> AppCreate(List<CreateOrEditEccpMaintenanceWorkFlowDto> list)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var returnModel = new ReturnModel();
                var isFlowCount = list.Count();
                if (isFlowCount == 0)
                {
                    returnModel.flag = false;
                    returnModel.message = this.L("PleaseUploadData");
                    return returnModel;
                }
                var eccpMaintenanceWork = this._eccpMaintenanceWork.FirstOrDefault(e => e.MaintenanceWorkOrderId == list[0].MaintenanceWorkId);
                if (eccpMaintenanceWork == null)
                {
                    returnModel.flag = false;
                    returnModel.message = this.L("MaintenanceWorkIsNull");
                    return returnModel;
                }
                eccpMaintenanceWork.Latitude = list[0].Latitude;
                eccpMaintenanceWork.Longitude = list[0].Longitude;
                await _eccpMaintenanceWork.UpdateAsync(eccpMaintenanceWork);

                if (this.AbpSession.TenantId == null)
                {
                    returnModel.flag = false;
                    returnModel.message = this.L("AbpSessionIsNull");
                    return returnModel;
                }

                int tenantId = this.AbpSession.TenantId.Value;

                var myFlows = _eccpMaintenanceWorkFlowRepository.GetAll().Where(e =>
                    e.MaintenanceWorkId == eccpMaintenanceWork.Id && e.CreatorUserId == list[0].CreatorUserId).ToList();
                foreach (var item in list)
                {
                    var m = myFlows.Find(w => w.MaintenanceTemplateNodeId == item.MaintenanceTemplateNodeId);
                    if ((m == null && item.DictMaintenanceWorkFlowStatusId == 3) || m == null || item.DictMaintenanceWorkFlowStatusId == 2)
                    {
                        var eccpMaintenanceWorkFlow = this.ObjectMapper.Map<EccpMaintenanceWorkFlow>(item);
                        eccpMaintenanceWorkFlow.TenantId = tenantId;

                        var isImage = Guid.TryParse(eccpMaintenanceWorkFlow.ActionCodeValue, out _);
                        if (isImage)
                        {
                            byte[] byteArray;
                            var imageBytes = this._tempFileCacheManager.GetFile(eccpMaintenanceWorkFlow.ActionCodeValue);

                            if (imageBytes == null)
                            {
                                throw new UserFriendlyException("There is no such image file with the token: " + eccpMaintenanceWorkFlow.ActionCodeValue);
                            }

                            var user = await this.UserManager.FindByIdAsync(eccpMaintenanceWorkFlow.CreatorUserId.ToString());

                            string elevatorAddress = "";
                            string maintenanceTypeName = "";
                            var eccpMaintenanceWorkOrder = await _eccpMaintenanceWorkOrderRepository.FirstOrDefaultAsync(eccpMaintenanceWork.MaintenanceWorkOrderId);
                            if (eccpMaintenanceWorkOrder != null)
                            {
                                var eccpDictMaintenanceType = await this._eccpDictMaintenanceTypeRepository.FirstOrDefaultAsync(
                                    eccpMaintenanceWorkOrder.MaintenanceTypeId);
                                if (eccpDictMaintenanceType != null)
                                {
                                    maintenanceTypeName = eccpDictMaintenanceType.Name;
                                }

                                var eccpMaintenancePlan = await this._eccpMaintenancePlanRepository.FirstOrDefaultAsync(eccpMaintenanceWorkOrder
                                    .MaintenancePlanId);
                                if (eccpMaintenancePlan != null)
                                {
                                    var eccpBaseElevator = await this._eccpBaseElevatorsRepository.FirstOrDefaultAsync(eccpMaintenancePlan
                                        .ElevatorId);
                                    if (eccpBaseElevator != null)
                                    {
                                        elevatorAddress = eccpBaseElevator.InstallationAddress;
                                    }
                                }
                            }
                            
                            var eccpMaintenanceTemplateNode = await this._eccpMaintenanceTemplateNodeRepository.FirstOrDefaultAsync(
                                item.MaintenanceTemplateNodeId);

                            List<string> watermarkContents = new List<string>
                            {
                                elevatorAddress,
                                DateTime.Now.ToString(),
                                eccpMaintenanceTemplateNode.TemplateNodeName,
                                maintenanceTypeName,
                                user.Name
                            };

                            byteArray = ImageHelper.PictureWatermark(imageBytes, watermarkContents);

                            var storedFile = new BinaryObject(this.AbpSession.TenantId, byteArray);
                            await this._binaryObjectManager.SaveAsync(storedFile);
                            eccpMaintenanceWorkFlow.ActionCodeValue = storedFile.Id.ToString();
                        }

                        eccpMaintenanceWorkFlow.MaintenanceWorkId = eccpMaintenanceWork.Id;
                        await _eccpMaintenanceWorkFlowRepository.InsertAsync(eccpMaintenanceWorkFlow);
                        if (eccpMaintenanceWorkFlow.ActionCodeValue == "false")
                        {
                            var eccpMaintenanceWorkFlow_Refuse_Link = new EccpMaintenanceWorkFlow_Refuse_Link();
                            eccpMaintenanceWorkFlow_Refuse_Link.MaintenanceWorkFlowId = eccpMaintenanceWorkFlow.Id;
                            eccpMaintenanceWorkFlow_Refuse_Link.TenantId = tenantId;
                            eccpMaintenanceWorkFlow_Refuse_Link.RefusePictureId = item.RefusePictureId;
                            await _eccpMaintenanceWorkFlow_Refuse_LinksRepository.InsertAsync(eccpMaintenanceWorkFlow_Refuse_Link);
                        }

                        if (item.workFlowItems.Count != 0)
                        {
                            foreach (var flowItem in item.workFlowItems)
                            {
                                var workFlowItem = this.ObjectMapper.Map<EccpMaintenanceWorkFlow_Item_Link>(flowItem);
                                workFlowItem.TenantId = tenantId;
                                workFlowItem.MaintenanceWorkFlowId = eccpMaintenanceWorkFlow.Id;
                                await this._eccpMaintenanceWorkFlowItemLinks.InsertAsync(workFlowItem);
                            }
                        }
                    }
                    if (m != null && item.DictMaintenanceWorkFlowStatusId == 2)
                    {
                        _eccpMaintenanceWorkFlowRepository.Delete(m);

                    }
                }

                var branchNode = _eccpMaintenanceTemplateNodeRepository.GetAll().FirstOrDefault(e =>
                   e.MaintenanceTemplateId == eccpMaintenanceWork.EccpMaintenanceTemplateId &&
                   e.DictNodeType.Name == "分支节点");
                if (branchNode == null)
                {
                    returnModel.flag = false;
                    returnModel.message = this.L("branchNodeIsNull");
                    return returnModel;
                }

                var branchChildNodes = _eccpMaintenanceTemplateNodeRepository.GetAll().Where(e =>
                   e.ParentNodeId == branchNode.Id).ToList();


                var workFlowsNodeIds = _eccpMaintenanceWorkFlowRepository.GetAll().Where(e =>
                   e.MaintenanceWorkId == eccpMaintenanceWork.Id).Select(s => s.MaintenanceTemplateNodeId).ToList();

                workFlowsNodeIds.AddRange(list.Select(s => s.MaintenanceTemplateNodeId));
                branchChildNodes = branchChildNodes.Where(w => !workFlowsNodeIds.Contains(w.Id)).ToList();


                if (branchChildNodes.Count != 0)
                {
                    returnModel.flag = false;
                    string msg = string.Join(",", branchChildNodes.Select(s => s.TemplateNodeName));
                    returnModel.message = this.L("此工单还有{" + msg + "}未处理 如需在其他维保人员手机上处理 可忽略此信息");
                    return returnModel;
                }

                var workOrder =
                    await this._eccpMaintenanceWorkOrderRepository.GetAsync(eccpMaintenanceWork
                        .MaintenanceWorkOrderId);
                workOrder.IsComplete = true;
                workOrder.ComplateDate = DateTime.Now;
                workOrder.MaintenanceStatusId = 3;
                await this._eccpMaintenanceWorkOrderRepository.UpdateAsync(workOrder);

                var resultCount =
                    this._eccpMaintenanceWorkOrderManager.CompletionWorkRearrangeWorkOrder(
                        eccpMaintenanceWork.MaintenanceWorkOrderId, workOrder.ComplateDate.Value);
                if (resultCount >= 0)
                {
                    returnModel.flag = true;
                    returnModel.message = this.L("工单完成");
                }
                else
                {
                    throw new Exception("计划删除计划工单刷新异常，错误代码：" + resultCount);
                }
                return returnModel;
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
        public async Task CreateOrEdit(CreateOrEditEccpMaintenanceWorkFlowDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await this._eccpMaintenanceWorkFlowRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpMaintenanceWorkFlowForView>> GetAll(
            GetAllEccpMaintenanceWorkFlowsInput input)
        {
            var filteredEccpMaintenanceWorkFlows = this._eccpMaintenanceWorkFlowRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.ActionCodeValue.Contains(input.Filter) || e.Remark.Contains(input.Filter)).WhereIf(
                    !string.IsNullOrWhiteSpace(input.ActionCodeValueFilter),
                    e => e.ActionCodeValue.ToLower() == input.ActionCodeValueFilter.ToLower().Trim());

            var query =
                (from o in filteredEccpMaintenanceWorkFlows
                 join o1 in this._eccpMaintenanceTemplateNodeRepository.GetAll() on o.MaintenanceTemplateNodeId equals
                     o1.Id into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpMaintenanceWorkRepository.GetAll() on o.MaintenanceWorkId equals o2.Id into j2
                 from s2 in j2.DefaultIfEmpty()
                 join o3 in this._eccpDictMaintenanceWorkFlowStatusRepository.GetAll() on
                     o.DictMaintenanceWorkFlowStatusId equals o3.Id into j3
                 from s3 in j3.DefaultIfEmpty()
                 select new
                 {
                     EccpMaintenanceWorkFlow = o,
                     EccpMaintenanceTemplateNodeNodeName = s1 == null ? string.Empty : s1.TemplateNodeName,
                     EccpMaintenanceWorkTaskName = s2 == null ? string.Empty : s2.TaskName,
                     EccpDictMaintenanceWorkFlowStatusName = s3 == null ? string.Empty : s3.Name
                 }).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpMaintenanceTemplateNodeNodeNameFilter),
                    e => e.EccpMaintenanceTemplateNodeNodeName.ToLower()
                         == input.EccpMaintenanceTemplateNodeNodeNameFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpMaintenanceWorkTaskNameFilter),
                    e => e.EccpMaintenanceWorkTaskName.ToLower()
                         == input.EccpMaintenanceWorkTaskNameFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpDictMaintenanceWorkFlowStatusNameFilter),
                    e => e.EccpDictMaintenanceWorkFlowStatusName.ToLower()
                         == input.EccpDictMaintenanceWorkFlowStatusNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var eccpMaintenanceWorkFlows = new List<GetEccpMaintenanceWorkFlowForView>();

            query.OrderBy(input.Sorting ?? "eccpMaintenanceWorkFlow.id asc").PageBy(input).MapTo(eccpMaintenanceWorkFlows);

            return new PagedResultDto<GetEccpMaintenanceWorkFlowForView>(totalCount, eccpMaintenanceWorkFlows);
        }

        /// <summary>
        /// The get all eccp dict maintenance work flow status for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows)]
        public async Task<PagedResultDto<EccpDictMaintenanceWorkFlowStatusLookupTableDto>> GetAllEccpDictMaintenanceWorkFlowStatusForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpDictMaintenanceWorkFlowStatusRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpDictMaintenanceWorkFlowStatusList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpDictMaintenanceWorkFlowStatusLookupTableDto>();
            foreach (var eccpDictMaintenanceWorkFlowStatus in eccpDictMaintenanceWorkFlowStatusList)
            {
                lookupTableDtoList.Add(
                    new EccpDictMaintenanceWorkFlowStatusLookupTableDto
                    {
                        Id = eccpDictMaintenanceWorkFlowStatus.Id,
                        DisplayName = eccpDictMaintenanceWorkFlowStatus.Name
                    });
            }

            return new PagedResultDto<EccpDictMaintenanceWorkFlowStatusLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp maintenance template node for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows)]
        public async Task<PagedResultDto<EccpMaintenanceTemplateNodeLookupTableDto>> GetAllEccpMaintenanceTemplateNodeForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpMaintenanceTemplateNodeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.TemplateNodeName.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpMaintenanceTemplateNodeList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpMaintenanceTemplateNodeLookupTableDto>();
            foreach (var eccpMaintenanceTemplateNode in eccpMaintenanceTemplateNodeList)
            {
                lookupTableDtoList.Add(
                    new EccpMaintenanceTemplateNodeLookupTableDto
                    {
                        Id = eccpMaintenanceTemplateNode.Id,
                        DisplayName = eccpMaintenanceTemplateNode.TemplateNodeName
                    });
            }

            return new PagedResultDto<EccpMaintenanceTemplateNodeLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp maintenance work for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows)]
        public async Task<PagedResultDto<EccpMaintenanceWorkLookupTableDto>> GetAllEccpMaintenanceWorkForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpMaintenanceWorkRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.TaskName.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpMaintenanceWorkList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpMaintenanceWorkLookupTableDto>();
            foreach (var eccpMaintenanceWork in eccpMaintenanceWorkList)
            {
                lookupTableDtoList.Add(
                    new EccpMaintenanceWorkLookupTableDto
                    {
                        Id = eccpMaintenanceWork.Id,
                        DisplayName = eccpMaintenanceWork.TaskName
                    });
            }

            return new PagedResultDto<EccpMaintenanceWorkLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get eccp maintenance work flow for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Edit)]
        public async Task<GetEccpMaintenanceWorkFlowForEditOutput> GetEccpMaintenanceWorkFlowForEdit(
            EntityDto<Guid> input)
        {
            var eccpMaintenanceWorkFlow = await this._eccpMaintenanceWorkFlowRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpMaintenanceWorkFlowForEditOutput
            {
                EccpMaintenanceWorkFlow =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenanceWorkFlowDto>(
                                         eccpMaintenanceWorkFlow)
            };
            var eccpMaintenanceTemplateNode =
                await this._eccpMaintenanceTemplateNodeRepository.FirstOrDefaultAsync(
                    output.EccpMaintenanceWorkFlow.MaintenanceTemplateNodeId);
            output.EccpMaintenanceTemplateNodeNodeName = eccpMaintenanceTemplateNode.TemplateNodeName;
            var eccpMaintenanceWork =
                await this._eccpMaintenanceWorkRepository.FirstOrDefaultAsync(
                    output.EccpMaintenanceWorkFlow.MaintenanceWorkId);
            output.EccpMaintenanceWorkTaskName = eccpMaintenanceWork.TaskName;
            var eccpDictMaintenanceWorkFlowStatus =
                await this._eccpDictMaintenanceWorkFlowStatusRepository.FirstOrDefaultAsync(
                    output.EccpMaintenanceWorkFlow.DictMaintenanceWorkFlowStatusId);
            output.EccpDictMaintenanceWorkFlowStatusName = eccpDictMaintenanceWorkFlowStatus.Name;

            return output;
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Create)]
        private async Task Create(CreateOrEditEccpMaintenanceWorkFlowDto input)
        {
            var eccpMaintenanceWorkFlow = this.ObjectMapper.Map<EccpMaintenanceWorkFlow>(input);

            if (this.AbpSession.TenantId != null)
            {
                eccpMaintenanceWorkFlow.TenantId = (int)this.AbpSession.TenantId;
            }

            await this._eccpMaintenanceWorkFlowRepository.InsertAsync(eccpMaintenanceWorkFlow);
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Edit)]
        private async Task Update(CreateOrEditEccpMaintenanceWorkFlowDto input)
        {
            if (input.Id != null)
            {
                var eccpMaintenanceWorkFlow =
                    await this._eccpMaintenanceWorkFlowRepository.FirstOrDefaultAsync((Guid)input.Id);
                this.ObjectMapper.Map(input, eccpMaintenanceWorkFlow);
            }
        }
    }
}