using Abp.Application.Services;
using Sinodom.ElevatorCloud.Dto;
using Sinodom.ElevatorCloud.Logging.Dto;

namespace Sinodom.ElevatorCloud.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
