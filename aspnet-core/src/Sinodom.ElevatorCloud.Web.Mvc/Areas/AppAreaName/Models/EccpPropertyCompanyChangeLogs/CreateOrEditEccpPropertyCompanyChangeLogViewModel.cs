// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpPropertyCompanyChangeLogViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpPropertyCompanyChangeLogs
{
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos;

    /// <summary>
    /// The create or edit eccp property company change log view model.
    /// </summary>
    public class CreateOrEditEccpPropertyCompanyChangeLogViewModel
    {
        /// <summary>
        /// Gets or sets the eccp base property company name.
        /// </summary>
        public string EccpBasePropertyCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp property company change log.
        /// </summary>
        public CreateOrEditEccpPropertyCompanyChangeLogDto EccpPropertyCompanyChangeLog { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpPropertyCompanyChangeLog.Id.HasValue;
    }
}