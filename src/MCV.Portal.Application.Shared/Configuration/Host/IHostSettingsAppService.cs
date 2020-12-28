using System.Threading.Tasks;
using Abp.Application.Services;
using MCV.Portal.Configuration.Host.Dto;

namespace MCV.Portal.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
