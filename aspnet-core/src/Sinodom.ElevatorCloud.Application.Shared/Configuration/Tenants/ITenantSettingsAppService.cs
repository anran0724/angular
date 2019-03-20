using System.Threading.Tasks;
using Abp.Application.Services;
using Sinodom.ElevatorCloud.Configuration.Tenants.Dto;

namespace Sinodom.ElevatorCloud.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
