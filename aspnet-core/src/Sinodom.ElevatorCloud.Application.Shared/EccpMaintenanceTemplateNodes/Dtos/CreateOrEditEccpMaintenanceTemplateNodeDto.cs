// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceTemplateNodeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance template node dto.
    /// </summary>
    public class CreateOrEditEccpMaintenanceTemplateNodeDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the action code.
        /// </summary>
        [StringLength(
            EccpMaintenanceTemplateNodeConsts.MaxActionCodeLength,
            MinimumLength = EccpMaintenanceTemplateNodeConsts.MinActionCodeLength)]
        public string ActionCode { get; set; }

        /// <summary>
        /// Gets or sets the assigned item ids.
        /// </summary>
        public int[] AssignedItemIds { get; set; }

        /// <summary>
        /// Gets or sets the dict node type id.
        /// </summary>
        public int DictNodeTypeId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance template id.
        /// </summary>
        public int MaintenanceTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the next node id.
        /// </summary>
        public int? NextNodeId { get; set; }

        /// <summary>
        /// Gets or sets the node desc.
        /// </summary>
        [StringLength(
            EccpMaintenanceTemplateNodeConsts.MaxNodeDescLength,
            MinimumLength = EccpMaintenanceTemplateNodeConsts.MinNodeDescLength)]
        public string NodeDesc { get; set; }

        /// <summary>
        /// Gets or sets the node index.
        /// </summary>
        public int NodeIndex { get; set; }

        /// <summary>
        /// Gets or sets the parent node id.
        /// </summary>
        public int? ParentNodeId { get; set; }

        /// <summary>
        /// Gets or sets the template node name.
        /// </summary>
        [Required]
        [StringLength(
            EccpMaintenanceTemplateNodeConsts.MaxTemplateNodeNameLength,
            MinimumLength = EccpMaintenanceTemplateNodeConsts.MinTemplateNodeNameLength)]
        public string TemplateNodeName { get; set; }        

        public bool MustDo { get; set; }

        public int? SpareNodeId { get; set; }
    }
}