using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.PortalNotifications.ServicesNotifications.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.PortalNotifications.ServicesNotifications
{
    public interface IServiceNotificationAppService : IApplicationService
    {
        ListResultDto<ServiceNotificationListDto> GetServiceNotificationsList(GetServiceNotificationFilter input);

        Task CreateServiceNotification(CreateNotificationServiceInput input);

        Task DeleteServiceNotification(EntityDto input);

        Task<GetNotificationServiceForUpdate> GetServiceNotificationForUpdate(EntityDto input);

        Task EditServiceNotification(UpdateNotificationServiceInput input);
    }
}
