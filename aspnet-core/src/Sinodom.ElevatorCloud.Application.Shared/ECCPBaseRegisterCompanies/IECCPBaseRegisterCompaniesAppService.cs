// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IECCPBaseRegisterCompaniesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Dtos;

    /// <summary>
    /// The ECCPBaseRegisterCompaniesAppService interface.
    /// </summary>
    public interface IECCPBaseRegisterCompaniesAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditECCPBaseRegisterCompanyDto input);

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
        Task<PagedResultDto<GetECCPBaseRegisterCompanyForView>> GetAll(GetAllECCPBaseRegisterCompaniesInput input);

        /// <summary>
        /// The get all eccp base area for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<ECCPBaseAreaLookupTableDto>> GetAllECCPBaseAreaForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get eccp base register companies to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FileDto> GetECCPBaseRegisterCompaniesToExcel(GetAllECCPBaseRegisterCompaniesForExcelInput input);

        /// <summary>
        /// The get eccp base register company for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetECCPBaseRegisterCompanyForEditOutput> GetECCPBaseRegisterCompanyForEdit(EntityDto<long> input);
    }
}