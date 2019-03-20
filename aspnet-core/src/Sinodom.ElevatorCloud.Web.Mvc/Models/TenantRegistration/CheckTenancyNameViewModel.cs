namespace Sinodom.ElevatorCloud.Web.Models.TenantRegistration
{
    using System.ComponentModel.DataAnnotations;

    public class CheckTenancyNameViewModel
    {
        /// <summary>
        /// Gets or sets the tenancy name.
        /// </summary>
        [Required]
        public string TenancyName { get; set; }

        /// <summary>
        /// Gets or sets the admin password.
        /// </summary>
        [Required]
        public string AdminPassword { get; set; }
    }
}