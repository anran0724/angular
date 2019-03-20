// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllForLookupTableInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos
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
        /// Gets or sets the maintenance template id.
        /// </summary>
        public int MaintenanceTemplateId { get; set; }
    }
}