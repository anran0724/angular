// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceWorkLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;

    /// <summary>
    /// The EccpMaintenanceWorkLogsAppService interface.
    /// </summary>
    public interface IEccpMaintenanceWorkLogsAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditEccpMaintenanceWorkLogDto input);

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
        Task<PagedResultDto<GetEccpMaintenanceWorkLogForView>> GetAll(GetAllEccpMaintenanceWorkLogsInput input);

        /// <summary>
        /// The get eccp maintenance work log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpMaintenanceWorkLogForEditOutput> GetEccpMaintenanceWorkLogForEdit(EntityDto<long> input);
    }
}