// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceContractsForExcelInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos
{
    using System;

    /// <summary>
    /// The get all eccp maintenance contracts for excel input.
    /// </summary>
    public class GetAllEccpMaintenanceContractsForExcelInput
    {
        /// <summary>
        /// Gets or sets the eccp base maintenance company name filter.
        /// </summary>
        public string ECCPBaseMaintenanceCompanyNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base property company name filter.
        /// </summary>
        public string ECCPBasePropertyCompanyNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the max end date filter.
        /// </summary>
        public DateTime? MaxEndDateFilter { get; set; }

        /// <summary>
        /// Gets or sets the max start date filter.
        /// </summary>
        public DateTime? MaxStartDateFilter { get; set; }

        /// <summary>
        /// Gets or sets the min end date filter.
        /// </summary>
        public DateTime? MinEndDateFilter { get; set; }

        /// <summary>
        /// Gets or sets the min start date filter.
        /// </summary>
        public DateTime? MinStartDateFilter { get; set; }
    }
}