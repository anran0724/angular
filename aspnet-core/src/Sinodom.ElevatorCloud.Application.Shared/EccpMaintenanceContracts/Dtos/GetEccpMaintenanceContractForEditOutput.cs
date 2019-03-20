// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceContractForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos
{
    /// <summary>
    /// The get eccp maintenance contract for edit output.
    /// </summary>
    public class GetEccpMaintenanceContractForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp base elevators ids.
        /// </summary>
        public string EccpBaseElevatorsIds { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevators names.
        /// </summary>
        public string EccpBaseElevatorsNames { get; set; }

        /// <summary>
        /// Gets or sets the eccp base maintenance company name.
        /// </summary>
        public string ECCPBaseMaintenanceCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base property company name.
        /// </summary>
        public string ECCPBasePropertyCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance contract.
        /// </summary>
        public CreateOrEditEccpMaintenanceContractDto EccpMaintenanceContract { get; set; }
    }
}