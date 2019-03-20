// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpBaseElevatorLabelBindLogsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp base elevator label bind logs input.
    /// </summary>
    public class GetAllEccpBaseElevatorLabelBindLogsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp base elevator name filter.
        /// </summary>
        public string EccpBaseElevatorNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict label status name filter.
        /// </summary>
        public string EccpDictLabelStatusNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the max binding time filter.
        /// </summary>
        public DateTime? MaxBindingTimeFilter { get; set; }

        /// <summary>
        /// Gets or sets the min binding time filter.
        /// </summary>
        public DateTime? MinBindingTimeFilter { get; set; }
    }
}