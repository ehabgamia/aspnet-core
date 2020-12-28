using Abp.AutoMapper;
using MCV.Portal.MultiTenancy.Dto;

namespace MCV.Portal.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
