using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCV.Portal.Source.PortalNotifications.ServicesNotifications.Dto
{
    public class CreateNotificationServiceInput
    {
        [Required]
        public string ServiceNotificationMessage { get; set; }
        
        [Required]
        public string ServiceNotificationDescription { get; set; }
        
        [Required]
        public int EmailTemplateId { get; set; }

        [Required]
        public int PortalServiceId { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
