 

using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.LanFlows
{
	 
    public class LanFlowInput
    {
        public int FlowStatusActionsId { get; set; }
        public string SchemeName { get; set; }
        public string SchemeType { get; set; }
        public string UserRoleCode { get; set; }
        public int AuthorizeType { get; set; }

    }

    public class UpdateLanFlowInput
    {
        public List<string> ObjIds { get; set; }
        public int  FlowStatusActionId { get; set; } 
        public string ActionValue { get; set; }
        public string TableName { get; set; }
        public string SchemeType { get; set; }
    }



}