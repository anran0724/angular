// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpBaseElevatorsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;

    /// <summary>
    /// The EccpBaseElevatorsAppService interface.
    /// </summary>
    public interface IEccpBaseElevatorsAppService : IApplicationService
    {
        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task CreateOrEdit(CreateOrEditEccpBaseElevatorDto input);

        /// <summary>
        /// The claim elevators.
        /// </summary>
        /// <param name="ids">
        /// The ids.
        /// </param>
        /// <param name="eccpBaseMaintenanceCompanyId">
        /// The eccp base maintenance company id.
        /// </param>
        /// <param name="eccpBasePropertyCompanyId">
        /// The eccp base property company id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task ClaimElevators(List<Guid> ids, int? eccpBaseMaintenanceCompanyId, int? eccpBasePropertyCompanyId);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Delete(EntityDto<Guid> input);

        /// <summary>
        /// The elevator data synchronization.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int ElevatorDataSynchronization(ElevatorDataSynchronizationDto input);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpBaseElevatorForView>> GetAll(GetAllEccpBaseElevatorsInput input);

        /// <summary>
        /// The get all eccp base annual inspection unit for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<ECCPBaseAnnualInspectionUnitLookupTableDto>> GetAllECCPBaseAnnualInspectionUnitForLookupTable(GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp base area for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<ECCPBaseAreaLookupTableDto>> GetAllECCPBaseAreaForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp base community for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<ECCPBaseCommunityLookupTableDto>> GetAllECCPBaseCommunityForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp base elevator brand for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpBaseElevatorBrandLookupTableDto>> GetAllEccpBaseElevatorBrandForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp base elevator model for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpBaseElevatorModelLookupTableDto>> GetAllEccpBaseElevatorModelForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp base maintenance company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<ECCPBaseMaintenanceCompanyLookupTableDto>> GetAllECCPBaseMaintenanceCompanyForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp base production company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<ECCPBaseProductionCompanyLookupTableDto>> GetAllECCPBaseProductionCompanyForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp base property company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<ECCPBasePropertyCompanyLookupTableDto>> GetAllECCPBasePropertyCompanyForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp base register company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<ECCPBaseRegisterCompanyLookupTableDto>> GetAllECCPBaseRegisterCompanyForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp dict elevator status for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<ECCPDictElevatorStatusLookupTableDto>> GetAllECCPDictElevatorStatusForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp dict elevator type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpDictElevatorTypeLookupTableDto>> GetAllEccpDictElevatorTypeForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp dict place type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpDictPlaceTypeLookupTableDto>> GetAllEccpDictPlaceTypeForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all work order evaluation by elevator id.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpElevatorMaintenanceWorkOrderEvaluationForView>> GetAllWorkOrderEvaluationByElevatorId(GetAllByElevatorIdEccpMaintenanceWorkOrderEvaluationsInput input);

        /// <summary>
        /// The get eccp base elevator for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpBaseElevatorForEditOutput> GetEccpBaseElevatorForEdit(EntityDto<Guid> input);

        /// <summary>
        /// The get eccp base elevators to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FileDto> GetEccpBaseElevatorsToExcel(GetAllEccpBaseElevatorsForExcelInput input);

        /// <summary>
        /// The get eccp base elevator subsidiary info dto.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="EccpBaseElevatorSubsidiaryInfoDto"/>.
        /// </returns>
        EccpBaseElevatorSubsidiaryInfoDto GetEccpBaseElevatorSubsidiaryInfoDto(EntityDto<Guid> input);


        List<GetChangeLogByElevatorIdDto> GetAllChangeLogByElevatorId(string elevatorId);
    }
}