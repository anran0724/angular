// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetECCPBaseAnnualInspectionUnitForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos
{
    /// <summary>
    /// The get eccp base annual inspection unit for edit output.
    /// </summary>
    public class GetECCPBaseAnnualInspectionUnitForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp base annual inspection unit.
        /// </summary>
        public CreateOrEditECCPBaseAnnualInspectionUnitDto ECCPBaseAnnualInspectionUnit { get; set; }

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