// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpDictMaintenanceItemsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp dict maintenance items input.
    /// </summary>
    public class GetAllEccpDictMaintenanceItemsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the term code filter.
        /// </summary>
        public string TermCodeFilter { get; set; }
    }
}