using MCV.Portal.Editions;
using MCV.Portal.Editions.Dto;
using MCV.Portal.MultiTenancy.Payments;
using MCV.Portal.Security;
using MCV.Portal.MultiTenancy.Payments.Dto;

namespace MCV.Portal.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
