// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpBaseElevatorModelsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp base elevator models input.
    /// </summary>
    public class GetAllEccpBaseElevatorModelsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp base elevator brand name filter.
        /// </summary>
        public string EccpBaseElevatorBrandNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }
    }
}