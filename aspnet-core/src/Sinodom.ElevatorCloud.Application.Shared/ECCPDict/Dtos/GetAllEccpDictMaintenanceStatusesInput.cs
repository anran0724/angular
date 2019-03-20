// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpDictMaintenanceStatusesInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp dict maintenance statuses input.
    /// </summary>
    public class GetAllEccpDictMaintenanceStatusesInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }
    }
}