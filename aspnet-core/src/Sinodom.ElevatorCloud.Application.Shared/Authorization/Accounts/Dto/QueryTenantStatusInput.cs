// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryTenantStatusInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Authorization.Accounts.Dto
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The query tenant status input.
    /// </summary>
    public class QueryTenantStatusInput
    {
        /// <summary>
        /// Gets or sets the tenancy name.
        /// </summary>
        [Required]
        public string TenancyName { get; set; }
    }
}