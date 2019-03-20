// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpBaseElevatorModelsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorModels
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos;

    /// <summary>
    /// The EccpBaseElevatorModelsAppService interface.
    /// </summary>
    public interface IEccpBaseElevatorModelsAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditEccpBaseElevatorModelDto input);

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
        Task<PagedResultDto<GetEccpBaseElevatorModelForView>> GetAll(GetAllEccpBaseElevatorModelsInput input);

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
        /// The get eccp base elevator model for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpBaseElevatorModelForEditOutput> GetEccpBaseElevatorModelForEdit(EntityDto input);

        /// <summary>
        /// The get eccp base elevator models to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FileDto> GetEccpBaseElevatorModelsToExcel(GetAllEccpBaseElevatorModelsForExcelInput input);
    }
}