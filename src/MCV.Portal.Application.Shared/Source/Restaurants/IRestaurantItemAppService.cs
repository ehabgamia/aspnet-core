using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.Restaurants.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCV.Portal.Source.Restaurants
{
    public interface IRestaurantItemAppService : IApplicationService
    {
        PagedResultDto<RestItemsListDto> GetRestItems(GetRestItemsInput input);

        Task CreateRestItem(CreateRestItemInput input);

        Task DeleteRestItem(EntityDto input);

        Task EditRestItem(EditRestItemInput input);

        List<ItemsCategoryListDto> ItemsCategories();

        bool checkdeleteitem(EntityDto input);
    }
}
