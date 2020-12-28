using Abp.Events.Bus;

namespace MCV.Portal.MultiTenancy
{
    public class RecurringPaymentsEnabledEventData : EventData
    {
        public int TenantId { get; set; }
    }
}