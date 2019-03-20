// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTemplateNodesAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpMaintenanceTemplateNodes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks;

    using Xunit;

    /// <summary>
    /// The eccp maintenance template nodes app service_ tests.
    /// </summary>
    public class EccpMaintenanceTemplateNodesAppService_Tests : AppTestBase
    {
        // TODO: 待优化，应改为使用系统种子数据或测试数据，而不是单元测试内自行创建数据

        /// <summary>
        /// The _maintenance template nodes app service.
        /// </summary>
        private readonly IEccpMaintenanceTemplateNodesAppService _maintenanceTemplateNodesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTemplateNodesAppService_Tests"/> class.
        /// </summary>
        public EccpMaintenanceTemplateNodesAppService_Tests()
        {
            this.LoginAsHostAdmin();
            this._maintenanceTemplateNodesAppService = this.Resolve<IEccpMaintenanceTemplateNodesAppService>();
            this.Resolve<IEccpMaintenanceWorksAppService>();
        }

        /// <summary>
        /// 手机端接口
        /// </summary>
        [Fact]
        public void Should_Get_AppEccpMaintenanceTemplateNodes()
        {
            EccpDictMaintenanceType dictMaintenanceTypeEntity;

            EccpDictElevatorType dictElevatorTypeEntity;

            EccpDictNodeType dictNodeTypeEntity;

            var maintenanceTemplateEntity = new EccpMaintenanceTemplate();

            EccpMaintenanceTemplateNode maintenanceTemplateNodeEntity;

            EccpMaintenanceTemplateNode maintenanceTemplateNode1Entity;

            EccpDictMaintenanceItem dictMaintenanceItemEntity;

            this.UsingDbContext(
                context =>
                    {
                        context.EccpDictMaintenanceTypes.Add(new EccpDictMaintenanceType { Name = "测试维保类型" });

                        context.EccpDictElevatorTypes.Add(new EccpDictElevatorType { Name = "测试电梯类型" });
                        context.SaveChanges();

                        dictMaintenanceTypeEntity =
                            context.EccpDictMaintenanceTypes.FirstOrDefault(e => e.Name == "测试维保类型");
                        dictElevatorTypeEntity = context.EccpDictElevatorTypes.FirstOrDefault(e => e.Name == "测试电梯类型");
                        context.EccpMaintenanceTemplates.Add(
                            new EccpMaintenanceTemplate
                                {
                                    TempName = "测试模板",
                                    TempDesc = "模板描述",
                                    TempAllow = "通过",
                                    TempDeny = "未通过",
                                    TempCondition = "saveCount",
                                    TempNodeCount = 1,
                                    ElevatorTypeId = dictElevatorTypeEntity.Id,
                                    MaintenanceTypeId = dictMaintenanceTypeEntity.Id
                                });
                        context.SaveChanges();
                        maintenanceTemplateEntity =
                            context.EccpMaintenanceTemplates.FirstOrDefault(e => e.TempName == "测试模板");
                        context.EccpDictNodeTypes.Add(new EccpDictNodeType { Name = "测试节点类型" });
                        context.SaveChanges();
                        dictNodeTypeEntity = context.EccpDictNodeTypes.FirstOrDefault(e => e.Name == "测试节点类型");

                        context.EccpMaintenanceTemplateNodes.Add(
                            new EccpMaintenanceTemplateNode
                                {
                                    TemplateNodeName = "测试父父节点12",
                                    ParentNodeId = 0,
                                    NodeDesc = "节点描述",
                                    NodeIndex = 0,
                                    ActionCode = "123123",
                                    MaintenanceTemplateId = maintenanceTemplateEntity.Id,
                                    DictNodeTypeId = dictNodeTypeEntity.Id,
                                    NextNodeId = 0
                                });
                        context.SaveChanges();
                        maintenanceTemplateNodeEntity =
                            context.EccpMaintenanceTemplateNodes.FirstOrDefault(e => e.TemplateNodeName == "测试父父节点12");
                        context.EccpMaintenanceTemplateNodes.Add(
                            new EccpMaintenanceTemplateNode
                                {
                                    TemplateNodeName = "测试父节点12",
                                    ParentNodeId = maintenanceTemplateNodeEntity.Id,
                                    NodeDesc = "节点描述",
                                    NodeIndex = 0,
                                    ActionCode = "123123",
                                    MaintenanceTemplateId = maintenanceTemplateEntity.Id,
                                    DictNodeTypeId = dictNodeTypeEntity.Id,
                                    NextNodeId = 0
                                });
                        context.SaveChanges();
                        maintenanceTemplateNode1Entity =
                            context.EccpMaintenanceTemplateNodes.FirstOrDefault(e => e.TemplateNodeName == "测试父节点12");
                        context.EccpMaintenanceTemplateNodes.Add(
                            new EccpMaintenanceTemplateNode
                                {
                                    TemplateNodeName = "测试节点12",
                                    ParentNodeId = maintenanceTemplateNode1Entity.Id,
                                    NodeDesc = "节点描述",
                                    NodeIndex = 0,
                                    ActionCode = "123123",
                                    MaintenanceTemplateId = maintenanceTemplateEntity.Id,
                                    DictNodeTypeId = dictNodeTypeEntity.Id,
                                    NextNodeId = 0
                                });
                        context.EccpDictMaintenanceItems.Add(
                            new EccpDictMaintenanceItem
                                {
                                    Name = "测试维保项目", TermCode = "10000", DisOrder = 1, TermDesc = "测试维保项目"
                                });
                        context.SaveChanges();
                        dictMaintenanceItemEntity =
                            context.EccpDictMaintenanceItems.FirstOrDefault(e => e.Name == "测试维保项目");

                        context.EccpMaintenanceTemplateNode_DictMaintenanceItem_Links.Add(
                            new EccpMaintenanceTemplateNode_DictMaintenanceItem_Link
                                {
                                    MaintenanceTemplateNodeId = maintenanceTemplateNode1Entity.Id,
                                    DictMaintenanceItemId = dictMaintenanceItemEntity.Id,
                                    Sort = 0
                                });
                    });
            var mainenancePlanEntity = new EccpMaintenancePlan();
            var maintenanceTypeEntity = new EccpDictMaintenanceType();
            var maintenanceStatusEntity = new EccpDictMaintenanceStatus();
            var maintenanceWorkOrderEntity = new EccpMaintenanceWorkOrder();
            this.UsingDbContext(
                context =>
                    {
                        mainenancePlanEntity = context.EccpMaintenancePlans.FirstOrDefault(e => e.PollingPeriod == 30);
                        context.EccpMaintenancePlan_Template_Links.Add(
                            new EccpMaintenancePlan_Template_Link
                                {
                                    TenantId = 1,
                                    MaintenancePlanId = mainenancePlanEntity.Id,
                                    MaintenanceTemplateId = maintenanceTemplateEntity.Id
                                });

                        maintenanceTypeEntity = context.EccpDictMaintenanceTypes.FirstOrDefault(e => e.Name == "半月维保");
                        maintenanceStatusEntity =
                            context.EccpDictMaintenanceStatuses.FirstOrDefault(e => e.Name == "进行中");
                        context.EccpMaintenanceWorkOrders.Add(
                            new EccpMaintenanceWorkOrder
                                {
                                    TenantId = 1,
                                    MaintenancePlanId = mainenancePlanEntity.Id,
                                    MaintenanceStatusId = maintenanceStatusEntity.Id,
                                    MaintenanceTypeId = maintenanceTypeEntity.Id,
                                    PlanCheckDate = DateTime.Now,
                                    Remark = "测试维保工单"
                                });
                        context.SaveChanges();

                        maintenanceWorkOrderEntity =
                            context.EccpMaintenanceWorkOrders.FirstOrDefault(e => e.Remark == "测试维保工单");
                        context.EccpMaintenanceWorks.Add(
                            new EccpMaintenanceWork
                                {
                                    TenantId = 1,
                                    TaskName = maintenanceTemplateEntity.TempName,
                                    MaintenanceWorkOrderId = maintenanceWorkOrderEntity.Id,
                                    EccpMaintenanceTemplateId = maintenanceTemplateEntity.Id,
                                    Remark = "测试工作"
                                });
                    });

            var list = this._maintenanceTemplateNodesAppService.GetAppMaintenanceTemplateNodes(
                maintenanceWorkOrderEntity.Id);
            list[0].ChildNode.Count.ShouldBe(1);
            list[0].ChildNode[0].ChildNode.Count.ShouldBe(1);
            list[0].ChildNode[0].eccpDictMaintenanceItemsList.Count.ShouldBe(1);
        }

        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task Should_Get_EccpMaintenanceTemplateNode()
        {
            // Arrange
            var tempName = "测试节点";

            this.UsingDbContext(
                context =>
                    {
                        var dictMaintenanceItemEntity = this.GetDictMaintenanceItemEntity("消防开关");
                        var maintenanceTemplateNodeEntity =
                            context.EccpMaintenanceTemplateNodes.FirstOrDefault(e => e.TemplateNodeName == tempName);

                        context.EccpMaintenanceTemplateNode_DictMaintenanceItem_Links.Add(
                            new EccpMaintenanceTemplateNode_DictMaintenanceItem_Link
                                {
                                    MaintenanceTemplateNodeId = maintenanceTemplateNodeEntity.Id,
                                    DictMaintenanceItemId = dictMaintenanceItemEntity.Id,
                                    Sort = 0
                                });
                    });

            var entity = this.GetEntity(tempName);

            var output =
                await this._maintenanceTemplateNodesAppService.GetEccpMaintenanceTemplateNodeForEdit(
                    new EntityDto(entity.Id));

            output.eccpDictMaintenanceItemDtos.Length.ShouldBe(73);
            output.EccpMaintenanceTemplateNode.TemplateNodeName.ShouldBe(tempName);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        [Fact]
        public void Should_Get_EccpMaintenanceTemplateNodes()
        {
            var maintenanceTemplateEntity = this.GetMaintenanceTemplateEntity("单元测试月度维保模板");

            var list = this._maintenanceTemplateNodesAppService.GetMaintenanceTemplateNodes(
                maintenanceTemplateEntity.Id);
            list[0].ChildNode.Count.ShouldBe(1);
            list[0].ChildNode[0].ChildNode.Count.ShouldBe(1);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task Test_Create_EccpMaintenanceTemplateNodes()
        {
            // Arrange
            var tempName = "测试节点12";
            var AssignedItemIds = new List<int>();

            var dictMaintenanceItemEntity = this.GetDictMaintenanceItemEntity("消防开关");
            AssignedItemIds.Add(dictMaintenanceItemEntity.Id);

            var newDictMaintenanceItemEntity = this.GetDictMaintenanceItemEntity("导电回路绝缘性能测试");
            AssignedItemIds.Add(newDictMaintenanceItemEntity.Id);

            var dictNodeTypeEntity = this.GetEccpDictNodeTypeEntity("判断节点");

            var maintenanceTemplateEntity = new EccpMaintenanceTemplate();
            var maintenanceTemplateNodeEntity = new EccpMaintenanceTemplateNode();

            this.UsingDbContext(
                context =>
                    {
                        context.EccpMaintenanceTemplates.Add(
                            new EccpMaintenanceTemplate
                                {
                                    TempName = "测试模板12",
                                    TempDesc = "模板描述",
                                    TempAllow = "通过",
                                    TempDeny = "未通过",
                                    TempCondition = "saveCount",
                                    TempNodeCount = 1,
                                    ElevatorTypeId = 12,
                                    MaintenanceTypeId = 2
                                });
                        context.SaveChanges();

                        maintenanceTemplateEntity =
                            context.EccpMaintenanceTemplates.FirstOrDefault(e => e.TempName == "测试模板");

                        context.EccpMaintenanceTemplateNodes.Add(
                            new EccpMaintenanceTemplateNode
                                {
                                    TemplateNodeName = "测试父节点12",
                                    ParentNodeId = 0,
                                    NodeDesc = "节点描述",
                                    NodeIndex = 0,
                                    ActionCode = "123123",
                                    MaintenanceTemplateId = maintenanceTemplateEntity.Id,
                                    DictNodeTypeId = dictNodeTypeEntity.Id,
                                    NextNodeId = 0
                                });
                        context.SaveChanges();

                        maintenanceTemplateNodeEntity =
                            context.EccpMaintenanceTemplateNodes.FirstOrDefault(e => e.TemplateNodeName == "测试父节点12");
                    });

            // Act
            await this._maintenanceTemplateNodesAppService.CreateOrEdit(
                new CreateOrEditEccpMaintenanceTemplateNodeDto
                    {
                        TemplateNodeName = tempName,
                        ParentNodeId = maintenanceTemplateNodeEntity.Id,
                        NodeDesc = "节点描述",
                        NodeIndex = 0,
                        ActionCode = "123123",
                        MaintenanceTemplateId = maintenanceTemplateEntity.Id,
                        DictNodeTypeId = dictNodeTypeEntity.Id,
                        NextNodeId = maintenanceTemplateNodeEntity.Id,
                        AssignedItemIds = AssignedItemIds.ToArray()
                    });

            // Assert
            this.UsingDbContext(
                context =>
                    {
                        var entity =
                            context.EccpMaintenanceTemplateNodes.FirstOrDefault(e => e.TemplateNodeName == tempName);
                        entity.ShouldNotBeNull();

                        var eccpMaintenanceTemplateNodeItem =
                            context.EccpMaintenanceTemplateNode_DictMaintenanceItem_Links.Where(
                                e => e.MaintenanceTemplateNodeId == entity.Id);
                        eccpMaintenanceTemplateNodeItem.Count().ShouldBe(2);

                        maintenanceTemplateEntity =
                            context.EccpMaintenanceTemplates.FirstOrDefault(e => e.TempName == "测试模板12");
                        maintenanceTemplateEntity.TempNodeCount.ShouldBe(1);
                    });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task Test_Delete_EccpMaintenanceTemplateNodes()
        {
            var dictNodeTypeEntity = this.GetEccpDictNodeTypeEntity("判断节点");

            var maintenanceTemplateEntity = new EccpMaintenanceTemplate();
            var maintenanceTemplateNodeEntity = new EccpMaintenanceTemplateNode();

            // Arrange
            this.UsingDbContext(
                context =>
                    {
                        context.EccpMaintenanceTemplates.Add(
                            new EccpMaintenanceTemplate
                                {
                                    TempName = "测试模板",
                                    TempDesc = "模板描述",
                                    TempAllow = "通过",
                                    TempDeny = "未通过",
                                    TempCondition = "saveCount",
                                    TempNodeCount = 1,
                                    ElevatorTypeId = 12,
                                    MaintenanceTypeId = 2
                                });
                        context.SaveChanges();

                        maintenanceTemplateEntity =
                            context.EccpMaintenanceTemplates.FirstOrDefault(e => e.TempName == "测试模板");

                        context.EccpMaintenanceTemplateNodes.Add(
                            new EccpMaintenanceTemplateNode
                                {
                                    TemplateNodeName = "测试父节点",
                                    ParentNodeId = 0,
                                    NodeDesc = "节点描述",
                                    NodeIndex = 0,
                                    ActionCode = "123123",
                                    MaintenanceTemplateId = maintenanceTemplateEntity.Id,
                                    DictNodeTypeId = dictNodeTypeEntity.Id,
                                    NextNodeId = 0
                                });
                        context.SaveChanges();

                        maintenanceTemplateNodeEntity =
                            context.EccpMaintenanceTemplateNodes.FirstOrDefault(e => e.TemplateNodeName == "测试父节点");
                    });

            // Act
            await this._maintenanceTemplateNodesAppService.Delete(
                new EntityDto(maintenanceTemplateNodeEntity.Id),
                maintenanceTemplateNodeEntity.ParentNodeId.Value,
                maintenanceTemplateEntity.Id);

            // Assert
            this.GetEntity(maintenanceTemplateNodeEntity.Id).IsDeleted.ShouldBeTrue();

            this.UsingDbContext(
                context =>
                    {
                        maintenanceTemplateEntity =
                            context.EccpMaintenanceTemplates.FirstOrDefault(e => e.TempName == "单元测试月度维保模板");
                        maintenanceTemplateEntity.TempNodeCount.ShouldBe(0);
                    });
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task Test_Update_EccpMaintenanceTemplateNodes()
        {
            // Arrange          
            var newName = "测试节点3";

            var maintenanceTemplateEntity = this.GetMaintenanceTemplateEntity("单元测试月度维保模板");

            var entity = this.GetEntity("测试节点");

            var AssignedItemIds = new List<int>();

            var dictMaintenanceItemEntity = this.GetDictMaintenanceItemEntity("消防开关");
            AssignedItemIds.Add(dictMaintenanceItemEntity.Id);

            var newDictMaintenanceItemEntity = this.GetDictMaintenanceItemEntity("导电回路绝缘性能测试");
            AssignedItemIds.Add(newDictMaintenanceItemEntity.Id);

            var dictNodeTypeEntity = this.GetEccpDictNodeTypeEntity("判断节点");

            // Act
            await this._maintenanceTemplateNodesAppService.CreateOrEdit(
                new CreateOrEditEccpMaintenanceTemplateNodeDto
                    {
                        Id = entity.Id,
                        TemplateNodeName = newName,
                        ParentNodeId = 0,
                        NodeDesc = "节点描述",
                        NodeIndex = 0,
                        ActionCode = "123123",
                        MaintenanceTemplateId = maintenanceTemplateEntity.Id,
                        DictNodeTypeId = dictNodeTypeEntity.Id,
                        NextNodeId = 0,
                        AssignedItemIds = AssignedItemIds.ToArray()
                    });

            // Assert
            this.GetEntity(entity.Id).TemplateNodeName.ShouldBe(newName);

            this.UsingDbContext(
                context =>
                    {
                        var eccpMaintenanceTemplateNodeItem =
                            context.EccpMaintenanceTemplateNode_DictMaintenanceItem_Links.Where(
                                e => e.MaintenanceTemplateNodeId == entity.Id);
                        eccpMaintenanceTemplateNodeItem.Count().ShouldBe(2);
                    });
        }

        /// <summary>
        /// The get dict maintenance item entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpDictMaintenanceItem"/>.
        /// </returns>
        private EccpDictMaintenanceItem GetDictMaintenanceItemEntity(string name)
        {
            var entity = this.UsingDbContext(
                context => context.EccpDictMaintenanceItems.FirstOrDefault(e => e.Name == name));
            entity.ShouldNotBeNull();

            return entity;
        }

        /// <summary>
        /// The get eccp dict node type entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpDictNodeType"/>.
        /// </returns>
        private EccpDictNodeType GetEccpDictNodeTypeEntity(string name)
        {
            var entity = this.UsingDbContext(context => context.EccpDictNodeTypes.FirstOrDefault(e => e.Name == name));
            entity.ShouldNotBeNull();

            return entity;
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpMaintenanceTemplateNode"/>.
        /// </returns>
        private EccpMaintenanceTemplateNode GetEntity(string name)
        {
            var entity = this.UsingDbContext(
                context => context.EccpMaintenanceTemplateNodes.FirstOrDefault(e => e.TemplateNodeName == name));
            entity.ShouldNotBeNull();

            return entity;
        }

        /// <summary>
        /// The get entity.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="EccpMaintenanceTemplateNode"/>.
        /// </returns>
        private EccpMaintenanceTemplateNode GetEntity(long id)
        {
            var entity = this.UsingDbContext(
                context => context.EccpMaintenanceTemplateNodes.FirstOrDefault(e => e.Id == id));
            entity.ShouldNotBeNull();

            return entity;
        }

        /// <summary>
        /// The get maintenance template entity.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="EccpMaintenanceTemplate"/>.
        /// </returns>
        private EccpMaintenanceTemplate GetMaintenanceTemplateEntity(string name)
        {
            var entity = this.UsingDbContext(
                context => context.EccpMaintenanceTemplates.FirstOrDefault(e => e.TempName == name));
            entity.ShouldNotBeNull();

            return entity;
        }
    }
}