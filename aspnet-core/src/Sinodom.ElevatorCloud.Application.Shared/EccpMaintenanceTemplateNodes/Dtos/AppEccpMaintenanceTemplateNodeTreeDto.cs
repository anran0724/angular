// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppEccpMaintenanceTemplateNodeTreeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos
{
    using System.Collections.Generic;

    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The app eccp maintenance template node tree dto.
    /// </summary>
    public class AppEccpMaintenanceTemplateNodeTreeDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the action code.
        /// </summary>
        public string ActionCode { get; set; }

        /// <summary>
        /// Gets or sets the child node.
        /// </summary>
        public List<AppEccpMaintenanceTemplateNodeTreeDto> ChildNode { get; set; }

        /// <summary>
        /// Gets or sets the dict node name.
        /// </summary>
        public string DictNodeName { get; set; }

        /// <summary>
        /// Gets or sets the dict node type id.
        /// </summary>
        public int DictNodeTypeId { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict maintenance items list.
        /// </summary>
        public List<EccpDictMaintenanceItemDto> eccpDictMaintenanceItemsList { get; set; }

        /// <summary>
        /// Gets or sets the maintenance template id.
        /// </summary>
        public int MaintenanceTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the next node id.
        /// </summary>
        public int NextNodeId { get; set; }

        /// <summary>
        /// Gets or sets the node desc.
        /// </summary>
        public string NodeDesc { get; set; }

        /// <summary>
        /// Gets or sets the node index.
        /// </summary>
        public int NodeIndex { get; set; }

        /// <summary>
        /// Gets or sets the parent node id.
        /// </summary>
        public int ParentNodeId { get; set; }

        /// <summary>
        /// Gets or sets the template node name.
        /// </summary>
        public string TemplateNodeName { get; set; }

        public bool MustDo { get; set; }

        public int SpareNodeId { get; set; }
    }
}