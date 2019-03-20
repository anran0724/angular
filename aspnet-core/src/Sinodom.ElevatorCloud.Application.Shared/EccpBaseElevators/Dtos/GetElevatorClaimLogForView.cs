// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetElevatorClaimLogForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    /// <summary>
    /// The get elevator claim log for view.
    /// </summary>
    public class GetElevatorClaimLogForView
    {
        /// <summary>
        /// Gets or sets the eccp base elevator name.
        /// </summary>
        public string EccpBaseElevatorName { get; set; }

        /// <summary>
        /// Gets or sets the elevator claim log.
        /// </summary>
        public ElevatorClaimLogDto ElevatorClaimLog { get; set; }
    }
}