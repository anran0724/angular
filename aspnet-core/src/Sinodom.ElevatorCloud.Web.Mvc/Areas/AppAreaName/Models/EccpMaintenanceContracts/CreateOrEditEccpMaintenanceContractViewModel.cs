// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceContractViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceContracts
{
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos;

    /// <summary>
    /// The create or edit eccp maintenance contract view model.
    /// </summary>
    public class CreateOrEditEccpMaintenanceContractViewModel
    {
        /// <summary>
        /// Gets or sets the eccp base elevators ids.
        /// </summary>
        public string EccpBaseElevatorsIds { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevators names.
        /// </summary>
        public string EccpBaseElevatorsNames { get; set; }

        /// <summary>
        /// Gets or sets the eccp base maintenance company name.
        /// </summary>
        public string EccpBaseMaintenanceCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base property company name.
        /// </summary>
        public string EccpBasePropertyCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance contract.
        /// </summary>
        public CreateOrEditEccpMaintenanceContractDto EccpMaintenanceContract { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpMaintenanceContract.Id.HasValue;
    }
}