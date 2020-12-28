using System.Collections.Generic;
using MCV.Portal.Authorization.Permissions.Dto;

namespace MCV.Portal.Web.Areas.App.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}