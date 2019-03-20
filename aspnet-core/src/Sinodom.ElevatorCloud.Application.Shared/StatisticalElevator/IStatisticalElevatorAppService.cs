using Abp.Application.Services;
using Sinodom.ElevatorCloud.StatisticalElevator.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sinodom.ElevatorCloud.StatisticalElevator
{
    public interface IStatisticalElevatorAppService : IApplicationService
    {
        Task<GetRegionalCollectionForView> RegionalCollection(GetRegionalCollectionInput input);

        Task<GetElevatorCollectionForView> ElevatorCollection(GetElevatorCollectionInput input);

        Task<GetRegionalPersonCollectionForView> RegionalPersonCollection(GetRegionalPersonCollectionInput input);

        Task<GetPersonnelDistributionForView> PersonnelDistribution(GetPersonnelDistributionInput input);
    }
}
