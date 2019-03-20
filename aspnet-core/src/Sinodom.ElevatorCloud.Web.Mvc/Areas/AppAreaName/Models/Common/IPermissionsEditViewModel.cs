using System.Collections.Generic;
using Sinodom.ElevatorCloud.Authorization.Permissions.Dto;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}
