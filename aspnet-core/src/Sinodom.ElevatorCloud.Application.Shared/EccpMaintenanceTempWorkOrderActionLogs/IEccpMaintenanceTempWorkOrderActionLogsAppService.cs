// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceTempWorkOrderActionLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs
{
    using System;
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs.Dtos;

    /// <summary>
    /// The EccpMaintenanceTempWorkOrderActionLogsAppService interface.
    /// </summary>
    public interface IEccpMaintenanceTempWorkOrderActionLogsAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditEccpMaintenanceTempWorkOrderActionLogDto input);

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
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpMaintenanceTempWorkOrderActionLogForView>> GetAll(
            GetAllEccpMaintenanceTempWorkOrderActionLogsInput input);

        /// <summary>
        /// The get all eccp maintenance temp work order for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpMaintenanceTempWorkOrderLookupTableDto>> GetAllEccpMaintenanceTempWorkOrderForLookupTable(GetAllForLookupTableInput input);

        /// <summary>
        /// The get all user for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<UserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);

        /// <summary>
        /// The get eccp maintenance temp work order action log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpMaintenanceTempWorkOrderActionLogForEditOutput> GetEccpMaintenanceTempWorkOrderActionLogForEdit(
            EntityDto<Guid> input);
    }
}