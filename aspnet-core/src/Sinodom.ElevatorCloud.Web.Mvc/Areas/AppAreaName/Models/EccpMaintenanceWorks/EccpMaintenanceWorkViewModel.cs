// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorks
{
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;

    /// <summary>
    ///     The eccp maintenance work view model.
    /// </summary>
    public class EccpMaintenanceWorkViewModel : GetEccpMaintenanceWorkForView
    {
        /// <summary>
        /// Gets or sets the eccp maintenance template node node name.
        /// </summary>
        public string EccpMaintenanceTemplateNodeNodeName { get; set; }
    }
}