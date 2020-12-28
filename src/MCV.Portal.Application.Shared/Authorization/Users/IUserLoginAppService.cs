using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Authorization.Users.Dto;

namespace MCV.Portal.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<ListResultDto<UserLoginAttemptDto>> GetRecentUserLoginAttempts();
    }
}
