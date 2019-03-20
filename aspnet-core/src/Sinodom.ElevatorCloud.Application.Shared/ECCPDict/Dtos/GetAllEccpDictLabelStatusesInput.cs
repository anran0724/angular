// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpDictLabelStatusesInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp dict label statuses input.
    /// </summary>
    public class GetAllEccpDictLabelStatusesInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }
    }
}