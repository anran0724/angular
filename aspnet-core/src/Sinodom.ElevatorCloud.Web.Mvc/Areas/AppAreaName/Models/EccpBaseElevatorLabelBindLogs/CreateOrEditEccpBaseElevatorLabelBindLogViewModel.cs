// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpBaseElevatorLabelBindLogViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseElevatorLabelBindLogs
{
    using Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos;

    /// <summary>
    /// The create or edit eccp base elevator label bind log modal view model.
    /// </summary>
    public class CreateOrEditEccpBaseElevatorLabelBindLogViewModel
    {
        /// <summary>
        /// Gets or sets the eccp base elevator label bind log.
        /// </summary>
        public CreateOrEditEccpBaseElevatorLabelBindLogDto EccpBaseElevatorLabelBindLog { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator name.
        /// </summary>
        public string EccpBaseElevatorName { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict label status name.
        /// </summary>
        public string EccpDictLabelStatusName { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpBaseElevatorLabelBindLog.Id.HasValue;
    }
}