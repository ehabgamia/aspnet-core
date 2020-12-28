using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.Restaurants.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCV.Portal.Source.Restaurants
{
    public interface IRestaurantResponseAppService : IApplicationService
    {
        PagedResultDto<RestRespListDto> GetRestResps(GetRestRespInput input);

        PagedResultDto<RestReqListDto> GetRestReqs(GetRestReqInput input);

        List<RestItemsListDto> GetRestSchItems(GetRestReqSchInput input);

        Task SaveRestReqItems(List<RestItemsListDto> input, int RestRequestsId);

        Task StatusStartProcess(int RestRequestsId);

        Task StatusDone(int RestRequestsId);

        List<RequestLogListDto> GetRquestLogs(int RestRequestId);

        PagedResultDto<RestReqListDto> HistoryGetRestReqs();
    }
}
