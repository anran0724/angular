// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllECCPBaseAreasInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp base areas input.
    /// </summary>
    public class GetAllECCPBaseAreasInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the code filter.
        /// </summary>
        public string CodeFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the max level filter.
        /// </summary>
        public int? MaxLevelFilter { get; set; }

        /// <summary>
        /// Gets or sets the max parent id filter.
        /// </summary>
        public int? MaxParentIdFilter { get; set; }

        /// <summary>
        /// Gets or sets the min level filter.
        /// </summary>
        public int? MinLevelFilter { get; set; }

        /// <summary>
        /// Gets or sets the min parent id filter.
        /// </summary>
        public int? MinParentIdFilter { get; set; }

        /// <summary>
        /// Gets or sets the name filter.
        /// </summary>
        public string NameFilter { get; set; }
    }
}