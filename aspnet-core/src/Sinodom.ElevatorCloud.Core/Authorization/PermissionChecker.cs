using Abp.Authorization;
using Sinodom.ElevatorCloud.Authorization.Roles;
using Sinodom.ElevatorCloud.Authorization.Users;

namespace Sinodom.ElevatorCloud.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
