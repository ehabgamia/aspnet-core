using Abp.Domain.Entities.Auditing;
using MCV.Portal.Source.PortalNotifications.ServicesNotifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.PortalServices
{
    [Table("PortalServices")]
    public class PortalService : FullAuditedEntity
    {
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public ICollection<ServiceNotification> ServiceNotifications { get; set; }
    }
}
