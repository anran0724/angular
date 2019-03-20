// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetECCPBaseAreaForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos
{
    /// <summary>
    /// The get eccp base area for edit output.
    /// </summary>
    public class GetECCPBaseAreaForEditOutput
    {
        /// <summary>
        /// Gets or sets the city id.
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the district id.
        /// </summary>
        public int? DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the district name.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base area.
        /// </summary>
        public CreateOrEditECCPBaseAreaDto ECCPBaseArea { get; set; }

        /// <summary>
        /// Gets or sets the province id.
        /// </summary>
        public int? ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the province name.
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the street id.
        /// </summary>
        public int? StreetId { get; set; }
    }
}