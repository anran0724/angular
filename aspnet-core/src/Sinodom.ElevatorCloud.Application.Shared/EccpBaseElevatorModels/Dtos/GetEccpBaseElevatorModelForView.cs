// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpBaseElevatorModelForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos
{
    /// <summary>
    /// The get eccp base elevator model for view.
    /// </summary>
    public class GetEccpBaseElevatorModelForView
    {
        /// <summary>
        /// Gets or sets the eccp base elevator brand name.
        /// </summary>
        public string EccpBaseElevatorBrandName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator model.
        /// </summary>
        public EccpBaseElevatorModelDto EccpBaseElevatorModel { get; set; }
    }
}