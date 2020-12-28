using Abp.Runtime.Validation;
using MCV.Portal.Authorization.Users.Dto;
using MCV.Portal.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCV.Portal.Source.AnnouncementsTypes.Dto
{

    public class AnnouncementTypeListDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IconPath { get; set; }

        public int CreatorUserId { get; set; }

        public UserListDto User { get; set; }

    }
}
