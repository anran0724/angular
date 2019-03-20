using System.Threading.Tasks;
using Sinodom.ElevatorCloud.Sessions.Dto;

namespace Sinodom.ElevatorCloud.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
