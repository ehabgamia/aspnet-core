using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MCV.Portal.Authorization.Users.Dto;
using MCV.Portal.Source.AnnouncementsTypes.Dto;

namespace MCV.Portal.Source.Anouncements.Dto
{
    public class AnnouncementsListDto
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public int AnnouncementTypeId { get; set; }

        public AnnouncementTypeListDto AnnouncementType { get; set; }

        public int CreatorUserId { get; set; }

        public UserListDto User { get; set; }
    }
}
