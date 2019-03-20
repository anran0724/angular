// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceCompanyChangeLogViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceCompanyChangeLogs
{
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos;

    /// <summary>
    /// The create or edit eccp maintenance company change log view model.
    /// </summary>
    public class CreateOrEditEccpMaintenanceCompanyChangeLogViewModel
    {
        /// <summary>
        /// Gets or sets the eccp base maintenance company name.
        /// </summary>
        public string EccpBaseMaintenanceCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance company change log.
        /// </summary>
        public CreateOrEditEccpMaintenanceCompanyChangeLogDto EccpMaintenanceCompanyChangeLog { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpMaintenanceCompanyChangeLog.Id.HasValue;
    }
}