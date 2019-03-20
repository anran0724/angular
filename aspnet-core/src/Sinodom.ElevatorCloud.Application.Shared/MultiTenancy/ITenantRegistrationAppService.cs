using System.Threading.Tasks;
using Abp.Application.Services;
using Sinodom.ElevatorCloud.Editions.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.Dto;

namespace Sinodom.ElevatorCloud.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}
