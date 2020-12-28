using System.Threading.Tasks;
using Abp.Application.Services;
using MCV.Portal.Sessions.Dto;

namespace MCV.Portal.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
