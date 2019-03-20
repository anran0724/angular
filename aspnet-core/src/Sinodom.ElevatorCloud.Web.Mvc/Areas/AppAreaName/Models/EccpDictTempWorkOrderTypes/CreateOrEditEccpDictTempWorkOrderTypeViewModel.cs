// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictTempWorkOrderTypeViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictTempWorkOrderTypes
{
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The create or edit eccp dict temp work order type view model.
    /// </summary>
    public class CreateOrEditEccpDictTempWorkOrderTypeViewModel
    {
        /// <summary>
        /// Gets or sets the eccp dict temp work order type.
        /// </summary>
        public CreateOrEditEccpDictTempWorkOrderTypeDto EccpDictTempWorkOrderType { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpDictTempWorkOrderType.Id.HasValue;
    }
}