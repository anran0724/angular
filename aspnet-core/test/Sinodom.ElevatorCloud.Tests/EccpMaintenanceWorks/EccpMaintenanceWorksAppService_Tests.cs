// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorksAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpMaintenanceWorks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;

    using Xunit;

    /// <summary>
    /// The eccp maintenance works app service_ tests.
    /// </summary>
    public class EccpMaintenanceWorksAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _eccp maintenance work flows app service.
        /// </summary>
        private readonly IEccpMaintenanceWorkFlowsAppService _eccpMaintenanceWorkFlowsAppService;

        /// <summary>
        /// The _eccp maintenance works app service.
        /// </summary>
        private readonly IEccpMaintenanceWorksAppService _eccpMaintenanceWorksAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorksAppService_Tests"/> class.
        /// </summary>
        public EccpMaintenanceWorksAppService_Tests()
        {
            this.LoginAsDefaultTenantAdmin();
            this._eccpMaintenanceWorksAppService = this.Resolve<IEccpMaintenanceWorksAppService>();
            this._eccpMaintenanceWorkFlowsAppService = this.Resolve<IEccpMaintenanceWorkFlowsAppService>();
        }

        /// <summary>
        /// 获取工单
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task Should_Get_EccpMaintenanceWorks()
        {
            var mainenancePlanEntity = new EccpMaintenancePlan();
            var maintenanceTypeEntity = new EccpDictMaintenanceType();
            var maintenanceStatusEntity = new EccpDictMaintenanceStatus();
            var templateEntity = new EccpMaintenanceTemplate();

            var maintenanceWorkOrderEntity = new EccpMaintenanceWorkOrder();
            this.UsingDbContext(
                context =>
                    {
                        mainenancePlanEntity = context.EccpMaintenancePlans.FirstOrDefault(e => e.PollingPeriod == 30);
                        templateEntity = context.EccpMaintenanceTemplates.FirstOrDefault(e => e.TempName == "测试模板");
                        context.EccpMaintenancePlan_Template_Links.Add(
                            new EccpMaintenancePlan_Template_Link
                                {
                                    TenantId = this.AbpSession.TenantId.Value,
                                    MaintenancePlanId = mainenancePlanEntity.Id,
                                    MaintenanceTemplateId = templateEntity.Id
                                });

                        maintenanceTypeEntity = context.EccpDictMaintenanceTypes.FirstOrDefault(e => e.Name == "半月维保");
                        maintenanceStatusEntity =
                            context.EccpDictMaintenanceStatuses.FirstOrDefault(e => e.Name == "进行中");
                        context.EccpMaintenanceWorkOrders.Add(
                            new EccpMaintenanceWorkOrder
                                {
                                    TenantId = this.AbpSession.TenantId.Value,
                                    MaintenancePlanId = mainenancePlanEntity.Id,
                                    MaintenanceStatusId = maintenanceStatusEntity.Id,
                                    MaintenanceTypeId = maintenanceTypeEntity.Id,
                                    PlanCheckDate = DateTime.Now,
                                    Remark = "测试维保工单"
                                });
                        context.SaveChanges();

                        maintenanceWorkOrderEntity =
                            context.EccpMaintenanceWorkOrders.FirstOrDefault(e => e.Remark == "测试维保工单");
                    });

            await this._eccpMaintenanceWorksAppService.AppCreate(
                new CreateOrEditAppEccpMaintenanceWorkDto
                    {
                        MaintenanceWorkOrderId = maintenanceWorkOrderEntity.Id,
                        Remark = "Remark",
                        MaintenanceTypeId = maintenanceTypeEntity.Id
                    });
            this.UsingDbContext(
                context =>
                    {
                        var eccpMaintenanceWorkEntity = context.EccpMaintenanceWorks.FirstOrDefault(
                            e => e.MaintenanceWorkOrderId == maintenanceWorkOrderEntity.Id);
                        eccpMaintenanceWorkEntity.ShouldNotBeNull();
                        eccpMaintenanceWorkEntity.TaskName.ShouldBe("测试模板");
                    });
            var entities =
                await this._eccpMaintenanceWorksAppService.GetAppAll(
                    new GetAllEccpMaintenanceWorksInput { Filter = string.Empty });
            entities.Items.Count.ShouldBe(1);
        }

        /// <summary>
        /// 工单完成添加
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task Test_Create_EccpMaintenanceWorkFlows()
        {
            var mainenancePlanEntity = new EccpMaintenancePlan();
            var maintenanceTypeEntity = new EccpDictMaintenanceType();
            var maintenanceStatusEntity = new EccpDictMaintenanceStatus();
            var templateEntity = new EccpMaintenanceTemplate();
            var maintenanceWorkOrderEntity = new EccpMaintenanceWorkOrder();
            var maintenanceWork = new EccpMaintenanceWork();
            var maintenanceTemplateNode = new EccpMaintenanceTemplateNode();



            this.UsingDbContext(
                context =>
                    {
                        mainenancePlanEntity = context.EccpMaintenancePlans.FirstOrDefault(e => e.TenantId==1);
                        templateEntity = context.EccpMaintenanceTemplates.FirstOrDefault(e => e.TempName == "测试模板");
                        context.EccpMaintenancePlan_Template_Links.Add(
                            new EccpMaintenancePlan_Template_Link
                                {
                                    TenantId = this.AbpSession.TenantId.Value,
                                    MaintenancePlanId = mainenancePlanEntity.Id,
                                    MaintenanceTemplateId = templateEntity.Id
                                });

                        maintenanceTypeEntity = context.EccpDictMaintenanceTypes.FirstOrDefault(e => e.Name == "半月维保");
                        maintenanceStatusEntity =
                            context.EccpDictMaintenanceStatuses.FirstOrDefault(e => e.Name == "进行中");
                        context.EccpMaintenanceWorkOrders.Add(
                            new EccpMaintenanceWorkOrder
                                {
                                    TenantId = this.AbpSession.TenantId.Value,
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
                                    MaintenanceWorkOrderId = maintenanceWorkOrderEntity.Id,
                                    Remark = "Remark",
                                    TenantId = this.AbpSession.TenantId.Value,
                                    TaskName = "测试模板",
                                    EccpMaintenanceTemplateId = templateEntity.Id
                                });
                        context.SaveChanges();
                        maintenanceWork = context.EccpMaintenanceWorks.FirstOrDefault(e => e.TaskName == "测试模板");

                        context.EccpMaintenanceTemplateNodes.Add(
                            new EccpMaintenanceTemplateNode
                                {
                                    TemplateNodeName = "测试节点",
                                    NodeIndex = 0,
                                    TenantId = this.AbpSession.TenantId.Value,
                                    MaintenanceTemplateId = templateEntity.Id,
                                    DictNodeTypeId = 1
                                });
                        context.SaveChanges();
                        maintenanceTemplateNode =
                            context.EccpMaintenanceTemplateNodes.FirstOrDefault(e => e.TemplateNodeName == "测试节点");
                    });
            var list = new List<CreateOrEditEccpMaintenanceWorkFlowDto>
                           {
                               new CreateOrEditEccpMaintenanceWorkFlowDto
                                   {
                                       ActionCodeValue = "123123123",
                                       Remark = "备注",
                                       CreatorUserId =   AbpSession.UserId,
                                       MaintenanceWorkId = maintenanceWorkOrderEntity.Id,
                                       MaintenanceTemplateNodeId = maintenanceTemplateNode.Id,
                                       DictMaintenanceWorkFlowStatusId = 2,
                                       workFlowItems = new List<CreateAPPEccpMaintenanceWorkFlow_Item_LinkDto>
                                                           {
                                                               new CreateAPPEccpMaintenanceWorkFlow_Item_LinkDto
                                                                   {
                                                                       DictMaintenanceItemId = 1,
                                                                       Remark = "123",
                                                                       IsQualified = true
                                                                   }
                                                           }
                                   }
                           };
            var returnModel = await this._eccpMaintenanceWorkFlowsAppService.AppCreate(list);
            returnModel.flag.ShouldBeFalse();
            this.UsingDbContext(
                context =>
                    {
                        maintenanceWorkOrderEntity =
                            context.EccpMaintenanceWorkOrders.FirstOrDefault(e => e.Remark == "测试维保工单");
                        maintenanceWorkOrderEntity.IsComplete.ShouldBeFalse();

                        var eccpMaintenanceWorkFlow_Item_LinkEntity =
                            context.EccpMaintenanceWorkFlow_Item_Links.FirstOrDefault(
                                e => e.DictMaintenanceItemId == 1);

                        eccpMaintenanceWorkFlow_Item_LinkEntity.ShouldNotBeNull();
                    });

            var list1 = new List<CreateOrEditEccpMaintenanceWorkFlowDto>();
            var returnModel1 = await this._eccpMaintenanceWorkFlowsAppService.AppCreate(list1);
            returnModel1.flag.ShouldBeFalse();
            returnModel1.message.ShouldBe("请上传正确数据");

            var list2 = new List<CreateOrEditEccpMaintenanceWorkFlowDto>
                            {
                                new CreateOrEditEccpMaintenanceWorkFlowDto
                                    {
                                        ActionCodeValue = "xxxxx",
                                        Remark = "备注",
                                        CreatorUserId =   AbpSession.UserId,
                                        MaintenanceWorkId = maintenanceWorkOrderEntity.Id,
                                        MaintenanceTemplateNodeId = maintenanceTemplateNode.Id,
                                        DictMaintenanceWorkFlowStatusId = 1,
                                        workFlowItems = new List<CreateAPPEccpMaintenanceWorkFlow_Item_LinkDto>
                                                            {
                                                                new CreateAPPEccpMaintenanceWorkFlow_Item_LinkDto
                                                                    {
                                                                        DictMaintenanceItemId = 1,
                                                                        Remark = "123",
                                                                        IsQualified = true
                                                                    }
                                                            }
                                    }
                            };
            var returnModel2 = await this._eccpMaintenanceWorkFlowsAppService.AppCreate(list2);
            returnModel2.flag.ShouldBeFalse();
            returnModel2.message.ShouldBe("[branch node is null]");
        }

        /// <summary>
        /// 创建工单
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task Test_Create_EccpMaintenanceWorks()
        {
            var mainenancePlanEntity = new EccpMaintenancePlan();
            var maintenanceTypeEntity = new EccpDictMaintenanceType();
            var maintenanceStatusEntity = new EccpDictMaintenanceStatus();
            var templateEntity = new EccpMaintenanceTemplate();

            var maintenanceWorkOrderEntity = new EccpMaintenanceWorkOrder();
            this.UsingDbContext(
                context =>
                    {
                        mainenancePlanEntity = context.EccpMaintenancePlans.FirstOrDefault(e => e.PollingPeriod == 30);
                        templateEntity = context.EccpMaintenanceTemplates.FirstOrDefault(e => e.TempName == "测试模板");
                        context.EccpMaintenancePlan_Template_Links.Add(
                            new EccpMaintenancePlan_Template_Link
                                {
                                    TenantId = this.AbpSession.TenantId.Value,
                                    MaintenancePlanId = mainenancePlanEntity.Id,
                                    MaintenanceTemplateId = templateEntity.Id
                                });

                        maintenanceTypeEntity = context.EccpDictMaintenanceTypes.FirstOrDefault(e => e.Name == "半月维保");
                        maintenanceStatusEntity =
                            context.EccpDictMaintenanceStatuses.FirstOrDefault(e => e.Name == "进行中");
                        context.EccpMaintenanceWorkOrders.Add(
                            new EccpMaintenanceWorkOrder
                                {
                                    TenantId = this.AbpSession.TenantId.Value,
                                    MaintenancePlanId = mainenancePlanEntity.Id,
                                    MaintenanceStatusId = maintenanceStatusEntity.Id,
                                    MaintenanceTypeId = maintenanceTypeEntity.Id,
                                    PlanCheckDate = DateTime.Now,
                                    Remark = "测试维保工单"
                                });
                        context.SaveChanges();

                        maintenanceWorkOrderEntity =
                            context.EccpMaintenanceWorkOrders.FirstOrDefault(e => e.Remark == "测试维保工单");
                    });

            await this._eccpMaintenanceWorksAppService.AppCreate(
                new CreateOrEditAppEccpMaintenanceWorkDto
                    {
                        MaintenanceWorkOrderId = maintenanceWorkOrderEntity.Id,
                        Remark = "Remark",
                        MaintenanceTypeId = maintenanceTypeEntity.Id
                    });
            this.UsingDbContext(
                context =>
                    {
                        var eccpMaintenanceWorkEntity = context.EccpMaintenanceWorks.FirstOrDefault(
                            e => e.MaintenanceWorkOrderId == maintenanceWorkOrderEntity.Id);
                        eccpMaintenanceWorkEntity.ShouldNotBeNull();
                        eccpMaintenanceWorkEntity.TaskName.ShouldBe("测试模板");
                    });
        }
    }
}