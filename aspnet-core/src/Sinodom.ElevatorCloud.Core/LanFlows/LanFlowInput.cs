 

using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.LanFlows
{
	 
    public class LanFlow 
    {
        public int FlowStatusActionsId { get; set; }
        public string StatusValue { get; set; }
        public string StatusName { get; set; }
        public List<LanFlowState> LanFlowStates { get; set; }
        
    }

    public class LanFlowState
    { 
        public int FlowStatusActionsId { get; set; }
        public string ActionName { get; set; }
        public string ActionCode { get; set; }
        public string ActionDesc { get; set; }
    }
    

}