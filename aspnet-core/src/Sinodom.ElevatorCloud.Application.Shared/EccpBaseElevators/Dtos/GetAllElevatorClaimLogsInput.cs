// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllElevatorClaimLogsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all elevator claim logs input.
    /// </summary>
    public class GetAllElevatorClaimLogsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp base elevator name filter.
        /// </summary>
        public string EccpBaseElevatorNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }
    }
}