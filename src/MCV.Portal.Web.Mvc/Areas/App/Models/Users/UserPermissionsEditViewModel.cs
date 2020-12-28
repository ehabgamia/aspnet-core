using Abp.AutoMapper;
using MCV.Portal.Authorization.Users;
using MCV.Portal.Authorization.Users.Dto;
using MCV.Portal.Web.Areas.App.Models.Common;

namespace MCV.Portal.Web.Areas.App.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}