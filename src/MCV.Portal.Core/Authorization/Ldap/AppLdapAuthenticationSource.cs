using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using MCV.Portal.Authorization.Users;
using MCV.Portal.MultiTenancy;

namespace MCV.Portal.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}