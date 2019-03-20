using Abp.AutoMapper;
using Sinodom.ElevatorCloud.MultiTenancy.Dto;

namespace Sinodom.ElevatorCloud.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
        public EditionsSelectViewModel(EditionsSelectOutput output)
        {
            output.MapTo(this);
        }
    }
}
