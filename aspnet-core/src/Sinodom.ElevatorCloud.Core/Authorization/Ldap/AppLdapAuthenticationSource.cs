using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.MultiTenancy;

namespace Sinodom.ElevatorCloud.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}
