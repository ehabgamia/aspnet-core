using Abp.Domain.Entities.Auditing;
using MCV.Portal.Authorization.Users;
using MCV.Portal.Source.Announcements;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.AnnouncementsTypes
{
    [Table("AnnouncementTypes")]
    public class AnnouncementType : FullAuditedEntity
    {
        public const int MaxNameLength = 150;

        [Required]
        [MaxLength(MaxNameLength)]
        public virtual string Name { get; set; }

        [Required]
        public virtual string IconPath { get; set; }

        public ICollection<Announcement> Announcements { get; set; }

        [ForeignKey("CreatorUserId")]
        public virtual User User { get; set; }
    }
}
