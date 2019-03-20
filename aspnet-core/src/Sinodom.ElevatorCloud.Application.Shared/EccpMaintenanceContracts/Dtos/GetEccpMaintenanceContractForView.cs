// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceContractForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos
{
    /// <summary>
    /// The get eccp maintenance contract for view.
    /// </summary>
    public class GetEccpMaintenanceContractForView
    {
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
        public EccpMaintenanceContractDto EccpMaintenanceContract { get; set; }
    }
}