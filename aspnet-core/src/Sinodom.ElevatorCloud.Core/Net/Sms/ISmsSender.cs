using System.Threading.Tasks;

namespace Sinodom.ElevatorCloud.Identity
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}