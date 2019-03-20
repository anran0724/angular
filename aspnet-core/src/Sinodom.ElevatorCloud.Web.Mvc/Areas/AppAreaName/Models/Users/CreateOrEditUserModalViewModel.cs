using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Sinodom.ElevatorCloud.Authorization.Users.Dto;
using Sinodom.ElevatorCloud.Security;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Common;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Users
{
    [AutoMapFrom(typeof(GetUserForEditOutput))]
    public class CreateOrEditUserModalViewModel : GetUserForEditOutput, IOrganizationUnitsEditViewModel
    {
        public bool CanChangeUserName
        {
            get { return User.UserName != Authorization.Users.User.AdminUserName; }
        }

        public int AssignedRoleCount
        {
            get { return Roles.Count(r => r.IsAssigned); }
        }

        public bool IsEditMode
        {
            get { return User.Id.HasValue; }
        }

        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public CreateOrEditUserModalViewModel(GetUserForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
