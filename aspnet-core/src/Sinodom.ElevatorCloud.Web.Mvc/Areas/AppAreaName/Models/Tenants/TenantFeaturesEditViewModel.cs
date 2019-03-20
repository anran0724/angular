using Abp.AutoMapper;
using Sinodom.ElevatorCloud.MultiTenancy;
using Sinodom.ElevatorCloud.MultiTenancy.Dto;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Common;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }

        public TenantFeaturesEditViewModel(Tenant tenant, GetTenantFeaturesEditOutput output)
        {
            Tenant = tenant;
            output.MapTo(this);
        }
    }
}
