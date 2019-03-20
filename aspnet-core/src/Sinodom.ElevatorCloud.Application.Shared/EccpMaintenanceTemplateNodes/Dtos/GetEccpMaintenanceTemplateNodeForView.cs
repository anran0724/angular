// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceTemplateNodeForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos
{
    /// <summary>
    /// The get eccp maintenance template node for view.
    /// </summary>
    public class GetEccpMaintenanceTemplateNodeForView
    {
        /// <summary>
        /// Gets or sets the eccp dict node type name.
        /// </summary>
        public string EccpDictNodeTypeName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance next node name.
        /// </summary>
        public string EccpMaintenanceNextNodeName { get; set; }

        public string EccpMaintenanceSpareNodeName { get; set; }
        /// <summary>
        /// Gets or sets the eccp maintenance template node.
        /// </summary>
        public EccpMaintenanceTemplateNodeDto EccpMaintenanceTemplateNode { get; set; }
    }
}