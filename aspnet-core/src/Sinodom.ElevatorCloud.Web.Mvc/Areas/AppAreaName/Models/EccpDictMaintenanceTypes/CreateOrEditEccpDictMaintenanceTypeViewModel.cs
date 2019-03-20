// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictMaintenanceTypeViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictMaintenanceTypes
{
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The create or edit eccp dict maintenance type view model.
    /// </summary>
    public class CreateOrEditEccpDictMaintenanceTypeViewModel
    {
        /// <summary>
        /// Gets or sets the eccp dict maintenance type.
        /// </summary>
        public CreateOrEditEccpDictMaintenanceTypeDto EccpDictMaintenanceType { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpDictMaintenanceType.Id.HasValue;
    }
}