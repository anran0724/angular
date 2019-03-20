using Abp.AutoMapper;
using Abp.Organizations;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.OrganizationUnits
{
    [AutoMapFrom(typeof(OrganizationUnit))]
    public class EditOrganizationUnitModalViewModel
    {
        public long? Id { get; set; }

        public string DisplayName { get; set; }
    }
}
