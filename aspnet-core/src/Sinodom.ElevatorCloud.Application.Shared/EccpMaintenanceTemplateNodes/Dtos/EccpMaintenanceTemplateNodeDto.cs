// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTemplateNodeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp maintenance template node dto.
    /// </summary>
    public class EccpMaintenanceTemplateNodeDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the action code.
        /// </summary>
        public string ActionCode { get; set; }

        /// <summary>
        /// Gets or sets the dict node type id.
        /// </summary>
        public int DictNodeTypeId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance template id.
        /// </summary>
        public int MaintenanceTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the node index.
        /// </summary>
        public int NodeIndex { get; set; }

        /// <summary>
        /// Gets or sets the template node name.
        /// </summary>
        public string TemplateNodeName { get; set; }
    }
}