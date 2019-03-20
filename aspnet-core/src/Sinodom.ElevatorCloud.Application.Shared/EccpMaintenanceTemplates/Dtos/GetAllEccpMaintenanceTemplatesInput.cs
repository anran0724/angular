// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceTemplatesInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance templates input.
    /// </summary>
    public class GetAllEccpMaintenanceTemplatesInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp dict elevator type name filter.
        /// </summary>
        public string EccpDictElevatorTypeNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict maintenance type name filter.
        /// </summary>
        public string EccpDictMaintenanceTypeNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the max temp node count filter.
        /// </summary>
        public int? MaxTempNodeCountFilter { get; set; }

        /// <summary>
        /// Gets or sets the min temp node count filter.
        /// </summary>
        public int? MinTempNodeCountFilter { get; set; }

        /// <summary>
        /// Gets or sets the temp allow filter.
        /// </summary>
        public string TempAllowFilter { get; set; }

        /// <summary>
        /// Gets or sets the temp deny filter.
        /// </summary>
        public string TempDenyFilter { get; set; }
    }
}