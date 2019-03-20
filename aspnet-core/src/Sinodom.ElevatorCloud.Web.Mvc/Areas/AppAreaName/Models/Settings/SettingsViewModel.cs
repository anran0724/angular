using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Configuration.Tenants.Dto;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Settings
{
    public class SettingsViewModel
    {
        public TenantSettingsEditDto Settings { get; set; }
        
        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}
