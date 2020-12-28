using Abp.Application.Services;
using MCV.Portal.Dto;
using MCV.Portal.Logging.Dto;

namespace MCV.Portal.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
