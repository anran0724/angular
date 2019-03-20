// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceNextNodeLookupTableDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos
{
    /// <summary>
    /// The eccp maintenance next node lookup table dto.
    /// </summary>
    public class EccpMaintenanceNextNodeLookupTableDto
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the node index.
        /// </summary>
        public int NodeIndex { get; set; }
    }
}