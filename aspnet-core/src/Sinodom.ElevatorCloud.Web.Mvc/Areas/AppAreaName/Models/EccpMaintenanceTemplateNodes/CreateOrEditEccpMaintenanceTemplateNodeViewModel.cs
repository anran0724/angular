// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceTemplateNodeViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceTemplateNodes
{
    using Sinodom.ElevatorCloud.EccpDict.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos;

    /// <summary>
    ///     The create or edit eccp maintenance template node view model.
    /// </summary>
    public class CreateOrEditEccpMaintenanceTemplateNodeViewModel
    {
        /// <summary>
        ///     Gets or sets the eccp dict maintenance item dtos.
        /// </summary>
        public EccpDictMaintenanceItemTemplateNodeDto[] EccpDictMaintenanceItemDtos { get; set; }

        /// <summary>
        ///     Gets or sets the eccp dict node type name.
        /// </summary>
        public string EccpDictNodeTypeName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp maintenance next node name.
        /// </summary>
        public string EccpMaintenanceNextNodeName { get; set; }

        public string EccpMaintenanceSpareNodeName { get; set; }
        /// <summary>
        ///     Gets or sets the eccp maintenance template node.
        /// </summary>
        public CreateOrEditEccpMaintenanceTemplateNodeDto EccpMaintenanceTemplateNode { get; set; }

        /// <summary>
        ///     The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpMaintenanceTemplateNode.Id.HasValue;
    }
}