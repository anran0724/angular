// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpBaseElevatorModelForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos
{
    /// <summary>
    /// The get eccp base elevator model for edit output.
    /// </summary>
    public class GetEccpBaseElevatorModelForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp base elevator brand name.
        /// </summary>
        public string EccpBaseElevatorBrandName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator model.
        /// </summary>
        public CreateOrEditEccpBaseElevatorModelDto EccpBaseElevatorModel { get; set; }
    }
}