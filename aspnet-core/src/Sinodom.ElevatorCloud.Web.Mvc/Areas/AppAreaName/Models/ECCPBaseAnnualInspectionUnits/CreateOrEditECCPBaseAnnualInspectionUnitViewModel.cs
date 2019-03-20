// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditECCPBaseAnnualInspectionUnitViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPBaseAnnualInspectionUnits
{
    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos;

    /// <summary>
    /// The create or edit eccp base annual inspection unit modal view model.
    /// </summary>
    public class CreateOrEditECCPBaseAnnualInspectionUnitViewModel
    {
        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the district name.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base annual inspection unit.
        /// </summary>
        public CreateOrEditECCPBaseAnnualInspectionUnitDto EccpBaseAnnualInspectionUnit { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpBaseAnnualInspectionUnit.Id.HasValue;

        /// <summary>
        /// Gets or sets the province name.
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the street name.
        /// </summary>
        public string StreetName { get; set; }
    }
}