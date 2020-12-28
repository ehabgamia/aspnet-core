using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace MCV.Portal.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
