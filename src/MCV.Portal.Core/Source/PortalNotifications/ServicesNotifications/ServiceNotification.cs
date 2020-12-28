using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MCV.Portal.Source.PortalNotifications.EmailTemplates;
using MCV.Portal.Source.PortalServices;
using MCV.Portal.Source.ServicesNotifications;

namespace MCV.Portal.Source.PortalNotifications.ServicesNotifications
{
    [Table("ServicesNotifications")]
    public class ServiceNotification : FullAuditedEntity
    {
        public ServiceNotificationType ServiceNotificationType { get; set; }

        public string ServiceNotificationMessage { get; set; }

        public string ServiceNotificationDescription { get; set; }

        [ForeignKey("EmailTemplateId")]
        public virtual EmailTemplate EmailTemplate { get; set; }

        public int EmailTemplateId { get; set; }

        [ForeignKey("PortalServiceId")]
        public virtual PortalService PortalService { get; set; }

        public int PortalServiceId { get; set; }

        public bool Active { get; set; }

    }
}
