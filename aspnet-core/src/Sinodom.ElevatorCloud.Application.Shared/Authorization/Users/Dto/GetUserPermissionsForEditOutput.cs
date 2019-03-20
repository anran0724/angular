using System.Collections.Generic;
using Sinodom.ElevatorCloud.Authorization.Permissions.Dto;

namespace Sinodom.ElevatorCloud.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}
