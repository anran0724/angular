// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenancePlanForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    /// <summary>
    /// The get eccp maintenance plan for view.
    /// </summary>
    public class GetEccpMaintenancePlanForView
    {
        /// <summary>
        /// Gets or sets the eccp base elevator num.
        /// </summary>
        public int EccpBaseElevatorNum { get; set; }
        
        /// <summary>
        /// Gets or sets the eccp maintenance plan.
        /// </summary>
        public EccpMaintenancePlanDto EccpMaintenancePlan { get; set; }
    }
}