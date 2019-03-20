using Abp.AutoMapper;
using Sinodom.ElevatorCloud.Localization.Dto;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Languages
{
    [AutoMapFrom(typeof(GetLanguageForEditOutput))]
    public class CreateOrEditLanguageModalViewModel : GetLanguageForEditOutput
    {
        public bool IsEditMode => Language.Id.HasValue;

        public CreateOrEditLanguageModalViewModel(GetLanguageForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
