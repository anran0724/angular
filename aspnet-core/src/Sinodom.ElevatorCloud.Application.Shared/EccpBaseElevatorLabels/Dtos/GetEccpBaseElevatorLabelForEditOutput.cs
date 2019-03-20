// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpBaseElevatorLabelForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos
{
    /// <summary>
    /// The get eccp base elevator label for edit output.
    /// </summary>
    public class GetEccpBaseElevatorLabelForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp base elevator label.
        /// </summary>
        public CreateOrEditEccpBaseElevatorLabelDto EccpBaseElevatorLabel { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator name.
        /// </summary>
        public string EccpBaseElevatorName { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict label status name.
        /// </summary>
        public string EccpDictLabelStatusName { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }
    }
}