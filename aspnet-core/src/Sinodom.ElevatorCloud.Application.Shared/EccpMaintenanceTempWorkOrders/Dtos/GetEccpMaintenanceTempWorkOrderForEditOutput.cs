// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceTempWorkOrderForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos
{
    /// <summary>
    /// The get eccp maintenance temp work order for edit output.
    /// </summary>
    public class GetEccpMaintenanceTempWorkOrderForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp dict temp work order type name.
        /// </summary>
        public string EccpDictTempWorkOrderTypeName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance temp work order.
        /// </summary>
        public CreateOrEditEccpMaintenanceTempWorkOrderDto EccpMaintenanceTempWorkOrder { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }
        public string EccpBaseElevatorName { get; set; }
    }
}