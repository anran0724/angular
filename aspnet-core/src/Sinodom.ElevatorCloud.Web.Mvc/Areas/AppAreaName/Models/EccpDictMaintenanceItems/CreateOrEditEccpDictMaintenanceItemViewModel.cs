// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictMaintenanceItemViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictMaintenanceItems
{
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The create or edit eccp dict maintenance item view model.
    /// </summary>
    public class CreateOrEditEccpDictMaintenanceItemViewModel
    {
        /// <summary>
        /// Gets or sets the eccp dict maintenance item.
        /// </summary>
        public CreateOrEditEccpDictMaintenanceItemDto EccpDictMaintenanceItem { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpDictMaintenanceItem.Id.HasValue;
    }
}