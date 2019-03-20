using Abp.Domain.Services;

namespace Sinodom.ElevatorCloud
{
    public abstract class ElevatorCloudDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected ElevatorCloudDomainServiceBase()
        {
            LocalizationSourceName = ElevatorCloudConsts.LocalizationSourceName;
        }
    }
}
