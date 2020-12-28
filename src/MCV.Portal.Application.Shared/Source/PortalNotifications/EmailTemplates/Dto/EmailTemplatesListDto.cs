using System;
using System.Collections.Generic;
using System.Text;

namespace MCV.Portal.Source.PortalNotifications.EmailTemplates.Dto
{
    public class EmailTemplatesListDto
    {
        public int Id { get; set; }

        public string MessageBody { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }
    }
}
