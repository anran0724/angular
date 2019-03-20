// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceTroubledWorkOrderForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    /// <summary>
    /// The get eccp maintenance troubled work order for view.
    /// </summary>
    public class GetEccpMaintenanceTroubledWorkOrderForView
    {
        /// <summary>
        /// Gets or sets the eccp maintenance troubled work order.
        /// </summary>
        public EccpMaintenanceTroubledWorkOrderDto EccpMaintenanceTroubledWorkOrder { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order remark.
        /// </summary>
        public string EccpMaintenanceWorkOrderRemark { get; set; }
    }
}