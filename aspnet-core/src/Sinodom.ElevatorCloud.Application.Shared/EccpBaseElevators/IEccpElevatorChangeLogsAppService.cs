// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpElevatorChangeLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;

    /// <summary>
    /// The EccpElevatorChangeLogsAppService interface.
    /// </summary>
    public interface IEccpElevatorChangeLogsAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditEccpElevatorChangeLogDto input);

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
        Task<PagedResultDto<GetEccpElevatorChangeLogForView>> GetAll(GetAllEccpElevatorChangeLogsInput input);

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
        /// The get eccp elevator change log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpElevatorChangeLogForEditOutput> GetEccpElevatorChangeLogForEdit(EntityDto input);
    }
}