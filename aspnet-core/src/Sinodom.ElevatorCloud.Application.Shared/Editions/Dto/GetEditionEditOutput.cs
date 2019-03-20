using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.Editions.Dto
{
    public class GetEditionEditOutput
    {
        public EditionEditDto Edition { get; set; }

        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }

        public List<FlatEditionPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}
