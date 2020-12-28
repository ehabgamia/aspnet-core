using System;
using System.Collections.Generic;
using System.Text;

namespace MCV.Portal.Source.Anouncements.Dto
{
    public class AnnouncementListDto
    {
        public string Subject { get; set; }

        
        public string Message { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string UserName { get; set; }

        //public AnnouncementType AnnouncementType { get; set; }
    }
}
