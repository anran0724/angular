using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}
