// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceWorkOrderForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using System.Collections.Generic;

    /// <summary>
    /// The get eccp maintenance work order for view.
    /// </summary>
    public class GetEccpMaintenanceWorkOrderForView
    {
        /// <summary>
        /// Gets or sets the eccp dict maintenance status name.
        /// </summary>
        public string EccpDictMaintenanceStatusName { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict maintenance type name.
        /// </summary>
        public string EccpDictMaintenanceTypeName { get; set; }

        /// <summary>
        /// Gets or sets the eccp elevator name.
        /// </summary>
        public string EccpElevatorName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance plan polling period.
        /// </summary>
        public string EccpMaintenancePlanPollingPeriod { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance user name list.
        /// </summary>
        public List<string> EccpMaintenanceUserNameList { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order.
        /// </summary>
        public EccpMaintenanceWorkOrderDto EccpMaintenanceWorkOrder { get; set; }

        /// <summary>
        /// Gets or sets the eccp property user name list.
        /// </summary>
        //public List<string> EccpPropertyUserNameList { get; set; }
    }
}