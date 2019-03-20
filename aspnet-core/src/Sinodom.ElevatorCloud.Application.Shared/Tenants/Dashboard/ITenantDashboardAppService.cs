using Abp.Application.Services;
using Sinodom.ElevatorCloud.Tenants.Dashboard.Dto;
using System.Threading.Tasks;

namespace Sinodom.ElevatorCloud.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        GetMemberActivityOutput GetMemberActivity();

        Task<GetDashboardDataOutput> GetDashboardData();

        GetSalesSummaryOutput GetSalesSummary(GetSalesSummaryInput input);

        GetRegionalStatsOutput GetRegionalStats(GetRegionalStatsInput input);

        GetGeneralStatsOutput GetGeneralStats(GetGeneralStatsInput input);
    }
}
