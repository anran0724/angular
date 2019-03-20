// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceContractsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos;

    /// <summary>
    /// The EccpMaintenanceContractsAppService interface.
    /// </summary>
    public interface IEccpMaintenanceContractsStopAppService : IApplicationService
    {
        Task<PagedResultDto<GetEccpMaintenanceContractForView>> GetAllStopMaintenanceContract(
            GetAllEccpMaintenanceContractsInput input);

        Task<ECCPBaseProblemElevatorDto> GetAllECCPBaseElevator(
            GetAllForLookupTableInput input);

        Task<PagedResultDto<ECCPBaseElevatorLookupTableDto>> GetAllECCPBaseElevatorForLookupTable(
            GetAllForLookupTableInput input);

        Task<PagedResultDto<ECCPBaseMaintenanceCompanyLookupTableDto>> GetAllECCPBaseMaintenanceCompanyForLookupTable(
            GetAllForLookupTableInput input);

        Task<PagedResultDto<ECCPBasePropertyCompanyLookupTableDto>> GetAllECCPBasePropertyCompanyForLookupTable(
            GetAllForLookupTableInput input);

        Task RecoveryContract(UploadRecoveryContractEccpMaintenanceContractDto input);

        Task<GetEccpMaintenanceContractForEditOutput> GetEccpMaintenanceContractForRecoveryContract(
            EntityDto<long> input);
    }
}