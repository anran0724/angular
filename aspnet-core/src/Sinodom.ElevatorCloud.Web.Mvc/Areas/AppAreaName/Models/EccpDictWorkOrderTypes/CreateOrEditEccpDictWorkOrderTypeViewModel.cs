// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictWorkOrderTypeViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictWorkOrderTypes
{
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The create or edit eccp dict work order type view model.
    /// </summary>
    public class CreateOrEditEccpDictWorkOrderTypeViewModel
    {
        /// <summary>
        /// Gets or sets the eccp dict work order type.
        /// </summary>
        public CreateOrEditEccpDictWorkOrderTypeDto EccpDictWorkOrderType { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpDictWorkOrderType.Id.HasValue;
    }
}