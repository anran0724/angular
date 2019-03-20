// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpBaseElevatorLabelBindLogForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos
{
    /// <summary>
    /// The get eccp base elevator label bind log for view.
    /// </summary>
    public class GetEccpBaseElevatorLabelBindLogForView
    {
        /// <summary>
        /// Gets or sets the eccp base elevator label bind log.
        /// </summary>
        public EccpBaseElevatorLabelBindLogDto EccpBaseElevatorLabelBindLog { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator name.
        /// </summary>
        public string EccpBaseElevatorName { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict label status name.
        /// </summary>
        public string EccpDictLabelStatusName { get; set; }
    }
}