using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.Restaurant.Dto;
using MCV.Portal.Source.Restaurants.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCV.Portal.Source.Restaurants
{
    public interface IRestaurantRequestAppService : IApplicationService
    {
        PagedResultDto<RestReqListDto> GetRestReqs(GetRestReqInput input);

        Task DeleteRestReq(EntityDto input);

        Task EditRestReq(EditRestReqInput input);

        List<RestSchesListDto> GetRestSches();

        List<EmployeesViewList> GetEmployees();

        Task SelectRestReqScheItem(List<RestItemsListDto> input, int RestRequestsId, int? RestSchedulesId, int? RestInfosID);

        List<Days> GetDates(int RestInfoID);

        List<RestInfosListDto> GetRestInfos();

        List<RestItemsListDto> GetSelectedRestItems(GetRestReqForEditInput input);

        Task UpdateSelectRestReqScheItem(List<RestItemsListDto> input);

        List<RestItemsListDto> GetSelectedNonSchRestItems(int RestInfoID);

        Task CreateRestReqAsDraft(CreateRestReqInput input);

        Task CreateRestReqSubmit(CreateRestReqInput input);

        List<PaymentTypesListDto> GetPaymentTyps();

        List<RequestLogListDto> GetRquestLogs(int RestRequestId);

        RestItemsListDto GetRestItem(int RestItemId, int RestInfoID);

        Task StatusProceed(int RestRequestsId);

        Task StatusCancelOrder(int RestRequestsId);

        GetRestReqForEditOutput GetRestReqForEdit(GetRestReqForEditInput input);

        List<RestItemsListDto> GetNonScheRestItems(int RestInfoID, string filter);

        List<RestItemsListDto> GetReqRestSchItems(GetRestReqSchInput input);

        List<RestItemsListDto> GetRestSchItems(int RestSchedulesId, string filter, int RestRequestsId);
    }
}
