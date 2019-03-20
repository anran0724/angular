// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllForLookupTableInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
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
    }
}