// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpPropertyCompanyChangeLogsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp property company change logs input.
    /// </summary>
    public class GetAllEccpPropertyCompanyChangeLogsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp base property company name filter.
        /// </summary>
        public string ECCPBasePropertyCompanyNameFilter { get; set; }

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