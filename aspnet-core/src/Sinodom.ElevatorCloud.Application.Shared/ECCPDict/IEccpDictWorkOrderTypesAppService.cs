// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpDictWorkOrderTypesAppService.cs" company="Sinodom">
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
    /// The EccpDictWorkOrderTypesAppService interface.
    /// </summary>
    public interface IEccpDictWorkOrderTypesAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditEccpDictWorkOrderTypeDto input);

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
        Task<PagedResultDto<GetEccpDictWorkOrderTypeForView>> GetAll(GetAllEccpDictWorkOrderTypesInput input);

        /// <summary>
        /// The get eccp dict work order type for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpDictWorkOrderTypeForEditOutput> GetEccpDictWorkOrderTypeForEdit(EntityDto input);
    }
}