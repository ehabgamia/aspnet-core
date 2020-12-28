using System.Collections.Generic;
using Abp.Application.Services.Dto;
using MCV.Portal.Authorization.Permissions.Dto;
using MCV.Portal.Web.Areas.App.Models.Common;

namespace MCV.Portal.Web.Areas.App.Models.Roles
{
    public class RoleListViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}