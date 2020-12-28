using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using MCV.Portal.Authorization;
using MCV.Portal.Source.Restaurant;
using MCV.Portal.Source.Restaurants.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCV.Portal.Source.Restaurants
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantItem)]

    public class RestaurantItemAppService : PortalAppServiceBase, IRestaurantItemAppService
    {
        private readonly IRepository<RestItem> _RestItemRepository;
        private readonly IRepository<ItemsCategory> _ItemsCategoryRepository;
        private readonly IRepository<RestScheduleItem> _RestScheduleItemRepository;
        private readonly IRepository<RestNonSchItem> _RestNonSchItemRepository;


        public RestaurantItemAppService(IRepository<RestItem> RestItemRepository,
                                        IRepository<ItemsCategory> ItemsCategoryRepository,
                                        IRepository<RestScheduleItem> RestScheduleItemRepository,
                                        IRepository<RestNonSchItem> RestNonSchItemRepository)
        {
            _RestItemRepository = RestItemRepository;
            _ItemsCategoryRepository = ItemsCategoryRepository;
            _RestScheduleItemRepository = RestScheduleItemRepository;
            _RestNonSchItemRepository = RestNonSchItemRepository;
        }

        public PagedResultDto<RestItemsListDto> GetRestItems(GetRestItemsInput input)
        {
            var UserId = GetCurrentUser().Id;
            
            var Category = _ItemsCategoryRepository.GetAll().Where(a => a.CategoryDesc.Contains(input.Filter)).Select(a => a.Id).SingleOrDefault();

            List<RestItem> restitems = new List<RestItem>();

            if(Category != 0)
            {
                restitems = _RestItemRepository
                .GetAll().Include(a => a.ItemsCategory)
                .Where(a => a.CreatorUserId == UserId && a.ItemsCategoryId == Category)
                .OrderBy(p => p.ItemDesc)
                .ThenBy(p => p.ItemCode)
                .ToList();
            }
            else
            {
                restitems = _RestItemRepository
                .GetAll().Include(a => a.ItemsCategory)
                .Where(a => a.CreatorUserId == UserId)
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.ItemDesc.Contains(input.Filter) ||
                         p.ItemCode.Contains(input.Filter) ||
                         p.CostPrice.ToString().Contains(input.Filter) ||
                         p.SalesPrice.ToString().Contains(input.Filter))
                .OrderBy(p => p.ItemDesc)
                .ThenBy(p => p.ItemCode)
                .ToList();
            }

            var restitemsCount = restitems.Count();

            var restitemListDtos = ObjectMapper.Map<List<RestItemsListDto>>(restitems);

            foreach (var item in restitemListDtos)
            {
                var ItemCategoryDesc = _ItemsCategoryRepository.GetAll().Where(a => a.Id == item.ItemsCategoryId).SingleOrDefault();
                item.ItemsCategoryDesc = ItemCategoryDesc.CategoryDesc;
            }

            return new PagedResultDto<RestItemsListDto>(restitemsCount, restitemListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantItem_CreateRestItem)]
        public async Task CreateRestItem(CreateRestItemInput input)
        {
            var item = ObjectMapper.Map<RestItem>(input);

            if (input.Picture == null)
            {
                item.Picture = "default.jpg";
            }

            await _RestItemRepository.InsertAsync(item);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantItem_DeleteRestItem)]
        public async Task DeleteRestItem(EntityDto input)
        {
            await _RestItemRepository.DeleteAsync(input.Id);
        }

        public bool checkdeleteitem(EntityDto input)
        {
            var RestSchs = _RestScheduleItemRepository.GetAll().Where(a => a.RestItemsId == input.Id).ToList();
            var RestNonSchs = _RestNonSchItemRepository.GetAll().Where(a => a.RestItemId == input.Id).ToList();

            if (RestSchs == null || RestNonSchs == null)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantItem_EditRestItem)]
        public async Task<GetRestItemForEditOutput> GetRestItemForEdit(GetRestItemForEditInput input)
        {
            var RestItem = await _RestItemRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetRestItemForEditOutput>(RestItem);
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantItem_EditRestItem)]
        public async Task EditRestItem(EditRestItemInput input)
        {
            var restitem = await _RestItemRepository.GetAsync(input.Id);
            restitem.ItemDesc = input.ItemDesc;
            restitem.ItemCode = input.ItemCode;
            restitem.SalesPrice = input.SalesPrice;
            restitem.CostPrice = input.CostPrice;
            restitem.ItemsCategoryId = input.ItemsCategoryId;
            restitem.Picture = input.Picture;

            if (input.Picture == null)
            {
                restitem.Picture = "default.jpg";
            }

            await _RestItemRepository.UpdateAsync(restitem);
        }

        public List<ItemsCategoryListDto> ItemsCategories()
        {
            var itemscategories = _ItemsCategoryRepository.GetAll().ToList();

            List<ItemsCategoryListDto> ItemCategory = ObjectMapper.Map<List<ItemsCategoryListDto>>(itemscategories);

            return ItemCategory;
        }
    }
}
