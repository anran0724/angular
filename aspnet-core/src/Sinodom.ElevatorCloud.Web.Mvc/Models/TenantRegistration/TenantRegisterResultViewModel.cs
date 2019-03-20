using Abp.AutoMapper;
using Sinodom.ElevatorCloud.MultiTenancy.Dto;

namespace Sinodom.ElevatorCloud.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(RegisterTenantOutput))]
    public class TenantRegisterResultViewModel : RegisterTenantOutput
    {
        public string TenantLoginAddress { get; set; }
    }
}
