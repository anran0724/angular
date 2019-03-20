using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    public class GetEccpMaintenanceWorkFlowsDto
    {
        public DateTime? MaintenanceLastModificationTime { get; set; }
        public string MaintenanceUserName { get; set; }
        public string TemplateNodeName { get; set; }
        public string NodeDesc { get; set; }
        public string ActionCodeValue { get; set; }
        public Guid? ProfilePictureId { get; set; }
        public string ActionCode { get; set; }
        public string LabelName { get; set; }
        public string InstallationAddress { get; set; }
        public JObject JObjActionCodeValue { get; set; }
        public string DictMaintenanceWorkFlowStatusName { get; set; }
    }
}
