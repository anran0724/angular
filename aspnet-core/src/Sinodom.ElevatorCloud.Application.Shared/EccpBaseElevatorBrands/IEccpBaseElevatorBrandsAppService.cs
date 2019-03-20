// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpBaseElevatorBrandsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorBrands
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos;

    /// <summary>
    /// The EccpBaseElevatorBrandsAppService interface.
    /// </summary>
    public interface IEccpBaseElevatorBrandsAppService : IApplicationService
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
        Task CreateOrEdit(CreateOrEditEccpBaseElevatorBrandDto input);

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
        Task<PagedResultDto<GetEccpBaseElevatorBrandForView>> GetAll(GetAllEccpBaseElevatorBrandsInput input);

        /// <summary>
        /// The get all eccp base production company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<ECCPBaseProductionCompanyLookupTableDto>> GetAllECCPBaseProductionCompanyForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get eccp base elevator brand for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpBaseElevatorBrandForEditOutput> GetEccpBaseElevatorBrandForEdit(EntityDto input);

        /// <summary>
        /// The get eccp base elevator brands to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FileDto> GetEccpBaseElevatorBrandsToExcel(GetAllEccpBaseElevatorBrandsForExcelInput input);
    }
}