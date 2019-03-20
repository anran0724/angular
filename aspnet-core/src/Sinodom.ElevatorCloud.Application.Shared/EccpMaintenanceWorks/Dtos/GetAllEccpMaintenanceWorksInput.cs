// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceWorksInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance works input.
    /// </summary>
    public class GetAllEccpMaintenanceWorksInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the eccp maintenance template node node name filter.
        /// </summary>
        public string EccpMaintenanceTemplateNodeNodeNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work order plan check date filter.
        /// </summary>
        public string EccpMaintenanceWorkOrderPlanCheckDateFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }
    }
}