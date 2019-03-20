using Abp.AutoMapper;
using Sinodom.ElevatorCloud.Authorization.Roles.Dto;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Common;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode
        {
            get { return Role.Id.HasValue; }
        }

        public CreateOrEditRoleModalViewModel(GetRoleForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
