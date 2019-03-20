// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceWorkFlowsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.AppReturnModel;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;

    /// <summary>
    /// The EccpMaintenanceWorkFlowsAppService interface.
    /// </summary>
    public interface IEccpMaintenanceWorkFlowsAppService : IApplicationService
    {
        /// <summary>
        /// The app create.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<ReturnModel> AppCreate(List<CreateOrEditEccpMaintenanceWorkFlowDto> list);

        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task CreateOrEdit(CreateOrEditEccpMaintenanceWorkFlowDto input);

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
        Task<PagedResultDto<GetEccpMaintenanceWorkFlowForView>> GetAll(GetAllEccpMaintenanceWorkFlowsInput input);

        /// <summary>
        /// The get all eccp dict maintenance work flow status for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpDictMaintenanceWorkFlowStatusLookupTableDto>> GetAllEccpDictMaintenanceWorkFlowStatusForLookupTable(GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp maintenance template node for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpMaintenanceTemplateNodeLookupTableDto>> GetAllEccpMaintenanceTemplateNodeForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp maintenance work for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpMaintenanceWorkLookupTableDto>> GetAllEccpMaintenanceWorkForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get eccp maintenance work flow for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpMaintenanceWorkFlowForEditOutput> GetEccpMaintenanceWorkFlowForEdit(EntityDto<Guid> input);
    }
}