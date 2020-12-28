using System.Threading.Tasks;
using Abp.Application.Services;
using MCV.Portal.MultiTenancy.Payments.PayPal.Dto;

namespace MCV.Portal.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
