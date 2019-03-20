// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IECCPBaseAnnualInspectionUnitsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos;

    /// <summary>
    /// The ECCPBaseAnnualInspectionUnitsAppService interface.
    /// </summary>
    public interface IECCPBaseAnnualInspectionUnitsAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditECCPBaseAnnualInspectionUnitDto input);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Delete(EntityDto<long> input);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetECCPBaseAnnualInspectionUnitForView>> GetAll(
            GetAllECCPBaseAnnualInspectionUnitsInput input);

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
        /// The get eccp base annual inspection unit for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetECCPBaseAnnualInspectionUnitForEditOutput> GetECCPBaseAnnualInspectionUnitForEdit(EntityDto<long> input);

        /// <summary>
        /// The get eccp base annual inspection units to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FileDto> GetECCPBaseAnnualInspectionUnitsToExcel(GetAllECCPBaseAnnualInspectionUnitsForExcelInput input);
    }
}