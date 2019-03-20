using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public class TestEccpMaintenanceTemplateNodesBuilder
    {
        private readonly ElevatorCloudDbContext _context;


        public TestEccpMaintenanceTemplateNodesBuilder(ElevatorCloudDbContext context)
        {
            this._context = context;
        }

        public void Create()
        {
            this.CreateMaintenanceTemplateNodes();
        }


        private void CreateMaintenanceTemplateNodes()
        {
            var maintenanceTemplateEntity = this._context.EccpMaintenanceTemplates.FirstOrDefault(e => e.TempName == "单元测试月度维保模板");
            var dictNodeTypeEntity = this._context.EccpDictNodeTypes.FirstOrDefault(e => e.Name == "判断节点");

            this._context.EccpMaintenanceTemplateNodes.Add(
                 new EccpMaintenanceTemplateNode
                 {
                     TemplateNodeName = "测试父父节点",
                     ParentNodeId = 0,
                     NodeDesc = "节点描述",
                     NodeIndex = 0,
                     ActionCode = "123123",
                     MaintenanceTemplateId = maintenanceTemplateEntity.Id,
                     DictNodeTypeId = dictNodeTypeEntity.Id,
                     NextNodeId = 0
                 });
            this._context.SaveChanges();

            EccpMaintenanceTemplateNode maintenanceTemplateNodeEntity = this._context.EccpMaintenanceTemplateNodes.FirstOrDefault(e => e.TemplateNodeName == "测试父父节点");

            this._context.EccpMaintenanceTemplateNodes.Add(
            new EccpMaintenanceTemplateNode
            {
                TemplateNodeName = "测试父节点",
                ParentNodeId = maintenanceTemplateNodeEntity.Id,
                NodeDesc = "节点描述",
                NodeIndex = 0,
                ActionCode = "123123",
                MaintenanceTemplateId = maintenanceTemplateEntity.Id,
                DictNodeTypeId = dictNodeTypeEntity.Id,
                NextNodeId = 0
            });
            this._context.SaveChanges();


            EccpMaintenanceTemplateNode maintenanceTemplateNode1Entity = this._context.EccpMaintenanceTemplateNodes.FirstOrDefault(e => e.TemplateNodeName == "测试父节点");

            this._context.EccpMaintenanceTemplateNodes.Add(
            new EccpMaintenanceTemplateNode
            {
                TemplateNodeName = "测试节点",
                ParentNodeId = maintenanceTemplateNode1Entity.Id,
                NodeDesc = "节点描述",
                NodeIndex = 0,
                ActionCode = "123123",
                MaintenanceTemplateId = maintenanceTemplateEntity.Id,
                DictNodeTypeId = dictNodeTypeEntity.Id,
                NextNodeId = 0
            });
            this._context.SaveChanges();

        }
    }
}
