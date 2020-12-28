using Abp.Authorization;
using MCV.Portal.Authorization.Roles;
using MCV.Portal.Authorization.Users;

namespace MCV.Portal.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
