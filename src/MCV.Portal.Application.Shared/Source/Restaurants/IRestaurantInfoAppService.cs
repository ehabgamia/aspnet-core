using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.Restaurant.Dto;
using MCV.Portal.Source.Restaurants.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCV.Portal.Source.Restaurants
{
    public interface IRestaurantInfoAppService : IApplicationService
    {
        PagedResultDto<RestInfosListDto> GetRestInfos(GetRestInfosInput input);

        Task CreateRestInfo(CreateRestInfoInput input);

        Task DeleteRestInfo(EntityDto input);

        Task EditRestInfo(EditRestInfoInput input);

        List<EmployeesViewList> GetEmployees();

        List<RestCategoryListDto> RestCategories();

        List<EmployeesViewList> GetRestaurantAdmins(int RestInfoID);

        List<NameValue<string>> FiltterGetEmployees(string searchTerm);
    }
}
