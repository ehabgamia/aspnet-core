using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Common.Dto;
using MCV.Portal.Editions.Dto;

namespace MCV.Portal.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();
    }
}