using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Editions.Dto;

namespace Sinodom.ElevatorCloud.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}
