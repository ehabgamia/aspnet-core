using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Caching.Dto;

namespace MCV.Portal.Caching
{
    public interface ICachingAppService : IApplicationService
    {
        ListResultDto<CacheDto> GetAllCaches();

        Task ClearCache(EntityDto<string> input);

        Task ClearAllCaches();
    }
}
