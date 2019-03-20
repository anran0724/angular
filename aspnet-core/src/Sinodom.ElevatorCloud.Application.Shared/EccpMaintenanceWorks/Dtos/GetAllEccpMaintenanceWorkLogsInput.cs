// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceWorkLogsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance work logs input.
    /// </summary>
    public class GetAllEccpMaintenanceWorkLogsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp elevator num filter.
        /// </summary>
        public string EccpElevatorNumFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance company name filter.
        /// </summary>
        public string EccpMaintenanceCompanyNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance user name filter.
        /// </summary>
        public string EccpMaintenanceUserNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp property company name filter.
        /// </summary>
        public string EccpPropertyCompanyNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }
    }
}