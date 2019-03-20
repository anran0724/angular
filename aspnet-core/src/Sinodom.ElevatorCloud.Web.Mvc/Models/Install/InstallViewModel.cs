using System.Collections.Generic;
using Abp.Localization;
using Sinodom.ElevatorCloud.Install.Dto;

namespace Sinodom.ElevatorCloud.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
