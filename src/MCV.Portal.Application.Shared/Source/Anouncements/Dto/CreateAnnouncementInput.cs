using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCV.Portal.Source.Anouncements.Dto
{
    public class CreateAnnouncementInput
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
        
        [Required]
        public DateTime EntryDate { get; set; }
        
        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public int AnnouncementTypeId { get; set; }
    }
}
