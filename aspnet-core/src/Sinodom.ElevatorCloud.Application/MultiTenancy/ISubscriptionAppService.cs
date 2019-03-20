using System.Threading.Tasks;
using Abp.Application.Services;

namespace Sinodom.ElevatorCloud.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task UpgradeTenantToEquivalentEdition(int upgradeEditionId);
    }
}
