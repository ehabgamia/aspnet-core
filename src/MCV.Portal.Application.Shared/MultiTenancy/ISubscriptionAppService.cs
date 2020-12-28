using System.Threading.Tasks;
using Abp.Application.Services;

namespace MCV.Portal.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
