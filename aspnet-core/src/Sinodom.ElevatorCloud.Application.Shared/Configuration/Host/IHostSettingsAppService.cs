using System.Threading.Tasks;
using Abp.Application.Services;
using Sinodom.ElevatorCloud.Configuration.Host.Dto;

namespace Sinodom.ElevatorCloud.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
