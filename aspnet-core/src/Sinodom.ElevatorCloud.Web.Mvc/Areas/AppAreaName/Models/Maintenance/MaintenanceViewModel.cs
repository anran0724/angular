using System.Collections.Generic;
using Sinodom.ElevatorCloud.Caching.Dto;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}
