using System.Threading.Tasks;
using Abp.Dependency;

namespace MCV.Portal.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}