// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceCompanyChangeLogForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos
{
    /// <summary>
    /// The get eccp maintenance company change log for view.
    /// </summary>
    public class GetEccpMaintenanceCompanyChangeLogForView
    {
        /// <summary>
        /// Gets or sets the eccp base maintenance company name.
        /// </summary>
        public string ECCPBaseMaintenanceCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance company change log.
        /// </summary>
        public EccpMaintenanceCompanyChangeLogDto EccpMaintenanceCompanyChangeLog { get; set; }
    }
}