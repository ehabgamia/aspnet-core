using Abp.Domain.Entities.Auditing;
using MCV.Portal.Authorization.Users;
using MCV.Portal.Source.AnnouncementsTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.Announcements
{
    [Table("Announcements")]
    public class Announcement : FullAuditedEntity
    {
        public const int MaxNameLength = 150;

        [ForeignKey("AnnouncementTypeId")]
        public virtual AnnouncementType AnnouncementType { get; set; }
        public virtual int AnnouncementTypeId { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public virtual string Subject { get; set; }

        [Required]
        public virtual string Message { get; set; }

        public virtual DateTime EntryDate { get; set; }

        public virtual DateTime ExpiryDate { get; set; }

        [ForeignKey("CreatorUserId")]
        public virtual User User { get; set; }

    }
}
