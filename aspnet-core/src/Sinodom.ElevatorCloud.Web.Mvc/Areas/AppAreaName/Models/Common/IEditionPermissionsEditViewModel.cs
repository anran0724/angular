namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Common
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Editions.Dto;

    public interface IEditionPermissionsEditViewModel
    {
        List<FlatEditionPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}