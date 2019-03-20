// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpBaseElevatorLabelViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseElevatorLabels
{
    using Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos;

    /// <summary>
    /// The create or edit eccp base elevator label modal view model.
    /// </summary>
    public class CreateOrEditEccpBaseElevatorLabelViewModel
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
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpBaseElevatorLabel.Id.HasValue;

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }
    }
}