// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceCompanyChangeLogsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance company change logs input.
    /// </summary>
    public class GetAllEccpMaintenanceCompanyChangeLogsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp base maintenance company name filter.
        /// </summary>
        public string ECCPBaseMaintenanceCompanyNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the field name filter.
        /// </summary>
        public string FieldNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the new value filter.
        /// </summary>
        public string NewValueFilter { get; set; }

        /// <summary>
        /// Gets or sets the old value filter.
        /// </summary>
        public string OldValueFilter { get; set; }
    }
}