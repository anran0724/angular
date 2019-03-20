// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpCompanyUserAuditLogViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpCompanyUserAuditLogs
{
    using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos;

    /// <summary>
    ///     The create or edit eccp company user audit log modal view model.
    /// </summary>
    public class CreateOrEditEccpCompanyUserAuditLogViewModel
    {
        /// <summary>
        ///     Gets or sets the eccp company user audit log.
        /// </summary>
        public CreateOrEditEccpCompanyUserAuditLogDto EccpCompanyUserAuditLog { get; set; }

        /// <summary>
        ///     The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpCompanyUserAuditLog.Id.HasValue;

        /// <summary>
        ///     Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }
    }
}