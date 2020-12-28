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
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MCV.Portal.Source.Restaurants
{
    //[AbpAuthorize(AppPermissions.Pages_Tenant_RestaurantResp)]
    public class RestaurantResponseAppService : PortalAppServiceBase, IRestaurantResponseAppService
    {

        private readonly IRepository<RestResponse> _RestRespRepository;
        private readonly IRepository<RestRequest> _RestReqRepository;
        private readonly IRepository<RestSchedule> _RestSchRepository;
        private readonly IRepository<EmployeesView> _EmployeesViewRepository;
        private readonly IRepository<RestInfo> _RestInfoRepository;
        private readonly IRepository<RestItem> _RestItemRepository;
        private readonly IRepository<RestScheduleItem> _RestScheduleItemRepository;
        private readonly IRepository<RestRequestItem> _RestRequestItemRepository;
        private readonly IRepository<RestNonSchItem> _RestNonSchItemRepository;
        private readonly IRepository<RequestStatus> _RequestStatusRepository;
        private readonly IRepository<RequestLog> _RequestLogRepository;
        private readonly IRepository<RestInfoAdmins> _RestInfoAdminsRepository;


        public RestaurantResponseAppService(IRepository<RestResponse> RestRespRepository,
                                           IRepository<RestRequest> RestReqRepository,
                                           IRepository<RestSchedule> RestSchRepository,
                                           IRepository<EmployeesView> EmployeesViewRepository,
                                           IRepository<RestInfo> RestInfoRepository,
                                           IRepository<RestItem> RestItemRepository,
                                           IRepository<RestScheduleItem> RestScheduleItem,
                                           IRepository<RestRequestItem> RestRequestItem,
                                           IRepository<RestNonSchItem> RestNonSchItemRepository,
                                           IRepository<RequestStatus> RequestStatusRepository,
                                           IRepository<RequestLog> RequestLogRepository,
                                           IRepository<RestInfoAdmins> RestInfoAdminsRepository)
        {
            _RestRespRepository = RestRespRepository;
            _RestReqRepository = RestReqRepository;
            _RestSchRepository = RestSchRepository;
            _EmployeesViewRepository = EmployeesViewRepository;
            _RestInfoRepository = RestInfoRepository;
            _RestItemRepository = RestItemRepository;
            _RestScheduleItemRepository = RestScheduleItem;
            _RestRequestItemRepository = RestRequestItem;
            _RestNonSchItemRepository = RestNonSchItemRepository;
            _RequestStatusRepository = RequestStatusRepository;
            _RequestLogRepository = RequestLogRepository;
            _RestInfoAdminsRepository = RestInfoAdminsRepository;
        }


        public PagedResultDto<RestRespListDto> GetRestResps(GetRestRespInput input)
        {
            var RestResps = _RestRespRepository.GetAll().OrderByDescending(a => a.CreationTime).ToList();

            var RestRespsCount = RestResps.Count();

            List<RestRespListDto> RestRespListDtos = ObjectMapper.Map<List<RestRespListDto>>(RestResps);

            return new PagedResultDto<RestRespListDto>(RestRespsCount, RestRespListDtos);
        }

        public PagedResultDto<RestReqListDto> GetRestReqs(GetRestReqInput input)
        {
            var loginSAPCode = GetCurrentUser().SAPCode;

            var RestReqs = _RestReqRepository.GetAll().Include(a => a.RestInfos.RestInfoAdmins)
                .Where(a => a.RequestStatus.StatusDesc != "Draft"
                             && a.RequestStatus.StatusDesc != "Cancel Order"
                             && a.RequestStatus.StatusDesc != "Done"
                             && a.RequestStatus.StatusDesc != "Not Available")
                .WhereIf(!input.Filter.IsNullOrEmpty(),
                    p => p.UserId.ToString().Contains(input.Filter) ||
                    p.PaymentTypes.PaymentTypeDesc.Contains(input.Filter))
                .OrderByDescending(a => a.CreationTime)
                .ToList();

            List<RestReqListDto> RestReqListDtos = new List<RestReqListDto>();

            foreach (var item in RestReqs)
            {
                var restadmin = _RestInfoAdminsRepository.GetAll().Where(a => a.RestInfoID == item.RestInfosID).ToList();

                foreach (var admin in restadmin)
                {
                    if (admin.emp_id == loginSAPCode)
                    {
                        RestReqListDto Filterditem = ObjectMapper.Map<RestReqListDto>(item);
                        RestReqListDtos.Add(Filterditem);
                    }
                }
            }

            var RestReqsCount = RestReqs.Count();


            foreach (RestReqListDto req in RestReqListDtos)
            {
                var RestSches = _RestSchRepository.GetAll().Where(a => a.Id == req.RestSchedulesId).ToList();

                List<RestSchesListDto> RestScheListDtos = ObjectMapper.Map<List<RestSchesListDto>>(RestSches);

                if (RestScheListDtos.Count() > 0)
                {
                    foreach (RestSchesListDto sch in RestScheListDtos)
                    {
                        var RestInfos = _RestInfoRepository.GetAll().Where(a => a.Id == sch.RestInfoID).ToList();

                        string Area = RestInfos.Select(a => a.Area).SingleOrDefault();
                        string Building = RestInfos.Select(a => a.Building).SingleOrDefault();

                        req.RestaurantInfo = Area + " - " + Building;
                        req.Day = sch.Day;

                    }

                    req.StatusDesc = _RequestStatusRepository.GetAll().Where(a => a.Id == req.RequestStatusID).Select(a => a.StatusDesc).SingleOrDefault();
                }
                else
                {
                    var RestInfos = _RestInfoRepository.GetAll().Where(a => a.Id == req.RestInfosID).ToList();

                    string Area = RestInfos.Select(a => a.Area).SingleOrDefault();
                    string Building = RestInfos.Select(a => a.Building).SingleOrDefault();

                    req.RestaurantInfo = Area + " - " + Building;

                    req.StatusDesc = _RequestStatusRepository.GetAll().Where(a => a.Id == req.RequestStatusID).Select(a => a.StatusDesc).SingleOrDefault();
                }

                req.Username = _EmployeesViewRepository.GetAll().Where(a => a.emp_id == req.UserId).Select(a => a.emp_username).SingleOrDefault();
                req.ExtNum = (int?)_EmployeesViewRepository.GetAll().Where(a => a.emp_id == req.UserId).Select(a => a.emp_ext).SingleOrDefault();
            }

            return new PagedResultDto<RestReqListDto>(RestReqsCount, RestReqListDtos);
        }

        public PagedResultDto<RestReqListDto> HistoryGetRestReqs()
        {
            var loginSAPCode = GetCurrentUser().SAPCode;

            var RestReqs = _RestReqRepository.GetAll()
                .OrderByDescending(a => a.CreationTime)
                .Where(a => a.RequestStatus.StatusDesc == "Done")
                .ToList();
            //&& a.RestInfos.AdminPerson == loginUserName
            var RestReqsCount = RestReqs.Count();

            List<RestReqListDto> RestReqListDtos = new List<RestReqListDto>();

            foreach (var item in RestReqs)
            {
                var restadmin = _RestInfoAdminsRepository.GetAll().Where(a => a.RestInfoID == item.RestInfosID).ToList();

                foreach (var admin in restadmin)
                {
                    if (admin.emp_id == loginSAPCode)
                    {
                        RestReqListDto Filterditem = ObjectMapper.Map<RestReqListDto>(item);
                        RestReqListDtos.Add(Filterditem);
                    }
                }
            }

            foreach (RestReqListDto req in RestReqListDtos)
            {
                var RestSches = _RestSchRepository.GetAll().Where(a => a.Id == req.RestSchedulesId).ToList();

                List<RestSchesListDto> RestScheListDtos = ObjectMapper.Map<List<RestSchesListDto>>(RestSches);

                if (RestScheListDtos.Count() > 0)
                {
                    foreach (RestSchesListDto sch in RestScheListDtos)
                    {
                        var RestInfos = _RestInfoRepository.GetAll().Where(a => a.Id == sch.RestInfoID).ToList();

                        string Area = RestInfos.Select(a => a.Area).SingleOrDefault();
                        string Building = RestInfos.Select(a => a.Building).SingleOrDefault();

                        req.RestaurantInfo = Area + " - " + Building;
                        req.Day = sch.Day;
                    }

                    req.StatusDesc = _RequestStatusRepository.GetAll().Where(a => a.Id == req.RequestStatusID).Select(a => a.StatusDesc).SingleOrDefault();
                }
                else
                {
                    var RestInfos = _RestInfoRepository.GetAll().Where(a => a.Id == req.RestInfosID).ToList();

                    string Area = RestInfos.Select(a => a.Area).SingleOrDefault();
                    string Building = RestInfos.Select(a => a.Building).SingleOrDefault();

                    req.RestaurantInfo = Area + " - " + Building;

                    req.StatusDesc = _RequestStatusRepository.GetAll().Where(a => a.Id == req.RequestStatusID).Select(a => a.StatusDesc).SingleOrDefault();
                }

                req.Username = _EmployeesViewRepository.GetAll().Where(a => a.emp_id == req.UserId).Select(a => a.emp_username).SingleOrDefault();
                req.ExtNum = (int?)_EmployeesViewRepository.GetAll().Where(a => a.emp_id == req.UserId).Select(a => a.emp_ext).SingleOrDefault();
            }

            return new PagedResultDto<RestReqListDto>(RestReqsCount, RestReqListDtos);
        }

        public List<RestItemsListDto> GetRestSchItems(GetRestReqSchInput input)
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

        public async Task SaveRestReqItems(List<RestItemsListDto> input, int RestRequestsId)
        {
            try
            {
                var RestReqitem = _RestRequestItemRepository.GetAll().Where(a => a.RestRequestsId == RestRequestsId && a.IsDeleted == false).ToList();

                List<RestRequestItem> RestReqItems = ObjectMapper.Map<List<RestRequestItem>>(RestReqitem);

                //

                var RestResponses = _RestRespRepository.GetAll().Where(a => a.IsDeleted == false).ToList();


                if (input.Count() > 0)
                {
                    for (int i = 0; i < input.Count(); i++)
                    {
                        if (input[i].NotAvailable == true && input.Count() != 1)
                        {
                            RestResponse exist = RestResponses.Find(a => a.RestRequestItemsId == input[i].RestRequestItemsId);

                            if (RestReqItems.Count() > 0)
                            {
                                if (exist != null)
                                {
                                    exist.NotAvailable = input[i].NotAvailable;
                                    await _RestRespRepository.UpdateAsync(exist);

                                    await RestStatusPartialNotAvailable(RestRequestsId);
                                }
                                else
                                {
                                    var reqresp = new RestResponse();

                                    reqresp.RestRequestItemsId = input[i].RestRequestItemsId;
                                    reqresp.NotAvailable = true;

                                    await _RestRespRepository.InsertAsync(reqresp);

                                    await RestStatusPartialNotAvailable(RestRequestsId);
                                }
                            }
                            else
                            {
                                var reqresp = new RestResponse();

                                reqresp.RestRequestItemsId = input[i].RestRequestItemsId;
                                reqresp.NotAvailable = true;

                                await _RestRespRepository.InsertAsync(reqresp);

                                await RestStatusPartialNotAvailable(RestRequestsId);
                            }
                        }
                        else if (input[i].NotAvailable == true && input.Count() == 1)
                        {
                            if (RestReqItems.Count() > 0)
                            {
                                var reqresp = new RestResponse();

                                reqresp.RestRequestItemsId = input[i].RestRequestItemsId;
                                reqresp.InternalCancel = true;

                                await _RestRespRepository.InsertAsync(reqresp);

                                await RestStatusPartialNotAvailable(RestRequestsId);
                            }
                        }
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                SqlException s = e.InnerException.InnerException as SqlException;
            }
        }

        public async Task RestStatusApprove(int RestRequestsId)
        {
            var RestReq = await _RestReqRepository.GetAsync(RestRequestsId);

            var ReqStatusId = _RequestStatusRepository.GetAll().Where(a => a.StatusDesc == "Approved").FirstOrDefault().Id;
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

        public async Task RestStatusPartialNotAvailable(int RestRequestsId)
        {
            var RestReq = await _RestReqRepository.GetAsync(RestRequestsId);

            var ReqStatusId = _RequestStatusRepository.GetAll().Where(a => a.StatusDesc == "Partial Not Available").FirstOrDefault().Id;
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

        public async Task RestStatusNotAvailable(int RestRequestsId)
        {
            var RestReq = await _RestReqRepository.GetAsync(RestRequestsId);

            var ReqStatusId = _RequestStatusRepository.GetAll().Where(a => a.StatusDesc == "Not Available").FirstOrDefault().Id;
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

        public async Task StatusStartProcess(int RestRequestsId)
        {
            var RestReq = await _RestReqRepository.GetAsync(RestRequestsId);

            var ReqStatusId = _RequestStatusRepository.GetAll().Where(a => a.StatusDesc == "In Process").FirstOrDefault().Id;
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

        public async Task StatusDone(int RestRequestsId)
        {
            var RestReq = await _RestReqRepository.GetAsync(RestRequestsId);

            var ReqStatusId = _RequestStatusRepository.GetAll().Where(a => a.StatusDesc == "Done").FirstOrDefault().Id;
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
