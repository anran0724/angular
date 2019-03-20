// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpBaseElevatorLabelBindLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorLabels
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos;

    /// <summary>
    /// The EccpBaseElevatorLabelBindLogsAppService interface.
    /// </summary>
    public interface IEccpBaseElevatorLabelBindLogsAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditEccpBaseElevatorLabelBindLogDto input);

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
        Task<PagedResultDto<GetEccpBaseElevatorLabelBindLogForView>> GetAll(
            GetAllEccpBaseElevatorLabelBindLogsInput input);

        /// <summary>
        /// The get all eccp base elevator for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpBaseElevatorLookupTableDto>> GetAllEccpBaseElevatorForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp dict label status for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpDictLabelStatusLookupTableDto>> GetAllEccpDictLabelStatusForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get eccp base elevator label bind log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpBaseElevatorLabelBindLogForEditOutput> GetEccpBaseElevatorLabelBindLogForEdit(EntityDto<long> input);
    }
}