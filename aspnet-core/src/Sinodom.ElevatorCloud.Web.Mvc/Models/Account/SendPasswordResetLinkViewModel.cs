using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}
