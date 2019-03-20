// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceWorkLogForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    using System.Collections.Generic;

    /// <summary>
    /// The get eccp maintenance work log for view.
    /// </summary>
    public class GetEccpMaintenanceWorkLogForView
    {
        /// <summary>
        /// Gets or sets the eccp base elevator num.
        /// </summary>
        public string EccpBaseElevatorNum { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance company name.
        /// </summary>
        public string EccpMaintenanceCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance user name list.
        /// </summary>
        public List<string> EccpMaintenanceUserNameList { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work log.
        /// </summary>
        public EccpMaintenanceWorkLogDto EccpMaintenanceWorkLog { get; set; }

        /// <summary>
        /// Gets or sets the eccp property company name.
        /// </summary>
        public string EccpPropertyCompanyName { get; set; }
    }
}