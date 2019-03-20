using Abp.AutoMapper;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.Authorization.Users.Dto;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Common;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; private set; }

        public UserPermissionsEditViewModel(GetUserPermissionsForEditOutput output, User user)
        {
            User = user;
            output.MapTo(this);
        }
    }
}
