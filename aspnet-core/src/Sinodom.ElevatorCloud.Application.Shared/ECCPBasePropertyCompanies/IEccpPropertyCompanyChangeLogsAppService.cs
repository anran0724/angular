// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpPropertyCompanyChangeLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos;

    /// <summary>
    /// The EccpPropertyCompanyChangeLogsAppService interface.
    /// </summary>
    public interface IEccpPropertyCompanyChangeLogsAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditEccpPropertyCompanyChangeLogDto input);

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
        Task<PagedResultDto<GetEccpPropertyCompanyChangeLogForView>> GetAll(
            GetAllEccpPropertyCompanyChangeLogsInput input);

        /// <summary>
        /// The get all eccp base property company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<ECCPBasePropertyCompanyLookupTableDto>> GetAllECCPBasePropertyCompanyForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get eccp property company change log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpPropertyCompanyChangeLogForEditOutput> GetEccpPropertyCompanyChangeLogForEdit(EntityDto input);
    }
}