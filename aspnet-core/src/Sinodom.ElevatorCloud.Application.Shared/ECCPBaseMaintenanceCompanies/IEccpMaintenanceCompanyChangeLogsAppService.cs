// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceCompanyChangeLogsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos;

    /// <summary>
    /// The EccpMaintenanceCompanyChangeLogsAppService interface.
    /// </summary>
    public interface IEccpMaintenanceCompanyChangeLogsAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditEccpMaintenanceCompanyChangeLogDto input);

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
        Task<PagedResultDto<GetEccpMaintenanceCompanyChangeLogForView>> GetAll(
            GetAllEccpMaintenanceCompanyChangeLogsInput input);

        /// <summary>
        /// The get all eccp base maintenance company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<ECCPBaseMaintenanceCompanyLookupTableDto>> GetAllECCPBaseMaintenanceCompanyForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get eccp maintenance company change log for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpMaintenanceCompanyChangeLogForEditOutput> GetEccpMaintenanceCompanyChangeLogForEdit(EntityDto input);
    }
}