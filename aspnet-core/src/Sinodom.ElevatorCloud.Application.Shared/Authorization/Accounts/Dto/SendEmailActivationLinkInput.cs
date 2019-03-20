using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}
