// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceWorkOrderForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using System.Collections.Generic;

    /// <summary>
    /// The get eccp maintenance work order for edit output.
    /// </summary>
    public class GetEccpMaintenanceWorkOrderForEditOutput
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
        /// Gets or sets the eccp elevator installation address.
        /// </summary>
        public string EccpElevatorInstallationAddress { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance plan polling period.
        /// </summary>
        public string EccpMaintenancePlanPollingPeriod { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance user name and phones.
        /// </summary>
        public List<GetEccpMaintenanceUserNameAndPhoneDto> EccpMaintenanceUserNameAndPhones { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order.
        /// </summary>
        public CreateOrEditEccpMaintenanceWorkOrderDto EccpMaintenanceWorkOrder { get; set; }
    }
}