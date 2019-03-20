// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetECCPBaseAnnualInspectionUnitForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos
{
    /// <summary>
    /// The get eccp base annual inspection unit for view.
    /// </summary>
    public class GetECCPBaseAnnualInspectionUnitForView
    {
        /// <summary>
        /// Gets or sets the eccp base annual inspection unit.
        /// </summary>
        public ECCPBaseAnnualInspectionUnitDto ECCPBaseAnnualInspectionUnit { get; set; }

        /// <summary>
        /// Gets or sets the eccp base area name.
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base area name 2.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base area name 3.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base area name 4.
        /// </summary>
        public string StreetName { get; set; }
    }
}