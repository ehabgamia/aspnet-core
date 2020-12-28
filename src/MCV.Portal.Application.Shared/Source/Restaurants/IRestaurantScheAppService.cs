using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.Restaurants.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCV.Portal.Source.Restaurants
{
    public interface IRestaurantScheAppService : IApplicationService
    {
        Task CreateRestSche(CreateRestScheInput input);

        Task DeleteRestSche(EntityDto input);

        Task EditRestSche(EditRestScheInput input);

        List<RestItemsListDto> GetSelectedRestItems(GetRestScheForEditInput input);

        List<RestItemsListDto> GetUnSelectedRestItems(GetRestScheForEditInput input);

        Task AttachRestScheItem(List<RestItemsListDto> selecteditem, int RestSchedulesId);

        List<RestItemsListDto> GetUnSelectedRestItemsforedit(GetRestScheForEditInput input);

        List<RestSchesListDto> GetRestSchesbyID(int infoid);

        Task CreateNonRestSche(CreateRestScheInput input);

        List<RestItemsListDto> GetRestnonSchesbyID(int infoid);

        Task UnAttachRestNonScheItem(List<RestItemsListDto> selecteditem, int RestInfoID);

        List<RestItemsListDto> GetUnSelectedNonSchRestItemsforedit(GetRestNonScheForEditInput input);

        List<RestItemsListDto> GetSelectedNonSchRestItems(GetRestNonScheForEditInput input);

        Task EditRestNonSche(EditRestNonScheInput input);

        Task UpdateRestItemSalesPrice(List<RestItemsListDto> NonSchesrestitems, int RestInfoID);
    }
}
