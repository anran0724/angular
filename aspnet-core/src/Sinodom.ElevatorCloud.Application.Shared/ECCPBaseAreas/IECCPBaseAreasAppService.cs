// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IECCPBaseAreasAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAreas
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos;
    using Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos;

    using GetAllForLookupTableInput =
        ECCPBaseAnnualInspectionUnits.Dtos.GetAllForLookupTableInput;

    /// <summary>
    /// The ECCPBaseAreasAppService interface.
    /// </summary>
    public interface IECCPBaseAreasAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditECCPBaseAreaDto input);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Delete(EntityDto input);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetECCPBaseAreaForView>> GetAll(GetAllECCPBaseAreasInput input);

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
        /// The get eccp base area for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetECCPBaseAreaForEditOutput> GetECCPBaseAreaForEdit(EntityDto input);

        /// <summary>
        /// The get eccp base areas to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FileDto> GetECCPBaseAreasToExcel(GetAllECCPBaseAreasForExcelInput input);
    }
}