// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpDictMaintenanceWorkFlowStatusesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The EccpDictMaintenanceWorkFlowStatusesAppService interface.
    /// </summary>
    public interface IEccpDictMaintenanceWorkFlowStatusesAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditEccpDictMaintenanceWorkFlowStatusDto input);

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
        Task<PagedResultDto<GetEccpDictMaintenanceWorkFlowStatusForView>> GetAll(
            GetAllEccpDictMaintenanceWorkFlowStatusesInput input);

        /// <summary>
        /// The get eccp dict maintenance work flow status for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpDictMaintenanceWorkFlowStatusForEditOutput> GetEccpDictMaintenanceWorkFlowStatusForEdit(
            EntityDto input);
    }
}