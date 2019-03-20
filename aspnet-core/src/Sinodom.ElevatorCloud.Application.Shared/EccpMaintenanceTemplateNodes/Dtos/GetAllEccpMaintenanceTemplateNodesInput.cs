// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceTemplateNodesInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance template nodes input.
    /// </summary>
    public class GetAllEccpMaintenanceTemplateNodesInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the action code filter.
        /// </summary>
        public string ActionCodeFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict node type name filter.
        /// </summary>
        public string EccpDictNodeTypeNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance template temp name filter.
        /// </summary>
        public string EccpMaintenanceTemplateTempNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the max node index filter.
        /// </summary>
        public int? MaxNodeIndexFilter { get; set; }

        /// <summary>
        /// Gets or sets the min node index filter.
        /// </summary>
        public int? MinNodeIndexFilter { get; set; }

        /// <summary>
        /// Gets or sets the node name filter.
        /// </summary>
        public string NodeNameFilter { get; set; }
    }
}