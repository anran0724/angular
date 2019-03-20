// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceTempWorkOrderForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos
{
    using System;

    /// <summary>
    ///     The get eccp maintenance temp work order for view.
    /// </summary>
    public class GetEccpMaintenanceTempWorkOrderForView
    {
        /// <summary>
        ///     Gets or sets the eccp base maintenance company name.
        /// </summary>
        public string ECCPBaseMaintenanceCompanyName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp maintenance temp work order.
        /// </summary>
        public EccpMaintenanceTempWorkOrderDto EccpMaintenanceTempWorkOrder { get; set; }

        /// <summary>
        ///     Gets or sets the temp order type name.
        /// </summary>
        public string TempOrderTypeName { get; set; }

        /// <summary>
        ///     Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }
        public string EccpBaseElevatorName { get; set; }
    }
}