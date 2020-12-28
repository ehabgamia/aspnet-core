using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using MCV.Portal.MultiTenancy.Accounting.Dto;

namespace MCV.Portal.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
