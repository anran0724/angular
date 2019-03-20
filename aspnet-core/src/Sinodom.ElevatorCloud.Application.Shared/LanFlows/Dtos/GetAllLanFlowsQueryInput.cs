// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceTroubledWorkOrdersInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance troubled work orders input.
    /// </summary>
    public class GetAllLanFlowsQueryInput : PagedAndSortedResultRequestDto
    {
               
        public string TableName { get; set; }   
        public string SchemeType { get; set; }
        public int? FlowStatusActionId { get; set; }
        
    }
}