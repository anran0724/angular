using System.Threading.Tasks;
using Abp.Application.Services;
using Sinodom.ElevatorCloud.Install.Dto;

namespace Sinodom.ElevatorCloud.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}
