using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Configuration.Host.Dto;
using Sinodom.ElevatorCloud.Editions.Dto;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.HostSettings
{
    public class HostSettingsViewModel
    {
        public HostSettingsEditDto Settings { get; set; }

        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }

        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}
