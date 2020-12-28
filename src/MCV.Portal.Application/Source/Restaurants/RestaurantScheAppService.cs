using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using MCV.Portal.Source.Restaurant;
using MCV.Portal.Source.Restaurants.Dto;
using Microsoft.EntityFrameworkCore;

namespace MCV.Portal.Source.Restaurants
{
    //[AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantSche)]

    public class RestaurantScheAppService : PortalAppServiceBase, IRestaurantScheAppService
    {
        private readonly IRepository<RestSchedule> _RestScheRepository;
        private readonly IRepository<RestItem> _RestItemRepository;
        private readonly IRepository<RestCategory> _RestCategoryRepository;
        private readonly IRepository<RestInfo> _RestInfoRepository;
        private readonly IRepository<RestScheduleItem> _RestScheduleItemRepository;
        private readonly IRepository<ItemsCategory> _ItemsCategoryRepository;
        private readonly IRepository<RestNonSchItem> _RestNonSchItemRepository;


        public RestaurantScheAppService(IRepository<RestSchedule> RestScheRepository,
            IRepository<RestItem> RestItemRepository,
            IRepository<RestInfo> RestInfoRepository,
            IRepository<RestScheduleItem> RestScheduleItem,
            IRepository<RestCategory> RestCategoryRepository,
            IRepository<ItemsCategory> ItemsCategoryRepository,
            IRepository<RestNonSchItem> RestNonSchItemRepository)
        {
            _RestScheRepository = RestScheRepository;
            _RestItemRepository = RestItemRepository;
            _RestInfoRepository = RestInfoRepository;
            _RestScheduleItemRepository = RestScheduleItem;
            _RestCategoryRepository = RestCategoryRepository;
            _ItemsCategoryRepository = ItemsCategoryRepository;
            _RestNonSchItemRepository = RestNonSchItemRepository;
        }

        #region Schedule Items
        public List<RestSchesListDto> GetRestSchesbyID(int infoid)
        {
            var UserId = GetCurrentUser().Id;

            var RestSches = _RestScheRepository.GetAll().Where(a => a.RestInfoID == infoid && a.CreatorUserId == UserId).ToList();

            List<RestSchesListDto> RestScheListDtos = ObjectMapper.Map<List<RestSchesListDto>>(RestSches);

            if (RestScheListDtos.Count() > 0)
            {
                for (int i = 0; i < RestScheListDtos.Count(); i++)
                {
                    RestScheListDtos[i].Date = RestScheListDtos[i].CurrentDate.ToString("dd/MM/yyyy");
                }
            }
            return new List<RestSchesListDto>(RestScheListDtos);
        }

        public List<RestItemsListDto> GetUnSelectedRestItems(GetRestScheForEditInput input)
        {
            var restitemListDtos = new List<RestItemsListDto>();

            try
            {
                var restitems = _RestItemRepository.GetAll()
                    .Include(a => a.ItemsCategory)
                    .Where(p => p.IsDeleted == false)
                    .OrderBy(p => p.ItemDesc)
                    .ToList();

                restitemListDtos = ObjectMapper.Map<List<RestItemsListDto>>(restitems).OrderBy(a => a.Id).ToList();

                foreach (var item in restitemListDtos)
                {
                    var ItemCategoryDesc = _ItemsCategoryRepository.GetAll().Where(a => a.Id == item.ItemsCategoryId).SingleOrDefault();
                    item.ItemsCategoryDesc = ItemCategoryDesc.CategoryDesc;
                }

                return new List<RestItemsListDto>(restitemListDtos);
            }
            catch (Exception e)
            {

            }
            return new List<RestItemsListDto>(restitemListDtos);
        }

        public List<RestInfosListDto> GetRestInfos()
        {
            var restinfos = _RestInfoRepository.GetAll().Include(a => a.RestCategory).Where(a => a.isAvailable == true).ToList();

            var restinfoListDtos = ObjectMapper.Map<List<RestInfosListDto>>(restinfos);

            foreach (var item in restinfoListDtos)
            {
                var RestCategoryDesc = _RestCategoryRepository.GetAll().Where(a => a.Id == item.RestCategoryId).SingleOrDefault();
                item.RestCategoryDesc = RestCategoryDesc.RestCategoryDesc;
            }

            return new List<RestInfosListDto>(restinfoListDtos);
        }

        public async Task CreateRestSche(CreateRestScheInput input)
        {
            var item = ObjectMapper.Map<RestSchedule>(input);

            string day = input.CurrentDate.DayOfWeek.ToString();

            item.Day = day;

            await _RestScheRepository.InsertAsync(item);

            await CurrentUnitOfWork.SaveChangesAsync();

            await AttachRestScheItem(input.targetItems, item.Id);
        }

        public async Task DeleteRestSche(EntityDto input)
        {
            await _RestScheRepository.DeleteAsync(input.Id);
        }

        public async Task<GetRestScheForEditOutput> GetRestScheForEdit(GetRestScheForEditInput input)
        {
            var RestSche = await _RestScheRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetRestScheForEditOutput>(RestSche);
        }

        public async Task EditRestSche(EditRestScheInput input)
        {
            var RestSche = await _RestScheRepository.GetAsync(input.Id);

            RestSche.CurrentDate = input.CurrentDate;

            string day = input.CurrentDate.DayOfWeek.ToString();

            RestSche.Day = day;

            await _RestScheRepository.UpdateAsync(RestSche);

            await UnAttachRestScheItem(input.targetItems, RestSche.Id);
        }

        public List<RestItemsListDto> GetSelectedRestItems(GetRestScheForEditInput input)
        {
            List<RestItemsListDto> selectdrestitemListDtos = new List<RestItemsListDto>();
            List<int> schitemid = new List<int>();

            try
            {
                var restitems = _RestItemRepository.GetAll()
                                                   .Where(p => p.IsDeleted == false)
                                                   .OrderBy(p => p.ItemDesc)
                                                   .ToList();

                var restitemListDtos = ObjectMapper.Map<List<RestItemsListDto>>(restitems).OrderBy(a => a.Id).ToList();

                var RestSchesItem = _RestScheduleItemRepository.GetAll().Where(a => a.RestSchedulesId == input.Id).ToList();

                var RestScheItemListDtos = ObjectMapper.Map<List<RestSchesItemListDto>>(RestSchesItem).OrderBy(a => a.RestItemsId).ToList();

                for (int i = 0; i < RestScheItemListDtos.Count(); i++)
                {
                    schitemid.Add(RestScheItemListDtos[i].RestItemsId);
                }

                for (int n = 0; n < restitemListDtos.Count(); n++)
                {
                    if (schitemid.Contains(restitemListDtos[n].Id) == true &&
                        selectdrestitemListDtos.Contains(restitemListDtos[n]) == false)
                    {
                        selectdrestitemListDtos.Add(restitemListDtos[n]);
                    }
                }

                foreach (var item in selectdrestitemListDtos)
                {
                    var ItemCategoryDesc = _ItemsCategoryRepository.GetAll().Where(a => a.Id == item.ItemsCategoryId).SingleOrDefault();
                    item.ItemsCategoryDesc = ItemCategoryDesc.CategoryDesc;
                }
                return new List<RestItemsListDto>(selectdrestitemListDtos);

            }
            catch (Exception e)
            {

            }

            return new List<RestItemsListDto>(selectdrestitemListDtos);
        }

        public List<RestItemsListDto> GetUnSelectedRestItemsforedit(GetRestScheForEditInput input)
        {
            List<RestItemsListDto> unselectdrestitemListDtos = new List<RestItemsListDto>();
            List<int> schitemid = new List<int>();

            try
            {
                var restitems = _RestItemRepository.GetAll().Include(a => a.ItemsCategory)
                                                    .Where(p => p.IsDeleted == false)
                                                    .OrderBy(p => p.ItemDesc)
                                                    .ToList();

                var restitemListDtos = ObjectMapper.Map<List<RestItemsListDto>>(restitems).OrderBy(a => a.Id).ToList();

                var RestSchesItem = _RestScheduleItemRepository.GetAll().Where(a => a.RestSchedulesId == input.Id).ToList();

                var RestScheItemListDtos = ObjectMapper.Map<List<RestSchesItemListDto>>(RestSchesItem).OrderBy(a => a.RestItemsId).ToList();

                for (int i = 0; i < RestScheItemListDtos.Count(); i++)
                {
                    schitemid.Add(RestScheItemListDtos[i].RestItemsId);
                }

                for (int n = 0; n < restitemListDtos.Count(); n++)
                {
                    if (schitemid.Contains(restitemListDtos[n].Id) == false)
                    {
                        unselectdrestitemListDtos.Add(restitemListDtos[n]);
                    }
                }

                foreach (var item in unselectdrestitemListDtos)
                {
                    var ItemCategoryDesc = _ItemsCategoryRepository.GetAll().Where(a => a.Id == item.ItemsCategoryId).SingleOrDefault();
                    item.ItemsCategoryDesc = ItemCategoryDesc.CategoryDesc;
                }
                return new List<RestItemsListDto>(unselectdrestitemListDtos);

            }
            catch (Exception e)
            {

            }

            return new List<RestItemsListDto>(unselectdrestitemListDtos);
        }

        public async Task AttachRestScheItem(List<RestItemsListDto> selecteditem, int RestSchedulesId)
        {
            if (selecteditem != null)
            {
                if (selecteditem.Count > 0)
                {
                    for (int i = 0; i < selecteditem.Count(); i++)
                    {
                        RestScheduleItem item = new RestScheduleItem();
                        item.RestSchedulesId = RestSchedulesId;
                        item.RestItemsId = selecteditem[i].Id;

                        await _RestScheduleItemRepository.InsertAsync(item);
                    }
                }
            }
        }

        public async Task UnAttachRestScheItem(List<RestItemsListDto> selecteditem, int RestSchedulesId)
        {
            try
            {
                var RestSchesItem = _RestScheduleItemRepository.GetAll().Where(a => a.RestSchedulesId == RestSchedulesId).ToList();

                List<RestScheduleItem> RestScheItemListDtos = ObjectMapper.Map<List<RestScheduleItem>>(RestSchesItem);

                if (selecteditem != null)
                {
                    if (selecteditem.Count > 0)
                    {
                        for (int i = 0; i < selecteditem.Count(); i++)
                        {
                            RestScheduleItem add = RestScheItemListDtos.Find(a => a.RestItemsId == selecteditem[i].Id);

                            if (add == null)
                            {
                                RestScheduleItem item = new RestScheduleItem();
                                item.RestSchedulesId = RestSchedulesId;
                                item.RestItemsId = selecteditem[i].Id;

                                await _RestScheduleItemRepository.InsertAsync(item);
                            }
                        }
                    }
                }
                if (RestScheItemListDtos != null)
                {
                    if (RestScheItemListDtos.Count() > 0)
                    {
                        for (int i = 0; i < RestScheItemListDtos.Count(); i++)
                        {
                            RestItemsListDto remove = selecteditem.Find(a => a.Id == RestScheItemListDtos[i].RestItemsId);

                            if (remove == null)
                            {
                                await _RestScheduleItemRepository.DeleteAsync(RestScheItemListDtos[i].Id);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        #endregion

        #region Non Schedule Items

        public List<RestItemsListDto> GetRestnonSchesbyID(int infoid)
        {
            var UserId = GetCurrentUser().Id;

            var RestNonSches = _RestNonSchItemRepository.GetAll().Where(a => a.RestInfoID == infoid && a.CreatorUserId == UserId).ToList();

            List<RestNonSchesListDto> RestNonSchesListDto = ObjectMapper.Map<List<RestNonSchesListDto>>(RestNonSches);
            List<RestItemsListDto> RestItemsListDtos = new List<RestItemsListDto>();

            if (RestNonSchesListDto.Count() > 0)
            {
                for (int i = 0; i < RestNonSchesListDto.Count(); i++)
                {
                    var restItemsListDto = _RestItemRepository.GetAll().Where(a => a.Id == RestNonSchesListDto[i].RestItemId).SingleOrDefault();

                    var item = ObjectMapper.Map<RestItemsListDto>(restItemsListDto);
                    item.RestNonSchId = RestNonSchesListDto[i].Id;
                    item.RestInfoId = RestNonSchesListDto[i].RestInfoID;

                    if (RestNonSchesListDto[i].SpecialSalesPrice > 0 && item.SalesPrice != RestNonSchesListDto[i].SpecialSalesPrice)
                    {
                        item.SalesPrice = RestNonSchesListDto[i].SpecialSalesPrice;
                    }

                    RestItemsListDtos.Add(item);
                }
            }
            return new List<RestItemsListDto>(RestItemsListDtos);
        }

        public async Task CreateNonRestSche(CreateRestScheInput input)
        {
            foreach (var schitem in input.targetItems)
            {
                var item = ObjectMapper.Map<RestNonSchItem>(input);

                item.RestInfoID = input.RestInfoID;
                item.RestItemId = schitem.Id;

                await _RestNonSchItemRepository.InsertAsync(item);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        public async Task EditRestNonSche(EditRestNonScheInput input)
        {
            await UnAttachRestNonScheItem(input.targetItems, input.RestInfoID);
        }

        public List<RestItemsListDto> GetSelectedNonSchRestItems(GetRestNonScheForEditInput input)
        {
            List<RestItemsListDto> selectdnonschrestitemListDtos = new List<RestItemsListDto>();
            List<int> nonschitemid = new List<int>();

            try
            {
                var restitems = _RestItemRepository.GetAll()
                                                   .Where(p => p.IsDeleted == false)
                                                   .OrderBy(p => p.ItemDesc)
                                                   .ToList();

                var restitemListDtos = ObjectMapper.Map<List<RestItemsListDto>>(restitems).OrderBy(a => a.Id).ToList();

                var RestNonSchesItem = _RestNonSchItemRepository.GetAll().Where(a => a.RestInfoID == input.RestInfoId).ToList();

                var RestNonScheItemListDtos = ObjectMapper.Map<List<RestNonSchesListDto>>(RestNonSchesItem).OrderBy(a => a.RestItemId).ToList();

                for (int i = 0; i < RestNonScheItemListDtos.Count(); i++)
                {
                    nonschitemid.Add(RestNonScheItemListDtos[i].RestItemId);
                }

                for (int n = 0; n < restitemListDtos.Count(); n++)
                {
                    if (nonschitemid.Contains(restitemListDtos[n].Id) == true &&
                        selectdnonschrestitemListDtos.Contains(restitemListDtos[n]) == false)
                    {
                        selectdnonschrestitemListDtos.Add(restitemListDtos[n]);
                    }
                }

                foreach (var item in selectdnonschrestitemListDtos)
                {
                    var ItemCategoryDesc = _ItemsCategoryRepository.GetAll().Where(a => a.Id == item.ItemsCategoryId).SingleOrDefault();
                    item.ItemsCategoryDesc = ItemCategoryDesc.CategoryDesc;
                }
                return new List<RestItemsListDto>(selectdnonschrestitemListDtos);

            }
            catch (Exception e)
            {

            }

            return new List<RestItemsListDto>(selectdnonschrestitemListDtos);
        }

        public List<RestItemsListDto> GetUnSelectedNonSchRestItemsforedit(GetRestNonScheForEditInput input)
        {
            List<RestItemsListDto> unselectdnonschrestitemListDtos = new List<RestItemsListDto>();
            List<int> nonschitemid = new List<int>();

            try
            {
                var restitems = _RestItemRepository.GetAll().Include(a => a.ItemsCategory)
                                                    .Where(p => p.IsDeleted == false)
                                                    .OrderBy(p => p.ItemDesc)
                                                    .ToList();

                var restitemListDtos = ObjectMapper.Map<List<RestItemsListDto>>(restitems).OrderBy(a => a.Id).ToList();

                var RestNonSchesItem = _RestNonSchItemRepository.GetAll().Where(a => a.RestInfoID == input.RestInfoId).ToList();

                var RestNonScheItemListDtos = ObjectMapper.Map<List<RestNonSchesListDto>>(RestNonSchesItem).OrderBy(a => a.RestItemId).ToList();

                for (int i = 0; i < RestNonScheItemListDtos.Count(); i++)
                {
                    nonschitemid.Add(RestNonScheItemListDtos[i].RestItemId);
                }

                for (int n = 0; n < restitemListDtos.Count(); n++)
                {
                    if (nonschitemid.Contains(restitemListDtos[n].Id) == false)
                    {
                        unselectdnonschrestitemListDtos.Add(restitemListDtos[n]);
                    }
                }

                foreach (var item in unselectdnonschrestitemListDtos)
                {
                    var ItemCategoryDesc = _ItemsCategoryRepository.GetAll().Where(a => a.Id == item.ItemsCategoryId).SingleOrDefault();
                    item.ItemsCategoryDesc = ItemCategoryDesc.CategoryDesc;
                }
                return new List<RestItemsListDto>(unselectdnonschrestitemListDtos);

            }
            catch (Exception e)
            {

            }

            return new List<RestItemsListDto>(unselectdnonschrestitemListDtos);
        }

        public async Task UnAttachRestNonScheItem(List<RestItemsListDto> selecteditem, int RestInfoID)
        {
            try
            {
                var RestNonSchesItem = _RestNonSchItemRepository.GetAll().Where(a => a.RestInfoID == RestInfoID).ToList();

                List<RestNonSchItem> RestNonScheItemListDtos = ObjectMapper.Map<List<RestNonSchItem>>(RestNonSchesItem);

                if (selecteditem != null)
                {
                    if (selecteditem.Count > 0)
                    {
                        for (int i = 0; i < selecteditem.Count(); i++)
                        {
                            RestNonSchItem add = RestNonScheItemListDtos.Find(a => a.RestItemId == selecteditem[i].Id);

                            if (add == null)
                            {
                                RestNonSchItem item = new RestNonSchItem();
                                item.RestInfoID = RestInfoID;
                                item.RestItemId = selecteditem[i].Id;

                                await _RestNonSchItemRepository.InsertAsync(item);
                            }
                        }
                    }
                }
                if (RestNonScheItemListDtos != null)
                {
                    if (RestNonScheItemListDtos.Count() > 0)
                    {
                        for (int i = 0; i < RestNonScheItemListDtos.Count(); i++)
                        {
                            RestItemsListDto remove = selecteditem.Find(a => a.Id == RestNonScheItemListDtos[i].RestItemId);

                            if (remove == null)
                            {
                                await _RestNonSchItemRepository.DeleteAsync(RestNonScheItemListDtos[i].Id);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public async Task UpdateRestItemSalesPrice(List<RestItemsListDto> NonSchesrestitems, int RestInfoID)
        {
            var RestNonSchesItem = _RestNonSchItemRepository.GetAll().Where(a => a.RestInfoID == RestInfoID).ToList();

            List<RestNonSchItem> RestNonScheItemListDtos = ObjectMapper.Map<List<RestNonSchItem>>(RestNonSchesItem);

            var restitems = _RestItemRepository.GetAll().Where(p => p.IsDeleted == false).OrderBy(p => p.ItemDesc).ToList();

            List<RestItemsListDto> restitemListDtos = ObjectMapper.Map<List<RestItemsListDto>>(restitems).OrderBy(a => a.Id).ToList();

            for (int i = 0; i < NonSchesrestitems.Count(); i++)
            {
                for (int n = 0; n < restitemListDtos.Count; n++)
                {
                    if (NonSchesrestitems[i].Id == restitemListDtos[n].Id)
                    {
                        if (NonSchesrestitems[i].SalesPrice != restitemListDtos[n].SalesPrice)
                        {
                            RestNonSchItem update = RestNonScheItemListDtos.Find(a => a.RestItemId == NonSchesrestitems[i].Id);

                            update.SpecialSalesPrice = NonSchesrestitems[i].SalesPrice;

                            await _RestNonSchItemRepository.UpdateAsync(update);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
