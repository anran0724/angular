// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpDictMaintenanceTypesInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp dict maintenance types input.
    /// </summary>
    public class GetAllEccpDictMaintenanceTypesInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }
    }
}