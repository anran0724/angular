// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpPropertyCompanyChangeLogForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos
{
    /// <summary>
    /// The get eccp property company change log for edit output.
    /// </summary>
    public class GetEccpPropertyCompanyChangeLogForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp base property company name.
        /// </summary>
        public string ECCPBasePropertyCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp property company change log.
        /// </summary>
        public CreateOrEditEccpPropertyCompanyChangeLogDto EccpPropertyCompanyChangeLog { get; set; }
    }
}