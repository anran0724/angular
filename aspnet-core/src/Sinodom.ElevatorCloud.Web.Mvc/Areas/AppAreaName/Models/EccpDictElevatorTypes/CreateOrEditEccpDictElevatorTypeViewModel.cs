// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictElevatorTypeViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictElevatorTypes
{
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    ///     The create or edit eccp dict elevator type view model.
    /// </summary>
    public class CreateOrEditEccpDictElevatorTypeViewModel
    {
        /// <summary>
        ///     Gets or sets the eccp dict elevator type.
        /// </summary>
        public CreateOrEditEccpDictElevatorTypeDto EccpDictElevatorType { get; set; }

        /// <summary>
        ///     The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpDictElevatorType.Id.HasValue;
    }
}