using System.Collections.Generic;
using MCV.Portal.Authorization.Permissions.Dto;

namespace MCV.Portal.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}