using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
