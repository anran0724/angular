// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceTemplateNodeForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos
{
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The get eccp maintenance template node for edit output.
    /// </summary>
    public class GetEccpMaintenanceTemplateNodeForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp dict maintenance item dtos.
        /// </summary>
        public EccpDictMaintenanceItemTemplateNodeDto[] eccpDictMaintenanceItemDtos { get; set; }

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
        public CreateOrEditEccpMaintenanceTemplateNodeDto EccpMaintenanceTemplateNode { get; set; }
    }
}