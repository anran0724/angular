// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceTemplateNodesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos;

    /// <summary>
    /// The EccpMaintenanceTemplateNodesAppService interface.
    /// </summary>
    public interface IEccpMaintenanceTemplateNodesAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditEccpMaintenanceTemplateNodeDto input);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="pid">
        /// The pid.
        /// </param>
        /// <param name="maintenanceTypeId">
        /// The maintenance type id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Delete(EntityDto input, int pid, int maintenanceTypeId);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpMaintenanceTemplateNodeForView>> GetAll(
            GetAllEccpMaintenanceTemplateNodesInput input);

        /// <summary>
        /// The get all eccp dict node type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpDictNodeTypeLookupTableDto>> GetAllEccpDictNodeTypeForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp maintenance next node for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpMaintenanceNextNodeLookupTableDto>> GetAllEccpMaintenanceNextNodeForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get app maintenance template nodes.
        /// </summary>
        /// <param name="templateId">
        /// The template id.
        /// </param>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        List<AppEccpMaintenanceTemplateNodeTreeDto> GetAppMaintenanceTemplateNodes(int templateId);

        /// <summary>
        /// The get eccp maintenance template node for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpMaintenanceTemplateNodeForEditOutput> GetEccpMaintenanceTemplateNodeForEdit(EntityDto input);

        /// <summary>
        /// The get maintenance template nodes.
        /// </summary>
        /// <param name="templateId">
        /// The template id.
        /// </param>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        List<EccpMaintenanceTemplateNodeTreeDto> GetMaintenanceTemplateNodes(int templateId);
    }
}