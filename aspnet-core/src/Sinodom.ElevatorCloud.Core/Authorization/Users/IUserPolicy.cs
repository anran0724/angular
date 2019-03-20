using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace Sinodom.ElevatorCloud.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
