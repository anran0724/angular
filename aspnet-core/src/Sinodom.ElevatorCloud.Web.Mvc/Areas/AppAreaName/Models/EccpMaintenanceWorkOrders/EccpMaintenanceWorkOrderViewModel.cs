// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrderViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorkOrders
{
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;

    /// <summary>
    /// The eccp maintenance work order view model.
    /// </summary>
    public class EccpMaintenanceWorkOrderViewModel : GetEccpMaintenanceWorkOrderForView
    {
        public GetEccpBaseElevatorsInfoDto EccpBaseElevatorsInfo { get; set; }
        public List<GetEccpMaintenanceWorkFlowsDto> EccpMaintenanceWorkFlowsList { get; set; }
    }
}