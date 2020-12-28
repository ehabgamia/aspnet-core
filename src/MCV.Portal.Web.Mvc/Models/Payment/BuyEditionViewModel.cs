using System.Collections.Generic;
using MCV.Portal.Editions;
using MCV.Portal.Editions.Dto;
using MCV.Portal.MultiTenancy.Payments;
using MCV.Portal.MultiTenancy.Payments.Dto;

namespace MCV.Portal.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
