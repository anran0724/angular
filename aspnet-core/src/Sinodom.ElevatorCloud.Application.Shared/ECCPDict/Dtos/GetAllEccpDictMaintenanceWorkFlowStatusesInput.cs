// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpDictMaintenanceWorkFlowStatusesInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp dict maintenance work flow statuses input.
    /// </summary>
    public class GetAllEccpDictMaintenanceWorkFlowStatusesInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }
    }
}