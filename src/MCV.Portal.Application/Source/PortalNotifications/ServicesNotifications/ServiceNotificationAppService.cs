using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using MCV.Portal.Source.PortalNotifications.ServicesNotifications.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.PortalNotifications.ServicesNotifications
{
    public class ServiceNotificationAppService : PortalAppServiceBase, IServiceNotificationAppService
    {
        private readonly IRepository<ServiceNotification> _serviceNotification;

        public ServiceNotificationAppService(IRepository<ServiceNotification> serviceNotification)
        {
            _serviceNotification = serviceNotification;
        }

        public async Task CreateServiceNotification(CreateNotificationServiceInput input)
        {
            var notificationService = ObjectMapper.Map<ServiceNotification>(input);
            await _serviceNotification.InsertAsync(notificationService);
        }

        public async Task DeleteServiceNotification(EntityDto input)
        {
            await _serviceNotification.DeleteAsync(input.Id);
        }

        public async Task EditServiceNotification(UpdateNotificationServiceInput input)
        {
            var notificationService = await _serviceNotification.GetAsync(input.Id);
            notificationService.EmailTemplateId = input.EmailTemplateId;
            notificationService.PortalServiceId = input.PortalServiceId;
            notificationService.ServiceNotificationDescription = input.ServiceNotificationDescription;
            notificationService.ServiceNotificationMessage = input.ServiceNotificationMessage;
            notificationService.Active = input.Active;
            await _serviceNotification.UpdateAsync(notificationService);
        }

        public async Task<GetNotificationServiceForUpdate> GetServiceNotificationForUpdate(EntityDto input)
        {
            var serviceNotification = await _serviceNotification.GetAsync(input.Id);
            return ObjectMapper.Map<GetNotificationServiceForUpdate>(serviceNotification);
        }

        public ListResultDto<ServiceNotificationListDto> GetServiceNotificationsList(GetServiceNotificationFilter input)
        {
            var serviceNotifications = _serviceNotification
            .GetAllIncluding(a => a.EmailTemplate, a => a.PortalService)
            .WhereIf(
               !input.Filter.IsNullOrEmpty(),
               p => p.ServiceNotificationDescription.Contains(input.Filter)
            )
            .OrderBy(p => p.PortalServiceId)
            .ToList();


            var serviceNotificationCount = serviceNotifications.Count();

            return new ListResultDto<ServiceNotificationListDto>(ObjectMapper.Map<List<ServiceNotificationListDto>>(serviceNotifications));
        }
    }
}
