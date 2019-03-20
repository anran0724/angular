// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllForLookupTableInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all for lookup table input.
    /// </summary>
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        public int ParentId { get; set; }
    }
}