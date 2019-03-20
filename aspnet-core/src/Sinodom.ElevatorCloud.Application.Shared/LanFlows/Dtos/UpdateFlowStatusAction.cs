// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceTroubledWorkOrdersInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
 
    /// <summary>
    /// The get all eccp maintenance troubled work orders input.
    /// </summary>
    public class UpdateFlowStatusAction 
    {
        public List<string> ObjIds { get; set; }
        public int FlowStatusActionId { get; set; }
        public string ActionValue { get; set; }
        public string TableName { get; set; }
        public string SchemeType { get; set; }

    }
}