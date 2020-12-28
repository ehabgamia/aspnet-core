using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using MCV.Portal.Authorization;
using MCV.Portal.Source.Restaurant;
using MCV.Portal.Source.Restaurant.Dto;
using MCV.Portal.Source.Restaurants.Dto;
using Microsoft.EntityFrameworkCore;

namespace MCV.Portal.Source.Restaurants
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantInfo)]
    public class RestaurantInfoAppService : PortalAppServiceBase, IRestaurantInfoAppService
    {

        private readonly IRepository<RestInfo> _RestInfoRepository;
        private readonly IRepository<EmployeesView> _EmployeesViewRepository;
        private readonly IRepository<RestCategory> _RestCategoryRepository;
        private readonly IRepository<RestInfoAdmins> _RestInfoAdminsRepository;

        public RestaurantInfoAppService(IRepository<RestInfo> RestInfoRepository,
                                        IRepository<EmployeesView> EmployeesViewRepository,
                                        IRepository<RestCategory> RestCategoryRepository,
                                        IRepository<RestInfoAdmins> RestInfoAdminsRepository)
        {
            _RestInfoRepository = RestInfoRepository;
            _EmployeesViewRepository = EmployeesViewRepository;
            _RestCategoryRepository = RestCategoryRepository;
            _RestInfoAdminsRepository = RestInfoAdminsRepository;
        }

        public PagedResultDto<RestInfosListDto> GetRestInfos(GetRestInfosInput input)
        {
            string Filter = "";

            if (input.Filter != null)
            {
                Filter = input.Filter.Trim();
            }

            var UserId = GetCurrentUser().Id;

            List<RestInfo> RestInfos = new List<RestInfo>();

            var Category = _RestCategoryRepository.GetAll().Where(a => a.RestCategoryDesc.Contains(input.Filter)).Select(a => a.Id).SingleOrDefault();

            if (Category != 0)
            {
                RestInfos = _RestInfoRepository
             .GetAll().Include(a => a.RestCategory)
             .Where(a => a.CreatorUserId == UserId && a.RestCategoryId == Category)
             .OrderByDescending(p => p.CreationTime)
             .ToList();
            }
            else
            {
                RestInfos = _RestInfoRepository
               .GetAll().Include(a => a.RestCategory)
               .Where(a => a.CreatorUserId == UserId)
               .WhereIf(
                   !Filter.IsNullOrEmpty(),
                   p => p.Area.Contains(Filter) ||
                        p.Building.Contains(Filter) ||
                        p.City.Contains(Filter) ||
                        p.Country.Contains(Filter) ||
                        p.ExtNum.Contains(Filter)

               ).OrderByDescending(p => p.CreationTime)
               .ToList();
            }
            var RestInfosCount = RestInfos.Count();

            var RestInfoListDtos = ObjectMapper.Map<List<RestInfosListDto>>(RestInfos);

            foreach (var item in RestInfoListDtos)
            {
                var RestCategoryDesc = _RestCategoryRepository.GetAll().Where(a => a.Id == item.RestCategoryId).SingleOrDefault();
                item.RestCategoryDesc = RestCategoryDesc.RestCategoryDesc;
            }

            return new PagedResultDto<RestInfosListDto>(RestInfosCount, RestInfoListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantInfo_CreateRestInfo)]
        public async Task CreateRestInfo(CreateRestInfoInput input)
        {
            var item = ObjectMapper.Map<RestInfo>(input);
            await _RestInfoRepository.InsertAsync(item);
            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.selectedEmployees.Count() > 0)
            {
                for (int i = 0; i < input.selectedEmployees.Count(); i++)
                {
                    int employeeid = Convert.ToInt32(input.selectedEmployees[i].Value);

                    RestInfoAdmins restInfoAdmins = new RestInfoAdmins
                    {
                        emp_id = employeeid,
                        RestInfoID = item.Id
                    };

                    await _RestInfoAdminsRepository.InsertAsync(restInfoAdmins);
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantInfo_DeleteRestInfo)]
        public async Task DeleteRestInfo(EntityDto input)
        {
            await _RestInfoRepository.DeleteAsync(input.Id);
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantInfo_EditRestInfo)]
        public async Task<GetRestInfoForEditOutput> GetRestInfoForEdit(GetRestInfoForEditInput input)
        {
            var RestInfo = await _RestInfoRepository.GetAsync(input.Id);

            List<RestInfoAdmins> admins = _RestInfoAdminsRepository.GetAll().Where(a => a.RestInfoID == input.Id).ToList();

            List<RestInfoAdmins> adminsList = ObjectMapper.Map<List<RestInfoAdmins>>(admins);

            var employeeslist = new List<NameValue<string>>();

            foreach (var item in adminsList)
            {
                var Employees = _EmployeesViewRepository.GetAll().Where(a => a.Id == item.emp_id).SingleOrDefault();

                string emp_username = Employees.emp_username;
                string emp_id = item.emp_id.ToString();

                var employee = new NameValue<string> { Name = emp_username, Value = emp_id };

                employeeslist.Add(employee);
            }

            GetRestInfoForEditOutput getRestInfoForEditOutput = ObjectMapper.Map<GetRestInfoForEditOutput>(RestInfo);

            getRestInfoForEditOutput.selectedEmployees = employeeslist;

            return ObjectMapper.Map<GetRestInfoForEditOutput>(getRestInfoForEditOutput);
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantInfo_EditRestInfo)]
        public async Task EditRestInfo(EditRestInfoInput input)
        {
            RestInfoAdmins restInfoAdmins = new RestInfoAdmins();

            var restinfo = await _RestInfoRepository.GetAsync(input.Id);
            restinfo.Area = input.Area;
            restinfo.Building = input.Building;
            restinfo.City = input.City;
            restinfo.Country = input.Country;
            restinfo.isAvailable = input.isAvailable;
            restinfo.RestCategoryId = input.RestCategoryId;
            restinfo.ExtNum = input.ExtNum;

            if (input.selectedEmployees.Count() > 0)
            {
                for (int i = 0; i < input.selectedEmployees.Count(); i++)
                {
                    int employeeid = Convert.ToInt32(input.selectedEmployees[i].Value);

                    var notexsit = _RestInfoAdminsRepository.GetAll().Where(a => a.RestInfoID == input.Id && a.emp_id == employeeid).SingleOrDefault();

                    if (notexsit == null)
                    {
                        restInfoAdmins = new RestInfoAdmins
                        {
                            emp_id = employeeid,
                            RestInfoID = input.Id
                        };
                        await _RestInfoAdminsRepository.InsertAsync(restInfoAdmins);
                    }
                }

                var exsit = _RestInfoAdminsRepository.GetAll().Where(a => a.RestInfoID == input.Id).ToList();

                if (exsit != null)
                {
                    for (int n = 0; n < exsit.Count(); n++)
                    {
                        for (int i = 0; i < input.selectedEmployees.Count(); i++)
                        {
                            var find = input.selectedEmployees.Find(a => a.Value == exsit[n].emp_id.ToString());

                            if (find == null)
                            {
                                await _RestInfoAdminsRepository.DeleteAsync(exsit[n]);

                            }
                        }
                    }
                }

                await _RestInfoRepository.UpdateAsync(restinfo);
            }
        }

        public List<EmployeesViewList> GetEmployees()
        {
            var Employees = _EmployeesViewRepository.GetAll().ToList();

            List<EmployeesViewList> EmployeesList = ObjectMapper.Map<List<EmployeesViewList>>(Employees);

            return new List<EmployeesViewList>(EmployeesList);
        }

        public List<NameValue<string>> FiltterGetEmployees(string searchTerm)
        {
            var Employees = _EmployeesViewRepository.GetAll().Where(a => a.emp_username.Contains(searchTerm)).ToList();

            var employeeslist = new List<NameValue<string>>();

            List<EmployeesViewList> EmployeesList = ObjectMapper.Map<List<EmployeesViewList>>(Employees);

            foreach (var item in EmployeesList)
            {
                string emp_username = item.emp_username;

                string emp_id = item.emp_id.ToString();

                var employee = new NameValue<string> { Name = emp_username, Value = emp_id };

                employeeslist.Add(employee);
            }

            return employeeslist;
        }

        public List<NameValue<string>> SendAndGetSelectedCountries(List<NameValue<string>> selectedCountries)
        {
            return selectedCountries;
        }

        public List<EmployeesViewList> GetRestaurantAdmins(int RestInfoID)
        {
            List<EmployeesViewList> employeesViewList = new List<EmployeesViewList>();
            var admins = _RestInfoAdminsRepository.GetAll().Where(a => a.RestInfoID == RestInfoID).ToList();

            foreach (var admin in admins)
            {
                var Employees = _EmployeesViewRepository.GetAll().Where(a => a.Id == admin.emp_id).SingleOrDefault();

                EmployeesViewList EmployeesList = ObjectMapper.Map<EmployeesViewList>(Employees);

                employeesViewList.Add(EmployeesList);
            }

            return new List<EmployeesViewList>(employeesViewList);
        }

        public List<RestCategoryListDto> RestCategories()
        {
            var restcategories = _RestCategoryRepository.GetAll().ToList();

            List<RestCategoryListDto> RestCategory = ObjectMapper.Map<List<RestCategoryListDto>>(restcategories);

            return RestCategory;
        }
    }
}
