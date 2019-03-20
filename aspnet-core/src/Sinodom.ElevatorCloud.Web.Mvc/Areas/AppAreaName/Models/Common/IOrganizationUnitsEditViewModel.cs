using System.Collections.Generic;
using Sinodom.ElevatorCloud.Organizations.Dto;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Common
{
    public interface IOrganizationUnitsEditViewModel
    {
        List<OrganizationUnitDto> AllOrganizationUnits { get; set; }

        List<string> MemberedOrganizationUnits { get; set; }
    }
}
