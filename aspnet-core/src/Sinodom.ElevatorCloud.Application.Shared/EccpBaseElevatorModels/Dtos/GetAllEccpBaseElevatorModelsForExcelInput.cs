// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpBaseElevatorModelsForExcelInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos
{
    /// <summary>
    /// The get all eccp base elevator models for excel input.
    /// </summary>
    public class GetAllEccpBaseElevatorModelsForExcelInput
    {
        /// <summary>
        /// Gets or sets the eccp base elevator brand name filter.
        /// </summary>
        public string EccpBaseElevatorBrandNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }
    }
}