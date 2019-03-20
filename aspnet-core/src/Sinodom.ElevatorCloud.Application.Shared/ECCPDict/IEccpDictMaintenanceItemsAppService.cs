// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpDictMaintenanceItemsAppService.cs" company="Sinodom">
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
    /// The EccpDictMaintenanceItemsAppService interface.
    /// </summary>
    public interface IEccpDictMaintenanceItemsAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditEccpDictMaintenanceItemDto input);

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
        Task<PagedResultDto<GetEccpDictMaintenanceItemForView>> GetAll(GetAllEccpDictMaintenanceItemsInput input);

        /// <summary>
        /// The get eccp dict maintenance item for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpDictMaintenanceItemForEditOutput> GetEccpDictMaintenanceItemForEdit(EntityDto input);

        /// <summary>
        /// The get maintenance item template node all.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<EccpDictMaintenanceItemTemplateNodeDto[]> GetMaintenanceItemTemplateNodeAll();
    }
}