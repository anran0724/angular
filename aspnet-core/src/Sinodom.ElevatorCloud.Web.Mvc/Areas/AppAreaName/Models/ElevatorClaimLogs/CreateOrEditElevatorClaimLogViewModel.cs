// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditElevatorClaimLogViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ElevatorClaimLogs
{
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;

    /// <summary>
    ///     The create or edit elevator claim log view model.
    /// </summary>
    public class CreateOrEditElevatorClaimLogViewModel
    {
        /// <summary>
        ///     Gets or sets the eccp base elevator name.
        /// </summary>
        public string EccpBaseElevatorName { get; set; }

        /// <summary>
        ///     Gets or sets the elevator claim log.
        /// </summary>
        public CreateOrEditElevatorClaimLogDto ElevatorClaimLog { get; set; }

        /// <summary>
        ///     The is edit mode.
        /// </summary>
        public bool IsEditMode => this.ElevatorClaimLog.Id.HasValue;
    }
}