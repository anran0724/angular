// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IECCPDictElevatorStatusesAppService.cs" company="Sinodom">
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
    /// The ECCPDictElevatorStatusesAppService interface.
    /// </summary>
    public interface IECCPDictElevatorStatusesAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditECCPDictElevatorStatusDto input);

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
        Task<PagedResultDto<GetECCPDictElevatorStatusForView>> GetAll(GetAllECCPDictElevatorStatusesInput input);

        /// <summary>
        /// The get eccp dict elevator status for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetECCPDictElevatorStatusForEditOutput> GetECCPDictElevatorStatusForEdit(EntityDto input);
    }
}