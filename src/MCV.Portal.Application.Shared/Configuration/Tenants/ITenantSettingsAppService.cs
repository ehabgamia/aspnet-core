using System.Threading.Tasks;
using Abp.Application.Services;
using MCV.Portal.Configuration.Tenants.Dto;

namespace MCV.Portal.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
