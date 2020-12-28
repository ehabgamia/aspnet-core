using System.Threading.Tasks;
using MCV.Portal.Sessions.Dto;

namespace MCV.Portal.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
