using System.Collections.Generic;
using MCV.Portal.Authorization.Users.Dto;

namespace MCV.Portal.Web.Areas.App.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}