// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditECCPEditionsTypeViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPEditionsTypes
{
    using Sinodom.ElevatorCloud.Editions.Dtos;

    /// <summary>
    /// The create or edit eccp editions type view model.
    /// </summary>
    public class CreateOrEditECCPEditionsTypeViewModel
    {
        /// <summary>
        /// Gets or sets the eccp editions type.
        /// </summary>
        public CreateOrEditECCPEditionsTypeDto EccpEditionsType { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpEditionsType.Id.HasValue;
    }
}