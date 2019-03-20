using System.Threading.Tasks;
using Abp.Application.Services;
using Sinodom.ElevatorCloud.LanFlows.Dtos;

namespace Sinodom.ElevatorCloud.LanFlows
{
    /// <summary>
    ///  
    /// </summary>
    public interface IEccpFlowInstancesAppService : IApplicationService
    {
        Task<object> GetFlowInstancesList(GetAllLanFlowsQueryInput input);

        Task<bool> UpdateFlowInstances(UpdateFlowStatusAction input);
    }
}