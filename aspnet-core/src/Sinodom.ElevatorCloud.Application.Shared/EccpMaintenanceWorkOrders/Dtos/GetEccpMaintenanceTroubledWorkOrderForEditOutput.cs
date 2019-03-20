// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceTroubledWorkOrderForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    /// <summary>
    /// The get eccp maintenance troubled work order for edit output.
    /// </summary>
    public class GetEccpMaintenanceTroubledWorkOrderForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp maintenance troubled work order.
        /// </summary>
        public CreateOrEditEccpMaintenanceTroubledWorkOrderDto EccpMaintenanceTroubledWorkOrder { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order remark.
        /// </summary>
        public string EccpMaintenanceWorkOrderRemark { get; set; }
    }
}