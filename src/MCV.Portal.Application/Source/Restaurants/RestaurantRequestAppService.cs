using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using MCV.Portal.Authorization.Users;
using MCV.Portal.Source.Restaurant;
using MCV.Portal.Source.Restaurant.Dto;
using MCV.Portal.Source.Restaurants.Dto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MCV.Portal.Source.Restaurants
{
    //[AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantReq)]
    public class RestaurantRequestAppService : PortalAppServiceBase, IRestaurantRequestAppService
    {
        private readonly IRepository<RestRequest> _RestReqRepository;
        private readonly IRepository<RestSchedule> _RestSchRepository;
        private readonly IRepository<EmployeesView> _EmployeesViewRepository;
        private readonly IRepository<RestInfo> _RestInfoRepository;
        private readonly IRepository<RestItem> _RestItemRepository;
        private readonly IRepository<RestScheduleItem> _RestScheduleItemRepository;
        private readonly IRepository<RestRequestItem> _RestRequestItemRepository;
        private readonly IRepository<RestCategory> _RestCategoryRepository;
        private readonly IRepository<ItemsCategory> _ItemsCategoryRepository;
        private readonly IRepository<RestNonSchItem> _RestNonSchItemRepository;
        private readonly IRepository<RequestStatus> _RequestStatusRepository;
        private readonly IRepository<PaymentType> _PaymentTypesRepository;
        private readonly IRepository<RequestLog> _RequestLogRepository;
        private readonly IRepository<RestResponse> _RestRespRepository;

        public RestaurantRequestAppService(IRepository<RestRequest> RestReqRepository,
                                           IRepository<RestSchedule> RestSchRepository,
                                           IRepository<EmployeesView> EmployeesViewRepository,
                                           IRepository<RestInfo> RestInfoRepository,
                                           IRepository<RestItem> RestItemRepository,
                                           IRepository<RestScheduleItem> RestScheduleItem,
                                           IRepository<RestRequestItem> RestRequestItem,
                                           IRepository<RestCategory> RestCategoryRepository,
                                           IRepository<ItemsCategory> ItemsCategoryRepository,
                                           IRepository<RestNonSchItem> RestNonSchItemRepository,
                                           IRepository<RequestStatus> RequestStatusRepository,
                                           IRepository<PaymentType> PaymentTypesRepository,
                                           IRepository<RequestLog> RequestLogRepository,
                                           IRepository<RestResponse> RestRespRepository)
        {
            _RestReqRepository = RestReqRepository;
            _RestSchRepository = RestSchRepository;
            _EmployeesViewRepository = EmployeesViewRepository;
            _RestInfoRepository = RestInfoRepository;
            _RestItemRepository = RestItemRepository;
            _RestScheduleItemRepository = RestScheduleItem;
            _RestRequestItemRepository = RestRequestItem;
            _RestCategoryRepository = RestCategoryRepository;
            _ItemsCategoryRepository = ItemsCategoryRepository;
            _RestNonSchItemRepository = RestNonSchItemRepository;
            _RequestStatusRepository = RequestStatusRepository;
            _PaymentTypesRepository = PaymentTypesRepository;
            _RequestLogRepository = RequestLogRepository;
            _RestRespRepository = RestRespRepository;
        }

        public PagedResultDto<RestReqListDto> GetRestReqs(GetRestReqInput input)
        {
            var UserId = GetCurrentUser().Id;

            List<RestRequest> RestReqs = new List<RestRequest>();

            var payments = _PaymentTypesRepository.GetAll().Where(a => a.PaymentTypeDesc.Contains(input.Filter)).Select(a => a.Id).SingleOrDefault();
            var day = _RestSchRepository.GetAll().Where(a => a.Day.Contains(input.Filter)).Select(a => a.Day).FirstOrDefault();
            var status = _RequestStatusRepository.GetAll().Where(a => a.StatusDesc.Contains(input.Filter)).Select(a => a.Id).SingleOrDefault();

            if (payments != 0)
            {
                RestReqs = _RestReqRepository.GetAll()
                                .Where(a => a.CreatorUserId == UserId && a.PaymentTypeID == payments)
                                .OrderByDescending(a => a.CreationTime)
                               .ToList();
            }
            if (day != null)
            {
                RestReqs = _RestReqRepository.GetAll().Include(a => a.RestSchedules)
                                .Where(a => a.CreatorUserId == UserId && a.RestSchedules.Day == day)
                                .OrderByDescending(a => a.CreationTime)
                               .ToList();
            }
            if (status != 0)
            {
                RestReqs = _RestReqRepository.GetAll().Include(a => a.RestSchedules)
                                .Where(a => a.CreatorUserId == UserId && a.RequestStatusID == status)
                                .OrderByDescending(a => a.CreationTime)
                               .ToList();
            }
            else
            {
                RestReqs = _RestReqRepository.GetAll()
                           .Where(a => a.CreatorUserId == UserId)
                           .OrderByDescending(a => a.CreationTime)
                          .ToList();
            }

            var RestReqsCount = RestReqs.Count();

            List<RestReqListDto> RestReqListDtos = ObjectMapper.Map<List<RestReqListDto>>(RestReqs);

            foreach (RestReqListDto req in RestReqListDtos)
            {
                var RestSches = _RestSchRepository.GetAll().Where(a => a.Id == req.RestSchedulesId).SingleOrDefault();

                int? RestInfoID = 0;
                if (RestSches != null)
                {
                    RestInfoID = RestSches.RestInfoID;
                    req.Day = RestSches.Day;

                }
                else
                {
                    RestInfoID = req.RestInfosID;
                }

                var RestInfos = _RestInfoRepository.GetAll().Where(a => a.Id == RestInfoID).SingleOrDefault();

                if (RestInfos != null)
                {
                    string Area = RestInfos.Area;
                    string Building = RestInfos.Building;

                    var RestCategoryDesc = _RestCategoryRepository.GetAll().Where(a => a.Id == RestInfos.RestCategoryId).SingleOrDefault();
                    string RestDesc = RestCategoryDesc.RestCategoryDesc;

                    req.RestaurantInfo = Area + " - " + Building + " / " + RestDesc;
                }
                req.Username = _EmployeesViewRepository.GetAll().Where(a => a.emp_id == req.UserId).Select(a => a.emp_username).SingleOrDefault();

                req.StatusDesc = _RequestStatusRepository.GetAll().Where(a => a.Id == req.RequestStatusID).Select(a => a.StatusDesc).SingleOrDefault();

                req.PaymentTypeDesc = _PaymentTypesRepository.GetAll().Where(a => a.Id == req.PaymentTypeID).Select(a => a.PaymentTypeDesc).SingleOrDefault();

                string time = req.CreationTime.ToString("dd/MM/yyyy");
                req.Time = time;
            }

            return new PagedResultDto<RestReqListDto>(RestReqsCount, RestReqListDtos);
        }

        public List<RestSchesListDto> GetRestSches()
        {
            var RestSches = _RestSchRepository.GetAll().Distinct().ToList();

            List<RestSchesListDto> RestScheListDtos = ObjectMapper.Map<List<RestSchesListDto>>(RestSches);

            foreach (RestSchesListDto sch in RestScheListDtos)
            {
                var RestInfos = _RestInfoRepository.GetAll().Where(a => a.Id == sch.RestInfoID).Distinct().ToList();

                string Country = RestInfos.Select(a => a.Country).SingleOrDefault();
                string City = RestInfos.Select(a => a.City).SingleOrDefault();
                string Area = RestInfos.Select(a => a.Area).SingleOrDefault();
                string Building = RestInfos.Select(a => a.Building).SingleOrDefault();

                sch.RestaurantInfo = Country + " - " + City + " - " + Area + " - " + Building;

                int count = RestScheListDtos.Select(a => a.RestInfoID == sch.RestInfoID).Count();

                if (count > 1)
                {
                    RestScheListDtos.Select(a => a.RestInfoID == sch.RestInfoID).Distinct().FirstOrDefault();
                }
            }

            return new List<RestSchesListDto>(RestScheListDtos);
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

        public List<EmployeesViewList> GetEmployees()
        {
            var Employees = _EmployeesViewRepository.GetAll().ToList();

            List<EmployeesViewList> EmployeesList = ObjectMapper.Map<List<EmployeesViewList>>(Employees);

            return new List<EmployeesViewList>(EmployeesList);
        }

        public List<PaymentTypesListDto> GetPaymentTyps()
        {
            var payments = _PaymentTypesRepository.GetAll().ToList();

            List<PaymentTypesListDto> PaymentTypesListDto = ObjectMapper.Map<List<PaymentTypesListDto>>(payments);

            return new List<PaymentTypesListDto>(PaymentTypesListDto);
        }

        public List<RequestLogListDto> GetRquestLogs(int RestRequestId)
        {
            var RequestLog = _RequestLogRepository.GetAll().OrderByDescending(a => a.ActionDateTime).Where(a => a.RestRequestId == RestRequestId).ToList();

            List<RequestLogListDto> RequestLogListDto = ObjectMapper.Map<List<RequestLogListDto>>(RequestLog);

            foreach (var log in RequestLogListDto)
            {
                log.Username = UserManager.Users.Where(a => a.Id == log.UserId).Select(a => a.UserName).SingleOrDefault();
                log.StatusDesc = _RequestStatusRepository.GetAll().Where(a => a.Id == log.RequestStatusId).Select(a => a.StatusDesc).SingleOrDefault();
                log.DateTime = log.ActionDateTime.Date.ToString("dd/MM/yyyy") + " " + log.ActionDateTime.ToShortTimeString();
            }

            return new List<RequestLogListDto>(RequestLogListDto);
        }

        public RestItemsListDto GetRestItem(int RestItemId, int RestInfoId)
        {
            var Restitem = _RestItemRepository.GetAll().Where(a => a.Id == RestItemId).SingleOrDefault();

            RestItemsListDto RestItems = ObjectMapper.Map<RestItemsListDto>(Restitem);

            var RestNonSchItem = _RestNonSchItemRepository.GetAll().Where(a => a.RestItemId == RestItemId && a.RestInfoID == RestInfoId && a.IsDeleted == false).FirstOrDefault();

            if (RestNonSchItem != null)
            {
                if (RestItems.SalesPrice != RestNonSchItem.SpecialSalesPrice && RestNonSchItem.SpecialSalesPrice != 0)
                {
                    RestItems.SalesPrice = RestNonSchItem.SpecialSalesPrice;
                }
            }

            return RestItems;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantReq_CreateRestReq)]
        public async Task CreateRestReqAsDraft(CreateRestReqInput input)
        {
            var Req = ObjectMapper.Map<RestRequest>(input);

            var SAPCode = GetCurrentUser().SAPCode;

            if (SAPCode != null || SAPCode != 0)
            {
                Req.RequesterId = (long)SAPCode;
            }
            else
            {
                var Username = GetCurrentUser().UserName;

                var Employe = _EmployeesViewRepository.GetAll().Where(a => a.emp_username == Username).SingleOrDefault().emp_id;

                Req.RequesterId = (long)Employe;
            }

            if (Req.RestSchedulesId == null || Req.RestSchedulesId == 0)
            {
                Req.RestSchedulesId = null;
            }

            var ReqStatusId = _RequestStatusRepository.GetAll().Where(a => a.StatusDesc == "Draft").FirstOrDefault().Id;

            Req.RequestStatusID = ReqStatusId;

            await _RestReqRepository.InsertAsync(Req);
            await CurrentUnitOfWork.SaveChangesAsync();

            RequestLog requestLog = new RequestLog();
            var userid = GetCurrentUser().Id;

            requestLog.RestRequestId = Req.Id;
            requestLog.RequestStatusId = (int)Req.RequestStatusID;
            requestLog.ActionDateTime = DateTime.Now;
            requestLog.UserId = (int)userid;

            await _RequestLogRepository.InsertAsync(requestLog);

            await SelectRestReqScheItem(input.RestItemsListDto, Req.Id, input.RestSchedulesId, input.RestInfosID);
        }

        public async Task CreateRestReqSubmit(CreateRestReqInput input)
        {
            var Req = ObjectMapper.Map<RestRequest>(input);

            var SAPCode = GetCurrentUser().SAPCode;

            if (SAPCode != null || SAPCode != 0)
            {
                Req.RequesterId = (long)SAPCode;
            }
            else
            {
                var Username = GetCurrentUser().UserName;

                var Employe = _EmployeesViewRepository.GetAll().Where(a => a.emp_username == Username).FirstOrDefault().emp_id;

                Req.RequesterId = (long)Employe;
            }

            if (Req.RestSchedulesId == null || Req.RestSchedulesId == 0)
            {
                Req.RestSchedulesId = null;
            }

            var ReqStatusId = _RequestStatusRepository.GetAll().Where(a => a.StatusDesc == "Submitted").FirstOrDefault().Id;

            Req.RequestStatusID = ReqStatusId;

            await _RestReqRepository.InsertAsync(Req);
            await CurrentUnitOfWork.SaveChangesAsync();

            RequestLog requestLog = new RequestLog();
            var userid = GetCurrentUser().Id;

            requestLog.RestRequestId = Req.Id;
            requestLog.RequestStatusId = (int)Req.RequestStatusID;
            requestLog.ActionDateTime = DateTime.Now;
            requestLog.UserId = (int)userid;

            await _RequestLogRepository.InsertAsync(requestLog);

            await SelectRestReqScheItem(input.RestItemsListDto, Req.Id, input.RestSchedulesId, input.RestInfosID);
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantReq_DeleteRestReq)]
        public async Task DeleteRestReq(EntityDto input)
        {
            await _RestReqRepository.DeleteAsync(input.Id);
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantReq_EditRestReq)]
        public GetRestReqForEditOutput GetRestReqForEdit(GetRestReqForEditInput input)
        {
            RestSchedule RestSches = new RestSchedule();
            RestNonSchItem RestNonSches = new RestNonSchItem();

            var RestReq = _RestReqRepository.Get(input.Id);

            GetRestReqForEditOutput req = ObjectMapper.Map<GetRestReqForEditOutput>(RestReq);

            RestSches = _RestSchRepository.GetAll().Where(a => a.Id == req.RestSchedulesId).SingleOrDefault();

            RestNonSches = _RestNonSchItemRepository.GetAll().Where(a => a.Id == req.RestNonSchItemID).SingleOrDefault();

            var RestInfos = _RestInfoRepository.GetAll().Where(a => a.Id == req.RestInfosID).SingleOrDefault();

            var RestCategoryDesc = _RestCategoryRepository.GetAll().Where(a => a.Id == RestInfos.RestCategoryId).Select(a => a.RestCategoryDesc).SingleOrDefault();

            req.RestaurantInfo = RestInfos.Area + " - " + RestInfos.Building + " - " +  " / " + RestCategoryDesc;

            req.Username = _EmployeesViewRepository.GetAll().Where(a => a.emp_id == RestReq.UserId).Select(a => a.emp_username).FirstOrDefault();

            if (RestSches != null)
            {
                req.RestInfosID = RestSches.RestInfoID;

                req.Day = RestSches.Day;

                req.Date = RestSches.CurrentDate.ToString("dd-MM-yyyy");
            }

            req.GetDates = GetDates(req.RestInfosID);

            req.SelectedRestItems = GetRestSchItems(req.RestSchedulesId, "", input.Id);

            return req;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantReq_EditRestReq)]
        public async Task EditRestReq(EditRestReqInput input)
        {
            var RestReq = await _RestReqRepository.GetAsync(input.Id);
            RestReq.RestSchedulesId = input.RestSchedulesId;
            RestReq.RequesterId = input.RequesterId;
            RestReq.UserId = input.UserId;

            var ReqStatusId = _RequestStatusRepository.GetAll().Where(a => a.StatusDesc == "Submitted").FirstOrDefault().Id;
            RestReq.RequestStatusID = ReqStatusId;

            await _RestReqRepository.UpdateAsync(RestReq);

            RequestLog requestLog = new RequestLog();
            var userid = GetCurrentUser().Id;

            requestLog.RestRequestId = RestReq.Id;
            requestLog.RequestStatusId = (int)RestReq.RequestStatusID;
            requestLog.ActionDateTime = DateTime.Now;
            requestLog.UserId = (int)userid;

            await _RequestLogRepository.InsertAsync(requestLog);

            await UpdateSelectRestReqScheItem(input.targetItems);
        }

        public async Task UpdateSelectRestReqScheItem(List<RestItemsListDto> input)
        {
            try
            {
                if (input.Count() > 0)
                {
                    for (int i = 0; i < input.Count(); i++)
                    {
                        RestRequestItem RestRequestItems = _RestRequestItemRepository.GetAll().Where(a => a.Id == input[i].RestRequestItemsId).FirstOrDefault();

                        if (RestRequestItems != null)
                        {

                            if (RestRequestItems.RestRequests.RestSchedulesId == 0)
                            {
                                RestRequestItems.RestRequests.RestSchedulesId = null;
                            }

                            RestRequestItems.Count = input[i].Count;
                            _RestRequestItemRepository.Update(RestRequestItems);

                            await CurrentUnitOfWork.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (DbUpdateException e)
            {
                SqlException s = e.InnerException.InnerException as SqlException;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<RestItemsListDto> GetRestSchItems(int RestSchedulesId, string filter, int RestRequestsId)
        {
            List<RestItemsListDto> ItemsList = new List<RestItemsListDto>();
            try
            {

                RestItemsListDto Items = new RestItemsListDto();

                List<RestScheduleItem> schitems = _RestScheduleItemRepository.GetAll().Where(a => a.RestSchedulesId == RestSchedulesId).ToList();

                var restreq = _RestReqRepository.GetAll().Where(a => a.Id == RestRequestsId).SingleOrDefault();

                var restreqitems = _RestRequestItemRepository.GetAll().Where(a => a.RestRequestsId == RestRequestsId).ToList();

                if (filter != null && filter != "")
                {
                    List<RestItem> FilterItemsList = _RestItemRepository.GetAll().Where(a => a.ItemDesc.Contains(filter) ||
                                                           a.ItemsCategory.CategoryDesc.Contains(filter) ||
                                                           a.SalesPrice.ToString().Contains(filter)).ToList();

                    ItemsList = ObjectMapper.Map<List<RestItemsListDto>>(FilterItemsList);
                }
                else
                {
                    foreach (RestScheduleItem schitem in schitems)
                    {
                        RestItem items = _RestItemRepository.GetAll().Where(a => a.Id == schitem.RestItemsId && a.IsDeleted == false).SingleOrDefault();

                        Items = ObjectMapper.Map<RestItemsListDto>(items);

                        if (Items != null)
                        {
                            ItemsList.Add(Items);
                        }

                        foreach (var ItemCategory in ItemsList)
                        {
                            var ItemCategoryDesc = _ItemsCategoryRepository.GetAll().Where(a => a.Id == ItemCategory.ItemsCategoryId).SingleOrDefault();
                            ItemCategory.ItemsCategoryDesc = ItemCategoryDesc.CategoryDesc;
                        }

                        if (restreqitems.Count() > 0)
                        {
                            foreach (var reqitem in restreqitems)
                            {
                                if (reqitem.RestNonSchItemId != 0 && reqitem.RestNonSchItemId != null)
                                {
                                    var RestNonSchesItem = _RestNonSchItemRepository.GetAll().Where(a => a.Id == reqitem.RestNonSchItemId && a.RestInfoID == restreq.RestInfosID && a.IsDeleted == false).SingleOrDefault();
                                    var RestItem = _RestItemRepository.GetAll().Where(a => a.Id == RestNonSchesItem.RestItemId && a.IsDeleted == false).SingleOrDefault();

                                    var RestItemdto = ObjectMapper.Map<RestItemsListDto>(RestItem);
                                    RestItemdto.Count = reqitem.Count;
                                    RestItemdto.ItemsCategoryDesc = _ItemsCategoryRepository.GetAll().Where(a => a.Id == RestItem.ItemsCategoryId && a.IsDeleted == false).Select(a => a.CategoryDesc).SingleOrDefault();
                                    RestItemdto.NotAvailable = _RestRespRepository.GetAll().Where(a => a.RestRequestItemsId == reqitem.Id).Select(a => a.NotAvailable).SingleOrDefault();
                                    RestItemdto.RestNonSchId = RestNonSchesItem.Id;

                                    RestItemsListDto exist = ItemsList.Find(a => a.Id == RestItemdto.Id &&
                                                                            a.RestRequestItemsId == RestItemdto.RestRequestItemsId);

                                    if (exist != null)
                                    {
                                        ItemsList.Remove(exist);
                                        ItemsList.Add(RestItemdto);
                                    }
                                }
                                else if (reqitem.RestScheduleItemsId != 0 && reqitem.RestScheduleItemsId != null)
                                {
                                    var RestScheduleItems = _RestScheduleItemRepository.GetAll().Include(a => a.RestSchedules).Where(a => a.Id == reqitem.RestScheduleItemsId && a.RestSchedules.RestInfoID == restreq.RestInfosID && a.IsDeleted == false).SingleOrDefault();
                                    var RestItem = _RestItemRepository.GetAll().Where(a => a.Id == RestScheduleItems.RestItemsId && a.IsDeleted == false).SingleOrDefault();

                                    var RestItemdto = ObjectMapper.Map<RestItemsListDto>(RestItem);

                                    RestItemdto.Count = reqitem.Count;
                                    RestItemdto.ItemsCategoryDesc = _ItemsCategoryRepository.GetAll().Where(a => a.Id == RestItem.ItemsCategoryId && a.IsDeleted == false).Select(a => a.CategoryDesc).SingleOrDefault();
                                    RestItemdto.NotAvailable = _RestRespRepository.GetAll().Where(a => a.RestRequestItemsId == reqitem.Id).Select(a => a.NotAvailable).SingleOrDefault();
                                    RestItemdto.RestSchedulesId = RestScheduleItems.RestSchedulesId;

                                    RestItemsListDto exist = ItemsList.Find(a => a.Id == RestItemdto.Id &&
                                                                                 a.RestRequestItemsId == RestItemdto.RestRequestItemsId);

                                    if (exist != null)
                                    {
                                        ItemsList.Remove(exist);
                                        ItemsList.Add(RestItemdto);
                                    }
                                }
                            }
                        }
                    }
                }

                return new List<RestItemsListDto>(ItemsList);
            }
            catch (Exception e)
            {

            }

            return new List<RestItemsListDto>(ItemsList);
        }

        public List<RestItemsListDto> GetReqRestSchItems(GetRestReqSchInput input)
        {
            List<RestItemsListDto> ItemsList = new List<RestItemsListDto>();
            try
            {

                RestNonSchItem Nonschitem = new RestNonSchItem();

                RestItemsListDto Items = new RestItemsListDto();

                RestRequest RestReq = _RestReqRepository.GetAll().Where(a => a.Id == input.Id).SingleOrDefault();

                var RestReqitem = _RestRequestItemRepository.GetAll().Where(a => a.RestRequestsId == input.Id).ToList();

                List<RestReqItemListDto> RestReqItemListDtos = ObjectMapper.Map<List<RestReqItemListDto>>(RestReqitem);

                List<RestScheduleItem> schitems = _RestScheduleItemRepository.GetAll().Where(a => a.RestSchedulesId == RestReq.RestSchedulesId).ToList();

                foreach (RestReqItemListDto reqitem in RestReqItemListDtos)
                {
                    Nonschitem = _RestNonSchItemRepository.GetAll().Where(a => a.Id == reqitem.RestNonSchItemId).SingleOrDefault();
                }

                if (schitems.Count() > 0)
                {
                    foreach (RestScheduleItem item in schitems)
                    {
                        RestItem items = _RestItemRepository.GetAll().Where(a => a.Id == item.RestItemsId).SingleOrDefault();

                        Items = ObjectMapper.Map<RestItemsListDto>(items);

                        foreach (RestReqItemListDto reqitem in RestReqItemListDtos)
                        {
                            if (item.Id == reqitem.RestScheduleItemsId)
                            {
                                Items.Count = reqitem.Count;

                                if (reqitem.Count > 0)
                                {
                                    Items.RestRequestItemsId = reqitem.Id;
                                }

                                var RestResponse = _RestRespRepository.GetAll().Where(a => a.RestRequestItemsId == reqitem.Id).ToList();

                                List<RestRespListDto> RestRespListDtos = ObjectMapper.Map<List<RestRespListDto>>(RestResponse);

                                foreach (RestRespListDto restrsp in RestRespListDtos)
                                {
                                    if (restrsp.RestRequestItemsId == reqitem.Id)
                                    {
                                        Items.NotAvailable = restrsp.NotAvailable;
                                    }
                                }
                                ItemsList.Add(Items);
                            }
                        }
                    }
                }

                if (Nonschitem != null)
                {
                    foreach (RestReqItemListDto reqitem in RestReqItemListDtos)
                    {
                        Nonschitem = _RestNonSchItemRepository.GetAll().Where(a => a.Id == reqitem.RestNonSchItemId).SingleOrDefault();

                        RestItem items = _RestItemRepository.GetAll().Where(a => a.Id == Nonschitem.RestItemId).SingleOrDefault();

                        Items = ObjectMapper.Map<RestItemsListDto>(items);

                        Items.Count = reqitem.Count;

                        if (reqitem.Count > 0)
                        {
                            Items.RestRequestItemsId = reqitem.Id;
                        }

                        var RestResponse = _RestRespRepository.GetAll().Where(a => a.RestRequestItemsId == reqitem.Id).SingleOrDefault();

                        if (RestResponse != null)
                        {
                            if (RestResponse.RestRequestItemsId == reqitem.Id)
                            {
                                Items.NotAvailable = RestResponse.NotAvailable;
                            }
                        }
                        ItemsList.Add(Items);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<RestItemsListDto>(ItemsList);
        }

        public async Task SelectRestReqScheItem(List<RestItemsListDto> input, int RestRequestsId, int? RestSchedulesId, int? RestInfosID)
        {
            try
            {
                if (input.Count() > 0)
                {
                    for (int i = 0; i < input.Count(); i++)
                    {
                        if (input[i].Count > 0)
                        {
                            RestScheduleItem ScheduleItem = _RestScheduleItemRepository.GetAll().Where(a => a.RestItemsId == input[i].Id && a.RestSchedulesId == RestSchedulesId).FirstOrDefault();

                            if (ScheduleItem != null)
                            {
                                var reqitem = new RestRequestItem();

                                reqitem.RestScheduleItemsId = ScheduleItem.Id;
                                reqitem.RestRequestsId = RestRequestsId;
                                reqitem.Count = input[i].Count;

                                await _RestRequestItemRepository.InsertAsync(reqitem);
                            }
                            else
                            {
                                RestNonSchItem RestNonSchItem = _RestNonSchItemRepository.GetAll().Where(a => a.RestItemId == input[i].Id && a.RestInfoID == RestInfosID).FirstOrDefault();


                                var reqitem = new RestRequestItem();

                                reqitem.RestNonSchItemId = RestNonSchItem.Id;
                                reqitem.RestRequestsId = RestRequestsId;
                                reqitem.Count = input[i].Count;

                                await _RestRequestItemRepository.InsertAsync(reqitem);
                            }
                        }
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                }
            }
            catch (DbUpdateException e)
            {
                SqlException s = e.InnerException.InnerException as SqlException;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<RestItemsListDto> GetNonScheRestItems(int RestInfoID, string filter)
        {
            List<RestItemsListDto> restitemListDtos = new List<RestItemsListDto>();

            List<RestNonSchesListDto> RestNonSchesListDto = new List<RestNonSchesListDto>();

            try
            {
                var RestNonSchItem = _RestNonSchItemRepository.GetAll().Where(a => a.RestInfoID == RestInfoID && a.IsDeleted == false).ToList();

                RestNonSchesListDto = ObjectMapper.Map<List<RestNonSchesListDto>>(RestNonSchItem);

                for (int i = 0; i < RestNonSchesListDto.Count(); i++)
                {
                    int RestItemId = RestNonSchesListDto[i].RestItemId;

                    RestItem restitems = _RestItemRepository.GetAll().Where(a => a.Id == RestItemId ||
                                                        a.ItemDesc.Contains(filter) ||
                                                        a.ItemsCategory.CategoryDesc.Contains(filter) ||
                                                        a.SalesPrice.ToString().Contains(filter)).SingleOrDefault();

                    RestItemsListDto restitem = ObjectMapper.Map<RestItemsListDto>(restitems);

                    if (RestNonSchesListDto[i].SpecialSalesPrice > 0 &&
                        restitem.SalesPrice != RestNonSchesListDto[i].SpecialSalesPrice && restitem != null)
                    {
                        restitem.SalesPrice = RestNonSchesListDto[i].SpecialSalesPrice;
                    }
                    if (restitem != null)
                    {
                        var ItemCategoryDesc = _ItemsCategoryRepository.GetAll().Where(a => a.Id == restitem.ItemsCategoryId).SingleOrDefault();
                        restitem.ItemsCategoryDesc = ItemCategoryDesc.CategoryDesc;

                        restitemListDtos.Add(restitem);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new List<RestItemsListDto>(restitemListDtos);
        }

        public List<RestItemsListDto> GetSelectedRestItems(GetRestReqForEditInput input)
        {
            List<RestItemsListDto> RestSchItems = new List<RestItemsListDto>();

            try
            {

                var restreq = _RestReqRepository.GetAll().Where(a => a.Id == input.Id).SingleOrDefault();

                var restreqitems = _RestRequestItemRepository.GetAll().Where(a => a.RestRequestsId == input.Id).ToList();

                foreach (var reqitem in restreqitems)
                {
                    if (reqitem.RestNonSchItemId != 0 && reqitem.RestNonSchItemId != null)
                    {
                        var RestNonSchesItem = _RestNonSchItemRepository.GetAll().Where(a => a.Id == reqitem.RestNonSchItemId && a.RestInfoID == restreq.RestInfosID && a.IsDeleted == false).SingleOrDefault();
                        var RestItem = _RestItemRepository.GetAll().Where(a => a.Id == RestNonSchesItem.RestItemId && a.IsDeleted == false).SingleOrDefault();

                        var RestItemdto = ObjectMapper.Map<RestItemsListDto>(RestItem);
                        RestItemdto.Count = reqitem.Count;
                        RestItemdto.ItemsCategoryDesc = _ItemsCategoryRepository.GetAll().Where(a => a.Id == RestItem.ItemsCategoryId && a.IsDeleted == false).Select(a => a.CategoryDesc).SingleOrDefault();

                        RestSchItems.Add(RestItemdto);
                    }
                    else if (reqitem.RestScheduleItemsId != 0 && reqitem.RestScheduleItemsId != null)
                    {
                        var RestScheduleItems = _RestScheduleItemRepository.GetAll().Include(a => a.RestSchedules).Where(a => a.Id == reqitem.RestScheduleItemsId && a.RestSchedules.RestInfoID == restreq.RestInfosID && a.IsDeleted == false).SingleOrDefault();
                        var RestItem = _RestItemRepository.GetAll().Where(a => a.Id == RestScheduleItems.RestItemsId && a.IsDeleted == false).SingleOrDefault();

                        var RestItemdto = ObjectMapper.Map<RestItemsListDto>(RestItem);
                        RestItemdto.Count = reqitem.Count;
                        RestItemdto.ItemsCategoryDesc = _ItemsCategoryRepository.GetAll().Where(a => a.Id == RestItem.ItemsCategoryId && a.IsDeleted == false).Select(a => a.CategoryDesc).SingleOrDefault();

                        RestSchItems.Add(RestItemdto);
                    }
                }

                return new List<RestItemsListDto>(RestSchItems);

            }
            catch (Exception e)
            {

            }

            return new List<RestItemsListDto>(RestSchItems);
        }

        public List<RestItemsListDto> GetSelectedNonSchRestItems(int RestInfoID)
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

                var RestNonSchesItem = _RestNonSchItemRepository.GetAll().Where(a => a.RestInfoID == RestInfoID).ToList();

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

        public List<RestItemsListDto> GetSelectedSchRestItems(int RestInfoID)
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

                var RestNonSchesItem = _RestNonSchItemRepository.GetAll().Where(a => a.RestInfoID == RestInfoID).ToList();

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

        public List<Days> GetDates(int RestInfoID)
        {
            List<RestSchedule> schdates = new List<RestSchedule>();

            schdates = _RestSchRepository.GetAll().Where(a => a.RestInfoID == RestInfoID).ToList();

            List<Days> datetime = new List<Days>();

            for (int i = 0; i < schdates.Count(); i++)
            {
                Days days = new Days();

                days.Day = schdates[i].CurrentDate.DayOfWeek.ToString();

                days.Date = schdates[i].CurrentDate.ToString("dd-MM-yyyy");

                days.RestSchesId = schdates[i].Id;

                datetime.Add(days);
            }
            return datetime;
        }

        public async Task StatusProceed(int RestRequestsId)
        {
            var RestReq = await _RestReqRepository.GetAsync(RestRequestsId);

            var ReqStatusId = _RequestStatusRepository.GetAll().Where(a => a.StatusDesc == "Proceed").FirstOrDefault().Id;
            RestReq.RequestStatusID = ReqStatusId;

            await _RestReqRepository.UpdateAsync(RestReq);

            RequestLog requestLog = new RequestLog();
            var userid = GetCurrentUser().Id;

            requestLog.RestRequestId = RestReq.Id;
            requestLog.RequestStatusId = (int)RestReq.RequestStatusID;
            requestLog.ActionDateTime = DateTime.Now;
            requestLog.UserId = (int)userid;

            await _RequestLogRepository.InsertAsync(requestLog);
        }

        public async Task StatusCancelOrder(int RestRequestsId)
        {
            var RestReq = await _RestReqRepository.GetAsync(RestRequestsId);

            var ReqStatusId = _RequestStatusRepository.GetAll().Where(a => a.StatusDesc == "Cancel Order").FirstOrDefault().Id;
            RestReq.RequestStatusID = ReqStatusId;

            await _RestReqRepository.UpdateAsync(RestReq);

            RequestLog requestLog = new RequestLog();
            var userid = GetCurrentUser().Id;

            requestLog.RestRequestId = RestReq.Id;
            requestLog.RequestStatusId = (int)RestReq.RequestStatusID;
            requestLog.ActionDateTime = DateTime.Now;
            requestLog.UserId = (int)userid;

            await _RequestLogRepository.InsertAsync(requestLog);
        }
    }
}
