// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceWorkViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorks
{
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;

    /// <summary>
    ///     The create or edit eccp maintenance work view model.
    /// </summary>
    public class CreateOrEditEccpMaintenanceWorkViewModel
    {
        /// <summary>
        ///     Gets or sets the eccp maintenance template node node name.
        /// </summary>
        public string EccpMaintenanceTemplateNodeNodeName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp maintenance work.
        /// </summary>
        public CreateOrEditEccpMaintenanceWorkDto EccpMaintenanceWork { get; set; }

        /// <summary>
        ///     Gets or sets the eccp maintenance work order plan check date.
        /// </summary>
        public string EccpMaintenanceWorkOrderPlanCheckDate { get; set; }

        /// <summary>
        ///     The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpMaintenanceWork.Id.HasValue;
    }
}