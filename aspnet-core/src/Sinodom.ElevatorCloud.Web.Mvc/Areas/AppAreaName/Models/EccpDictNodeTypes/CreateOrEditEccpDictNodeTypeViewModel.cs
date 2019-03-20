// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictNodeTypeViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictNodeTypes
{
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The create or edit eccp dict node type view model.
    /// </summary>
    public class CreateOrEditEccpDictNodeTypeViewModel
    {
        /// <summary>
        /// Gets or sets the eccp dict node type.
        /// </summary>
        public CreateOrEditEccpDictNodeTypeDto EccpDictNodeType { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpDictNodeType.Id.HasValue;
    }
}