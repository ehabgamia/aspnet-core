using System.Threading.Tasks;
using Abp.Application.Services;
using MCV.Portal.Editions.Dto;
using MCV.Portal.MultiTenancy.Dto;

namespace MCV.Portal.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}