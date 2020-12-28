using System.Threading.Tasks;
using Abp.Application.Services;
using MCV.Portal.Install.Dto;

namespace MCV.Portal.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}