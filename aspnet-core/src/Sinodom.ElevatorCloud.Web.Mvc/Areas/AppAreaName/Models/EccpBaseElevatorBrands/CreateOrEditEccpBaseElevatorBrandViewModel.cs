// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpBaseElevatorBrandViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseElevatorBrands
{
    using Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos;

    /// <summary>
    /// The create or edit eccp base elevator brand modal view model.
    /// </summary>
    public class CreateOrEditEccpBaseElevatorBrandViewModel
    {
        /// <summary>
        /// Gets or sets the eccp base elevator brand.
        /// </summary>
        public CreateOrEditEccpBaseElevatorBrandDto EccpBaseElevatorBrand { get; set; }

        /// <summary>
        /// Gets or sets the eccp base production company name.
        /// </summary>
        public string EccpBaseProductionCompanyName { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpBaseElevatorBrand.Id.HasValue;
    }
}