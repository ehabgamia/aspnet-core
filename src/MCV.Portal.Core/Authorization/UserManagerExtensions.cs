﻿using System.Threading.Tasks;
using Abp.Authorization.Users;
using MCV.Portal.Authorization.Users;

namespace MCV.Portal.Authorization
{
    public static class UserManagerExtensions
    {
        public static async Task<User> GetAdminAsync(this UserManager userManager)
        {
            return await userManager.FindByNameAsync(AbpUserBase.AdminUserName);
        }
    }
}
