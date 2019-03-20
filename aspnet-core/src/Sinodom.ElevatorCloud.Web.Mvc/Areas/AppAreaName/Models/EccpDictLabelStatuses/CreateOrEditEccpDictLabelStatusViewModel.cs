// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictLabelStatusViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictLabelStatuses
{
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The create or edit eccp dict label status view model.
    /// </summary>
    public class CreateOrEditEccpDictLabelStatusViewModel
    {
        /// <summary>
        /// Gets or sets the eccp dict label status.
        /// </summary>
        public CreateOrEditEccpDictLabelStatusDto EccpDictLabelStatus { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpDictLabelStatus.Id.HasValue;
    }
}