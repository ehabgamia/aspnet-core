using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.PortalNotifications.EmailTemplates
{
    [Table("NotificationsEmailTemplates")]
    public class EmailTemplate : FullAuditedEntity
    {
        public string Subject { get; set; }

        public string MessageBody { get; set; }

        public string Description { get; set; }
    }
}
