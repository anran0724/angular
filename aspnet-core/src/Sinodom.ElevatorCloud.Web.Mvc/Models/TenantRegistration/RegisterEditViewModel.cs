// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterEditViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Models.TenantRegistration
{
    using Sinodom.ElevatorCloud.MultiTenancy.Dto;

    /// <summary>
    /// The register edit view model.
    /// </summary>
    public class RegisterEditViewModel
    {
        /// <summary>
        /// Gets or sets the eccp base register company.
        /// </summary>
        public RegisterTenantEditOutput EccpBaseRegisterCompany { get; set; }
    }
}