using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Editions.Dto;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}
