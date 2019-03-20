// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpPropertyCompanyChangeLogForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos
{
    /// <summary>
    /// The get eccp property company change log for view.
    /// </summary>
    public class GetEccpPropertyCompanyChangeLogForView
    {
        /// <summary>
        /// Gets or sets the eccp base property company name.
        /// </summary>
        public string ECCPBasePropertyCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp property company change log.
        /// </summary>
        public EccpPropertyCompanyChangeLogDto EccpPropertyCompanyChangeLog { get; set; }
    }
}