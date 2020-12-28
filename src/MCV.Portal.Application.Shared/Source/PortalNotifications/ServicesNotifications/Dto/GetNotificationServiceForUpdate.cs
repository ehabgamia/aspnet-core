using System;
using System.Collections.Generic;
using System.Text;

namespace MCV.Portal.Source.PortalNotifications.ServicesNotifications.Dto
{
    public class GetNotificationServiceForUpdate
    {
        public int Id { get; set; }
 
        public string ServiceNotificationMessage { get; set; }

        public string ServiceNotificationDescription { get; set; }

        public int EmailTemplateId { get; set; }

        public int PortalServiceId { get; set; }

        public bool Active { get; set; }
    }
}
