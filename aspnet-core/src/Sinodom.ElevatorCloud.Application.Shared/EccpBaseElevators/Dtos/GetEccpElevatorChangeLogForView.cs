// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpElevatorChangeLogForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    /// <summary>
    /// The get eccp elevator change log for view.
    /// </summary>
    public class GetEccpElevatorChangeLogForView
    {
        /// <summary>
        /// Gets or sets the eccp base elevator name.
        /// </summary>
        public string EccpBaseElevatorName { get; set; }

        /// <summary>
        /// Gets or sets the eccp elevator change log.
        /// </summary>
        public EccpElevatorChangeLogDto EccpElevatorChangeLog { get; set; }
    }
}