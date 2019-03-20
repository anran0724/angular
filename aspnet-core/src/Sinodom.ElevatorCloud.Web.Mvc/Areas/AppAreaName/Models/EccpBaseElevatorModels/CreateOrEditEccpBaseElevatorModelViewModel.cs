// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpBaseElevatorModelViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseElevatorModels
{
    using Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos;

    /// <summary>
    ///     The create or edit eccp base elevator model modal view model.
    /// </summary>
    public class CreateOrEditEccpBaseElevatorModelViewModel
    {
        /// <summary>
        ///     Gets or sets the eccp base elevator brand name.
        /// </summary>
        public string EccpBaseElevatorBrandName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp base elevator model.
        /// </summary>
        public CreateOrEditEccpBaseElevatorModelDto EccpBaseElevatorModel { get; set; }

        /// <summary>
        ///     The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpBaseElevatorModel.Id.HasValue;
    }
}