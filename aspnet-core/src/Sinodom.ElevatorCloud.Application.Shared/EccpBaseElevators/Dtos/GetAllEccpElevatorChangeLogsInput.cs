// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpElevatorChangeLogsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp elevator change logs input.
    /// </summary>
    public class GetAllEccpElevatorChangeLogsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp base elevator name filter.
        /// </summary>
        public string EccpBaseElevatorNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the field name filter.
        /// </summary>
        public string FieldNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the new value filter.
        /// </summary>
        public string NewValueFilter { get; set; }

        /// <summary>
        /// Gets or sets the old value filter.
        /// </summary>
        public string OldValueFilter { get; set; }
    }
}