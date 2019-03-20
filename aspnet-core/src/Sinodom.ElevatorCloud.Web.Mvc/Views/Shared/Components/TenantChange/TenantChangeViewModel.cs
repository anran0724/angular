using Abp.AutoMapper;
using Sinodom.ElevatorCloud.Sessions.Dto;

namespace Sinodom.ElevatorCloud.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
