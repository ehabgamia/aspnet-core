using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCV.Portal.Source.PortalNotifications.EmailTemplates.Dto
{
    public class CreateEmailTemplateInput
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string MessageBody { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
