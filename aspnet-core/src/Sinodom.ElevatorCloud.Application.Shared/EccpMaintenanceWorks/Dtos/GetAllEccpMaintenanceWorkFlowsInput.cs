// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpMaintenanceWorkFlowsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance work flows input.
    /// </summary>
    public class GetAllEccpMaintenanceWorkFlowsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the action code value filter.
        /// </summary>
        public string ActionCodeValueFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict maintenance work flow status name filter.
        /// </summary>
        public string EccpDictMaintenanceWorkFlowStatusNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance template node node name filter.
        /// </summary>
        public string EccpMaintenanceTemplateNodeNodeNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance work task name filter.
        /// </summary>
        public string EccpMaintenanceWorkTaskNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }
    }
}