using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.MultiTenancy.HostDashboard.Dto
{
    public class GetRecentTenantsOutput
    {
        public List<RecentTenant> RecentTenants;

        public GetRecentTenantsOutput(List<RecentTenant> recentTenants)
        {
            RecentTenants = recentTenants;
        }
    }
}
