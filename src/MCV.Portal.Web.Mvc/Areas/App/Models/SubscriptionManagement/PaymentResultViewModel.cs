using Abp.AutoMapper;
using MCV.Portal.Editions;
using MCV.Portal.MultiTenancy.Payments.Dto;

namespace MCV.Portal.Web.Areas.App.Models.SubscriptionManagement
{
    [AutoMapTo(typeof(ExecutePaymentDto))]
    public class PaymentResultViewModel : SubscriptionPaymentDto
    {
        public EditionPaymentType EditionPaymentType { get; set; }
    }
}