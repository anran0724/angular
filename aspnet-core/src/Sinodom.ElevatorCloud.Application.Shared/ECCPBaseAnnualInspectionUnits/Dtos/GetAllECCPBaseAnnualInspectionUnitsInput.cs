// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllECCPBaseAnnualInspectionUnitsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp base annual inspection units input.
    /// </summary>
    public class GetAllECCPBaseAnnualInspectionUnitsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the addresse filter.
        /// </summary>
        public string AddresseFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base area name 2 filter.
        /// </summary>
        public string CityNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base area name 3 filter.
        /// </summary>
        public string DistrictNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base area name 4 filter.
        /// </summary>
        public string StreetNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base area name filter.
        /// </summary>
        public string ProvinceNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the name filter.
        /// </summary>
        public string NameFilter { get; set; }

        /// <summary>
        /// Gets or sets the telephone filter.
        /// </summary>
        public string TelephoneFilter { get; set; }
    }
}