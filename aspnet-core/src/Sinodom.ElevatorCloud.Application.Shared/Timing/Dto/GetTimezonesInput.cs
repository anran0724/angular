using Abp.Configuration;

namespace Sinodom.ElevatorCloud.Timing.Dto
{
    public class GetTimezonesInput
    {
        public SettingScopes DefaultTimezoneScope { get; set; }
    }
}
