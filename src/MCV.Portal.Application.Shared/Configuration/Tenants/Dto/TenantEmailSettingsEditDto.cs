using Abp.Auditing;
using MCV.Portal.Configuration.Dto;

namespace MCV.Portal.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}