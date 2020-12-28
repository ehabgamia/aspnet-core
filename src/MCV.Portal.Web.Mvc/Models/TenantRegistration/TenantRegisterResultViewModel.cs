using Abp.AutoMapper;
using MCV.Portal.MultiTenancy.Dto;

namespace MCV.Portal.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(RegisterTenantOutput))]
    public class TenantRegisterResultViewModel : RegisterTenantOutput
    {
        public string TenantLoginAddress { get; set; }
    }
}