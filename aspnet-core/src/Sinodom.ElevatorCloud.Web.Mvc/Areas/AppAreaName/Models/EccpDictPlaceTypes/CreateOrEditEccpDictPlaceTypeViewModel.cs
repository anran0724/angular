// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictPlaceTypeViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictPlaceTypes
{
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The create or edit eccp dict place type view model.
    /// </summary>
    public class CreateOrEditEccpDictPlaceTypeViewModel
    {
        /// <summary>
        /// Gets or sets the eccp dict place type.
        /// </summary>
        public CreateOrEditEccpDictPlaceTypeDto EccpDictPlaceType { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpDictPlaceType.Id.HasValue;
    }
}