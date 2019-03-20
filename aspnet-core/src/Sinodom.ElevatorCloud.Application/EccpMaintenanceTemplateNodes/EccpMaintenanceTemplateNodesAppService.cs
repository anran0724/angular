// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTemplateNodesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes
{
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
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpDict.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks;

    using GetAllForLookupTableInput = Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos.GetAllForLookupTableInput;

    /// <summary>
    /// The eccp maintenance template nodes app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes)]
    public class EccpMaintenanceTemplateNodesAppService : ElevatorCloudAppServiceBase,
                                                          IEccpMaintenanceTemplateNodesAppService
    {
        /// <summary>
        /// The _eccp dict maintenance item repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceItem, int> _eccpDictMaintenanceItemRepository;

        /// <summary>
        /// The _eccp dict node type repository.
        /// </summary>
        private readonly IRepository<EccpDictNodeType, int> _eccpDictNodeTypeRepository;

        /// <summary>
        /// The _eccp maintenance template node_ dict maintenance item_ link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTemplateNode_DictMaintenanceItem_Link, long> _eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository;

        /// <summary>
        /// The _eccp maintenance template node repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTemplateNode> _eccpMaintenanceTemplateNodeRepository;

        /// <summary>
        /// The _eccp maintenance template repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTemplate, int> _eccpMaintenanceTemplateRepository;

        /// <summary>
        /// The _eccp maintenance work.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWork> _eccpMaintenanceWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTemplateNodesAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceTemplateNodeRepository">
        /// The eccp maintenance template node repository.
        /// </param>
        /// <param name="eccpMaintenanceTemplateRepository">
        /// The eccp maintenance template repository.
        /// </param>
        /// <param name="eccpDictNodeTypeRepository">
        /// The eccp dict node type repository.
        /// </param>
        /// <param name="eccpDictMaintenanceItemRepository">
        /// The eccp dict maintenance item repository.
        /// </param>
        /// <param name="eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository">
        /// The eccp maintenance template node_ dict maintenance item_ link repository.
        /// </param>
        /// <param name="eccpMaintenanceWork">
        /// The eccp maintenance work.
        /// </param>
        public EccpMaintenanceTemplateNodesAppService(
            IRepository<EccpMaintenanceTemplateNode> eccpMaintenanceTemplateNodeRepository,
            IRepository<EccpMaintenanceTemplate, int> eccpMaintenanceTemplateRepository,
            IRepository<EccpDictNodeType, int> eccpDictNodeTypeRepository,
            IRepository<EccpDictMaintenanceItem, int> eccpDictMaintenanceItemRepository,
            IRepository<EccpMaintenanceTemplateNode_DictMaintenanceItem_Link, long> eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository,
            IRepository<EccpMaintenanceWork> eccpMaintenanceWork)
        {
            this._eccpMaintenanceTemplateNodeRepository = eccpMaintenanceTemplateNodeRepository;
            this._eccpMaintenanceTemplateRepository = eccpMaintenanceTemplateRepository;
            this._eccpDictNodeTypeRepository = eccpDictNodeTypeRepository;
            this._eccpDictMaintenanceItemRepository = eccpDictMaintenanceItemRepository;
            this._eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository =
                eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository;
            this._eccpMaintenanceWork = eccpMaintenanceWork;
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
        public async Task CreateOrEdit(CreateOrEditEccpMaintenanceTemplateNodeDto input)
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
        /// <param name="pid">
        /// The pid.
        /// </param>
        /// <param name="maintenanceTypeId">
        /// The maintenance type id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Delete)]
        public async Task Delete(EntityDto input, int pid, int maintenanceTypeId)
        {
            if (pid == 0)
            {
                var tempModel = await this._eccpMaintenanceTemplateRepository.FirstOrDefaultAsync(maintenanceTypeId);
                tempModel.TempNodeCount -= 1;
                this._eccpMaintenanceTemplateRepository.Update(tempModel);
            }

            await this._eccpMaintenanceTemplateNodeRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpMaintenanceTemplateNodeForView>> GetAll(
            GetAllEccpMaintenanceTemplateNodesInput input)
        {
            var filteredEccpMaintenanceTemplateNodes = this._eccpMaintenanceTemplateNodeRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => false || e.TemplateNodeName.Contains(input.Filter) || e.NodeDesc.Contains(input.Filter)
                         || e.ActionCode.Contains(input.Filter))
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.NodeNameFilter),
                    e => e.TemplateNodeName.ToLower() == input.NodeNameFilter.ToLower().Trim())
                .WhereIf(input.MinNodeIndexFilter != null, e => e.NodeIndex >= input.MinNodeIndexFilter)
                .WhereIf(input.MaxNodeIndexFilter != null, e => e.NodeIndex <= input.MaxNodeIndexFilter).WhereIf(
                    !string.IsNullOrWhiteSpace(input.ActionCodeFilter),
                    e => e.ActionCode.ToLower() == input.ActionCodeFilter.ToLower().Trim());

            var query =
                (from o in filteredEccpMaintenanceTemplateNodes
                 join o1 in this._eccpMaintenanceTemplateRepository.GetAll() on o.MaintenanceTemplateId equals o1.Id
                     into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpDictNodeTypeRepository.GetAll() on o.DictNodeTypeId equals o2.Id into j2
                 from s2 in j2.DefaultIfEmpty()
                 select new 
                 {
                     EccpMaintenanceTemplateNode = o,
                     EccpMaintenanceNextNodeName = s1 == null ? string.Empty : s1.TempName,
                     EccpDictNodeTypeName = s2 == null ? string.Empty : s2.Name
                 }).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpMaintenanceTemplateTempNameFilter),
                    e => e.EccpMaintenanceNextNodeName.ToLower()
                         == input.EccpMaintenanceTemplateTempNameFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpDictNodeTypeNameFilter),
                    e => e.EccpDictNodeTypeName.ToLower() == input.EccpDictNodeTypeNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var eccpMaintenanceTemplateNodes = new List<GetEccpMaintenanceTemplateNodeForView>();

            query.OrderBy(input.Sorting ?? "eccpMaintenanceTemplateNode.id asc").PageBy(input).MapTo(eccpMaintenanceTemplateNodes);

            return new PagedResultDto<GetEccpMaintenanceTemplateNodeForView>(totalCount, eccpMaintenanceTemplateNodes);
        }

        /// <summary>
        /// The get all eccp dict node type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes)]
        public async Task<PagedResultDto<EccpDictNodeTypeLookupTableDto>> GetAllEccpDictNodeTypeForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpDictNodeTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpDictNodeTypeList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpDictNodeTypeLookupTableDto>();
            foreach (var eccpDictNodeType in eccpDictNodeTypeList)
            {
                lookupTableDtoList.Add(
                    new EccpDictNodeTypeLookupTableDto
                    {
                        Id = eccpDictNodeType.Id,
                        DisplayName = eccpDictNodeType.Name
                    });
            }

            return new PagedResultDto<EccpDictNodeTypeLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// 获取下一步节点集合
        /// </summary>
        /// <param name="input">
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes)]
        public async Task<PagedResultDto<EccpMaintenanceNextNodeLookupTableDto>> GetAllEccpMaintenanceNextNodeForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpMaintenanceTemplateNodeRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.TemplateNodeName.ToString().Contains(input.Filter))
                .Where(e => e.MaintenanceTemplateId == input.MaintenanceTemplateId);

            var totalCount = await query.CountAsync();

            var eccpMaintenanceTemplateList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpMaintenanceNextNodeLookupTableDto>();
            foreach (var eccpMaintenanceTemplate in eccpMaintenanceTemplateList)
            {
                lookupTableDtoList.Add(
                    new EccpMaintenanceNextNodeLookupTableDto
                    {
                        Id = eccpMaintenanceTemplate.Id,
                        DisplayName = eccpMaintenanceTemplate.TemplateNodeName,
                        NodeIndex = eccpMaintenanceTemplate.NodeIndex
                    });
            }

            return new PagedResultDto<EccpMaintenanceNextNodeLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get app maintenance template nodes.
        /// </summary>
        /// <param name="maintenanceWorkOrderId">
        /// The maintenance work order id.
        /// </param>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        public List<AppEccpMaintenanceTemplateNodeTreeDto> GetAppMaintenanceTemplateNodes(int maintenanceWorkOrderId)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpMaintenanceWork = this._eccpMaintenanceWork.GetAll()
                    .FirstOrDefault(e => e.MaintenanceWorkOrderId == maintenanceWorkOrderId);

                if (eccpMaintenanceWork != null)
                {
                    var templateId = eccpMaintenanceWork.EccpMaintenanceTemplateId;

                    var list = from o in this._eccpMaintenanceTemplateNodeRepository.GetAll()
                               join t in this._eccpDictNodeTypeRepository.GetAll() on o.DictNodeTypeId equals t.Id
                               where o.MaintenanceTemplateId == templateId && o.ParentNodeId == 0 && o.IsDeleted == false
                               select new AppEccpMaintenanceTemplateNodeTreeDto
                               {
                                   Id = o.Id,
                                   TemplateNodeName = o.TemplateNodeName,
                                   NodeIndex = o.NodeIndex,
                                   ActionCode = o.ActionCode,
                                   ParentNodeId = o.ParentNodeId.Value,
                                   NodeDesc = o.NodeDesc,
                                   NextNodeId = o.NextNodeId ?? 0,
                                   MaintenanceTemplateId = o.MaintenanceTemplateId,
                                   DictNodeTypeId = o.DictNodeTypeId,
                                   DictNodeName = t.Name,
                                   SpareNodeId =  o.SpareNodeId ?? 0,
                                   MustDo = o.MustDo
                               };
                    var nodeList = this.GetAppNodes(list.ToList());
                    return nodeList;
                }
            }

            return null;
        }

        /// <summary>
        /// The get app nodes.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        public List<AppEccpMaintenanceTemplateNodeTreeDto> GetAppNodes(List<AppEccpMaintenanceTemplateNodeTreeDto> list)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var nodeList = new List<AppEccpMaintenanceTemplateNodeTreeDto>();

                if (list.Count != 0)
                {
                    foreach (var item in list)
                    {
                        var maintenanceLinkItems = this._eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository
                            .GetAll().Where(e => e.MaintenanceTemplateNodeId == item.Id);
                        var eccpDictMaintenanceItemsList =
                            from maintenanceItem in this._eccpDictMaintenanceItemRepository.GetAll()
                            join maintenanceItemLink in maintenanceLinkItems on maintenanceItem.Id equals maintenanceItemLink.DictMaintenanceItemId
                            select new EccpDictMaintenanceItemDto
                            {
                                Id = maintenanceItem.Id,
                                Name = maintenanceItem.Name,
                                DisOrder = maintenanceItem.DisOrder,
                                TermCode = maintenanceItem.TermCode,
                                TermDesc = maintenanceItem.TermDesc
                            };
                        var childList = from o in this._eccpMaintenanceTemplateNodeRepository.GetAll()
                                        join t in this._eccpDictNodeTypeRepository.GetAll() on o.DictNodeTypeId equals
                                            t.Id
                                        where o.ParentNodeId == item.Id
                                        select new AppEccpMaintenanceTemplateNodeTreeDto
                                        {
                                            Id = o.Id,
                                            TemplateNodeName = o.TemplateNodeName,
                                            NodeIndex = o.NodeIndex,
                                            ActionCode = o.ActionCode,
                                            ParentNodeId = o.ParentNodeId.Value,
                                            NodeDesc = o.NodeDesc,
                                            NextNodeId = o.NextNodeId ?? 0,
                                            MaintenanceTemplateId = o.MaintenanceTemplateId,
                                            DictNodeTypeId = o.DictNodeTypeId,
                                            DictNodeName = t.Name,
                                            SpareNodeId = o.SpareNodeId ?? 0,
                                            MustDo = o.MustDo
                                        };
                        item.eccpDictMaintenanceItemsList = eccpDictMaintenanceItemsList.Count() != 0 ? eccpDictMaintenanceItemsList.OrderByDescending(e=>e.DisOrder).ToList() : new List<EccpDictMaintenanceItemDto>();

                        var childNodeList = childList.ToList();
                        item.ChildNode = childNodeList.Count != 0 ? this.GetAppNodes(childNodeList) : new List<AppEccpMaintenanceTemplateNodeTreeDto>();

                        nodeList.Add(item);
                    }
                }

                return nodeList;
            }
        }

        /// <summary>
        /// The get eccp maintenance template node for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Edit)]
        public async Task<GetEccpMaintenanceTemplateNodeForEditOutput> GetEccpMaintenanceTemplateNodeForEdit(
            EntityDto input)
        {
            var eccpMaintenanceTemplateNode =
                await this._eccpMaintenanceTemplateNodeRepository.FirstOrDefaultAsync(input.Id);

            var eccpDictMaintenanceItems = await this._eccpDictMaintenanceItemRepository.GetAll().Select(
                                               r => new EccpDictMaintenanceItemTemplateNodeDto
                                               {
                                                   DictMaintenanceItemID = r.Id,
                                                   Name = r.Name,
                                                   DisOrder = r.DisOrder,
                                                   IsAssigned = false
                                               }).OrderByDescending(e => e.DisOrder).ToArrayAsync();

            var eccpDictMaintenanceItemLinks = this._eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository
                .GetAll().Where(e => e.MaintenanceTemplateNodeId == input.Id);

            foreach (var eccpDictMaintenanceItemLink in eccpDictMaintenanceItemLinks)
            {
                var defaultItem = eccpDictMaintenanceItems.FirstOrDefault(
                    e => e.DictMaintenanceItemID == eccpDictMaintenanceItemLink.DictMaintenanceItemId);
                if (defaultItem != null)
                {
                    defaultItem.IsAssigned = true;
                }
            }

            var output = new GetEccpMaintenanceTemplateNodeForEditOutput
            {
                EccpMaintenanceTemplateNode =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenanceTemplateNodeDto>(
                                         eccpMaintenanceTemplateNode)
            };

            if (output.EccpMaintenanceTemplateNode.NextNodeId != null
                && output.EccpMaintenanceTemplateNode.NextNodeId != 0)
            {
                var eccpMaintenanceNextNode =
                    await this._eccpMaintenanceTemplateNodeRepository.FirstOrDefaultAsync(
                        e => e.Id == (int)output.EccpMaintenanceTemplateNode.NextNodeId);
                output.EccpMaintenanceNextNodeName = eccpMaintenanceNextNode.TemplateNodeName;
            }

            if (output.EccpMaintenanceTemplateNode.SpareNodeId != null
            && output.EccpMaintenanceTemplateNode.SpareNodeId != 0)
            {
                var eccpSpareNode =
                    await this._eccpMaintenanceTemplateNodeRepository.FirstOrDefaultAsync(
                        e => e.Id == (int)output.EccpMaintenanceTemplateNode.SpareNodeId);
                output.EccpMaintenanceSpareNodeName = eccpSpareNode.TemplateNodeName;
            }


            if (output.EccpMaintenanceTemplateNode.DictNodeTypeId != 0)
            {
                var eccpDictNodeType =
                    await this._eccpDictNodeTypeRepository.FirstOrDefaultAsync(
                        e => e.Id == output.EccpMaintenanceTemplateNode.DictNodeTypeId);
                output.EccpDictNodeTypeName = eccpDictNodeType.Name;
            }

            output.eccpDictMaintenanceItemDtos = eccpDictMaintenanceItems;

            return output;
        }

        /// <summary>
        /// The get maintenance template nodes.
        /// </summary>
        /// <param name="templateId">
        /// The template id.
        /// </param>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        public List<EccpMaintenanceTemplateNodeTreeDto> GetMaintenanceTemplateNodes(int templateId)
        {
            var list = from o in this._eccpMaintenanceTemplateNodeRepository.GetAll()
                       join t in this._eccpDictNodeTypeRepository.GetAll() on o.DictNodeTypeId equals t.Id
                       where o.MaintenanceTemplateId == templateId && o.ParentNodeId == 0 && o.IsDeleted == false
                       select new EccpMaintenanceTemplateNodeTreeDto
                       {
                           Id = o.Id,
                           TemplateNodeName = o.TemplateNodeName,
                           NodeIndex = o.NodeIndex,
                           ActionCode = o.ActionCode,
                           ParentNodeId = o.ParentNodeId.Value,
                           NodeDesc = o.NodeDesc,
                           NextNodeId = o.NextNodeId ?? 0,
                           MaintenanceTemplateId = o.MaintenanceTemplateId,
                           DictNodeTypeId = o.DictNodeTypeId,
                           DictNodeName = t.Name,
                           SpareNodeId = o.SpareNodeId ?? 0,
                           MustDo = o.MustDo
                       };
            var nodeList = this.GetNodes(list.ToList());
            return nodeList;
        }

        /// <summary>
        /// The get nodes.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        public List<EccpMaintenanceTemplateNodeTreeDto> GetNodes(List<EccpMaintenanceTemplateNodeTreeDto> list)
        {
            var nodeList = new List<EccpMaintenanceTemplateNodeTreeDto>();
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    var childList = from o in this._eccpMaintenanceTemplateNodeRepository.GetAll()
                                    join t in this._eccpDictNodeTypeRepository.GetAll() on o.DictNodeTypeId equals t.Id
                                    where o.ParentNodeId == item.Id
                                    select new EccpMaintenanceTemplateNodeTreeDto
                                    {
                                        Id = o.Id,
                                        TemplateNodeName = o.TemplateNodeName,
                                        NodeIndex = o.NodeIndex,
                                        ActionCode = o.ActionCode,
                                        ParentNodeId = o.ParentNodeId.Value,
                                        NodeDesc = o.NodeDesc,
                                        NextNodeId = o.NextNodeId ?? 0,
                                        MaintenanceTemplateId = o.MaintenanceTemplateId,
                                        DictNodeTypeId = o.DictNodeTypeId,
                                        DictNodeName = t.Name,
                                        SpareNodeId = o.SpareNodeId ?? 0,
                                        MustDo = o.MustDo
                                    };
                    var childNodeList = childList.ToList();
                    item.ChildNode = childNodeList.Count != 0 ? this.GetNodes(childNodeList) : new List<EccpMaintenanceTemplateNodeTreeDto>();

                    nodeList.Add(item);
                }
            }

            return nodeList;
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Create)]
        private async Task Create(CreateOrEditEccpMaintenanceTemplateNodeDto input)
        {
            var eccpMaintenanceTemplateNode = this.ObjectMapper.Map<EccpMaintenanceTemplateNode>(input);

            if (this.AbpSession.TenantId != null)
            {
                eccpMaintenanceTemplateNode.TenantId = this.AbpSession.TenantId;
            }

            if (input.ParentNodeId == 0)
            {
                var tempModel =
                    await this._eccpMaintenanceTemplateRepository.FirstOrDefaultAsync(input.MaintenanceTemplateId);
                tempModel.TempNodeCount += 1;
                this._eccpMaintenanceTemplateRepository.Update(tempModel);
            }

            if (input.NextNodeId == null)
            {
                eccpMaintenanceTemplateNode.NextNodeId = 0;
            }

            await this._eccpMaintenanceTemplateNodeRepository.InsertAsync(eccpMaintenanceTemplateNode);

            if (input.AssignedItemIds.Length != 0)
            {
                foreach (var i in input.AssignedItemIds)
                {
                    var itemLinkModel = new EccpMaintenanceTemplateNode_DictMaintenanceItem_Link
                    {
                        Sort = 0,
                        MaintenanceTemplateNodeId = eccpMaintenanceTemplateNode.Id,
                        DictMaintenanceItemId = i
                    };
                    await this._eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository.InsertAsync(
                        itemLinkModel);
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Edit)]
        private async Task Update(CreateOrEditEccpMaintenanceTemplateNodeDto input)
        {
            if (input.Id != null)
            {
                var eccpMaintenanceTemplateNode =
                    await this._eccpMaintenanceTemplateNodeRepository.FirstOrDefaultAsync((int)input.Id);
                if (input.NextNodeId == null)
                {
                    eccpMaintenanceTemplateNode.NextNodeId = 0;
                }

                this.ObjectMapper.Map(input, eccpMaintenanceTemplateNode);

                if (input.AssignedItemIds.Length != 0)
                {
                    await this._eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository.DeleteAsync(
                        e => e.MaintenanceTemplateNodeId == eccpMaintenanceTemplateNode.Id);

                    foreach (var i in input.AssignedItemIds)
                    {
                        var itemLinkModel = new EccpMaintenanceTemplateNode_DictMaintenanceItem_Link
                        {
                            Sort = 0,
                            MaintenanceTemplateNodeId = eccpMaintenanceTemplateNode.Id,
                            DictMaintenanceItemId = i
                        };

                        await this._eccpMaintenanceTemplateNodeDictMaintenanceItemLinkRepository.InsertAsync(
                            itemLinkModel);
                    }
                }
            }
        }
    }
}