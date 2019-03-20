// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpElevatorChangeLogViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpElevatorChangeLogs
{
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;

    /// <summary>
    ///     The create or edit eccp elevator change log view model.
    /// </summary>
    public class CreateOrEditEccpElevatorChangeLogViewModel
    {
        /// <summary>
        ///     Gets or sets the eccp base elevator name.
        /// </summary>
        public string EccpBaseElevatorName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp elevator change log.
        /// </summary>
        public CreateOrEditEccpElevatorChangeLogDto EccpElevatorChangeLog { get; set; }

        /// <summary>
        ///     The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpElevatorChangeLog.Id.HasValue;
    }
}